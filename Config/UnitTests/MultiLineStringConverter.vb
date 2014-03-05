Public Class MultiLineStringConverter
    Inherits System.ComponentModel.TypeConverter

    Public Overloads Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
        ' if sourceType Is GetType(String) Then Return True, editing occurs in PG w/o editor
        Return MyBase.CanConvertFrom(context, sourceType)
    End Function

    Public Overloads Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
        If destinationType Is GetType(String) Then
            Return True
        End If
        Return MyBase.CanConvertFrom(context, destinationType)
    End Function

    Public Overloads Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
        If TypeOf value Is String Then
            Try
                Return New MultiLineString(CStr(value))
            Catch
                Throw New InvalidCastException(CStr(value))
            End Try
        End If
        Return MyBase.ConvertFrom(context, culture, value)
    End Function

    Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
        If (destinationType.Equals(GetType(String))) Then
            Return value.ToString()
        Else
            Return MyBase.ConvertTo(context, culture, value, destinationType)
        End If
    End Function

    Public Overloads Overrides Function GetPropertiesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        ' otherwise properties will be recursive
        Return False
    End Function

    Public Overloads Overrides Function GetProperties(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal value As Object, ByVal attributes() As System.Attribute) As System.ComponentModel.PropertyDescriptorCollection
        Return Nothing
    End Function
End Class