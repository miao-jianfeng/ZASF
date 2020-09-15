Public Class frmLiuHuaJiAlarm
    Private Sub frmLiuHuaJiAlarm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For Each tsl As TSL In Me.Controls
            With tsl
                .Status = 2
                .EquipNo = tsl.Name.ToUpper.Replace("TSL"， "").ToString
                .CutOffTime = "0:15"
            End With

        Next
        With Tsl1
            .Status = 0
            .CutOffTime = "0:10"
        End With
        With Tsl2
            .Status = 1
            .CutOffTime = "0:12"
        End With
    End Sub

End Class