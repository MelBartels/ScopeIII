#Region "Imports"
#End Region

Public Class DGVDatafileProperties

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

    '    Public Shared Function GetInstance() As DGVDatafileProperties
    '        Return NestedInstance.INSTANCE
    '    End Function

    '    Private Class NestedInstance
    '        ' explicit constructor informs compiler not to mark type as beforefieldinit
    '        Shared Sub New()
    '        End Sub
    '        ' friend = internal, shared = static, readonly = final
    '        Friend Shared ReadOnly INSTANCE As DGVDatafileProperties = New DGVDatafileProperties
    '    End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As DGVDatafileProperties
        Return New DGVDatafileProperties
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub SetProperties(ByRef dgvDatafiles As DataGridView)
        With dgvDatafiles
            ' otherwise entry point for this thread needs to be marked as single threaded apartment thanks to ole
            .ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable
            ' necessary for .DisplayIndex to work
            .AutoGenerateColumns = False
            ' default .Width is 110
            .Columns(DGVColumnNames.RA.Description).Visible = False
            .Columns(DGVColumnNames.Dec.Description).Visible = False
            .Columns(DGVColumnNames.RADisplay.Description).DisplayIndex = 0
            .Columns(DGVColumnNames.RADisplay.Description).HeaderText = CStr(DGVColumnNames.RADisplay.Tag)
            .Columns(DGVColumnNames.RADisplay.Description).Width = 110
            .Columns(DGVColumnNames.DecDisplay.Description).DisplayIndex = 1
            .Columns(DGVColumnNames.DecDisplay.Description).HeaderText = CStr(DGVColumnNames.DecDisplay.Tag)
            .Columns(DGVColumnNames.DecDisplay.Description).Width = 90
            .Columns(DGVColumnNames.Name.Description).DisplayIndex = 2
            .Columns(DGVColumnNames.Name.Description).HeaderText = CStr(DGVColumnNames.Name.Tag)
            .Columns(DGVColumnNames.Name.Description).Width = 175
            .Columns(DGVColumnNames.Source.Description).DisplayIndex = 3
            .Columns(DGVColumnNames.Source.Description).HeaderText = CStr(DGVColumnNames.Source.Tag)
            .Columns(DGVColumnNames.Source.Description).Width = 100

            ' runs very slowly on large amounts of data
            '.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            '.AutoResizeColumns()
        End With
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
