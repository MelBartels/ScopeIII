#Region "Imports"
Imports System.ComponentModel
#End Region

Public Class JRKerrServoControlToPropGridAdapter
    Inherits DevPropToPropGridAdapterBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public Const ClassDescAttr As String = "JRKerr Servo Motor Control Settings"

    Public Const OutputLimitOLName As String = "OutputLimit"
    Public Const OutputLimitOLDesc As String = "The output limit of the motor.  For instance, a 12 volt motor running on 24 volts should have an output limit of 255/2=127."
    Public Const CurrentLimitCLName As String = "CurrentLimit"
    Public Const CurrentLimitCLDesc As String = "The current limit of the motor.  Max=255."
    Public Const PositionErrorLimitELName As String = "PositionErrorLimit"
    Public Const PositionErrorLimitELDesc As String = "The maximum position error for the motor.  If exceeded, the position servo is disabled.  Max=32767."
    Public Const ServoRateDivisorSRName As String = "ServoRateDivisor"
    Public Const ServoRateDivisorSRDesc As String = "The rate divisor.  Increasing this value from the default of 1 increases the time difference between position measurements, reducing digitation noise and averaging the derivative error term."
    Public Const AmpDeadbandCompensationDBName As String = "AmpDeadbandCompensation"
    Public Const AmpDeadbandCompensationDBDesc As String = "The deadband is an 8-bit offset used to compensate for static friction or a dead band region in the amplifier."
    Public Const StepRateMultiplierSMName As String = "StepRateMultiplier"
    Public Const StepRateMultiplierSMDesc As String = "The step count is multiplied by this value every servo cycle.  Default is 1."
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pJRKerrServoControl As JRKerrServoControl
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As JRKerrServoControlToPropGridAdapter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As JRKerrServoControlToPropGridAdapter = New JRKerrServoControlToPropGridAdapter
    'End Class
#End Region

#Region "Constructors"
    ' must be public for .NET to instantiate
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As JRKerrServoControlToPropGridAdapter
        Return New JRKerrServoControlToPropGridAdapter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property JRKerrServoControl() As JRKerrServoControl
        Get
            Return pJRKerrServoControl
        End Get
        Set(ByVal value As JRKerrServoControl)
            pJRKerrServoControl = value
        End Set
    End Property

    Public Sub Adapt(ByRef JRKerrServoControl As JRKerrServoControl)
        Me.JRKerrServoControl = JRKerrServoControl

        pPropContainer = Config.PropContainer.GetInstance

        AddHandler pPropContainer.GetValue, AddressOf getValue
        AddHandler pPropContainer.SetValue, AddressOf setValue

        initAndAddPropParm(OutputLimitOLName, GetType(System.Byte), ClassDescAttr, OutputLimitOLDesc, Nothing, Nothing, GetType(ByteConverter))

        initAndAddPropParm(CurrentLimitCLName, GetType(System.Byte), ClassDescAttr, CurrentLimitCLDesc, Nothing, Nothing, GetType(ByteConverter))

        initAndAddPropParm(PositionErrorLimitELName, GetType(System.Int16), ClassDescAttr, PositionErrorLimitELDesc, Nothing, Nothing, GetType(Int16Converter))

        initAndAddPropParm(ServoRateDivisorSRName, GetType(System.Byte), ClassDescAttr, ServoRateDivisorSRDesc, Nothing, Nothing, GetType(ByteConverter))

        initAndAddPropParm(AmpDeadbandCompensationDBName, GetType(System.Byte), ClassDescAttr, AmpDeadbandCompensationDBDesc, Nothing, Nothing, GetType(ByteConverter))

        initAndAddPropParm(StepRateMultiplierSMName, GetType(System.Byte), ClassDescAttr, StepRateMultiplierSMDesc, Nothing, Nothing, GetType(ByteConverter))

    End Sub

    Public Overrides Function Clone() As Object
        Dim JRKerrServoControlToPropGridAdapter As JRKerrServoControlToPropGridAdapter = ScopeIII.Devices.JRKerrServoControlToPropGridAdapter.GetInstance
        If JRKerrServoControl IsNot Nothing Then
            JRKerrServoControlToPropGridAdapter.Adapt(CType(JRKerrServoControl.Clone, JRKerrServoControl))
        End If
        Return JRKerrServoControlToPropGridAdapter
    End Function
#End Region

#Region "Private and Protected Methods"
    ' ideally get/set from storage
    Protected Overrides Function getValue(ByVal sender As Object, ByVal e As PropParmEventArgs) As Boolean
        Select Case e.Property.Name
            Case OutputLimitOLName
                e.Value = JRKerrServoControl.OutputLimitOL
            Case CurrentLimitCLName
                e.Value = JRKerrServoControl.CurrentLimitCL
            Case PositionErrorLimitELName
                e.Value = JRKerrServoControl.PositionErrorLimitEL
            Case ServoRateDivisorSRName
                e.Value = JRKerrServoControl.ServoRateDivisorSR
            Case AmpDeadbandCompensationDBName
                e.Value = JRKerrServoControl.AmpDeadbandCompensationDB
            Case StepRateMultiplierSMName
                e.Value = JRKerrServoControl.StepRateMultiplierSM
            Case Else
                Return False
        End Select
        Return True
    End Function

    Protected Overrides Function setValue(ByVal sender As Object, ByVal e As PropParmEventArgs) As Boolean
        Select Case e.Property.Name
            Case OutputLimitOLName
                JRKerrServoControl.OutputLimitOL = CByte(e.Value)
            Case CurrentLimitCLName
                JRKerrServoControl.CurrentLimitCL = CByte(e.Value)
            Case PositionErrorLimitELName
                ' Int16 = Short
                JRKerrServoControl.PositionErrorLimitEL = CType(e.Value, Int16)
            Case ServoRateDivisorSRName
                JRKerrServoControl.ServoRateDivisorSR = CByte(e.Value)
            Case AmpDeadbandCompensationDBName
                JRKerrServoControl.AmpDeadbandCompensationDB = CByte(e.Value)
            Case StepRateMultiplierSMName
                JRKerrServoControl.StepRateMultiplierSM = CByte(e.Value)
            Case Else
                Return False
        End Select
        Return True
    End Function
#End Region

End Class
