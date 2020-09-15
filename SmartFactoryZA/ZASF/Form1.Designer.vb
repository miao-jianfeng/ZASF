<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.ip = New System.Windows.Forms.TextBox()
        Me.PORT = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtMSG = New System.Windows.Forms.TextBox()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(235, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(166, 77)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Connect"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(62, 125)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(127, 45)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "SendMessage"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(465, 12)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(166, 77)
        Me.Button3.TabIndex = 5
        Me.Button3.Text = "DisConnect"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 12
        Me.ListBox1.Location = New System.Drawing.Point(12, 183)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(720, 220)
        Me.ListBox1.TabIndex = 2
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(566, 135)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(108, 35)
        Me.Button4.TabIndex = 3
        Me.Button4.Text = "Button4"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(680, 136)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(75, 32)
        Me.Button5.TabIndex = 4
        Me.Button5.Text = "Button5"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'ip
        '
        Me.ip.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.ip.Location = New System.Drawing.Point(62, 19)
        Me.ip.Name = "ip"
        Me.ip.Size = New System.Drawing.Size(216, 21)
        Me.ip.TabIndex = 0
        Me.ip.Text = "172.29.103.200"
        '
        'PORT
        '
        Me.PORT.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.PORT.Location = New System.Drawing.Point(62, 51)
        Me.PORT.Name = "PORT"
        Me.PORT.Size = New System.Drawing.Size(216, 21)
        Me.PORT.TabIndex = 1
        Me.PORT.Text = "5001"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(39, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(17, 12)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "IP"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 12)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "PORT"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 101)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 12)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Message"
        '
        'txtMSG
        '
        Me.txtMSG.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtMSG.Location = New System.Drawing.Point(62, 98)
        Me.txtMSG.Name = "txtMSG"
        Me.txtMSG.Size = New System.Drawing.Size(569, 21)
        Me.txtMSG.TabIndex = 2
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(309, 133)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(114, 34)
        Me.Button6.TabIndex = 9
        Me.Button6.Text = "Button6"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(755, 427)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.txtMSG)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PORT)
        Me.Controls.Add(Me.ip)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents ip As TextBox
    Friend WithEvents PORT As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtMSG As TextBox
    Friend WithEvents Button6 As Button
End Class
