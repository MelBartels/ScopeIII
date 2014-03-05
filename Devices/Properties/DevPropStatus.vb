#Region "Imports"
#End Region

Public Class DevPropStatus
    Inherits DevPropBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private pStatuses As ArrayList
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As DevPropStatus
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DevPropStatus = New DevPropStatus
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As DevPropStatus
        Return New DevPropStatus
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Overrides Property Value() As String
        Get
            Return getValue()
        End Get
        Set(ByVal value As String)
            ' no setter
        End Set
    End Property

    Public Property Statuses() As ArrayList
        Get
            Return pStatuses
        End Get
        Set(ByVal value As ArrayList)
            pStatuses = value
        End Set
    End Property

    Public Function SetStatus(ByVal type As String, ByVal value As String) As String
        ' create if necessary
        If pStatuses Is Nothing Then
            pStatuses = New ArrayList
        End If

        ' find existing status type and set its value
        Dim statusTypeValuePair As StatusTypeValuePair
        For Each statusTypeValuePair In pStatuses
            If statusTypeValuePair.Type.Equals(type) Then
                statusTypeValuePair.Value = value
                Return value
            End If
        Next

        ' if necessary, create new status, set its value, and add it to the status array 
        statusTypeValuePair = ScopeIII.Devices.StatusTypeValuePair.GetInstance
        statusTypeValuePair.Type = type
        statusTypeValuePair.Value = value
        pStatuses.Add(statusTypeValuePair)
        Return value
    End Function

    Public Function GetStatus(ByVal type As String) As String
        If pStatuses Is Nothing Then
            Return Nothing
        End If

        ' find existing status type and return its value
        Dim statusTypeValuePair As StatusTypeValuePair
        For Each statusTypeValuePair In pStatuses
            If statusTypeValuePair.Type.Equals(type) Then
                Return statusTypeValuePair.Value
            End If
        Next

        Return Nothing
    End Function

    Public Function GetAllStatusValues() As ArrayList
        Dim rArray As New ArrayList
        If pStatuses IsNot Nothing Then
            For Each statusTypeValuePair As StatusTypeValuePair In pStatuses
                rArray.Add(statusTypeValuePair.Value)
            Next
        End If
        Return rArray
    End Function

    Public Overrides Function Clone() As Object
        Dim DevPropStatus As DevPropStatus = ScopeIII.Devices.DevPropStatus.GetInstance
        If pStatuses IsNot Nothing Then
            For Each statusTypeValuePair As StatusTypeValuePair In pStatuses
                Dim clonedStatusTypeValuePair As StatusTypeValuePair = CType(statusTypeValuePair.Clone, StatusTypeValuePair)
                DevPropStatus.SetStatus(clonedStatusTypeValuePair.Type, clonedStatusTypeValuePair.Value)
            Next
        End If
        Return DevPropStatus
    End Function
#End Region

#Region "Private and Protected Methods"
    Private Function getValue() As String
        Dim sb As New Text.StringBuilder
        Dim statuses As ArrayList = GetAllStatusValues()
        If statuses IsNot Nothing Then
            For Each status As String In statuses
                sb.Append(status)
                sb.Append(", ")
            Next
        End If
        If sb.Length >= 2 Then
            sb.Remove(sb.Length - 2, 2)
        End If
        Return sb.ToString
    End Function
#End Region

End Class
