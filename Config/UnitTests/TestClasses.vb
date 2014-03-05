Imports System.ComponentModel
Imports System.Drawing

Public Class TestParentClass
    Dim pIx As Int32
    Dim pCc As ChildClass
    Dim pCc2 As ChildClass2
    Dim pCc3 As ChildClass3
    Dim pCca() As ChildClass
    Dim pOa() As Object
    Dim pEditableString As String
    Dim pSa() As String
    Public Property Ix() As Int32
        Get
            Return pIx
        End Get
        Set(ByVal Value As Int32)
            pIx = Value
        End Set
    End Property
    <TypeConverter(GetType(ChildClassConverter))> _
    Public Property ChildClass() As ChildClass
        Get
            Return pCc
        End Get
        Set(ByVal Value As ChildClass)
            pCc = Value
        End Set
    End Property
    Public Property ChildClass2() As ChildClass2
        Get
            Return pCc2
        End Get
        Set(ByVal Value As ChildClass2)
            pCc2 = Value
        End Set
    End Property
    <TypeConverter(GetType(ChildClass3Converter))> _
    Public Property ChildClass3() As ChildClass3
        Get
            Return pCc3
        End Get
        Set(ByVal Value As ChildClass3)
            pCc3 = Value
        End Set
    End Property
    <TypeConverter(GetType(ChildrenConverter))> _
    Public Property Children() As ChildClass()
        Get
            Return pCca
        End Get
        Set(ByVal Value As ChildClass())
            pCca = Value
        End Set
    End Property
    Public Property ObjectArray() As Object()
        Get
            Return pOa
        End Get
        Set(ByVal Value As Object())
            pOa = Value
        End Set
    End Property
    <TypeConverter(GetType(List))> _
    Public Property EditableString() As String
        Get
            Return pEditableString
        End Get
        Set(ByVal Value As String)
            pEditableString = Value
        End Set
    End Property
    Public Property StringArray() As String()
        Get
            Return pSa
        End Get
        Set(ByVal Value As String())
            pSa = Value
        End Set
    End Property
    Public Sub New()
        pCc = New ChildClass
        pCc2 = New ChildClass2
        pCc3 = New ChildClass3
        pCca = New ChildClass() {New ChildClass, New ChildClass, New ChildClass}
        pOa = New Object() {New ChildClass, New ChildClass}
        pSa = New String() {"one", "two", "three"}
    End Sub
End Class

<DefaultPropertyAttribute("Num")> Public Class ChildClass
    Dim pNum As Int32
    Public Property Num() As Int32
        Get
            Return pNum
        End Get
        Set(ByVal Value As Int32)
            pNum = Value
        End Set
    End Property
    Dim pDbl As Double
    Public Property Dbl() As Double
        Get
            Return pDbl
        End Get
        Set(ByVal Value As Double)
            pDbl = Value
        End Set
    End Property
    Public Sub New()
    End Sub
End Class

<Editor(GetType(TestEditor), GetType(Drawing.Design.UITypeEditor)), TypeConverter(GetType(ExpandableObjectConverter))> _
Public Class ChildClass2
    Inherits Drawing.Design.UITypeEditor
    Dim pNum As Int32
    <Editor(GetType(TestEditor), GetType(Drawing.Design.UITypeEditor))> _
    Public Property Num() As Int32
        Get
            Return pNum
        End Get
        Set(ByVal Value As Int32)
            pNum = Value
        End Set
    End Property
    Dim pDbl As Double
    Public Property Dbl() As Double
        Get
            Return pDbl
        End Get
        Set(ByVal Value As Double)
            pDbl = Value
        End Set
    End Property
    Public Sub New()
    End Sub
End Class

Public Class ChildClass3
    Private pNum As Int32
    Public Property Num() As Int32
        Get
            Return pNum
        End Get
        Set(ByVal Value As Int32)
            pNum = Value
        End Set
    End Property
    Private pChildClass As ChildClass
    <TypeConverter(GetType(ChildClassConverter))> _
    Public Property ChildClass() As ChildClass
        Get
            Return pChildClass
        End Get
        Set(ByVal Value As ChildClass)
            pChildClass = Value
        End Set
    End Property
    Public Sub New()
        pChildClass = New ChildClass
    End Sub
End Class

Public Class TestEditor
    Inherits Drawing.Design.UITypeEditor
    Public Overloads Overrides Function GetPaintValueSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function
    Public Overloads Overrides Sub PaintValue(ByVal e As System.Drawing.Design.PaintValueEventArgs)
        e.Graphics.DrawIcon(New Icon("..\ScopeIIIsmall.ico"), 1, 1)
    End Sub
End Class

' To display custom data type with collapsible list, use a TypeConverter that converts
' between object and string.  Use it with the property definition of the object, ie, 
' <TypeConverter(GetType(ChildClassConverter))> Public Property ChildClass() As ChildClass...
Public Class ChildClassConverter
    Inherits ExpandableObjectConverter
    Public Overloads Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
        If (destinationType Is GetType(ChildClass)) Then
            Return True
        End If
        Return MyBase.CanConvertFrom(context, destinationType)
    End Function
    Public Overloads Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
        If (sourceType Is GetType(String)) Then
            Return True
        End If
        Return MyBase.CanConvertFrom(context, sourceType)
    End Function
    Public Overloads Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
        If TypeOf value Is String Then
            Try
                Dim s As String = CStr(value)
                Dim stringParts() As String = Split(s, ",")
                If stringParts IsNot Nothing Then
                    Dim pChildClass As New ChildClass
                    pChildClass.Num = CType(stringParts(0), Int32)
                    pChildClass.Dbl = CDbl(stringParts(1))
                    Return pChildClass
                End If
            Catch ex As Exception
                DebugTrace.WriteLine(ex.Message & ex.StackTrace)
            End Try
        End If
        Return MyBase.ConvertFrom(context, culture, value)
    End Function
    Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
        If (destinationType Is GetType(System.String) AndAlso TypeOf value Is ChildClass) Then
            Dim pChildClass As ChildClass = CType(value, ChildClass)
            Return pChildClass.Num.ToString & "," & pChildClass.Dbl.ToString
        End If
        Return MyBase.ConvertTo(context, culture, value, destinationType)
    End Function
End Class

Public Class ChildClass3Converter
    Inherits ExpandableObjectConverter
    Public Overloads Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
        If (destinationType Is GetType(ChildClass3)) Then
            Return True
        End If
        Return MyBase.CanConvertFrom(context, destinationType)
    End Function
    Public Overloads Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
        If (sourceType Is GetType(String)) Then
            Return True
        End If
        Return MyBase.CanConvertFrom(context, sourceType)
    End Function
    Public Overloads Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
        If TypeOf value Is String Then
            Try
                Dim s As String = CStr(value)
                Dim stringParts() As String = Split(s, ",")
                If stringParts IsNot Nothing Then
                    Dim pChildClass3 As New ChildClass3
                    pChildClass3.Num = CType(stringParts(0), Int32)
                    pChildClass3.ChildClass.Num = CType(stringParts(1), Int32)
                    pChildClass3.ChildClass.Dbl = CDbl(stringParts(2))
                    Return pChildClass3
                End If
            Catch ex As Exception
                DebugTrace.WriteLine(ex.Message & ex.StackTrace)
            End Try
        End If
        Return MyBase.ConvertFrom(context, culture, value)
    End Function
    Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
        If (destinationType Is GetType(System.String) AndAlso TypeOf value Is ChildClass3) Then
            Dim pChildClass3 As ChildClass3 = CType(value, ChildClass3)
            Return pChildClass3.Num & ", " & pChildClass3.ChildClass.Num & ", " & pChildClass3.ChildClass.Dbl
        End If
        Return MyBase.ConvertTo(context, culture, value, destinationType)
    End Function
End Class

Public Class ChildrenConverter
    Inherits ExpandableObjectConverter
    Public Overloads Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
        If (destinationType Is GetType(ChildClass())) Then
            Return True
        End If
        Return MyBase.CanConvertFrom(context, destinationType)
    End Function
    Public Overloads Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
        If (sourceType Is GetType(String)) Then
            Return True
        End If
        Return MyBase.CanConvertFrom(context, sourceType)
    End Function
    Public Overloads Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
        Dim childClass() As ChildClass
        If TypeOf value Is String Then
            Try
                Dim st As StringTokenizer = StringTokenizer.GetInstance
                st.Tokenize(CStr(value), ",".ToCharArray)
                ReDim childClass(st.GetCountDoubles - 1)
                Dim ix As Int32
                For ix = 0 To st.GetCountDoubles - 1
                    Dim pChildClass As New ChildClass
                    pChildClass.Dbl = st.GetNextDouble
                    childClass(ix) = pChildClass
                Next
                Return childClass
            Catch ex As Exception
                DebugTrace.WriteLine(ex.Message & ex.StackTrace)
            End Try
        End If
        Return MyBase.ConvertFrom(context, culture, value)
    End Function
    Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
        If (destinationType Is GetType(System.String) AndAlso TypeOf value Is ChildClass()) Then
            Dim sb As New System.Text.StringBuilder
            Dim pChildClass() As ChildClass = CType(value, ChildClass())
            For Each cc As ChildClass In pChildClass
                sb.Append(cc.Dbl.ToString)
                sb.Append(",")
            Next
            Return sb.ToString
        End If
        Return MyBase.ConvertTo(context, culture, value, destinationType)
    End Function
End Class

Public Class List
    Inherits System.ComponentModel.StringConverter
    Dim pList() As String
    Public Sub New()
        pList = New String() {"item1", "item2", "item3"}
    End Sub
    Public Overloads Overrides Function GetStandardValues(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.ComponentModel.TypeConverter.StandardValuesCollection
        Return New StandardValuesCollection(pList)
    End Function
    ' no drop down w/o this
    Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function
    ' false == editable
    Public Overloads Overrides Function GetStandardValuesExclusive(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return False
    End Function
End Class

Public Class TestPropContainer
    Dim pPropContainer As PropContainer
    Public Property PropContainer() As PropContainer
        Get
            Return pPropContainer
        End Get
        Set(ByVal Value As PropContainer)
            pPropContainer = Value
        End Set
    End Property
    Dim pPropValues As PropValues
    Public Property PropValues() As PropValues
        Get
            Return pPropValues
        End Get
        Set(ByVal Value As PropValues)
            pPropValues = Value
        End Set
    End Property
    Public Enum Flavor
        Vanilla
        Chocolate
        Strawberry
        Peach
        Marionberry
    End Enum
    Public PropContainerFlavor As Flavor = Flavor.Strawberry
    Public PropContainerNum As Int32 = 22
    Public propContainerBoolean As Boolean = True

    <Serializable()> Public Class PU
        Public Product As String
        Public UnitSize As Int32
    End Class

    Public Sub New()
        ' PropContainer test

        pPropContainer = Config.PropContainer.GetInstance

        AddHandler pPropContainer.GetValue, AddressOf thisGetValue
        AddHandler pPropContainer.SetValue, AddressOf thisSetValue

        Dim propParm As PropParm = Config.PropParm.GetInstance
        propParm.Init("Flavor", GetType(Flavor), Nothing, Nothing, Flavor.Peach, Nothing, Nothing)
        pPropContainer.Properties.Add(propParm)

        propParm = Config.PropParm.GetInstance
        propParm.Init("Number", GetType(Int32), "Numbers", "An Int32", 44, Nothing, Nothing)
        pPropContainer.Properties.Add(propParm)

        propParm = Config.PropParm.GetInstance
        propParm.Init("Boolean", GetType(Boolean), "Boolean", "A boolean", True, Nothing, GetType(BooleanConverter))
        pPropContainer.Properties.Add(propParm)

        ' PropValue test

        pPropValues = Config.PropValues.GetInstance

        propParm = Config.PropParm.GetInstance
        propParm.Init("Flavor", GetType(Flavor), Nothing, Nothing, Flavor.Strawberry, Nothing, Nothing)
        pPropValues.Properties.Add(propParm)

        propParm = Config.PropParm.GetInstance
        propParm.Init("Picture", GetType(Image), "Icon Category", "This is a sample description.", Nothing, Nothing, Nothing)
        pPropValues.Properties.Add(propParm)

        propParm = Config.PropParm.GetInstance
        propParm.Init("Typeface", GetType(Font), "Another Category", Nothing, New Font("Tahoma", 8.25F), Nothing, Nothing)
        pPropValues.Properties.Add(propParm)

        propParm = Config.PropParm.GetInstance
        Dim editorTypeString As String = GetType(TestEditor).FullName
        propParm.Init("Icon Boolean", "System.Boolean", "Icon Category", Nothing, False, editorTypeString, Nothing)
        pPropValues.Properties.Add(propParm)

        propParm = Config.PropParm.GetInstance
        propParm.Init("Number", "System.Int64", Nothing, "A big number.", 1234567890L, Nothing, Nothing)
        pPropValues.Properties.Add(propParm)

        ' add more attributes
        propParm = Config.PropParm.GetInstance
        propParm.Init("Readonly", GetType(String), Nothing, "This property is read-only.", "Readonly string value", Nothing, Nothing)
        Dim attr() As Attribute = {ReadOnlyAttribute.Yes}
        propParm.Attributes = attr
        pPropValues.Properties.Add(propParm)

        ' must set values
        pPropValues("Flavor") = Flavor.Vanilla
        pPropValues("Picture") = Nothing
        pPropValues("Typeface") = New Font("Times New Roman", 12.0F)
        pPropValues("Icon Boolean") = True
        pPropValues("Number") = 1234567890L
        pPropValues("Readonly") = "Readonly string value"
        Dim pus(3) As PU
        Dim ix As Int32
        For ix = 0 To pus.Length - 1
            pus(ix) = New PU
            pus(ix).Product = "product " & ix
            pus(ix).UnitSize = ix * 10
            Dim s As String
            s = ix & "'s unit size"
            propParm = Config.PropParm.GetInstance
            propParm.Init(s, GetType(Int32), "products", Nothing, Nothing, Nothing, Nothing)
            pPropValues.Properties.Add(propParm)
            pPropValues(s) = pus(ix).UnitSize
        Next
    End Sub
    ' ideally get/set from storage
    Private Sub thisGetValue(ByVal sender As Object, ByVal e As PropParmEventArgs)
        Select Case e.Property.Name
            Case "Flavor"
                e.Value = PropContainerFlavor
            Case "Number"
                e.Value = PropContainerNum
            Case "Boolean"
                e.Value = propContainerBoolean
        End Select
    End Sub
    Private Sub thisSetValue(ByVal sender As Object, ByVal e As PropParmEventArgs)
        Select Case e.Property.Name
            Case "Flavor"
                PropContainerFlavor = CType(e.Value, Flavor)
            Case "Number"
                PropContainerNum = CType(e.Value, Int32)
            Case "Boolean"
                propContainerBoolean = CBool(e.Value)
        End Select
    End Sub
End Class

Public Class TestPropContainer2
    Public Property PropContainer() As PropContainer
        Get
            Return pPropContainer
        End Get
        Set(ByVal Value As PropContainer)
            pPropContainer = Value
        End Set
    End Property
    'Public Property Number() As Int32
    '    Get
    '        Return pNumber
    '    End Get
    '    Set(ByVal Value As Int32)
    '        pNumber = Value
    '    End Set
    'End Property
    'Public Property Number2() As Int32
    '    Get
    '        Return pNumber2
    '    End Get
    '    Set(ByVal Value As Int32)
    '        pNumber2 = Value
    '    End Set
    'End Property
    Private pPropContainer As PropContainer
    Private pNumber As Int32
    Private pNumber2 As Int32
    Private pTestChildPropContainer As TestChildPropContainer
    Public Sub New()
        pPropContainer = Config.PropContainer.GetInstance
        AddHandler pPropContainer.GetValue, AddressOf thisGetValue
        AddHandler pPropContainer.SetValue, AddressOf thisSetValue

        ' instantiate for get/set value subs
        pTestChildPropContainer = New TestChildPropContainer

        Dim propParm As PropParm = Config.PropParm.GetInstance
        propParm.Init("Number", GetType(System.Int32), Nothing, Nothing, Nothing, Nothing, Nothing)
        pPropContainer.Properties.Add(propParm)

        propParm = Config.PropParm.GetInstance
        propParm.Init("Number2", GetType(System.Int32), Nothing, Nothing, Nothing, Nothing, Nothing)
        pPropContainer.Properties.Add(propParm)

        propParm = Config.PropParm.GetInstance
        propParm.Init("TestChildPropContainer", GetType(System.String), Nothing, Nothing, Nothing, Nothing, GetType(TestChildPropContainerConverter))
        pPropContainer.Properties.Add(propParm)

        propParm = Config.PropParm.GetInstance
        propParm.Init("PropContainer", GetType(PropContainer), Nothing, Nothing, Nothing, Nothing, GetType(PropContainerConverter))
        pPropContainer.Properties.Add(propParm)
    End Sub
    ' ideally get/set from storage
    Private Sub thisGetValue(ByVal sender As Object, ByVal e As PropParmEventArgs)
        Select Case e.Property.Name
            Case "Number"
                e.Value = pNumber
            Case "Number2"
                e.Value = pNumber2
            Case "TestChildPropContainer"
                e.Value = pTestChildPropContainer
            Case "PropContainer"
                e.Value = pTestChildPropContainer
        End Select
    End Sub
    Private Sub thisSetValue(ByVal sender As Object, ByVal e As PropParmEventArgs)
        Select Case e.Property.Name
            Case "Number"
                pNumber = CType(e.Value, Int32)
            Case "Number2"
                pNumber2 = CType(e.Value, Int32)
            Case "TestChildPropContainer"
                pTestChildPropContainer = CType(e.Value, TestChildPropContainer)
            Case "PropContainer"
                pTestChildPropContainer = CType(e.Value, TestChildPropContainer)
        End Select
    End Sub
End Class

Public Class TestChildPropContainerConverter
    Inherits ExpandableObjectConverter
    Public Overloads Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
        If (destinationType Is GetType(TestChildPropContainer())) Then
            Return True
        End If
        Return MyBase.CanConvertFrom(context, destinationType)
    End Function
    Public Overloads Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
        If (sourceType Is GetType(String)) Then
            Return True
        End If
        Return MyBase.CanConvertFrom(context, sourceType)
    End Function
    Public Overloads Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
        Return String.Empty
        'Return MyBase.ConvertFrom(context, culture, value)
    End Function
    Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
        Return String.Empty
        'Return MyBase.ConvertTo(context, culture, value, destinationType)
    End Function
End Class

Public Class PropContainerConverter
    Inherits ExpandableObjectConverter
    Public Overloads Overrides Function CanConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal destinationType As System.Type) As Boolean
        If (destinationType Is GetType(TestChildPropContainer())) Then
            Return False
        End If
        Return MyBase.CanConvertFrom(context, destinationType)
    End Function
    Public Overloads Overrides Function CanConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal sourceType As System.Type) As Boolean
        If (sourceType Is GetType(String)) Then
            Return False
        End If
        Return MyBase.CanConvertFrom(context, sourceType)
    End Function
    Public Overloads Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
        Return String.Empty
        'Return MyBase.ConvertFrom(context, culture, value)
    End Function
    Public Overloads Overrides Function ConvertTo(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object, ByVal destinationType As System.Type) As Object
        Return String.Empty
        'Return MyBase.ConvertTo(context, culture, value, destinationType)
    End Function
End Class

Public Class TestChildPropContainer
    <TypeConverter(GetType(PropContainerConverter))> _
    Public Property PropContainer() As PropContainer
        Get
            Return pPropContainer
        End Get
        Set(ByVal Value As PropContainer)
            pPropContainer = Value
        End Set
    End Property
    Public Property X() As String
        Get
            Return _x
        End Get
        Set(ByVal Value As String)
            _x = Value
        End Set
    End Property
    'Public Property Name() As String
    '    Get
    '        Return pName
    '    End Get
    '    Set(ByVal Value As String)
    '        pName = Value
    '    End Set
    'End Property
    'Public Property Name2() As String
    '    Get
    '        Return pName2
    '    End Get
    '    Set(ByVal Value As String)
    '        pName2 = Value
    '    End Set
    'End Property
    Private pPropContainer As PropContainer
    Private _x As String
    Private pName As String
    Private pName2 As String
    Public Sub New()
        pPropContainer = Config.PropContainer.GetInstance
        AddHandler pPropContainer.GetValue, AddressOf thisGetValue
        AddHandler pPropContainer.SetValue, AddressOf thisSetValue

        Dim propParm As PropParm = Config.PropParm.GetInstance
        propParm.Init("Name", GetType(System.String), Nothing, Nothing, Nothing, Nothing, Nothing)
        pPropContainer.Properties.Add(propParm)

        propParm = Config.PropParm.GetInstance
        propParm.Init("Name2", GetType(System.String), Nothing, Nothing, Nothing, Nothing, Nothing)
        pPropContainer.Properties.Add(propParm)
    End Sub
    ' ideally get/set from storage
    Private Sub thisGetValue(ByVal sender As Object, ByVal e As PropParmEventArgs)
        Select Case e.Property.Name
            Case "Name"
                e.Value = pName
            Case "Name2"
                e.Value = pName2
        End Select
    End Sub
    Private Sub thisSetValue(ByVal sender As Object, ByVal e As PropParmEventArgs)
        Select Case e.Property.Name
            Case "Name"
                pName = CStr(e.Value)
            Case "Name2"
                pName2 = CStr(e.Value)
        End Select
    End Sub
End Class

Public Class GroupTest
    Const MaxItems As Int32 = 2
    ' PG selectedObject set to this
    Public PropContainer As PropContainer = Config.PropContainer.GetInstance
    ' group of objects where one object at a time is displayed in the PG
    Public Group(MaxItems) As System.Windows.Forms.RadioButton
    Private pPropGridObj As TestPropGridObj
    Private pArrayTestPropGridObj(MaxItems) As TestPropGridObj

    Public Sub New()
        init()
        Group(0).Checked = True
    End Sub

    Private Sub getValue(ByVal sender As Object, ByVal e As PropParmEventArgs)
        Select Case e.Property.Name
            Case "TheTitle"
                e.Value = Me.pPropGridObj.Title
            Case "TheDescription"
                e.Value = Me.pPropGridObj.Description
            Case "TheID"
                e.Value = Me.pPropGridObj.ID
        End Select
    End Sub
    Private Sub setValue(ByVal sender As Object, ByVal e As PropParmEventArgs)
        Select Case e.Property.Name
            ' "TheID" is readonly 
            Case "TheTitle"
                Me.pPropGridObj.Title = CStr(e.Value)
            Case "TheDescription"
                Me.pPropGridObj.Description = CType(e.Value, MultiLineString)
        End Select
    End Sub

    Private Sub checkedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If CType(sender, System.Windows.Forms.RadioButton).Checked Then
            ' set pPropGridObj to obj in array so that when pPropGridObj changes, pArrayTestPropGridObj(sender.tag) will change also
            ' see above getValue/setValue
            Me.pPropGridObj = pArrayTestPropGridObj(CType(CType(sender, System.Windows.Forms.RadioButton).Tag, Int32))
        End If
    End Sub

    Private Sub init()
        Dim ix As Int32
        For ix = 0 To MaxItems
            ' Fill pArrayTestPropGridObj (normally with values from database)
            pArrayTestPropGridObj(ix) = New TestPropGridObj(ix)
            pArrayTestPropGridObj(ix).Title = "Title " & ix
            pArrayTestPropGridObj(ix).Description = New MultiLineString("line 1" & vbCrLf & "line2, number is " & ix)

            ' use a control to store values
            Group(ix) = New System.Windows.Forms.RadioButton
            Group(ix).Text = "Object " & ix
            ' store reference to object in pArrayTestPropGridObj()
            Group(ix).Tag = ix

            AddHandler Group(ix).CheckedChanged, AddressOf Me.checkedChanged
        Next

        AddHandler PropContainer.GetValue, AddressOf getValue
        AddHandler PropContainer.SetValue, AddressOf setValue

        Dim propParm As PropParm = Config.PropParm.GetInstance
        propParm.Init("TheID", GetType(Integer), "Normal", "The identifier of the object", Nothing, Nothing, Nothing)
        Dim a() As Attribute = {System.ComponentModel.ReadOnlyAttribute.Yes}
        propParm.Attributes = a
        Me.PropContainer.Properties.Add(propParm)

        propParm = Config.PropParm.GetInstance
        propParm.Init("TheTitle", GetType(String), "Normal", "This is the description of the title", Nothing, Nothing, Nothing)
        Me.PropContainer.Properties.Add(propParm)

        propParm = Config.PropParm.GetInstance
        propParm.Init("TheDescription", GetType(MultiLineString), "Custom", "This is a description of the object", Nothing, GetType(MultiLineStringEditor), GetType(MultiLineStringConverter))
        Me.PropContainer.Properties.Add(propParm)
    End Sub
End Class

Public Class TestPropGridObj
    Private pId As Integer
    Private pTitle As String
    Private pDesc As MultiLineString
    Private pExtra As MultiLineString
    Public Sub New(ByVal id As Integer)
        Me.pId = id
    End Sub
    Public ReadOnly Property ID() As Integer
        Get
            Return Me.pId
        End Get
    End Property
    Public Property Title() As String
        Get
            Return Me.pTitle
        End Get
        Set(ByVal Value As String)
            Me.pTitle = Value
        End Set
    End Property
    Public Property Description() As MultiLineString
        Get
            Return Me.pDesc
        End Get
        Set(ByVal Value As MultiLineString)
            Me.pDesc = Value
        End Set
    End Property
    Public Property Extra() As MultiLineString
        Get
            Return Me.pExtra
        End Get
        Set(ByVal Value As MultiLineString)
            Me.pExtra = Value
        End Set
    End Property
End Class

Public Class StrongFinalTypeTest
    Public Property StrongFinalTypeTestContainer() As PropContainer
        Get
            Return pPropContainer
        End Get
        Set(ByVal Value As PropContainer)
            pPropContainer = Value
        End Set
    End Property
    Private pPropContainer As PropContainer
    Private pSft1 As ISFT = SFTTest.GetInstance.FirstItem
    Private pSft2 As ISFT = SFTTest.GetInstance.FirstItem
    Public Sub New()
        pPropContainer = PropContainer.GetInstance
        AddHandler pPropContainer.GetValue, AddressOf thisGetValue
        AddHandler pPropContainer.SetValue, AddressOf thisSetValue

        Dim propParm As PropParm = Config.PropParm.GetInstance
        propParm.Init("enum", GetType(SFTTest.Names), "Test Category", "Strong Final Type", pSft1, Nothing, Nothing)
        pPropContainer.Properties.Add(propParm)

        propParm = Config.PropParm.GetInstance
        propParm.Init("SFT list", GetType(System.String), "Test Category", "String", pSft1.Name, Nothing, GetType(SFTList))
        pPropContainer.Properties.Add(propParm)
    End Sub
    ' ideally get/set from storage: set here by class loader that uses the default value above
    Protected Overridable Sub thisGetValue(ByVal sender As Object, ByVal e As PropParmEventArgs)
        If e.Property.Type.Equals(GetType(System.String).AssemblyQualifiedName) Then
            e.Value = pSft2.Name
        Else
            e.Value = pSft1.Key
        End If
    End Sub
    Private Sub thisSetValue(ByVal sender As Object, ByVal e As PropParmEventArgs)
        If e.Property.Type.Equals(GetType(System.String).AssemblyQualifiedName) Then
            pSft2 = pSft2.MatchString(CStr(e.Value))
        Else
            pSft1 = pSft1.MatchKey(CType(e.Value, Int32))
        End If
    End Sub
End Class

Public Class SFTList
    Inherits System.ComponentModel.StringConverter
    Private list As New Collection
    Public Sub New()
        Dim eSFTtest As IEnumerator = SFTTest.ISFT.Enumerator
        While eSFTtest.MoveNext
            list.Add(CType(eSFTtest.Current, ISFT).Name)
        End While
    End Sub
    Public Overloads Overrides Function GetStandardValues(ByVal context As System.ComponentModel.ITypeDescriptorContext) As System.ComponentModel.TypeConverter.StandardValuesCollection
        Return New StandardValuesCollection(list)
    End Function
    ' no drop down w/o this
    Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function
    ' false == editable
    Public Overloads Overrides Function GetStandardValuesExclusive(ByVal context As System.ComponentModel.ITypeDescriptorContext) As Boolean
        Return True
    End Function
End Class

Public Class StrongFinalTypeTest2
    Public Property StrongFinalTypeTestContainer() As PropContainer
        Get
            Return pPropContainer
        End Get
        Set(ByVal Value As PropContainer)
            pPropContainer = Value
        End Set
    End Property
    Private pPropContainer As PropContainer
    Private pValues As String
    Public Sub New()
        ' start with some values
        pValues = SFTTest.aaa.Name & "," & SFTTest.ccc.Name

        pPropContainer = PropContainer.GetInstance
        AddHandler pPropContainer.GetValue, AddressOf thisGetValue
        AddHandler pPropContainer.SetValue, AddressOf thisSetValue

        Dim propParm As PropParm = Config.PropParm.GetInstance
        propParm.Init("enum+LVedit", GetType(SFTTest.Names), "Test Category", "ListView", Nothing, GetType(ListViewTypeEditorSFTTest), GetType(StringConverter))
        pPropContainer.Properties.Add(propParm)
    End Sub
    ' ideally get/set from storage: set here by class loader that uses the default value above
    Protected Overridable Sub thisGetValue(ByVal sender As Object, ByVal e As PropParmEventArgs)
        e.Value = pValues
    End Sub
    Private Sub thisSetValue(ByVal sender As Object, ByVal e As PropParmEventArgs)
        pValues = CStr(e.Value)
    End Sub
End Class
