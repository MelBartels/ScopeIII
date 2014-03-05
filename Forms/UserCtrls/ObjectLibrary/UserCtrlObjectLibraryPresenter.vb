Imports BartelsLibrary.DelegateSigs

Public Class UserCtrlObjectLibraryPresenter
    Inherits MVPUserCtrlPresenterBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public Const ResultsSize As Int32 = 12
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Event ObjectSelected(ByVal selectedLWPosition As LWPosition)
#End Region

#Region "Private and Protected Members"
    Private pUserCtrl2AxisCoordPresenter As UserCtrl2AxisCoordPresenter
    Private pUserCtrlDatafilePresenter As UserCtrlDatafilePresenter
    Private WithEvents pUserCtrlObjectLibrary As UserCtrlObjectLibrary

    Private WithEvents pProgressMediator As ProgressMediator
    Private pDatafiles As String()
    Private pLibrary As Library
    Private pSearchResults As ArrayList
    Private pDatafileSort() As Boolean
    Private pSelectedLWPosition As LWPosition
    Private pSourceArray As ArrayList
    Private pSepPosition As Position
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As UserCtrlObjectLibraryPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As UserCtrlObjectLibraryPresenter = New UserCtrlObjectLibraryPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As UserCtrlObjectLibraryPresenter
        Return New UserCtrlObjectLibraryPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property Library() As Library
        Get
            Return pLibrary
        End Get
        Set(ByVal Value As Library)
            pLibrary = Value
        End Set
    End Property

    Public Property SelectedLWPosition() As LWPosition
        Get
            Return pSelectedLWPosition
        End Get
        Set(ByVal Value As LWPosition)
            pSelectedLWPosition = Value
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pUserCtrlObjectLibrary = CType(IMVPUserCtrl, UserCtrlObjectLibrary)
        pUserCtrlObjectLibrary.SetToolTip()
        AddHandler pUserCtrlObjectLibrary.SourceFilterChanged, AddressOf sourceFilterChanged
        AddHandler pUserCtrlObjectLibrary.Closest, AddressOf closest
        AddHandler pUserCtrlObjectLibrary.SortDatafiles, AddressOf sortDatafiles
        AddHandler pUserCtrlObjectLibrary.ObjectSelected, AddressOf anObjectSelected

        Library = Coordinates.Library.GetInstance
        pSepPosition = Position.GetInstance
        pProgressMediator = ProgressMediator.GetInstance
        ReDim pDatafileSort(DGVColumnNames.ISFT.Size - 1)

        pUserCtrlDatafilePresenter = UserCtrlDatafilePresenter.GetInstance
        pUserCtrlDatafilePresenter.IMVPUserCtrl = pUserCtrlObjectLibrary.UserCtrlDatafile
        pUserCtrlDatafilePresenter.EpochText = ScopeLibrary.Settings.GetInstance.DatafilesEpoch
        AddHandler pUserCtrlDatafilePresenter.DatafilesSelected, AddressOf loadDatafiles

        pUserCtrl2AxisCoordPresenter = UserCtrl2AxisCoordPresenter.GetInstance
        pUserCtrl2AxisCoordPresenter.IMVPUserCtrl = pUserCtrlObjectLibrary.UserCtrl2AxisCoord
        pUserCtrl2AxisCoordPresenter.SetAxisNames(CoordName.RA.Description, CoordName.Dec.Description)
        pUserCtrl2AxisCoordPresenter.SetExpCoordTypes(CType(CoordExpType.FormattedHMS, ISFT), CType(CoordExpType.FormattedDMS, ISFT))
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Sub search()
        executeFilters()
    End Sub

    Private Sub sourceFilterChanged()
        executeFilters()
    End Sub

    Private Sub loadDatafiles(ByVal datafiles As String())
        If pUserCtrlDatafilePresenter.ValidateEpoch Then
            pDatafiles = datafiles
            pProgressMediator.ProcessStartup(New DelegateObjDoWorkEventArgs(AddressOf loadDatafilesCallback))
        End If
    End Sub

    Private Sub loadDatafilesCallback(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
        Library.Cancel = False

        ' count of .ShowMsgs
        pProgressMediator.ProgressBarMaxValue = 4 + pDatafiles.Length
        pProgressMediator.ShowMsg("Loading datafiles.")

        Library.ObservableImp.Attach(CType(pProgressMediator, IObserver))
        Library.Load(pDatafiles)
        Library.ObservableImp.Detach(CType(pProgressMediator, IObserver))

        pProgressMediator.ShowMsg("Setting the form's DataSources...please wait.")

        Dim sb As New Text.StringBuilder
        sb.Append(Library.Objects.Count.ToString)
        sb.Append(" objects in ")
        sb.Append(Library.Sources.Count.ToString)
        sb.Append(" files")
        pUserCtrlObjectLibrary.Counts = sb.ToString()

        datafilesDataSource = Library.Objects
        buildCmbBxSourceFilter()

        pProgressMediator.ShowMsg("Finished.")
        pProgressMediator.ProcessShutdown()
    End Sub

    Private Sub progressCancel() Handles pProgressMediator.CancelBackgroundWork
        Library.Cancel = True
    End Sub

    Private Sub setRADec()
        pSepPosition.RA = pUserCtrl2AxisCoordPresenter.CoordinatePri
        pSepPosition.Dec = pUserCtrl2AxisCoordPresenter.CoordinateSec
    End Sub

    Private Sub buildCmbBxSourceFilter()
        pSourceArray = New ArrayList

        pSourceArray.Add(ScopeLibrary.Constants.AllSources)

        Array.ForEach(Library.Sources.ToArray, AddressOf New SubDelegate(Of Object, Object) _
            (AddressOf addSourceToArray, pSourceArray).CallDelegate)

        pUserCtrlObjectLibrary.SourceFilterDataSource = pSourceArray
    End Sub

    Private Sub addSourceToArray(ByVal source As Object, ByVal array As Object)
        CType(array, ArrayList).Add(CType(source, Source).Source)
    End Sub

    ' create new ArrayLists for the data source, otherwise the DataGridView's BindingSource has trouble 
    ' with subsequent updates

    Private Sub executeFilters()
        If Library.Objects IsNot Nothing AndAlso Library.Objects.Count > 0 Then
            pSearchResults = New ArrayList

            Array.ForEach(Library.Objects.ToArray, AddressOf New SubDelegate(Of Object, Object) _
                (AddressOf addSearchResults, pSearchResults).CallDelegate)

            datafilesDataSource = pSearchResults
        End If
    End Sub

    Private Sub addSearchResults(ByVal lwpos As Object, ByVal array As Object)
        If matchAgainstFilters(lwpos) Then
            CType(array, ArrayList).Add(lwpos)
        End If
    End Sub

    Private Function matchAgainstFilters(ByRef lwpos As Object) As Boolean
        Return CType(lwpos, LWPosition).Name.ToLower.IndexOf(pUserCtrlObjectLibrary.NameFilter.ToLower) > -1 _
        AndAlso (pUserCtrlObjectLibrary.SourceFilterSelectedItem Is Nothing _
                 OrElse pUserCtrlObjectLibrary.SourceFilterSelectedItem.Equals(ScopeLibrary.Constants.AllSources) _
                 OrElse CStr(pUserCtrlObjectLibrary.SourceFilterSelectedItem).Equals(CType(lwpos, LWPosition).Source))
    End Function

    Private Sub closest()
        setRADec()
        loadClosest()
    End Sub

    Private Sub loadClosest()
        Dim coordinateParser As CoordinateParser = Coordinates.CoordinateParser.GetInstance

        Dim separationIxArray As New ArrayList
        Dim CelestialCoordinateCalcs As CelestialCoordinateCalcs = Coordinates.CelestialCoordinateCalcs.GetInstance
        Dim libPosition As Position = Position.GetInstance

        Dim ix As Int32
        For Each lwpos As LWPosition In Library.Objects
            libPosition.RA.Rad = lwpos.RA
            libPosition.Dec.Rad = lwpos.Dec
            Dim separationIx As New SeparationIx
            separationIx.Separation = CelestialCoordinateCalcs.AngSepDiffViaRa(pSepPosition, libPosition)
            separationIx.Ix = ix
            ix += 1
            separationIxArray.Add(separationIx)
        Next

        separationIxArray.Sort(New SeparationIxComparer)

        pSearchResults = New ArrayList
        ix = 0
        For Each separationIx As SeparationIx In separationIxArray
            Dim lwpos As Object = Library.Objects.Item(separationIx.Ix)
            If matchAgainstFilters(lwpos) Then
                pSearchResults.Add(lwpos)
                ix += 1
                If ix >= ResultsSize Then
                    Exit For
                End If
            End If
        Next

        datafilesDataSource = pSearchResults
    End Sub

    Private Sub sortDatafiles(ByVal name As String)
        Dim DatafileSort As DatafileSort = Forms.DatafileSort.GetInstance
        DatafileSort.DatafileColumnName = DatafileSort.DatafileColumnName.FirstItem.MatchString(name)
        Dim selectedColumn As Int32 = DatafileSort.DatafileColumnName.Key
        DatafileSort.UpDown = pDatafileSort(selectedColumn)
        pDatafileSort(selectedColumn) = Not pDatafileSort(selectedColumn)

        CType(datafilesDataSource, ArrayList).Sort(DatafileSort)
        pUserCtrlObjectLibrary.RefreshDataGridView()
    End Sub

    Private Property datafilesDataSource() As Object
        Get
            Return pUserCtrlObjectLibrary.DatafilesDataSource
        End Get
        Set(ByVal value As Object)
            pUserCtrlObjectLibrary.DatafilesDataSource = value
        End Set
    End Property

    Private Sub anObjectSelected(ByVal row As Int32)
        SelectedLWPosition = CType(CType(datafilesDataSource, ArrayList)(row), LWPosition)
        RaiseEvent ObjectSelected(SelectedLWPosition)
    End Sub
#End Region

End Class
