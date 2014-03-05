#Region "Imports"
#End Region

Public Class IOPresenterIO
    Implements IIOPresenter

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pIIO As IIO
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As IOPresenterIO
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As IOPresenterIO = New IOPresenterIO
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As IOPresenterIO
        Return New IOPresenterIO
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property IIO() As IIO Implements IIOPresenter.IIO
        Get
            Return pIIO
        End Get
        Set(ByVal value As IIO)
            pIIO = value
        End Set
    End Property

    Public Function BuildIO(ByRef ioType As ISFT) As Boolean Implements IIOPresenter.BuildIO
        IIO = IOBuilder.GetInstance.Build(ioType)
        If IIO Is Nothing Then
            Exit Function
        End If
    End Function

    Public Sub ShutdownIO() Implements IIOPresenter.ShutdownIO
        If IIO Is Nothing Then
            Exit Sub
        End If
        IIO.Shutdown()
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
