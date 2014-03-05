#Region "imports"
#End Region

Public Class Z12CompareMultiXYDataAdapter

#Region "Inner Classes"
#End Region

#Region "Constant Members"
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

    'Public Shared Function GetInstance() As Z12CompareMultiXYDataAdapter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As Z12CompareMultiXYDataAdapter = New Z12CompareMultiXYDataAdapter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As Z12CompareMultiXYDataAdapter
        Return New Z12CompareMultiXYDataAdapter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub GetZ12(ByVal z1Rad As Double, ByVal z2Rad As Double, ByVal latitudeRad As Double, ByVal azimuthRad As Double, ByRef multiXYData As MultiXYData)

        Dim Z12CompareTest As Z12CompareTest = Coordinates.Z12CompareTest.GetInstance
        Z12CompareTest.GenerateValues(z1Rad, 0, latitudeRad, azimuthRad)

        Dim altZ1(Z12CompareTest.Z12TestArray.Count - 1) As Double
        Dim azZ1(Z12CompareTest.Z12TestArray.Count - 1) As Double
        Dim ix As Int32 = 0
        For Each Z12Test As Z12Test In Z12CompareTest.Z12TestArray
            altZ1(ix) = CType(Z12Test.Position.Alt.Rad * Units.RadToDeg, Int32)
            azZ1(ix) = Z12Test.AzError.Rad * Units.RadToArcmin
            ix += 1
        Next

        Z12CompareTest.GenerateValues(0, z2Rad, latitudeRad, azimuthRad)

        Dim altZ2(Z12CompareTest.Z12TestArray.Count - 1) As Double
        Dim azZ2(Z12CompareTest.Z12TestArray.Count - 1) As Double
        ix = 0
        For Each Z12Test As Z12Test In Z12CompareTest.Z12TestArray
            altZ2(ix) = CType(Z12Test.Position.Alt.Rad * Units.RadToDeg, Int32)
            azZ2(ix) = Z12Test.AzError.Rad * Units.RadToArcmin
            ix += 1
        Next

        Dim altdata(1)() As Double
        Dim azdata(1)() As Double
        altdata(0) = altZ1
        azdata(0) = azZ1
        altdata(1) = altZ2
        azdata(1) = azZ2

        ' x data (horizontal) is az error, y data (vertical) is altitude
        multiXYData.XData = azdata
        multiXYData.YData = altdata
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
