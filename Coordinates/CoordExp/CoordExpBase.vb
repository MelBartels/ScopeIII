''' -----------------------------------------------------------------------------
''' Project	 : Coordinates
''' Class	 : CoordExpBase
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Base class for all ICoordExp.
''' Incorporates common functionality.
'''
''' Signs
''' output sign at front, ie, -00:34:56
''' can read - 00:34:56 and -00:34:56 and 00:-34:56
''' + sign always included for Dec, never included for HMS/HMSM
''' 
''' LX200 
''' DMS formatting: -12^34#   where ^ stands for the degree symbol
'''     long format -12^34:56#
''' must use leading zeroes - no blanks, ie, 02^34# is correct while _2^34# will fail
''' if no sign, then use 3 digits for degree value (ie, longitude)
''' HMS formatting: 12:34.5#
'''     long format 12:34:56#
''' must use leading zeroes - no blanks, ie, 02:34.5# is correct while _2:34.5# will fail
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[MBartels]	2/19/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class CoordExpBase
    Implements ICoordExp

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
    Public Shared LX200DegSym As Char = Microsoft.VisualBasic.Chr(223)
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Dim pCoordExpType As ISFT

    Protected pRad As Double
    Protected pPosRad As Double

    Protected pSign As String
    Protected pHrRad As Double
    Protected pDegRad As Double
    Protected pMinRad As Double
    Protected pHr As Int32
    Protected pDeg As Int32
    Protected pMin As Int32
    Protected pMinDouble As Double
    Protected pSec As Int32
    Protected pSecDouble As Double
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As CoordExpBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As CoordExpBase = New CoordExpBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    'Public Shared Function GetInstance() As CoordExpBase
    '    Return New CoordExpBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property CoordExpType() As ISFT Implements ICoordExp.CoordExpType
        Get
            Return pCoordExpType
        End Get
        Set(ByVal Value As ISFT)
            pCoordExpType = Value
        End Set
    End Property

    Public Overridable Overloads Function ToString(ByVal rad As Double) As String Implements ICoordExp.ToString
        pRad = rad
        Return pRad.ToString
    End Function

#End Region

#Region "Private and Protected Methods"
    Protected Sub setPosRadSign(ByVal rad As Double)
        If rad >= 0 Then
            pPosRad = rad
            pSign = BartelsLibrary.Constants.Plus
        Else
            pPosRad = -rad
            pSign = BartelsLibrary.Constants.Minus
        End If
    End Sub

    Protected Sub getDMDouble(ByVal rad As Double)
        setPosRadSign(rad)

        pDeg = CType(Math.Floor(pPosRad * Units.RadToDeg), Int32)
        pDegRad = pDeg * Units.DegToRad
        pMinDouble = (pPosRad - pDegRad) * Units.RadToArcmin
    End Sub

    Protected Sub getDMS(ByVal rad As Double)
        getDM(rad)
        pSec = CType((pPosRad - pDegRad - pMinRad) * Units.RadToArcsec, Int32)

        fixDMS()
    End Sub

    Protected Sub getDMSDouble(ByVal rad As Double)
        getDM(rad)
        pSecDouble = (pPosRad - pDegRad - pMinRad) * Units.RadToArcsec

        fixDMSDouble()
    End Sub

    Protected Sub getDM(ByVal rad As Double)
        setPosRadSign(rad)

        pDeg = CType(Math.Floor(pPosRad * Units.RadToDeg), Int32)
        pDegRad = pDeg * Units.DegToRad
        pMin = CType(Math.Floor((pPosRad - pDegRad) * Units.RadToArcmin), Int32)
        pMinRad = pMin * Units.ArcminToRad

        fixDMS()
    End Sub

    Protected Sub getHM(ByVal rad As Double)
        setPosRadSign(rad)

        pHr = CType(Math.Floor(pPosRad * Units.RadToHr), Int32)
        pHrRad = pHr * Units.HrToRad
        pMin = CType((pPosRad - pHrRad) * Units.RadToMin, Int32)

        fixHM()
    End Sub

    Protected Sub getHMS(ByVal rad As Double)
        setPosRadSign(rad)

        pHr = CType(Math.Floor(pPosRad * Units.RadToHr), Int32)
        pHrRad = pHr * Units.HrToRad
        pMin = CType(Math.Floor((pPosRad - pHrRad) * Units.RadToMin), Int32)
        pMinRad = pMin * Units.MinToRad
        pSec = CType((pPosRad - pHrRad - pMinRad) * Units.RadToSec, Int32)

        fixHMS()
    End Sub

    Protected Sub getHMSM(ByVal rad As Double)
        setPosRadSign(rad)

        pHr = CType(Math.Floor(pPosRad * Units.RadToHr), Int32)
        pHrRad = pHr * Units.HrToRad
        pMin = CType(Math.Floor((pPosRad - pHrRad) * Units.RadToMin), Int32)
        pMinRad = pMin * Units.MinToRad
        pSecDouble = (pPosRad - pHrRad - pMinRad) * Units.RadToSec

        fixHMSM()
    End Sub

    Protected Function DMSToString(ByVal rad As Double, ByVal delimiter As String) As String
        pRad = rad

        getDMS(rad)

        Dim sb As New Text.StringBuilder
        sb.Append(pSign)
        If pDeg >= 100 Then
            sb.Append(pDeg.ToString("000"))
        Else
            sb.Append(pDeg.ToString("00"))
        End If
        sb.Append(delimiter)
        sb.Append(pMin.ToString("00"))
        sb.Append(delimiter)
        sb.Append(pSec.ToString("00"))
        Return sb.ToString
    End Function

    Protected Function HMSToString(ByVal rad As Double, ByVal delimiter As String) As String
        pRad = rad

        getHMS(rad)

        Dim sb As New Text.StringBuilder
        If pSign.Equals(BartelsLibrary.Constants.Minus) Then
            sb.Append(pSign)
        End If
        sb.Append(pHr.ToString("00"))
        sb.Append(delimiter)
        sb.Append(pMin.ToString("00"))
        sb.Append(delimiter)
        sb.Append(pSec.ToString("00"))
        Return sb.ToString
    End Function

    Protected Function HMSMToString(ByVal rad As Double, ByVal delimiter As String) As String
        pRad = rad

        getHMSM(rad)

        Dim sb As New Text.StringBuilder
        If pSign.Equals(BartelsLibrary.Constants.Minus) Then
            sb.Append(pSign)
        End If
        sb.Append(pHr.ToString("00"))
        sb.Append(delimiter)
        sb.Append(pMin.ToString("00"))
        sb.Append(delimiter)
        sb.Append(pSecDouble.ToString("00.000"))
        Return sb.ToString
    End Function

    Private Sub fixHM()
        fixMin()
        fixHr()
    End Sub

    Private Sub fixHMSM()
        fixSecDouble()
        fixHM()
    End Sub

    Private Sub fixHMS()
        fixSec()
        fixHM()
    End Sub

    Private Sub fixDM()
        fixMin()
        fixDeg()
    End Sub

    Private Sub fixDMS()
        fixSec()
        fixDM()
    End Sub

    Private Sub fixDMSDouble()
        fixSecDouble()
        fixDM()
    End Sub

    Private Sub fixSecDouble()
        If pSecDouble > 59.999 AndAlso pSecDouble < 60.001 Then
            pSecDouble = 0
            pSec = 0
            pMin += 1
        End If
    End Sub

    Private Sub fixSec()
        If pSec.Equals(60) Then
            pSec = 0
            pMin += 1
        End If
    End Sub

    ' update both hours and degrees if necessary
    Private Sub fixMin()
        If pMin.Equals(60) Then
            pMin = 0
            pDeg += 1
            pHr += 1
        End If
    End Sub

    Private Sub fixDeg()
        If pDeg.Equals(360) Then
            pDeg = 0
        End If
    End Sub

    Private Sub fixHr()
        If pHr.Equals(24) Then
            pHr = 0
        End If
    End Sub

#End Region

End Class