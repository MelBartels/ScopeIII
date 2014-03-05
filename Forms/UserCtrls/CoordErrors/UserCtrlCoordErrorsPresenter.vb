#Region "Imports"
Imports System.IO
Imports System.Windows.Forms
#End Region

Public Class UserCtrlCoordErrorsPresenter
    Inherits MVPUserCtrlPresenterBase
    Implements IObserver

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    Public ReadOnly Property UncorrectedUserCtrl2AxisCoordPresenter() As UserCtrl2AxisCoordPresenter
        Get
            Return p2AxisCoordPresenterUncorrected
        End Get
    End Property

    Public ReadOnly Property CorrectedUserCtrl2AxisCoordPresenter() As UserCtrl2AxisCoordPresenter
        Get
            Return p2AxisCoordPresenterCorrected
        End Get
    End Property
#End Region

#Region "Private and Protected Members"
    Private WithEvents pUserCtrlCoordErrors As UserCtrlCoordErrors

    Private p2AxisCoordPresenterUncorrected As UserCtrl2AxisCoordPresenter
    Private p2AxisCoordPresenterCorrected As UserCtrl2AxisCoordPresenter
    Private p2AxisCoordPresenterPrecession As UserCtrl2AxisCoordPresenter
    Private p2AxisCoordPresenterNutation As UserCtrl2AxisCoordPresenter
    Private p2AxisCoordPresenterAnnualAberration As UserCtrl2AxisCoordPresenter

    Private pEmath As eMath
    Private pPrecNutAnAberCalculator As PrecNutAnAberCalculator
    Private pUncorrectedPosition As Position
    Private pCorrectedPosition As Position
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As UserCtrlCoordErrorsPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As UserCtrlCoordErrorsPresenter = New UserCtrlCoordErrorsPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As UserCtrlCoordErrorsPresenter
        Return New UserCtrlCoordErrorsPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property UncorrectedPosition() As Position
        Get
            Return pUncorrectedPosition
        End Get
        Set(ByVal Value As Position)
            pUncorrectedPosition = Value
        End Set
    End Property

    Public Property CorrectedPosition() As Position
        Get
            Return pCorrectedPosition
        End Get
        Set(ByVal Value As Position)
            pCorrectedPosition = Value
        End Set
    End Property

    Public Function ValidateCoordinates() As Boolean
        If pUserCtrlCoordErrors.ValidateEpoch() Then
            UncorrectedPosition.RA = p2AxisCoordPresenterUncorrected.CoordinatePri
            UncorrectedPosition.Dec = p2AxisCoordPresenterUncorrected.CoordinateSec
            Return True
        End If
        Return False
    End Function

    Public Sub DisplayUncorrectedCoordinates(ByVal UncorrectedCoordinates As Position)
        Me.UncorrectedPosition.CopyFrom(UncorrectedCoordinates)
        p2AxisCoordPresenterUncorrected.DisplayCoordinates(UncorrectedPosition.RA.Rad, UncorrectedPosition.Dec.Rad)
    End Sub

    Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements Common.IObserver.ProcessMsg
        calcErrors()
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pUserCtrlCoordErrors = CType(IMVPUserCtrl, UserCtrlCoordErrors)
        AddHandler pUserCtrlCoordErrors.CalcErrors, AddressOf calcErrors

        buildUserCtrl2AxisCoordPresenter(pUserCtrlCoordErrors.UserCtrl2AxisCoordUncorrected, p2AxisCoordPresenterUncorrected)
        p2AxisCoordPresenterUncorrected.CoordinatePriObservableImp.Attach(Me)
        p2AxisCoordPresenterUncorrected.CoordinateSecObservableImp.Attach(Me)
        buildUserCtrl2AxisCoordPresenter(pUserCtrlCoordErrors.UserCtrl2AxisCoordCorrected, p2AxisCoordPresenterCorrected)
        buildUserCtrl2AxisCoordPresenter(pUserCtrlCoordErrors.UserCtrl2AxisCoordPrecession, p2AxisCoordPresenterPrecession)
        buildUserCtrl2AxisCoordPresenter(pUserCtrlCoordErrors.UserCtrl2AxisCoordNutation, p2AxisCoordPresenterNutation)
        buildUserCtrl2AxisCoordPresenter(pUserCtrlCoordErrors.UserCtrl2AxisCoordAnnualAberration, p2AxisCoordPresenterAnnualAberration)

        pUserCtrlCoordErrors.EpochText = Settings.GetInstance.DatafilesEpoch

        pEmath = eMath.GetInstance
        pPrecNutAnAberCalculator = PrecNutAnAberCalculator.GetInstance
        UncorrectedPosition = Position.GetInstance
        CorrectedPosition = Position.GetInstance

        ValidateCoordinates()
        calcErrors()
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Sub buildUserCtrl2AxisCoordPresenter(ByRef userCtrl As UserCtrl2AxisCoord, ByRef presenter As UserCtrl2AxisCoordPresenter)
        presenter = UserCtrl2AxisCoordPresenter.GetInstance
        presenter.IMVPUserCtrl = userCtrl
        presenter.SetAxisNames(CoordName.RA.Description, CoordName.Dec.Description)
        presenter.SetExpCoordTypes(CType(CoordExpType.FormattedHMSM, ISFT), CType(CoordExpType.FormattedDMS, ISFT))
    End Sub

    Private Sub calcErrors()
        If ValidateCoordinates() Then
            CorrectedPosition.CopyFrom(UncorrectedPosition)

            Dim workDateTime As New DateTime(CType(Math.Floor(Double.Parse(pUserCtrlCoordErrors.EpochText)), Int32), 1, 1)
            Dim uncorrectedDateTime As DateTime = workDateTime.AddDays(CType(Double.Parse(pUserCtrlCoordErrors.EpochText) * 365.25 Mod 365.25, Int32))

            pPrecNutAnAberCalculator.CalculateErrors(UncorrectedPosition, uncorrectedDateTime, pUserCtrlCoordErrors.DateTimePickerValue)

            If pUserCtrlCoordErrors.IncludePrecessionChecked Then
                CorrectedPosition.RA.Rad += UncorrectedPosition.CoordErrorArray.CoordError(CType(CoordName.RA, ISFT), CType(CoordErrorType.Precession, ISFT)).Rad
                CorrectedPosition.Dec.Rad += UncorrectedPosition.CoordErrorArray.CoordError(CType(CoordName.Dec, ISFT), CType(CoordErrorType.Precession, ISFT)).Rad
            End If

            If pUserCtrlCoordErrors.IncludeNutationAnnualAberrationChecked Then
                CorrectedPosition.RA.Rad += UncorrectedPosition.CoordErrorArray.CoordError(CType(CoordName.RA, ISFT), CType(CoordErrorType.Nutation, ISFT)).Rad _
                                          + UncorrectedPosition.CoordErrorArray.CoordError(CType(CoordName.RA, ISFT), CType(CoordErrorType.AnnualAberration, ISFT)).Rad
                CorrectedPosition.Dec.Rad += UncorrectedPosition.CoordErrorArray.CoordError(CType(CoordName.Dec, ISFT), CType(CoordErrorType.Nutation, ISFT)).Rad _
                                           + UncorrectedPosition.CoordErrorArray.CoordError(CType(CoordName.Dec, ISFT), CType(CoordErrorType.AnnualAberration, ISFT)).Rad
            End If

            p2AxisCoordPresenterPrecession.DisplayCoordinates( _
                    UncorrectedPosition.CoordErrorArray.CoordError(CType(CoordName.RA, ISFT), CType(CoordErrorType.Precession, ISFT)).Rad, _
                    UncorrectedPosition.CoordErrorArray.CoordError(CType(CoordName.Dec, ISFT), CType(CoordErrorType.Precession, ISFT)).Rad)
            p2AxisCoordPresenterNutation.DisplayCoordinates( _
                    UncorrectedPosition.CoordErrorArray.CoordError(CType(CoordName.RA, ISFT), CType(CoordErrorType.Nutation, ISFT)).Rad, _
                    UncorrectedPosition.CoordErrorArray.CoordError(CType(CoordName.Dec, ISFT), CType(CoordErrorType.Nutation, ISFT)).Rad)
            p2AxisCoordPresenterAnnualAberration.DisplayCoordinates( _
                    UncorrectedPosition.CoordErrorArray.CoordError(CType(CoordName.RA, ISFT), CType(CoordErrorType.AnnualAberration, ISFT)).Rad, _
                    UncorrectedPosition.CoordErrorArray.CoordError(CType(CoordName.Dec, ISFT), CType(CoordErrorType.AnnualAberration, ISFT)).Rad)

            p2AxisCoordPresenterCorrected.DisplayCoordinates(pEmath.ValidRad(CorrectedPosition.RA.Rad), CorrectedPosition.Dec.Rad)
        End If
    End Sub
#End Region

End Class
