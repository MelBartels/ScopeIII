Public Class MainTestEnterOneTwo
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

    'Public Shared Function GetInstance() As MainTestEnterOneTwo
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MainTestEnterOneTwo = New MainTestEnterOneTwo
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MainTestEnterOneTwo
        Return New MainTestEnterOneTwo
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub work()
        Dim oneTwoPresenter As OneTwoPresenter = Forms.OneTwoPresenter.GetInstance
        oneTwoPresenter.IMVPView = New FrmEnterOneTwo
        oneTwoPresenter.LatitudeRad = 40 * Units.DegToRad
        oneTwoPresenter.ShowDialog()

        If CType(oneTwoPresenter.IMVPView, Windows.Forms.Form).DialogResult.equals(DialogResult.OK) Then
            With CType(oneTwoPresenter.DataModel, OneTwoPresenterDataModel)
                Dim sb As New System.Text.StringBuilder
                sb.Append(.One.ShowCoordDeg)
                sb.AppendLine()
                sb.Append(.Two.ShowCoordDeg)
                sb.AppendLine()
                If Not .UseCorrections Then
                    sb.Append("not ")
                End If
                sb.Append("using corrections")
                If .UseCorrections Then
                    Dim coordExpType As ISFT = CType(Coordinates.CoordExpType.DMS, ISFT)
                    sb.AppendLine()
                    sb.Append(.FabErrors.Z1.ToString(coordExpType))
                    sb.AppendLine()
                    sb.Append(.FabErrors.Z2.ToString(coordExpType))
                    sb.AppendLine()
                    sb.Append(.FabErrors.Z3.ToString(coordExpType))
                End If
                MessageBoxCoord.GetInstance.Show(sb.ToString, "One-Two Entered")
            End With
        Else
            AppMsgBox.Show(BartelsLibrary.Constants.NothingEntered)
        End If
    End Sub
#End Region

End Class
