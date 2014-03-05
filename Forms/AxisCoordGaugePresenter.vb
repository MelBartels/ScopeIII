#Region "Imports"
#End Region

Public Class AxisCoordGaugePresenter

#Region "Inner Classes"
    Private Class Model
        Public CoordName As ISFT
        Public CoordLabelColor As Drawing.Color
        Public CoordExpType As ISFT
        Public IRenderer As IRenderer
    End Class
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
    '    Private Sub New()
    '    End Sub

    '    Public Shared Function GetInstance() As AxisCoordGaugePresenter
    '        Return NestedInstance.INSTANCE
    '    End Function

    '    Private Class NestedInstance
    '        ' explicit constructor informs compiler not to mark type as beforefieldinit
    '        Shared Sub New()
    '        End Sub
    '        ' friend = internal, shared = static, readonly = final
    '        Friend Shared ReadOnly INSTANCE As AxisCoordGaugePresenter = New AxisCoordGaugePresenter
    '    End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As AxisCoordGaugePresenter
        Return New AxisCoordGaugePresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub Build(ByRef IRendererCoordPresenter As IRendererCoordPresenter, ByVal coordName As ISFT)
        Dim model As Model = buildModel(coordName)
        With IRendererCoordPresenter
            .SetCoordinateName(model.CoordName.Description)
            .SetCoordinateLabelColor(model.CoordLabelColor)
            .CoordExpType = CType(model.CoordExpType, ISFT)
            .IRenderer = model.IRenderer
        End With
    End Sub

    Public Sub Build(ByRef IGauge2AxisCoordPresenter As IGauge2AxisCoordPresenter, ByVal priCoordName As ISFT, ByVal secCoordName As ISFT)
        Dim priModel As Model = buildModel(priCoordName)
        Dim secModel As Model = buildModel(secCoordName)
        With IGauge2AxisCoordPresenter
            .SetAxisNames(priModel.CoordName.Description, secModel.CoordName.Description)
            .SetCoordinateLabelColors(priModel.CoordLabelColor, secModel.CoordLabelColor)
            .SetExpCoordTypes(priModel.CoordExpType, secModel.CoordExpType)
            .SetRenderers(priModel.IRenderer, secModel.IRenderer)
        End With
    End Sub

    Public Sub Build(ByRef IGauge3AxisCoordPresenter As IGauge3AxisCoordPresenter, ByVal priCoordName As ISFT, ByVal secCoordName As ISFT, ByVal tierCoordName As ISFT)
        Dim priModel As Model = buildModel(priCoordName)
        Dim secModel As Model = buildModel(secCoordName)
        Dim tierModel As Model = buildModel(tierCoordName)
        With IGauge3AxisCoordPresenter
            .SetAxisNames(priModel.CoordName.Description, secModel.CoordName.Description, tierModel.CoordName.Description)
            .SetCoordinateLabelColors(priModel.CoordLabelColor, secModel.CoordLabelColor, tierModel.CoordLabelColor)
            .SetExpCoordTypes(priModel.CoordExpType, secModel.CoordExpType, tierModel.CoordExpType)
            .SetRenderers(priModel.IRenderer, secModel.IRenderer, tierModel.IRenderer)
        End With
    End Sub
#End Region

#Region "Private and Protected Methods"
    Private Function buildModel(ByVal coordName As ISFT) As Model
        Dim model As New Model

        model.CoordName = coordName

        Select Case coordName.Key
            Case Coordinates.CoordName.PriAxis.Key
                model.CoordLabelColor = CType(AxisColors.Primary.Tag, Drawing.Color)
                model.CoordExpType = CoordExpType.FormattedDegree
                model.IRenderer = DegreeCircGaugeRenderer.GetInstance
            Case Coordinates.CoordName.SecAxis.Key
                model.CoordLabelColor = CType(AxisColors.Secondary.Tag, Drawing.Color)
                model.CoordExpType = CoordExpType.FormattedDegree
                model.IRenderer = DegreeCircGaugeRenderer.GetInstance
            Case Coordinates.CoordName.TierAxis.Key
                model.CoordLabelColor = CType(AxisColors.Tiertiary.Tag, Drawing.Color)
                model.CoordExpType = CoordExpType.FormattedDegree
                model.IRenderer = DegreeCircGaugeRenderer.GetInstance

            Case Coordinates.CoordName.RA.Key
                model.CoordLabelColor = CType(AxisColors.Primary.Tag, Drawing.Color)
                model.CoordExpType = CoordExpType.FormattedHMS
                model.IRenderer = HourCircGaugeRenderer.GetInstance
            Case Coordinates.CoordName.Dec.Key
                model.CoordLabelColor = CType(AxisColors.Secondary.Tag, Drawing.Color)
                model.CoordExpType = CoordExpType.FormattedDMS
                model.IRenderer = DeclinationCircGaugeRenderer.GetInstance
            Case Coordinates.CoordName.SidT.Key
                model.CoordLabelColor = CType(AxisColors.Tiertiary.Tag, Drawing.Color)
                model.CoordExpType = CoordExpType.FormattedHMS
                model.IRenderer = HourCircGaugeRenderer.GetInstance

            Case Coordinates.CoordName.Az.Key
                model.CoordLabelColor = CType(AxisColors.Primary.Tag, Drawing.Color)
                model.CoordExpType = CoordExpType.FormattedDegree
                model.IRenderer = DegreeCircGaugeRenderer.GetInstance
            Case Coordinates.CoordName.Alt.Key
                model.CoordLabelColor = CType(AxisColors.Secondary.Tag, Drawing.Color)
                model.CoordExpType = CoordExpType.FormattedDegree
                model.IRenderer = DegreeNegCircGaugeRenderer.GetInstance
            Case Coordinates.CoordName.FieldRotation.Key
                model.CoordLabelColor = CType(AxisColors.Tiertiary.Tag, Drawing.Color)
                model.CoordExpType = CoordExpType.FormattedDegree
                model.IRenderer = DegreeCircGaugeRenderer.GetInstance

            Case Coordinates.CoordName.Longitude.Key
                model.CoordLabelColor = CType(AxisColors.Primary.Tag, Drawing.Color)
                model.CoordExpType = CoordExpType.FormattedDMS
                model.IRenderer = DegreeCircGaugeRenderer.GetInstance
            Case Coordinates.CoordName.Latitude.Key
                model.CoordLabelColor = CType(AxisColors.Secondary.Tag, Drawing.Color)
                model.CoordExpType = CoordExpType.FormattedDMS
                model.IRenderer = DeclinationCircGaugeRenderer.GetInstance

            Case Coordinates.CoordName.TiltPri.Key
                model.CoordLabelColor = CType(AxisColors.Primary.Tag, Drawing.Color)
                model.CoordExpType = CoordExpType.WholeNumNegDegree
                model.IRenderer = DegreeNegSliderRenderer.GetInstance
            Case Coordinates.CoordName.TiltSec.Key
                model.CoordLabelColor = CType(AxisColors.Secondary.Tag, Drawing.Color)
                model.CoordExpType = CoordExpType.WholeNumNegDegree
                model.IRenderer = DegreeNegSliderRenderer.GetInstance
            Case Coordinates.CoordName.TiltTier.Key
                model.CoordLabelColor = CType(AxisColors.Tiertiary.Tag, Drawing.Color)
                model.CoordExpType = CoordExpType.WholeNumNegDegree
                model.IRenderer = DegreeNegSliderRenderer.GetInstance

            Case Coordinates.CoordName.Z1.Key
                model.CoordLabelColor = CType(AxisColors.Primary.Tag, Drawing.Color)
                model.CoordExpType = CoordExpType.FormattedDegree
                model.IRenderer = ArcminSliderRenderer.GetInstance
            Case Coordinates.CoordName.Z2.Key
                model.CoordLabelColor = CType(AxisColors.Secondary.Tag, Drawing.Color)
                model.CoordExpType = CoordExpType.FormattedDegree
                model.IRenderer = ArcminSliderRenderer.GetInstance
            Case Coordinates.CoordName.Z3.Key
                model.CoordLabelColor = CType(AxisColors.Tiertiary.Tag, Drawing.Color)
                model.CoordExpType = CoordExpType.FormattedDegree
                model.IRenderer = ArcminSliderRenderer.GetInstance

            Case Else
                model.CoordLabelColor = CType(AxisColors.Primary.Tag, Drawing.Color)
                model.CoordExpType = CoordExpType.FormattedDegree
                model.IRenderer = DegreeCircGaugeRenderer.GetInstance
        End Select


        Return model
    End Function
#End Region

End Class
