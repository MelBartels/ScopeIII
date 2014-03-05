#Region "Imports"
Imports System.IO
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
#End Region

Public Class ScopePilotObjectToRender

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public PriRad As Double
    Public SecRad As Double
    Public TiltPriRad As Double
    Public TiltSecRad As Double
    Public TiltTierRad As Double
    Public AzDir As Rotation
    Public AzFormat As CoordExpType
    Public PrimaryColor As Drawing.Color
    Public SecondaryColor As Drawing.Color
    Public DisplayInits As Boolean
    Public Inits(ConvertMatrix.NumberOfInits - 1) As AZdouble
    Public Hemisphere As Hemisphere
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ScopePilotObjectToRender
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ScopePilotObjectToRender = New ScopePilotObjectToRender
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        For init As Int32 = 0 To Inits.Length - 1
            Inits(init) = AZdouble.GetInstance
        Next
        Hemisphere = Hemisphere.GetInstance
    End Sub

    Public Shared Function GetInstance() As ScopePilotObjectToRender
        Return New ScopePilotObjectToRender
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
#End Region

End Class

