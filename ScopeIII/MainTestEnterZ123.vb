Public Class MainTestEnterZ123
    Inherits MainPrototype

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
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As MainTestEnterZ123
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MainTestEnterZ123 = New MainTestEnterZ123
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MainTestEnterZ123
        Return New MainTestEnterZ123
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub work()
        Dim Z123Presenter As Z123Presenter = Forms.Z123Presenter.GetInstance
        Z123Presenter.IMVPView = New FrmEnterZ123
        Z123Presenter.ShowDialog()

        Dim sb As New System.Text.StringBuilder
        If Not Z123Presenter.UseCorrections Then
            sb.Append("not ")
        End If
        sb.Append("using corrections")
        If Z123Presenter.UseCorrections Then
            Dim coordExpType As ISFT = CType(Coordinates.CoordExpType.DMS, ISFT)
            sb.appendline()
            sb.Append(Z123Presenter.FabErrors.Z1.ToString(coordExpType))
            sb.appendline()
            sb.Append(Z123Presenter.FabErrors.Z2.ToString(coordExpType))
            sb.appendline()
            sb.Append(Z123Presenter.FabErrors.Z3.ToString(coordExpType))
        End If
        MessageBoxCoord.GetInstance.Show(sb.ToString, "Error Correction Values")
    End Sub
#End Region

End Class
