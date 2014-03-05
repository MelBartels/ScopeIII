Imports System.Windows.Forms

Public Class FrmSelectStrongFinalType
    Inherits MVPViewBase

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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents lv As System.Windows.Forms.ListView
    Friend WithEvents Type As System.Windows.Forms.ColumnHeader
    Friend WithEvents Description As System.Windows.Forms.ColumnHeader
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSelectStrongFinalType))
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.lv = New System.Windows.Forms.ListView
        Me.Type = New System.Windows.Forms.ColumnHeader
        Me.Description = New System.Windows.Forms.ColumnHeader
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnOK = New System.Windows.Forms.Button
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'ToolTip
        '
        Me.ToolTip.AutoPopDelay = 30000
        Me.ToolTip.InitialDelay = 500
        Me.ToolTip.ReshowDelay = 100
        '
        'lv
        '
        Me.lv.AllowColumnReorder = True
        Me.lv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lv.CheckBoxes = True
        Me.lv.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Type, Me.Description})
        Me.lv.FullRowSelect = True
        Me.lv.Location = New System.Drawing.Point(8, 8)
        Me.lv.Name = "lv"
        Me.lv.Size = New System.Drawing.Size(328, 168)
        Me.lv.TabIndex = 0
        Me.lv.UseCompatibleStateImageBehavior = False
        Me.lv.View = System.Windows.Forms.View.Details
        '
        'Type
        '
        Me.Type.Text = "Type"
        Me.Type.Width = 120
        '
        'Description
        '
        Me.Description.Text = "Description"
        Me.Description.Width = 200
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(256, 184)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(72, 23)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Cancel"
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(168, 184)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(72, 23)
        Me.btnOK.TabIndex = 4
        Me.btnOK.Text = "OK"
        '
        'FrmSelectStrongFinalType
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(342, 212)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.lv)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmSelectStrongFinalType"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ScopeIII Select Type"
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private pMultiSelect As Boolean
    Private lvCheckedUpdatingUnderway As Boolean
    Private pISFTFacade As ISFTFacade

    Public Property MultiSelect() As Boolean
        Get
            Return pMultiSelect
        End Get
        Set(ByVal Value As Boolean)
            pMultiSelect = Value
        End Set
    End Property

    Public Function AddStrongFinalTypeToListView(ByRef ISFTFacade As ISFTFacade) As Boolean
        ' add listView items
        Dim eSFT As IEnumerator = ISFTFacade.FirstItem.Enumerator
        While eSFT.MoveNext
            lv.Items.Add(New Windows.Forms.ListViewItem(CType(eSFT.Current, ISFT).ToStringArray))
        End While
        ' set starting checked state
        For Each lvi As Windows.Forms.ListViewItem In lv.Items
            For Each ISFT As ISFT In ISFTFacade.SelectedItems
                If lvi.Text.Equals(ISFT.Name) Then
                    lvi.Checked = True
                End If
            Next
        Next
        pISFTFacade = ISFTFacade
    End Function

    Protected Sub form_Closed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Closed
        lv.Items.Clear()
        lvCheckedUpdatingUnderway = False
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        pISFTFacade.SelectedItems.Clear()

        For Each lvi As Windows.Forms.ListViewItem In lv.Items
            If lvi.Checked Then
                pISFTFacade.SelectedItems.Add(pISFTFacade.FirstItem.MatchString(lvi.Text))
            End If
        Next

        MyBase.DialogResult = Windows.Forms.DialogResult.OK
        MyBase.Close()
    End Sub

    ' throw away events if updating checked manually
    Private Sub ListViewItemCheck(ByVal sender As Object, ByVal e As ItemCheckEventArgs) Handles lv.ItemCheck
        If Not pMultiSelect AndAlso Not lvCheckedUpdatingUnderway Then
            lvCheckedUpdatingUnderway = True
            updateLVchecked(e.Index)
            lvCheckedUpdatingUnderway = False
        End If
    End Sub

    Private Sub updateLVchecked(ByVal ix As Int32)
        For Each lvi As Windows.Forms.ListViewItem In lv.Items
            lvi.Checked = False
        Next
        lv.Items(ix).Checked = True
    End Sub

    Private mouseDragging As Boolean
    Private draggingItem As ListViewItem
    Private targetItem As ListViewItem

    Private Sub lv_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lv.MouseDown
        Dim lvHitTestInfo As ListViewHitTestInfo = CType(sender, ListView).HitTest(e.X, e.Y)
        draggingItem = lvHitTestInfo.Item

        If draggingItem IsNot Nothing Then
            'Debug.WriteLine(draggingItem.Text)
            mouseDragging = True
        End If
    End Sub

    Private Sub lv_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lv.MouseMove
        If mouseDragging Then
            If CType(CType(sender, ListView).HitTest(e.X, e.Y), ListViewHitTestInfo).Item Is Nothing Then
                Me.Cursor = System.Windows.Forms.Cursors.No
            Else
                Me.Cursor = System.Windows.Forms.Cursors.UpArrow
            End If
        End If
    End Sub

    Private Sub lv_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lv.MouseUp
        If mouseDragging Then
            Dim lvHitTestInfo As ListViewHitTestInfo = CType(sender, ListView).HitTest(e.X, e.Y)
            targetItem = lvHitTestInfo.Item

            If targetItem IsNot Nothing Then
                'Debug.WriteLine(targetItem.Text)
                CType(sender, ListView).Items.Insert(targetItem.Index, CType(draggingItem.Clone, ListViewItem))
                draggingItem.Remove()
            End If

            mouseDragging = False
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End If
    End Sub

End Class
