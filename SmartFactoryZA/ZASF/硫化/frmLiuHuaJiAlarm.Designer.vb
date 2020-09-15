<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLiuHuaJiAlarm
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Tsl1 = New ZASF.TSL()
        Me.Tsl2 = New ZASF.TSL()
        Me.Tsl3 = New ZASF.TSL()
        Me.Tsl4 = New ZASF.TSL()
        Me.Tsl5 = New ZASF.TSL()
        Me.Tsl6 = New ZASF.TSL()
        Me.Tsl7 = New ZASF.TSL()
        Me.SuspendLayout()
        '
        'Tsl1
        '
        Me.Tsl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tsl1.CutOffTime = Nothing
        Me.Tsl1.EquipNo = Nothing
        Me.Tsl1.Location = New System.Drawing.Point(17, 19)
        Me.Tsl1.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.Tsl1.Name = "Tsl1"
        Me.Tsl1.Size = New System.Drawing.Size(1077, 81)
        Me.Tsl1.Status = 0
        Me.Tsl1.TabIndex = 0
        '
        'Tsl2
        '
        Me.Tsl2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tsl2.CutOffTime = Nothing
        Me.Tsl2.EquipNo = Nothing
        Me.Tsl2.Location = New System.Drawing.Point(17, 110)
        Me.Tsl2.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.Tsl2.Name = "Tsl2"
        Me.Tsl2.Size = New System.Drawing.Size(1077, 81)
        Me.Tsl2.Status = 0
        Me.Tsl2.TabIndex = 0
        '
        'Tsl3
        '
        Me.Tsl3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tsl3.CutOffTime = Nothing
        Me.Tsl3.EquipNo = Nothing
        Me.Tsl3.Location = New System.Drawing.Point(17, 201)
        Me.Tsl3.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.Tsl3.Name = "Tsl3"
        Me.Tsl3.Size = New System.Drawing.Size(1077, 81)
        Me.Tsl3.Status = 0
        Me.Tsl3.TabIndex = 0
        '
        'Tsl4
        '
        Me.Tsl4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tsl4.CutOffTime = Nothing
        Me.Tsl4.EquipNo = Nothing
        Me.Tsl4.Location = New System.Drawing.Point(17, 292)
        Me.Tsl4.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.Tsl4.Name = "Tsl4"
        Me.Tsl4.Size = New System.Drawing.Size(1077, 81)
        Me.Tsl4.Status = 0
        Me.Tsl4.TabIndex = 0
        '
        'Tsl5
        '
        Me.Tsl5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tsl5.CutOffTime = Nothing
        Me.Tsl5.EquipNo = Nothing
        Me.Tsl5.Location = New System.Drawing.Point(17, 383)
        Me.Tsl5.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.Tsl5.Name = "Tsl5"
        Me.Tsl5.Size = New System.Drawing.Size(1077, 81)
        Me.Tsl5.Status = 0
        Me.Tsl5.TabIndex = 0
        '
        'Tsl6
        '
        Me.Tsl6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tsl6.CutOffTime = Nothing
        Me.Tsl6.EquipNo = Nothing
        Me.Tsl6.Location = New System.Drawing.Point(17, 475)
        Me.Tsl6.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.Tsl6.Name = "Tsl6"
        Me.Tsl6.Size = New System.Drawing.Size(1077, 81)
        Me.Tsl6.Status = 0
        Me.Tsl6.TabIndex = 0
        '
        'Tsl7
        '
        Me.Tsl7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Tsl7.CutOffTime = Nothing
        Me.Tsl7.EquipNo = Nothing
        Me.Tsl7.Location = New System.Drawing.Point(17, 566)
        Me.Tsl7.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.Tsl7.Name = "Tsl7"
        Me.Tsl7.Size = New System.Drawing.Size(1077, 81)
        Me.Tsl7.Status = 0
        Me.Tsl7.TabIndex = 0
        '
        'frmLiuHuaJiAlarm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 22.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1124, 671)
        Me.Controls.Add(Me.Tsl7)
        Me.Controls.Add(Me.Tsl6)
        Me.Controls.Add(Me.Tsl5)
        Me.Controls.Add(Me.Tsl4)
        Me.Controls.Add(Me.Tsl3)
        Me.Controls.Add(Me.Tsl2)
        Me.Controls.Add(Me.Tsl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmLiuHuaJiAlarm"
        Me.Text = "待出炉提示画面"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Tsl1 As TSL
    Friend WithEvents Tsl2 As TSL
    Friend WithEvents Tsl3 As TSL
    Friend WithEvents Tsl4 As TSL
    Friend WithEvents Tsl5 As TSL
    Friend WithEvents Tsl6 As TSL
    Friend WithEvents Tsl7 As TSL
End Class
