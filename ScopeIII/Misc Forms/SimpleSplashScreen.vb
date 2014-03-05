Public NotInheritable Class SimpleSplashScreen

    'TODO: This form can easily be set as the splash screen for the application by going to the "Application" tab
    '  of the Project Designer ("Properties" under the "Project" menu).


    Private Sub SimpleSplashScreen_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Set up the dialog text at runtime according to the application's assembly information.  

        'TODO: Customize the application's assembly information in the "Application" pane of the project 
        '  properties dialog (under the "Project" menu).

        With My.Application.Info
            ApplicationTitle.Text = .ProductName
            Version.Text = String.Format("Version {0}.{1:00}.{2}.{3}", .Version.Major, .Version.Minor, .Version.Build, .Version.Revision)
            Copyright.Text = .Copyright
            Description.Text = .Description
        End With

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MyBase.Close()
    End Sub
End Class
