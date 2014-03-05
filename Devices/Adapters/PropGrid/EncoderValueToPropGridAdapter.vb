Imports System.ComponentModel

#Region "Imports"
#End Region

Public Class EncoderValueToPropGridAdapter
    Inherits ValueObjectToPropGridAdapter

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public Const EncoderValueDescAttr As String = "Encoder Value Settings"

    Public Const Carries As String = "Carries"
    Public Const CarriesDesc As String = "The encoder's carries, or times the encoder has rotated a complete revolution."
    Public Const Rotation As String = "Direction of Rotation"
    Public Const RotationDesc As String = "The direction of rotation when the encoder counts upward.  Must select from a list."
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pEncoderValue As EncoderValue
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As EncoderValueToPropGridAdapter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As EncoderValueToPropGridAdapter = New EncoderValueToPropGridAdapter
    'End Class
#End Region

#Region "Constructors"
    ' must be public for .NET to instantiate
    Protected Sub New()
    End Sub

    Public Shared Shadows Function GetInstance() As EncoderValueToPropGridAdapter
        Return New EncoderValueToPropGridAdapter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property EncoderValue() As EncoderValue
        Get
            Return pEncoderValue
        End Get
        Set(ByVal value As EncoderValue)
            pEncoderValue = value
        End Set
    End Property

    Public Overloads Sub Adapt(ByRef encoderValue As EncoderValue)
        Me.EncoderValue = encoderValue
        MyBase.Adapt(CType(encoderValue, ValueObject))

        initAndAddPropParm(Carries, GetType(System.Int32), EncoderValueDescAttr, CarriesDesc, Nothing, Nothing, GetType(Int32Converter))

        initAndAddPropParm(Rotation, GetType(System.String), EncoderValueDescAttr, RotationDesc, Nothing, GetType(ListViewTypeEditorRotation), GetType(StringConverter))
    End Sub

    Public Overrides Function Clone() As Object
        Dim devPropEncoderValueToPropGridAdapter As EncoderValueToPropGridAdapter = ScopeIII.Devices.EncoderValueToPropGridAdapter.GetInstance
        If EncoderValue IsNot Nothing Then
            devPropEncoderValueToPropGridAdapter.Adapt(CType(EncoderValue.Clone, EncoderValue))
        End If
        Return devPropEncoderValueToPropGridAdapter
    End Function
#End Region

#Region "Private and Protected Methods"
    ' ideally get/set from storage
    Protected Overrides Function getValue(ByVal sender As Object, ByVal e As PropParmEventArgs) As Boolean
        If MyBase.getValue(sender, e) Then
            Return True
        End If
        Select Case e.Property.Name
            Case Carries
                e.Value = EncoderValue.Carries
            Case Rotation
                e.Value = EncoderValue.Rotation
            Case Else
                unprocessedProperty(e)
                Return False
        End Select
        Return True
    End Function

    Protected Overrides Function setValue(ByVal sender As Object, ByVal e As PropParmEventArgs) As Boolean
        If MyBase.setValue(sender, e) Then
            Return True
        End If
        Select Case e.Property.Name
            Case Carries
                EncoderValue.Carries = CType(e.Value, Int32)
            Case Rotation
                EncoderValue.Rotation = CStr(e.Value)
            Case Else
                unprocessedProperty(e)
                Return False
        End Select
        Return True
    End Function
#End Region

End Class
