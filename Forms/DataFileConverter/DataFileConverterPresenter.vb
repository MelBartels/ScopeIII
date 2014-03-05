#Region "imports"
Imports System.Windows.Forms
Imports System
Imports System.IO
Imports System.Threading

#End Region

Public Class DataFileConverterPresenter
    Inherits MVPPresenterBase
    Implements IObserver

#Region "Inner Classes"
    Private Class columnIx
        Public Ix As Int32
    End Class

    Private Class writeParms
        Public CoordExpRA As ICoordExp = DatafileHMS.GetInstance
        Public CoordExpDec As ICoordExp = DatafileDMS.GetInstance
        Public SB As New Text.StringBuilder
        Public Writer As StreamWriter
    End Class
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

    'Public Shared Function GetInstance() As DataFileConverterPresenter
    '    Return NestedInstance.INSTANCE
    'End Function

    'Private Class NestedInstance
    '    ' explicit constructor informs compiler not to mark type as beforefieldinit
    '    Shared Sub New()
    '    End Sub
    '    ' friend = internal, shared = static, readonly = final
    '    Friend Shared ReadOnly INSTANCE As DataFileConverterPresenter = New DataFileConverterPresenter
    'End Class
#End Region

#Region "Constructors"
    Private Sub New()
    End Sub

    Public Shared Function GetInstance() As DataFileConverterPresenter
        Return New DataFileConverterPresenter
    End Function
#End Region

#Region "Shared Methods"
#End Region

#Region "Public and Friend Methods"
    Public Function ProcessMsg(ByRef [object] As Object) As Boolean Implements BartelsLibrary.IObserver.ProcessMsg
        If [object].GetType IsNot GetType(String) Then
            Return False
        End If

        frmDataFileConverter.DisplayText(CStr([object]))
        Return True
    End Function
#End Region

#Region "Private and Protected Methods"
    Protected Overrides Sub init()
        MyBase.init()

        AddHandler frmDataFileConverter.SelectFile, AddressOf loadFile
    End Sub

    Protected Overrides Sub loadViewFromModel()

    End Sub

    Protected Overrides Sub saveToModel()

    End Sub

    Protected Overrides Sub viewUpdated()

    End Sub

    Private Function frmDataFileConverter() As FrmDataFileConverter
        Return CType(IMVPView, FrmDataFileConverter)
    End Function

    Private Sub loadFile()
        Dim loadFileThread As New Thread(AddressOf convertDataFile)
        loadFileThread.SetApartmentState(ApartmentState.STA)
        loadFileThread.Start(Me)
    End Sub

    Private Sub convertDataFile(ByVal IObserver As Object)
        Dim ofd As OpenFileDialog = New OpenFileDialog
        If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Coordinates.ConvertDataFile.GetInstance.Convert(CType(IObserver, BartelsLibrary.IObserver), ofd.FileName)
        End If
    End Sub
#End Region

End Class
