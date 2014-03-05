Public Class MVPUserCtrlGaugesBase
    Inherits BartelsLibrary.MVPUserCtrlBase
    Implements IMVPUserCtrlGauges

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        components = New System.ComponentModel.Container
    End Sub

#End Region

    Private pGaugeLayout As ISFT

    Public Property GaugeLayout() As ISFT Implements IMVPUserCtrlGauges.GaugeLayout
        Get
            Return pGaugeLayout
        End Get
        Set(ByVal Value As ISFT)
            pGaugeLayout = Value
        End Set
    End Property

    Public Overridable Sub NewSize(ByVal width As Int32, ByVal height As Int32) Implements IMVPUserCtrlGauges.NewSize

    End Sub

End Class
