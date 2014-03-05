#Region "Imports"
#End Region

Public Class PreLoadConfig

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

    'Public Shared Function GetInstance() As PreLoadConfig
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As PreLoadConfig = New PreLoadConfig
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As PreLoadConfig
        Return New PreLoadConfig
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' include types before serialize/deserialize that occurs in load/save
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[mbartels]	8/11/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function IncludeTypes() As Boolean
        Dim sf As SettingsFacade = SettingsFacade.GetInstance

        sf.IncludeType(GetType(SerialSettings))
        sf.IncludeType(GetType(IPsettings))

        Return True
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
