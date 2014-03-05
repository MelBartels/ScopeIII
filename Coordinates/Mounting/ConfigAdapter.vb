Public Class ConfigAdapter

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
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As ConfigAdapter
        Return NestedInstance.INSTANCE
    End Function

    Private Class NestedInstance
        ' explicit constructor informs compiler not to mark type as beforefieldinit
        Shared Sub New()
        End Sub
        ' friend = internal, shared = static, readonly = final
        Friend Shared ReadOnly INSTANCE As ConfigAdapter = New ConfigAdapter
    End Class
#End Region

#Region "Constructors"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ConfigAdapter
    '    Return New ConfigAdapter
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function GetMounting(ByVal mountType As ISFT) As IMount
        Dim IMount As IMount = MountTemplate.GetInstance

        Dim IDictionary As IDictionary = AppConfig.GetInstance.GetMounting(mountType)
        IMount.CanMoveThruPriAxisPole = CBool(IDictionary.Item("CanMoveThruPriAxisPole"))
        IMount.CanMoveToPriAxisPole = CBool(IDictionary.Item("CanMoveToPriAxisPole"))
        IMount.MeridianFlip = MeridianFlip.GetInstance
        IMount.MeridianFlip.Possible = CBool(IDictionary.Item("MeridianFlipPossible"))
        IMount.MeridianFlip.Required = CBool(IDictionary.Item("MeridianFlipRequired"))
        IMount.MountType = mountType
        IMount.PriAxisAlign = PriAxisAlign.ISFT.MatchString(CStr(IDictionary.Item("PriAxisAlign")))
        IMount.PriAxisFullyRotates = CBool(IDictionary.Item("PriAxisFullyRotates"))

        Return IMount
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
