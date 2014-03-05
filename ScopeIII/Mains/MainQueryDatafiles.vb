Public Class MainQueryDatafiles
    Inherits MainPrototype

#Region "Inner Classes"
    Private Class mainCelestialErrors : Inherits MainPrototype
        Public ShowCelestialErrorsPresenter As ShowCelestialErrorsPresenter
        Public MainCelestialErrorsBuilt As Boolean
        Protected Overrides Sub work()
            ShowCelestialErrorsPresenter = Forms.ShowCelestialErrorsPresenter.GetInstance
            ShowCelestialErrorsPresenter.UseCalculator = True
            ShowCelestialErrorsPresenter.StartingIncludeRefraction = True
            ShowCelestialErrorsPresenter.IMVPView = New FrmShowCelestialErrors
            MainCelestialErrorsBuilt = True
            CType(ShowCelestialErrorsPresenter.IMVPView, Form).StartPosition = FormStartPosition.WindowsDefaultLocation
            ShowCelestialErrorsPresenter.ShowDialog()
        End Sub
    End Class

    Private Class mainLatitudeLongitude : Inherits MainPrototype
        Public Gauge2AxisCoordOKPresenter As Gauge2AxisCoordOKPresenter
        Public MainLatitudeLongitudeBuilt As Boolean
        Protected Overrides Sub work()
            Gauge2AxisCoordOKPresenter = Forms.Gauge2AxisCoordOKPresenter.GetInstance
            Gauge2AxisCoordOKPresenter.IMVPView = New FrmGauge2AxisCoordOK
            Gauge2AxisCoordOKPresenter.SetTitle("Query Datafiles Geographic Location")
            Gauge2AxisCoordOKPresenter.IGauge2AxisCoordPresenter.SetAxisNames(CoordName.Longitude.Description, CoordName.Latitude.Description)
            MainLatitudeLongitudeBuilt = True
            CType(Gauge2AxisCoordOKPresenter.IMVPView, Form).StartPosition = FormStartPosition.WindowsDefaultLocation
            Gauge2AxisCoordOKPresenter.ShowDialog()
        End Sub
    End Class
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pQueryDatafilesPresenter As QueryDatafilesPresenter
    Private pMainCelestialErrors As mainCelestialErrors
    Private pMainLatitudeLongitude As mainLatitudeLongitude
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As MainQueryDatafiles
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MainQueryDatafiles = New MainQueryDatafiles
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MainQueryDatafiles
        Return New MainQueryDatafiles
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub work()
        pQueryDatafilesPresenter = Forms.QueryDatafilesPresenter.GetInstance
        pQueryDatafilesPresenter.IMVPView = New FrmQueryDatafiles
        AddHandler pQueryDatafilesPresenter.ObjectSelected, AddressOf objectSelected

        buildShowCelestialErrorsPresenter()
        buildLatitudeLongitudePresenter()
        connectObservers()

        pQueryDatafilesPresenter.ShowDialog()
    End Sub

    Private Sub objectSelected(ByVal selectedLWPosition As LWPosition)
        pMainCelestialErrors.ShowCelestialErrorsPresenter.UpdateDataModel(selectedLWPosition.RA, selectedLWPosition.Dec, 0, 0)
    End Sub

    Private Sub buildShowCelestialErrorsPresenter()
        pMainCelestialErrors = New mainCelestialErrors
        pMainCelestialErrors.ShowCelestialErrorsPresenter = ShowCelestialErrorsPresenter.GetInstance
        pMainCelestialErrors.Main()
    End Sub

    Private Sub buildLatitudeLongitudePresenter()
        pMainLatitudeLongitude = New mainLatitudeLongitude
        pMainLatitudeLongitude.Gauge2AxisCoordOKPresenter = Gauge2AxisCoordOKPresenter.GetInstance
        pMainLatitudeLongitude.Main()
    End Sub

    Private Sub connectObservers()
        While pMainCelestialErrors Is Nothing _
        OrElse Not pMainCelestialErrors.MainCelestialErrorsBuilt _
        OrElse pMainLatitudeLongitude Is Nothing _
        OrElse Not pMainLatitudeLongitude.MainLatitudeLongitudeBuilt
            Threading.Thread.Sleep(100)
        End While

        Dim IObserver As IObserver = pMainCelestialErrors.ShowCelestialErrorsPresenter
        Dim IGauge2AxisCoordPresenter As IGauge2AxisCoordPresenter = pMainLatitudeLongitude.Gauge2AxisCoordOKPresenter.IGauge2AxisCoordPresenter
        IGauge2AxisCoordPresenter.CoordinatePriObservableImp.Attach(IObserver)
        IGauge2AxisCoordPresenter.CoordinateSecObservableImp.Attach(IObserver)
    End Sub

#End Region

End Class
