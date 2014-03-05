#Region "Imports"
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Runtime.Serialization
Imports System.IO
#End Region

Public Class Cloner

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As Cloner
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As Cloner = New Cloner
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As Cloner
        Return New Cloner
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    ' copies object to stream then deserializes stream returning output to clone new object;
    ' object must be serializable
    Public Function ViaSerialization(ByVal [object] As Object) As Object
        Dim binFormatter As New BinaryFormatter(Nothing, New StreamingContext(StreamingContextStates.Clone))
        Dim memStream As New MemoryStream(8192)
        binFormatter.Serialize(memStream, [object])

        memStream.Seek(0, SeekOrigin.Begin)
        Dim cloneObject As Object = binFormatter.Deserialize(memStream)
        memStream.Close()
        Return cloneObject
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
