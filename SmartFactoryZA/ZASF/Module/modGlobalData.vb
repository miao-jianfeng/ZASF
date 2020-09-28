Module modGlobalData

    '数据库关联
    Public DBacs As New clsOdbcDbIf
    Public g_fDBconnect As Boolean
    Public g_iDBconnResult As Integer
    Public g_szDBIpAddress As String

    '全局变量
    Public g_szInitFileName As String

    Public Structure ServerDBacsTYPE
        Dim szDrive As String
        Dim szIPadr As String
        Dim szDsn As String
        Dim szDbn As String
        Dim szUid As String
        Dim szPas As String
        Dim iOver As Integer
    End Structure
    Public g_Server As ServerDBacsTYPE

    '工厂
    Public g_factory_id = "TS"

    '车间ID
    Public g_ws_gjb As String = "GJB" '骨架板
    Public g_ws_jp As String = "JP"   '胶片
    Public g_ws_lh As String = "LH"   '硫化


    '设备类型
    Public g_equipType_jxb As String = "JXB"  '机械臂
    Public g_equipType_yjb As String = "YJB"  '运胶臂
    Public g_equipType_ybb As String = "YBB"  '运板臂
    Public g_equipType_dzc As String = "DZC"  '电子秤
    Public g_equipType_smq As String = "SMQ"  '扫描枪
    Public g_equipType_dpdzc As String = "DPDZC" '端片电子秤
    Public g_equipType_bhjdzc As String = "BHJDZC" '保护胶电子秤
    Public g_equiptype_LHJ As String = "LHJ" '硫化机
    Public g_equiptype_AGV As String = "AGV" 'AGV

    '保存MESSAGE
    Public htMsgMst As New Hashtable


End Module
