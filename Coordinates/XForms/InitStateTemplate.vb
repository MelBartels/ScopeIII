Public Class InitStateTemplate

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pCoordXformType As ISFT
    Private pInitStateType As ISFT
    Private pIcoordXform As ICoordXform
    Private pIinit As IInit
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As InitStateTemplate
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As InitStateTemplate = New InitStateTemplate
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As InitStateTemplate
        Return New InitStateTemplate
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property CoordXformType() As ISFT
        Get
            Return pCoordXformType
        End Get
        Set(ByVal Value As ISFT)
            pCoordXformType = Value
        End Set
    End Property

    Public Property InitStateType() As ISFT
        Get
            Return pInitStateType
        End Get
        Set(ByVal Value As ISFT)
            pInitStateType = Value
        End Set
    End Property

    Public Property ICoordXform() As ICoordXform
        Get
            Return pIcoordXform
        End Get
        Set(ByVal Value As ICoordXform)
            pIcoordXform = Value
        End Set
    End Property

    ' InitBase implements IInit and InitConvertMatrix.. + InitDoNothing subclasses inherit from InitBase
    Public Property IInit() As IInit
        Get
            Return pIinit
        End Get
        Set(ByVal Value As IInit)
            pIinit = Value
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
#End Region
End Class
