Public Class DisplayPosition

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pName As String
    Private pRaRad As Double
    Private pDecRad As Double
    Private pAzRad As Double
    Private pAltRad As Double
    Private pRADisplay As String
    Private pDecDisplay As String
    Private pAzDisplay As String
    Private pAltDisplay As String
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DisplayPosition
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DisplayPosition = New DisplayPosition
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As DisplayPosition
        Return New DisplayPosition
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property Name() As String
        Get
            Return pName
        End Get
        Set(ByVal Value As String)
            pName = Value
        End Set
    End Property

    Public Property RA() As Double
        Get
            Return pRARad
        End Get
        Set(ByVal value As Double)
            pRARad = value
        End Set
    End Property

    Public Property Dec() As Double
        Get
            Return pDecRad
        End Get
        Set(ByVal value As Double)
            pDecRad = value
        End Set
    End Property

    Public Property Az() As Double
        Get
            Return pAzRad
        End Get
        Set(ByVal value As Double)
            pAzRad = value
        End Set
    End Property

    Public Property Alt() As Double
        Get
            Return pAltRad
        End Get
        Set(ByVal value As Double)
            pAltRad = value
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

    Public Property AzDisplay() As String
        Get
            Return pAzDisplay
        End Get
        Set(ByVal Value As String)
            pAzDisplay = Value
        End Set
    End Property

    Public Property AltDisplay() As String
        Get
            Return pAltDisplay
        End Get
        Set(ByVal Value As String)
            pAltDisplay = Value
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
#End Region

End Class