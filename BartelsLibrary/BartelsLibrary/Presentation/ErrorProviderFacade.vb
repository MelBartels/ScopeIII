#Region "imports"
Imports System.Windows.Forms
#End Region

Public Class ErrorProviderFacade

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

    'Public Shared Function GetInstance() As ErrorProviderFacade
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    public sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ErrorProviderFacade = New ErrorProviderFacade
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As ErrorProviderFacade
        Return New ErrorProviderFacade
    End Function
#End Region

#Region "Shared Methods"
    Public Sub Clear(ByRef errorProvider As ErrorProvider, ByRef txtBx As TextBox)
        errorProvider.SetIconAlignment(txtBx, ErrorIconAlignment.MiddleRight)
        ' moves icon in from the right
        errorProvider.SetIconPadding(txtBx, -10)
        ' w/o this, icon never clears
        errorProvider.SetError(txtBx, "")
    End Sub

    Public Sub ShowNonNumeric(ByRef errorProvider As ErrorProvider, ByRef txtBx As TextBox)
        errorProvider.SetError(txtBx, "Error: ' " & txtBx.Text & " ' is not numeric.")
    End Sub

    Public Sub ShowNonExistent(ByRef errorProvider As ErrorProvider, ByRef txtBx As TextBox)
        errorProvider.SetError(txtBx, "Error: ' " & txtBx.Text & " ' does not exist or is not accessible.")
    End Sub

    Public Sub ShowInvalidFormat(ByRef errorProvider As ErrorProvider, ByRef txtBx As TextBox)
        errorProvider.SetError(txtBx, "Error: ' " & txtBx.Text & " ' is not a valid format.")
    End Sub

    Public Sub ShowRange_1_To_90(ByRef errorProvider As ErrorProvider, ByRef txtBx As TextBox)
        errorProvider.SetError(txtBx, "Error: ' " & txtBx.Text & " ' must be between 1 and 90.")
    End Sub

#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
#End Region

End Class

