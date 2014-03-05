Public Class CoordinateObserver : Implements IObserver
    Public CoordinateObserverPresenter As CoordinateObserverPresenter
    Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
        CoordinateObserverPresenter.UserCtrlCoordPresenter.DisplayCoordinate(CType([object], Coordinate).Rad)
    End Function
End Class