#Region "Imports"
Imports System.ComponentModel
Imports System.Drawing.Design
Imports ScopeIII.Config.DelegateSigs
#End Region

''' -----------------------------------------------------------------------------
''' Project	 : Config
''' Class	 : Config.PropContainer
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' To use:
''' 1. Write a class that encapsulates PropContainer, or inherits PropContainer, then create an instance.
''' 2. Set the property grid's selected object to the instance's PropValues, ie,
''' Dim PGClass As PGClass (encapsultes a PropValues instance) = PGClass.GetInstance
''' propGrid.SelectedObject = PGClass.PropValues
''' 
''' When a property is set, a PropValues is instantiated and filled.
''' 1. a PropParm is instantiated and .Init called using the property's key (use its name);
'''    the PropParm simply holds the properites: Attribute, Category, DefaultValue, Description, Editor, Name, Type, TypeConverter
''' 2. the PropParm is added to the PropValues.Properties (PropContainer.[get_]Properties returns the properties
'''    and PropContainer.PropParmCollection.Add adds the PropParm)
''' 3. the PropParm's value is set: PropValues(key) = value; (PropValues.[set_]Item stores key and value)
''' 
''' PropValues inherits from PropContainer, and provides a hashtable to store the values, 
''' along with OnSetValue and OnGetValue event handlers that get/set the particular values.
''' 
''' PropContainer is a busy class, Implementing ICustomTypeDescriptor, and containing 2 inner classes:
''' Serializable PropParmCollection and private PropParmDescriptor.
''' 
''' PropContainer has 2 events: Get/SetValue As PropParmEventHandler.
''' Events from the property grid start in the PropContainer.PropParmDescriptor.OnGet/SetValue methods, 
''' which call PropValues.OnGet/SetValues (via a MyBase. overriding call), where values are pulled/inserted 
''' into the hashtable and the events raised.  The raised set event is to be handled in the PGClass, 
''' which updates the value.  Depending on if the value is being observed, another event may be raised 
''' or an observer notifed.
''' 
''' Why ICustomTypeDescriptor?  "ICustomTypeDescriptor allows an object to provide type information about itself. 
''' Typically, this interface is used when an object needs dynamic type information."
''' TypeDescriptor.GetProperties is intercepted and properties substituted as found in Properties (PropParmCollection).
''' This is done for each PropParm and a single new PropertyDescriptorCollection is returned encompassing all PropParm's.
''' 
''' PropParmCollection, internal class of PropContainer, implements IList (which implements ICollection), 
''' storing PropParms in an internal array. PropParmCollection needs to be serializable.  PropParmCollection
''' is used by PropContainer's public property Properties.
''' 
''' PropParmDescriptor, the other internal class of PropContainer, inherits PropertyDescriptor, 
''' has its own private PropContainer and PropParm.  
''' 
''' These two private instances are loaded through the constructor's parameters.  
''' Private PropContainer is used by PropParmDescriptor to call OnSet/GetValue.
''' Private PropParmDescriptor is used by PropContainer's GetDefaultProperty and GetProperties.
''' 
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[MBartels]	7/4/2005	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class PropContainer
    Inherits SettingsBase
    Implements ICustomTypeDescriptor

#Region "Inner Classes"
    ''' -----------------------------------------------------------------------------
    ''' Project	 : Config
    ''' Class	 : Config.PropContainer.PropParmCollection
    ''' 
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Save property parameters collection to private ArrayList
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[MBartels]	6/29/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    <Serializable()> Public Class PropParmCollection
        Implements IList
        Private pArray As ArrayList

        Private Sub New()
            pArray = New ArrayList
        End Sub

        Public Shared Function GetInstance() As PropParmCollection
            Return New PropParmCollection
        End Function

        Public ReadOnly Property Count() As Integer Implements IList.Count
            Get
                Return pArray.Count
            End Get
        End Property

        Public ReadOnly Property IsFixedSize() As Boolean Implements IList.IsFixedSize
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property IsReadOnly() As Boolean Implements IList.IsReadOnly
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property IsSynchronized() As Boolean Implements IList.IsSynchronized
            Get
                Return False
            End Get
        End Property

        Default Public Property Item(ByVal index As Integer) As Object Implements IList.Item
            Get
                Return CType(pArray(index), PropParm)
            End Get
            Set(ByVal Value As Object)
                pArray(index) = Value
            End Set
        End Property

        Public Sub Clear() Implements IList.Clear
            pArray.Clear()
        End Sub

        Public Function GetEnumerator() As IEnumerator Implements IList.GetEnumerator
            Return pArray.GetEnumerator()
        End Function

        Public Sub RemoveAt(ByVal index As Integer) Implements IList.RemoveAt
            pArray.RemoveAt(index)
        End Sub

        Function Add(ByVal value As Object) As Integer Implements IList.Add
            Return Add(CType(value, PropParm))
        End Function

        Overloads Function Contains(ByVal obj As Object) As Boolean Implements IList.Contains
            Return Contains(CType(obj, PropParm))
        End Function

        Overloads Function IndexOf(ByVal obj As Object) As Integer Implements IList.IndexOf
            Return IndexOf(CType(obj, PropParm))
        End Function

        Sub Insert(ByVal index As Integer, ByVal value As Object) Implements IList.Insert
            Insert(index, CType(value, PropParm))
        End Sub

        Overloads Sub Remove(ByVal value As Object) Implements IList.Remove
            Remove(CType(value, PropParm))
        End Sub

        Overloads Sub CopyTo(ByVal array As Array, ByVal index As Integer) Implements ICollection.CopyTo
            CopyTo(CType(array, PropParm()), index)
        End Sub

        ReadOnly Property SyncRoot() As Object Implements ICollection.SyncRoot
            Get
                Return Nothing
            End Get
        End Property

        Public Function Add(ByVal value As PropParm) As Integer
            Return pArray.Add(value)
        End Function

        Public Sub AddRange(ByVal array() As PropParm)
            pArray.AddRange(array)
        End Sub

        Public Overloads Function Contains(ByVal item As PropParm) As Boolean
            Return pArray.Contains(item)
        End Function

        Public Overloads Function Contains(ByVal name As String) As Boolean
            Dim spec As PropParm
            For Each spec In pArray
                If spec.Name = name Then
                    Return True
                End If
            Next spec
            Return False
        End Function

        Public Overloads Sub CopyTo(ByVal array() As PropParm)
            pArray.CopyTo(array)
        End Sub

        Public Overloads Sub CopyTo(ByVal array() As PropParm, ByVal index As Integer)
            pArray.CopyTo(array, index)
        End Sub

        Public Overloads Function IndexOf(ByVal value As PropParm) As Integer
            Return pArray.IndexOf(value)
        End Function

        Public Overloads Function IndexOf(ByVal name As String) As Integer
            Dim ix As Integer = 0

            Dim spec As PropParm
            For Each spec In pArray
                If spec.Name = name Then
                    Return ix
                End If
                ix += 1
            Next spec

            Return -1
        End Function

        Public Sub Insert(ByVal index As Integer, ByVal value As PropParm)
            pArray.Insert(index, value)
        End Sub

        Public Overloads Sub Remove(ByVal obj As PropParm)
            pArray.Remove(obj)
        End Sub

        Public Overloads Sub Remove(ByVal name As String)
            RemoveAt(IndexOf(name))
        End Sub

        Public Function ToArray() As PropParm()
            Return CType(pArray.ToArray(GetType(PropParm)), PropParm())
        End Function

        Property IList(ByVal index As Integer) As Object
            Get
                Return CType(Me, PropParmCollection)(index)
            End Get
            Set(ByVal Value As Object)
                CType(Me, PropParmCollection)(index) = CType(Value, PropParm)
            End Set
        End Property
    End Class

    Private Class PropParmDescriptor
        Inherits PropertyDescriptor

        Private pPropContainer As PropContainer
        Private pPropParm As PropParm

        ' base class PropertyDescriptor constructor requires parameters
        Public Sub New(ByVal item As PropParm, ByVal propContainer As PropContainer, ByVal name As String, ByVal attrs() As Attribute)
            MyBase.New(name, attrs)
            pPropContainer = propContainer
            pPropParm = item
        End Sub

        Public Overrides ReadOnly Property ComponentType() As Type
            Get
                Return pPropParm.GetType()
            End Get
        End Property

        Public Overrides ReadOnly Property IsReadOnly() As Boolean
            Get
                Return Attributes.Matches(ReadOnlyAttribute.Yes)
            End Get
        End Property

        Public Overrides ReadOnly Property PropertyType() As Type
            Get
                Return Type.GetType(pPropParm.Type)
            End Get
        End Property

        Public Overrides Function CanResetValue(ByVal component As Object) As Boolean
            If pPropParm.DefaultValue Is Nothing Then
                Return False
            Else
                Return Not Me.GetValue(component).Equals(pPropParm.DefaultValue)
            End If
        End Function

        ' raise event to get property's current value
        Public Overrides Function GetValue(ByVal component As Object) As Object
            Dim e As PropParmEventArgs = PropParmEventArgs.GetInstance
            e.Init(pPropParm, Nothing)
            pPropContainer.OnGetValue(e)
            Return e.Value
        End Function

        Public Overrides Sub ResetValue(ByVal component As Object)
            SetValue(component, pPropParm.DefaultValue)
        End Sub

        ' raise event to set property's current value
        Public Overrides Sub SetValue(ByVal component As Object, ByVal value As Object)
            Dim e As PropParmEventArgs = PropParmEventArgs.GetInstance
            e.Init(pPropParm, value)
            pPropContainer.OnSetValue(e)
        End Sub

        Public Overrides Function ShouldSerializeValue(ByVal component As Object) As Boolean
            Dim val As Object = Me.GetValue(component)

            If pPropParm.DefaultValue Is Nothing And val Is Nothing Then
                Return False
            Else
                Return Not val.Equals(pPropParm.DefaultValue)
            End If
        End Function
    End Class
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Event GetValue As DelegateObjPropParmEventArgs
    Public Event SetValue As DelegateObjPropParmEventArgs
#End Region

#Region "Private and Protected Members"
    Private pDefaultProperty As String
    Private pProperties As PropParmCollection
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As PropContainer
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As PropContainer = New PropContainer
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        SetToDefaults()
    End Sub

    Public Shared Function GetInstance() As PropContainer
        Return New PropContainer
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property DefaultProperty() As String
        Get
            Return pDefaultProperty
        End Get
        Set(ByVal Value As String)
            pDefaultProperty = Value
        End Set
    End Property

    Public ReadOnly Property Properties() As PropParmCollection
        Get
            Return pProperties
        End Get
    End Property

    Public Overrides Sub SetToDefaults()
        pDefaultProperty = Nothing
        pProperties = PropParmCollection.GetInstance
    End Sub

    Public Overrides Sub CopyPropertiesTo(ByRef ISettings As BartelsLibrary.ISettings)
        Dim newPropContainer As PropContainer = CType(ISettings, PropContainer)

        newPropContainer.Name = Name
        For Each oldPropParm As PropParm In pProperties
            Dim newPropParm As PropParm = CType(oldPropParm.Clone, PropParm)
            newPropContainer.Properties.Add(newPropParm)
        Next
        'copy over the original's event handlers for the newly cloned property container
        'AddHandler newPropContainer.GetValue, GetValueEvent
        'AddHandler newPropContainer.SetValue, SetValueEvent
    End Sub

    Public Overrides Function Clone() As Object
        Dim newPropContainer As ISettings = PropContainer.GetInstance
        CopyPropertiesTo(newPropContainer)
        Return newPropContainer
    End Function

    Public Overrides Function PropertiesSet() As Boolean
        Return pProperties IsNot Nothing
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Overridable Sub OnGetValue(ByVal e As PropParmEventArgs)
        RaiseEvent GetValue(Me, e)
    End Sub

    Protected Overridable Sub OnSetValue(ByVal e As PropParmEventArgs)
        RaiseEvent SetValue(Me, e)
    End Sub

    ' following methods Implements ICustomTypeDescriptor methods

    ' TypeDescriptor provides information about the properties and events for a component.
    ' Methods can take the type of class or an instance of an object.
    ' All methods of TypeDescriptor are shared.

    Protected Function GetAttributes() As AttributeCollection Implements ICustomTypeDescriptor.GetAttributes
        Return TypeDescriptor.GetAttributes(Me, True)
    End Function

    Protected Function GetClassName() As String Implements ICustomTypeDescriptor.GetClassName
        Return TypeDescriptor.GetClassName(Me, True)
    End Function

    Protected Function GetComponentName() As String Implements ICustomTypeDescriptor.GetComponentName
        Return TypeDescriptor.GetComponentName(Me, True)
    End Function

    Protected Function GetConverter() As TypeConverter Implements ICustomTypeDescriptor.GetConverter
        Return TypeDescriptor.GetConverter(Me, True)
    End Function

    Protected Function GetDefaultEvent() As EventDescriptor Implements ICustomTypeDescriptor.GetDefaultEvent
        Return TypeDescriptor.GetDefaultEvent(Me, True)
    End Function

    ' search Properties for property w/ same name as defaultProperty, and, if found, return the descriptor
    Protected Function GetDefaultProperty() As PropertyDescriptor Implements ICustomTypeDescriptor.GetDefaultProperty
        Dim PropParm As PropParm = Nothing
        If DefaultProperty IsNot Nothing Then
            Dim index As Integer = Properties.IndexOf(DefaultProperty)
            PropParm = CType(Properties(index), PropParm)
        End If

        If PropParm IsNot Nothing Then
            Return New PropParmDescriptor(PropParm, Me, PropParm.Name, Nothing)
        Else
            Return Nothing
        End If
    End Function

    Protected Function GetEditor(ByVal editorBaseType As Type) As Object Implements ICustomTypeDescriptor.GetEditor
        Return TypeDescriptor.GetEditor(Me, editorBaseType, True)
    End Function

    Protected Overloads Function GetEvents() As EventDescriptorCollection Implements ICustomTypeDescriptor.GetEvents
        Return TypeDescriptor.GetEvents(Me, True)
    End Function

    Protected Overloads Function GetEvents(ByVal attributes() As Attribute) As EventDescriptorCollection Implements ICustomTypeDescriptor.GetEvents
        Return TypeDescriptor.GetEvents(Me, attributes, True)
    End Function

    Protected Overloads Function GetProperties() As PropertyDescriptorCollection Implements ICustomTypeDescriptor.GetProperties
        Return CType(Me, ICustomTypeDescriptor).GetProperties(New Attribute(0) {})
    End Function

    ' intercept TypeDescriptor.GetProperties and substitute properties as found in Properties
    Protected Overloads Function GetProperties(ByVal attributes() As Attribute) As PropertyDescriptorCollection Implements ICustomTypeDescriptor.GetProperties
        Dim props As New ArrayList

        Dim lproperty As PropParm
        For Each lproperty In Properties
            Dim attrs As New ArrayList

            If lproperty.Category IsNot Nothing Then
                attrs.Add(New CategoryAttribute(lproperty.Category))
            End If
            If lproperty.Description IsNot Nothing Then
                attrs.Add(New DescriptionAttribute(lproperty.Description))
            End If
            If lproperty.EditorType IsNot Nothing Then
                attrs.Add(New EditorAttribute(lproperty.EditorType, GetType(UITypeEditor)))
            End If
            If lproperty.TypeConverter IsNot Nothing Then
                attrs.Add(New TypeConverterAttribute(lproperty.TypeConverter))
            End If
            ' add array of custom attributes
            If lproperty.Attributes IsNot Nothing Then
                attrs.AddRange(lproperty.Attributes)
            End If
            Dim attrArray As Attribute() = CType(attrs.ToArray(GetType(Attribute)), Attribute())

            ' add new property descriptor for the property and add 
            Dim pd As New PropParmDescriptor(lproperty, Me, lproperty.Name, attrArray)
            props.Add(pd)
        Next lproperty

        ' change PropertyDescriptors for ICustomTypeDescriptor's purposes
        Dim propArray As PropertyDescriptor() = CType(props.ToArray(GetType(PropertyDescriptor)), PropertyDescriptor())
        Return New PropertyDescriptorCollection(propArray)
    End Function

    Protected Function GetPropertyOwner(ByVal pd As PropertyDescriptor) As Object Implements ICustomTypeDescriptor.GetPropertyOwner
        Return Me
    End Function
#End Region

End Class
