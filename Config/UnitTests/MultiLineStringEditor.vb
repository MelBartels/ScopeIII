Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.Windows.Forms.Design

Public Class MultiLineStringEditor
    Inherits UITypeEditor
    Private pService As IWindowsFormsEditorService

    Public Overloads Overrides Function EditValue(ByVal context As ITypeDescriptorContext, ByVal provider As IServiceProvider, ByVal value As Object) As Object
        If (Not context Is Nothing And Not context.Instance Is Nothing And Not provider Is Nothing) Then
            pService = CType(provider.GetService(GetType(IWindowsFormsEditorService)), IWindowsFormsEditorService)
            If (Not pService Is Nothing) Then
                ' Edit changes the value if user clicks OK
                MultiLineStringEditorForm.Edit(CType(value, MultiLineString), "Edit " & context.PropertyDescriptor.Name)
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
End Class