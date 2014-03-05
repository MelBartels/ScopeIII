Public Class Constants

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public Const AllSources As String = "*All Sources*"
    Public Const ObjectNameFilter As String = "eg, M104"

    Public Const DmsString As String = "+00d 00m 00s"
    Public Const HmsString As String = "00h 00m 00s"

    Public Const CoordFormatMsg As String = _
        "formats:" _
        & vbCrLf _
        & vbCrLf _
        & "00h 00m 00s" _
        & vbCrLf _
        & "00:00:00 h" _
        & vbCrLf _
        & "00.000 h" _
        & vbCrLf _
        & vbCrLf _
        & "+00°0'00.0""" _
        & vbCrLf _
        & vbCrLf _
        & "+00d 00m 00s" _
        & vbCrLf _
        & "+00:00:00 d" _
        & vbCrLf _
        & "+00.000 d" _
        & vbCrLf _
        & vbCrLf _
        & "+00.000 radians" _
        & vbCrLf _
        & vbCrLf _
        & "+00 00 00 (defaults to degrees)" _
        & vbCrLf _
        & "+00.000 (defaults to degrees)"

    Public Const ConvertDataFilePostpend As String = ".out.dat"

    Public Const Z123ToolTip As String = _
        "Z1: Offset of elevation to perpendicular of horizon, ie, one side of rocker box higher than the other." _
        & vbNewLine _
        & "Z2: Optical axis pointing error in same plane, ie, tube horiz.: optical axis error left to right (horiz)." _
        & vbNewLine _
        & "Z3: Correction to zero setting of elevation, ie, vertical offset error (same as altitude offset)."

    Public Const GaugeToolTip As String = "Click to point or drag pointer with mouse."

    Public Const PositionArrayFilenamePostpend As String = "pPositionArray.soap.xml"

    Public Const DevicesSettings As String = "DevicesSettings"

    Public Const Devices As String = " Devices"
    Public Const TestEncoder As String = " TestEncoder"

    Public Const GraphicsToolTipZ12 As String = "Error = (position calculated with Z12) minus (position with no Z12)"

    Public Const ByteCr As Byte = Asc(vbCr)
    ' create a Byte that is the value of VbCrLf (which is two chars)
    Public Const ByteCrLf As Byte = Asc(vbCrLf)

    Public Const DeviceDefaultReplyTimeMilliseconds As Int32 = 500
    Public Const DeviceCmdFinished As String = "Device command finished"

#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As eString
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As eString = New eString
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As Constants
        Return New Constants
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
#End Region

End Class