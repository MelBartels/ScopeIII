#Region "Imports"
#End Region

Public Class MVPViewBase
    Inherits System.Windows.Forms.Form
    Implements IMVPView

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Event LoadViewFromModel() Implements IMVPView.LoadViewFromModel
    Public Event SaveToModel() Implements IMVPView.SaveToModel
    Public Event ViewUpdated() Implements IMVPView.ViewUpdated
#End Region

#Region "Private and Protected Members"
    Protected pIMVPPresenter As IMVPPresenter
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As MVPViewBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MVPViewBase = New MVPViewBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    'Public Shared Function GetInstance() As MVPViewBase
    '    Return New MVPViewBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"

    ' to avoid cross threading exceptions that are caused by the form and its user controls being built 
    ' on different threads, override ShowDialog() and possibly Close() in the subclass setting 
    ' controlToInvokeOn inside each method to a user control;
    ' avoid making controlToInvokeOn a property or variable at the class level as this causes subclassed 
    ' forms to acquire "controlToInvokeOn" as their (class) name;

    ' copy the following code directly into the subclassed form's code:
    'Public Overrides Sub ShowDialog()
    '    Dim controlToInvokeOn As Windows.Forms.Control = myUserCtrl
    '    If controlToInvokeOn.InvokeRequired Then
    '        controlToInvokeOn.Invoke(New Windows.Forms.MethodInvoker(AddressOf ShowDialog))
    '    Else
    '        MyBase.ShowDialog()
    '    End If
    'End Sub

    'Public Overrides Sub Show()
    '    Dim controlToInvokeOn As Windows.Forms.Control = myUserCtrl
    '    If controlToInvokeOn.InvokeRequired Then
    '        controlToInvokeOn.Invoke(New Windows.Forms.MethodInvoker(AddressOf Show))
    '    Else
    '        MyBase.Show()
    '    End If
    'End Sub

    'Public Overridable Shadows Sub Close() Implements IMVPView.Close
    '    Try
    '        Dim controlToInvokeOn As Windows.Forms.Control = Me
    '        If controlToInvokeOn.InvokeRequired Then
    '            controlToInvokeOn.Invoke(New Windows.Forms.MethodInvoker(AddressOf Close))
    '        Else
    '            MyBase.Close()
    '        End If
    '    Catch ode As ObjectDisposedException
    '        ' if form disposed, then it is also closed
    '    End Try
    'End Sub

    Public Overridable Shadows Sub ShowDialog() Implements IMVPView.ShowDialog
        Dim controlToInvokeOn As Windows.Forms.Control = Me
        If controlToInvokeOn.InvokeRequired Then
            controlToInvokeOn.Invoke(New Windows.Forms.MethodInvoker(AddressOf Me.ShowDialog))
        Else
            MyBase.ShowDialog()
        End If
    End Sub

    Public Overridable Shadows Sub Show() Implements IMVPView.Show
        Dim controlToInvokeOn As Windows.Forms.Control = Me
        If controlToInvokeOn.InvokeRequired Then
            controlToInvokeOn.Invoke(New Windows.Forms.MethodInvoker(AddressOf Me.Show))
        Else
            MyBase.Show()
        End If
    End Sub

    Public Overridable Shadows Sub Close() Implements IMVPView.Close
        Try
            Dim controlToInvokeOn As Windows.Forms.Control = Me
            If controlToInvokeOn.InvokeRequired Then
                controlToInvokeOn.Invoke(New Windows.Forms.MethodInvoker(AddressOf Close))
            Else
                MyBase.Close()
            End If
        Catch ode As ObjectDisposedException
            ' if form disposed, then it is also closed
        End Try
    End Sub
#End Region

#Region "Private and Protected Methods"
    Protected Overridable Sub onLoadViewFromModel()
        RaiseEvent LoadViewFromModel()
    End Sub

    Protected Overridable Sub onSaveToModel()
        RaiseEvent SaveToModel()
    End Sub

    Protected Overridable Sub onViewUpdated()
        RaiseEvent ViewUpdated()
    End Sub

    Protected Function initDefaultPresenter(ByRef presenter As IMVPPresenter) As IMVPPresenter
        presenter.IMVPView = Me
        Return presenter
    End Function
#End Region

    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MVPViewBase))
        Me.SuspendLayout()
        '
        'MVPViewBase
        '
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MVPViewBase"
        Me.ResumeLayout(False)
    End Sub

End Class
