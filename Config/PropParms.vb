#Region "Imports"
#End Region

Public Class PropParm
    Implements ICloneable

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pAttributes() As Attribute
    Private pCategory As String
    Private pDefaultValue As Object
    Private pDescription As String
    Private pEditor As String
    Private pName As String
    Private pType As String
    Private pTypeConverter As String
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As PropParm
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As PropParm = New PropParm
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As PropParm
        Return New PropParm
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property Attributes() As Attribute()
        Get
            Return pAttributes
        End Get
        Set(ByVal Value As Attribute())
            pAttributes = Value
        End Set
    End Property

    Public Property Category() As String
        Get
            Return pCategory
        End Get
        Set(ByVal Value As String)
            pCategory = Value
        End Set
    End Property

    Public Property DefaultValue() As Object
        Get
            Return pDefaultValue
        End Get
        Set(ByVal Value As Object)
            pDefaultValue = Value
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

    Public Property EditorType() As String
        Get
            Return pEditor
        End Get
        Set(ByVal Value As String)
            pEditor = Value
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

    Public Property Type() As String
        Get
            Return pType
        End Get
        Set(ByVal Value As String)
            pType = Value
        End Set
    End Property

    Public Property TypeConverter() As String
        Get
            Return pTypeConverter
        End Get
        Set(ByVal Value As String)
            pTypeConverter = Value
        End Set
    End Property

    Public Sub Init(ByVal name As String, ByVal type As Type, ByVal category As String, ByVal description As String, ByVal defaultValue As Object, ByVal editor As Type, ByVal typeConverter As Type)
        Dim typeString As String = Nothing
        If type IsNot Nothing Then
            typeString = type.AssemblyQualifiedName
        End If

        Dim editorString As String = Nothing
        If editor IsNot Nothing Then
            editorString = editor.AssemblyQualifiedName
        End If

        Dim typeConverterString As String = Nothing
        If typeConverter IsNot Nothing Then
            typeConverterString = typeConverter.AssemblyQualifiedName
        End If

        Init(name, typeString, category, description, defaultValue, editorString, typeConverterString)
    End Sub

    Public Sub Init(ByVal name As String, ByVal type As String, ByVal category As String, ByVal description As String, ByVal defaultValue As Object, ByVal editor As String, ByVal typeConverter As String)
        Me.Name = name
        Me.Type = type
        Me.Category = category
        Me.Description = description
        Me.DefaultValue = defaultValue
        Me.Attributes = Nothing
        EditorType = editor
        Me.TypeConverter = typeConverter
    End Sub

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Dim newPropParm As PropParm = PropParm.GetInstance

        newPropParm.Name = pName
        newPropParm.Type = pType
        newPropParm.Category = pCategory
        newPropParm.Description = pDescription
        newPropParm.DefaultValue = pDefaultValue
        newPropParm.EditorType = pEditor
        newPropParm.TypeConverter = pTypeConverter

        If pAttributes IsNot Nothing AndAlso pAttributes.Length > 0 Then
            Dim attributes(pAttributes.Length - 1) As Attribute
            Dim ix As Int32 = 0
            For Each attribute As Attribute In pAttributes
                attributes(ix) = attribute
                ix += 1
            Next
            newPropParm.Attributes = attributes
        End If

        Return newPropParm
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
