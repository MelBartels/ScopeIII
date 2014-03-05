#Region "imports"
Imports System.Windows.Forms
#End Region

Public Class frmTestMVCMController
    Inherits Controller

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public ReadOnly Property Frm() As ViewWin
        Get
            Return pFrm
        End Get
    End Property
#End Region

#Region "Private and Protected Members"
    ' view
    Private WithEvents pFrm As FrmTestMVCM

    ' events to handle
    Private WithEvents pBtn As Button
    Private WithEvents pDg As DataGrid

    ' components with events to handle

    ' components

    ' data models

    ' other

#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As frmTestMVCMController
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As frmTestMVCMController = New frmTestMVCMController
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As frmTestMVCMController
        Return New frmTestMVCMController
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub SetFormReferences()
    End Sub

    Protected Overrides Sub SetTypedModelReferences()
    End Sub

    Protected Overrides Sub SetTypedViewReference()
        pFrm = CType(pIView, FrmTestMVCM)
    End Sub

    Protected Overrides Sub SetViewDataBindings()
        ' nothing to implement here as data bindings created later by user actions that are handled by mediator objects
    End Sub

    Protected Overrides Sub SetViewWithEventsReferences()
        pBtn = pFrm.Button1
        pDg = pFrm.DataGrid1
    End Sub

    Protected Sub form_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pFrm.Load

    End Sub

    Private Sub btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pBtn.Click
        pDg.DataSource = createData()
        pDg.CaptionText = CType(pDg.DataSource, DataTable).TableName
    End Sub

    Private Function createData() As DataTable
        Dim dt As New DataTable("Names")
        dt.Columns.Add(New DataColumn("First"))
        dt.Columns.Add(New DataColumn("Last"))
        dt.LoadDataRow(New Object() {"Sam", "Brown"}, True)
        dt.LoadDataRow(New Object() {"Sara", "Black"}, True)
        Return dt
    End Function
#End Region

End Class
