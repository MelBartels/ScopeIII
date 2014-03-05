Public Class MainTest3AxisCoord
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

    'Public Shared Function GetInstance() As MainTest3AxisCoord
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MainTest3AxisCoord = New MainTest3AxisCoord
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MainTest3AxisCoord
        Return New MainTest3AxisCoord
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub work()
        Dim enter3AxisCoordPresenter As Enter3AxisCoordPresenter = Forms.Enter3AxisCoordPresenter.GetInstance
        enter3AxisCoordPresenter.IMVPView = New FrmEnter3AxisCoord
        enter3AxisCoordPresenter.ShowDialog()

        Dim coordinatePri As Coordinate = enter3AxisCoordPresenter.CoordinatePri
        Dim coordinateSec As Coordinate = enter3AxisCoordPresenter.CoordinateSec
        Dim coordinateTier As Coordinate = enter3AxisCoordPresenter.CoordinateTier
        MessageBoxCoord.GetInstance.Show(coordinatePri, coordinateSec, coordinateTier)
    End Sub
#End Region

End Class
