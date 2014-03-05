#Region "imports"
Imports System.Windows.Forms
Imports System.IO
Imports System.Threading
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
    Private pSourcePresenter As UserCtrlDatafilePresenter
    Private pDestinationPresenter As UserCtrlDatafilePresenter

    Private pDeltaYr As Double
    Private pPrecession As Precession
    Private pPositionArray As PositionArray
    Private pEmath As eMath
    Private WithEvents pProgressMediator As ScopeIII.Forms.ProgressMediator
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

        AddHandler frmPrecessDatafiles.Precess, AddressOf precess
        AddHandler frmPrecessDatafiles.Cancel, AddressOf cancel

        pPrecession = Precession.GetInstance
        pPositionArray = PositionArray.GetInstance
        pPositionArray.IPositionArrayIO = PositionArrayIODatafile.GetInstance
        pEmath = eMath.GetInstance
        pProgressMediator = ScopeIII.Forms.ProgressMediator.GetInstance

        pSourcePresenter = UserCtrlDatafilePresenter.GetInstance
        pSourcePresenter.IMVPUserCtrl = frmPrecessDatafiles.UserCtrlDatafileSource
        pSourcePresenter.SourceText = Settings.GetInstance.DefaultDatafilesLocation
        pSourcePresenter.EpochText = Settings.GetInstance.DatafilesEpoch

        pDestinationPresenter = UserCtrlDatafilePresenter.GetInstance
        pDestinationPresenter.IMVPUserCtrl = frmPrecessDatafiles.UserCtrlDatafileDestination
        pDestinationPresenter.SourceText = ".\"
        pDestinationPresenter.EpochText = Now.Year.ToString
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
        If pSourcePresenter.ValidateEpoch _
        AndAlso pDestinationPresenter.ValidateEpoch _
        AndAlso pSourcePresenter.ValidateSource Then
            Return True
        End If
        Return False
    End Function

    Private Sub precess()
        If validateForm() Then
            pProgressMediator.ProcessStartup(New ProgressMediator.CallbackDelegate(AddressOf precessFilesCallback))
        End If
    End Sub

    Private Sub precessFilesCallback(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
        cancelBackgroundWork = False
        pDeltaYr = CType(pDestinationPresenter.EpochText, Double) - CType(pSourcePresenter.EpochText, Double)

        Dim files As String() = Library.GetInstance.GetFilesFromFilepath(pSourcePresenter.SourceText)

        ' count of .ShowMsgs
        pProgressMediator.ProgressBarMaxValue = 1 + 2 * files.Length

        Dim writeToDir As Boolean = Directory.Exists(pDestinationPresenter.SourceText)
        Dim writeToPositionArray As PositionArray = Nothing
        If Not writeToDir Then
            writeToPositionArray = PositionArray.GetInstance
            writeToPositionArray.IPositionArrayIO = PositionArrayIODatafile.GetInstance
        End If
        For Each file As String In files
            If cancelBackgroundWork Then
                Exit For
            End If
            processAFile(file, False)
            If writeToDir Then
                writeFile(pPositionArray, pDestinationPresenter.SourceText & "\" & Path.GetFileName(file))
            Else
                For Each position As Position In pPositionArray.PositionArray
                    writeToPositionArray.PositionArray.Add(position)
                Next
            End If
        Next
        If Not writeToDir Then
            writeFile(writeToPositionArray, pDestinationPresenter.SourceText)
        End If

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
            position.RA.Rad = pEmath.ValidRad(position.RA.Rad)
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
