﻿Public Class frmHWParamSetting
    Private objHWSetting As New clsHWSetting
    '追加 更新区分
    Private INSERT_TYPE As String = "I"
    Private UPDATE_TYPE As String = "U"
    Private DELETE_TYPE As String = "D"

    '各车间用的dataTable
    Private dtGJB As DataTable
    Private dtJP As DataTable
    Private dtLH As DataTable
    Private dtAGV As DataTable

    Private GJB_IUType As String = UPDATE_TYPE
    Private JP_IUType As String = UPDATE_TYPE
    Private LH_IUType As String = UPDATE_TYPE
    Private AGV_IUType As String = UPDATE_TYPE
    '骨架板车间用Entity
    Private old_gjb As New entity_gjb
    Private new_gjb As New entity_gjb
    '胶片车间用Entity
    Private old_jp As New entity_jp
    Private new_jp As New entity_jp
    '硫化车间用Entity
    Private old_lh As New entity_lhAgv
    Private new_lh As New entity_lhAgv
    'agv用Entity
    Private old_agv As New entity_lhAgv
    Private new_agv As New entity_lhAgv


    Private Sub frmHWParamSetting_Load(sender As Object, e As EventArgs) Handles Me.Load
        tabWorkshop.SelectedTabPage = tp_gjb
        '设定骨架板车间
        setGJB()

        '设定胶片车间
        setJP()

        '设定硫化车间
        setLH()

        '设定AGV
        setAGV
    End Sub
    Private Sub setAGV()
        '按车间ID,ID查询数据

        dtAGV = objHWSetting.selectByType(g_ws_lh， g_equiptype_AGV)

        dtAGV.Columns.Add("status_name", Type.GetType("System.String"))
        For i As Integer = 0 To dtAGV.Rows.Count - 1
            dtAGV(i)("status_name") = getStatusByNo(dtAGV(i)("status"))
        Next
        dtAGV.AcceptChanges()

        Me.dgvList_AGV.DataSource = dtAGV
    End Sub
    Private Sub setJP()
        Dim drsYJB As DataRow()
        Dim drsYBB As DataRow()
        Dim drsDZC As DataRow()
        Dim drsSMQ As DataRow()
        Dim drsDPDZC As DataRow()
        Dim drsBHJDZC As DataRow()

        '按车间ID查询数据
        dtJP = objHWSetting.selectByWorkshop(g_ws_jp)
        '有数据，则标志为更新，否则，标志为追加
        JP_IUType = INSERT_TYPE
        If dtJP.Rows.Count > 0 Then
            JP_IUType = UPDATE_TYPE
            '运胶臂
            drsYJB = dtJP.Select("equip_type_id='" & g_equipType_yjb & "'")
            If drsYJB.Length > 0 Then
                jp_yjb_id.Text = drsYJB(0)("equip_id")
                jp_yjb_ip.Text = drsYJB(0)("ip")
                jp_yjb_port.Text = drsYJB(0)("port")
            End If
            '运板臂
            drsYBB = dtJP.Select("equip_type_id='" & g_equipType_ybb & "'")
            If drsYBB.Length > 0 Then
                jp_ybb_id.Text = drsYBB(0)("equip_id")
                jp_ybb_ip.Text = drsYBB(0)("ip")
                jp_ybb_port.Text = drsYBB(0)("port")
            End If
            '电子秤
            drsDZC = dtJP.Select("equip_type_id='" & g_equipType_dzc & "'")
            If drsDZC.Length > 0 Then
                jp_dzc_id.Text = drsDZC(0)("equip_id")
                jp_dzc_ip.Text = drsDZC(0)("ip")
                jp_dzc_port.Text = drsDZC(0)("port")

            End If
            '打描枪
            drsSMQ = dtJP.Select("equip_type_id='" & g_equipType_smq & "'")
            If drsSMQ.Length > 0 Then
                jp_smq_com.Text = drsSMQ(0)("com")
            End If
            '端片 电子秤
            drsDPDZC = dtJP.Select("equip_type_id='" & g_equipType_dpdzc & "'")
            If drsDPDZC.Length > 0 Then
                jp_dpdzc_id.Text = drsDPDZC(0)("equip_id")
                jp_dpdzc_ip.Text = drsDPDZC(0)("ip")
                jp_dpdzc_port.Text = drsDPDZC(0)("port")
            End If
            '保护胶 电子秤
            drsBHJDZC = dtJP.Select("equip_type_id='" & g_equipType_bhjdzc & "'")
            If drsBHJDZC.Length > 0 Then
                jp_bhjdzc_id.Text = drsBHJDZC(0)("equip_id")
                jp_bhjdzc_ip.Text = drsBHJDZC(0)("ip")
                jp_bhjdzc_port.Text = drsBHJDZC(0)("port")
            End If

            setJPEntity(old_jp)

        End If
    End Sub
    Private Sub setGJB()
        '按车间ID查询数据
        dtGJB = objHWSetting.selectByWorkshop(g_ws_gjb)
        Dim drsJXB As DataRow()
        Dim drsDZC As DataRow()
        Dim drsSMQ1 As DataRow()
        Dim drsSMQ2 As DataRow()

        '有数据，则标志为更新，否则，标志为追加
        GJB_IUType = INSERT_TYPE
        If dtGJB.Rows.Count > 0 Then
            GJB_IUType = UPDATE_TYPE
            drsJXB = dtGJB.Select("equip_type_id='" & g_equipType_jxb & "'")
            If drsJXB.Length > 0 Then
                gjb_jxb_ID.Text = drsJXB(0)("equip_id")
                gjb_jxb_ip.Text = drsJXB(0)("ip")
                gjb_jxb_port.Text = drsJXB(0)("port")
            End If
            drsDZC = dtGJB.Select("equip_type_id='" & g_equipType_dzc & "'")
            If drsDZC.Length > 0 Then
                gjb_dzc_id.Text = drsDZC(0)("equip_id")
                gjb_dzc_ip.Text = drsDZC(0)("ip")
                gjb_dzc_port.Text = drsDZC(0)("port")
            End If
            drsSMQ1 = dtGJB.Select("equip_type_id='" & g_equipType_smq & "' and equip_id= 'Scaner1'")
            If drsSMQ1.Length > 0 Then
                gjb_scaner1_com.Text = drsSMQ1(0)("com")
            End If
            drsSMQ2 = dtGJB.Select("equip_type_id='" & g_equipType_smq & "' and equip_id= 'Scaner2'")
            If drsSMQ1.Length > 0 Then
                gjb_scaner2_com.Text = drsSMQ2(0)("com")
            End If
            setGJBEntity(old_gjb)

        End If
    End Sub
    Private Sub setLH()
        '按车间ID,ID查询数据
        btnClear_lhj.PerformClick()
        dtLH = objHWSetting.selectForLHJByID(g_ws_lh， lh_search_id.Text)

        dtLH.Columns.Add("status_name", Type.GetType("System.String"))
        For i As Integer = 0 To dtLH.Rows.Count - 1
            dtLH(i)("status_name") = getStatusByNo(dtLH(i)("status"))
        Next
        dtLH.AcceptChanges()

        Me.dgvList_lhj.DataSource = dtLH
    End Sub
    Private Sub btnSave_gjb_Click(sender As Object, e As EventArgs) Handles btnSave_gjb.Click
        '非空检查
        If chkEmpty(gjb_jxb_ID) = False Then
            Exit Sub
        End If
        If chkEmpty(gjb_jxb_ip) = False OrElse chkIP(gjb_jxb_ip) = False Then
            Exit Sub
        End If
        If chkEmpty(gjb_jxb_port) = False OrElse chkZero(gjb_jxb_port) = False Then
            Exit Sub
        End If
        If chkEmpty(gjb_dzc_id) = False Then
            Exit Sub
        End If
        If chkEmpty(gjb_dzc_ip) = False OrElse chkIP(gjb_dzc_ip) = False Then
            Exit Sub
        End If
        If chkEmpty(gjb_dzc_port) = False OrElse chkZero(gjb_dzc_port) = False Then
            Exit Sub
        End If
        If chkEmpty(gjb_scaner1_com) = False OrElse chkZero(gjb_scaner1_com) = False Then
            Exit Sub
        End If
        If chkEmpty(gjb_scaner2_com) = False OrElse chkZero(gjb_scaner2_com) = False Then
            Exit Sub
        End If

        setGJBEntity(new_gjb)

        '保存数据
        If objHWSetting.saveGJB(GJB_IUType, old_gjb, new_gjb) <> -2 Then
            MsgBox(getMsgStr("msg004"))
            GJB_IUType = UPDATE_TYPE
            setGJBEntity(old_gjb)
        Else
            MsgBox(getMsgStr("msg005"))
        End If

    End Sub

    Private Sub btnSave_jp_Click(sender As Object, e As EventArgs) Handles btnSave_jp.Click
        '非空检查
        '端片秤重 电子秤ID
        If chkEmpty(jp_dpdzc_id) = False Then
            Exit Sub
        End If
        If chkEmpty(jp_dpdzc_ip) = False OrElse chkIP(jp_dpdzc_ip) = False Then
            Exit Sub
        End If
        If chkEmpty(jp_dpdzc_port) = False OrElse chkZero(jp_dpdzc_port) = False Then
            Exit Sub
        End If
        '--------运胶臂---------
        If chkEmpty(jp_yjb_id) = False Then
            Exit Sub
        End If
        If chkEmpty(jp_yjb_ip) = False OrElse chkIP(jp_yjb_ip) = False Then
            Exit Sub
        End If
        If chkEmpty(jp_yjb_port) = False OrElse chkZero(jp_yjb_port) = False Then
            Exit Sub
        End If
        '--------运胶臂---------
        '--------运板臂---------
        If chkEmpty(jp_ybb_id) = False Then
            Exit Sub
        End If
        If chkEmpty(jp_ybb_ip) = False OrElse chkIP(jp_ybb_ip) = False Then
            Exit Sub
        End If
        If chkEmpty(jp_ybb_port) = False OrElse chkZero(jp_ybb_port) = False Then
            Exit Sub
        End If
        '--------运板臂---------
        '电子秤
        If chkEmpty(jp_dzc_id) = False Then
            Exit Sub
        End If
        '扫描枪
        If chkEmpty(jp_smq_com) = False OrElse chkZero(jp_smq_com) = False Then
            Exit Sub
        End If
        '保护胶秤重 电子秤
        If chkEmpty(jp_bhjdzc_id) = False Then
            Exit Sub
        End If
        If chkEmpty(jp_bhjdzc_ip) = False OrElse chkIP(jp_bhjdzc_ip) = False Then
            Exit Sub
        End If
        If chkEmpty(jp_bhjdzc_port) = False OrElse chkZero(jp_bhjdzc_port) = False Then
            Exit Sub
        End If
        '设定Entity
        setJPEntity(new_jp)

        '保存数据
        If objHWSetting.saveJP(JP_IUType, old_jp, new_jp) <> -2 Then
            MsgBox(getMsgStr("msg004"))
            JP_IUType = UPDATE_TYPE
            setJPEntity(old_jp)
        Else
            MsgBox(getMsgStr("msg005"))

        End If
    End Sub
    ''' <summary>
    ''' 设置画面内容放入entity
    ''' </summary>
    ''' <param name="gjbEntity">要设定的entity</param>
    Private Sub setGJBEntity(ByRef gjbEntity As entity_gjb)
        With gjbEntity
            .jxb_ID_text = gjb_jxb_ID.Text
            .jxb_ip_text = gjb_jxb_ip.Text
            .jxb_port_text = gjb_jxb_port.Text
            .dzc_id_text = gjb_dzc_id.Text
            .dzc_ip_text = gjb_dzc_ip.Text
            .dzc_port_text = gjb_dzc_port.Text

            .scaner1_com_text = gjb_scaner1_com.Text
            .scaner2_com_text = gjb_scaner2_com.Text
        End With
    End Sub
    ''' <summary>
    ''' 设置画面内容放入entity
    ''' </summary>
    ''' <param name="jpEntity">要设定的entity</param>
    Private Sub setJPEntity(ByRef jpEntity As entity_jp)
        With jpEntity
            .yjb_id_text = jp_yjb_id.Text
            .ybb_id_text = jp_ybb_id.Text
            .yjb_ip_text = jp_yjb_ip.Text
            .ybb_ip_text = jp_ybb_ip.Text
            .yjb_port_text = jp_yjb_port.Text
            .ybb_port_text = jp_ybb_port.Text
            .dzc_id_text = jp_dzc_id.Text
            .dzc_ip_text = jp_dzc_ip.Text
            .dzc_port_text = jp_dzc_port.Text

            .smq_com_text = jp_smq_com.Text
            .dpdzc_id_text = jp_dpdzc_id.Text
            .dpdzc_ip_text = jp_dpdzc_ip.Text
            .dpdzc_port_text = jp_dpdzc_port.Text

            .bhjdzc_id_text = jp_bhjdzc_id.Text
            .bhjdzc_ip_text = jp_bhjdzc_ip.Text
            .bhjdzc_port_text = jp_bhjdzc_port.Text

        End With
    End Sub

    Private Sub btnClear_lhj_Click(sender As Object, e As EventArgs) Handles btnClear_lhj.Click
        lhj_ID.Text = String.Empty
        lhj_ip.Text = String.Empty
        lhj_port.Text = String.Empty
        lhj_Status.Text = String.Empty
        lhj_deleted.Checked = False
        lhj_type_name.Text = String.Empty
    End Sub

    Private Sub dgvList_lhj_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList_lhj.CellClick
        '当点击表头部的列时，e.RowIndex==-1
        If e.RowIndex > -1 Then
            With dgvList_lhj.Rows(e.RowIndex)
                lhj_ID.Text = .Cells("collh_id").Value.ToString
                lhj_ip.Text = .Cells("collh_ip").Value.ToString
                lhj_port.Text = .Cells("collh_port").Value.ToString
                lhj_Status.Text = .Cells("collh_status_name").Value.ToString
                lhj_deleted.Checked = .Cells("collh_deleted").Value
                lhj_type_name.Text = .Cells("collh_type_name").Value.ToString
            End With
            setLHEntity(old_lh)
        End If
    End Sub
    Private Sub setLHEntity(ByRef entity As entity_lhAgv)
        With entity
            .id_text = lhj_ID.Text
            .ip_text = lhj_ip.Text
            .port_text = lhj_port.Text
            .type_name_text = lhj_type_name.Text
            .status_text = getStatusNoByText(lhj_Status.Text)
            .deleted_text = lhj_deleted.Checked
        End With
    End Sub
    Private Sub setAGVEntity(ByRef entity As entity_lhAgv)
        With entity
            .id_text = agv_id.Text
            .ip_text = agv_ip.Text
            .port_text = agv_port.Text
            .type_name_text = ""
            .status_text = getStatusNoByText(agv_status.Text)
            .deleted_text = agv_deleted.Checked
        End With
    End Sub
    Private Sub btnAdd_lhj_Click(sender As Object, e As EventArgs) Handles btnAdd_lhj.Click
        If chkLH() = False Then Exit Sub
        setLHEntity(new_lh)
        If objHWSetting.saveLHAGV(INSERT_TYPE, g_equiptype_LHJ， old_lh, new_lh) <> -2 Then
            MsgBox(getMsgStr("msg004"))
            setLH()
        Else
            MsgBox(getMsgStr("msg005"))
        End If
    End Sub


    Private Sub btnUpdate_lhj_Click(sender As Object, e As EventArgs) Handles btnUpdate_lhj.Click
        If chkLH() = False Then Exit Sub
        setLHEntity(new_lh)
        If objHWSetting.saveLHAGV(UPDATE_TYPE, g_equiptype_LHJ， old_lh, new_lh) <> -2 Then
            setLHEntity(old_lh)
            MsgBox(getMsgStr("msg004"))
            setLH()
        Else
            MsgBox(getMsgStr("msg005"))
        End If

    End Sub
    Private Function chkLH() As Boolean
        Dim rtn As Boolean = True
        If chkEmpty(lhj_ID) = False Then
            Return False
        End If
        If chkEmpty(lhj_ip) = False OrElse chkIP(lhj_ip) = False Then
            Return False
        End If
        If chkEmpty(lhj_port) = False OrElse chkZero(lhj_port) = False Then
            Return False
        End If
        If chkEmpty(lhj_Status) = False Then
            Return False
        End If

        Return rtn
    End Function
    Private Sub btnDEL_lhj_Click(sender As Object, e As EventArgs) Handles btnDEL_lhj.Click
        If chkLH() = False Then Exit Sub
        If MsgBox(getMsgStr("msg006"), MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Exit Sub
        End If
        setLHEntity(new_lh)
        If objHWSetting.saveLHAGV(DELETE_TYPE, g_equiptype_LHJ， old_lh, new_lh) <> -2 Then
            MsgBox(getMsgStr("msg004"))
            btnClear_lhj.PerformClick()
            setLH()

        Else
            MsgBox(getMsgStr("msg005"))
        End If
    End Sub

    Private Sub btnSearch_lhj_Click(sender As Object, e As EventArgs) Handles btnSearch_lhj.Click
        setLH()
    End Sub

    ''' <summary>
    ''' 根据数据库里的状态得到状态的中文名称
    ''' </summary>
    ''' <param name="sno">数据库里的状态值</param>
    ''' <returns></returns>
    Private Function getStatusByNo(sno As String) As String
        Dim sn As String = ""
        Select Case sno
            Case "0"
                sn = "闲"
            Case "1"
                sn = "忙"
            Case "2"
                sn = "维修中"
        End Select
        Return sn
    End Function
    ''' <summary>
    ''' 根据数据库里的状态得到状态的中文名称
    ''' </summary>
    ''' <param name="sno">数据库里的状态值</param>
    ''' <returns></returns>
    Private Function getStatusNoByText(sno As String) As String
        Dim sn As String = ""
        Select Case sno
            Case "闲"
                sn = "0"
            Case "忙"
                sn = "1"
            Case "维修中"
                sn = "2"
        End Select
        Return sn
    End Function

    Private Sub btnClear_AGV_Click(sender As Object, e As EventArgs) Handles btnClear_AGV.Click
        agv_id.Text = String.Empty
        agv_ip.Text = String.Empty
        agv_port.Text = String.Empty
        agv_status.Text = String.Empty
        agv_deleted.Checked = False
    End Sub

    Private Function chkAGV() As Boolean
        Dim rtn As Boolean = True
        If chkEmpty(agv_id) = False Then
            Return False
        End If
        If chkEmpty(agv_ip) = False OrElse chkIP(agv_ip) = False Then
            Return False
        End If
        If chkEmpty(agv_port) = False OrElse chkZero(agv_port) = False Then
            Return False
        End If
        If chkEmpty(agv_status) = False Then
            Return False
        End If

        Return rtn
    End Function

    Private Sub btnAdd_AGV_Click(sender As Object, e As EventArgs) Handles btnAdd_AGV.Click

        If chkAGV() = False Then Exit Sub
        setAGVEntity(new_agv)
        If objHWSetting.saveLHAGV(INSERT_TYPE, g_equiptype_AGV， old_agv, new_agv) <> -2 Then
            MsgBox(getMsgStr("msg004"))
            setAGV()
        Else
            MsgBox(getMsgStr("msg005"))
        End If
    End Sub

    Private Sub btnUpdate_AGV_Click(sender As Object, e As EventArgs) Handles btnUpdate_AGV.Click
        If chkAGV() = False Then Exit Sub
        setAGVEntity(new_agv)
        If objHWSetting.saveLHAGV(UPDATE_TYPE, g_equiptype_AGV， old_agv, new_agv) <> -2 Then
            setAGVEntity(old_agv)
            MsgBox(getMsgStr("msg004"))
            setAGV()
        Else
            MsgBox(getMsgStr("msg005"))
        End If
    End Sub

    Private Sub btnDelete_AGV_Click(sender As Object, e As EventArgs) Handles btnDelete_AGV.Click
        If chkAGV() = False Then Exit Sub
        If MsgBox(getMsgStr("msg006"), MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Exit Sub
        End If
        setAGVEntity(new_agv)
        If objHWSetting.saveLHAGV(DELETE_TYPE, g_equiptype_AGV， old_agv, new_agv) <> -2 Then
            MsgBox(getMsgStr("msg004"))
            btnClear_AGV.PerformClick()
            setAGV()

        Else
            MsgBox(getMsgStr("msg005"))
        End If
    End Sub

    Private Sub dgvList_AGV_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvList_AGV.CellClick
        '当点击表头部的列时，e.RowIndex==-1
        If e.RowIndex > -1 Then
            With dgvList_AGV.Rows(e.RowIndex)
                agv_id.Text = .Cells("colagv_id").Value.ToString
                agv_ip.Text = .Cells("colagv_ip").Value.ToString
                agv_port.Text = .Cells("colagv_port").Value.ToString
                agv_status.Text = .Cells("colagv_status_name").Value.ToString
                agv_deleted.Checked = .Cells("colagv_deleted").Value

            End With
            setAGVEntity(old_agv)
        End If
    End Sub
End Class


Public Class entity_gjb '骨架板画面项目
    Public jxb_ID_text As String
    Public jxb_ip_text As String
    Public jxb_port_text As String
    Public dzc_id_text As String
    Public dzc_ip_text As String
    Public dzc_port_text As String

    Public scaner1_com_text As String
    Public scaner2_com_text As String
End Class
Public Class entity_jp '胶片画面项目
    Public yjb_id_text As String
    Public ybb_id_text As String
    Public yjb_ip_text As String
    Public ybb_ip_text As String
    Public yjb_port_text As String
    Public ybb_port_text As String
    Public dzc_id_text As String
    Public dzc_ip_text As String
    Public dzc_port_text As String
    Public smq_com_text As String
    Public dpdzc_id_text As String
    Public dpdzc_ip_text As String
    Public dpdzc_port_text As String
    Public bhjdzc_id_text As String
    Public bhjdzc_ip_text As String
    Public bhjdzc_port_text As String
End Class

Public Class entity_lhAgv '硫化,AGV画面项目
    Public id_text As String
    Public ip_text As String
    Public port_text As String
    Public type_name_text As String
    Public deleted_text As Boolean
    Public status_text As String

End Class

