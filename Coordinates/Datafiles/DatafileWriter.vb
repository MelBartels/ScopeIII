#Region "Imports"
Imports System.IO
#End Region

Public Class DatafileWriter

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Dim pCetRA As ISFT
    Dim pCetDec As ISFT
    Private pObservableImp As ObservableImp
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DatafileWriter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DatafileWriter = New DatafileWriter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        pCetRA = CoordExpType.DatafileHMS
        pCetDec = CoordExpType.DatafileDMS
        pObservableImp = ObservableImp.GetInstance
    End Sub

    Public Shared Function GetInstance() As DatafileWriter
        Return New DatafileWriter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property ObservableImp() As ObservableImp
        Get
            Return pObservableImp
        End Get
        Set(ByVal Value As ObservableImp)
            pObservableImp = Value
        End Set
    End Property

    Public Function Write(ByVal filename As String, ByRef positionArray As ArrayList) As Boolean
        Dim writer As StreamWriter
        writer = New StreamWriter(filename)
        Dim ix As Int32

        For Each p As Object In positionArray
            If (ix Mod 1000).Equals(0) Then
                pObservableImp.Notify("Saved " & ix & " of " & positionArray.Count & " objects.")
            End If
            ix += 1

            Dim RA As Coordinate = Coordinate.GetInstance
            Dim Dec As Coordinate = Coordinate.GetInstance
            Dim objName As String

            If p.GetType Is GetType(LWPosition) Then
                Dim lwpos As LWPosition = CType(p, LWPosition)
                RA.Rad = lwpos.RA
                Dec.Rad = lwpos.Dec
                objName = lwpos.Name
            ElseIf p.GetType Is GetType(Position) Then
                Dim position As Position = CType(p, Position)
                RA = position.RA
                Dec = position.Dec
                objName = position.ObjName
            Else
                Return False
            End If

            writer.Write(RA.ToString(pCetRA) & BartelsLibrary.Constants.Delimiter)
            writer.Write(Dec.ToString(pCetDec) & BartelsLibrary.Constants.Delimiter)
            writer.Write(objName)
            writer.WriteLine(String.Empty)
        Next

        writer.Close()
        Return True
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
