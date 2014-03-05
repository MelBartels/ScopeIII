#Region "imports"
#End Region

Public Class UserCtrlGaugeCoordPresenter
    Inherits MVPUserCtrlPresenterBase
    Implements IRendererCoordPresenter, IObserver

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private WithEvents pUserCtrlGaugeCoord As UserCtrlGaugeCoord
    Private WithEvents pMvpUserCtrlGaugeBase As MVPUserCtrlGaugeBase
    Private pICoordPresenter As ICoordPresenter
    Private WithEvents pUserCtrlCoord As UserCtrlCoord
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As UserCtrlGaugeCoordPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As UserCtrlGaugeCoordPresenter = New UserCtrlGaugeCoordPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As UserCtrlGaugeCoordPresenter
        Return New UserCtrlGaugeCoordPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property CoordUpdatedByMe() As Boolean Implements ICoordPresenter.CoordUpdatedByMe
        Get
            Return pICoordPresenter.CoordUpdatedByMe
        End Get
        Set(ByVal value As Boolean)
            pICoordPresenter.CoordUpdatedByMe = value
        End Set
    End Property

    Public Property CoordinateObservableImp() As ObservableImp Implements IRendererCoordPresenter.CoordinateObservableImp
        Get
            Return pICoordPresenter.CoordinateObservableImp
        End Get
        Set(ByVal Value As ObservableImp)
            pICoordPresenter.CoordinateObservableImp = Value
        End Set
    End Property

    Public Property CoordExpType() As ISFT Implements IRendererCoordPresenter.CoordExpType
        Get
            Return pICoordPresenter.CoordExpType
        End Get
        Set(ByVal Value As ISFT)
            pICoordPresenter.CoordExpType = Value
        End Set
    End Property

    Public Property Coordinate() As Coordinate Implements IRendererCoordPresenter.Coordinate
        Get
            Return pICoordPresenter.Coordinate
        End Get
        Set(ByVal Value As Coordinate)
            pICoordPresenter.Coordinate = Value
        End Set
    End Property

    Public Property IRenderer() As IRenderer Implements IRendererCoordPresenter.IRenderer
        Get
            Return pMvpUserCtrlGaugeBase.IRenderer
        End Get
        Set(ByVal Value As IRenderer)
            pMvpUserCtrlGaugeBase.IRenderer = Value
            pMvpUserCtrlGaugeBase.IRenderer.ObjectToRender = pICoordPresenter.Coordinate
        End Set
    End Property

    Public Sub SetCoordinateName(ByVal name As String) Implements IRendererCoordPresenter.SetCoordinateName
        pICoordPresenter.SetCoordinateName(name)
    End Sub

    Public Sub SetCoordinateLabelColor(ByVal color As Drawing.Color) Implements IRendererCoordPresenter.SetCoordinateLabelColor
        pICoordPresenter.SetCoordinateLabelColor(color)
    End Sub

    Public Sub DisplayCoordinate(ByVal angleRad As Double) Implements IRendererCoordPresenter.DisplayCoordinate
        displayAndRenderCoord(angleRad)
    End Sub

    Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
        'Debug.WriteLine("rendering " & pICoordPresenter.Coordinate.Name)
        Render()
    End Function

    Public Sub Render() Implements IRendererCoordPresenter.Render
        pMvpUserCtrlGaugeBase.Render()
    End Sub
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pUserCtrlGaugeCoord = CType(IMVPUserCtrl, UserCtrlGaugeCoord)
        AddHandler pUserCtrlGaugeCoord.MeasurementToPoint, AddressOf MeasurementToPoint

        pICoordPresenter = UserCtrlCoordPresenter.GetInstance
        CType(pICoordPresenter, MVPUserCtrlPresenterBase).IMVPUserCtrl = pUserCtrlGaugeCoord.UserCtrlCoord
        pICoordPresenter.CoordinateObservableImp.Attach(Me)

        pMvpUserCtrlGaugeBase = pUserCtrlGaugeCoord.MvpUserCtrlGaugeBase
        IRenderer = DegreeCircGaugeRenderer.GetInstance
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Sub measurementToPoint(ByVal value As Double)
        displayAndRenderCoord(value)
    End Sub

    Private Sub displayAndRenderCoord(ByVal angleRad As Double)
        pICoordPresenter.DisplayCoordinate(angleRad)
        pMvpUserCtrlGaugeBase.Render()
    End Sub
#End Region

End Class
