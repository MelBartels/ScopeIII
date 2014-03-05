#Region "Imports"
Imports System.ComponentModel
#End Region

Public Class ValueObjectToPropGridAdapter
    Inherits DevPropToPropGridAdapterBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public Const ClassDescAttr As String = "Value Settings"

    Public Const ValueName As String = "Value Name"
    Public Const ValueNameDesc As String = "The name of the value."
    Public Const UOMName As String = "Unit of Measurement"
    Public Const UOMDesc As String = "Unit of measurement.  Select from a list."
    Public Const Value As String = "Value"
    Public Const ValueDesc As String = "The value."
    Public Const MinValue As String = "Minimum Value"
    Public Const MinValueDesc As String = "The minimum value."
    Public Const MaxValue As String = "Maximum Value"
    Public Const MaxValueDesc As String = "The maximum value."
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pValueObject As ValueObject
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ValueObjectToPropGridAdapter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ValueObjectToPropGridAdapter = New ValueObjectToPropGridAdapter
    'End Class
#End Region

#Region "Constructors"
    ' must be public for .NET to instantiate
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As ValueObjectToPropGridAdapter
        Return New ValueObjectToPropGridAdapter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property ValueObject() As ValueObject
        Get
            Return pValueObject
        End Get
        Set(ByVal value As ValueObject)
            pValueObject = value
        End Set
    End Property

    Public Sub Adapt(ByRef valueObject As ValueObject)
        Me.ValueObject = ValueObject

        pPropContainer = Config.PropContainer.GetInstance

        AddHandler pPropContainer.GetValue, AddressOf getValue
        AddHandler pPropContainer.SetValue, AddressOf setValue

        pPropParm = initAndAddPropParm(ValueName, GetType(System.String), ClassDescAttr, ValueNameDesc, Nothing, Nothing, GetType(StringConverter))
        pPropParm.Attributes = New Attribute() {ReadOnlyAttribute.Yes}

        initAndAddPropParm(UOMName, GetType(System.String), ClassDescAttr, UOMDesc, Nothing, GetType(ListViewTypeEditorUOM), GetType(StringConverter))

        initAndAddPropParm(Value, GetType(System.String), ClassDescAttr, ValueDesc, Nothing, Nothing, Nothing)

        initAndAddPropParm(MinValue, GetType(System.String), ClassDescAttr, MinValueDesc, Nothing, Nothing, Nothing)

        initAndAddPropParm(MaxValue, GetType(System.String), ClassDescAttr, MaxValueDesc, Nothing, Nothing, Nothing)
    End Sub

    Public Overrides Function Clone() As Object
        Dim valueObjectToPropGridAdapter As ValueObjectToPropGridAdapter = ScopeIII.Devices.ValueObjectToPropGridAdapter.GetInstance
        If ValueObject IsNot Nothing Then
            valueObjectToPropGridAdapter.Adapt(CType(ValueObject.Clone, ValueObject))
        End If
        Return valueObjectToPropGridAdapter
    End Function
#End Region

#Region "Private and Protected Methods"
    ' ideally get/set from storage
    Protected Overrides Function getValue(ByVal sender As Object, ByVal e As PropParmEventArgs) As Boolean
        Select Case e.Property.Name
            Case ValueName
                e.Value = ValueObject.ValueName
            Case UOMName
                e.Value = ValueObject.UOM
            Case Value
                e.Value = ValueObject.Value
            Case MinValue
                e.Value = ValueObject.MinValue
            Case MaxValue
                e.Value = ValueObject.MaxValue
            Case Else
                Return False
        End Select
        Return True
    End Function

    Protected Overrides Function setValue(ByVal sender As Object, ByVal e As PropParmEventArgs) As Boolean
        Select Case e.Property.Name
            Case ValueName
                ValueObject.ValueName = CStr(e.Value)
            Case UOMName
                ValueObject.UOM = CStr(e.Value)
            Case Value
                ValueObject.Value = CStr(e.Value)
            Case MinValue
                ValueObject.MinValue = CStr(e.Value)
            Case MaxValue
                ValueObject.MaxValue = CStr(e.Value)
            Case Else
                Return False
        End Select
        Return True
    End Function
#End Region

End Class
