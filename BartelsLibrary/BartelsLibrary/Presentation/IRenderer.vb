Public Interface IRenderer
    Property ToolTip() As String
    Property WidthToHeightRatio() As Double
    Property ObjectToRender() As Object
    Function Render(ByRef g As Drawing.Graphics, ByVal width As Int32, ByVal height As Int32) As Drawing.Graphics
End Interface