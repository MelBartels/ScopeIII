#Region "Imports"
#End Region

Public Class UserCtrlCoordPresenter
    Inherits MVPUserCtrlPresenterBase
    Implements ICoordPresenter

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private WithEvents pUserCtrlCoord As UserCtrlCoord
    Private pCoordinate As Coordinate
    Private pCoordExpType As ISFT
    Private pCoordinateObservableImp As ObservableImp
    Private pLastObservedCoordRad As Double
    Private pCoordUpdatedByMe As Boolean
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As UserCtrlCoordPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As UserCtrlCoordPresenter = New UserCtrlCoordPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As UserCtrlCoordPresenter
        Return New UserCtrlCoordPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property CoordUpdatedByMe() As Boolean Implements ICoordPresenter.CoordUpdatedByMe
        Get
            Return pCoordUpdatedByMe
        End Get
        Set(ByVal value As Boolean)
            pCoordUpdatedByMe = value
        End Set
    End Property

    Public Property CoordinateObservableImp() As ObservableImp Implements ICoordPresenter.CoordinateObservableImp
        Get
            Return pCoordinateObservableImp
        End Get
        Set(ByVal Value As ObservableImp)
            pCoordinateObservableImp = Value
        End Set
    End Property

    Public Property Coordinate() As Coordinate Implements ICoordPresenter.Coordinate
        Get
            Return pCoordinate
        End Get
        Set(ByVal Value As Coordinate)
            pCoordinate = Value
        End Set
    End Property

    Public Property CoordExpType() As ISFT Implements ICoordPresenter.CoordExpType
        Get
            Return pCoordExpType
        End Get
        Set(ByVal Value As ISFT)
            pCoordExpType = Value
        End Set
    End Property

    Public Sub SetCoordinateName(ByVal name As String) Implements ICoordPresenter.SetCoordinateName
        pUserCtrlCoord.SetCoordinateName(name)
        pCoordinate.Name = name
    End Sub

    Public Sub SetCoordinateLabelColor(ByVal color As Drawing.Color) Implements ICoordPresenter.SetCoordinateLabelColor
        pUserCtrlCoord.SetCoordinateLabelColor(color)
    End Sub

    Public Sub DisplayCoordinate(ByVal rad As Double) Implements ICoordPresenter.DisplayCoordinate
        Coordinate.Rad = rad
        saveToModel()
        pUserCtrlCoord.CoordinateText = Coordinate.ToString(CoordExpType)

        setCoordUpdatedByMe()

        Dim notifyObservers As Boolean = False
        If rad <> pLastObservedCoordRad Then
            notifyObservers = True
            pLastObservedCoordRad = rad
        End If

        If notifyObservers AndAlso Not pCoordinateObservableImp Is Nothing Then
            pCoordinateObservableImp.Notify(CObj(Coordinate))
            DebugTrace.WriteLine("Coordinate " & Coordinate.Name & " observer notified: " & rad * Units.RadToDeg & " deg.")
        End If
    End Sub
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pUserCtrlCoord = CType(IMVPUserCtrl, UserCtrlCoord)
        AddHandler pUserCtrlCoord.ValidCoord, AddressOf ValidCoord
        pUserCtrlCoord.SetToolTip()

        pCoordinateObservableImp = ObservableImp.GetInstance
        Coordinate = ScopeIII.Coordinates.Coordinate.GetInstance
        CoordExpType = Coordinates.CoordExpType.FormattedDMS
    End Sub

    Protected Overrides Sub loadViewFromModel()
        DisplayCoordinate(CType(DataModel, Coordinate).Rad)
    End Sub

    Protected Overrides Sub saveToModel()
        ' cannot create a new DataModel here, as loadViewFromModel() will be called, setting the displayed
        ' coordinate to 0
        If DataModel IsNot Nothing Then
            CType(DataModel, Coordinate).Rad = Coordinate.Rad
        End If
    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Sub ValidCoord(ByVal rad As Double)
        pUserCtrlCoord.CoordUpdatedByMe = True
        DisplayCoordinate(rad)
    End Sub

    ' if DisplayCoordinate() called internally because my UserCtrl changed the coordinate, then
    ' set CoordUpdatedByMe to true, otherwise DisplayCoordinate() was called externally;
    ' sequence: 
    ' 1st time DisplayCoordinate() called, CoordUpdatedByMe set to true if updated internally
    '     and UserCtrl's CoordUpdatedByMe set to false,
    ' next time through DisplayCoordinate(), CoordUpdatedByMe set to false;
    Private Sub SetCoordUpdatedByMe()
        CoordUpdatedByMe = pUserCtrlCoord.CoordUpdatedByMe
        pUserCtrlCoord.CoordUpdatedByMe = False
        'Debug.WriteLine(Coordinate.Name & " CoordUpdatedByMe " & CoordUpdatedByMe)
    End Sub
#End Region

End Class
