#Region "Imports"
Imports System.IO
#End Region

Public Class Z12CompareTest

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public ReadOnly ALTITUDEpSTEP As Double
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pZ12TestArray As ArrayList
    Private pInitStateTemplate As InitStateTemplate
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As Z12CompareTest
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As Z12CompareTest = New Z12CompareTest
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        ALTITUDEpSTEP = 1 * Units.DegToRad
        pInitStateTemplate = InitStateFactory.GetInstance.Build(CType(CoordXformType.ConvertMatrix, ISFT), CType(InitStateType.Altazimuth, ISFT))
    End Sub

    Public Shared Function GetInstance() As Z12CompareTest
        Return New Z12CompareTest
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public ReadOnly Property Z12TestArray() As ArrayList
        Get
            Return pZ12TestArray
        End Get
    End Property

    Public Sub GenerateValues(ByVal z1Rad As Double, ByVal z2Rad As Double, ByVal latitudeRad As Double, ByVal azimuthRad As Double)
        Dim z12test As Z12Test

        pZ12TestArray = New ArrayList

        pInitStateTemplate.ICoordXform.Site.Latitude.Rad = latitudeRad

        CType(pInitStateTemplate.ICoordXform, ConvertMatrix).FabErrors.SetFabErrorsDeg(0, 0, 0)
        pInitStateTemplate.IInit.Init()

        pInitStateTemplate.ICoordXform.Position.SidT.Rad = 0
        pInitStateTemplate.ICoordXform.Position.Az.Rad = azimuthRad

        ' set initial az w/ no Z12: getEquat that will form the basis for calc'ing a new az based on z12

        For pInitStateTemplate.ICoordXform.Position.Alt.Rad = -(Units.QtrRev - ALTITUDEpSTEP) To (Units.QtrRev - ALTITUDEpSTEP) Step ALTITUDEpSTEP
            pInitStateTemplate.ICoordXform.GetEquat()

            z12test = Coordinates.Z12Test.GetInstance
            z12test.Position.CopyFrom(pInitStateTemplate.ICoordXform.Position)

            pZ12TestArray.Add(z12test)
        Next

        ' set new az based on z12 values 

        CType(pInitStateTemplate.ICoordXform, ConvertMatrix).FabErrors.SetFabErrorsDeg(z1Rad * Units.RadToDeg, z2Rad * Units.RadToDeg, 0)
        pInitStateTemplate.IInit.Init()

        For Each z12test In pZ12TestArray
            pInitStateTemplate.ICoordXform.Position.CopyFrom(z12test.Position)
            pInitStateTemplate.ICoordXform.GetAltaz()
            z12test.AzError.Rad = z12test.Position.Az.Rad - pInitStateTemplate.ICoordXform.Position.Az.Rad
        Next
    End Sub

    Public Function WriteCVSToFile(ByVal filename As String, ByVal delimiter As String) As Boolean
        Dim writer As StreamWriter = Nothing
        Try
            writer = New StreamWriter(filename)

            For Each z12test As Z12Test In pZ12TestArray
                Dim coordExpType As ISFT = CType(Coordinates.CoordExpType.Degree, ISFT)
                Dim sb As New Text.StringBuilder

                sb.Append("Z1Deg")
                sb.Append(delimiter)
                sb.Append(CType(pInitStateTemplate.ICoordXform, ConvertMatrix).FabErrors.Z1.ToString(coordExpType))
                sb.Append(delimiter)

                sb.Append("Z2Deg")
                sb.Append(delimiter)
                sb.Append(CType(pInitStateTemplate.ICoordXform, ConvertMatrix).FabErrors.Z2.ToString(coordExpType))
                sb.Append(delimiter)

                sb.Append("AltDeg")
                sb.Append(delimiter)
                sb.Append(z12test.Position.Alt.ToString(coordExpType))
                sb.Append(delimiter)

                sb.Append("AzErrorDeg")
                sb.Append(delimiter)
                sb.Append(z12test.AzError.ToString(coordExpType))

                writer.WriteLine(sb.ToString)
            Next

        Catch ex As Exception
            ExceptionService.Notify(ex)
        Finally
            writer.Close()
        End Try

        Return True
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
