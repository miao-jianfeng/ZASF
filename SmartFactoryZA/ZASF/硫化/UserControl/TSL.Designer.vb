<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TSL
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TSL))
        Me.lblEquipNo = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.lblTime = New DevExpress.XtraEditors.LabelControl()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton2 = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblEquipNo
        '
        Me.lblEquipNo.Appearance.Font = New System.Drawing.Font("Tahoma", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEquipNo.Appearance.Options.UseFont = True
        Me.lblEquipNo.Location = New System.Drawing.Point(102, 20)
        Me.lblEquipNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lblEquipNo.Name = "lblEquipNo"
        Me.lblEquipNo.Size = New System.Drawing.Size(92, 36)
        Me.lblEquipNo.TabIndex = 0
        Me.lblEquipNo.Text = "X 号机"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(217, 27)
        Me.LabelControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(78, 22)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "生产编号:"
        '
        'PanelControl1
        '
        Me.PanelControl1.ContentImage = CType(resources.GetObject("PanelControl1.ContentImage"), System.Drawing.Image)
        Me.PanelControl1.Location = New System.Drawing.Point(19, 3)
        Me.PanelControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(60, 71)
        Me.PanelControl1.TabIndex = 2
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(310, 27)
        Me.LabelControl3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(161, 22)
        Me.LabelControl3.TabIndex = 3
        Me.LabelControl3.Text = "XXXX-XXXXX--XXXXX"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(538, 27)
        Me.LabelControl4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(90, 22)
        Me.LabelControl4.TabIndex = 4
        Me.LabelControl4.Text = "剩余时间："
        '
        'lblTime
        '
        Me.lblTime.Appearance.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTime.Appearance.ForeColor = System.Drawing.Color.Blue
        Me.lblTime.Appearance.Options.UseFont = True
        Me.lblTime.Appearance.Options.UseForeColor = True
        Me.lblTime.Location = New System.Drawing.Point(637, 24)
        Me.lblTime.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(85, 29)
        Me.lblTime.TabIndex = 3
        Me.lblTime.Text = "00：10"
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Appearance.Font = New System.Drawing.Font("Tahoma", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SimpleButton1.Appearance.Options.UseFont = True
        Me.SimpleButton1.ImageOptions.Image = CType(resources.GetObject("SimpleButton1.ImageOptions.Image"), System.Drawing.Image)
        Me.SimpleButton1.Location = New System.Drawing.Point(759, 4)
        Me.SimpleButton1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(144, 69)
        Me.SimpleButton1.TabIndex = 5
        Me.SimpleButton1.Text = "关闭报警"
        '
        'SimpleButton2
        '
        Me.SimpleButton2.Appearance.Font = New System.Drawing.Font("Tahoma", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SimpleButton2.Appearance.Options.UseFont = True
        Me.SimpleButton2.ImageOptions.Image = CType(resources.GetObject("SimpleButton2.ImageOptions.Image"), System.Drawing.Image)
        Me.SimpleButton2.Location = New System.Drawing.Point(911, 4)
        Me.SimpleButton2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SimpleButton2.Name = "SimpleButton2"
        Me.SimpleButton2.Size = New System.Drawing.Size(144, 69)
        Me.SimpleButton2.TabIndex = 6
        Me.SimpleButton2.Text = "出库"
        '
        'TSL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.SimpleButton2)
        Me.Controls.Add(Me.SimpleButton1)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.lblTime)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.PanelControl1)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.lblEquipNo)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "TSL"
        Me.Size = New System.Drawing.Size(1076, 77)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblEquipNo As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents lblTime As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton2 As DevExpress.XtraEditors.SimpleButton
End Class
