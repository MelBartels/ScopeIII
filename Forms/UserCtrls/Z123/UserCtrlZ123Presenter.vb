#Region "Imports"
Imports System.IO
Imports System.Windows.Forms
#End Region

Public Class UserCtrlZ123Presenter
    Inherits MVPUserCtrlPresenterBase
    Implements IObserver

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private WithEvents pUserCtrlZ123 As UserCtrlZ123

    Private pUserCtrlCoordPresenterZ1 As UserCtrlCoordPresenter
    Private pUserCtrlCoordPresenterZ2 As UserCtrlCoordPresenter
    Private pUserCtrlCoordPresenterZ3 As UserCtrlCoordPresenter

    Private pFabErrors As FabErrors
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As UserCtrlZ123Presenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As UserCtrlZ123Presenter = New UserCtrlZ123Presenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As UserCtrlZ123Presenter
        Return New UserCtrlZ123Presenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"

    Public Property UseCorrections() As Boolean
        Get
            Return pUserCtrlZ123.UseZ123
        End Get
        Set(ByVal Value As Boolean)
            pUserCtrlZ123.UseZ123 = Value
        End Set
    End Property

    Public Property FabErrors() As FabErrors
        Get
            Return pFabErrors
        End Get
        Set(ByVal Value As FabErrors)
            pFabErrors = Value
        End Set
    End Property

    Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
        Select Case CType([object], Coordinate).Name
            Case CoordName.Z1.Description, _
                 CoordName.Z2.Description, _
                 CoordName.Z3.Description

                FabErrors.Z1.Rad = pUserCtrlCoordPresenterZ1.Coordinate.Rad
                FabErrors.Z2.Rad = pUserCtrlCoordPresenterZ2.Coordinate.Rad
                FabErrors.Z3.Rad = pUserCtrlCoordPresenterZ3.Coordinate.Rad

                Return True
        End Select

        Return False
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pUserCtrlZ123 = CType(IMVPUserCtrl, UserCtrlZ123)
        pUserCtrlZ123.SetToolTip()

        pUserCtrlCoordPresenterZ1 = UserCtrlCoordPresenter.GetInstance
        pUserCtrlCoordPresenterZ1.IMVPUserCtrl = pUserCtrlZ123.UserCtrlCoordZ1
        pUserCtrlCoordPresenterZ1.SetCoordinateName(CoordName.Z1.Description)
        pUserCtrlCoordPresenterZ1.CoordinateObservableImp.Attach(Me)

        pUserCtrlCoordPresenterZ2 = UserCtrlCoordPresenter.GetInstance
        pUserCtrlCoordPresenterZ2.IMVPUserCtrl = pUserCtrlZ123.UserCtrlCoordZ2
        pUserCtrlCoordPresenterZ2.SetCoordinateName(CoordName.Z2.Description)
        pUserCtrlCoordPresenterZ2.CoordinateObservableImp.Attach(Me)

        pUserCtrlCoordPresenterZ3 = UserCtrlCoordPresenter.GetInstance
        pUserCtrlCoordPresenterZ3.IMVPUserCtrl = pUserCtrlZ123.UserCtrlCoordZ3
        pUserCtrlCoordPresenterZ3.SetCoordinateName(CoordName.Z3.Description)
        pUserCtrlCoordPresenterZ3.CoordinateObservableImp.Attach(Me)

        FabErrors = Coordinates.FabErrors.GetInstance
    End Sub

    Protected Overrides Sub loadViewFromModel()
        UseCorrections = CBool(CType(DataModel, Object())(0))

        FabErrors = CType(CType(DataModel, Object())(1), FabErrors)
        pUserCtrlCoordPresenterZ1.DisplayCoordinate(FabErrors.Z1.Rad)
        pUserCtrlCoordPresenterZ2.DisplayCoordinate(FabErrors.Z2.Rad)
        pUserCtrlCoordPresenterZ3.DisplayCoordinate(FabErrors.Z3.Rad)
    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub
#End Region

End Class
