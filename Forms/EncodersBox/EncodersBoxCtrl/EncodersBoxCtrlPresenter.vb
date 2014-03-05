#Region "imports"
#End Region

Public Class EncodersBoxCtrlPresenter
    Inherits EncodersBoxPresenterBase
    Implements IEncodersBoxCtrlPresenter

#Region "Inner Classes"

#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "protected and Protected Members"
    Protected WithEvents pFrmEncodersBoxCtrl As FrmEncodersBoxCtrl
    Protected pDeviceCmdFacade As DeviceCmdFacade
#End Region

#Region "Constructors (Singleton Pattern)"
    'protected Sub New()
    'End Sub

    'Public Shared Function GetInstance() As EncodersBoxCtrlPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'protected Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As EncodersBoxCtrlPresenter = New EncodersBoxCtrlPresenter
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As EncodersBoxCtrlPresenter
        Return New EncodersBoxCtrlPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pFrmEncodersBoxCtrl = CType(pIFrmEncodersBox, FrmEncodersBoxCtrl)
        AddHandler pFrmEncodersBoxCtrl.SendCmd, AddressOf sendCmd
        AddHandler pFrmEncodersBoxCtrl.AutoQuery, AddressOf autoQuery

        buildDeviceCmdFacade()
        setTermPortTypeToDeviceType()

        updateViewCmdsDataSource()
        showTwoAxisEncoderTranslatorForm()
    End Sub

    Protected Overrides Sub closeForm()
        stopQuerying()
        MyBase.closeForm()
    End Sub

#Region "Device Properties"
    Protected Overrides Sub settingsUpdated()
        MyBase.settingsUpdated()
        updateViewCmdsDataSource()
    End Sub

    Protected Sub updateViewCmdsDataSource()
        pFrmEncodersBoxCtrl.CmdsDataSource = getEncodersBox.GetCmdSet.FirstItem.DataSource
    End Sub
#End Region

#Region "CmdFacade"
    ' share DeviceToIOBridge instance between pDeviceCmdFacade and pUserCtrlTerminalPresenter
    Protected Sub buildDeviceCmdFacade()
        pDeviceCmdFacade = DeviceCmdFacade.GetInstance
        pDeviceCmdFacade.IDevice = getEncodersBox()
        CType(pUserCtrlTerminalPresenter.IIOPresenter, IOPresenterDevice).DeviceToIOBridge = pDeviceCmdFacade.DeviceToIOBridge
    End Sub

    Protected Overrides Sub newPortBuilt()
        pDeviceCmdFacade.IIO.StatusObservers.Attach(CType(ObserverWithID.GetInstance.Build(Me.GetType.Name, "IO", Me), IObserver))
        pDeviceCmdFacade.StartIOListening()
        updateTerminalIOFromDevice()
    End Sub

    Protected Sub sendCmd(ByVal cmdIx As Int32)
        pLastCmdIx = cmdIx
        pDeviceCmdFacade.Execute(getEncodersBox.GetCmdSet.FirstItem.MatchKey(cmdIx))
    End Sub
#End Region

#Region "AutoQuery"
    Protected pLastCmdIx As Int32 = 0
    Protected pAutoQuerying As Boolean
    Protected pQueryTimer As Timers.Timer
    Protected queryUpdateMillisec As Int32 = 1000

    Protected Sub autoQuery(ByVal switch As Boolean)
        If switch Then
            If Not pAutoQuerying Then
                pAutoQuerying = True
                pQueryTimer = New Timers.Timer(queryUpdateMillisec)
                AddHandler pQueryTimer.Elapsed, AddressOf autoQuery
                pQueryTimer.Start()
            End If
        Else
            If pAutoQuerying Then
                pAutoQuerying = False
                pQueryTimer.Stop()
            End If
        End If
    End Sub

    Protected Sub autoQuery(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        pDeviceCmdFacade.Execute(getEncodersBox.GetCmdSet.FirstItem.MatchKey(pLastCmdIx))
    End Sub

    Protected Sub stopQuerying()
        If pAutoQuerying Then
            pQueryTimer.Stop()
        End If
    End Sub
#End Region

#Region "TwoAxisEncoderTranslator"
    Protected pITwoAxisEncoderTranslatorPresenter As ITwoAxisEncoderTranslatorPresenter

    Protected Sub showTwoAxisEncoderTranslatorForm()
        Dim frmTwoAxisEncoderTranslatorThread As New Threading.Thread(AddressOf initTwoAxisEncoderTranslatorPresenter)
        frmTwoAxisEncoderTranslatorThread.Start()
    End Sub

    Protected Sub initTwoAxisEncoderTranslatorPresenter()
        pITwoAxisEncoderTranslatorPresenter = FormsDependencyInjector.GetInstance.ITwoAxisEncoderTranslatorPresenterFactory
        With pITwoAxisEncoderTranslatorPresenter
            .IMVPView = New FrmTwoAxisEncoderTranslator
            AxisCoordGaugePresenter.GetInstance.Build(.IGauge2AxisCoordPresenter, CoordName.RA, CoordName.Dec)

            Dim userCtrl2AxisEncoderTranslatorPresenter As UserCtrl2AxisEncoderTranslatorPresenter = _
                CType(.IGauge2AxisCoordPresenter, UserCtrl2AxisEncoderTranslatorPresenter)
            userCtrl2AxisEncoderTranslatorPresenter.BuildAxisEncoderAdapters(getSettingsEncoderValuePri, getSettingsEncoderValueSec)

            .ShowDialog()
        End With
    End Sub
#End Region

#End Region

End Class
