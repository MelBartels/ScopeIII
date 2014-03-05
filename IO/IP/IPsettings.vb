#Region "Imports"
Imports System.ComponentModel

#End Region

''' -----------------------------------------------------------------------------
''' Project	 : IO
''' Class	 : IO.IPsettings
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Class holding an IP address and port number.
''' Cannot use IPEndPoint and IPAddress directly as they do not have default constructors,
''' causing reflection to fail during the configuration manager's save.
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[mbartels]	8/18/2005	Created
''' </history>
''' -----------------------------------------------------------------------------

<DefaultPropertyAttribute(IPsettings.ClassDefaultProp), _
Description(IPsettings.ClassDescAttr)> _
Public Class IPsettings
    Inherits SettingsBase
    Implements ISettings, ISettingsToPropGridAdapter, ICloneable

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public Const ClassDefaultProp As String = "Port"
    Public Const ClassDescAttr As String = "IP Settings"
    Public Const IPAddressDesc As String = "IP Address."
    Public Const PortDesc As String = "IP Port Number."
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pAddress As String
    Private pPort As Integer
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As IPsettings
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As IPsettings = New IPsettings
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As IPsettings
        Return New IPsettings
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    <CategoryAttribute(ClassDescAttr), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DesignOnly(False), _
    DefaultValueAttribute(BartelsLibrary.Constants.DefaultIPAddress), _
    DescriptionAttribute(IPAddressDesc)> _
    Public Property Address() As String
        Get
            Return pAddress
        End Get
        Set(ByVal Value As String)
            ' if DefaultValueAttribute() is included and property value is same as the default, 
            ' the property will not be serialized; 
            ' therefore if value is Nothing then set it to default
            If Value Is Nothing Then
                Value = BartelsLibrary.Constants.DefaultIPAddress
            End If
            Try
                Net.IPAddress.Parse(Value)
                pAddress = Value
            Catch fex As FormatException
                ExceptionService.Notify(BartelsLibrary.Constants.BadIPAddressFormat)
            End Try
        End Set
    End Property

    <CategoryAttribute(ClassDescAttr), _
    Browsable(True), _
    [ReadOnly](False), _
    BindableAttribute(False), _
    DesignOnly(False), _
    DefaultValueAttribute(BartelsLibrary.Constants.DefaultIPPort), _
    DescriptionAttribute(PortDesc)> _
    Public Property Port() As Int32
        Get
            Return pPort
        End Get
        Set(ByVal Value As Int32)
            If Value.Equals(0) Then
                Value = BartelsLibrary.Constants.DefaultIPPort
            End If
            pPort = Value
        End Set
    End Property

    Public Overrides Sub SetToDefaults()
        With BartelsLibrary.Settings.GetInstance
            pAddress = .DefaultIPAddress
            pPort = .DefaultIPPort
        End With
    End Sub

    Public Overrides Sub CopyPropertiesTo(ByRef ISettings As BartelsLibrary.ISettings)
        Dim IPSettings As IPsettings = CType(ISettings, IPsettings)

        IPSettings.Name = Name
        IPSettings.Address = pAddress
        IPSettings.Port = pPort
    End Sub

    Public Overrides Function Clone() As Object Implements ICloneable.Clone
        Dim IPsettings As ISettings = IO.IPsettings.GetInstance
        CopyPropertiesTo(IPsettings)
        Return IPsettings
    End Function

    Public Overrides Function PropertiesSet() As Boolean
        Return Not String.IsNullOrEmpty(pAddress)
    End Function

    Public Function PropGridSelectedObject() As Object Implements ISettingsToPropGridAdapter.PropGridSelectedObject
        Return Me
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
