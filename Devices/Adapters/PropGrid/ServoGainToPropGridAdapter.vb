#Region "Imports"
Imports System.ComponentModel
#End Region

Public Class ServoGainToPropGridAdapter
    Inherits DevPropToPropGridAdapterBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public Const ClassDescAttr As String = "Servo Motor Gain Settings"

    Public Const PositionGainKpName As String = "PositionGain Kp"
    Public Const PositionGainKpDesc As String = "The proportional gain of the motor."
    Public Const DerivativeGainKdName As String = "DerivativeGain Kd"
    Public Const DerivativeGainKdDesc As String = "The derivative gain of the motor."
    Public Const IntegralGainKiName As String = "IntegralGain Ki"
    Public Const IntegralGainKiDesc As String = "The integral gain of the motor."
    Public Const IntegrationLimitILName As String = "IntegrationLimit"
    Public Const IntegrationLimitILDesc As String = "The integration limit of the motor."
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pServoGain As ServoGain
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ServoGainToPropGridAdapter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ServoGainToPropGridAdapter = New ServoGainToPropGridAdapter
    'End Class
#End Region

#Region "Constructors"
    ' must be public for .NET to instantiate
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As ServoGainToPropGridAdapter
        Return New ServoGainToPropGridAdapter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property ServoGain() As ServoGain
        Get
            Return pServoGain
        End Get
        Set(ByVal value As ServoGain)
            pServoGain = value
        End Set
    End Property

    Public Sub Adapt(ByRef servoGain As ServoGain)
        Me.ServoGain = servoGain

        pPropContainer = Config.PropContainer.GetInstance

        AddHandler pPropContainer.GetValue, AddressOf getValue
        AddHandler pPropContainer.SetValue, AddressOf setValue

        initAndAddPropParm(PositionGainKpName, GetType(System.Int16), ClassDescAttr, PositionGainKpDesc, Nothing, Nothing, GetType(Int16Converter))

        initAndAddPropParm(DerivativeGainKdName, GetType(System.Int16), ClassDescAttr, DerivativeGainKdDesc, Nothing, Nothing, GetType(Int16Converter))

        initAndAddPropParm(IntegralGainKiName, GetType(System.Int16), ClassDescAttr, IntegralGainKiDesc, Nothing, Nothing, GetType(Int16Converter))

        initAndAddPropParm(IntegrationLimitILName, GetType(System.Int16), ClassDescAttr, IntegrationLimitILDesc, Nothing, Nothing, GetType(Int16Converter))
    End Sub

    Public Overrides Function Clone() As Object
        Dim devPropServoGainToPropGridAdapter As ServoGainToPropGridAdapter = ScopeIII.Devices.ServoGainToPropGridAdapter.GetInstance
        If ServoGain IsNot Nothing Then
            devPropServoGainToPropGridAdapter.Adapt(CType(ServoGain.Clone, ServoGain))
        End If
        Return devPropServoGainToPropGridAdapter
    End Function
#End Region

#Region "Private and Protected Methods"
    ' ideally get/set from storage
    Protected Overrides Function getValue(ByVal sender As Object, ByVal e As PropParmEventArgs) As Boolean
        Select Case e.Property.Name
            Case PositionGainKpName
                e.Value = ServoGain.PositionGainKp
            Case DerivativeGainKdName
                e.Value = ServoGain.DerivativeGainKd
            Case IntegralGainKiName
                e.Value = ServoGain.IntegralGainKi
            Case IntegrationLimitILName
                e.Value = ServoGain.IntegrationLimitIL
            Case Else
                Return False
        End Select
        Return True
    End Function

    Protected Overrides Function setValue(ByVal sender As Object, ByVal e As PropParmEventArgs) As Boolean
        Select Case e.Property.Name
            Case PositionGainKpName
                ' Int16 = short
                ServoGain.PositionGainKp = CType(e.Value, Int16)
            Case DerivativeGainKdName
                ServoGain.DerivativeGainKd = CType(e.Value, Int16)
            Case IntegralGainKiName
                ServoGain.IntegralGainKi = CType(e.Value, Int16)
            Case IntegrationLimitILName
                ServoGain.IntegrationLimitIL = CType(e.Value, Int16)
            Case Else
                Return False
        End Select
        Return True
    End Function
#End Region

End Class
