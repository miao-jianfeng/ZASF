<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LHJ
    Inherits DevExpress.XtraEditors.XtraUserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim ButtonImageOptions1 As DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions = New DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LHJ))
        Dim ButtonImageOptions2 As DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions = New DevExpress.XtraEditors.ButtonsPanelControl.ButtonImageOptions()
        Me.gpNo = New DevExpress.XtraEditors.GroupControl()
        Me.btnMPa = New System.Windows.Forms.Button()
        Me.lblCutOffTime = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.btnTemperature = New System.Windows.Forms.Button()
        CType(Me.gpNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpNo.SuspendLayout()
        Me.SuspendLayout()
        '
        'gpNo
        '
        Me.gpNo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.gpNo.CaptionLocation = DevExpress.Utils.Locations.Top
        Me.gpNo.Controls.Add(Me.btnMPa)
        Me.gpNo.Controls.Add(Me.lblCutOffTime)
        Me.gpNo.Controls.Add(Me.LabelControl3)
        Me.gpNo.Controls.Add(Me.btnTemperature)
        ButtonImageOptions1.Image = CType(resources.GetObject("ButtonImageOptions1.Image"), System.Drawing.Image)
        ButtonImageOptions2.Image = CType(resources.GetObject("ButtonImageOptions2.Image"), System.Drawing.Image)
        Me.gpNo.CustomHeaderButtons.AddRange(New DevExpress.XtraEditors.ButtonPanel.IBaseButton() {New DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("", True, ButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, True, Nothing, True, False, True, Nothing, -1), New DevExpress.XtraEditors.ButtonsPanelControl.GroupBoxButton("", True, ButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, True, Nothing, True, False, True, Nothing, -1)})
        Me.gpNo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gpNo.Location = New System.Drawing.Point(0, 0)
        Me.gpNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gpNo.Name = "gpNo"
        Me.gpNo.Size = New System.Drawing.Size(150, 164)
        Me.gpNo.TabIndex = 0
        Me.gpNo.Text = "x 号机"
        '
        'btnMPa
        '
        Me.btnMPa.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnMPa.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnMPa.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMPa.ForeColor = System.Drawing.Color.Transparent
        Me.btnMPa.Location = New System.Drawing.Point(2, 85)
        Me.btnMPa.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnMPa.Name = "btnMPa"
        Me.btnMPa.Size = New System.Drawing.Size(146, 41)
        Me.btnMPa.TabIndex = 5
        Me.btnMPa.Text = "110 MPa"
        Me.btnMPa.UseVisualStyleBackColor = False
        '
        'lblCutOffTime
        '
        Me.lblCutOffTime.Appearance.Font = New System.Drawing.Font("Tahoma", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCutOffTime.Appearance.ForeColor = System.Drawing.Color.Teal
        Me.lblCutOffTime.Appearance.Options.UseFont = True
        Me.lblCutOffTime.Appearance.Options.UseForeColor = True
        Me.lblCutOffTime.Location = New System.Drawing.Point(46, 37)
        Me.lblCutOffTime.Name = "lblCutOffTime"
        Me.lblCutOffTime.Size = New System.Drawing.Size(61, 25)
        Me.lblCutOffTime.TabIndex = 4
        Me.lblCutOffTime.Text = "2：13"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Location = New System.Drawing.Point(6, 37)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(22, 25)
        Me.LabelControl3.TabIndex = 2
        Me.LabelControl3.Text = "剩"
        '
        'btnTemperature
        '
        Me.btnTemperature.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.btnTemperature.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.btnTemperature.Font = New System.Drawing.Font("Tahoma", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnTemperature.ForeColor = System.Drawing.Color.Green
        Me.btnTemperature.Location = New System.Drawing.Point(2, 126)
        Me.btnTemperature.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnTemperature.Name = "btnTemperature"
        Me.btnTemperature.Size = New System.Drawing.Size(146, 36)
        Me.btnTemperature.TabIndex = 3
        Me.btnTemperature.Text = "115.4 ℃"
        '
        'LHJ
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.gpNo)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "LHJ"
        Me.Size = New System.Drawing.Size(150, 164)
        CType(Me.gpNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpNo.ResumeLayout(False)
        Me.gpNo.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents gpNo As DevExpress.XtraEditors.GroupControl
    Friend WithEvents btnTemperature As Button
    Friend WithEvents lblCutOffTime As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnMPa As Button
End Class
