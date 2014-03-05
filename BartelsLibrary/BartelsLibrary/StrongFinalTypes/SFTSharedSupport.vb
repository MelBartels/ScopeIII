#Region "Imports"
Imports System.Collections.Generic
#End Region

Public Class SFTSharedSupport

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pFacadeType As Type
    Private pName As String
    ' array added to in SFTPrototype.Build via public Add(), which is called by SFTPrototype ctor
    Private pArray As ArrayList
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As SFTSharedSupport
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SFTSharedSupport = New SFTSharedSupport
    'End Class
#End Region

#Region "Constructors"
    Public Sub New(ByVal facadeType As Type)
        pFacadeType = facadeType
        pArray = New ArrayList
    End Sub

    'Public Shared Function GetInstance() As SFTSharedSupport
    '    Return New SFTSharedSupport
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property FacadeType() As Type
        Get
            Return pFacadeType
        End Get
        Set(ByVal Value As Type)
            pFacadeType = Value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return pName
        End Get
        Set(ByVal Value As String)
            pName = Value
        End Set
    End Property

    Public Sub Add(ByVal isft As ISFT)
        pArray.Add(isft)
    End Sub

    Public Function Size() As Int32
        Return pArray.Count
    End Function

    Public Function FirstItem() As ISFT
        Return CType(pArray(0), ISFT)
    End Function

    Public Function Enumerator() As System.Collections.IEnumerator
        Return New AnonymousInnerClassAsEnumerator(pArray)
    End Function

    Public Function MatchISFT(ByRef ISFT As ISFT) As ISFT
        For Each myIsft As ISFT In pArray
            If myIsft Is ISFT Then
                Return myIsft
            End If
        Next
        Return Nothing
    End Function

    Public Function MatchKey(ByVal key As Int32) As ISFT
        If key < pArray.Count Then
            Return CType(pArray(key), ISFT)
        End If
        Return Nothing
    End Function

    Public Function MatchString(ByVal [string] As String) As ISFT
        For Each isft As ISFT In pArray
            If isft.Name.Equals([string]) OrElse isft.Description.Equals([string]) Then
                Return isft
            End If
        Next
        Return Nothing
    End Function

    Public Function DataSource() As ArrayList
        Dim array As New ArrayList
        For Each isft As ISFT In pArray
            If Not String.IsNullOrEmpty(isft.Description) Then
                array.Add(isft.Description)
            End If
        Next
        Return array
    End Function

    Public Function GetList() As List(Of ISFT)
        Dim list As New List(Of ISFT)
        For Each isft As ISFT In pArray
            list.Add(isft)
        Next
        Return list
    End Function

    Public Function DisplayAll() As System.String
        Dim sb As New Text.StringBuilder
        For Each isft As ISFT In pArray
            sb.Append(isft.Name)
            sb.Append(", ")
            sb.Append(isft.Description)
            sb.Append(ControlChars.Lf)
        Next
        Return sb.ToString
    End Function
#End Region

#Region "Private and Protected Methods"
    Private Class AnonymousInnerClassAsEnumerator
        Implements System.Collections.IEnumerator
        Private pArray As ArrayList
        Private pKey As Int32
        Public Sub New(ByVal array As ArrayList)
            pArray = array
            Reset()
        End Sub
        Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
            pKey += 1
            If pKey >= pArray.Count Then
                Return False
            End If
            Return True
        End Function
        Public Sub Reset() Implements System.Collections.IEnumerator.Reset
            pKey = -1
        End Sub
        Public ReadOnly Property Current() As System.Object Implements System.Collections.IEnumerator.Current
            Get
                Return pArray(pKey)
            End Get
        End Property
    End Class
#End Region

End Class
