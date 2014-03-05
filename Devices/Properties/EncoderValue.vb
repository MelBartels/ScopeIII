#Region "Imports"
#End Region

Public Class EncoderValue
    Inherits ValueObject

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected pValueType As String
    Private pValueStatus As String
    Private pLastValue As String
    Private pCarries As Int32
    Private pRotation As String
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As EncoderValue
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As EncoderValue = New EncoderValue
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Shadows Function GetInstance() As EncoderValue
        Return New EncoderValue
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Property Value() As String
        Get
            Return MyBase.Value
        End Get
        Set(ByVal Value As String)
            validateAndSetValue(Value)
        End Set
    End Property

    Public Property ValueType() As String
        Get
            If pValueType Is Nothing OrElse SensorTypes.ISFT.MatchString(pValueType) Is Nothing Then
                Return SensorTypes.ISFT.Description
            Else
                Return pValueType
            End If
        End Get
        Set(ByVal Value As String)
            If Value IsNot Nothing AndAlso SensorTypes.ISFT.MatchString(Value) IsNot Nothing Then
                pValueType = Value
            End If
        End Set
    End Property

    Public Property ValueStatus() As String
        Get
            If pValueStatus Is Nothing OrElse SensorStatus.ISFT.MatchString(pValueStatus) Is Nothing Then
                Return SensorStatus.ISFT.Description
            Else
                Return pValueStatus
            End If
        End Get
        Set(ByVal Value As String)
            If Value IsNot Nothing AndAlso SensorStatus.ISFT.MatchString(Value) IsNot Nothing Then
                pValueStatus = Value
            End If
        End Set
    End Property

    Public Property Carries() As Int32
        Get
            Return pCarries
        End Get
        Set(ByVal Value As Int32)
            pCarries = Value
        End Set
    End Property

    Public Property Rotation() As String
        Get
            If pRotation Is Nothing OrElse BartelsLibrary.Rotation.ISFT.MatchString(pRotation) Is Nothing Then
                Return BartelsLibrary.Rotation.ISFT.Description
            Else
                Return pRotation
            End If
        End Get
        Set(ByVal Value As String)
            If Value IsNot Nothing AndAlso BartelsLibrary.Rotation.ISFT.MatchString(Value) IsNot Nothing Then
                pRotation = Value
            End If
        End Set
    End Property

    Public Overloads Sub Build(ByVal minValue As Double, ByVal maxValue As Double, ByVal rotation As String)
        Me.ValueName = GetType(ScopeIII.Devices.EncoderValue).Name
        Me.ValueType = GetType(ScopeIII.Devices.EncoderValue).Name
        Me.MinValue = minValue.ToString
        Me.MaxValue = maxValue.ToString
        Me.Rotation = rotation
        Me.UOM = BartelsLibrary.UOM.Counts.Name
        setValue(pMinValue.ToString)
    End Sub

    Public Overrides Function Range() As Double
        Dim maxValueDbl As Double = CType(MaxValue, Double)
        Dim minValueDbl As Double = CType(MinValue, Double)
        Return maxValueDbl - minValueDbl + 1
    End Function

    Public Function ConvertRadToTicks(ByVal angleRad As Double) As String
        Dim maxValueDbl As Double = CType(MaxValue, Double)
        Dim minValueDbl As Double = CType(MinValue, Double)
        Dim ticks As Double = angleRad / Units.OneRev * Range() + minValueDbl
        Return CStr(ticks)
    End Function

    Public Function TotalTicks() As Double
        Dim maxValueDbl As Double = CType(MaxValue, Double)
        Dim minValueDbl As Double = CType(MinValue, Double)
        Dim ticks As Double = CType(Value, Double)
        Return Carries * Range() + ticks - minValueDbl
    End Function

    Public Overloads Sub CopyPropertiesTo(ByRef encoderValue As EncoderValue)
        MyBase.CopyPropertiesTo(CType(encoderValue, ValueObject))

        With encoderValue
            .ValueType = ValueType
            .ValueStatus = ValueStatus
            .Carries = Carries
            .Rotation = Rotation
            .UOM = UOM
        End With
    End Sub

    Public Overrides Function Clone() As Object
        Dim clonedEncoderValue As EncoderValue = EncoderValue.GetInstance
        CopyPropertiesTo(clonedEncoderValue)
        Return clonedEncoderValue
    End Function

    Public Overrides Function Properties() As String
        Dim sb As New Text.StringBuilder

        sb.Append(MyBase.Properties)
        sb.Append(", ValueType: ")
        sb.Append(ValueType)
        sb.Append(", ValueStatus: ")
        sb.Append(ValueStatus)
        sb.Append(", Carries: ")
        sb.Append(Carries)
        sb.Append(", Rotation: ")
        sb.Append(Rotation)
        sb.Append(", UOM: ")
        sb.Append(UOM)

        Return sb.ToString
    End Function

    Public Overloads Function SetValueStatus(ByVal status As String) As String
        pValueStatus = status
        Return status
    End Function
#End Region

#Region "Private and Protected Methods"
    Private Sub validateAndSetValue(ByVal newValue As String)
        If validateValue(newValue) Then
            setValue(newValue)
        End If
    End Sub

    Private Sub setValue(ByVal newValue As String)
        Dim maxValueDbl As Double = CType(MaxValue, Double)
        Dim minValueDbl As Double = CType(MinValue, Double)
        Dim valueRange As Double = maxValueDbl - minValueDbl
        Dim lastValueDbl As Double = CType(pLastValue, Double)
        Dim newValueDbl As Double = CType(newValue, Double)
        Dim absDiffDbl As Double = Math.Abs(lastValueDbl - newValueDbl)

        ' new    last    carries
        '  30      60          0
        '  30     330         +1 (crossed 0 going forward)
        '  60      30          0
        ' 330      30         -1 (crossed 0 going backwards)
        ' crossed zero?
        If absDiffDbl > valueRange / 2 Then
            ' which direction?
            If newValueDbl < lastValueDbl Then
                pCarries += 1
            Else
                pCarries -= 1
            End If
        End If

        ' convert string -> double -> int -> string (handles numeric formatting like '+', 5623.7)
        pValue = CStr(eMath.RInt(newValueDbl))
        pLastValue = pValue

        SetValueStatus(SensorStatus.ValidRead.Description)
        ObservableImp.Notify(CType(Value, Object))
    End Sub

    Private Function validateValue(ByVal newValue As String) As Boolean
        Dim newValueDbl As Double = 0
        Dim valid As Boolean = Double.TryParse(newValue, newValueDbl)
        If valid Then
            Dim maxValueDbl As Double = CType(MaxValue, Double)
            Dim minValueDbl As Double = CType(MinValue, Double)
            valid = newValueDbl >= minValueDbl AndAlso newValueDbl <= maxValueDbl
            SetValueStatus(newValueDbl, minValueDbl, maxValueDbl)
        Else
            SetValueStatus(SensorStatus.InvalidFormat.Description)
        End If
        Return valid
    End Function

    Private Overloads Sub setValueStatus(ByVal newValue As Double, ByVal minValue As Double, ByVal maxValue As Double)
        If newValue > maxValue Then
            SetValueStatus(SensorStatus.ExceedsMaximum.Description)
        ElseIf newValue < minValue Then
            SetValueStatus(SensorStatus.BelowMinimum.Description)
        ElseIf Not ValueStatus.Equals(SensorStatus.ValidRead.Description) Then
            SetValueStatus(SensorStatus.ValidRead.Description)
        End If
    End Sub
#End Region

End Class
