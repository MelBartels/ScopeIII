Imports BartelsLibrary.DelegateSigs

Public Class UserCtrlTerminal
    Inherits MVPUserCtrlBase
    Implements IUserCtrlTerminal

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ErrorProvider As System.Windows.Forms.ErrorProvider
    Friend WithEvents cmbBxPortType As System.Windows.Forms.ComboBox
    Friend WithEvents btnSettings As System.Windows.Forms.Button
    Friend WithEvents btnOpen As System.Windows.Forms.Button
    Friend WithEvents rTxBxDisplay As System.Windows.Forms.RichTextBox
    Friend WithEvents chBxAppendReturn As System.Windows.Forms.CheckBox
    Friend WithEvents cmbBxDisplayType As System.Windows.Forms.ComboBox
    Friend WithEvents btnSendByteCodes As System.Windows.Forms.Button
    Friend WithEvents btnSendText As System.Windows.Forms.Button
    Friend WithEvents txBxSendText As System.Windows.Forms.TextBox
    Friend WithEvents txBxSendByteCodes As System.Windows.Forms.TextBox
    Friend WithEvents lblDisplayType As System.Windows.Forms.Label
    Friend WithEvents UserCtrlLogging As ScopeIII.Forms.UserCtrlLogging
    Friend WithEvents lblSelectPort As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ErrorProvider = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmbBxPortType = New System.Windows.Forms.ComboBox
        Me.btnOpen = New System.Windows.Forms.Button
        Me.btnSettings = New System.Windows.Forms.Button
        Me.btnSendText = New System.Windows.Forms.Button
        Me.txBxSendText = New System.Windows.Forms.TextBox
        Me.rTxBxDisplay = New System.Windows.Forms.RichTextBox
        Me.chBxAppendReturn = New System.Windows.Forms.CheckBox
        Me.cmbBxDisplayType = New System.Windows.Forms.ComboBox
        Me.txBxSendByteCodes = New System.Windows.Forms.TextBox
        Me.btnSendByteCodes = New System.Windows.Forms.Button
        Me.lblDisplayType = New System.Windows.Forms.Label
        Me.lblSelectPort = New System.Windows.Forms.Label
        Me.UserCtrlLogging = New ScopeIII.Forms.UserCtrlLogging
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ErrorProvider
        '
        Me.ErrorProvider.ContainerControl = Me
        '
        'ToolTip
        '
        Me.ToolTip.AutoPopDelay = 30000
        Me.ToolTip.InitialDelay = 500
        Me.ToolTip.ReshowDelay = 100
        '
        'cmbBxPortType
        '
        Me.cmbBxPortType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBxPortType.Location = New System.Drawing.Point(68, 3)
        Me.cmbBxPortType.Name = "cmbBxPortType"
        Me.cmbBxPortType.Size = New System.Drawing.Size(80, 21)
        Me.cmbBxPortType.TabIndex = 13
        '
        'btnOpen
        '
        Me.btnOpen.Location = New System.Drawing.Point(235, 3)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(75, 23)
        Me.btnOpen.TabIndex = 11
        Me.btnOpen.Text = "Open"
        '
        'btnSettings
        '
        Me.btnSettings.Location = New System.Drawing.Point(154, 3)
        Me.btnSettings.Name = "btnSettings"
        Me.btnSettings.Size = New System.Drawing.Size(75, 23)
        Me.btnSettings.TabIndex = 10
        Me.btnSettings.Text = "Settings"
        '
        'btnSendText
        '
        Me.btnSendText.Location = New System.Drawing.Point(336, 30)
        Me.btnSendText.Name = "btnSendText"
        Me.btnSendText.Size = New System.Drawing.Size(75, 23)
        Me.btnSendText.TabIndex = 9
        Me.btnSendText.Text = "Send Text"
        '
        'txBxSendText
        '
        Me.txBxSendText.Location = New System.Drawing.Point(11, 32)
        Me.txBxSendText.Name = "txBxSendText"
        Me.txBxSendText.Size = New System.Drawing.Size(218, 20)
        Me.txBxSendText.TabIndex = 8
        '
        'rTxBxDisplay
        '
        Me.rTxBxDisplay.Location = New System.Drawing.Point(3, 113)
        Me.rTxBxDisplay.Name = "rTxBxDisplay"
        Me.rTxBxDisplay.ReadOnly = True
        Me.rTxBxDisplay.Size = New System.Drawing.Size(413, 174)
        Me.rTxBxDisplay.TabIndex = 14
        Me.rTxBxDisplay.Text = ""
        '
        'chBxAppendReturn
        '
        Me.chBxAppendReturn.Location = New System.Drawing.Point(235, 32)
        Me.chBxAppendReturn.Name = "chBxAppendReturn"
        Me.chBxAppendReturn.Size = New System.Drawing.Size(100, 24)
        Me.chBxAppendReturn.TabIndex = 15
        Me.chBxAppendReturn.Text = "Append Return"
        '
        'cmbBxDisplayType
        '
        Me.cmbBxDisplayType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBxDisplayType.Location = New System.Drawing.Point(68, 86)
        Me.cmbBxDisplayType.Name = "cmbBxDisplayType"
        Me.cmbBxDisplayType.Size = New System.Drawing.Size(80, 21)
        Me.cmbBxDisplayType.TabIndex = 16
        '
        'txBxSendByteCodes
        '
        Me.txBxSendByteCodes.Location = New System.Drawing.Point(11, 60)
        Me.txBxSendByteCodes.Name = "txBxSendByteCodes"
        Me.txBxSendByteCodes.Size = New System.Drawing.Size(319, 20)
        Me.txBxSendByteCodes.TabIndex = 17
        '
        'btnSendByteCodes
        '
        Me.btnSendByteCodes.Location = New System.Drawing.Point(336, 58)
        Me.btnSendByteCodes.Name = "btnSendByteCodes"
        Me.btnSendByteCodes.Size = New System.Drawing.Size(75, 23)
        Me.btnSendByteCodes.TabIndex = 18
        Me.btnSendByteCodes.Text = "Send Bytes"
        '
        'lblDisplayType
        '
        Me.lblDisplayType.Location = New System.Drawing.Point(3, 83)
        Me.lblDisplayType.Name = "lblDisplayType"
        Me.lblDisplayType.Size = New System.Drawing.Size(59, 24)
        Me.lblDisplayType.TabIndex = 19
        Me.lblDisplayType.Text = "Display in"
        Me.lblDisplayType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSelectPort
        '
        Me.lblSelectPort.Location = New System.Drawing.Point(3, 0)
        Me.lblSelectPort.Name = "lblSelectPort"
        Me.lblSelectPort.Size = New System.Drawing.Size(59, 24)
        Me.lblSelectPort.TabIndex = 21
        Me.lblSelectPort.Text = "Select Port"
        Me.lblSelectPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'UserCtrlLogging
        '
        Me.UserCtrlLogging.Location = New System.Drawing.Point(151, 84)
        Me.UserCtrlLogging.LoggingFilename = "Logging Filename"
        Me.UserCtrlLogging.Name = "UserCtrlLogging"
        Me.UserCtrlLogging.Size = New System.Drawing.Size(267, 26)
        Me.UserCtrlLogging.TabIndex = 22
        '
        'UserCtrlTerminal
        '
        Me.Controls.Add(Me.UserCtrlLogging)
        Me.Controls.Add(Me.lblSelectPort)
        Me.Controls.Add(Me.lblDisplayType)
        Me.Controls.Add(Me.btnSendByteCodes)
        Me.Controls.Add(Me.txBxSendByteCodes)
        Me.Controls.Add(Me.cmbBxDisplayType)
        Me.Controls.Add(Me.chBxAppendReturn)
        Me.Controls.Add(Me.rTxBxDisplay)
        Me.Controls.Add(Me.cmbBxPortType)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.btnSettings)
        Me.Controls.Add(Me.btnSendText)
        Me.Controls.Add(Me.txBxSendText)
        Me.Name = "UserCtrlTerminal"
        Me.Size = New System.Drawing.Size(420, 291)
        CType(Me.ErrorProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Public OpenColor As Drawing.Color = System.Drawing.Color.White
    Public ClosedColor As Drawing.Color = System.Drawing.Color.LightGray
    Public XmitColor As Drawing.Color = System.Drawing.Color.Red
    Public RecColor As Drawing.Color = System.Drawing.Color.Black
    Public StatusColor As Drawing.Color = System.Drawing.Color.Green
    Public DefaultColor As Drawing.Color = System.Drawing.Color.Blue

    Public Event PortType(ByVal portDesc As String)
    Public Event Settings()
    Public Event OpenClose()
    Public Event SendText(ByVal text As String)
    Public Event SendByteCodes(ByVal bytes As Byte())
    Public Event DisplayAsHex(ByVal typeToDisplay As Object)
    Public Event Shutdown()
    Public Event Logging(ByVal switch As Boolean)

    Public FireEvents As Boolean

    Private pErrorProviderFacade As ErrorProviderFacade
    Private pLastTextWasRec As Boolean

    Public Property PortTypeDataSource() As Object
        Get
            Return cmbBxPortType.DataSource
        End Get
        Set(ByVal Value As Object)
            cmbBxPortType.DataSource = Value
        End Set
    End Property

    Public Property DisplayTypeDataSource() As Object
        Get
            Return cmbBxDisplayType.DataSource
        End Get
        Set(ByVal Value As Object)
            cmbBxDisplayType.DataSource = Value
        End Set
    End Property

    Public Sub SetToolTip()
        ToolTip.SetToolTip(cmbBxPortType, "Select the type of port.")
        ToolTip.SetToolTip(btnSettings, "Open a dialog handling the port's settings.")
        ToolTip.SetToolTip(btnOpen, "Open/Close the port.")
        ToolTip.SetToolTip(chBxAppendReturn, "Append a <return> to the end of every text string.")
        ToolTip.SetToolTip(txBxSendText, "Enter text to send.")
        ToolTip.SetToolTip(btnSendText, "Send the displayed text.")
        ToolTip.SetToolTip(btnSendByteCodes, "Send the displayed byte codes.")
        ToolTip.SetToolTip(txBxSendByteCodes, "Enter byte codes to send. " _
                            & Environment.NewLine _
                            & "Enter as integers, ie, 72, or in hex, ie, x48. " _
                            & Environment.NewLine _
                            & "Delimit each byte code with a space, eg, 72 x48 xD.")
        ToolTip.SetToolTip(rTxBxDisplay, "Display of transmitted and received data. " _
                            & Environment.NewLine _
                            & "Grayed out if port is not active.")
        ToolTip.SetToolTip(cmbBxDisplayType, "Set the display type.")

        UserCtrlLogging.SetToolTip()
        UserCtrlLogging.SetTxBxLoggingToolTip()

        ToolTip.IsBalloon = True
    End Sub

    Public Sub SetOpenCloseState(ByVal state As Boolean)
        If state Then
            btnOpen.Text = BartelsLibrary.Constants.ClosePort
            rTxBxDisplay.BackColor = OpenColor
        Else
            btnOpen.Text = BartelsLibrary.Constants.OpenPort
            rTxBxDisplay.BackColor = ClosedColor
        End If

        cmbBxPortType.Enabled = Not state
        btnSettings.Enabled = Not state
        btnSendText.Enabled = state
        btnSendByteCodes.Enabled = state
    End Sub

    Public Sub SetPortType(ByVal portType As ISFT)
        If cmbBxPortType.InvokeRequired Then
            cmbBxPortType.Invoke(New DelegateISFT(AddressOf SetPortType), New Object() {portType})
        Else
            cmbBxPortType.SelectedItem = portType.Description
        End If
    End Sub

    Public Overridable Function AppendText(ByRef [object] As Object) As Boolean Implements IUserCtrlTerminal.AppendText
        If rTxBxDisplay.InvokeRequired Then
            rTxBxDisplay.Invoke(New DelegateObjAsBool(AddressOf AppendText), New Object() {[object]})
        Else
            Dim text As String = CStr([object])
            ' note when text is nothing or empty
            If String.IsNullOrEmpty(text) Then
                Dim sb As New Text.StringBuilder
                sb.Append(vbCrLf)
                sb.Append(IOState.Status.Description)
                sb.Append("Error: unexpectedly received empty text.")
                sb.Append(vbCrLf)
                text = sb.ToString
            End If

            ' guess at selection color
            If text.IndexOf(IOState.Status.Description) > -1 Then
                rTxBxDisplay.SelectionColor = StatusColor
                pLastTextWasRec = False
            ElseIf text.IndexOf(IOState.Xmt.Description) > -1 Then
                rTxBxDisplay.SelectionColor = XmitColor
                pLastTextWasRec = False
            ElseIf text.IndexOf(IOState.Rec.Description) > -1 OrElse pLastTextWasRec Then
                rTxBxDisplay.SelectionColor = RecColor
                pLastTextWasRec = True
            Else
                rTxBxDisplay.SelectionColor = DefaultColor
            End If

            ' append to text display
            rTxBxDisplay.AppendText(text)
            rTxBxDisplay.SelectionStart = rTxBxDisplay.Text.Length
            ' RichTextBox's ScrollToCaret needs focus to work (ordinary TextBox doesn't need focus to ScrollToCaret)
            rTxBxDisplay.Focus()
            rTxBxDisplay.ScrollToCaret()
        End If
    End Function

    Private Sub cmbBxPortTypeSelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBxPortType.SelectedIndexChanged
        If FireEvents Then
            RaiseEvent PortType(CStr(cmbBxPortType.SelectedItem))
        End If
    End Sub

    Private Sub btnSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSettings.Click
        If FireEvents Then
            RaiseEvent Settings()
        End If
    End Sub

    Private Sub btnOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpen.Click
        If FireEvents Then
            RaiseEvent OpenClose()
        End If
    End Sub

    Private Sub btnSendText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendText.Click
        If chBxAppendReturn.Checked Then
            txBxSendText.Text += vbCr
        End If
        If FireEvents Then
            RaiseEvent SendText(txBxSendText.Text)
        End If
    End Sub

    Private Sub btnSendByteCodes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSendByteCodes.Click
        Dim bytes As Byte() = getValidByteCodes()
        If bytes IsNot Nothing Then
            If FireEvents Then
                RaiseEvent SendByteCodes(bytes)
            End If
        End If
    End Sub

    Private Sub cmbBxDisplayType_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBxDisplayType.SelectedValueChanged
        If FireEvents Then
            RaiseEvent DisplayAsHex(cmbBxDisplayType.SelectedValue)
        End If
    End Sub

    Private Function getValidByteCodes() As Byte()
        If pErrorProviderFacade Is Nothing Then
            pErrorProviderFacade = ErrorProviderFacade.GetInstance
        End If
        pErrorProviderFacade.Clear(ErrorProvider, txBxSendByteCodes)

        Dim st As StringTokenizer = StringTokenizer.GetInstance
        st.Tokenize(txBxSendByteCodes.Text)

        Dim byteCodeArray As New ArrayList
        Dim byteCodesToSend As Byte()
        Dim ix As Int32
        For ix = 0 To st.Count - 1
            Dim s As String = st.NextToken
            Dim num As Int32 = 0
            Try
                Dim hexIx As Int32 = s.IndexOf("x")
                If hexIx > -1 Then
                    Int32.TryParse(s.Substring(hexIx + 1), Globalization.NumberStyles.HexNumber, Nothing, num)
                Else
                    Int32.TryParse(s, num)
                End If
                byteCodeArray.Add(num)
            Catch ex As Exception
                pErrorProviderFacade.ShowNonNumeric(ErrorProvider, txBxSendByteCodes)
                Return Nothing
            End Try
        Next
        ReDim byteCodesToSend(byteCodeArray.Count - 1)
        ix = 0
        For Each num As Int32 In byteCodeArray
            byteCodesToSend(ix) = CByte(num)
            ix += 1
        Next
        Return byteCodesToSend
    End Function

    Private Sub userCtrlLoggingHandler(ByVal switch As Boolean) Handles UserCtrlLogging.Logging
        If FireEvents Then
            RaiseEvent Logging(switch)
        End If
    End Sub
End Class
