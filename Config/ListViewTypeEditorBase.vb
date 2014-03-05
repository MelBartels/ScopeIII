#Region "Imports"
Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.Windows.Forms.Design
#End Region

Public MustInherit Class ListViewTypeEditorBase
    Inherits UITypeEditor

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pService As IWindowsFormsEditorService
    Protected pISFTFacade As ISFTFacade
    Protected pMultiSelect As Boolean
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ListViewTypeEditorBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ListViewTypeEditorBase = New ListViewTypeEditorBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    'Public Shared Function GetInstance() As ListViewTypeEditorBase
    '    Return New ListViewTypeEditorBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overloads Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object) As Object
        If (Not context Is Nothing And Not context.Instance Is Nothing And Not provider Is Nothing) Then
            pService = CType(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
            If (Not pService Is Nothing) Then
                Dim selectStrongFinalTypeMediator As SelectStrongFinalTypeMediator = Config.SelectStrongFinalTypeMediator.GetInstance

                pISFTFacade.SelectedItemsAsCommaDelimitedString = CStr(value)

                If selectStrongFinalTypeMediator.GetSelection(pISFTFacade, pMultiSelect) Then
                    Return pISFTFacade.SelectedItemsAsCommaDelimitedString
                End If
            End If
        End If
        Return value
    End Function

    Public Overloads Overrides Function GetEditStyle(ByVal context As ITypeDescriptorContext) As UITypeEditorEditStyle
        If (Not context Is Nothing And Not context.Instance Is Nothing) Then
            Return UITypeEditorEditStyle.Modal
        End If
        Return MyBase.GetEditStyle(context)
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
