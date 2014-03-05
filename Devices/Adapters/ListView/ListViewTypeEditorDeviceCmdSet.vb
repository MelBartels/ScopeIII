#Region "Imports"
#End Region

Public Class ListViewTypeEditorDeviceCmdSet
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

    'Public Shared Function GetInstance() As ListViewTypeEditorDeviceCmdSet
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ListViewTypeEditorDeviceCmdSet = New ListViewTypeEditorDeviceCmdSet
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        pISFTFacade = DeviceCmdSet.GetInstance
        pMultiSelect = False
    End Sub

    Public Shared Function GetInstance() As ListViewTypeEditorDeviceCmdSet
        Return New ListViewTypeEditorDeviceCmdSet
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
