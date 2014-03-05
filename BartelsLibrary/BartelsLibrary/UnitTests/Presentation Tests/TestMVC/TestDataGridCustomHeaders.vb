#Region "imports"
Imports System.Windows.Forms
Imports System.Diagnostics
Imports System.Reflection
#End Region

Public Class TestDataGridCustomHeaders
    Inherits Windows.Forms.DataGrid

    Private pColumnHeaderHeight As Int32 = 26
    Private pColumnYstart As Int32 = 20
    Private pColumnYend As Int32 = 33
    Private pQuietZoneX As Int32 = 2

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub Adjust()
        Dim headerFontHeightFieldInfo As FieldInfo = (New DataGrid).GetType().GetField("headerFontHeight", BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.IgnoreCase)
        headerFontHeightFieldInfo.SetValue(Me, pColumnHeaderHeight)
    End Sub

    Protected Overloads Sub OnLayout(ByVal le As LayoutEventArgs)
        Adjust()
        MyBase.OnLayout(le)
    End Sub

    Protected Overrides Sub OnPaint(ByVal pe As PaintEventArgs)
        Adjust()
        MyBase.OnPaint(pe)
        Dim g As Drawing.Graphics = CreateGraphics()
        g.FillRectangle(New Drawing.SolidBrush(Drawing.Color.LightBlue), pQuietZoneX, pColumnYstart, Me.Width - 2 * pQuietZoneX, pColumnYend)
        g.DrawString("Testing... Testing...", New Drawing.Font("Arial", 8), New Drawing.SolidBrush(Drawing.Color.DarkBlue), pQuietZoneX, pColumnYstart)
    End Sub

End Class
