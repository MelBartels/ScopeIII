#Region "Imports"
Imports System.IO
Imports System.Xml.Serialization
#End Region

Public Class SettingsFacade

#Region "Inner Classes"
    Public Class SettingsPackage
        Public Settings As New ArrayList

        Public Function FullPathFilename() As String
            Return System.Windows.Forms.Application.StartupPath & "\" & BartelsLibrary.Settings.GetInstance.SettingsFilename
        End Function
        Public Function CreateFile() As Boolean
            Dim writer As IO.StreamWriter = New IO.StreamWriter(FullPathFilename)
            writer.Close()
            Return True
        End Function
    End Class
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pSettingsPackage As SettingsPackage
    Dim pTypes() As Type
    Dim pXmlSerializer As XmlSerializer
#End Region

#Region "Constructors (Singleton Pattern)"
    Private Sub New()
        pSettingsPackage = New SettingsPackage
        ReDim pTypes(0)
        pTypes(0) = GetType(Setting)
    End Sub

    Public Shared Function GetInstance() As SettingsFacade
        Return NestedInstance.INSTANCE
    End Function

    Private Class NestedInstance
        ' explicit constructor informs compiler not to mark type as beforefieldinit
        Shared Sub New()
        End Sub
        ' friend = internal, shared = static, readonly = final
        Friend Shared ReadOnly INSTANCE As SettingsFacade = New SettingsFacade
    End Class
#End Region

#Region "Constructors"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As SettingsFacade
    '    Return New SettingsFacade
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property Settings() As ArrayList
        Get
            Return pSettingsPackage.Settings
        End Get
        Set(ByVal value As ArrayList)
            pSettingsPackage.Settings = value
        End Set
    End Property

    Public Function LoadConfig() As Boolean
        Try
            If File.Exists(pSettingsPackage.FullPathFilename) Then
                Dim streamReader As New StreamReader(pSettingsPackage.FullPathFilename)
                pSettingsPackage = CType(pXmlSerializer.Deserialize(streamReader), SettingsPackage)
                streamReader.Close()
            End If
            Return True
        Catch onf As NullReferenceException
            DebugTrace.WriteLine("Unable to deserialize " & pSettingsPackage.FullPathFilename)
        End Try
        Return False
    End Function

    Public Function SaveConfig() As Boolean
        Dim StreamWriter As New StreamWriter(pSettingsPackage.FullPathFilename)
        pXmlSerializer.Serialize(StreamWriter, pSettingsPackage)
        StreamWriter.Close()
        Return True
    End Function

    Public Sub ClearSettings()
        pSettingsPackage.Settings.Clear()
    End Sub

    Public Sub IncludeType(ByVal type As Type)
        For Each t As Type In pTypes
            If t Is type Then
                Exit Sub
            End If
        Next

        ReDim Preserve pTypes(pTypes.Length)
        pTypes(pTypes.Length - 1) = type

        pXmlSerializer = New XmlSerializer(GetType(SettingsPackage), pTypes)
    End Sub

    Public Function GetIncludedTypes() As Type()
        Return pTypes
    End Function

    Public Function GetSetting(ByVal name As String) As Setting
        If pSettingsPackage IsNot Nothing AndAlso pSettingsPackage.Settings IsNot Nothing Then
            For Each [object] As Object In pSettingsPackage.Settings
                If [object].GetType Is GetType(ScopeIII.Config.Setting) Then
                    Dim setting As Setting = CType([object], Setting)
                    If Not String.IsNullOrEmpty(setting.Name) AndAlso setting.Name.Equals(name) Then
                        Return setting
                    End If
                Else
                    DebugTrace.WriteLine("Could not cast " & [object].GetType.Name & " to Config.Setting in SettingsFacade.GetSetting()")
                End If
            Next
        End If

        ' setting could not be found, so create new one
        Dim newSetting As Setting = Setting.GetInstance
        newSetting.Name = name
        AddSetting(newSetting)
        Return newSetting
    End Function
#End Region

#Region "Private and Protected Methods"
    Private Sub AddSetting(ByRef newSetting As Setting)
        pSettingsPackage.Settings.Add(newSetting)
    End Sub
#End Region

End Class
