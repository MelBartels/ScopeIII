#Region "Imports"
Imports System.ComponentModel
#End Region

Public Class JRKerrServoStatusToPropGridAdapter
    Inherits DevPropToPropGridAdapterBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public Const ClassDescAttr As String = "JRKerr Servo Module Status Settings"
    Public Const ModuleDescAttr As String = "Module Settings"
    Public Const StatusDescAttr As String = "Settings Values"
    Public Const ReportedDescAttr As String = "Reported Parms"

    Public Const ModuleTypeName As String = "Module Type"
    Public Const ModuleTypeDesc As String = "The type of the JRKerr controller."
    Public Const ModuleVersionName As String = "Module Version"
    Public Const ModuleVersionDesc As String = "The version of the module."

    Public Const GroupAddressName As String = "Group Address"
    Public Const GroupAddressDesc As String = "The module's group address."
    Public Const GroupLeaderName As String = "Group Leader"
    Public Const GroupLeaderDesc As String = "The module is the group leader."

    Public Const StatusItemsName As String = "Status Items"
    Public Const StatusItemsDesc As String = "Included status items."
    Public Const StatusItemsBitsName As String = "Status Items By Bits"
    Public Const StatusItemsBitsDesc As String = "Included status items by bit."
    Public Const StatusName As String = "Status"
    Public Const StatusDesc As String = "Status byte."
    Public Const AuxStatusName As String = "Auxiliary Status"
    Public Const AuxStatusDesc As String = "Auxiliary status byte."

    Public Const PositionName As String = "Position"
    Public Const PositionDesc As String = "Reported motor position."
    Public Const ServoPositionErrorName As String = "Servo Position Error"
    Public Const ServoPositionErrorDesc As String = "Reported servo position error."
    Public Const HomePositionName As String = "Home Position"
    Public Const HomePositionDesc As String = "reported home position."
    Public Const ADName As String = "AD"
    Public Const ADDesc As String = "Reported AD value."
    Public Const VelocityName As String = "Velocity"
    Public Const VelocityDesc As String = "Reported velocity."
    Public Const PathPointsInBufferName As String = "Path Points in Buffer"
    Public Const PathPointsInBufferDesc As String = "Reported number of path points in the buffer."
    Public Const ChecksumIsValidName As String = "Valid Checksum"
    Public Const ChecksumIsValidDesc As String = "Reported checksum validity."
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pJRKerrServoStatus As JRKerrServoStatus
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As JRKerrServoStatusToPropGridAdapter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As JRKerrServoStatusToPropGridAdapter = New JRKerrServoStatusToPropGridAdapter
    'End Class
#End Region

#Region "Constructors"
    ' must be public for .NET to instantiate
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As JRKerrServoStatusToPropGridAdapter
        Return New JRKerrServoStatusToPropGridAdapter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property JRKerrServoStatus() As JRKerrServoStatus
        Get
            Return pJRKerrServoStatus
        End Get
        Set(ByVal value As JRKerrServoStatus)
            pJRKerrServoStatus = value
        End Set
    End Property

    Public Sub Adapt(ByRef devPropJRKerrServoStatus As JRKerrServoStatus)
        Me.JRKerrServoStatus = devPropJRKerrServoStatus

        pPropContainer = Config.PropContainer.GetInstance

        AddHandler pPropContainer.GetValue, AddressOf getValue
        AddHandler pPropContainer.SetValue, AddressOf setValue

        initAndAddPropParm(ModuleTypeName, GetType(System.Byte), ClassDescAttr, ModuleTypeDesc, Nothing, Nothing, GetType(ByteConverter))

        initAndAddPropParm(ModuleVersionName, GetType(System.Byte), ClassDescAttr, ModuleVersionDesc, Nothing, Nothing, GetType(ByteConverter))

        initAndAddPropParm(GroupAddressName, GetType(System.Byte), ClassDescAttr, GroupAddressDesc, Nothing, Nothing, GetType(ByteConverter))

        initAndAddPropParm(GroupLeaderName, GetType(System.Boolean), ClassDescAttr, GroupLeaderDesc, Nothing, Nothing, GetType(BooleanConverter))

        initAndAddPropParm(StatusItemsName, GetType(System.Byte), ClassDescAttr, StatusItemsDesc, Nothing, Nothing, GetType(ByteConverter))

        initAndAddPropParm(StatusItemsBitsName, GetType(System.String), ClassDescAttr, StatusItemsBitsDesc, Nothing, GetType(ListViewTypeEditorJRKerrServoStatusByteBitDefs), GetType(StringConverter))

        initAndAddPropParm(StatusName, GetType(System.Byte), ClassDescAttr, StatusDesc, Nothing, Nothing, GetType(ByteConverter))

        initAndAddPropParm(AuxStatusName, GetType(System.Byte), ClassDescAttr, GroupLeaderDesc, Nothing, Nothing, GetType(ByteConverter))

        initAndAddPropParm(PositionName, GetType(System.Int32), ClassDescAttr, AuxStatusDesc, Nothing, Nothing, GetType(Int32Converter))

        initAndAddPropParm(ServoPositionErrorName, GetType(System.Int16), ClassDescAttr, GroupLeaderDesc, Nothing, Nothing, GetType(Int16Converter))

        initAndAddPropParm(HomePositionName, GetType(System.Int32), ClassDescAttr, ServoPositionErrorDesc, Nothing, Nothing, GetType(Int32Converter))

        initAndAddPropParm(ADName, GetType(System.Byte), ClassDescAttr, ADDesc, Nothing, Nothing, GetType(ByteConverter))

        initAndAddPropParm(VelocityName, GetType(System.Int16), ClassDescAttr, VelocityDesc, Nothing, Nothing, GetType(Int16Converter))

        initAndAddPropParm(PathPointsInBufferName, GetType(System.Byte), ClassDescAttr, PathPointsInBufferDesc, Nothing, Nothing, GetType(ByteConverter))

        initAndAddPropParm(ChecksumIsValidName, GetType(System.Boolean), ClassDescAttr, ChecksumIsValidDesc, Nothing, Nothing, GetType(BooleanConverter))
    End Sub

    Public Overrides Function Clone() As Object
        Dim JRKerrServoStatusToPropGridAdapter As JRKerrServoStatusToPropGridAdapter = ScopeIII.Devices.JRKerrServoStatusToPropGridAdapter.GetInstance
        If JRKerrServoStatus IsNot Nothing Then
            JRKerrServoStatusToPropGridAdapter.Adapt(CType(JRKerrServoStatus.Clone, JRKerrServoStatus))
        End If
        Return JRKerrServoStatusToPropGridAdapter
    End Function
#End Region

#Region "Private and Protected Methods"
    ' ideally get/set from storage
    Protected Overrides Function getValue(ByVal sender As Object, ByVal e As PropParmEventArgs) As Boolean
        Select Case e.Property.Name
            Case ModuleTypeName
                e.Value = JRKerrServoStatus.ModuleType
            Case ModuleVersionName
                e.Value = JRKerrServoStatus.ModuleVersion
            Case GroupAddressName
                e.Value = JRKerrServoStatus.GroupAddress
            Case GroupLeaderName
                e.Value = JRKerrServoStatus.GroupLeader
            Case StatusItemsName
                e.Value = JRKerrServoStatus.StatusItems
            Case StatusItemsBitsName
                e.Value = StatusItemsToStatusByteBitDefsAdapter.ConvertToString(JRKerrServoStatus.StatusItems)
            Case StatusName
                e.Value = JRKerrServoStatus.Status
            Case AuxStatusName
                e.Value = JRKerrServoStatus.AuxStatus
            Case PositionName
                e.Value = JRKerrServoStatus.Position
            Case ServoPositionErrorName
                e.Value = JRKerrServoStatus.ServoPositionError
            Case HomePositionName
                e.Value = JRKerrServoStatus.HomePosition
            Case ADName
                e.Value = JRKerrServoStatus.AD
            Case VelocityName
                e.Value = JRKerrServoStatus.Velocity
            Case PathPointsInBufferName
                e.Value = JRKerrServoStatus.PathPointsInBuffer
            Case ChecksumIsValidName
                e.Value = JRKerrServoStatus.ChecksumIsValid
            Case Else
                Return False
        End Select
        Return True
    End Function

    Protected Overrides Function setValue(ByVal sender As Object, ByVal e As PropParmEventArgs) As Boolean
        Select Case e.Property.Name
            Case ModuleTypeName
                JRKerrServoStatus.ModuleType = CType(e.Value, Byte)
            Case ModuleVersionName
                JRKerrServoStatus.ModuleVersion = CType(e.Value, Byte)
            Case GroupAddressName
                JRKerrServoStatus.GroupAddress = CType(e.Value, Byte)
            Case GroupLeaderName
                JRKerrServoStatus.GroupLeader = CType(e.Value, Boolean)
            Case StatusItemsName
                JRKerrServoStatus.StatusItems = CType(e.Value, Byte)
            Case StatusItemsBitsName
                JRKerrServoStatus.StatusItems = StatusItemsToStatusByteBitDefsAdapter.ConvertToStatusItems(CStr(e.Value))
            Case StatusName
                JRKerrServoStatus.Status = CType(e.Value, Byte)
            Case AuxStatusName
                JRKerrServoStatus.AuxStatus = CType(e.Value, Byte)
            Case PositionName
                JRKerrServoStatus.Position = CType(e.Value, Int32)
            Case ServoPositionErrorName
                JRKerrServoStatus.ServoPositionError = CType(e.Value, Int16)
            Case HomePositionName
                JRKerrServoStatus.HomePosition = CType(e.Value, Int32)
            Case ADName
                JRKerrServoStatus.AD = CType(e.Value, Byte)
            Case VelocityName
                JRKerrServoStatus.Velocity = CType(e.Value, Int16)
            Case PathPointsInBufferName
                JRKerrServoStatus.PathPointsInBuffer = CType(e.Value, Byte)
            Case ChecksumIsValidName
                JRKerrServoStatus.ChecksumIsValid = CType(e.Value, Boolean)
            Case Else
                Return False
        End Select
        Return True
    End Function
#End Region

End Class
