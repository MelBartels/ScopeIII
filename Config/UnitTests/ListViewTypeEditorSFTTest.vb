#Region "Imports"
#End Region

Public Class ListViewTypeEditorSFTTest
    Inherits ListViewTypeEditorBase

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
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ListViewTypeEditorSFTTest
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ListViewTypeEditorSFTTest = New ListViewTypeEditorSFTTest
    'End Class
#End Region

#Region "Constructors"
    ' must be public for .NET to instantiate
    Public Sub New()
        pISFTfacade = SFTtest.GetInstance
        pMultiSelect = True
    End Sub

    Public Shared Function GetInstance() As ListViewTypeEditorSFTTest
        Return New ListViewTypeEditorSFTTest
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
