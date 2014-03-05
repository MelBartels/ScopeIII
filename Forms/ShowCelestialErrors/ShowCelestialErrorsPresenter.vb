#Region "imports"
#End Region

Public Class ShowCelestialErrorsPresenter
    Inherits MVPPresenterBase
    Implements IObserver

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Event FormInvisible()
    Public Event ErrorsToIncludeChanged()
    Public StartingIncludeRefraction As Boolean
#End Region

#Region "Private and Protected Members"
    Private pUserCtrlCelestialErrorsPresenter As UserCtrlCelestialErrorsPresenter
    Private pTime As Time
    Private pInitInProgress As Boolean
    Private pCelestialErrorsCalculatorFacade As CelestialErrorsCalculatorFacade
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ShowCelestialErrorsPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ShowCelestialErrorsPresenter = New ShowCelestialErrorsPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As ShowCelestialErrorsPresenter
        Return New ShowCelestialErrorsPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property CelestialErrorsCalculatorFacade() As CelestialErrorsCalculatorFacade
        Get
            If pCelestialErrorsCalculatorFacade Is Nothing Then
                pCelestialErrorsCalculatorFacade = CelestialErrorsCalculatorFacade.GetInstance
            End If
            Return pCelestialErrorsCalculatorFacade
        End Get
        Set(ByVal value As CelestialErrorsCalculatorFacade)
            pCelestialErrorsCalculatorFacade = value
        End Set
    End Property

    Public Property FromPosition() As Position
        Get
            Return CelestialErrorsCalculatorFacade.FromPosition
        End Get
        Set(ByVal value As Position)
            CelestialErrorsCalculatorFacade.FromPosition = value
        End Set
    End Property

    Public Property ToPosition() As Position
        Get
            Return CelestialErrorsCalculatorFacade.ToPosition
        End Get
        Set(ByVal value As Position)
            CelestialErrorsCalculatorFacade.ToPosition = value
        End Set
    End Property

    Public Property FromEpoch() As DateTime
        Get
            Return frmShowCelestialErrors.FromEpoch
        End Get
        Set(ByVal value As DateTime)
            frmShowCelestialErrors.FromEpoch = value
        End Set
    End Property

    Public Property ToEpoch() As DateTime
        Get
            Return frmShowCelestialErrors.ToEpoch
        End Get
        Set(ByVal value As DateTime)
            frmShowCelestialErrors.ToEpoch = value
        End Set
    End Property

    Public Property UncorrectedToCorrected() As Boolean
        Get
            Return CelestialErrorsCalculatorFacade.UncorrectedToCorrected
        End Get
        Set(ByVal value As Boolean)
            CelestialErrorsCalculatorFacade.UncorrectedToCorrected = value
        End Set
    End Property

    Public Property IncludePrecession() As Boolean
        Get
            Return pUserCtrlCelestialErrorsPresenter.IncludePrecession
        End Get
        Set(ByVal value As Boolean)
            pUserCtrlCelestialErrorsPresenter.IncludePrecession = value
        End Set
    End Property

    Public Property IncludeNutationAnnualAberration() As Boolean
        Get
            Return pUserCtrlCelestialErrorsPresenter.IncludeNutationAnnualAberration
        End Get
        Set(ByVal value As Boolean)
            pUserCtrlCelestialErrorsPresenter.IncludeNutationAnnualAberration = value
        End Set
    End Property

    Public Property IncludeRefraction() As Boolean
        Get
            Return pUserCtrlCelestialErrorsPresenter.IncludeRefraction
        End Get
        Set(ByVal value As Boolean)
            pUserCtrlCelestialErrorsPresenter.IncludeRefraction = value
        End Set
    End Property

    Public Property LatitudeRad() As Double
        Get
            Return CelestialErrorsCalculatorFacade.LatitudeRad
        End Get
        Set(ByVal value As Double)
            CelestialErrorsCalculatorFacade.LatitudeRad = value
        End Set
    End Property

    Public Property UseCalculator() As Boolean
        Get
            Return CelestialErrorsCalculatorFacade.UseCalculator
        End Get
        Set(ByVal value As Boolean)
            CelestialErrorsCalculatorFacade.UseCalculator = value
        End Set
    End Property

    Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
        Dim rad As Double = CType([object], Coordinate).Rad

        Select Case CType([object], Coordinate).Name
            Case CoordName.RA.Description
                FromPosition.RA.Rad = rad
            Case CoordName.Dec.Description
                FromPosition.Dec.Rad = rad
            Case CoordName.SidT.Description
                FromPosition.SidT.Rad = rad
            Case CoordName.Longitude.Description
                pTime.Longitude.Rad = rad
                FromPosition.SidT.Rad = pTime.CalcSidTNow()
            Case CoordName.Latitude.Description
                LatitudeRad = rad
        End Select

        UncorrectedToCorrected = True
        UpdateDataModelByCalculatingCelestialErrors()

        Return True
    End Function

    Public Sub UpdateDataModelByCalculatingCelestialErrors()
        calculateCelestialErrors()
        UpdateDataModel()
    End Sub

    Public Sub UpdateDataModel()
        DataModel = CelestialErrorsCalculatorFacade.CelestialErrorsPosition
        pUserCtrlCelestialErrorsPresenter.DataModel = DataModel
        pUserCtrlCelestialErrorsPresenter.Update()
    End Sub

    Public Sub UpdateDataModel(ByVal RaRad As Double, ByVal DecRad As Double, ByVal SidTRad As Double, ByVal latitudeRad As Double)
        FromPosition.RA.Rad = RaRad
        FromPosition.Dec.Rad = DecRad
        FromPosition.SidT.Rad = SidTRad
        latitudeRad = latitudeRad
        uncorrectedToCorrected = True
        calculateCelestialErrors()
        UpdateDataModel()
    End Sub

    Public Sub SetCelestialErrorsCalculatorFacadeFromView()
        CelestialErrorsCalculatorFacade.FromDate = FromEpoch
        CelestialErrorsCalculatorFacade.ToDate = ToEpoch
        CelestialErrorsCalculatorFacade.IncludePrecession = IncludePrecession
        CelestialErrorsCalculatorFacade.IncludeNutationAnnualAberration = IncludeNutationAnnualAberration
        CelestialErrorsCalculatorFacade.IncludeRefraction = IncludeRefraction
    End Sub

    Public Function CoordErrorArray() As CoordErrorArray
        Return CelestialErrorsCalculatorFacade.CoordErrorArray
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pInitInProgress = True
        pTime = Time.GetInstance

        AddHandler frmShowCelestialErrors.PrecessionCheckChanged, AddressOf precessionCheckChanged
        AddHandler frmShowCelestialErrors.NutationAnnualAberrationCheckChanged, AddressOf nutationAnnualAberrationCheckChanged
        AddHandler frmShowCelestialErrors.RefractionCheckChanged, AddressOf refractionCheckChanged
        AddHandler frmShowCelestialErrors.FromEpochChanged, AddressOf epochChanged
        AddHandler frmShowCelestialErrors.ToEpochChanged, AddressOf epochChanged
        AddHandler frmShowCelestialErrors.Invisible, AddressOf invisibleHandler

        pUserCtrlCelestialErrorsPresenter = UserCtrlCelestialErrorsPresenter.GetInstance
        pUserCtrlCelestialErrorsPresenter.IMVPUserCtrl = frmShowCelestialErrors.UserCtrlCelestialErrors

        pUserCtrlCelestialErrorsPresenter.IncludePrecession = True
        pUserCtrlCelestialErrorsPresenter.IncludeNutationAnnualAberration = True

        pUserCtrlCelestialErrorsPresenter.IncludeRefraction = StartingIncludeRefraction
        frmShowCelestialErrors.IncludeRefractionVisible = StartingIncludeRefraction
        frmShowCelestialErrors.RefractionChecked = StartingIncludeRefraction

        frmShowCelestialErrors.FromEpoch = New DateTime(eMath.RInt(ScopeLibrary.Settings.GetInstance.DatafilesEpoch), 1, 1)
        frmShowCelestialErrors.ToEpoch = pTime.NowDate

        pInitInProgress = False
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Function frmShowCelestialErrors() As FrmShowCelestialErrors
        Return CType(IMVPView, FrmShowCelestialErrors)
    End Function

    Private Sub calculateCelestialErrors()
        CelestialErrorsCalculatorFacade.AdaptForCelestialErrors( _
                FromPosition, _
                ToPosition, _
                FromEpoch, _
                ToEpoch, _
                IncludePrecession, _
                IncludeNutationAnnualAberration, _
                IncludeRefraction, _
                UncorrectedToCorrected, _
                LatitudeRad)
    End Sub

    Private Sub precessionCheckChanged(ByVal include As Boolean)
        If Not pInitInProgress Then
            pUserCtrlCelestialErrorsPresenter.IncludePrecession = include
            UpdateDataModelByCalculatingCelestialErrors()
            RaiseEvent ErrorsToIncludeChanged()
        End If
    End Sub

    Private Sub nutationAnnualAberrationCheckChanged(ByVal include As Boolean)
        If Not pInitInProgress Then
            pUserCtrlCelestialErrorsPresenter.IncludeNutationAnnualAberration = include
            UpdateDataModelByCalculatingCelestialErrors()
            RaiseEvent ErrorsToIncludeChanged()
        End If
    End Sub

    Private Sub refractionCheckChanged(ByVal include As Boolean)
        If Not pInitInProgress Then
            pUserCtrlCelestialErrorsPresenter.IncludeRefraction = include
            UpdateDataModelByCalculatingCelestialErrors()
            RaiseEvent ErrorsToIncludeChanged()
        End If
    End Sub

    Private Sub epochChanged()
        UpdateDataModelByCalculatingCelestialErrors()
        RaiseEvent ErrorsToIncludeChanged()
    End Sub

    Private Sub invisibleHandler()
        RaiseEvent FormInvisible()
    End Sub
#End Region

End Class
