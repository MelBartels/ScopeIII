#Region "imports"
Imports System.Windows.Forms
Imports System.IO
#End Region

Public Class Validate

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "public Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Dim pErrorProviderFacade As ErrorProviderFacade
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As Validate
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    public Sub New()
    '    End Sub
    '    ' friend = internal, public = static, readonly = final
    '    Friend public ReadOnly INSTANCE As Validate = New Validate
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        pErrorProviderFacade = ErrorProviderFacade.GetInstance
    End Sub

    Public Shared Function GetInstance() As Validate
        Return New Validate
    End Function
#End Region

#Region "public Methods"
    Public Function ValidateEpoch(ByRef errorProvider As ErrorProvider, ByRef textBox As TextBox) As Boolean
        pErrorProviderFacade.Clear(errorProvider, textBox)

        Try
            Double.Parse(textBox.Text)
            Return True
        Catch ex As Exception
            pErrorProviderFacade.ShowNonNumeric(errorProvider, textBox)
            Return False
        End Try
    End Function

    Public Function ValidateSource(ByRef errorProvider As ErrorProvider, ByRef textBox As TextBox) As Boolean
        pErrorProviderFacade.Clear(errorProvider, textBox)

        If Directory.Exists(textBox.Text) _
        OrElse File.Exists(textBox.Text) Then
            Return True
        Else
            pErrorProviderFacade.ShowNonExistent(errorProvider, textBox)
            Return False
        End If
    End Function

    Public Function ValidateDir(ByRef errorProvider As ErrorProvider, ByRef textBox As TextBox) As Boolean
        pErrorProviderFacade.Clear(errorProvider, textBox)

        If Directory.Exists(textBox.Text) Then
            Return True
        Else
            pErrorProviderFacade.ShowNonExistent(errorProvider, textBox)
            Return False
        End If
    End Function

    Public Function ValidateFile(ByRef errorProvider As ErrorProvider, ByRef textBox As TextBox) As Boolean
        pErrorProviderFacade.Clear(errorProvider, textBox)

        If File.Exists(textBox.Text) Then
            Return True
        Else
            pErrorProviderFacade.ShowNonExistent(errorProvider, textBox)
            Return False
        End If
    End Function

    Public Function ValidateTextBoxAsDouble(ByRef errorProvider As ErrorProvider, ByRef textBox As TextBox, ByRef value As Double) As Boolean
        pErrorProviderFacade.Clear(errorProvider, textBox)

        If Double.TryParse(textBox.Text, value) Then
            Return True
        Else
            pErrorProviderFacade.ShowNonNumeric(errorProvider, textBox)
            Return False
        End If
    End Function

    Public Function ValidateTextBoxAsRad(ByRef errorProvider As ErrorProvider, ByRef textBox As TextBox, ByRef rad As Double) As Boolean
        pErrorProviderFacade.Clear(errorProvider, textBox)

        Try
            rad = CoordinateParser.GetInstance.Parse(textBox.Text)
            Return True
        Catch ex As Exception
            pErrorProviderFacade.ShowInvalidFormat(errorProvider, textBox)
            Return False
        End Try
    End Function

    Public Function ValidateTextBoxAsInt_1_To_90(ByRef errorProvider As ErrorProvider, ByRef textBox As TextBox, ByRef count As Int32) As Boolean
        pErrorProviderFacade.Clear(errorProvider, textBox)

        Try
            count = Int32.Parse(textBox.Text)
            If count >= 1 AndAlso count <= 90 Then
                Return True
            End If
            pErrorProviderFacade.ShowRange_1_To_90(errorProvider, textBox)
            Return False
        Catch ex As Exception
            pErrorProviderFacade.ShowInvalidFormat(errorProvider, textBox)
            Return False
        End Try
    End Function

#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
