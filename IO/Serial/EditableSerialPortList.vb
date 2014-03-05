#Region "Imports"
Imports System.IO
#End Region

Public Class EditableSerialPortList
    Inherits System.ComponentModel.StringConverter

#Region "Inner Classes"
    Private Class serialPortNumComparer : Implements IComparer
        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
            Return portNum(CStr(x)).CompareTo(portNum(CStr(y)))
        End Function
        Private Function portNum(ByVal portName As String) As Int32
            Return Int32.Parse(portName.Replace("COM", String.Empty))
        End Function
    End Class
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public SerialPortList() As String
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As EditableSerialPortList
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As EditableSerialPortList = New EditableSerialPortList
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        createSerialPortList()
    End Sub

    Public Shared Function GetInstance() As EditableSerialPortList
        Return New EditableSerialPortList
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overloads Overrides Function GetStandardValues(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.ComponentModel.TypeConverter.StandardValuesCollection
        Return New StandardValuesCollection(SerialPortList)
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
    Private Sub createSerialPortList()
        SerialPortList = Ports.SerialPort.GetPortNames
        Array.Sort(SerialPortList, New serialPortNumComparer)
    End Sub
#End Region

End Class
