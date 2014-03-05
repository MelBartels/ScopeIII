Public Class FrmTestDataGridView

    Private Class classDataValues
        Private pName As String
        Private pValue As String
        Public Sub New(ByVal name As String, ByVal value As String)
            pName = name
            pValue = value
        End Sub
        Public Property Name() As String
            Get
                Return pName
            End Get
            Set(ByVal value As String)
                pName = value
            End Set
        End Property
        Public Property Value() As String
            Get
                Return pValue
            End Get
            Set(ByVal value As String)
                pValue = value
            End Set
        End Property
    End Class

    Private Function fillDataValues() As ArrayList
        Dim dataValues As New ArrayList

        'DataValues.Add("1")
        'DataValues.Add("12")
        'DataValues.Add("123")

        dataValues.Add(New classDataValues("name1", "value1"))
        dataValues.Add(New classDataValues("name2", "value2"))
        dataValues.Add(New classDataValues("name3", "value3"))

        Return dataValues
    End Function

    Private Sub FrmTestDataGridView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BindingSource1.DataSource = fillDataValues()
        DataGridView1.DataSource = BindingSource1
        DataGridView1.Refresh()
    End Sub
End Class