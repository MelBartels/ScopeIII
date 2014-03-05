Public Class MVPUserCtrlBase
    Inherits System.Windows.Forms.UserControl
    Implements IMVPUserCtrl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
    End Sub

#End Region

    Public Event LoadViewFromModel() Implements IMVPUserCtrl.LoadViewFromModel
    Public Event SaveToModel() Implements IMVPUserCtrl.SaveToModel
    Public Event ViewUpdated() Implements IMVPUserCtrl.ViewUpdated

    Protected Overridable Sub onLoadViewFromModel()
        RaiseEvent LoadViewFromModel()
    End Sub

    Protected Overridable Sub onSaveToModel()
        RaiseEvent SaveToModel()
    End Sub

    Protected Overridable Sub onViewUpdated()
        RaiseEvent ViewUpdated()
    End Sub
End Class
