#Region "Imports"
#End Region

Public MustInherit Class SFTFacadeBase
    Implements ISFTFacade

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Protected pSelectedItems As ArrayList
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As SFTFacadeBase
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As SFTFacadeBase = New SFTFacadeBase
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
        pSelectedItems = New ArrayList
    End Sub

    'Public Shared Function GetInstance() As SFTFacadeBase
    '    Return New SFTFacadeBase
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public MustOverride ReadOnly Property FirstItem() As ISFT Implements ISFTFacade.FirstItem

    Public Property SelectedItem() As ISFT Implements ISFTFacade.SelectedItem
        Get
            Return CType(pSelectedItems(0), ISFT)
        End Get
        Set(ByVal Value As ISFT)
            pSelectedItems.Clear()
            pSelectedItems.Add(Value)
        End Set
    End Property

    Public Property SelectedItems() As System.Collections.ArrayList Implements ISFTFacade.SelectedItems
        Get
            Return pSelectedItems
        End Get
        Set(ByVal Value As System.Collections.ArrayList)
            pSelectedItems = Value
        End Set
    End Property

    Public Property SelectedItemsAsCommaDelimitedString() As String Implements ISFTFacade.SelectedItemsAsCommaDelimitedString
        Get
            Dim sb As New Text.StringBuilder
            For Each ISFT As ISFT In pSelectedItems
                sb.Append(ISFT.Name)
                sb.Append(",")
            Next
            If sb.Length > 0 Then
                sb.Remove(sb.Length - 1, 1)
            End If
            Return sb.ToString
        End Get
        Set(ByVal Value As String)
            Dim st As StringTokenizer = StringTokenizer.GetInstance
            st.Tokenize(Value, ",".ToCharArray)
            For Each item As String In st.Tokens
                Dim isft As ISFT = FirstItem.MatchString(item)
                If isft IsNot Nothing Then
                    pSelectedItems.Add(isft)
                End If
            Next
        End Set
    End Property

    Public Function SetSelectedItem(ByRef ISFT As ISFT) As ISFTFacade Implements ISFTFacade.SetSelectedItem
        SelectedItem = FirstItem.MatchISFT(ISFT)
        Return Me
    End Function

    Public Function SetSelectedItem(ByVal name As String) As ISFTFacade Implements ISFTFacade.SetSelectedItem
        SelectedItem = FirstItem.MatchString(name)
        Return Me
    End Function

    Public Function SetSelectedItem(ByVal key As Integer) As ISFTFacade Implements ISFTFacade.SetSelectedItem
        SelectedItem = FirstItem.MatchKey(key)
        Return Me
    End Function
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
