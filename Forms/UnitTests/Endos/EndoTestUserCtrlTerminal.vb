#Region "Imports"
#End Region

Public Class EndoTestUserCtrlTerminal
    Inherits UserCtrlTerminal

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    '    Private Sub New()
    '    End Sub

    '    Public Shared Function GetInstance() As EndoTestUserCtrlTerminal
    '        Return NestedInstance.INSTANCE
    '    End Function

    '    Private Class NestedInstance
    '        ' explicit constructor informs compiler not to mark type as beforefieldinit
    '        Shared Sub New()
    '        End Sub
    '        ' friend = internal, shared = static, readonly = final
    '        Friend Shared ReadOnly INSTANCE As EndoTestUserCtrlTerminal = New EndoTestUserCtrlTerminal
    '    End Class
#End Region

#Region "Constructors"
    Private Sub New()
        setComboBoxesVisible(False)
    End Sub

    Public Shared Shadows Function GetInstance() As EndoTestUserCtrlTerminal
        Return New EndoTestUserCtrlTerminal
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    ' override writing to RichTextBox as it fails during unit testing
    Public Overrides Function AppendText(ByRef [object] As Object) As Boolean
        Debug.WriteLine("test terminal displaying: " & CStr([object]))
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Sub setComboBoxesVisible(ByVal visible As Boolean)
        cmbBxPortType.Visible = visible
        cmbBxDisplayType.Visible = visible
    End Sub
#End Region

End Class
