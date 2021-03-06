#Region "Imports"
#End Region
Imports System.IO
Imports BartelsLibrary


Public Class ConvertDataFile

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
    '    Private Sub New()
    '    End Sub

    '    Public Shared Function GetInstance() As ConvertDataFile
    '        Return NestedInstance.INSTANCE
    '    End Function

    '    Private Class NestedInstance
    '        ' explicit constructor informs compiler not to mark type as beforefieldinit
    '        Shared Sub New()
    '        End Sub
    '        ' friend = internal, shared = static, readonly = final
    '        Friend Shared ReadOnly INSTANCE As ConvertDataFile = New ConvertDataFile
    '    End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As ConvertDataFile
        Return New ConvertDataFile
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Sub Convert(ByVal IObserver As IObserver, ByVal filename As String)
        Try
            Dim observableImp As ObservableImp = observableImp.GetInstance
            observableImp.Attach(CType(IObserver, BartelsLibrary.IObserver))

            observableImp.Notify("File: " & filename & Environment.NewLine)
            Dim reader As New StreamReader(filename)
            Dim writer As New StreamWriter(filename) '& ScopeLibrary.Constants.ConvertDataFilePostpend)

            'format input:
            '(empty line)
            'NGC 8
            '00h 08m 45.7s +23° 50' 16" Peg ** 

            'format output:
            '00 08 45.7 23 50 16 Peg NGC 8

            Dim lineCounter As Int32 = 0
            Dim objectName As String = Nothing
            Dim coords As String = Nothing

            Dim line As String = reader.ReadLine
            While line IsNot Nothing
                lineCounter += 1
                If lineCounter.Equals(2) Then
                    objectName = line
                ElseIf lineCounter.Equals(3) Then
                    coords = line
                    coords = coords.Replace("h", String.Empty)
                    coords = coords.Replace("m", String.Empty)
                    coords = coords.Replace("s", String.Empty)
                    coords = coords.Replace("+", String.Empty)
                    coords = coords.Replace("°", String.Empty)
                    coords = coords.Replace("�", String.Empty)
                    coords = coords.Replace("'", String.Empty)
                    coords = coords.Replace(BartelsLibrary.Constants.Quote, String.Empty)
                    coords = coords.Replace("*", String.Empty)
                    coords = coords.Replace("  ", " ")
                    coords = coords & objectName  'append NGC number
                    coords = coords.Trim

                    observableImp.Notify(coords & Environment.NewLine)
                    writer.WriteLine(coords)
                    lineCounter = 0
                End If

                line = reader.ReadLine
            End While

            observableImp.Notify("Done." & Environment.NewLine)
            reader.Close()
            writer.Close()
            observableImp.Detach(CType(IObserver, BartelsLibrary.IObserver))

        Catch ex As Exception
            ExceptionService.Notify(ex)
        End Try
    End Sub
#End Region

#Region "Private and Protected Methods"
#End Region

End Class
