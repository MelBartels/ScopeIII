#Region "Imports"
#End Region

Public Class SiTechReadControllerData

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    ' controller returns 21 bytes; see ServoDoc.pdf, page 41-2, 2008
    Public ReturnBytes(20) As Byte

    ' put return into the following variables
    Public SecMotorPosition As Int32
    Public PriMotorPosition As Int32
    Public SecEncoderPosition As Int32
    Public PriEncoderPosition As Int32
    Public KeypadStatus As Byte
    Public XBits As Byte
    Public YBits As Byte
    Public ExtraBits As Byte
    Public Checksum As Byte
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As SiTechReadControllerData
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SiTechReadControllerData = New SiTechReadControllerData
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As SiTechReadControllerData
        Return New SiTechReadControllerData
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub DecodeByteReturn()
        SecMotorPosition = BitConverter.ToInt32(ReturnBytes, 0)
        PriMotorPosition = BitConverter.ToInt32(ReturnBytes, 4)
        SecEncoderPosition = BitConverter.ToInt32(ReturnBytes, 8)
        PriEncoderPosition = BitConverter.ToInt32(ReturnBytes, 12)
        keypadStatus = ReturnBytes(16)
        XBits = ReturnBytes(17)
        YBits = ReturnBytes(18)
        ExtraBits = ReturnBytes(19)
        Checksum = ReturnBytes(20)
    End Sub

    Public Sub EncodeByteReturn()
        Array.Copy(BitConverter.GetBytes(SecMotorPosition), 0, ReturnBytes, 0, 4)
        Array.Copy(BitConverter.GetBytes(PriMotorPosition), 0, ReturnBytes, 4, 4)
        Array.Copy(BitConverter.GetBytes(SecEncoderPosition), 0, ReturnBytes, 8, 4)
        Array.Copy(BitConverter.GetBytes(PriEncoderPosition), 0, ReturnBytes, 12, 4)
        ReturnBytes(16) = keypadStatus
        ReturnBytes(17) = XBits
        ReturnBytes(18) = YBits
        ReturnBytes(19) = ExtraBits
        Checksum = BartelsLibrary.Checksum.Calc(ReturnBytes, 0, 20)
        ReturnBytes(20) = Checksum
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
