#Region "Imports"
Imports System.Collections.Generic
#End Region

Public Class DeviceCmdsFacade

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected pCmdSet As ISFTFacade
    Protected pCmdList As List(Of ISFT)
    Protected pDeviceCmdAndReplyTemplateDictionary As Dictionary(Of ISFT, DeviceCmdAndReplyTemplate)
    Protected pDeviceCmdAndReplyTemplateList As List(Of DeviceCmdAndReplyTemplate)
    Protected pIDeviceCmdMsgDiscriminator As IDeviceCmdMsgDiscriminator
    Protected pUpdateDeviceCmdsObserver As UpdateDeviceCmdsObserver
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DeviceCmdsFacade
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '     explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '     friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DeviceCmdsFacade = New DeviceCmdsFacade
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        UpdateDeviceCmdsObserver = DevicesDependencyInjector.GetInstance.UpdateDeviceCmdsObserverFactory
    End Sub

    Public Shared Function GetInstance() As DeviceCmdsFacade
        Return New DeviceCmdsFacade
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property CmdSet() As ISFTFacade
        Get
            Return pCmdSet
        End Get
        Set(ByVal value As ISFTFacade)
            If CmdSet Is Nothing OrElse Not value.GetType.Equals(CmdSet.GetType) Then
                pCmdSet = value
                build()
            End If
        End Set
    End Property

    Public Property CmdList() As List(Of ISFT)
        Get
            Return pCmdList
        End Get
        Set(ByVal value As List(Of ISFT))
            pCmdList = value
        End Set
    End Property

    Public Property DeviceCmdAndReplyTemplateList() As List(Of DeviceCmdAndReplyTemplate)
        Get
            Return pDeviceCmdAndReplyTemplateList
        End Get
        Set(ByVal value As List(Of DeviceCmdAndReplyTemplate))
            pDeviceCmdAndReplyTemplateList = value
        End Set
    End Property

    Public Property DeviceCmdAndReplyTemplateDictionary() As Dictionary(Of ISFT, DeviceCmdAndReplyTemplate)
        Get
            Return pDeviceCmdAndReplyTemplateDictionary
        End Get
        Set(ByVal value As Dictionary(Of ISFT, DeviceCmdAndReplyTemplate))
            pDeviceCmdAndReplyTemplateDictionary = value
        End Set
    End Property

    Public ReadOnly Property IDeviceCmdMsgDiscriminator() As IDeviceCmdMsgDiscriminator
        Get
            Return pIDeviceCmdMsgDiscriminator
        End Get
    End Property

    Public Property UpdateDeviceCmdsObserver() As UpdateDeviceCmdsObserver
        Get
            Return pUpdateDeviceCmdsObserver
        End Get
        Set(ByVal value As UpdateDeviceCmdsObserver)
            pUpdateDeviceCmdsObserver = value
        End Set
    End Property

    Public Function GetDeviceCmdAndReplyTemplate(ByRef cmd As BartelsLibrary.ISFT) As DeviceCmdAndReplyTemplate
        Return DeviceCmdAndReplyTemplateDictionary(cmd)
    End Function

    Public Function GetIDeviceCmd(ByRef cmd As BartelsLibrary.ISFT) As IDeviceCmd
        Return DeviceCmdAndReplyTemplateDictionary(cmd).IDeviceCmd
    End Function

    Public Function GetIDeviceReplyCmd(ByRef cmd As BartelsLibrary.ISFT) As IDeviceReplyCmd
        Return DeviceCmdAndReplyTemplateDictionary(cmd).IDeviceReplyCmd
    End Function

    Public Function GetCmdReplyParms(ByRef cmd As BartelsLibrary.ISFT) As CmdReplyParms
        Return DeviceCmdAndReplyTemplateDictionary(cmd).IDeviceCmd.CmdReplyParms
    End Function

    Public Function GetCmd(ByRef deviceCmdAndReplyTemplate As DeviceCmdAndReplyTemplate) As ISFT
        Dim ix As Int32 = DeviceCmdAndReplyTemplateList.FindIndex(AddressOf New FuncDelegate(Of DeviceCmdAndReplyTemplate, DeviceCmdAndReplyTemplate) _
                                                                  (AddressOf matchDeviceCmdAndReplyTemplate, deviceCmdAndReplyTemplate).CallDelegate)
        Return CmdList(ix)
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Overridable Sub build()
        buildCmdList(CmdSet)
        buildIDeviceCmdAndReplyTemplateDictionary(CmdList)
        buildDeviceCmdAndReplyTemplateList(DeviceCmdAndReplyTemplateDictionary)
        pIDeviceCmdMsgDiscriminator = ICmdMsgDiscriminatorFactory(CmdSet)
    End Sub

    Protected Sub buildCmdList(ByRef cmds As ISFTFacade)
        pCmdList = cmds.FirstItem.GetList
        pCmdList.Sort(CmdListComparer.GetInstance)
    End Sub

    Protected Sub buildIDeviceCmdAndReplyTemplateDictionary(ByVal cmdList As List(Of ISFT))
        DeviceCmdAndReplyTemplateDictionary = New Dictionary(Of ISFT, DeviceCmdAndReplyTemplate)
        cmdList.ForEach(AddressOf New SubDelegate(Of ISFT, Dictionary(Of ISFT, DeviceCmdAndReplyTemplate)) _
                           (AddressOf addDeviceCmdAndReplyTemplate, DeviceCmdAndReplyTemplateDictionary).CallDelegate)
    End Sub

    Protected Sub addDeviceCmdAndReplyTemplate(ByVal cmd As ISFT, ByVal deviceCmdAndReplyTemplateDictionary As Dictionary(Of ISFT, DeviceCmdAndReplyTemplate))
        Dim defaultDeviceCmdAndReplyTemplate As DeviceCmdAndReplyTemplate = CType(cmd.Tag, Devices.DeviceCmdAndReplyTemplate)
        Dim deviceCmdAndReplyTemplate As DeviceCmdAndReplyTemplate = deviceCmdAndReplyTemplate.GetInstance
        deviceCmdAndReplyTemplate.IDeviceCmd = CType(defaultDeviceCmdAndReplyTemplate.IDeviceCmd.Clone, IDeviceCmd)
        deviceCmdAndReplyTemplate.IDeviceReplyCmd = CType(defaultDeviceCmdAndReplyTemplate.IDeviceReplyCmd.Clone, IDeviceReplyCmd)
        ' shared object
        deviceCmdAndReplyTemplate.IDeviceReplyCmd.CmdReplyParms = deviceCmdAndReplyTemplate.IDeviceCmd.CmdReplyParms
        deviceCmdAndReplyTemplateDictionary.Add(cmd, deviceCmdAndReplyTemplate)
    End Sub

    Protected Sub buildDeviceCmdAndReplyTemplateList(ByVal deviceCmdAndReplyTemplateDictionary As Dictionary(Of ISFT, DeviceCmdAndReplyTemplate))
        DeviceCmdAndReplyTemplateList = New List(Of DeviceCmdAndReplyTemplate)
        Dim eDeviceCmdAndReplyTemplate As IEnumerator(Of DeviceCmdAndReplyTemplate) = deviceCmdAndReplyTemplateDictionary.Values.GetEnumerator
        While eDeviceCmdAndReplyTemplate.MoveNext
            DeviceCmdAndReplyTemplateList.Add(eDeviceCmdAndReplyTemplate.Current)
        End While
    End Sub

    Protected Function ICmdMsgDiscriminatorFactory(ByRef cmds As ISFTFacade) As IDeviceCmdMsgDiscriminator
        If cmds.GetType Is GetType(JRKerrCmds) Then
            Return JRKerrDeviceCmdMsgDiscriminator.GetInstance
        Else
            Return DeviceCmdMsgDiscriminator.GetInstance
        End If
    End Function

    Protected Function matchDeviceCmdAndReplyTemplate(ByVal findDeviceCmdAndReplyTemplate As DeviceCmdAndReplyTemplate, ByVal deviceCmdAndReplyTemplate As DeviceCmdAndReplyTemplate) As Boolean
        Return findDeviceCmdAndReplyTemplate Is deviceCmdAndReplyTemplate
    End Function
#End Region

End Class
