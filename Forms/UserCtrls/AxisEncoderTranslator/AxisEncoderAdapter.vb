#Region "Imports"
#End Region

Public Class AxisEncoderAdapter
    Implements IObserver

#Region "Inner Classes"
    Private Class CoordinateChangedObserver : Implements IObserver
        Private delegateProcessMsg As [Delegate]
        Public Sub RegisterDelegate(ByRef [delegate] As [Delegate])
            delegateProcessMsg = [delegate]
        End Sub
        Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
            delegateProcessMsg.DynamicInvoke()
        End Function
    End Class
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pTotalTicks As Double
    Private pOffsetRad As Double
    Private pEncoderValue As EncoderValue
    Private pICoordPresenter As ICoordPresenter
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As AxisEncoderAdapter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As AxisEncoderAdapter = New AxisEncoderAdapter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As AxisEncoderAdapter
        Return New AxisEncoderAdapter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property TotalTicks() As Double
        Get
            Return pTotalTicks
        End Get
        Set(ByVal value As Double)
            pTotalTicks = value
        End Set
    End Property

    Public Property OffsetRad() As Double
        Get
            Return pOffsetRad
        End Get
        Set(ByVal value As Double)
            pOffsetRad = value
        End Set
    End Property

    Public Property EncoderValue() As EncoderValue
        Get
            Return pEncoderValue
        End Get
        Set(ByVal value As EncoderValue)
            pEncoderValue = value
        End Set
    End Property

    Public Property ICoordPresenter() As ICoordPresenter
        Get
            Return pICoordPresenter
        End Get
        Set(ByVal value As ICoordPresenter)
            pICoordPresenter = value
        End Set
    End Property

    Public Sub RegisterCoordinateChange()
        Dim CoordinateChangedObserver As New CoordinateChangedObserver
        CoordinateChangedObserver.RegisterDelegate(New BartelsLibrary.DelegateSigs.DelegateNone(AddressOf UpdateOffset))
        ICoordPresenter.CoordinateObservableImp.Attach(CType(CoordinateChangedObserver, IObserver))
    End Sub

    Public Sub UpdateCoordinate()
        ICoordPresenter.DisplayCoordinate(EncoderValue.TotalTicks / getTotalTicks() * Units.OneRev + OffsetRad)
    End Sub

    Public Sub UpdateOffset()
        OffsetRad = ICoordPresenter.Coordinate.Rad - EncoderValue.TotalTicks / getTotalTicks() * Units.OneRev
    End Sub

    Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
        UpdateCoordinate()
    End Function
#End Region

#Region "Private and Protected Methods"
    Private Function getTotalTicks() As Double
        Dim useTotalTicks As Double = TotalTicks
        If useTotalTicks.Equals(0) Then
            useTotalTicks = EncoderValue.Range
        End If
        Return useTotalTicks
    End Function
#End Region

End Class
