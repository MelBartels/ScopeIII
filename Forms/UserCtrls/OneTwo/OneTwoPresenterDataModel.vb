#Region "Imports"
Imports System.IO
Imports System.Windows.Forms
#End Region

Public Class OneTwoPresenterDataModel

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pOne As Position
    Private pTwo As Position
    Private pUseCorrections As Boolean
    Private pFabErrors As FabErrors
    Private pLatitudeRad As Double
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As OneTwoPresenterDataModel
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As OneTwoPresenterDataModel = New OneTwoPresenterDataModel
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        One = PositionArray.GetInstance.GetPosition
        Two = PositionArray.GetInstance.GetPosition
        FabErrors = Coordinates.FabErrors.GetInstance
    End Sub

    Public Shared Function GetInstance() As OneTwoPresenterDataModel
        Return New OneTwoPresenterDataModel
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property One() As Position
        Get
            Return pOne
        End Get
        Set(ByVal Value As Position)
            pOne = Value
        End Set
    End Property

    Public Property Two() As Position
        Get
            Return pTwo
        End Get
        Set(ByVal Value As Position)
            pTwo = Value
        End Set
    End Property

    Public Property UseCorrections() As Boolean
        Get
            Return pUseCorrections
        End Get
        Set(ByVal Value As Boolean)
            pUseCorrections = Value
        End Set
    End Property

    Public Property FabErrors() As FabErrors
        Get
            Return pFabErrors
        End Get
        Set(ByVal Value As FabErrors)
            pFabErrors = Value
        End Set
    End Property

    Public Property LatitudeRad() As Double
        Get
            Return pLatitudeRad
        End Get
        Set(ByVal value As Double)
            pLatitudeRad = value
        End Set
    End Property

    Public Sub CopyFrom(ByRef oneTwoPresenterDataModel As OneTwoPresenterDataModel)
        With oneTwoPresenterDataModel
            One.CopyFrom(.One)
            Two.CopyFrom(.Two)
            UseCorrections = .UseCorrections
            FabErrors.CopyFrom(.FabErrors)
            LatitudeRad = .LatitudeRad
        End With
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
