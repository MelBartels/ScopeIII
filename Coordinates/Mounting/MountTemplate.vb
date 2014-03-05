Public Class MountTemplate
    Implements IMount

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pMountType As ISFT
    Private pPriAxisAlign As ISFT
    Private pCanMoveToPriAxisPole As Boolean
    Private pCanMoveThruPriAxisPole As Boolean
    Private pPriAxisFullyRotates As Boolean
    Private pMeridianFlip As MeridianFlip
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As MountTemplate
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MountTemplate = New MountTemplate
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MountTemplate
        Return New MountTemplate
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property CanMoveThruPriAxisPole() As Boolean Implements IMount.CanMoveThruPriAxisPole
        Get
            Return pCanMoveThruPriAxisPole
        End Get
        Set(ByVal Value As Boolean)
            pCanMoveThruPriAxisPole = Value
        End Set
    End Property

    Public Property CanMoveToPriAxisPole() As Boolean Implements IMount.CanMoveToPriAxisPole
        Get
            Return pCanMoveToPriAxisPole
        End Get
        Set(ByVal Value As Boolean)
            pCanMoveToPriAxisPole = Value
        End Set
    End Property

    Public Property MeridianFlip() As MeridianFlip Implements IMount.MeridianFlip
        Get
            Return pMeridianFlip
        End Get
        Set(ByVal Value As MeridianFlip)
            pMeridianFlip = Value
        End Set
    End Property

    Public Property MountType() As ISFT Implements IMount.MountType
        Get
            Return pMountType
        End Get
        Set(ByVal Value As ISFT)
            pMountType = Value
        End Set
    End Property

    Public Property PriAxisAlign() As ISFT Implements IMount.PriAxisAlign
        Get
            Return pPriAxisAlign
        End Get
        Set(ByVal Value As ISFT)
            pPriAxisAlign = Value
        End Set
    End Property

    Public Property PriAxisFullyRotates() As Boolean Implements IMount.PriAxisFullyRotates
        Get
            Return pPriAxisFullyRotates
        End Get
        Set(ByVal Value As Boolean)
            pPriAxisFullyRotates = False
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
