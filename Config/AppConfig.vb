#Region "Imports"
Imports System.Configuration
Imports System.Collections.Specialized
#End Region

Public Class AppConfig

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private Const SharedGroupnameMounting As String = "Mounting/"
#End Region

#Region "Constructors (Singleton Pattern)"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As AppConfig
        Return NestedInstance.INSTANCE
    End Function

    Private Class NestedInstance
        ' explicit constructor informs compiler not to mark type as beforefieldinit
        Shared Sub New()
        End Sub
        ' friend = internal, shared = static, readonly = final
        Friend Shared ReadOnly INSTANCE As AppConfig = New AppConfig
    End Class
#End Region

#Region "Constructors"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As AppConfig
    '    Return New AppConfig
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function GetMounting(ByVal mountType As ISFT) As IDictionary
        Return CType(ConfigurationManager.GetSection(SharedGroupnameMounting & mountType.Name), IDictionary)
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
