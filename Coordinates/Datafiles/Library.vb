Imports System.IO

Public Class Library

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pObjects As ArrayList
    Private pSources As ArrayList
    Private pObservableImp As ObservableImp
    Private pCoordinate As Coordinate
    Private pCancel As Boolean
#End Region

#Region "Constructors (Singleton Pattern)"
    Private Sub New()
        pObservableImp = ObservableImp.GetInstance
        pCoordinate = Coordinates.Coordinate.GetInstance
    End Sub

    Public Shared Function GetInstance() As Library
        Return NestedInstance.INSTANCE
    End Function

    Private Class NestedInstance
        ' explicit constructor informs compiler not to mark type as beforefieldinit
        Shared Sub New()
        End Sub
        ' friend = internal, shared = static, readonly = final
        Friend Shared ReadOnly INSTANCE As Library = New Library
    End Class
#End Region

#Region "Constructors"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As Library
    '    Return New Library
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property Objects() As ArrayList
        Get
            Return pObjects
        End Get
        Set(ByVal Value As ArrayList)
            pObjects = Value
        End Set
    End Property

    Public Property Sources() As ArrayList
        Get
            Return pSources
        End Get
        Set(ByVal Value As ArrayList)
            pSources = Value
        End Set
    End Property

    Public Property ObservableImp() As ObservableImp
        Get
            Return pObservableImp
        End Get
        Set(ByVal Value As ObservableImp)
            pObservableImp = Value
        End Set
    End Property

    Public Property Cancel() As Boolean
        Get
            Return pCancel
        End Get
        Set(ByVal value As Boolean)
            pCancel = value
        End Set
    End Property

    Public Function GetDatafilesFromDirectory(ByVal directory As String) As String()
        Return System.IO.Directory.GetFiles(directory, "*." & BartelsLibrary.Constants.DatafilesExtension)
    End Function

    Public Function Load(ByVal datafiles As String()) As Boolean
        If datafiles Is Nothing OrElse datafiles.Length.Equals(0) Then
            Return False
        End If

        pObjects = New ArrayList
        pSources = New ArrayList
        ' sets source, name, RA, Dec
        Array.ForEach(datafiles, AddressOf loadFile)
        ' sets formatted RA, Dec 
        SetCoordDisplays()
        Return True
    End Function

    Public Function Save(ByVal filepath As String) As Boolean
        Dim datafileWriter As DatafileWriter = Coordinates.DatafileWriter.GetInstance
        datafileWriter.ObservableImp.Observers(pObservableImp.Observers)
        datafileWriter.Write(filepath, pObjects)
        datafileWriter.ObservableImp.Observers.Clear()
    End Function
#End Region

#Region "Private and Protected Methods"
    Private Sub loadFile(ByVal filename As String)
        If Not Cancel Then
            loadFileSubr(filename)
        End If
    End Sub

    ' also see PositionArrayIODatafile.Import
    Private Sub loadFileSubr(ByVal filename As String)

        Dim datafileReader As DatafileReader = Coordinates.DatafileReader.GetInstance
        If datafileReader.Open(filename) Then
            pObservableImp.Notify("Loading " & filename)

            Dim source As Source = Coordinates.Source.GetInstance
            source.Source = Path.GetFileNameWithoutExtension(filename)
            pSources.Add(source)

            Dim lwpos As LWPosition = LWPosition.GetInstance
            While datafileReader.ReadValues(lwpos.RA, lwpos.Dec, lwpos.Name)
                lwpos.Source = source.Source
                pObjects.Add(lwpos)
                lwpos = LWPosition.GetInstance
            End While

            datafileReader.Close()
        End If
    End Sub

    Private Function SetCoordDisplays() As Boolean
        pObservableImp.Notify("Calculating coordinate display values...please wait.")
        Array.ForEach(pObjects.ToArray, AddressOf setCoordDisplay)
        Return True
    End Function

    Private Sub setCoordDisplay(ByVal [object] As Object)
        Dim lwpos As LWPosition = CType([object], LWPosition)
        pCoordinate.Rad = lwpos.RA
        lwpos.RADisplay = pCoordinate.ToString(CType(CoordExpType.HMS, ISFT))
        pCoordinate.Rad = lwpos.Dec
        lwpos.DecDisplay = pCoordinate.ToString(CType(CoordExpType.DMS, ISFT))
    End Sub
#End Region

End Class
