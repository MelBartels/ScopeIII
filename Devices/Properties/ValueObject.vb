#Region "Imports"
#End Region

Public Class ValueObject
    Inherits DevPropBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected pValueName As String
    Protected pMinValue As String
    Protected pMaxValue As String
    Protected pUom As String
    Protected pValue As String
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ValueObject
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ValueObject = New ValueObject
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As ValueObject
        Return New ValueObject
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property ValueName() As String
        Get
            Return pValueName
        End Get
        Set(ByVal value As String)
            pValueName = value
        End Set
    End Property

    Public Property MinValue() As String
        Get
            Return pMinValue
        End Get
        Set(ByVal Value As String)
            pMinValue = Value
        End Set
    End Property

    Public Property MaxValue() As String
        Get
            Return pMaxValue
        End Get
        Set(ByVal Value As String)
            pMaxValue = Value
        End Set
    End Property

    Public Property UOM() As String
        Get
            If pUom Is Nothing OrElse BartelsLibrary.UOM.ISFT.MatchString(pUom) Is Nothing Then
                Return BartelsLibrary.UOM.ISFT.Description
            Else
                Return pUom
            End If
        End Get
        Set(ByVal Value As String)
            If Value IsNot Nothing AndAlso BartelsLibrary.UOM.ISFT.MatchString(Value) IsNot Nothing Then
                pUom = Value
            End If
        End Set
    End Property

    Public Overrides Property Value() As String
        Get
            Return pValue
        End Get
        Set(ByVal value As String)
            pValue = value
            ObservableImp.Notify(CType(value, Object))
        End Set
    End Property

    Public Overridable Sub Build(ByVal name As String, ByVal value As String, ByVal minValue As String, ByVal maxValue As String, ByVal uom As String)
        Me.ValueName = name
        Me.MinValue = minValue
        Me.MaxValue = maxValue
        Me.UOM = uom
        Me.Value = value
    End Sub

    Public Overridable Function ConvertValueToRad(ByVal value As Double) As Double
        Dim maxValueDbl As Double = CType(MaxValue, Double)
        Dim minValueDbl As Double = CType(MinValue, Double)
        Return (value - minValueDbl) / Range() * Units.OneRev
    End Function

    Public Overridable Function Range() As Double
        Dim maxValueDbl As Double = CType(MaxValue, Double)
        Dim minValueDbl As Double = CType(MinValue, Double)
    End Function

    Public Overridable Sub CopyPropertiesTo(ByRef valueObject As ValueObject)
        valueObject.ValueName = ValueName
        valueObject.MinValue = MinValue
        valueObject.MaxValue = MaxValue
        valueObject.UOM = UOM
        valueObject.Value = Value
    End Sub

    Public Overrides Function Clone() As Object
        Dim clonedValueObject As ValueObject = ValueObject.GetInstance
        CopyPropertiesTo(clonedValueObject)
        Return clonedValueObject
    End Function

    Public Overridable Function Properties() As String
        Dim sb As New Text.StringBuilder

        sb.Append(ValueName)
        sb.Append(": ")
        sb.Append(Value)
        sb.Append(", Min: ")
        sb.Append(MinValue)
        sb.Append(", Max: ")
        sb.Append(MaxValue)
        sb.Append(", UOM: ")
        sb.Append(UOM)

        Return sb.ToString
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
