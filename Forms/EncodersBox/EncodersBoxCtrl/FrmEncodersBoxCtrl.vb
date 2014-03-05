Imports BartelsLibrary.DelegateSigs

Public Class FrmEncodersBoxCtrl
    Inherits FrmEncodersBox

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    Friend WithEvents btnAutoQuery As System.Windows.Forms.Button
    Friend WithEvents btnSendCmd As System.Windows.Forms.Button
    Friend WithEvents cmBxCmds As System.Windows.Forms.ComboBox

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnAutoQuery = New System.Windows.Forms.Button
        Me.btnSendCmd = New System.Windows.Forms.Button
        Me.cmBxCmds = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'btnAutoQuery
        '
        Me.btnAutoQuery.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btnAutoQuery.Location = New System.Drawing.Point(639, 41)
        Me.btnAutoQuery.Name = "btnAutoQuery"
        Me.btnAutoQuery.Size = New System.Drawing.Size(100, 23)
        Me.btnAutoQuery.TabIndex = 20
        Me.btnAutoQuery.Text = "Auto Query On"
        Me.btnAutoQuery.UseVisualStyleBackColor = True
        '
        'btnSendCmd
        '
        Me.btnSendCmd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSendCmd.Location = New System.Drawing.Point(664, 12)
        Me.btnSendCmd.Name = "btnSendCmd"
        Me.btnSendCmd.Size = New System.Drawing.Size(75, 23)
        Me.btnSendCmd.TabIndex = 19
        Me.btnSendCmd.Text = "Send Cmd"
        Me.btnSendCmd.UseVisualStyleBackColor = True
        '
        'cmBxCmds
        '
        Me.cmBxCmds.FormattingEnabled = True
        Me.cmBxCmds.Location = New System.Drawing.Point(439, 12)
        Me.cmBxCmds.Name = "cmBxCmds"
        Me.cmBxCmds.Size = New System.Drawing.Size(219, 21)
        Me.cmBxCmds.TabIndex = 18
        '
        'FrmEncodersBoxCtrl
        '
        Me.ClientSize = New System.Drawing.Size(848, 358)
        Me.Controls.Add(Me.btnAutoQuery)
        Me.Controls.Add(Me.btnSendCmd)
        Me.Controls.Add(Me.cmBxCmds)
        Me.Name = "FrmEncodersBoxCtrl"
        Me.Text = "ScopeIII Encoders Box Controller"
        Me.Title = "ScopeIII Encoders Box Controller"
        Me.Controls.SetChildIndex(Me.cmBxCmds, 0)
        Me.Controls.SetChildIndex(Me.btnSendCmd, 0)
        Me.Controls.SetChildIndex(Me.btnAutoQuery, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public Event SendCmd(ByVal cmdIx As Int32)
    Public Event AutoQuery(ByVal switch As Boolean)

    Private autoQueryState As Boolean

    Public Property CmdsDataSource() As Object
        Get
            Return cmBxCmds.DataSource
        End Get
        Set(ByVal Value As Object)
            updateCmBoxCmdsDataSource(Value)
        End Set
    End Property

    Private Sub btnSendCmd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendCmd.Click
        RaiseEvent SendCmd(cmBxCmds.SelectedIndex)
    End Sub

    Private Sub btnAutoQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAutoQuery.Click
        autoQueryState = Not autoQueryState
        If autoQueryState Then
            btnAutoQuery.Text = "AutoQuery Off"
        Else
            btnAutoQuery.Text = "AutoQuery On"
        End If
        RaiseEvent AutoQuery(autoQueryState)
    End Sub

    Private Sub updateCmBoxCmdsDataSource(ByVal value As Object)
        If cmBxCmds.InvokeRequired Then
            cmBxCmds.Invoke(New DelegateObj(AddressOf updateCmBoxCmdsDataSource), New Object() {value})
        Else
            cmBxCmds.DataSource = Nothing
            cmBxCmds.Items.Clear()
            cmBxCmds.ResetText()
            cmBxCmds.Refresh()
            cmBxCmds.DataSource = value
            cmBxCmds.Refresh()
        End If
    End Sub
End Class
