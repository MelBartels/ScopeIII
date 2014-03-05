#Region "Imports"
Imports System.Windows.Forms
#End Region

Public Class Main
    Inherits MainPrototype

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Event ExitApplication()
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As Main
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As Main = New Main
    'End Class
#End Region

#Region "Constructors"
    Public Sub New()
    End Sub

    Public Shared Function GetInstance() As Main
        Return New Main
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Function Main(ByRef args() As String) As Integer
        Dim rtnValue As Int32
        Try
            ' for example...
            'Dim presenter As IMVPPresenter = presenter.GetInstance
            'presenter.IMVPView = New FrmMain
            'presenter.ShowDialog()
            rtnValue = BartelsLibrary.Constants.NormalExit

        Catch ex As Exception
            ExceptionService.Notify(ex, BartelsLibrary.Constants.TopLevelExceptionMsg)
            rtnValue = BartelsLibrary.Constants.BadExit
        Finally
            Application.Exit()
        End Try
        ' don't return until truly ready to exit, eg, must have a ShowDialog on this thread 
        Return rtnValue
    End Function

    Protected Overrides Sub work()

    End Sub
#End Region

End Class
