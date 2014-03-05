Public Class StartupShell

    Private Sub Shell_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim mainThread As New Threading.Thread(AddressOf launchMain)
        mainThread.Name = "main thread"
        mainThread.Start()
        closeStrategy()
    End Sub

    Private Sub launchMain()
        Main.GetInstance.Main()
    End Sub

    Private Sub closeStrategy()
        ' keep splash screen up for a bit;
        ' don't close unless there's an splash screen 
        Threading.Thread.Sleep(5000)
        Me.Close()
    End Sub
End Class