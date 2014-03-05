#Region "imports"
Imports System.IO
Imports BartelsLibrary.DelegateSigs
#End Region

Public Class PrecessDatafilesPresenter
    Inherits MVPPresenterBase

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private WithEvents pSourcePresenter As UserCtrlDatafilePresenter
    Private pDatafiles As String()
    Private pDestinationDirectory As String
    Private pDeltaYr As Double
    Private pPrecession As Precession
    Private pPositionArray As PositionArray
    Private WithEvents pProgressMediator As ProgressMediator
    Private cancelBackgroundWork As Boolean
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As PrecessDatafilesPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As PrecessDatafilesPresenter = New PrecessDatafilesPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As PrecessDatafilesPresenter
        Return New PrecessDatafilesPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        AddHandler frmPrecessDatafiles.DestinationDirectorySelected, AddressOf destinationDirectorySelected
        AddHandler frmPrecessDatafiles.Precess, AddressOf precess
        AddHandler frmPrecessDatafiles.Cancel, AddressOf cancel

        pPrecession = Precession.GetInstance
        pPositionArray = PositionArray.GetInstance
        pPositionArray.IPositionArrayIO = PositionArrayIODatafile.GetInstance
        pProgressMediator = ProgressMediator.GetInstance

        pSourcePresenter = UserCtrlDatafilePresenter.GetInstance
        pSourcePresenter.IMVPUserCtrl = frmPrecessDatafiles.UserCtrlDatafile
        AddHandler pSourcePresenter.DatafilesSelected, AddressOf datafilesSelected
        pSourcePresenter.EpochText = ScopeLibrary.Settings.GetInstance.DatafilesEpoch

        frmPrecessDatafiles.DestinationEpochText = Now.Year.ToString
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Function frmPrecessDatafiles() As FrmPrecessDatafiles
        Return CType(IMVPView, FrmPrecessDatafiles)
    End Function

    Private Sub cancel()
        frmPrecessDatafiles.Close()
    End Sub

    Private Function validateForm() As Boolean
        Return pSourcePresenter.ValidateEpoch _
            AndAlso Not pDatafiles Is Nothing _
            AndAlso Not String.IsNullOrEmpty(pDestinationDirectory) _
            AndAlso frmPrecessDatafiles.ValidateDestinationEpoch
    End Function

    Private Sub destinationDirectorySelected()
        Dim getDirectoryThread As New Threading.Thread(AddressOf getDirectoryWork)
        ' necessary for OLE
        getDirectoryThread.SetApartmentState(Threading.ApartmentState.STA)
        getDirectoryThread.Start()
    End Sub

    Private Sub getDirectoryWork()
        Dim folderBrowserDialog As New FolderBrowserDialog
        folderBrowserDialog.Description = "Select a Destination Directory"
        folderBrowserDialog.SelectedPath = Application.StartupPath
        If folderBrowserDialog.ShowDialog = DialogResult.OK Then
            frmPrecessDatafiles.DestinationDirectory = folderBrowserDialog.SelectedPath
            pDestinationDirectory = folderBrowserDialog.SelectedPath
        End If
    End Sub

    Private Sub datafilesSelected(ByVal datafiles As String())
        pDatafiles = datafiles
    End Sub

    Private Sub precess()
        If validateForm() Then
            pProgressMediator.ProcessStartup(New DelegateObjDoWorkEventArgs(AddressOf precessFilesCallback))
        Else
            AppMsgBox.Show("Invalid form data.")
        End If
    End Sub

    Private Sub precessFilesCallback(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
        cancelBackgroundWork = False
        pDeltaYr = CDbl(frmPrecessDatafiles.DestinationEpochText) - CDbl(pSourcePresenter.EpochText)

        ' count of .ShowMsgs
        pProgressMediator.ProgressBarMaxValue = 1 + 2 * pDatafiles.Length

        Dim writeToPositionArray As PositionArray = Nothing
        For Each dataFile As String In pDatafiles
            If cancelBackgroundWork Then
                Exit For
            End If
            processAFile(dataFile, False)
            writeFile(pPositionArray, pDestinationDirectory & "\" & Path.GetFileName(dataFile))
        Next

        pProgressMediator.ShowMsg("Finished.")
        pProgressMediator.ProcessShutdown()
    End Sub

    Private Sub progressCancel() Handles pProgressMediator.CancelBackgroundWork
        cancelBackgroundWork = True
    End Sub

    Private Function writeFile(ByRef positionArray As PositionArray, ByVal filename As String) As Boolean
        pProgressMediator.ShowMsg("Writing to file: " & filename)
        positionArray.Export(filename)
        Return True
    End Function

    Private Function processAFile(ByVal filename As String, ByVal displayProgress As Boolean) As Boolean
        pPositionArray.Import(filename)
        pProgressMediator.ShowMsg("Processing file: " _
                                  & filename _
                                  & ", objects in file count: " _
                                  & pPositionArray.PositionArray.Count)

        Dim Ix As Int32 = 0
        For Each position As Position In pPositionArray.PositionArray
            Ix += 1
            If displayProgress Then
                pProgressMediator.ShowMsg(Ix _
                                          & "/" _
                                          & pPositionArray.PositionArray.Count _
                                          & ": " _
                                          & position.ObjName.PadRight(15) _
                                          & "   before: " _
                                          & position.RA.ToString(CType(CoordExpType.HMS, ISFT)) _
                                          & "   " _
                                          & position.Dec.ToString(CType(CoordExpType.DMS, ISFT)))
            End If

            pPrecession.Calc(position, pDeltaYr)
            position.RA.Rad += pPrecession.DeltaRa
            position.RA.Rad = eMath.ValidRad(position.RA.Rad)
            position.Dec.Rad += pPrecession.DeltaDec

            If displayProgress Then
                pProgressMediator.ShowMsg("                            after: " _
                                          & position.RA.ToString(CType(CoordExpType.HMS, ISFT)) _
                                          & "   " _
                                          & position.Dec.ToString(CType(CoordExpType.DMS, ISFT)))
            End If
        Next

        Return True
    End Function
#End Region

End Class
