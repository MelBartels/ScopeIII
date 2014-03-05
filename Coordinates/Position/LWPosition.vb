Public Class LWPosition

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pRA As Double
    Private pDec As Double
    Private pRADisplay As String
    Private pDecDisplay As String
    Private pName As String
    Private pSource As String
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As LWPosition
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As LWPosition = New LWPosition
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As LWPosition
        Return New LWPosition
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property RA() As Double
        Get
            Return pRA
        End Get
        Set(ByVal Value As Double)
            pRA = Value
        End Set
    End Property

    Public Property Dec() As Double
        Get
            Return pDec
        End Get
        Set(ByVal Value As Double)
            pDec = Value
        End Set
    End Property

    Public Property RADisplay() As String
        Get
            Return pRADisplay
        End Get
        Set(ByVal Value As String)
            pRADisplay = Value
        End Set
    End Property

    Public Property DecDisplay() As String
        Get
            Return pDecDisplay
        End Get
        Set(ByVal Value As String)
            pDecDisplay = Value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return pName
        End Get
        Set(ByVal Value As String)
            pName = Value
        End Set
    End Property

    Public Property Source() As String
        Get
            Return pSource
        End Get
        Set(ByVal Value As String)
            pSource = Value
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
#End Region

End Class