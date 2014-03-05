#Region "Imports"
#End Region
Imports System.Text

Public Class ExceptionService

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Public Shared QuietMode As Boolean
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As ExceptionService
        Return NestedInstance.INSTANCE
    End Function

    Private Class NestedInstance
        ' explicit constructor informs compiler not to mark type as beforefieldinit
        Shared Sub New()
        End Sub
        ' friend = internal, shared = static, readonly = final
        Friend Shared ReadOnly INSTANCE As ExceptionService = New ExceptionService
    End Class
#End Region

#Region "Constructors"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ExceptionService
    '    Return New ExceptionService
    'End Function
#End Region

#Region "Shared Methods"
    Public Shared Function Notify(ByVal humaneMsg As String) As MsgBoxResult
        Return Notify(Nothing, humaneMsg)
    End Function

    Public Shared Function Notify(ByRef ex As Exception) As MsgBoxResult
        Return Notify(ex, Nothing)
    End Function

    Public Shared Function Notify(ByRef ex As Exception, ByVal humaneMsg As String) As MsgBoxResult
        Dim sb As New Text.StringBuilder

        appendHumaneMessage(humaneMsg, sb)

        If ex IsNot Nothing Then
            appendLineIfNecessary(sb)
            appendExceptionMessage(sb, ex)
            appendInnerException(sb, ex)
            appendStackTrace(sb, ex)
        End If

        Dim rtnResult As MsgBoxResult
        If QuietMode Then
            rtnResult = MsgBoxResult.Ignore
        Else
            rtnResult = showMessageBox(sb, ex)
        End If

        LogService.LogMsg(sb.ToString)

        Return rtnResult
    End Function

    Protected Shared Function showMessageBox(ByVal sb As StringBuilder, ByRef ex As Exception) As MsgBoxResult
        Dim sbShow As New Text.StringBuilder
        sbShow.AppendLine(Constants.ErrorMessage)
        sbShow.Append(sb.ToString)
        Return AppMsgBox.Show(sbShow.ToString)
    End Function

    Protected Shared Sub appendHumaneMessage(ByVal humaneMsg As String, ByVal sb As StringBuilder)
        If Not String.IsNullOrEmpty(humaneMsg) Then
            sb.Append("Message: ")
            sb.Append(humaneMsg)
        End If
    End Sub

    Protected Shared Sub appendStackTrace(ByVal sb As StringBuilder, ByRef ex As Exception)
        sb.Append("Exception StackTrace: ")
        sb.Append(ex.StackTrace)
    End Sub

    Protected Shared Sub appendInnerException(ByVal sb As StringBuilder, ByRef ex As Exception)
        sb.Append("Exception InnerException: ")
        If ex Is Nothing OrElse ex.InnerException Is Nothing OrElse ex.InnerException.Message.Length.Equals(0) Then
            sb.AppendLine(String.Empty)
        Else
            sb.AppendLine(ex.InnerException.Message)
        End If
    End Sub

    Protected Shared Sub appendExceptionMessage(ByVal sb As StringBuilder, ByRef ex As Exception)
        sb.Append("Exception Message: ")
        sb.AppendLine(ex.Message)
    End Sub

    Protected Shared Sub appendLineIfNecessary(ByVal sb As StringBuilder)
        If sb.Length > 0 Then
            sb.AppendLine(String.Empty)
        End If
    End Sub
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
