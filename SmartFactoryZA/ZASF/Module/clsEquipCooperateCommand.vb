Imports System.Text
Imports System.Timers

''' <summary>
''' 装置連携コマンド送受信クラス
''' </summary>
Public Class clsEquipCooperateCommand
    ''' <summary>
    ''' IPアドレス
    ''' </summary>
    Private m_ipAddress As String
    ''' <summary>
    ''' ポート番号
    ''' </summary>
    Private m_port As String
    ''' <summary>
    ''' TCP/IP通信クラス
    ''' </summary>
    Private WithEvents m_TcpListener As TCPListener
    ''' <summary>
    ''' 受信コマンドリスト
    ''' </summary>
    Private m_ReciveCommandList As List(Of String)

    ''' <summary>
    ''' オンライン同期タイマー
    ''' </summary>
    Dim syncTimer As Timer

    ''' <summary>
    ''' オンライン同期後に送信するコマンド
    ''' </summary>
    Private afterSyncCommand As String
    ''' <summary>
    ''' オンライン同期中判定フラグ
    ''' </summary>
    Private IsOnlineSync As Boolean

    Private IsRetry As Boolean

    ''' <summary>
    ''' コンストラクタ
    ''' IPアドレスとポート番号を設定
    ''' </summary>
    ''' <param name="ipAddress">IPアドレス</param>
    ''' <param name="port">ポート番号</param>
    Public Sub New(ByVal ipAddress As String, ByVal port As String)
        m_TcpListener = New TCPListener(TCPListener.TcpConnectType.Client)
        setServerInfo(ipAddress, port)
        m_ReciveCommandList = New List(Of String)
        syncTimer_init()
        IsRetry = False
    End Sub

    ''' <summary>
    ''' オンライン同期タイマー初期化
    ''' </summary>
    Private Sub syncTimer_init()
        syncTimer = New Timer(60000)
        AddHandler syncTimer.Elapsed, New ElapsedEventHandler(AddressOf syncTimer_tick)
        syncTimer.AutoReset = False ' ワンショット
    End Sub
    ''' <summary>
    ''' オンライン同期タイムアウト処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub syncTimer_tick(sender As Object, e As ElapsedEventArgs)
        If IsOnlineSync Then
            Debug.WriteLine("オンライン確認のタイムアウト")
            IsOnlineSync = False
            Dim args = New TCPListener.TCPEventArgs(TCPListener.TCPEventArgs.TcpEventType.Timeout, New TCPListener.TCPStateObject)
            DoTCPCLEvent(args)
        End If
    End Sub

    ''' <summary>
    ''' 接続先サーバー情報を保持する
    ''' </summary>
    ''' <param name="ipAddress">IPアドレス</param>
    ''' <param name="port">ポート番号</param>
    Public Sub setServerInfo(ByVal ipAddress As String, ByVal port As String)
        m_ipAddress = ipAddress
        m_port = port
    End Sub

    ''' <summary>
    ''' 接続情報設定済み判定
    ''' </summary>
    ''' <returns></returns>
    Public Function IsSetting() As Boolean
        If Not String.IsNullOrEmpty(m_ipAddress) AndAlso Not String.IsNullOrEmpty(m_port) Then
            Return True
        Else
            MsgBox(("MSG00185"))
            Return False
        End If
    End Function

    ''' <summary>
    ''' MPCに接続する
    ''' </summary>
    Public Function CommandConnect() As Boolean
        Dim ret = False
        IsRetry = False
        If Not String.IsNullOrEmpty(m_ipAddress) AndAlso Not String.IsNullOrEmpty(m_port) Then
            ret = m_TcpListener.StartConnect(m_ipAddress, m_port)
            'MsgBox("OK")
        Else
            MsgBox(("MSG00185"))
        End If
        Return ret
    End Function

    ''' <summary>
    ''' MPCとの接続を切断する
    ''' </summary>
    Public Sub CommandDisConnect()
        m_TcpListener.DisConnect()
    End Sub

    ''' <summary>
    ''' 受信コマンドを設定
    ''' </summary>
    ''' <param name="CommandList">受信コマンドリスト</param>
    Public Sub setReceiveCommnad(ByVal CommandList As List(Of String))
        m_ReciveCommandList = CommandList
    End Sub

    ''' <summary>
    ''' 受信コマンドを設定(追加)
    ''' </summary>
    ''' <param name="Command">追加受信コマンド</param>
    Public Sub addReceiveCommnad(ByVal Command As String)
        m_ReciveCommandList.Add(Command)
    End Sub

    ''' <summary>
    ''' 受信コマンドを削除
    ''' </summary>
    ''' <param name="Command">削除受信コマンド</param>
    Public Sub removeReceiveCommnad(ByVal Command As String)
        m_ReciveCommandList.Remove(Command)
    End Sub

    ''' <summary>
    ''' 受信コマンドを全削除
    ''' </summary>
    Public Sub clearReceiveCommnad()
        m_ReciveCommandList.Clear()
    End Sub

    ''' <summary>
    ''' コマンド送信
    ''' </summary>
    ''' <param name="Data">送信文字列</param>
    ''' <returns></returns>
    Public Function CommandSend(ByVal Data As String) As Boolean
        If IsOnlineSync Then
            ' オンライン確認中は他のコマンドを送信しない
            Debug.WriteLine("オンライン確認中のためキャンセル")
            Return False
        End If
        '送信するコマンドデータを退避
        afterSyncCommand = Data
        If SYNC_COMMAND.Any(Function(x) x = getCommandId(Data)) Then
            OnlineCommandSync(Data)
        Else
            m_TcpListener.Send(Data)
        End If
        Return True
    End Function

    Private Sub OnlineCommandSync(ByVal Data As String)

        Debug.WriteLine("オンライン確認を実施")
        IsOnlineSync = True
        Dim sendCommand = createMTCommandStr(COMMAND_MT, getEquipId(Data))

        m_TcpListener.Send(sendCommand)

        syncTimer.Start()
    End Sub

    ''' <summary>
    ''' 通知イベント
    ''' </summary>
    Public Event tcpevent As TCPListener.TCPEventHandler

    ' TCPクライアント用イベント
    Delegate Sub TCPCLEventDelegate(e As TCPListener.TCPEventArgs)
    Private Sub TCPCLEvent(sender As System.Object, e As TCPListener.TCPEventArgs) Handles m_TcpListener.tcpevent
        DoTCPCLEvent(e)
    End Sub
    Private Sub DoTCPCLEvent(e As TCPListener.TCPEventArgs)
        ' 接続完了
        If e.eventType = TCPListener.TCPEventArgs.TcpEventType.Connect Then
            Debug.WriteLine("接続完了")
            If afterSyncCommand <> "" Then
                '再接続後にコマンド送信
                CommandSend(afterSyncCommand)
            End If
        End If
        If e.eventType = TCPListener.TCPEventArgs.TcpEventType.Shutdown Then
            Debug.WriteLine("接続切断")
            If IsSetting() AndAlso IsRetry Then
                ' 再接続
                If CommandConnect() = False Then
                    MsgBox(("MSG00186"))
                End If
            End If
        End If
        If e.eventType = TCPListener.TCPEventArgs.TcpEventType.Send Then
            Debug.WriteLine("データ送信成功")
            If Not IsOnlineSync Then
                afterSyncCommand = ""
            End If
        End If
        ' データ受信
        If e.eventType = TCPListener.TCPEventArgs.TcpEventType.Receive Then
            RaiseEvent tcpevent(Me, e) 'miaojf
            'If m_ReciveCommandList.Count = 0 Then
            '    MsgBox("ReciveCommandList: Nothing")
            'End If

            'For i = 0 To m_ReciveCommandList.Count - 1
            '    MsgBox("ReciveCommandList: " + m_ReciveCommandList(i))
            'Next

            'Debug.WriteLine("データ受信")
            'Dim receiveDataStr = Encoding.ASCII.GetString(e.receiveData, 0, e.receiveData.Count())
            'Debug.WriteLine(receiveDataStr)
            'Dim CommandId = getCommandId(receiveDataStr)

            'MsgBox("ReciveData: " + receiveDataStr)

            'If CommandId IsNot Nothing Then
            '    If IsOnlineSync Then
            '        If COMMAND_MA = CommandId Then
            '            IsOnlineSync = False
            '            Dim commandData = CommandAnalize(receiveDataStr)
            '            If commandData Is Nothing OrElse commandData.result <> COMMAND_RESULT_OK Then
            '                Debug.WriteLine("MAコマンドエラー")
            '                If Not ShowRetryDialog() Then
            '                    Dim ev As TCPListener.TCPEventArgs = New TCPListener.TCPEventArgs(TCPListener.TCPEventArgs.TcpEventType.Cancel, New TCPListener.TCPStateObject())
            '                    RaiseEvent tcpevent(Me, ev)
            '                End If
            '            Else
            '                syncTimer.Stop()
            '                m_TcpListener.Send(afterSyncCommand)
            '                afterSyncCommand = ""
            '            End If
            '        Else
            '            Debug.WriteLine("対象外コマンド：" + CommandId)
            '        End If
            '    Else
            '        If m_ReciveCommandList.Any(Function(x) x = CommandId) Then
            '            ' 受信データを通知
            '            RaiseEvent tcpevent(Me, e)
            '        Else
            '            Debug.WriteLine("対象外コマンド：" + CommandId)
            '        End If
            '    End If
            'Else
            '    Debug.WriteLine("異常データ")
            'End If
        End If
        ' Exception
        If e.eventType = TCPListener.TCPEventArgs.TcpEventType.Exception Then
            Debug.WriteLine("Exception受信")
            Debug.WriteLine(e.exception.Message)
            If Not ShowRetryDialog() Then
                Dim ev As TCPListener.TCPEventArgs = New TCPListener.TCPEventArgs(TCPListener.TCPEventArgs.TcpEventType.Cancel, New TCPListener.TCPStateObject())
                RaiseEvent tcpevent(Me, ev)
            End If
        End If
        ' Timeout
        If e.eventType = TCPListener.TCPEventArgs.TcpEventType.Timeout Then
            If Not ShowRetryDialog() Then
                Dim ev As TCPListener.TCPEventArgs = New TCPListener.TCPEventArgs(TCPListener.TCPEventArgs.TcpEventType.Cancel, New TCPListener.TCPStateObject())
                RaiseEvent tcpevent(Me, ev)
            End If
        End If
    End Sub

    Private Function ShowRetryDialog() As Boolean
        If IsRetry Then
            Return True
        End If
        Dim ShowMsg As New ShowSelectForm
        Dim ShowText = String.Empty
        Dim ShowColor = Color.Red
        With ShowMsg
            .lblText.Text = ("MSG00187")
            .lblText.BackColor = ShowColor
            .lblText.Height = 100
            .BackColor = ShowColor
            .Button1.Visible = False
            .Button4.Visible = False
            .Button2.Text = "Yes"
            .Button2.DialogResult = DialogResult.OK
            .Button2.Visible = True
            .Button3.Text = "No"
            .Button3.DialogResult = DialogResult.No
            .Button3.Visible = True
            .StartPosition = FormStartPosition.CenterParent
            .TopMost = True
        End With
        IsRetry = True
        Dim RetryDialog = ShowMsg.ShowDialog()
        If RetryDialog = DialogResult.OK Then
            ' 上位で切断を行ってもらう
            Dim ev As TCPListener.TCPEventArgs = New TCPListener.TCPEventArgs(TCPListener.TCPEventArgs.TcpEventType.Retry, New TCPListener.TCPStateObject())
            RaiseEvent tcpevent(Me, ev)
        Else
            afterSyncCommand = ""
            IsRetry = False
            Return False
        End If
        Return True
    End Function
End Class
