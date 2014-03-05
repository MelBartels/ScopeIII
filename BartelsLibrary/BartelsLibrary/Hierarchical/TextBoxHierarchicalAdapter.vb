#Region "Imports"
#End Region

Public Class TextBoxHierarchicalAdapter
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
    Private WithEvents pTb As Windows.Forms.TextBox
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As TextBoxHierarchicalAdapter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As TextBoxHierarchicalAdapter = New TextBoxHierarchicalAdapter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        pTb = New Windows.Forms.TextBox
    End Sub

    Public Shared Function GetInstance() As TextBoxHierarchicalAdapter
        Return New TextBoxHierarchicalAdapter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function AddChild(ByRef parent As Object, ByVal name As String) As Object Implements IHierarchical.AddChild
        If parent Is Nothing Then
            parent = String.Empty
        End If
        pTb.AppendText(CStr(parent) & name & vbCrLf)
        Return CStr(parent) & Constants.HierarchicalIncrement
    End Function

    Public Function RegisterComponent(ByRef component As Object) As Boolean Implements IHierarchical.RegisterComponent
        pTb = CType(component, Windows.Forms.TextBox)
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
