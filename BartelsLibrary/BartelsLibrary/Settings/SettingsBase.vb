#Region "Imports"
#End Region
Imports System.ComponentModel

Public MustInherit Class SettingsBase
    Implements ISettings

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected pName As String
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As SettingsBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SettingsBase = New SettingsBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    'Public Shared Function GetInstance() As SettingsBase
    '    Return New SettingsBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    <Browsable(False)> _
    Public Property Name() As String Implements ISettings.Name
        Get
            Return pName
        End Get
        Set(ByVal value As String)
            pName = value
        End Set
    End Property

    Public MustOverride Sub SetToDefaults() Implements ISettings.SetToDefaults

    Public MustOverride Sub CopyPropertiesTo(ByRef ISettings As ISettings) Implements ISettings.CopyPropertiesTo

    Public MustOverride Function Clone() As Object Implements ISettings.Clone

    Public MustOverride Function PropertiesSet() As Boolean Implements ISettings.PropertiesSet

#End Region

#Region "Private and Protected Methods"
#End Region

End Class
