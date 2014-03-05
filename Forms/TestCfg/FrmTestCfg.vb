Public Class FrmTestCfg
    Inherits System.Windows.Forms.Form

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
    Friend WithEvents PropertyGrid As System.Windows.Forms.PropertyGrid
    Friend WithEvents btnQuit As System.Windows.Forms.Button
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmTestCfg))
        Me.PropertyGrid = New System.Windows.Forms.PropertyGrid
        Me.btnQuit = New System.Windows.Forms.Button
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'PropertyGrid
        '
        Me.PropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar
        Me.PropertyGrid.Location = New System.Drawing.Point(8, 40)
        Me.PropertyGrid.Name = "PropertyGrid"
        Me.PropertyGrid.Size = New System.Drawing.Size(296, 344)
        Me.PropertyGrid.TabIndex = 0
        '
        'btnQuit
        '
        Me.btnQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnQuit.Location = New System.Drawing.Point(224, 392)
        Me.btnQuit.Name = "btnQuit"
        Me.btnQuit.Size = New System.Drawing.Size(75, 23)
        Me.btnQuit.TabIndex = 1
        Me.btnQuit.Text = "Quit"
        '
        'ComboBox1
        '
        Me.ComboBox1.Location = New System.Drawing.Point(56, 8)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(200, 21)
        Me.ComboBox1.TabIndex = 2
        Me.ComboBox1.Text = "ComboBox1"
        '
        'FrmTestCfg
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.btnQuit
        Me.ClientSize = New System.Drawing.Size(312, 422)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.btnQuit)
        Me.Controls.Add(Me.PropertyGrid)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmTestCfg"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TJ-OptiFab SettingsTest"
        Me.ResumeLayout(False)

    End Sub

#End Region

    Dim testParentClass As testParentClass
    Dim pDm As ArrayList
    Dim pTestPropContainer As TestPropContainer
    Dim pTestPropContainer2 As TestPropContainer2
    Dim pStrongFinalTypeTest As StrongFinalTypeTest
    Dim pStrongFinalTypeTest2 As StrongFinalTypeTest2
    Dim pGroupTest As GroupTest

    Private Sub SettingsTest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        testParentClass = New TestParentClass
        pTestPropContainer = New TestPropContainer
        pTestPropContainer2 = New TestPropContainer2
        pStrongFinalTypeTest = New StrongFinalTypeTest
        pStrongFinalTypeTest2 = New StrongFinalTypeTest2
        pGroupTest = New GroupTest

        pDm = New ArrayList
        pDm.Add(testParentClass)
        pDm.Add(pTestPropContainer.PropContainer)
        pDm.Add(pTestPropContainer.PropValues)
        pDm.Add(pTestPropContainer2.PropContainer)
        pDm.Add(pStrongFinalTypeTest.StrongFinalTypeTestContainer)
        pDm.Add(pStrongFinalTypeTest2.StrongFinalTypeTestContainer)
        For Each rb As RadioButton In pGroupTest.Group
            pDm.Add(rb)
        Next
        ComboBox1.DataSource = pDm

        PropertyGrid.SelectedObject = pDm(0)
    End Sub

    Private Sub btnQuit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuit.Click
        Me.Close()
    End Sub

    Private Sub ComboBox1pSelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Select Case ComboBox1.SelectedIndex
            Case 0
                PropertyGrid.SelectedObject = testParentClass
            Case 1
                PropertyGrid.SelectedObject = pTestPropContainer.PropContainer
            Case 2
                PropertyGrid.SelectedObject = pTestPropContainer.PropValues
            Case 3
                PropertyGrid.SelectedObject = pTestPropContainer2.PropContainer
            Case 4
                PropertyGrid.SelectedObject = pStrongFinalTypeTest.StrongFinalTypeTestContainer
            Case 5
                PropertyGrid.SelectedObject = pStrongFinalTypeTest2.StrongFinalTypeTestContainer
            Case 6
                pGroupTest.Group(0).Checked = True
                pGroupTest.Group(1).Checked = False
                pGroupTest.Group(2).Checked = False
                PropertyGrid.SelectedObject = pGroupTest.PropContainer
            Case 7
                pGroupTest.Group(0).Checked = False
                pGroupTest.Group(1).Checked = True
                pGroupTest.Group(2).Checked = False
                PropertyGrid.SelectedObject = pGroupTest.PropContainer
            Case 8
                pGroupTest.Group(0).Checked = False
                pGroupTest.Group(1).Checked = False
                pGroupTest.Group(2).Checked = True
                PropertyGrid.SelectedObject = pGroupTest.PropContainer
        End Select
    End Sub
End Class
