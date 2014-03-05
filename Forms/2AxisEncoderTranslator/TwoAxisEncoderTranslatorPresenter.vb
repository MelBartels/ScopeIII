#Region "imports"
#End Region

Public Class TwoAxisEncoderTranslatorPresenter
    Inherits MVPPresenterBase
    Implements ITwoAxisEncoderTranslatorPresenter

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
#End Region

#Region "Private and Protected Members"
    Private WithEvents pFrmTwoAxisEncoderTranslator As FrmTwoAxisEncoderTranslator
    Private WithEvents pIGauge2AxisCoordPresenter As IGauge2AxisCoordPresenter
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As TwoAxisEncoderTranslatorPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As TwoAxisEncoderTranslatorPresenter = New TwoAxisEncoderTranslatorPresenter
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    Public Shared Function GetInstance() As TwoAxisEncoderTranslatorPresenter
        Return New TwoAxisEncoderTranslatorPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Property IGauge2AxisCoordPresenter() As IGauge2AxisCoordPresenter Implements ITwoAxisEncoderTranslatorPresenter.IGauge2AxisCoordPresenter
        Get
            Return pIGauge2AxisCoordPresenter
        End Get
        Set(ByVal Value As IGauge2AxisCoordPresenter)
            pIGauge2AxisCoordPresenter = Value
        End Set
    End Property
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        pFrmTwoAxisEncoderTranslator = CType(IMVPView, FrmTwoAxisEncoderTranslator)
        pIGauge2AxisCoordPresenter = UserCtrl2AxisEncoderTranslatorPresenter.GetInstance
        CType(pIGauge2AxisCoordPresenter, UserCtrl2AxisEncoderTranslatorPresenter).IMVPUserCtrl = pFrmTwoAxisEncoderTranslator.UserCtrl2AxisEncoderTranslator
        pIGauge2AxisCoordPresenter.SetAxisNames(CoordName.PriAxis.Description, CoordName.SecAxis.Description)
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

#End Region

End Class
