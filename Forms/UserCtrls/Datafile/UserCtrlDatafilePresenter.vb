Public Class UserCtrlDatafilePresenter
    Inherits MVPUserCtrlPresenterBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public Event DatafilesSelected(ByVal selectedDatafiles As String())
#End Region

#Region "Private and Protected Members"
    Private WithEvents pUserCtrlDatafile As UserCtrlDatafile
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As UserCtrlDatafilePresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As UserCtrlDatafilePresenter = New UserCtrlDatafilePresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As UserCtrlDatafilePresenter
        Return New UserCtrlDatafilePresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property EpochText() As String
        Get
            Return pUserCtrlDatafile.EpochText
        End Get
        Set(ByVal Value As String)
            pUserCtrlDatafile.EpochText = Value
        End Set
    End Property

    Public Function ValidateEpoch() As Boolean
        Return pUserCtrlDatafile.ValidateEpoch
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pUserCtrlDatafile = CType(IMVPUserCtrl, UserCtrlDatafile)

        AddHandler pUserCtrlDatafile.DirectorySelected, AddressOf getDirectory
        AddHandler pUserCtrlDatafile.DatafilesSelected, AddressOf getDatafiles
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Sub getDirectory()
        Dim getDirectoryThread As New Threading.Thread(AddressOf getDirectoryWork)
        ' necessary for OLE
        getDirectoryThread.SetApartmentState(Threading.ApartmentState.STA)
        getDirectoryThread.Start()
    End Sub

    Private Sub getDirectoryWork()
        Dim folderBrowserDialog As New FolderBrowserDialog
        folderBrowserDialog.Description = "Select a Directory of Datafiles"
        folderBrowserDialog.SelectedPath = Application.StartupPath
        If folderBrowserDialog.ShowDialog = DialogResult.OK Then
            pUserCtrlDatafile.SelectedDatafiles = String.Empty
            pUserCtrlDatafile.SelectedDirectory = folderBrowserDialog.SelectedPath
            RaiseEvent DatafilesSelected(Library.GetInstance.GetDatafilesFromDirectory(folderBrowserDialog.SelectedPath))
        End If
    End Sub

    Private Sub getDatafiles()
        Dim getFileThread As New Threading.Thread(AddressOf getDatafilesWork)
        ' necessary for OLE
        getFileThread.SetApartmentState(Threading.ApartmentState.STA)
        getFileThread.Start()
    End Sub

    Private Sub getDatafilesWork()
        Dim openFileDialog As New OpenFileDialog
        openFileDialog.Title = "Select Datafiles"
        openFileDialog.Filter = "Datafiles (*." _
                                & BartelsLibrary.Constants.DatafilesExtension _
                                & ")|*." _
                                & BartelsLibrary.Constants.DatafilesExtension _
                                & "|All files (*.*)|*.*"
        openFileDialog.FilterIndex = 1
        openFileDialog.Multiselect = True
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            pUserCtrlDatafile.SelectedDirectory = String.Empty
            pUserCtrlDatafile.SelectedDatafiles = datafilesToStringAdapter(openFileDialog.FileNames)
            RaiseEvent DatafilesSelected(openFileDialog.FileNames)
        End If
    End Sub

    Private Function datafilesToStringAdapter(ByVal datafiles As String()) As String
        Const delimiter As String = ", "
        Dim sb As New Text.StringBuilder
        For Each dataFile As String In datafiles
            sb.Append(dataFile)
            sb.Append(delimiter)
        Next
        Return sb.ToString.TrimEnd(delimiter.ToCharArray)
    End Function
#End Region

End Class
