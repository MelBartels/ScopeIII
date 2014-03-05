#Region "Imports"
#End Region

Public Class TreeViewHierarchicalAdapter
    Implements IHierarchical

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private WithEvents pTv As Windows.Forms.TreeView
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As TreeViewHierarchicalAdapter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As TreeViewHierarchicalAdapter = New TreeViewHierarchicalAdapter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        pTv = New Windows.Forms.TreeView
    End Sub

    Public Shared Function GetInstance() As TreeViewHierarchicalAdapter
        Return New TreeViewHierarchicalAdapter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function AddChild(ByRef parent As Object, ByVal name As String) As Object Implements IHierarchical.AddChild
        Dim newNode As New Windows.Forms.TreeNode(name)
        If parent Is Nothing Then
            pTv.Nodes.Add(newNode)
        Else
            CType(parent, Windows.Forms.TreeNode).Nodes.Add(newNode)
        End If
        Return newNode
    End Function

    Public Function RegisterComponent(ByRef component As Object) As Boolean Implements IHierarchical.RegisterComponent
        pTv = CType(component, Windows.Forms.TreeView)
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
