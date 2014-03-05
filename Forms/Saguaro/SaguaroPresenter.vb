#Region "imports"
Imports System.Windows.Forms
Imports System
Imports System.IO
Imports System.Threading
#End Region

Public Class SaguaroPresenter
    Inherits MVPPresenterBase

#Region "Inner Classes"
    Private Class columnIx
        Public Ix As Int32
    End Class

    Private Class writeParms
        Public CoordExpRA As ICoordExp = DatafileHMS.GetInstance
        Public CoordExpDec As ICoordExp = DatafileDMS.GetInstance
        Public SB As New Text.StringBuilder
        Public Writer As StreamWriter
    End Class
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    ' column names are:
    '0: 
    '1: OBJECT          
    '2: OTHER             
    '3: TYPE 
    '4: CON
    '5: RA     
    '6: DEC   
    '7: MAG 
    '8: SUBR
    '9: U2K
    '10: TI
    '11: SIZE_MAX
    '12: SIZE_MIN
    '13: PA 
    '14: CLASS      
    '15: NSTS
    '16: BRSTR
    '17: BCHM
    '18: NGC DESCR                                                  
    '19: NOTES                                                                                 
    '20: 
    Private columnNames() As String

    Private pData As ArrayList
    Private pCoordinateParser As CoordinateParser
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As SaguaroPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SaguaroPresenter = New SaguaroPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As SaguaroPresenter
        Return New SaguaroPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        AddHandler frmSaguaro.LoadFile, AddressOf loadFile
        AddHandler frmSaguaro.SaveFile, AddressOf savefile

        pData = New ArrayList
        pCoordinateParser = CoordinateParser.GetInstance

        With ScopeLibrary.Settings.GetInstance
            frmSaguaro.txBxEpochFrom.Text = CStr(.SaguaroEpoch)
            frmSaguaro.txBxEpochTo.Text = .DatafilesEpoch
        End With
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Function frmSaguaro() As FrmSaguaro
        Return CType(IMVPView, FrmSaguaro)
    End Function

    Private Sub loadFile()
        Dim loadFileThread As New Thread(AddressOf loadFileSubr)
        loadFileThread.SetApartmentState(ApartmentState.STA)
        loadFileThread.Start()
    End Sub

    Private Sub loadFileSubr()
        Dim openFileDialog As New OpenFileDialog
        openFileDialog.Title = "Select the file"
        openFileDialog.Filter = "Saguaro Datafile|SAC*Fence.txt"
        openFileDialog.FilterIndex = 1
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            readFile(openFileDialog.FileName())
        End If
    End Sub

    Private Function convertStringToInt32(ByVal [string] As String) As Int32
        Dim result As Int32 = 0
        Int32.TryParse([string], result)
        Return result
    End Function

    Private Sub readFile(ByVal filename As String)
        Dim settings As ScopeLibrary.Settings = ScopeLibrary.Settings.GetInstance
        Dim precession As Precession = Coordinates.Precession.GetInstance
        Dim position As Position = PositionArray.GetInstance.GetPosition

        Dim coordExpRA As ICoordExp = HMS.GetInstance
        Dim coordExpDec As ICoordExp = DMS.GetInstance
        Dim deltaYr As Double = frmSaguaro.EpochDeltaYr

        Dim st As StringTokenizer = StringTokenizer.GetInstance
        Dim delimiter As Char() = "|".ToCharArray

        Dim SaguaroObjectNameTokenizer As StringTokenizer = StringTokenizer.GetInstance
        SaguaroObjectNameTokenizer.Tokenize(settings.SaguaroObjectNameColumns, ",".ToCharArray)
        Dim SaguaroObjectNameSequence() As Int32 = _
                Array.ConvertAll(Of String, Int32)(SaguaroObjectNameTokenizer.Tokens, AddressOf convertStringToInt32)

        pData.Clear()
        columnNames = Nothing

        Dim recordCounter As Int32 = 0
        Dim reader As New StreamReader(filename)

        ' read data line by line
        Dim line As String = reader.ReadLine
        While line IsNot Nothing
            st.Tokenize(line, delimiter)
            Dim dataRowString(st.Tokens.Length - 1) As String
            Array.Copy(st.Tokens, dataRowString, st.Tokens.Length - 1)
            ' get column names from 1st line
            If columnNames Is Nothing Then
                columnNames = dataRowString
                Array.ForEach(columnNames, AddressOf New SubDelegate(Of String, Object) _
                    (AddressOf displayColumn, New columnIx).CallDelegate)
            Else
                Dim lwpos As LWPosition = LWPosition.GetInstance

                ' compute coordinates, examples of starting strings (RA and Dec) are: 00 07.3   and   +32 37
                lwpos.RA = pCoordinateParser.Parse(dataRowString(settings.SaguaroRAColumn) & " h")
                lwpos.Dec = pCoordinateParser.Parse(dataRowString(settings.SaguaroDecColumn) & " d")
                position.RA.Rad = lwpos.RA
                position.Dec.Rad = lwpos.Dec
                precession.Calc(position, deltaYr)
                lwpos.RA = eMath.ValidRad(lwpos.RA + precession.DeltaRa)
                lwpos.Dec += precession.DeltaDec

                lwpos.RADisplay = coordExpRA.ToString(lwpos.RA)
                lwpos.DecDisplay = coordExpDec.ToString(lwpos.Dec)

                ' compile object name
                Dim sb As New Text.StringBuilder
                Array.ForEach(SaguaroObjectNameSequence, AddressOf New SubDelegate(Of Int32, Object) _
                    (AddressOf appendStringToSB, New Object() {dataRowString, sb}).CallDelegate)
                ' get rid of ending comma(s)
                lwpos.Name = sb.ToString.TrimEnd(",".ToCharArray)

                ' add to array of lightweight positions
                pData.Add(lwpos)
            End If

            recordCounter += 1
            line = reader.ReadLine
        End While

        frmSaguaro.DataSource = pData

        reader.Close()
    End Sub

    Private Sub displayColumn(ByVal [string] As String, ByVal columnIx As Object)
        DebugTrace.WriteLine(CType(columnIx, columnIx).Ix & ": " & [string])
        CType(columnIx, columnIx).Ix += 1
    End Sub

    Private Sub appendStringToSB(ByVal ix As Int32, ByVal [object] As Object)
        Dim dataRowString() As String = CType(CType([object], Object())(0), String())
        Dim sb As Text.StringBuilder = CType(CType([object], Object())(1), Text.StringBuilder)
        sb.Append(dataRowString(ix).Trim)
        sb.Append(",")
    End Sub

    Private Sub savefile()
        Dim saveFileThread As New Thread(AddressOf savefileSubr)
        saveFileThread.SetApartmentState(ApartmentState.STA)
        saveFileThread.Start()
    End Sub

    Private Sub savefileSubr()
        Dim saveFileDialog As New SaveFileDialog
        saveFileDialog.Title = "Save the file"
        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            writeFile(saveFileDialog.FileName())
        End If
    End Sub

    Private Sub writeFile(ByVal filename As String)
        Dim writeParms As New writeParms
        writeParms.Writer = New StreamWriter(filename)

        Array.ForEach(pData.ToArray, AddressOf New SubDelegate(Of Object, writeParms) _
            (AddressOf writeObject, writeParms).CallDelegate)

        writeParms.Writer.Close()
        AppMsgBox.Show(filename & " successfully saved.")
    End Sub

    Private Sub writeObject(ByVal lwpos As Object, ByVal writeParms As writeParms)
        Dim lp As LWPosition = CType(lwpos, LWPosition)

        With writeParms
            .SB.Remove(0, .SB.Length)

            .SB.Append(.CoordExpRA.ToString(lp.RA))
            .SB.Append("   ")
            .SB.Append(.CoordExpDec.ToString(lp.Dec))
            .SB.Append("   ")
            .SB.Append(lp.Name)

            .Writer.WriteLine(.SB.ToString)
        End With
    End Sub
#End Region

End Class
