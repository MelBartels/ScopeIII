#Region "Imports"
#End Region

Public Class DGVCelestialErrorsProperties

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    '    Private Sub New()
    '    End Sub

    '    Public Shared Function GetInstance() As DGVCelestialErrorsProperties
    '        Return NestedInstance.INSTANCE
    '    End Function

    '    Private Class NestedInstance
    '        ' explicit constructor informs compiler not to mark type as beforefieldinit
    '        Shared Sub New()
    '        End Sub
    '        ' friend = internal, shared = static, readonly = final
    '        Friend Shared ReadOnly INSTANCE As DGVCelestialErrorsProperties = New DGVCelestialErrorsProperties
    '    End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As DGVCelestialErrorsProperties
        Return New DGVCelestialErrorsProperties
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub SetProperties(ByRef dgvCelestialErrors As DataGridView)
        With dgvCelestialErrors
            ' otherwise entry point for this thread needs to be marked as single threaded apartment thanks to ole
            .ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable
            ' necessary for .DisplayIndex to work
            .AutoGenerateColumns = False

            .Columns(DGVColumnNames.RA.Description).Visible = False
            .Columns(DGVColumnNames.Dec.Description).Visible = False
            .Columns(DGVColumnNames.Az.Description).Visible = False
            .Columns(DGVColumnNames.Alt.Description).Visible = False
            '.Columns(DGVColumnNames.RADisplay.Description).Visible = False
            '.Columns(DGVColumnNames.DecDisplay.Description).Visible = False
            .Columns(DGVColumnNames.AzDisplay.Description).Visible = False
            .Columns(DGVColumnNames.AltDisplay.Description).Visible = False

            .Columns(DGVColumnNames.Name.Description).DisplayIndex = 0
            .Columns(DGVColumnNames.Name.Description).HeaderText = CStr(DGVColumnNames.ErrorType.Tag)

            .Columns(DGVColumnNames.RADisplay.Description).DisplayIndex = 1
            .Columns(DGVColumnNames.RADisplay.Description).HeaderText = CStr(DGVColumnNames.RADisplay.Tag)

            .Columns(DGVColumnNames.DecDisplay.Description).DisplayIndex = 2
            .Columns(DGVColumnNames.DecDisplay.Description).HeaderText = CStr(DGVColumnNames.DecDisplay.Tag)

            .Columns(DGVColumnNames.AzDisplay.Description).DisplayIndex = 3
            .Columns(DGVColumnNames.AzDisplay.Description).HeaderText = CStr(DGVColumnNames.AzDisplay.Tag)

            .Columns(DGVColumnNames.AltDisplay.Description).DisplayIndex = 4
            .Columns(DGVColumnNames.AltDisplay.Description).HeaderText = CStr(DGVColumnNames.AltDisplay.Tag)

            ' runs very slowly on large amounts of data
            .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            .AutoResizeColumns()
        End With
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
