#Region "Imports"
#End Region

Public Class JRKerrUtil

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public Const HEADER_BYTE As Byte = &HAA
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Enum Indeces
        Header
        Address
        Cmd
    End Enum
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As JRKerrUtil
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '     explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '     friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As JRKerrUtil = New JRKerrUtil
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As JRKerrUtil
        Return New JRKerrUtil
    End Function
#End Region

#Region "Shared Methods"
    Public Shared Sub SetCmdChecksum(ByVal bytes() As Byte)
        ' last byte is the checksum, checksum ignores the header byte (1st byte of the cmd)
        bytes(bytes.Length - 1) = BartelsLibrary.Checksum.Calc(bytes, 1, bytes.Length - 2)
    End Sub

    Public Shared Sub SetChecksum(ByVal bytes() As Byte)
        ' last byte is the checksum
        bytes(bytes.Length - 1) = BartelsLibrary.Checksum.Calc(bytes, 0, bytes.Length - 1)
    End Sub

    Public Shared Function VerifyCmdChecksum(ByVal bytes() As Byte) As Boolean
        Dim checkSum As Byte = bytes(bytes.Length - 1)
        SetCmdChecksum(bytes)
        Dim recalcCheckSum As Byte = bytes(bytes.Length - 1)
        Return checkSum.Equals(recalcCheckSum)
    End Function

    Public Shared Function VerifyChecksum(ByVal bytes() As Byte) As Boolean
        Dim checkSum As Byte = bytes(bytes.Length - 1)
        SetChecksum(bytes)
        Dim recalcCheckSum As Byte = bytes(bytes.Length - 1)
        Return checkSum.Equals(recalcCheckSum)
    End Function

    Public Shared Sub SetAddress(ByVal IDevice As IDevice, ByVal cmdBytes() As Byte)
        cmdBytes(Indeces.Address) = CByte(IDevice.GetValueObject(ValueObjectNames.ModuleAddress.Name).Value)
    End Sub

    Public Shared Sub EncodeStatusValues(ByRef IDevice As IDevice)
        Dim jrk As JRKerrServoController = CType(IDevice, JRKerrServoController)
        With GetJRKerrServoStatus(IDevice)
            Dim encoder As Encoder = CType(jrk.GetDevicesByName(ServoMotor.MotorEncoderName).Item(0), Encoder)
            .Position = eMath.RInt(encoder.Value)
        End With
    End Sub

    Public Shared Sub DecodeStatusValues(ByRef IDevice As IDevice)
        Dim jrk As JRKerrServoController = CType(IDevice, JRKerrServoController)
        With GetJRKerrServoStatus(IDevice)
            Dim encoder As Encoder = CType(jrk.GetDevicesByName(ServoMotor.MotorEncoderName).Item(0), Encoder)
            encoder.Value = CStr(.Position)
        End With
    End Sub

    ' always return at least status byte and checksum byte
    Public Shared Function EncodeStatusBytes(ByRef IDevice As IDevice) As Byte()
        EncodeStatusValues(IDevice)
        Dim jrk As JRKerrServoController = CType(IDevice, JRKerrServoController)
        With GetJRKerrServoStatus(IDevice)

            Dim rtnStatusBytes(StatusBytesCount(IDevice) - 1) As Byte
            Dim ix As Int32 = 0

            rtnStatusBytes(ix) = .Status
            ix += 1
            If .SendPosition Then
                BitConverter.GetBytes(.Position).CopyTo(rtnStatusBytes, ix)
                ix += .PositionByteCount
            End If
            If .SendAD Then
                rtnStatusBytes(ix) = .AD
                ix += .ADByteCount
            End If
            If .SendVelocity Then
                BitConverter.GetBytes(.Velocity).CopyTo(rtnStatusBytes, ix)
                ix += .VelocityByteCount
            End If
            If .SendAuxStatus Then
                rtnStatusBytes(ix) = .AuxStatus
                ix += .AuxSatusByteCount
            End If
            If .SendHome Then
                BitConverter.GetBytes(.HomePosition).CopyTo(rtnStatusBytes, ix)
                ix += .HomeByteCount
            End If
            If .SendID Then
                rtnStatusBytes(ix) = .ModuleType
                ix += 1
                rtnStatusBytes(ix) = .ModuleVersion
                ix += 1
            End If
            If .SendPositionError Then
                BitConverter.GetBytes(.ServoPositionError).CopyTo(rtnStatusBytes, ix)
                ix += .PositionErrorByteCount
            End If
            If .SendNumberOfPathPoints Then
                rtnStatusBytes(ix) = .PathPointsInBuffer
                ix += .PositionErrorByteCount
            End If
            JRKerrUtil.SetChecksum(rtnStatusBytes)

            Return rtnStatusBytes
        End With
    End Function

    Public Shared Sub DecodeStatusBytes(ByRef IDevice As IDevice, ByVal rtnStatusBytes() As Byte)
        Dim jrk As JRKerrServoController = CType(IDevice, JRKerrServoController)
        With GetJRKerrServoStatus(IDevice)

            Dim ix As Int32 = 0
            .Status = rtnStatusBytes(ix)
            ix += 1
            If .SendPosition Then
                .Position = BitConverter.ToInt32(rtnStatusBytes, ix)
                ix += .PositionByteCount
            End If
            If .SendAD Then
                .AD = rtnStatusBytes(ix)
                ix += .ADByteCount
            End If
            If .SendVelocity Then
                .Velocity = BitConverter.ToInt16(rtnStatusBytes, ix)
                ix += .VelocityByteCount
            End If
            If .SendAuxStatus Then
                .AuxStatus = rtnStatusBytes(ix)
                ix += .AuxSatusByteCount
            End If
            If .SendHome Then
                .HomePosition = BitConverter.ToInt32(rtnStatusBytes, ix)
                ix += .HomeByteCount
            End If
            If .SendID Then
                .ModuleType = rtnStatusBytes(ix)
                ix += 1
                .ModuleVersion = rtnStatusBytes(ix)
                ix += 1
            End If
            If .SendPositionError Then
                .ServoPositionError = BitConverter.ToInt16(rtnStatusBytes, ix)
                ix += .PositionErrorByteCount
            End If
            If .SendNumberOfPathPoints Then
                .PathPointsInBuffer = rtnStatusBytes(ix)
                ix += .PositionErrorByteCount
            End If
            .ChecksumIsValid = rtnStatusBytes(ix).Equals(BartelsLibrary.Checksum.Calc(rtnStatusBytes, 0, rtnStatusBytes.Length - 1))
        End With

        DecodeStatusValues(IDevice)
    End Sub

    Public Shared Function StatusBytesCount(ByRef IDevice As IDevice) As Int32
        With GetJRKerrServoStatus(IDevice)
            ' always return at least status byte and checksum
            Dim count As Int32 = 2
            If .SendPosition Then
                count += .PositionByteCount
            End If
            If .SendAD Then
                count += .ADByteCount
            End If
            If .SendVelocity Then
                count += .VelocityByteCount
            End If
            If .SendAuxStatus Then
                count += .AuxSatusByteCount
            End If
            If .SendHome Then
                count += .HomeByteCount
            End If
            If .SendID Then
                count += .IDByteCount
            End If
            If .SendPositionError Then
                count += .PositionErrorByteCount
            End If
            If .SendNumberOfPathPoints Then
                count += .NumberOfPathPointsByteCount
            End If
            Return count
        End With
    End Function

    Public Shared Function GetJRKerrServoStatus(ByVal IDevice As IDevice) As JRKerrServoStatus
        Return CType(IDevice.GetProperty(GetType(JRKerrServoStatus).Name), JRKerrServoStatus)
    End Function
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
