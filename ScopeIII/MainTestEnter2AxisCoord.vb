Public Class MainTest2AxisCoord
    Inherits MainPrototype

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As MainTest2AxisCoord
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MainTest2AxisCoord = New MainTest2AxisCoord
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MainTest2AxisCoord
        Return New MainTest2AxisCoord
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub work()
        Dim enter2AxisCoordPresenter As Enter2AxisCoordPresenter = Forms.Enter2AxisCoordPresenter.GetInstance
        enter2AxisCoordPresenter.IMVPView = New FrmEnter2AxisCoord
        enter2AxisCoordPresenter.ShowDialog()

        Dim coordinatePri As Coordinate = enter2AxisCoordPresenter.CoordinatePri
        Dim coordinateSec As Coordinate = enter2AxisCoordPresenter.CoordinateSec
        MessageBoxCoord.GetInstance.Show(coordinatePri, coordinateSec)
    End Sub
#End Region

End Class
