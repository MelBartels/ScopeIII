#Region "Imports"
#End Region

Public Class EndoTestFrmShowSettings
    Inherits MVPViewBase
    Implements IFrmShowSettings

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected pTitle As String
    Protected pUserCtrlPropGrid As New UserCtrlPropGrid
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As EndoTestFrmShowSettings
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As EndoTestFrmShowSettings = New EndoTestFrmShowSettings
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As EndoTestFrmShowSettings
        Return New EndoTestFrmShowSettings
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property Title() As String Implements IFrmShowSettings.Title
        Get
            Return pTitle
        End Get
        Set(ByVal value As String)
            pTitle = value
        End Set
    End Property

    Public Property UserCtrlPropGrid() As UserCtrlPropGrid Implements IFrmShowSettings.UserCtrlPropGrid
        Get
            Return pUserCtrlPropGrid
        End Get
        Set(ByVal value As UserCtrlPropGrid)
            pUserCtrlPropGrid = value
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
