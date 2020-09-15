<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBanChengPinKu
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBanChengPinKu))
        Dim FieldInfo1 As DevExpress.DataAccess.Excel.FieldInfo = New DevExpress.DataAccess.Excel.FieldInfo()
        Dim FieldInfo2 As DevExpress.DataAccess.Excel.FieldInfo = New DevExpress.DataAccess.Excel.FieldInfo()
        Dim FieldInfo3 As DevExpress.DataAccess.Excel.FieldInfo = New DevExpress.DataAccess.Excel.FieldInfo()
        Dim FieldInfo4 As DevExpress.DataAccess.Excel.FieldInfo = New DevExpress.DataAccess.Excel.FieldInfo()
        Dim FieldInfo5 As DevExpress.DataAccess.Excel.FieldInfo = New DevExpress.DataAccess.Excel.FieldInfo()
        Dim ExcelWorksheetSettings1 As DevExpress.DataAccess.Excel.ExcelWorksheetSettings = New DevExpress.DataAccess.Excel.ExcelWorksheetSettings()
        Dim ExcelSourceOptions1 As DevExpress.DataAccess.Excel.ExcelSourceOptions = New DevExpress.DataAccess.Excel.ExcelSourceOptions(ExcelWorksheetSettings1)
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.库位号DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.生产编号DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.入库时间DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.存放时长DataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.呼叫AGV = New System.Windows.Forms.DataGridViewButtonColumn()
        Me.ExcelDataSource1 = New DevExpress.DataAccess.Excel.ExcelDataSource()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.库位号DataGridViewTextBoxColumn, Me.生产编号DataGridViewTextBoxColumn, Me.入库时间DataGridViewTextBoxColumn, Me.存放时长DataGridViewTextBoxColumn, Me.呼叫AGV})
        Me.DataGridView1.DataSource = Me.ExcelDataSource1
        Me.DataGridView1.Location = New System.Drawing.Point(20, 8)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 62
        Me.DataGridView1.RowTemplate.Height = 30
        Me.DataGridView1.Size = New System.Drawing.Size(766, 277)
        Me.DataGridView1.TabIndex = 0
        '
        '库位号DataGridViewTextBoxColumn
        '
        Me.库位号DataGridViewTextBoxColumn.DataPropertyName = "库位号"
        Me.库位号DataGridViewTextBoxColumn.HeaderText = "库位号"
        Me.库位号DataGridViewTextBoxColumn.MinimumWidth = 8
        Me.库位号DataGridViewTextBoxColumn.Name = "库位号DataGridViewTextBoxColumn"
        Me.库位号DataGridViewTextBoxColumn.ReadOnly = True
        Me.库位号DataGridViewTextBoxColumn.Width = 150
        '
        '生产编号DataGridViewTextBoxColumn
        '
        Me.生产编号DataGridViewTextBoxColumn.DataPropertyName = "生产编号"
        Me.生产编号DataGridViewTextBoxColumn.HeaderText = "生产编号"
        Me.生产编号DataGridViewTextBoxColumn.MinimumWidth = 8
        Me.生产编号DataGridViewTextBoxColumn.Name = "生产编号DataGridViewTextBoxColumn"
        Me.生产编号DataGridViewTextBoxColumn.ReadOnly = True
        Me.生产编号DataGridViewTextBoxColumn.Width = 150
        '
        '入库时间DataGridViewTextBoxColumn
        '
        Me.入库时间DataGridViewTextBoxColumn.DataPropertyName = "入库时间"
        Me.入库时间DataGridViewTextBoxColumn.HeaderText = "入库时间"
        Me.入库时间DataGridViewTextBoxColumn.MinimumWidth = 8
        Me.入库时间DataGridViewTextBoxColumn.Name = "入库时间DataGridViewTextBoxColumn"
        Me.入库时间DataGridViewTextBoxColumn.ReadOnly = True
        Me.入库时间DataGridViewTextBoxColumn.Width = 150
        '
        '存放时长DataGridViewTextBoxColumn
        '
        Me.存放时长DataGridViewTextBoxColumn.DataPropertyName = "存放时长"
        Me.存放时长DataGridViewTextBoxColumn.HeaderText = "存放时长"
        Me.存放时长DataGridViewTextBoxColumn.MinimumWidth = 8
        Me.存放时长DataGridViewTextBoxColumn.Name = "存放时长DataGridViewTextBoxColumn"
        Me.存放时长DataGridViewTextBoxColumn.ReadOnly = True
        Me.存放时长DataGridViewTextBoxColumn.Width = 150
        '
        '呼叫AGV
        '
        Me.呼叫AGV.DataPropertyName = "呼叫AGV"
        Me.呼叫AGV.HeaderText = "呼叫AGV"
        Me.呼叫AGV.MinimumWidth = 8
        Me.呼叫AGV.Name = "呼叫AGV"
        Me.呼叫AGV.ReadOnly = True
        Me.呼叫AGV.Width = 150
        '
        'ExcelDataSource1
        '
        Me.ExcelDataSource1.FileName = "C:\Users\86132\Desktop\kuCun.xlsx"
        Me.ExcelDataSource1.Name = "ExcelDataSource1"
        Me.ExcelDataSource1.ResultSchemaSerializable = resources.GetString("ExcelDataSource1.ResultSchemaSerializable")
        FieldInfo1.Name = "库位号"
        FieldInfo1.Type = GetType(String)
        FieldInfo2.Name = "生产编号"
        FieldInfo2.Type = GetType(String)
        FieldInfo3.Name = "入库时间"
        FieldInfo3.Type = GetType(Date)
        FieldInfo4.Name = "存放时长"
        FieldInfo4.Type = GetType(String)
        FieldInfo5.Name = "呼叫AGV"
        FieldInfo5.Type = GetType(String)
        Me.ExcelDataSource1.Schema.AddRange(New DevExpress.DataAccess.Excel.FieldInfo() {FieldInfo1, FieldInfo2, FieldInfo3, FieldInfo4, FieldInfo5})
        ExcelWorksheetSettings1.CellRange = Nothing
        ExcelWorksheetSettings1.WorksheetName = "Sheet1"
        ExcelSourceOptions1.ImportSettings = ExcelWorksheetSettings1
        Me.ExcelDataSource1.SourceOptions = ExcelSourceOptions1
        '
        'frmBanChengPinKu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(797, 300)
        Me.Controls.Add(Me.DataGridView1)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "frmBanChengPinKu"
        Me.Text = "半成品库"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents ExcelDataSource1 As DevExpress.DataAccess.Excel.ExcelDataSource
    Friend WithEvents 库位号DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents 生产编号DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents 入库时间DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents 存放时长DataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents 呼叫AGV As DataGridViewButtonColumn
End Class
