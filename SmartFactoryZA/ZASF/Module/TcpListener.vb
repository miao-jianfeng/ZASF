'********************************************************************************
'*
'*      TCP/IP通信クラス
'*
'*      File Name : TcpListener.vb
'*      Title     : TCP/IP通信クラス
'*      機能    　：TCP/IP通信の基本クラス
'*
'********************************************************************************
'----------------------------------<<修正履歴>>----------------------------------
'ﾊﾞｰｼﾞｮﾝ   日付       作業者        ｺﾒﾝﾄ
'--------- ---------- ------------ ----------------------------------------------
' V1.0.0   2018/12/11 M.Ohga       新規作成
'********************************************************************************

Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading

Public Class TCPListener

    Private Sub LogStart(ByVal str As String)
        Debug.WriteLine(str + " IN")
    End Sub
    Private Sub LogEnd(ByVal str As String)
        Debug.WriteLine(str + " End")
    End Sub

    ''' <summary>
    ''' TCP/IP通信状態管理クラス
    ''' </summary>
    Public Class TCPStateObject
        Public workSocket As Socket = Nothing
        Public Const BufferSize As Integer = 1024
        Public buffer As Byte() = New Byte(1023) {}
        Public bytesRead As Integer = 0
        Public receiveData As Byte()
    End Class

    ''' <summary>
    ''' 接続タイプ
    ''' </summary>
    Enum TcpConnectType As Integer
        ''' <summary>
        ''' クライアント
        ''' </summary>
        Client
        ''' <summary>
        ''' サーバー
        ''' </summary>
        Server
    End Enum

    Private sendDone As ManualResetEvent = New ManualResetEvent(False)

    Private mListenerSocket As Socket = Nothing

    Private mConnectStateHash As Hashtable = New Hashtable
    Private mConnectServerStr As String = ""
    Private ReadOnly mTcpConnectType As TcpConnectType

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="inType">接続タイプ</param>
    Public Sub New(ByVal inType As TcpConnectType)
        mTcpConnectType = inType
    End Sub

    ''' <summary>
    ''' 接続状態確認
    ''' </summary>
    ''' <returns>True：接続中</returns>
    Public Function IsConnected() As Boolean
        LogStart(Reflection.MethodBase.GetCurrentMethod.ToString)
        Try
            If mConnectServerStr = "" Then
                Return False
            End If
            If Not mConnectStateHash.ContainsKey(mConnectServerStr) Then
                Return False
            End If
            Dim state As TCPStateObject = mConnectStateHash(mConnectServerStr)

            If state.workSocket IsNot Nothing Then
                If state.workSocket.Connected = True Then
                    Return True
                End If
            End If
        Catch
        Finally
            LogEnd(Reflection.MethodBase.GetCurrentMethod.ToString)
        End Try
        Return False
    End Function

    ''' <summary>
    ''' 接続開始
    ''' </summary>
    ''' <param name="ipString">IPアドレス</param>
    ''' <param name="port">ポート番号</param>
    Public Function StartConnect(ByVal ipString As String, ByVal port As Integer) As Boolean
        LogStart(Reflection.MethodBase.GetCurrentMethod.ToString)
        Dim ret = False
        Try
            If IsConnected() = True Then
                Throw New Exception("Clientはサーバーと接続が確立しています")
            End If
            If mConnectServerStr <> "" Then
                ' 現状は一度失敗したら再接続不可としている
                Throw New Exception("Clientはサーバーと接続中、または接続失敗しています")
            End If

            Dim ipAddress As IPAddress = System.Net.IPAddress.Parse(ipString)
            Dim remoteEP As IPEndPoint = New IPEndPoint(ipAddress, port)
            Dim client As Socket = New Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp)

            Dim state As TCPStateObject = New TCPStateObject()
            state.workSocket = client

            ' 接続中はダミー情報としてリスナー情報を入れておく
            mConnectServerStr = ipString + ":" + port.ToString()

            client.BeginConnect(remoteEP, New AsyncCallback(AddressOf ConnectCallback), state)
            ret = True
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        Finally
            LogEnd(Reflection.MethodBase.GetCurrentMethod.ToString)
        End Try
        Return ret
    End Function

    ''' <summary>
    ''' 接続切断
    ''' </summary>
    Public Sub DisConnect()
        LogStart(Reflection.MethodBase.GetCurrentMethod.ToString)
        Try
            If mConnectServerStr = "" Then
                Exit Sub
            End If
            If Not mConnectStateHash.ContainsKey(mConnectServerStr) Then
                Exit Sub
            End If
            Dim state As TCPStateObject = mConnectStateHash(mConnectServerStr)

            state.workSocket.Disconnect(False)
            state.workSocket.Dispose()

        Catch
        Finally
            mConnectStateHash.Remove(mConnectServerStr)
            mConnectServerStr = ""
            LogEnd(Reflection.MethodBase.GetCurrentMethod.ToString)
        End Try
        Dim ev As TCPEventArgs = New TCPEventArgs(TCPEventArgs.TcpEventType.Shutdown, New TCPStateObject)
        OnTcpEvent(ev)

    End Sub

    Private Sub ConnectCallback(ByVal ar As IAsyncResult)
        LogStart(Reflection.MethodBase.GetCurrentMethod.ToString)
        Try
            Dim state As TCPStateObject = CType(ar.AsyncState, TCPStateObject)
            Dim client As Socket = state.workSocket

            client.EndConnect(ar)
            'MsgBox("TCPIP Connect")

            Dim ev As TCPEventArgs = New TCPEventArgs(TCPEventArgs.TcpEventType.Connect, state)
            Dim connectStr As String = client.RemoteEndPoint.ToString()
            If Not mConnectStateHash.ContainsKey(connectStr) Then
                mConnectStateHash(connectStr) = state
                mConnectServerStr = connectStr
            End If

            client.BeginReceive(state.buffer, 0, TCPStateObject.BufferSize, 0, New AsyncCallback(AddressOf ReadCallback), state)
            OnTcpEvent(ev)
        Catch ex As Exception
            MsgBox("TCPIP Connect Error: " + ex.Message)
            Dim tcpExEv As TCPEventArgs = New TCPEventArgs(TCPEventArgs.TcpEventType.Exception, ex)
            OnTcpEvent(tcpExEv)
        Finally
            LogEnd(Reflection.MethodBase.GetCurrentMethod.ToString)
        End Try
    End Sub


    Public Function StartListening(ByVal port As Integer) As Boolean
        LogStart(Reflection.MethodBase.GetCurrentMethod.ToString)
        Try
            If mListenerSocket IsNot Nothing Then
                Throw New Exception("Listenerは起動中です")
            End If

            Dim ipAddress As IPAddress = System.Net.IPAddress.Parse("0.0.0.0")
            Dim localEndPoint As IPEndPoint = New IPEndPoint(ipAddress, port)
            Dim listener As Socket = New Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp)

            listener.Bind(localEndPoint)
            listener.Listen(100)

            ' 接続待ち状態に設定
            listener.BeginAccept(New AsyncCallback(AddressOf AcceptCallback), listener)

            mListenerSocket = listener
            Return True
        Catch ex As Exception
            Dim tcpExEv As TCPEventArgs = New TCPEventArgs(TCPEventArgs.TcpEventType.Exception, ex)
            OnTcpEvent(tcpExEv)
            Return False
        Finally
            LogEnd(Reflection.MethodBase.GetCurrentMethod.ToString)
        End Try
        Return False
    End Function

    Public Sub AcceptCallback(ByVal ar As IAsyncResult)
        LogStart(Reflection.MethodBase.GetCurrentMethod.ToString)
        Try

            Dim listener As Socket = CType(ar.AsyncState, Socket)
            Dim handler As Socket = listener.EndAccept(ar)
            Dim state As TCPStateObject = New TCPStateObject()
            state.workSocket = handler

            Dim ev As TCPEventArgs = New TCPEventArgs(TCPEventArgs.TcpEventType.Accept, state)
            OnTcpEvent(ev)

            Dim connectStr As String = handler.RemoteEndPoint.ToString()
            If Not mConnectStateHash.ContainsKey(connectStr) Then
                mConnectStateHash(connectStr) = state
                mConnectServerStr = connectStr
            End If

            handler.BeginReceive(state.buffer, 0, TCPStateObject.BufferSize, 0, New AsyncCallback(AddressOf ReadCallback), state)

            ' 接続待ち状態に設定
            listener.BeginAccept(New AsyncCallback(AddressOf AcceptCallback), listener)

        Catch ex As Exception
            Dim tcpExEv As TCPEventArgs = New TCPEventArgs(TCPEventArgs.TcpEventType.Exception, ex)
            OnTcpEvent(tcpExEv)
        Finally
            LogEnd(Reflection.MethodBase.GetCurrentMethod.ToString)
        End Try
    End Sub

    Public Sub ReadCallback(ByVal ar As IAsyncResult)
        LogStart(Reflection.MethodBase.GetCurrentMethod.ToString)
        Dim remoteEndPointStr As String = ""
        Dim state As TCPStateObject = Nothing
        Try
            Dim content As String = String.Empty
            state = CType(ar.AsyncState, TCPStateObject)
            Dim handler As Socket = state.workSocket
            If handler.Connected = False Then
                Debug.WriteLine("切断済み")
                Exit Try
            End If
            remoteEndPointStr = handler.RemoteEndPoint.ToString()
            Dim bytesRead As Integer = handler.EndReceive(ar)

            If bytesRead > 0 Then
                'state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead))
                content = Encoding.ASCII.GetString(state.buffer, 0, bytesRead)
                state.bytesRead = bytesRead

                state.receiveData = New Byte(bytesRead - 1) {}
                Array.Copy(state.buffer, 0, state.receiveData, 0, bytesRead)
                Dim ev As TCPEventArgs = New TCPEventArgs(TCPEventArgs.TcpEventType.Receive, state)
                OnTcpEvent(ev)
            ElseIf bytesRead = 0 Then
                MsgBox("TCPIP Disconnect")
                'handler.Disconnect(True)
                handler.Close()
            End If

            handler.BeginReceive(state.buffer, 0, TCPStateObject.BufferSize, 0, New AsyncCallback(AddressOf ReadCallback), state)
        Catch ex As Exception
            MsgBox("TCPIP Receive Error: " + ex.Message)
            Dim tcpExEv As TCPEventArgs = New TCPEventArgs(TCPEventArgs.TcpEventType.Exception, ex)
            OnTcpEvent(tcpExEv)
        Finally
            LogEnd(Reflection.MethodBase.GetCurrentMethod.ToString)
        End Try

    End Sub

    ''' <summary>
    ''' データ送信
    ''' </summary>
    ''' <param name="data">送信文字列</param>
    Public Sub Send(ByVal data As String)

        'MsgBox("Send Command: " + data)
        LogStart(Reflection.MethodBase.GetCurrentMethod.ToString)
        Debug.WriteLine("data : " + data)
        Try
            Send(Encoding.ASCII.GetBytes(data & vbCr)) 'UPD MIAO 2019/2/22
        Catch ex As Exception
            Dim tcpExEv As TCPEventArgs = New TCPEventArgs(TCPEventArgs.TcpEventType.Exception, ex)
            OnTcpEvent(tcpExEv)
        Finally
            LogEnd(Reflection.MethodBase.GetCurrentMethod.ToString)
        End Try
    End Sub

    ''' <summary>
    ''' データ送信
    ''' </summary>
    ''' <param name="byteData">送信バイト列</param>
    Public Sub Send(ByVal byteData As Byte())
        LogStart(Reflection.MethodBase.GetCurrentMethod.ToString)
        Try
            If mTcpConnectType <> TcpConnectType.Client Then
                Throw New Exception("Client以外は処理できません")
            End If
            If Not mConnectStateHash.ContainsKey(mConnectServerStr) Then
                Throw New Exception("接続されていません: " + mConnectServerStr)
            End If
            Send(CType(mConnectStateHash(mConnectServerStr), TCPStateObject), byteData)
        Catch ex As Exception
            Dim tcpExEv As TCPEventArgs = New TCPEventArgs(TCPEventArgs.TcpEventType.Exception, ex)
            OnTcpEvent(tcpExEv)
        Finally
            LogEnd(Reflection.MethodBase.GetCurrentMethod.ToString)
        End Try
    End Sub


    Public Sub Send(ByVal connectStr As String, ByVal byteData As Byte())
        Try
            If mTcpConnectType <> TcpConnectType.Server Then
                Throw New Exception("Server以外は処理できません")
            End If
            If Not mConnectStateHash.ContainsKey(connectStr) Then
                Throw New Exception("接続されていません: " + connectStr)
            End If
            Send(CType(mConnectStateHash(connectStr), TCPStateObject), byteData)
        Catch ex As Exception
            Dim tcpExEv As TCPEventArgs = New TCPEventArgs(TCPEventArgs.TcpEventType.Exception, ex)
            OnTcpEvent(tcpExEv)
        End Try
    End Sub

    Public Sub Send(ByVal connectStr As String, ByVal data As String)
        Try
            Send(CType(mConnectStateHash(connectStr), TCPStateObject), Encoding.ASCII.GetBytes(data))
        Catch ex As Exception
            Dim tcpExEv As TCPEventArgs = New TCPEventArgs(TCPEventArgs.TcpEventType.Exception, ex)
            OnTcpEvent(tcpExEv)
        End Try
    End Sub

    Public Sub Send(ByVal state As TCPStateObject, ByVal data As String)
        Try
            Send(state, Encoding.ASCII.GetBytes(data))
        Catch ex As Exception
            Dim tcpExEv As TCPEventArgs = New TCPEventArgs(TCPEventArgs.TcpEventType.Exception, ex)
            OnTcpEvent(tcpExEv)
        End Try
    End Sub

    ''' <summary>
    ''' TCP/IP送信
    ''' </summary>
    ''' <param name="state">管理オブジェクト</param>
    ''' <param name="byteData">送信データ</param>
    Protected Sub Send(ByVal state As TCPStateObject, ByVal byteData As Byte())
        LogStart(Reflection.MethodBase.GetCurrentMethod.ToString)
        Try
            If state.workSocket.Connected = False Then
                Throw New Exception("接続されていません: " + state.workSocket.RemoteEndPoint.ToString())
            End If
            sendDone.[Reset]()
            Dim handler As Socket = state.workSocket
            handler.BeginSend(byteData, 0, byteData.Length, 0, New AsyncCallback(AddressOf SendCallback), state)
            If sendDone.WaitOne(5000) = False Then
                Throw New SocketException
            End If

        Catch ex As Exception
            Dim tcpExEv As TCPEventArgs = New TCPEventArgs(TCPEventArgs.TcpEventType.Exception, ex)
            OnTcpEvent(tcpExEv)
        Finally
            LogEnd(Reflection.MethodBase.GetCurrentMethod.ToString)
        End Try
    End Sub



    Private Sub SendCallback(ByVal ar As IAsyncResult)
        LogStart(Reflection.MethodBase.GetCurrentMethod.ToString)
        Try
            Dim state As TCPStateObject = CType(ar.AsyncState, TCPStateObject)
            Dim handler As Socket = state.workSocket
            Dim bytesSent As Integer = handler.EndSend(ar)

            sendDone.[Set]()
            Dim ev As TCPEventArgs = New TCPEventArgs(TCPEventArgs.TcpEventType.Send, state)
            OnTcpEvent(ev)
        Catch ex As Exception
            Dim tcpExEv As TCPEventArgs = New TCPEventArgs(TCPEventArgs.TcpEventType.Exception, ex)
            OnTcpEvent(tcpExEv)
        Finally
            LogEnd(Reflection.MethodBase.GetCurrentMethod.ToString)
        End Try
    End Sub

    ' イベント定義
    Public Class TCPEventArgs
        Inherits EventArgs

        Public Enum TcpEventType As Integer
            Receive
            Accept
            Connect
            Shutdown
            Exception
            Timeout
            Send
            Retry
            Cancel
        End Enum

        Private mState As TCPStateObject
        Private mTcpException As Exception
        Private mEventType As TcpEventType

        Sub New(type As TcpEventType, value As TCPStateObject)
            mEventType = type
            mState = value
        End Sub
        Sub New(type As TcpEventType, value As Exception)
            mEventType = type
            mTcpException = value
        End Sub
        Sub New(type As TcpEventType, state As TCPStateObject, ex As Exception)
            mEventType = type
            mState = state
            mTcpException = ex
        End Sub

        Public Property state() As TCPStateObject
            Get
                Return mState
            End Get
            Set(value As TCPStateObject)
                mState = value
            End Set
        End Property
        Public Property eventType() As String
            Get
                Return mEventType
            End Get
            Set(value As String)
                ' 何もしない
            End Set
        End Property
        Public Property connectStr() As String
            Get
                Return mState.workSocket.LocalEndPoint.ToString()
            End Get
            Set(value As String)
                ' 何もしない
            End Set
        End Property
        Public Property acceptStr() As String
            Get
                Return mState.workSocket.RemoteEndPoint.ToString()
            End Get
            Set(value As String)
                ' 何もしない
            End Set
        End Property
        Public Property receiveData() As Byte()
            Get
                Return mState.receiveData
            End Get
            Set(value As Byte())
                ' 何もしない
            End Set
        End Property
        Public Property exception() As Exception
            Get
                Return mTcpException
            End Get
            Set(value As Exception)
                ' 何もしない
            End Set
        End Property
    End Class
    Public Delegate Sub TCPEventHandler(ByVal sender As Object, ByVal e As TCPEventArgs)
    Public Event tcpevent As TCPEventHandler
    Protected Sub OnTcpEvent(ByVal e As TCPEventArgs)
        RaiseEvent tcpevent(Me, e)
    End Sub

End Class
