Public Interface ISFTFacade
    ReadOnly Property FirstItem() As ISFT
    Property SelectedItem() As ISFT
    Property SelectedItems() As ArrayList
    Property SelectedItemsAsCommaDelimitedString() As String
    Function SetSelectedItem(ByRef ISFT As ISFT) As ISFTFacade
    Function SetSelectedItem(ByVal name As String) As ISFTFacade
    Function SetSelectedItem(ByVal key As Int32) As ISFTFacade
End Interface
