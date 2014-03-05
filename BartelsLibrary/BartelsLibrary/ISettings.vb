Public Interface ISettings
    Property Name() As String
    Sub SetToDefaults()
    Sub CopyPropertiesTo(ByRef ISettings As ISettings)
    Function Clone() As Object
    Function PropertiesSet() As Boolean
End Interface
