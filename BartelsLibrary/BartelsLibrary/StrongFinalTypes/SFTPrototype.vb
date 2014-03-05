#Region "Imports"
#End Region

Public Class SFTPrototype
    Implements ISFT

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected pSFTSupport As SFTSupport
    Protected pSFTSharedSupport As SFTSharedSupport
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As SFTPrototype
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SFTPrototype = New SFTPrototype
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Sub New(ByRef facadeType As Type, ByRef SFTSharedSupport As SFTSharedSupport, ByVal description As String)
        Me.New(facadeType, SFTSharedSupport, description, Nothing)
    End Sub

    ' for inherited facades
    Public Sub New(ByRef facadeType As Type, ByRef SFTSharedSupport As SFTSharedSupport, ByVal description As String, ByRef tag As Object)
        SFTSharedSupport.FacadeType = facadeType
        Build(SFTSharedSupport, description, tag)
    End Sub

    Public Sub New(ByRef SFTSharedSupport As SFTSharedSupport, ByVal description As String)
        Me.New(SFTSharedSupport, description, Nothing)
    End Sub

    Public Sub New(ByRef SFTSharedSupport As SFTSharedSupport, ByVal description As String, ByRef tag As Object)
        Build(SFTSharedSupport, description, tag)
    End Sub

    Public Shared Function GetInstance() As SFTPrototype
        Return New SFTPrototype
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public ReadOnly Property FacadeName() As String Implements ISFT.FacadeName
        Get
            Return pSFTSharedSupport.Name
        End Get
    End Property

    Public ReadOnly Property Name() As String Implements ISFT.Name
        Get
            Return pSFTSupport.Name
        End Get
    End Property

    Public ReadOnly Property Key() As Integer Implements ISFT.Key
        Get
            Return pSFTSupport.Key
        End Get
    End Property

    Public ReadOnly Property Description() As String Implements ISFT.Description
        Get
            Return pSFTSupport.Description
        End Get
    End Property

    Public ReadOnly Property Tag() As Object Implements ISFT.Tag
        Get
            Return pSFTSupport.Tag
        End Get
    End Property

    Public Function FirstItem() As ISFT Implements ISFT.FirstItem
        Return pSFTSharedSupport.FirstItem
    End Function

    Public Function NextItem() As ISFT Implements ISFT.NextItem
        Return pSFTSharedSupport.MatchKey(pSFTSupport.Key + 1)
    End Function

    Public Function Size() As Integer Implements ISFT.Size
        Return pSFTSharedSupport.Size
    End Function

    Public Function Enumerator() As System.Collections.IEnumerator Implements ISFT.Enumerator
        Return pSFTSharedSupport.Enumerator()
    End Function

    Public Function MatchISFT(ByRef ISFT As ISFT) As ISFT Implements ISFT.MatchISFT
        Return pSFTSharedSupport.MatchISFT(ISFT)
    End Function

    Public Function MatchKey(ByVal key As Integer) As ISFT Implements ISFT.MatchKey
        Return pSFTSharedSupport.MatchKey(key)
    End Function

    Public Function MatchString(ByVal [string] As String) As ISFT Implements ISFT.MatchString
        Return pSFTSharedSupport.MatchString([string])
    End Function

    Public Function DataSource() As ArrayList Implements ISFT.DataSource
        Return pSFTSharedSupport.DataSource
    End Function

    Public Function GetList() As System.Collections.Generic.List(Of ISFT) Implements ISFT.GetList
        Return pSFTSharedSupport.GetList
    End Function

    Public Function ToStringArray() As String() Implements ISFT.ToStringArray
        Return pSFTSupport.ToStringArray
    End Function

    Public Function DisplayAll() As String Implements ISFT.DisplayAll
        Return pSFTSharedSupport.DisplayAll
    End Function

    Public Sub Build(ByRef SFTSharedSupport As SFTSharedSupport, ByVal description As String, ByRef tag As Object)

        'build shared support

        pSFTSharedSupport = SFTSharedSupport
        pSFTSharedSupport.Name = SFTSharedSupport.FacadeType.FullName
        pSFTSharedSupport.Add(Me)

        pSFTSupport = SFTSupport.GetInstance
        pSFTSupport.Key = pSFTSharedSupport.Size - 1
        pSFTSupport.Description = description
        pSFTSupport.Tag = tag

        ' build support

        ' get the facade's public shared items using reflection
        Dim facadeFieldNames As New ArrayList
        For Each fieldInfo As Reflection.FieldInfo In pSFTSharedSupport.FacadeType.GetFields(Reflection.BindingFlags.Public Or Reflection.BindingFlags.DeclaredOnly Or Reflection.BindingFlags.Static)
            facadeFieldNames.Add(fieldInfo.Name)
        Next
        ' go through each SFT, advancing the current field name pointer if field name already set, 
        ' otherwise if SFT name is empty then add current field name
        Dim eFieldName As IEnumerator = facadeFieldNames.GetEnumerator
        eFieldName.MoveNext()
        Dim eSharedSupport As IEnumerator = pSFTSharedSupport.Enumerator
        While eSharedSupport.MoveNext
            Dim SFTPrototype As SFTPrototype = CType(eSharedSupport.Current, SFTPrototype)
            If SFTPrototype.Name IsNot Nothing AndAlso SFTPrototype.Name.Equals(CStr(eFieldName.Current)) Then
                eFieldName.MoveNext()
            ElseIf String.IsNullOrEmpty(SFTPrototype.Name) Then
                pSFTSupport.Name = CStr(eFieldName.Current)
                eFieldName.MoveNext()
            End If
        End While
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

#Region "Private Classes"
    Protected Class SFTSupport

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
        Public Property Key() As Int32
            Get
                Return pKey
            End Get
            Set(ByVal Value As Int32)
                pKey = Value
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

        Public Property Description() As String
            Get
                Return pDescription
            End Get
            Set(ByVal Value As String)
                pDescription = Value
            End Set
        End Property

        Public Property Tag() As Object
            Get
                Return pTag
            End Get
            Set(ByVal Value As Object)
                pTag = Value
            End Set
        End Property
#End Region

#Region "Private and Protected Members"
        Private pKey As Integer
        Private pName As System.String
        Private pDescription As String
        Private pTag As Object
#End Region

#Region "Constructors (Singleton Pattern)"
        'Private Sub New()
        'End Sub

        'Public Shared Function GetInstance() As SFTSupport
        '    Return NestedInstance.INSTANCE
        'End Function

        'Private Class NestedInstance
        '    ' explicit constructor informs compiler not to mark type as beforefieldinit
        '    Shared Sub New()
        '    End Sub
        '    ' friend = internal, shared = static, readonly = final
        '    Friend Shared ReadOnly INSTANCE As SFTSupport = New SFTSupport
        'End Class
#End Region

#Region "Constructors"
        Private Sub New()
        End Sub

        Public Shared Function GetInstance() As SFTSupport
            Return New SFTSupport
        End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
        Public Function ToStringArray() As String()
            Dim stringArray(1) As String
            stringArray(0) = pName
            stringArray(1) = pDescription
            Return stringArray
        End Function
#End Region

#Region "Private and Protected Methods"
#End Region

    End Class
#End Region

End Class
