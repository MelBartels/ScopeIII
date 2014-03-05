#Region "Imports"
#End Region

Public Class EditableDataBitsList
    Inherits System.ComponentModel.Int32Converter

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public DataBitsList() As Int32
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As EditableDataBitsList
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As EditableDataBitsList = New EditableDataBitsList
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        createDataBitsList()
    End Sub

    Public Shared Function GetInstance() As EditableDataBitsList
        Return New EditableDataBitsList
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overloads Overrides Function GetStandardValues(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.ComponentModel.TypeConverter.StandardValuesCollection
        Return New StandardValuesCollection(DataBitsList)
    End Function

    ' no drop down w/o this
    Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function

    ' false == editable
    Public Overloads Overrides Function GetStandardValuesExclusive(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function
#End Region

#Region "Private and Protected Methods"
    Private Sub createDataBitsList()
        DataBitsList = New Int32() { _
            5, _
            6, _
            7, _
            8 _
        }
    End Sub
#End Region

End Class
