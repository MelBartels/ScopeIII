#Region "Imports"
#End Region

Public Class JRKerrServoControl
    Inherits DevPropBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pOutputLimitOL As Byte
    Private pCurrentLimitCL As Byte
    Private pPositionErrorLimitEL As Int16
    Private pServoRateDivisorSR As Byte
    Private pAmpDeadbandCompensationDB As Byte
    Private pStepRateMultiplierSM As Byte
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As JRKerrServoControl
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '     explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '     friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As JRKerrServoControl = New JRKerrServoControl
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        DefaultSettings()
    End Sub

    Public Shared Function GetInstance() As JRKerrServoControl
        Return New JRKerrServoControl
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property OutputLimitOL() As Byte
        Get
            Return pOutputLimitOL
        End Get
        Set(ByVal value As Byte)
            pOutputLimitOL = value
        End Set
    End Property

    Public Property CurrentLimitCL() As Byte
        Get
            Return pCurrentLimitCL
        End Get
        Set(ByVal value As Byte)
            pCurrentLimitCL = value
        End Set
    End Property

    Public Property PositionErrorLimitEL() As Short
        Get
            Return pPositionErrorLimitEL
        End Get
        Set(ByVal value As Short)
            pPositionErrorLimitEL = value
        End Set
    End Property

    Public Property ServoRateDivisorSR() As Byte
        Get
            Return pServoRateDivisorSR
        End Get
        Set(ByVal value As Byte)
            pServoRateDivisorSR = value
        End Set
    End Property

    Public Property AmpDeadbandCompensationDB() As Byte
        Get
            Return pAmpDeadbandCompensationDB
        End Get
        Set(ByVal value As Byte)
            pAmpDeadbandCompensationDB = value
        End Set
    End Property

    Public Property StepRateMultiplierSM() As Byte
        Get
            Return pStepRateMultiplierSM
        End Get
        Set(ByVal value As Byte)
            pStepRateMultiplierSM = value
        End Set
    End Property

    Public Overrides Property Value() As String
        Get
            Return ToString()
        End Get
        Set(ByVal value As String)
            ' no setter
        End Set
    End Property

    Public Overrides Function Clone() As Object
        Return Me.MemberwiseClone
    End Function

    Public Overrides Function ToString() As String
        Dim sb As New Text.StringBuilder
        sb.Append(OutputLimitOL)
        sb.Append(", ")
        sb.Append(CurrentLimitCL)
        sb.Append(", ")
        sb.Append(PositionErrorLimitEL)
        sb.Append(", ")
        sb.Append(ServoRateDivisorSR)
        sb.Append(", ")
        sb.Append(AmpDeadbandCompensationDB)
        sb.Append(", ")
        sb.Append(StepRateMultiplierSM)
        Return sb.ToString
    End Function

    Public Sub DefaultSettings()
        OutputLimitOL = 255
        CurrentLimitCL = 255
        PositionErrorLimitEL = 16383
        ServoRateDivisorSR = 1
        AmpDeadbandCompensationDB = 0
        StepRateMultiplierSM = 1
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
