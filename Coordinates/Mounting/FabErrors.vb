''' -----------------------------------------------------------------------------
''' Project	 : Mounting
''' Class	 : Mounting.FabErrors
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' fabrication errors (in radians):
''' Z1: offset of elevation to perpendicular of horizon, ie, one side of rocker box higher than the other
''' Z2: optical axis pointing error in same plane, ie, tube horiz.: optical axis error left to right (horiz)
''' Z3: correction to zero setting of elevation, ie, vertical offset error (same as altitude offset)
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[MBartels]	2/26/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class FabErrors

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Z1 As Coordinate
    Public Z2 As Coordinate
    Public Z3 As Coordinate

    ' if Z1 or Z2 is non-zero
    Public Z12NonZero As Boolean
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As FabErrors
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As FabErrors = New FabErrors
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        Z1 = Coordinate.GetInstance
        Z2 = Coordinate.GetInstance
        Z3 = Coordinate.GetInstance
    End Sub

    Public Shared Function GetInstance() As FabErrors
        Return New FabErrors
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub SetFabErrorsDeg(ByVal z1Deg As Double, ByVal z2Deg As Double, ByVal z3Deg As Double)
        Z1.Rad = z1Deg * Units.DegToRad
        Z2.Rad = z2Deg * Units.DegToRad
        Z3.Rad = z3Deg * Units.DegToRad
        setZ12NonZero()
    End Sub

    Public Sub CopyFrom(ByVal fabErrors As FabErrors)
        Me.Z1.Rad = fabErrors.Z1.Rad
        Me.Z2.Rad = fabErrors.Z2.Rad
        Me.Z3.Rad = fabErrors.Z3.Rad
        setZ12NonZero()
    End Sub
#End Region

#Region "Private and Protected Methods"
    Private Sub setZ12NonZero()
        If Not Z1.Rad.Equals(0.0) OrElse Not Z2.Rad.Equals(0.0) Then
            Z12NonZero = True
        Else
            Z12NonZero = False
        End If
    End Sub
#End Region

End Class