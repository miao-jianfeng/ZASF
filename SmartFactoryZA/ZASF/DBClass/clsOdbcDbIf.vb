
Option Strict On
Option Explicit On

Imports System.Data.Odbc
'Imports Npgsql

Public Class clsOdbcDbIf
    ''' 
    ''' SQLコネクション
    ''' 
    Private _con As OdbcConnection = Nothing

    ''' 
    ''' トランザクション・オブジェクト
    ''' 
    ''' 
    Private _trn As OdbcTransaction = Nothing

    '''
    ''' 接続フラグ
    ''' 
    Private _connflg As Boolean = False

    Private SqlCmd As OdbcCommand

    Private WithEvents ConnectTimer As New Timer

    'Disconnect Timer Interval (Min)
    Private Const MINUTS_TO_DISCONNECT = 1
    Private it As Integer = 0

    Private strConnection As String = String.Empty

    Public Sub New()
        ConnectTimer.Interval = 1000
        ConnectTimer.Stop()
    End Sub

    ''' 
    ''' 
    ''' DB接続
    ''' 
    ''' データソース名
    ''' データベース名
    ''' ユーザーID
    ''' パスワード
    ''' タイムアウト値
    ''' 
    Public Function Connect(Optional ByVal drv As String = "", _
                            Optional ByVal IPadr As String = "", _
                            Optional ByVal dbn As String = "atlas", _
                            Optional ByVal uid As String = "atlas", _
                            Optional ByVal pas As String = "atlas", _
                            Optional ByVal tot As Integer = -1) As Integer

        Dim szMess As String

        _connflg = False
        Connect = 0
        g_szDBIpAddress = ""

        Try
            If _con Is Nothing Then
                _con = New OdbcConnection
            End If

            Dim cst As String = ""
            cst = cst & "Driver={" & drv & "}"
            cst = cst & ";Server=" & IPadr
            cst = cst & ";Database=" & dbn
            cst = cst & ";UID=" & uid
            cst = cst & ";PWD=" & pas
            'cst = cst & ";MultipleActiveResultsSets=True"
            If tot > -1 Then
                '_con.ConnectionTimeout = tot
                cst = cst & ";Connect Timeout=" & tot.ToString
            End If
            _con.ConnectionString = cst

            strConnection = "Server=" & IPadr + ";Database=" & dbn + ";UID=" & uid + ";PWD=" & pas

            _con.Open()

            g_szDBIpAddress = _con.DataSource

            _connflg = True
            Connect = 1

            ConnectTimer.Start()
            it = 0

        Catch ex As Exception
            'Throw New Exception("Connect Error", ex)
            Connect = -1
            szMess = "データベースに接続できません" & vbCrLf & "Connect Error:" & ex.Message
            MsgBox(szMess)
        End Try
    End Function

    ''' 
    ''' DB切断
    ''' 
    Public Sub Disconnect()

        If False = _connflg Then
            Exit Sub
        End If

        Try
            _con.Close()
        Catch ex As Exception
            'Throw New Exception("Disconnect Error", ex)
            MsgBox("Disconnect Error:" & ex.Message)
        End Try
    End Sub

    ''' 
    ''' SQLの実行
    ''' 
    ''' SQL文
    ''' タイムアウト値
    ''' 
    ''' 
    Public Function ExecuteSql(ByVal sql As String, _
                               Optional ByVal tot As Integer = -1) As DataTable
        'RSC_Log("ExecuteSql Without Para")
        'RSC_Log(sql)

        Dim dt As New DataTable

        If False = _connflg Then
            Return dt
            Exit Function
        End If

        Try
            CheckConnection()
            Dim sqlCommand As New OdbcCommand(sql, _con, _trn)

            If tot > -1 Then
                sqlCommand.CommandTimeout = tot
            End If

            Dim adapter As New OdbcDataAdapter(sqlCommand)
            adapter.Fill(dt)
            adapter.Dispose()
            sqlCommand.Dispose()
        Catch ex As Exception
            'Throw New Exception("ExecuteSql Error", ex)
            MsgBox("ExecuteSql Error:" & ex.Message)
            'RSC_Log("ExecuteSql Error:" & ex.Message)
        End Try

        Return dt
    End Function

    ''' 
    ''' トランザクション開始
    ''' 
    ''' 
    Public Sub BeginTransaction()

        If False = _connflg Then
            Exit Sub
        End If

        Try
            CheckConnection()
            _trn = _con.BeginTransaction()
        Catch ex As Exception
            'Throw New Exception("BeginTransaction Error", ex)
            MsgBox("BeginTransaction Error:" & ex.Message)
        End Try
    End Sub

    ''' 
    ''' コミット
    ''' 
    ''' 
    Public Sub CommitTransaction()

        If False = _connflg Then
            Exit Sub
        End If

        Try
            CheckConnection()
            If _trn Is Nothing = False Then
                _trn.Commit()
            End If
        Catch ex As Exception
            'Throw New Exception("CommitTransaction Error", ex)
            MsgBox("CommitTransaction Error:" & ex.Message)
        Finally
            _trn = Nothing
        End Try
    End Sub

    ''' 
    ''' ロールバック
    ''' 
    ''' 
    Public Sub RollbackTransaction()

        If False = _connflg Then
            Exit Sub
        End If

        Try
            CheckConnection()
            If _trn Is Nothing = False Then
                _trn.Rollback()
            End If
        Catch ex As Exception
            'Throw New Exception("RollbackTransaction Error", ex)
            MsgBox("RollbackTransaction Error:" & ex.Message)
        Finally
            _trn = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' パラメータ付きのSELECT SQLの実行
    ''' </summary>
    ''' <param name="sql">SQL文</param>
    ''' <param name="tot">タイムアウト時間</param>
    ''' <param name="Params">パラメータ</param>
    ''' <returns>取得したテーブル</returns>
    ''' <remarks></remarks>
    Public Function ExecuteSql(ByVal sql As String, ByVal tot As Integer, _
                               ByVal ParamArray Params() As Object) As DataTable
        'RSC_Log("ExecuteSql With Para")
        'RSC_Log(sql)

        Dim dt As New DataTable

        If False = _connflg Then
            Return dt
            Exit Function
        End If

        Try
            CheckConnection()
            Dim sqlCommand As New OdbcCommand(sql, _con, _trn)
            For Each obj In Params
                With sqlCommand
                    Dim param = .CreateParameter()
                    param.Value = obj.ToString
                    .Parameters.Add(param)
                End With
            Next

            If tot > -1 Then
                sqlCommand.CommandTimeout = tot
            End If
            Dim adapter As New OdbcDataAdapter(sqlCommand)
            adapter.Fill(dt)
            adapter.Dispose()
            sqlCommand.Dispose()
            'Disconnect()
        Catch ex As Exception
            'Throw New Exception("ExecuteSql Error", ex)
            MsgBox("ExecuteSql Error:" & ex.Message)
            'RSC_Log("ExecuteSql Error:" & ex.Message)
        End Try

        Return dt
    End Function

    ''' <summary>
    ''' DB更新系（INSERT、UPDATE、DELETE）のSQLの実行
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns>影響を受けた行数</returns>
    ''' <remarks></remarks>
    Public Function ExecuteUpdateSql(ByVal sql As String, ByVal tot As Integer, _
                               ByVal ParamArray Params() As Object) As Integer

        'RSC_Log("ExecuteUpdateSql")
        'RSC_Log(sql)

        If False = _connflg Then
            Return 0
            Exit Function
        End If

        Try
            CheckConnection()
            Dim sqlCommand As New OdbcCommand(sql, _con, _trn)

            For Each obj In Params
                With sqlCommand
                    Dim param = .CreateParameter()
                    param.Value = obj.ToString
                    .Parameters.Add(param)
                End With
            Next

            If tot > -1 Then
                sqlCommand.CommandTimeout = tot
            End If
            Dim intRows As Integer = 0
            intRows = sqlCommand.ExecuteNonQuery()
            sqlCommand.Dispose()
            Return intRows

        Catch ex As Exception
            'Throw New Exception("ExecuteSql Error", ex)
            MsgBox("ExecuteSql Error:" & ex.Message)
            'RSC_Log("ExecuteSql Error:" & ex.Message)
            Return -2
        End Try
    End Function

    ''' <summary>
    ''' SQLを実行して結果の最初行と最終列を返す
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <param name="tot"></param>
    ''' <param name="Params"></param>
    ''' <returns>結果の最初行と最終列の値</returns>
    ''' <remarks></remarks>
    Public Function ExccuteScalarSql(ByVal sql As String, ByVal tot As Integer, _
                               ByVal ParamArray Params() As Object) As Object
        'RSC_Log("ExccuteScalarSql")
        'RSC_Log(sql)

        If False = _connflg Then
            Return 0
            Exit Function
        End If

        Try
            CheckConnection()
            Dim sqlCommand As New OdbcCommand(sql, _con, _trn)
            For Each obj In Params
                With sqlCommand
                    Dim param = .CreateParameter()
                    param.Value = obj.ToString
                    .Parameters.Add(param)
                End With
            Next

            If tot > -1 Then
                sqlCommand.CommandTimeout = tot
            End If

            Dim objScalar As New Object
            objScalar = sqlCommand.ExecuteScalar()
            sqlCommand.Dispose()
            Return objScalar

        Catch ex As Exception
            'Throw New Exception("ExecuteSql Error", ex)
            MsgBox("ExecuteSql Error:" & ex.Message)
            'RSC_Log("ExecuteSql Error:" & ex.Message)
            Return -2
        End Try
    End Function

    ''' 
    ''' ファイナライズ
    ''' 
    ''' 
    Protected Overrides Sub Finalize()

        If False = _connflg Then
            Exit Sub
        End If

        Disconnect()
        MyBase.Finalize()
    End Sub

    Private Sub CheckConnection()
        If _con.State = ConnectionState.Closed Then
            'Debug.Print(Date.Now.ToString("mm:ss:ffff"))
            _con.Open()
            'Debug.Print(Date.Now.ToString("mm:ss:ffff"))
            'Connection Timer Start
            ConnectTimer.Start()
            'Debug.Print("Connection=" & _con.State.ToString)
        End If
        it = 0
        'Debug.Print("Checked")
    End Sub

    Private Sub ConnectTimer_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ConnectTimer.Tick
        If it >= MINUTS_TO_DISCONNECT * 60 Then
            it = 0
            Disconnect()
            ConnectTimer.Stop()
            'Debug.Print("Connection=" & _con.State.ToString)
        Else
            it += 1
        End If
    End Sub

    'Public Function ExecuteSqlNpgsql(ByVal sql As String) As DataTable
    '    'RSC_Log("ExecuteSqlNpgsql")
    '    'RSC_Log(sql)
    '    If _con Is Nothing Then
    '        Connect(g_Server.szDrive, g_Server.szIPadr, g_Server.szDsn)
    '    End If
    '    Dim dt As New DataTable
    '    Dim DBconnection As New NpgsqlConnection(strConnection)
    '    DBconnection.Open()

    '    Dim sqlcommond As New NpgsqlCommand
    '    sqlcommond.Connection = DBconnection
    '    sqlcommond.CommandText = sql
    '    sqlcommond.CommandTimeout = 30
    '    Try
    '        Dim sqlAdapter As New NpgsqlDataAdapter(sqlcommond)

    '        sqlAdapter.Fill(dt)
    '        sqlAdapter.Dispose()
    '        sqlcommond.Dispose()
    '        DBconnection.Close()
    '        DBconnection = New NpgsqlConnection
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        'RSC_Log("ExecuteSqlNpgsql Error:" + ex.Message)
    '    End Try

    '    Return dt
    'End Function

End Class

