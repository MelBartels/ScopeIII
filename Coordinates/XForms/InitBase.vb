Public MustInherit Class InitBase
    Implements IInit

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pIcoordXform As ICoordXform
    Private pInitType As ISFT
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As InitBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As InitBase = New InitBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    'Public Shared Function GetInstance() As InitBase
    '    Return New InitBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property ICoordXform() As ICoordXform Implements IInit.ICoordXform
        Get
            Return pIcoordXform
        End Get
        Set(ByVal Value As ICoordXform)
            pIcoordXform = Value
        End Set
    End Property

    Public Property InitType() As ISFT Implements IInit.InitType
        Get
            Return pInitType
        End Get
        Set(ByVal Value As ISFT)
            pInitType = Value
        End Set
    End Property

    Public MustOverride Function Init() As Boolean Implements IInit.Init
#End Region

#Region "Private and Protected Methods"
#End Region

End Class