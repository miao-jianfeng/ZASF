Public Class frmLiuHuaJiList
    Private Sub frmLiuHuaJiList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DevExpress.Skins.SkinManager.EnableFormSkins()

        For Each lhj As LHJ In Me.Controls
            With lhj
                .NetworkStatus = True
                .Status = True
                .Temperature = "150.1 ℃"
                .EquipmentNo = lhj.Name.ToUpper.Replace("LHJ"， "").ToString & " 号机"
                .CutOffTime = "1:31"
            End With

        Next
        With Lhj3
            .NetworkStatus = True
            .Status = False
            .Temperature = "180.12"
            .EquipmentNo = "3 号机"
            .CutOffTime = "2:31"
        End With
        With Lhj4
            .NetworkStatus = False

        End With

    End Sub
End Class