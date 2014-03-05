#Region "Imports"
#End Region

Public Class DeviceToHierarchicalAdapter

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pIHierarchical As IHierarchical
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DeviceToHierarchicalAdapter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DeviceToHierarchicalAdapter = New DeviceToHierarchicalAdapter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As DeviceToHierarchicalAdapter
        Return New DeviceToHierarchicalAdapter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function RegisterComponent(ByRef component As Object) As Boolean
        If IHierarchicalFactory(component) Then
            pIHierarchical.RegisterComponent(component)
            Return True
        End If
        Return False
    End Function

    Public Sub Adapt(ByRef IDevice As IDevice)
        adaptIDevice(pIHierarchical.AddChild(Nothing, "Device"), IDevice)
    End Sub
#End Region

#Region "Private and Protected Methods"
    Private Function IHierarchicalFactory(ByRef component As Object) As Boolean
        If component.GetType.Name.Equals("StringBuilder") Then
            pIHierarchical = StringHierarchicalAdapter.GetInstance
            Return True
        End If
        If component.GetType.Name.Equals("TextBox") Then
            pIHierarchical = TextBoxHierarchicalAdapter.GetInstance
            Return True
        End If
        If component.GetType.Name.Equals("TreeView") Then
            pIHierarchical = TreeViewHierarchicalAdapter.GetInstance
            Return True
        End If
        'ExceptionService.Notify("Unknown component type (" _
        '    & component.GetType.Name _
        '    & ").  DeviceToHierarchicalAdapter.IHierarchicalFactory()")
        Return False
    End Function

    Private Sub adaptIDevice(ByRef parent As Object, ByRef IDevice As IDevice)
        If IDevice.Properties IsNot Nothing Then
            For Each prop As IDevProp In IDevice.Properties
                Dim sb As New Text.StringBuilder

                If CType(prop, Object).GetType.Name.Equals(GetType(ValueObject).Name) Then
                    sb.Append(CType(prop, ValueObject).Properties)
                ElseIf CType(prop, Object).GetType.Name.Equals(GetType(EncoderValue).Name) Then
                    sb.Append(CType(prop, EncoderValue).Properties)
                Else
                    sb.Append(CObj(prop).GetType.Name.Replace("DevProp", String.Empty))
                    sb.Append(": ")
                    sb.Append(prop.Value)
                End If
                'Debug.WriteLine(sb.ToString)

                pIHierarchical.AddChild(parent, sb.ToString)
                ' adapt device property observers
                If prop.ObservableImp IsNot Nothing Then
                    adaptObservableImp("Observer(s): ", parent, prop.ObservableImp)
                End If
            Next

            Dim cmdSet As ISFTFacade = IDevice.GetCmdSet
            If cmdSet IsNot Nothing Then
                Dim sb As New Text.StringBuilder
                sb.Append("Device commands: ")
                Dim eCmd As IEnumerator = cmdSet.FirstItem.Enumerator
                While eCmd.MoveNext
                    Dim cmd As ISFT = CType(eCmd.Current, ISFT)
                    sb.Append(cmd.Name)
                    sb.Append("(")
                    sb.Append(cmd.Description)
                    sb.Append("); ")
                End While
                pIHierarchical.AddChild(parent, sb.ToString)
            End If
        End If

        Dim deviceTemplate As DeviceTemplate = IDevice.GetDeviceTemplate
        If deviceTemplate IsNot Nothing Then
            adaptObservableImp("Device observer(s): ", parent, deviceTemplate.DeviceObserver)
            adaptObservableImp("Status observer(s): ", parent, deviceTemplate.StatusObserver)
        End If

        ' recursively go through sub devices
        If IDevice.IDevices IsNot Nothing Then
            For Each device As IDevice In IDevice.IDevices
                adaptIDevice(pIHierarchical.AddChild(parent, "Device"), device)
            Next
        End If
    End Sub

    Private Sub adaptObservableImp(ByVal heading As String, ByRef parent As Object, ByVal observableImp As ObservableImp)
        If observableImp IsNot Nothing AndAlso observableImp.Observers IsNot Nothing AndAlso observableImp.Observers.Count > 0 Then
            Dim sb As New Text.StringBuilder
            sb.Append(heading)
            For Each observer As IObserver In observableImp.Observers
                If CType(observer, Object).GetType Is GetType(ObserverWithID) Then
                    Dim observerWithID As ObserverWithID = CType(observer, ObserverWithID)
                    sb.Append(observerWithID.GetType.Name)
                    sb.Append(": observing=")
                    sb.Append(observerWithID.ObservingID)
                    sb.Append(", observed=")
                    sb.Append(observerWithID.ObservedID)
                Else
                    sb.Append(CObj(observer).GetType.Name)
                End If
                sb.Append(", ")
            Next
            pIHierarchical.AddChild(parent, sb.ToString)
            'Debug.WriteLine(sb.ToString)
        End If
    End Sub
#End Region

End Class
