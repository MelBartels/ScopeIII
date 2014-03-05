#Region "Imports"
#End Region

Public Class TestFunction
    Implements IFunction

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pMinX As Double
    Private pMaxX As Double
    Private pMinY As Double
    Private pMaxY As Double
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As TestFunction
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As TestFunction = New TestFunction
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        pMinX = -5.0
        pMaxX = +5.0
        pMinY = pMinX
        pMaxY = pMaxX
    End Sub

    Public Shared Function GetInstance() As TestFunction
        Return New TestFunction
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property MaxX() As Double Implements IFunction.MaxX
        Get
            Return pMaxX
        End Get
        Set(ByVal Value As Double)
            pMaxX = Value
        End Set
    End Property

    Public Property MaxY() As Double Implements IFunction.MaxY
        Get
            Return pMaxY
        End Get
        Set(ByVal Value As Double)
            pMaxY = Value
        End Set
    End Property

    Public Property MinX() As Double Implements IFunction.MinX
        Get
            Return pMinX
        End Get
        Set(ByVal Value As Double)
            pMinX = Value
        End Set
    End Property

    Public Property MinY() As Double Implements IFunction.MinY
        Get
            Return pMinY
        End Get
        Set(ByVal Value As Double)
            pMinY = Value
        End Set
    End Property

    Public Function Y(ByVal X As Double) As Double Implements IFunction.Y
        Return Math.Exp(X)
    End Function

    Public Overrides Function ToString() As String Implements IFunction.ToString
        Return "Y = Exp(X)"
    End Function

#End Region

#Region "Private and Protected Methods"
#End Region

End Class
