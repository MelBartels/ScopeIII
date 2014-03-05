#Region "Imports"
Imports System.Collections.Generic
#End Region

Public Class ObserverWithID
    Implements IObserver

#Region "Inner Classes"
#End Region

#Region "Constant Members"
    Public IDSeparator As String = ": "
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pObservableImp As ObservableImp
    Private pObservingID As String
    Private pObservedID As String
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As ObserverWithID
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As ObserverWithID = New ObserverWithID
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
        ObservableImp = ObservableImp.GetInstance
    End Sub

    Public Shared Function GetInstance() As ObserverWithID
        Return New ObserverWithID
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property ObservableImp() As ObservableImp
        Get
            Return pObservableImp
        End Get
        Set(ByVal value As ObservableImp)
            pObservableImp = value
        End Set
    End Property

    Public Property ObservingID() As String
        Get
            Return pObservingID
        End Get
        Set(ByVal value As String)
            pObservingID = value
        End Set
    End Property

    Public Property ObservedID() As String
        Get
            Return pObservedID
        End Get
        Set(ByVal value As String)
            pObservedID = value
        End Set
    End Property

    Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements IObserver.ProcessMsg
        Dim sb As New Text.StringBuilder
        sb.Append(ObservedID)
        sb.Append(IDSeparator)
        sb.Append([object])

        ObservableImp.Notify(sb.ToString)
    End Function

    Public Function Build(ByVal observingID As String, ByVal observedID As String, ByRef IObserver As IObserver) As ObserverWithID
        Dim observerWithID As ObserverWithID = observerWithID.GetInstance
        observerWithID.ObservingID = observingID
        observerWithID.ObservedID = observedID
        observerWithID.ObservableImp.Attach(IObserver)
        Return observerWithID
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region
End Class
