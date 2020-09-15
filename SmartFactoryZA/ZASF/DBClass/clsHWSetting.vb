Public Class clsHWSetting

    Private INSERT_TYPE As String = "I"
    Private UPDATE_TYPE As String = "U"
    Private DELETE_TYPE As String = "D"

    Public Function selectByWorkshop(ws As String) As DataTable
        Dim szSql As String = String.Empty
        szSql &= "SELECT * "
        szSql &= "  FROM sf.t_hw_setting "
        szSql &= "  WHERE factory_id =? "
        szSql &= "  AND workshop_id = ? "
        szSql &= "  ORDER BY workshop_id,equip_id "

        Dim dt As DataTable = DBacs.ExecuteSql(szSql, -1, g_factory_id, ws)
        If IsNothing(dt) Then
            selectByWorkshop = New DataTable
        Else
            selectByWorkshop = dt
        End If
    End Function

    Public Function selectForLHJByID(ws As String, id As String) As DataTable
        Dim szSql As String = String.Empty
        szSql &= "SELECT * "
        szSql &= "  FROM sf.t_hw_setting "
        szSql &= "  WHERE factory_id =? "
        szSql &= "  AND workshop_id = ? "
        szSql &= "  AND equip_id like (?)"
        szSql &= "  ORDER BY workshop_id,equip_id "

        Dim dt As DataTable = DBacs.ExecuteSql(szSql, -1, g_factory_id, ws, "%" & id & "%")
        If IsNothing(dt) Then
            selectForLHJByID = New DataTable
        Else
            selectForLHJByID = dt
        End If
    End Function
    'Public Function saveBCP(IUtype As String, newDzcID As String, Optional ByVal oldDzcID As String = "") As Integer
    '    Dim szSql As String = String.Empty
    '    Dim rtn As Integer
    '    If IUtype = INSERT_TYPE Then
    '        szSql &= "insert into sf.t_hw_setting(factory_id,workshop_id,equip_type_id,"
    '        szSql &= "equip_id,ip,port,com,status,deleted,production_no,update_time) "
    '        szSql &= " values (?,?,?,?,'','',null,'0',False,null,?)"
    '        rtn = DBacs.ExecuteUpdateSql(szSql, -1， g_factory_id, g_ws_bcp, g_equipType_dzc, newDzcID, Now())
    '    ElseIf IUtype = UPDATE_TYPE Then
    '        szSql &= " update sf.t_hw_setting "
    '        szSql &= " set equip_id=? ,update_time=? "
    '        szSql &= " Where factory_id=? and workshop_id=? and equip_type_id=? and equip_id=?"
    '        rtn = DBacs.ExecuteUpdateSql(szSql, -1， newDzcID, Now(), g_factory_id, g_ws_bcp, g_equipType_dzc, oldDzcID)
    '    End If
    '    Return rtn
    'End Function

    Public Function saveGJB(IUType As String, oldgjb As entity_gjb, newgjb As entity_gjb) As Integer
        Dim szSql As String = String.Empty
        Dim rtn As Integer
        If IUType = INSERT_TYPE Then
            DBacs.BeginTransaction()
            Try
                szSql &= "insert into sf.t_hw_setting(factory_id,workshop_id,equip_type_id,"
                szSql &= "equip_id,ip,port,com,status,deleted,production_no,update_time) "
                szSql &= " values (?,?,?,?,?,?,?,'0',False,null,?)"
                '机械臂
                rtn = DBacs.ExecuteUpdateSql(szSql, -1， g_factory_id, g_ws_gjb, g_equipType_jxb, newgjb.jxb_ID_text, newgjb.jxb_ip_text， newgjb.jxb_port_text, DBNull.Value, Now())
                '电子秤
                rtn = DBacs.ExecuteUpdateSql(szSql, -1， g_factory_id, g_ws_gjb, g_equipType_dzc, newgjb.dzc_id_text, DBNull.Value， DBNull.Value, DBNull.Value, Now())
                '扫描枪1
                rtn = DBacs.ExecuteUpdateSql(szSql, -1， g_factory_id, g_ws_gjb, g_equipType_smq, "Scaner1", DBNull.Value， DBNull.Value, newgjb.scaner1_com_text, Now())
                '扫描枪2
                rtn = DBacs.ExecuteUpdateSql(szSql, -1， g_factory_id, g_ws_gjb, g_equipType_smq, "Scaner2", DBNull.Value， DBNull.Value, newgjb.scaner2_com_text, Now())
                DBacs.CommitTransaction()
            Catch ex As Exception
                DBacs.RollbackTransaction()
            Finally
                DBacs.RollbackTransaction()
            End Try

        ElseIf IUType = UPDATE_TYPE Then

            DBacs.BeginTransaction()
            Try
                szSql &= "update sf.t_hw_setting "
                szSql &= "set equip_id=? , ip=? ,port=? ,update_time=? "
                szSql &= "where factory_id=? and workshop_id=? and equip_type_id=? and equip_id=?"
                '机械臂
                rtn = DBacs.ExecuteUpdateSql(szSql, -1， newgjb.jxb_ID_text， newgjb.jxb_ip_text， newgjb.jxb_port_text, Now(), g_factory_id, g_ws_gjb, g_equipType_jxb, oldgjb.jxb_ID_text)
                '电子秤
                szSql = ""
                szSql &= "update sf.t_hw_setting "
                szSql &= "set equip_id=?  ,update_time=?  "
                szSql &= "where factory_id=? and workshop_id=? and equip_type_id=? and equip_id=?"
                rtn = DBacs.ExecuteUpdateSql(szSql, -1， newgjb.dzc_id_text， Now(), g_factory_id, g_ws_gjb, g_equipType_dzc, oldgjb.dzc_id_text)
                '扫描枪1
                szSql = ""
                szSql &= "update sf.t_hw_setting "
                szSql &= "set com=?  ,update_time=? "
                szSql &= "where factory_id=? and workshop_id=? and equip_type_id=? and equip_id=?"
                rtn = DBacs.ExecuteUpdateSql(szSql, -1， newgjb.scaner1_com_text， Now(), g_factory_id, g_ws_gjb, g_equipType_smq, "Scaner1")

                '扫描枪2
                rtn = DBacs.ExecuteUpdateSql(szSql, -1， newgjb.scaner2_com_text， Now(), g_factory_id, g_ws_gjb, g_equipType_smq, "Scaner2")
                DBacs.CommitTransaction()
            Catch ex As Exception
                DBacs.RollbackTransaction()
            Finally
                DBacs.RollbackTransaction()
            End Try
        End If
        Return rtn
    End Function

    Public Function saveJP(IUType As String, oldjp As entity_jp, newjp As entity_jp) As Integer
        Dim szSql As String = String.Empty
        Dim rtn As Integer
        If IUType = INSERT_TYPE Then
            DBacs.BeginTransaction()
            Try
                szSql &= "insert into sf.t_hw_setting(factory_id,workshop_id,equip_type_id,"
                szSql &= "equip_id,ip,port,com,status,deleted,production_no,update_time) "
                szSql &= " values (?,?,?,?,?,?,?,'0',False,null,?)"
                '运胶臂
                rtn = DBacs.ExecuteUpdateSql(szSql, -1， g_factory_id, g_ws_jp, g_equipType_yjb, newjp.yjb_id_text, newjp.yjb_ip_text， newjp.yjb_port_text, DBNull.Value, Now())
                '运板臂
                rtn = DBacs.ExecuteUpdateSql(szSql, -1， g_factory_id, g_ws_jp, g_equipType_ybb, newjp.ybb_id_text, newjp.ybb_ip_text， newjp.ybb_port_text, DBNull.Value, Now())
                '电子秤
                rtn = DBacs.ExecuteUpdateSql(szSql, -1， g_factory_id, g_ws_jp, g_equipType_dzc, newjp.dzc_id_text, DBNull.Value， DBNull.Value, DBNull.Value, Now())
                '扫描枪
                rtn = DBacs.ExecuteUpdateSql(szSql, -1， g_factory_id, g_ws_jp, g_equipType_smq, "Scaner", DBNull.Value， DBNull.Value, newjp.smq_com_text, Now())
                '端片 电子秤
                rtn = DBacs.ExecuteUpdateSql(szSql, -1， g_factory_id, g_ws_jp, g_equipType_dpdzc, newjp.dpdzc_id_text, DBNull.Value， DBNull.Value, DBNull.Value, Now())
                '保护胶电子秤
                rtn = DBacs.ExecuteUpdateSql(szSql, -1， g_factory_id, g_ws_jp, g_equipType_bhjdzc, newjp.bhjdzc_id_text, DBNull.Value， DBNull.Value, DBNull.Value, Now())
                DBacs.CommitTransaction()
            Catch ex As Exception
                DBacs.RollbackTransaction()
            Finally
                DBacs.RollbackTransaction()
            End Try

        ElseIf IUType = UPDATE_TYPE Then
            DBacs.BeginTransaction()
            Try
                '运胶臂
                szSql = ""
                szSql &= "update sf.t_hw_setting "
                szSql &= "set equip_id=? , ip=? ,port=? ,update_time=? "
                szSql &= "where factory_id=? and workshop_id=? and equip_type_id=? and equip_id=?"
                rtn = DBacs.ExecuteUpdateSql(szSql, -1， newjp.yjb_id_text， newjp.yjb_ip_text， newjp.yjb_port_text, Now(), g_factory_id, g_ws_jp, g_equipType_yjb, oldjp.yjb_id_text)
                '运板臂
                szSql = ""
                szSql &= "update sf.t_hw_setting "
                szSql &= "set equip_id=? , ip=? ,port=? ,update_time=? "
                szSql &= "where factory_id=? and workshop_id=? and equip_type_id=? and equip_id=?"
                rtn = DBacs.ExecuteUpdateSql(szSql, -1， newjp.ybb_id_text， newjp.ybb_ip_text， newjp.ybb_port_text, Now(), g_factory_id, g_ws_jp, g_equipType_ybb, oldjp.ybb_id_text)

                '电子秤
                szSql = ""
                szSql &= "update sf.t_hw_setting "
                szSql &= "set equip_id=?  ,update_time=?  "
                szSql &= "where factory_id=? and workshop_id=? and equip_type_id=? and equip_id=?"
                rtn = DBacs.ExecuteUpdateSql(szSql, -1， newjp.dzc_id_text， Now(), g_factory_id, g_ws_jp, g_equipType_dzc, oldjp.dzc_id_text)
                '扫描枪1
                szSql = ""
                szSql &= "update sf.t_hw_setting "
                szSql &= "set com=?  ,update_time=? "
                szSql &= "where factory_id=? and workshop_id=? and equip_type_id=? and equip_id=?"
                rtn = DBacs.ExecuteUpdateSql(szSql, -1， newjp.smq_com_text， Now(), g_factory_id, g_ws_jp, g_equipType_smq, "Scaner1")
                '端片秤重电子秤
                szSql = ""
                szSql &= "update sf.t_hw_setting "
                szSql &= "set equip_id=?  ,update_time=?  "
                szSql &= "where factory_id=? and workshop_id=? and equip_type_id=? and equip_id=?"
                rtn = DBacs.ExecuteUpdateSql(szSql, -1， newjp.dpdzc_id_text， Now(), g_factory_id, g_ws_jp, g_equipType_dpdzc, oldjp.dpdzc_id_text)
                '保护胶秤重电子秤
                szSql = ""
                szSql &= "update sf.t_hw_setting "
                szSql &= "set equip_id=?  ,update_time=?  "
                szSql &= "where factory_id=? and workshop_id=? and equip_type_id=? and equip_id=?"
                rtn = DBacs.ExecuteUpdateSql(szSql, -1， newjp.bhjdzc_id_text， Now(), g_factory_id, g_ws_jp, g_equipType_bhjdzc, oldjp.bhjdzc_id_text)

                DBacs.CommitTransaction()
            Catch ex As Exception
                DBacs.RollbackTransaction()
            Finally
                DBacs.RollbackTransaction()
            End Try
        End If
        Return rtn
    End Function

    Public Function saveLH(IUType As String, oldlh As entity_lh, newlh As entity_lh) As Integer
        Dim szSql As String = String.Empty
        Dim rtn As Integer
        If IUType = INSERT_TYPE Then

            szSql &= "insert into sf.t_hw_setting(factory_id,workshop_id,equip_type_id,"
                szSql &= "equip_id,ip,port,com,status,deleted,production_no,update_time) "
                szSql &= " values (?,?,?,?,?,?,?,?,?,?,?)"
                rtn = DBacs.ExecuteUpdateSql(szSql, -1， g_factory_id, g_ws_lh, g_equiptype_LHJ,
                                             newlh.id_text, newlh.ip_text， newlh.port_text,
                                             DBNull.Value, newlh.status_text, newlh.deleted_text,
                                             newlh.scbh_text, Now())


        End If
        If IUType = UPDATE_TYPE Then

            szSql &= "UPDATE sf.t_hw_setting "
                szSql &= "SET"
                szSql &= "    equip_id = ? "
                szSql &= "    , ip = ?"
                szSql &= "    , port = ?"
                szSql &= "    , status = ?"
                szSql &= "    , deleted = ?"
                szSql &= "    , production_no = ?"
                szSql &= "    , update_time = ?"
                szSql &= "WHERE"
                szSql &= "    factory_id = ? "
                szSql &= "    and workshop_id = ? "
                szSql &= "    and equip_type_id = ? "
                szSql &= "    and equip_id = ?"

                rtn = DBacs.ExecuteUpdateSql(szSql, -1，
                                             newlh.id_text, newlh.ip_text， newlh.port_text,
                                             newlh.status_text, newlh.deleted_text, newlh.scbh_text, Now(),
                                             g_factory_id, g_ws_lh, g_equiptype_LHJ, oldlh.id_text)


        End If

        If IUType = DELETE_TYPE Then

            szSql &= "DELETE "
                szSql &= "FROM"
                szSql &= "    sf.t_hw_setting "
                szSql &= "WHERE"
                szSql &= "    factory_id = ? "
                szSql &= "    and workshop_id = ? "
                szSql &= "    and equip_type_id = ? "
                szSql &= "    and equip_id = ?"

                rtn = DBacs.ExecuteUpdateSql(szSql, -1， g_factory_id, g_ws_lh, g_equiptype_LHJ, newlh.id_text)


        End If
        Return rtn
    End Function
End Class
