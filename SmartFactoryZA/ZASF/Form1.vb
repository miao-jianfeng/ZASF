Imports System.Text
Imports System.Threading
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json.Linq

Public Class Form1

    'Dim m_cEquipCommandControl As New clsEquipCooperateCommand("127.0.0.1", "4000")

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim objTh1 As New Thread(AddressOf th1)
        objTh1.Start()
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False

    End Sub
    Private Sub th1()
        Dim strList As New ArrayList
        strList.Add(ip.Text)
        'strList.Add("127.0.0.1")
        Dim strPort As New ArrayList
        strPort.Add(PORT.Text)
        'strPort.Add("4001")
        ReDim m_cEquipCommandContro(0)
        For i As Integer = 0 To strList.Count - 1

            m_cEquipCommandContro(i) = New clsEquipCooperateCommand(strList(i), strPort(i))

            AddHandler m_cEquipCommandContro(i).tcpevent, AddressOf TCPCLEvent

            m_cEquipCommandContro(i).CommandConnect()
            ListBox1.Items.Add("->" & ip.Text & ":" & PORT.Text & " OK")
        Next
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        For i As Integer = 0 To m_cEquipCommandContro.Length - 1

            'm_cEquipCommandContro(i).CommandDisConnect()
            m_cEquipCommandContro(i).CommandSend(txtMSG.Text)
            ListBox1.Items.Add("->" & txtMSG.Text & " OK")

        Next
        'sendResponseCommand(COMMAND_IT, RESPONSE_COMMAND_OK)

    End Sub
    Protected Function sendResponseCommand(ByVal commandName As String, ByVal result As String) As Boolean
        Dim sendCommand = createResponseCommandStr(commandName, result, "11111")
        'm_cEquipCommandControl.CommandSend(sendCommand)
        'm_cEquipCommandContro2.CommandSend(sendCommand)
        Return True
    End Function

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'm_cEquipCommandControl.CommandDisConnect()
        'm_cEquipCommandContro2.CommandDisConnect()
        For i As Integer = 0 To m_cEquipCommandContro.Length - 1

            m_cEquipCommandContro(i).CommandDisConnect()

        Next
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'Dim strList As New ArrayList
        'strList.Add("127.0.0.1")
        'strList.Add("127.0.0.1")
        'Dim tcpListeners(1) As TCPListener
        'For i As Integer = 0 To strList.Count - 1
        '    Dim a As TCPListener.TcpConnectType

        '    tcpListeners(i) = New TCPListener(a.Server)
        '    tcpListeners(i).StartConnect(strList(i), "4001")

        'Next
    End Sub


    ' TCPクライアント用イベント
    Protected m_cEquipCommandContro As clsEquipCooperateCommand()
    'Protected WithEvents m_cEquipCommandContro2 As clsEquipCooperateCommand
    Delegate Sub TCPCLEventDelegate(e As TCPListener.TCPEventArgs)
    ''' <summary>
    ''' イベント受信
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TCPCLEvent(sender As System.Object, e As TCPListener.TCPEventArgs) 'Handles m_cEquipCommandControl.tcpevent
        Debug.WriteLine("TCPCLEvent in")
        Dim adrData As New TCPCLEventDelegate(AddressOf DoTCPCLEvent)
        Me.BeginInvoke(adrData, e)
    End Sub
    Private Sub DoTCPCLEvent(e As TCPListener.TCPEventArgs)
        ' 接続完了
        If e.eventType = TCPListener.TCPEventArgs.TcpEventType.Connect Then
            Debug.WriteLine("接続完了")
        End If
        If e.eventType = TCPListener.TCPEventArgs.TcpEventType.Shutdown Then
            Debug.WriteLine("接続切断")
        End If
        ' データ受信
        If e.eventType = TCPListener.TCPEventArgs.TcpEventType.Receive Then
            Debug.WriteLine("データ受信")
            Dim receiveDataStr = Encoding.ASCII.GetString(e.receiveData, 0, e.receiveData.Count())
            ListBox1.Items.Add("<-" & receiveDataStr)
        End If
        ' Exception
        If e.eventType = TCPListener.TCPEventArgs.TcpEventType.Exception Then
            Debug.WriteLine("Exception受信")
            Debug.WriteLine(e.exception.Message)
            'CommandReceive(Nothing)
        End If
        If e.eventType = TCPListener.TCPEventArgs.TcpEventType.Timeout Then
            Debug.WriteLine("timeout受信")
            'CommandReceive(Nothing)
        End If
        If e.eventType = TCPListener.TCPEventArgs.TcpEventType.Cancel Then
            Debug.WriteLine("Cancel受信")
            'CommandReceive(Nothing)
        End If
        If e.eventType = TCPListener.TCPEventArgs.TcpEventType.Retry Then
            Debug.WriteLine("retry受信")
            'm_cEquipCommandControl.CommandDisConnect()
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'Dim js As JObject
        'Dim JsonStr As String
        'JsonStr = "{""Code"":0,""Message"":""ddddddddmmmmmmmsssss""}"
        'js = JObject.Parse(JsonStr)
        'MsgBox(js.SelectToken("Message"))

        'JsonStr = "{" & Chr(34) & "KeyNo" & Chr(34) & ":" & Chr(34) & "a" & Chr(34) & ","
        'JsonStr &= Chr(34) & "Katamei" & Chr(34) & ":" & Chr(34) & "b" & Chr(34) & ","
        'JsonStr &= Chr(34) & "FrameKatamei" & Chr(34) & ":" & Chr(34) & "c" & Chr(34) & ","
        ''JsonStr &= Chr(34) & "FlowChartKatamei" & Chr(34) & ":" & Chr(34) & "FC-A112-D6-AAAE" & Chr(34) & ","
        'JsonStr &= Chr(34) & "Tnyusu" & Chr(34) & ":" & 2 & ","
        'JsonStr &= Chr(34) & "FeedBoxNum" & Chr(34) & ":" & 1 & ","
        'JsonStr &= Chr(34) & "Resin" & Chr(34) & ":" & Chr(34) & "e" & Chr(34) & ","
        'JsonStr &= Chr(34) & "EquipNo" & Chr(34) & ":" & Chr(34) & "f" & Chr(34) & ","
        'JsonStr &= Chr(34) & "Rackid" & Chr(34) & ":" & Chr(34) & "g" & Chr(34) & "}"
        'MsgBox(JsonStr)

        MsgBox(createJSON)
    End Sub
    Function createJSON() As String

        Dim jss As JavaScriptSerializer = New JavaScriptSerializer
        Dim dic As ArrayList = New ArrayList()

        Dim drow As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()

        drow.Add("KeyNo1", "a")
        drow.Add("KeyNo2", 1)
        drow.Add("KeyNo3", "b")
        drow.Add("KeyNo4", 2)
        drow.Add("KeyNo5", "c")
        drow.Add("KeyNo6", 3)
        drow.Add("KeyNo7", "d")

        dic.Add(drow)

        Return jss.Serialize(dic)
    End Function

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        MsgBox(Math.Ceiling(80 / 84))
    End Sub
End Class
