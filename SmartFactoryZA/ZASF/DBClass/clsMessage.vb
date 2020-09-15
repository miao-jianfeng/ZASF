Public Class clsMessage

    Public Function GetAllMsg() As DataTable
        Dim szSql As String = ""
        szSql &= "SELECT msg_id, msg"
        szSql &= "  FROM sf.t_mst_message "
        szSql &= "  WHERE factory_id ='TS' "
        szSql &= "  ORDER BY msg_id"

        Dim dt As DataTable = DBacs.ExecuteSql(szSql, -1) ', g_factory_id)
        If IsNothing(dt) Then
            GetAllMsg = New DataTable
        Else
            GetAllMsg = dt
        End If
    End Function
End Class
