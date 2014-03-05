Public MustInherit Class MainPrototype

#Region "Inner Classes"
#End Region

#Region "Constant Members"
#End Region

#Region "Shared Members"
#End Region

#Region "Public and Friend Members"
    ' VS2005 w/ application framework sets starting thread to MTA
    Public ApartmentState As Threading.ApartmentState = Threading.ApartmentState.MTA
#End Region

#Region "Private and Protected Members"
#End Region

#Region "Constructors (Singleton Pattern)"
    'Private Sub New()
    'End Sub

    'Public Shared Function GetInstance() As MainPrototype
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As MainPrototype = New MainPrototype
    'End Class
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub

    'Public Shared Function GetInstance() As MainPrototype
    '    Return New MainPrototype
    'End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub Main()
        System.Environment.ExitCode = Main(System.Environment.GetCommandLineArgs)
    End Sub
#End Region

#Region "Private and Protected Methods"
    Protected Overridable Function Main(ByRef args() As String) As Integer
        Try
            Dim mainThread As New Threading.Thread(AddressOf tryCatchWork)
            mainThread.SetApartmentState(ApartmentState)
            mainThread.Start()
        Catch ex As Exception
            ExceptionService.Notify(ex)
        End Try

        Return 0
    End Function

    Protected Sub tryCatchWork()
        Try
            work()
        Catch ex As Exception
            ExceptionService.Notify(ex)
        End Try
    End Sub

    Protected MustOverride Sub work()
#End Region

End Class
