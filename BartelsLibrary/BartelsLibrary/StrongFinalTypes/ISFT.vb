Imports System.Collections.Generic

Public Interface ISFT
    ReadOnly Property FacadeName() As String
    ReadOnly Property Key() As Int32
    ReadOnly Property Name() As String
    ReadOnly Property Description() As String
    ReadOnly Property Tag() As Object
    Function FirstItem() As ISFT
    Function NextItem() As ISFT
    Function Size() As Int32
    Function Enumerator() As System.Collections.IEnumerator
    Function MatchISFT(ByRef ISFT As ISFT) As ISFT
    Function MatchKey(ByVal key As Int32) As ISFT
    Function MatchString(ByVal [string] As String) As ISFT
    Function DataSource() As ArrayList
    Function GetList() As List(Of ISFT)
    Function ToStringArray() As String()
    Function DisplayAll() As String
End Interface