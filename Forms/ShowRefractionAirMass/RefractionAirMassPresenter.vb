#Region "imports"
#End Region

Public Class RefractionAirMassPresenter
    Inherits MVPPresenterBase
    Implements IObserver

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Private Const NumDeg As Int32 = 90

    Private Enum graphIx
        refact = 0
        airmass = 1
        alt = 2
        max = 3
    End Enum
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public RefactColor As Drawing.Color = Drawing.Color.Blue
    Public AirMassColor As Drawing.Color = Drawing.Color.Green
    Public AltitudeColor As Drawing.Color = Drawing.Color.Red
#End Region

#Region "Private and Protected Members"
    Private WithEvents pFrmRefractionAirMass As FrmRefractionAirMass
    Private pUserCtrlCoordPresenterRefract As UserCtrlCoordPresenter
    Private pUserCtrlCoordPresenterAirMass As UserCtrlCoordPresenter
    Private pIRendererCoordPresenterAlt As IRendererCoordPresenter
    Private pMultiXYData As MultiXYData

    Private pAltCoordinate As Coordinate
    Private pRefractCoordinate As Coordinate
    Private pRefract As Refract
    Private pAirMassCalculator As AirMassCalculator
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As RefractionAirMassPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As RefractionAirMassPresenter = New RefractionAirMassPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As RefractionAirMassPresenter
        Return New RefractionAirMassPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property AltCoordinate() As Coordinate
        Get
            Return (pAltCoordinate)
        End Get
        Set(ByVal Value As Coordinate)
            pAltCoordinate = Value
        End Set
    End Property

    Public Property RefractCoordinate() As Coordinate
        Get
            Return (pRefractCoordinate)
        End Get
        Set(ByVal Value As Coordinate)
            pRefractCoordinate = Value
        End Set
    End Property

    Public Property AirMass() As Double
        Get
            Return pAirMassCalculator.AirMass
        End Get
        Set(ByVal Value As Double)
            pAirMassCalculator.AirMass = Value
        End Set
    End Property

    Public Property IRendererCoordPresenterAlt() As IRendererCoordPresenter
        Get
            Return pIRendererCoordPresenterAlt
        End Get
        Set(ByVal Value As IRendererCoordPresenter)
            pIRendererCoordPresenterAlt = Value
        End Set
    End Property

    ' will not be called if observer is not attached
    Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
        Select Case CType([object], Coordinate).Name
            Case CoordName.Alt.Description
                ' renderer's ObjectToRender is the coordinate, which having been updated, sends a message to this observer
                IRendererCoordPresenterAlt.Render()
                updateGraphWithNewAlt()
                calcRefract()
                Return True
        End Select

        Return False
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pFrmRefractionAirMass = CType(IMVPView, FrmRefractionAirMass)
        pFrmRefractionAirMass.GraphRenderer = MultiXYDataRenderer.GetInstance

        pRefract = Refract.GetInstance
        pAirMassCalculator = Coordinates.AirMassCalculator.GetInstance
        pMultiXYData = MultiXYData.GetInstance

        drawGraph()

        pIRendererCoordPresenterAlt = UserCtrlGaugeCoordPresenter.GetInstance
        CType(IRendererCoordPresenterAlt, MVPUserCtrlPresenterBase).IMVPUserCtrl = pFrmRefractionAirMass.UserCtrlGaugeCoordAlt
        IRendererCoordPresenterAlt.IRenderer = Degree0_90VertSliderRenderer.GetInstance
        IRendererCoordPresenterAlt.SetCoordinateName(CoordName.Alt.Description)
        IRendererCoordPresenterAlt.SetCoordinateLabelColor(AltitudeColor)
        IRendererCoordPresenterAlt.CoordinateObservableImp.Attach(Me)

        pUserCtrlCoordPresenterRefract = UserCtrlCoordPresenter.GetInstance
        pUserCtrlCoordPresenterRefract.IMVPUserCtrl = pFrmRefractionAirMass.UserCtrlCoordRefract
        pUserCtrlCoordPresenterRefract.SetCoordinateName(CoordName.Refraction.Description)
        pUserCtrlCoordPresenterRefract.SetCoordinateLabelColor(RefactColor)

        pUserCtrlCoordPresenterAirMass = UserCtrlCoordPresenter.GetInstance
        pUserCtrlCoordPresenterAirMass.IMVPUserCtrl = pFrmRefractionAirMass.UserCtrlCoordAirMass
        pUserCtrlCoordPresenterAirMass.CoordExpType = CoordExpType.AirMass
        pUserCtrlCoordPresenterAirMass.SetCoordinateName(CoordName.AirMass.Description)
        pUserCtrlCoordPresenterAirMass.SetCoordinateLabelColor(AirMassColor)

        ' set a starting value
        IRendererCoordPresenterAlt.DisplayCoordinate(45 * Units.DegToRad)
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Sub calcRefract()
        pAltCoordinate = IRendererCoordPresenterAlt.Coordinate
        pUserCtrlCoordPresenterRefract.DisplayCoordinate(pRefract.Calc(pAltCoordinate.Rad).Rad)

        calcAirMass()
    End Sub

    Private Sub calcAirMass()
        pAltCoordinate = IRendererCoordPresenterAlt.Coordinate
        pAirMassCalculator.Calc(pAltCoordinate.Rad)
        pUserCtrlCoordPresenterAirMass.DisplayCoordinate(pAirMassCalculator.AirMass)
    End Sub

    Private Sub drawGraph()
        pFrmRefractionAirMass.GraphRenderer.ObjectToRender = pMultiXYData

        pMultiXYData.BackgroundColor = Drawing.Color.LightYellow
        pMultiXYData.GridColor = Drawing.Color.LightGray
        pMultiXYData.XLogBase = 0
        pMultiXYData.XRangeStart = 0
        pMultiXYData.XRangeEnd = 30
        pMultiXYData.XGridSpacing = 10
        pMultiXYData.YLogBase = 0
        pMultiXYData.YRangeStart = 0
        pMultiXYData.YRangeEnd = 90
        pMultiXYData.YGridSpacing = 10
        pMultiXYData.DataColor = New Drawing.Color() {RefactColor, AirMassColor, AltitudeColor}

        buildMultiXYDataData(pMultiXYData, graphIx.max, NumDeg)

        For deg As Double = 0 To NumDeg
            pMultiXYData.YData(graphIx.refact)(eMath.RInt(deg)) = deg
            pMultiXYData.XData(graphIx.refact)(eMath.RInt(deg)) = pRefract.Calc(deg * Units.DegToRad).Rad * Units.RadToArcmin
        Next

        For deg As Double = 0 To NumDeg
            pMultiXYData.YData(graphIx.airmass)(eMath.RInt(deg)) = deg
            pAirMassCalculator.Calc(deg * Units.DegToRad)
            pMultiXYData.XData(graphIx.airmass)(eMath.RInt(deg)) = pAirMassCalculator.AirMass
        Next
    End Sub

    Private Sub buildMultiXYDataData(ByRef multiXYData As MultiXYData, ByVal outerSize As Int32, ByVal innerSize As Int32)
        multiXYData.XData = getNewDataArray(outerSize, innerSize)
        multiXYData.YData = getNewDataArray(outerSize, innerSize)
    End Sub

    Private Function getNewDataArray(ByVal outerSize As Int32, ByVal innerSize As Int32) As Double()()
        Dim [double](outerSize - 1)() As Double
        For ix As Int32 = 0 To outerSize - 1
            [double](ix) = getNewData(innerSize)
        Next
        Return [double]
    End Function

    Private Function getNewData(ByVal size As Int32) As Double()
        Dim [double](size) As Double
        Return [double]
    End Function

    Private Sub updateGraphWithNewAlt()
        Dim alt As Double = IRendererCoordPresenterAlt.Coordinate.Rad * Units.RadToDeg
        For deg As Double = 0 To NumDeg - 1
            pMultiXYData.YData(graphIx.alt)(eMath.RInt(deg)) = alt
            pMultiXYData.XData(graphIx.alt)(eMath.RInt(deg)) = pMultiXYData.XRangeStart
        Next
        pMultiXYData.YData(graphIx.alt)(NumDeg) = alt
        pMultiXYData.XData(graphIx.alt)(NumDeg) = pMultiXYData.XRangeEnd

        pFrmRefractionAirMass.RenderGraph()
    End Sub
#End Region

End Class
