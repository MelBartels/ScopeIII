#Region "Imports"
#End Region

Public MustInherit Class DevPropToPropGridAdapterBase
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
    Protected pPropContainer As PropContainer
    Protected pPropParm As PropParm
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DevPropToPropGridAdapterBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DevPropToPropGridAdapterBase = New DevPropToPropGridAdapterBase
    'End Class
#End Region

#Region "Constructors"
    ' must be public for .NET to instantiate
    Protected Sub New()
    End Sub

    'Public Shared Function GetInstance() As DevPropToPropGridAdapterBase
    '    Return New DevPropToPropGridAdapterBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property PropContainer() As PropContainer
        Get
            Return pPropContainer
        End Get
        Set(ByVal Value As PropContainer)
            pPropContainer = Value
        End Set
    End Property

    Public MustOverride Function Clone() As Object Implements System.ICloneable.Clone

#End Region

#Region "Private and Protected Methods"
    ' ideally get/set from storage
    Protected MustOverride Function getValue(ByVal sender As Object, ByVal e As PropParmEventArgs) As Boolean

    Protected MustOverride Function setValue(ByVal sender As Object, ByVal e As PropParmEventArgs) As Boolean

    Protected Function initAndAddPropParm(ByVal name As String, ByVal type As Type, ByVal category As String, ByVal description As String, ByVal defaultValue As Object, ByVal editor As Type, ByVal typeConverter As Type) As PropParm
        Dim propParm As PropParm = propParm.GetInstance
        propParm.Init(name, type, category, description, defaultValue, editor, typeConverter)
        pPropContainer.Properties.Add(propParm)
        Return propParm
    End Function

    Protected Overridable Sub unprocessedProperty(ByVal e As PropParmEventArgs)
        ExceptionService.Notify("Unprocessed property " & e.Property.Name)
    End Sub
#End Region

End Class
