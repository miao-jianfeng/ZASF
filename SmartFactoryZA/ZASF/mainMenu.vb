Imports DevExpress.XtraReports.UI

Public Class mainMenu
    Private Sub mainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        'イニシャルファイル情報取得
        g_szInitFileName = My.Application.Info.DirectoryPath & "\ZASF.ini"

        g_Server.szDrive = GetIniFile("SERVER", "DRIVE", "PostgreSQL ANSI", g_szInitFileName)
        g_Server.szIPadr = GetIniFile("SERVER", "IP", "", g_szInitFileName)
        g_Server.szDbn = GetIniFile("SERVER", "DBN", "atlas", g_szInitFileName)
        g_Server.szUid = GetIniFile("SERVER", "UID", "atlas", g_szInitFileName)
        g_Server.szPas = GetIniFile("SERVER", "PAS", "atlas", g_szInitFileName)
        g_Server.iOver = CInt(GetIniFile("SERVER", "OVER", "-1", g_szInitFileName))

        If False = g_fDBconnect And 0 = g_iDBconnResult Then
            If "" = g_Server.szIPadr Or "0.0.0.0" = g_Server.szIPadr Then
            Else
                g_fDBconnect = True

                'データベース接続
                With g_Server
                    g_iDBconnResult = DBacs.Connect(.szDrive, .szIPadr, .szDbn, .szUid, .szPas, .iOver)
                    If 1 <> g_iDBconnResult Then
                        .szDrive = CStr(IIf("PostgreSQL" = .szDrive, "PostgreSQL ANSI", "PostgreSQL"))
                        g_iDBconnResult = DBacs.Connect(.szDrive, .szIPadr, .szDbn, .szUid, .szPas, .iOver)
                    End If
                End With
            End If
        End If

        setAllErrMsg()

    End Sub
    Public Sub setAllErrMsg()
        'メッセージが取得済みの場合は設定しない
        If htMsgMst.Count = 0 Then
            Dim objmsg As New clsMessage
            Dim dtMsgMst As DataTable = objmsg.GetAllMsg()
            For Each dr In dtMsgMst.Rows
                With htMsgMst
                    .Add(dr("msg_id"), dr("msg"))
                End With
            Next
        End If
    End Sub

    Private Sub btnflb_Click(sender As Object, e As EventArgs) Handles btnflb.Click
        Dim a As New frmFLBinfo
        a.ShowDialog()
    End Sub

    Private Sub btnQxgl_Click(sender As Object, e As EventArgs) Handles btnQxgl.Click
        Dim a As New frmTermManagement
        a.ShowDialog()
    End Sub

    Private Sub btnCunFang_Click(sender As Object, e As EventArgs) Handles btnCunFang.Click
        Dim a As New frmCunFang
        a.ShowDialog()
    End Sub

    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        Dim a As New frmGJBassembling
        a.ShowDialog()
    End Sub

    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        Dim a As New frmGJBHistoryList
        a.ShowDialog()
    End Sub

    Private Sub SimpleButton6_Click(sender As Object, e As EventArgs) Handles SimpleButton6.Click
        Dim a As New frmJPinfo
        a.ShowDialog()
    End Sub

    Private Sub SimpleButton8_Click(sender As Object, e As EventArgs) Handles SimpleButton8.Click
        Dim a As New frmPreOrganization
        a.ShowDialog()
    End Sub

    Private Sub SimpleButton9_Click(sender As Object, e As EventArgs) Handles SimpleButton9.Click
        Dim a As New frmPreOrganizationEdit
        a.ShowDialog()
    End Sub

    Private Sub SimpleButton10_Click(sender As Object, e As EventArgs) Handles SimpleButton10.Click
        Dim a As New frmSetBaoHuJiao
        a.ShowDialog()
    End Sub

    Private Sub SimpleButton7_Click(sender As Object, e As EventArgs) Handles SimpleButton7.Click
        Dim a As New frmJPHistroyList
        a.ShowDialog()
    End Sub

    Private Sub SimpleButton15_Click(sender As Object, e As EventArgs) Handles SimpleButton15.Click
        Dim a As New frmLiuHuaJi
        a.ShowDialog()
    End Sub

    Private Sub SimpleButton14_Click(sender As Object, e As EventArgs) Handles SimpleButton14.Click
        Dim a As New frmLiuHuaJiList
        a.ShowDialog()
    End Sub

    Private Sub SimpleButton13_Click(sender As Object, e As EventArgs) Handles SimpleButton13.Click
        Dim a As New frmLiuHuaJiAlarm
        a.ShowDialog()
    End Sub

    Private Sub SimpleButton16_Click(sender As Object, e As EventArgs) Handles SimpleButton16.Click
        Dim a As New frmHWParamSetting
        a.ShowDialog()
    End Sub

    Private Sub SimpleButton12_Click(sender As Object, e As EventArgs) Handles SimpleButton12.Click
        Dim a As New frmProdMaster
        a.ShowDialog()
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Dim a As New frmProdMasterDetail
        a.ShowDialog()
    End Sub

    Private Sub btnBasketInfo_Click(sender As Object, e As EventArgs) Handles btnBasketInfo.Click
        Dim a As New frmBasketInfo
        a.ShowDialog()
    End Sub

    Private Sub GroupControl1_Paint(sender As Object, e As PaintEventArgs) Handles GroupControl1.Paint

    End Sub
End Class