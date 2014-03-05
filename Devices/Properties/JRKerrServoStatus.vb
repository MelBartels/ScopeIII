#Region "Imports"
#End Region

Public Class JRKerrServoStatus
    Inherits DevPropBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public ModuleType As Byte
    Public ModuleVersion As Byte
    Public GroupAddress As Byte
    Public GroupLeader As Boolean

    Public Status As Byte
    Public AuxStatus As Byte

    Public Position As Int32
    Public ServoPositionError As Int16
    Public HomePosition As Int32
    Public AD As Byte
    Public Velocity As Int16
    Public PathPointsInBuffer As Byte
    Public ChecksumIsValid As Boolean
#End Region

#Region "Private and Protected Members"
    Protected pStatusItems As Byte
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As JRKerrServoStatus
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '     explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '     friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As JRKerrServoStatus = New JRKerrServoStatus
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        SetToDefaults()
    End Sub

    Public Shared Function GetInstance() As JRKerrServoStatus
        Return New JRKerrServoStatus
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub SetToDefaults()
        MoveDone = True
    End Sub

#Region "status"
    Public Property MoveDone() As Boolean
        Get
            Return getStatus(NMCDefines.MOVE_DONE)
        End Get
        Set(ByVal value As Boolean)
            setStatus(NMCDefines.MOVE_DONE, value)
        End Set
    End Property

    Public Property ChecksumError() As Boolean
        Get
            Return getStatus(NMCDefines.CKSUM_ERROR)
        End Get
        Set(ByVal value As Boolean)
            setStatus(NMCDefines.CKSUM_ERROR, value)
        End Set
    End Property

    Public Property OverCurrent() As Boolean
        Get
            Return getStatus(NMCDefines.OVERCURRENT)
        End Get
        Set(ByVal value As Boolean)
            setStatus(NMCDefines.OVERCURRENT, value)
        End Set
    End Property

    Public Property PowerOn() As Boolean
        Get
            Return getStatus(NMCDefines.POWER_ON)
        End Get
        Set(ByVal value As Boolean)
            setStatus(NMCDefines.POWER_ON, value)
        End Set
    End Property

    Public Property PositionError() As Boolean
        Get
            Return getStatus(NMCDefines.POS_ERR)
        End Get
        Set(ByVal value As Boolean)
            setStatus(NMCDefines.POS_ERR, value)
        End Set
    End Property

    Public Property Limit1() As Boolean
        Get
            Return getStatus(NMCDefines.LIMIT1)
        End Get
        Set(ByVal value As Boolean)
            setStatus(NMCDefines.LIMIT1, value)
        End Set
    End Property

    Public Property Limit2() As Boolean
        Get
            Return getStatus(NMCDefines.LIMIT2)
        End Get
        Set(ByVal value As Boolean)
            setStatus(NMCDefines.LIMIT2, value)
        End Set
    End Property

    Public Property HomeInProgress() As Boolean
        Get
            Return getStatus(NMCDefines.HOME_IN_PROG)
        End Get
        Set(ByVal value As Boolean)
            setStatus(NMCDefines.HOME_IN_PROG, value)
        End Set
    End Property
#End Region

#Region "aux status"
    Public Property Index() As Boolean
        Get
            Return getAuxStatus(NMCDefines.INDEX)
        End Get
        Set(ByVal value As Boolean)
            setAuxStatus(NMCDefines.INDEX, value)
        End Set
    End Property

    Public Property PositionWrap() As Boolean
        Get
            Return getAuxStatus(NMCDefines.POS_WRAP)
        End Get
        Set(ByVal value As Boolean)
            setAuxStatus(NMCDefines.POS_WRAP, value)
        End Set
    End Property

    Public Property ServoOn() As Boolean
        Get
            Return getAuxStatus(NMCDefines.SERVO_ON)
        End Get
        Set(ByVal value As Boolean)
            setAuxStatus(NMCDefines.SERVO_ON, value)
        End Set
    End Property

    Public Property AccelerationDone() As Boolean
        Get
            Return getAuxStatus(NMCDefines.ACCEL_DONE)
        End Get
        Set(ByVal value As Boolean)
            setAuxStatus(NMCDefines.ACCEL_DONE, value)
        End Set
    End Property

    Public Property SlewDone() As Boolean
        Get
            Return getAuxStatus(NMCDefines.SLEW_DONE)
        End Get
        Set(ByVal value As Boolean)
            setAuxStatus(NMCDefines.SLEW_DONE, value)
        End Set
    End Property

    Public Property ServoOverrun() As Boolean
        Get
            Return getAuxStatus(NMCDefines.SERVO_OVERRUN)
        End Get
        Set(ByVal value As Boolean)
            setAuxStatus(NMCDefines.SERVO_OVERRUN, value)
        End Set
    End Property

    Public Property PathMode() As Boolean
        Get
            Return getAuxStatus(NMCDefines.PATH_MODE)
        End Get
        Set(ByVal value As Boolean)
            setAuxStatus(NMCDefines.PATH_MODE, value)
        End Set
    End Property
#End Region

#Region "status items"
    Public Property StatusItems() As Byte
        Get
            Return pStatusItems
        End Get
        Set(ByVal value As Byte)
            pStatusItems = value
            ObservableImp.Notify(CType(value, Object))
        End Set
    End Property

    Public Property SendPosition() As Boolean
        Get
            Return getStatusItem(SEND_POS)
        End Get
        Set(ByVal value As Boolean)
            setStatusItem(SEND_POS, value)
        End Set
    End Property

    Public Property SendAD() As Boolean
        Get
            Return getStatusItem(SEND_AD)
        End Get
        Set(ByVal value As Boolean)
            setStatusItem(SEND_AD, value)
        End Set
    End Property

    Public Property SendVelocity() As Boolean
        Get
            Return getStatusItem(SEND_VEL)
        End Get
        Set(ByVal value As Boolean)
            setStatusItem(SEND_VEL, value)
        End Set
    End Property

    Public Property SendAuxStatus() As Boolean
        Get
            Return getStatusItem(SEND_AUX)
        End Get
        Set(ByVal value As Boolean)
            setStatusItem(SEND_AUX, value)
        End Set
    End Property

    Public Property SendHome() As Boolean
        Get
            Return getStatusItem(SEND_HOME)
        End Get
        Set(ByVal value As Boolean)
            setStatusItem(SEND_HOME, value)
        End Set
    End Property

    Public Property SendID() As Boolean
        Get
            Return getStatusItem(SEND_ID)
        End Get
        Set(ByVal value As Boolean)
            setStatusItem(SEND_ID, value)
        End Set
    End Property

    Public Property SendPositionError() As Boolean
        Get
            Return getStatusItem(SEND_PERROR)
        End Get
        Set(ByVal value As Boolean)
            setStatusItem(SEND_PERROR, value)
        End Set
    End Property

    Public Property SendNumberOfPathPoints() As Boolean
        Get
            Return getStatusItem(SEND_NPOINTS)
        End Get
        Set(ByVal value As Boolean)
            setStatusItem(SEND_NPOINTS, value)
        End Set
    End Property
#End Region

#Region "byte counts"
    Public Function PositionByteCount() As Int32
        Return 4
    End Function

    Public Function ADByteCount() As Int32
        Return 1
    End Function

    Public Function VelocityByteCount() As Int32
        Return 2
    End Function

    Public Function AuxSatusByteCount() As Int32
        Return 1
    End Function

    Public Function HomeByteCount() As Int32
        Return 4
    End Function

    Public Function IDByteCount() As Int32
        Return 2
    End Function

    Public Function PositionErrorByteCount() As Int32
        Return 2
    End Function

    Public Function NumberOfPathPointsByteCount() As Int32
        Return 1
    End Function
#End Region

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
        sb.Append(ModuleType)
        sb.Append(", ")
        sb.Append(ModuleVersion)
        sb.Append(", ")
        sb.Append(GroupAddress)
        sb.Append(", ")
        sb.Append(GroupLeader)
        sb.Append(", ")
        sb.Append(StatusItems)
        sb.Append(", ")
        sb.Append(Status)
        sb.Append(", ")
        sb.Append(AuxStatus)
        sb.Append(", ")
        sb.Append(Position)
        sb.Append(", ")
        sb.Append(ServoPositionError)
        sb.Append(", ")
        sb.Append(HomePosition)
        sb.Append(", ")
        sb.Append(AD)
        sb.Append(", ")
        sb.Append(Velocity)
        sb.Append(", ")
        sb.Append(PathPointsInBuffer)
        sb.Append(", ")
        sb.Append(ChecksumIsValid)
        Return sb.ToString
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Function getStatus(ByVal bit As Byte) As Boolean
        Return CBool(Status And bit)
    End Function

    Protected Sub setStatus(ByVal bit As Byte, ByVal value As Boolean)
        Status = CByte(Status And (255 - bit))
        If value Then
            Status += bit
        End If
    End Sub

    Protected Function getAuxStatus(ByVal bit As Byte) As Boolean
        Return CBool(AuxStatus And bit)
    End Function

    Protected Sub setAuxStatus(ByVal bit As Byte, ByVal value As Boolean)
        AuxStatus = CByte(AuxStatus And (255 - bit))
        If value Then
            AuxStatus += bit
        End If
    End Sub

    Protected Function getStatusItem(ByVal bit As Byte) As Boolean
        Return CBool(StatusItems And bit)
    End Function

    Protected Sub setStatusItem(ByVal bit As Byte, ByVal value As Boolean)
        Dim mask As Byte = CByte(255 - bit)
        Dim maskedStatusItems As Byte = StatusItems And mask
        If value Then
            StatusItems = maskedStatusItems + bit
        Else
            StatusItems = maskedStatusItems
        End If
    End Sub
#End Region

End Class
