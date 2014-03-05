#Region "Imports"
#End Region

Public Class ConsoleHelper

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

    '    Public Shared Function GetInstance() As ConsoleHelper
    '        Return NestedInstance.INSTANCE
    '    End Function

    '    Private Class NestedInstance
    '        ' explicit constructor informs compiler not to mark type as beforefieldinit
    '        Shared Sub New()
    '        End Sub
    '        ' friend = internal, shared = static, readonly = final
    '        Friend Shared ReadOnly INSTANCE As ConsoleHelper = New ConsoleHelper
    '    End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As ConsoleHelper
        Return New ConsoleHelper
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function GetInt(ByVal prompt As String, ByRef v As Int32) As Boolean
        displayPrompt(prompt)
        Return Int32.TryParse(readTokenFromConsole(), v)
    End Function

    Public Function GetDouble(ByVal prompt As String, ByRef v As Double) As Boolean
        displayPrompt(prompt)
        Return Double.TryParse(readTokenFromConsole(), v)
    End Function

    Public Function GetString(ByVal prompt As String, ByRef v As String) As Boolean
        displayPrompt(prompt)
        v = readConsole()
        Return Not String.IsNullOrEmpty(v)
    End Function

    Public Sub PressReturnToContinue()
        displayPrompt("Press return to continue. ")
        readConsole()
    End Sub
#End Region

#Region "Private and Protected Methods"
    Protected Overridable Sub displayPrompt(ByVal prompt As String)
        Console.Write(prompt + " ")
    End Sub

    Protected Overridable Function readConsole() As String
        Return Console.ReadLine()
    End Function

    Protected Function readTokenFromConsole() As String
        Dim st As StringTokenizer = StringTokenizer.GetInstance()
        st.Tokenize(readConsole)
        If (st.Count > 0) Then
            Return st.NextToken()
        Else
            Return Nothing
        End If
    End Function
#End Region

End Class
