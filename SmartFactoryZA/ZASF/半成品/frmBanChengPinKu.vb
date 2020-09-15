Public Class frmBanChengPinKu
    Private Sub frmBanChengPinKu_Load(sender As Object, e As EventArgs) Handles Me.Load
        ExcelDataSource1.Fill()
    End Sub
End Class