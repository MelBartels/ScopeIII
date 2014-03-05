Imports System.IO

Public Class DatafileReader

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Dim pReader As StreamReader
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DatafileReader
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DatafileReader = New DatafileReader
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As DatafileReader
        Return New DatafileReader
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function Open(ByVal filename As String) As Boolean
        Try
            pReader = New StreamReader(filename)
        Catch ex As Exception
            ExceptionService.Notify(ex)
            Return False
        End Try

        Return True
    End Function

    Public Function ReadValues(ByRef RA As Double, ByRef Dec As Double, ByRef name As String) As Boolean
        Dim line As String
        line = pReader.ReadLine
        If line IsNot Nothing Then
            Dim st As StringTokenizer = StringTokenizer.GetInstance
            st.Tokenize(line)
            If st.Count >= 6 Then
                RA = CommonCalcs.GetInstance.ParseCoordinateValueRA(st)
                Dec = CommonCalcs.GetInstance.ParseCoordinateValueDec(st)
                name = st.StringToEOL
                Return True
            End If
        End If

        Return False
    End Function

    Public Function Close() As Boolean
        pReader.Close()
        Return True
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
