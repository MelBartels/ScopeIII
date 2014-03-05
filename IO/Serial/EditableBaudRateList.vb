#Region "Imports"
#End Region

Public Class EditableBaudRateList
    Inherits System.ComponentModel.Int32Converter

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public BaudRateList() As Int32
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As EditableBaudRateList
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As EditableBaudRateList = New EditableBaudRateList
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        createBaudRateList()
    End Sub

    Public Shared Function GetInstance() As EditableBaudRateList
        Return New EditableBaudRateList
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overloads Overrides Function GetStandardValues(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.ComponentModel.TypeConverter.StandardValuesCollection
        Return New StandardValuesCollection(BaudRateList)
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
    Private Sub createBaudRateList()
        BaudRateList = New Int32() { _
            128000, _
            115200, _
            57600, _
            38400, _
            19200, _
            14400, _
            9600, _
            7200, _
            4800, _
            2400, _
            1800, _
            1200, _
            600, _
            300, _
            150, _
            134, _
            110, _
            75 _
        }
    End Sub
#End Region

End Class
