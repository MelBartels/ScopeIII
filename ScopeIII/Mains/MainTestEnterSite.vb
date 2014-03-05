Public Class MainTestEnterSite
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

    'Public Shared Function GetInstance() As MainTestEnterSite
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MainTestEnterSite = New MainTestEnterSite
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As MainTestEnterSite
        Return New MainTestEnterSite
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub work()
        Dim sitePresenter As SitePresenter = Forms.SitePresenter.GetInstance
        sitePresenter.IMVPView = New FrmEnterSite
        sitePresenter.ShowDialog()

        Dim site As Coordinates.Site = sitePresenter.Site
        If site Is Nothing Then
            AppMsgBox.Show(BartelsLibrary.Constants.NothingEntered)
        Else
            MessageBoxCoord.GetInstance.Show(site.Name, site.Latitude, site.Longitude)
        End If
    End Sub
#End Region

End Class
