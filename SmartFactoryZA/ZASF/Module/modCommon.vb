Module modCommon
    Public Function getMsgStr(ByVal msgId As String, Optional ByVal args1 As String = "", Optional ByVal args2 As String = "") As String

        If String.IsNullOrEmpty(args1) AndAlso String.IsNullOrEmpty(args2) Then
            getMsgStr = htMsgMst(msgId)
            Exit Function

        ElseIf Not String.IsNullOrEmpty(args1) AndAlso String.IsNullOrEmpty(args2) Then
            getMsgStr = String.Format(htMsgMst(msgId), args1)
            Exit Function

        Else
            getMsgStr = String.Format(htMsgMst(msgId), args1, args2)
            Exit Function
        End If

    End Function
    ''' <summary>
    ''' 检查是否为空
    ''' </summary>
    ''' <param name="Sender">要检查的控件</param>
    ''' <returns></returns>
    Public Function chkEmpty(Sender As Object) As Boolean
        If Sender.text = "" Then
            MsgBox(getMsgStr("msg001", Sender.tag))
            Sender.Select()
            Return False
        End If
        Return True
    End Function
    ''' <summary>
    ''' 检查是否为空
    ''' </summary>
    ''' <param name="Sender">要检查的控件</param>
    ''' <returns></returns>
    Public Function chkZero(Sender As Object) As Boolean
        If Sender.text = "0" Then
            MsgBox(getMsgStr("msg002", Sender.tag))
            Sender.Select()
            Return False
        End If
        Return True
    End Function

    Public Function chkIP(Sender As Object) As Boolean
        '检查IP地址是否合法函数
        Dim intLoop As Integer
        Dim arrIP
        Dim rtn As Boolean = True '函数初始值为true 
        '将输入的IP用"."分割为数组，数组下标从0开始，所以有效IP分割后的数组上界必须为3   
        arrIP = Split(Sender.text, ".")
        If UBound(arrIP) <> 3 Then
            rtn = False
        Else
            For intLoop = 0 To UBound(arrIP)
                '检查数组元素中各项是否为数字，如果不是则不是有效IP 
                If Not IsNumeric(arrIP(intLoop)) Then
                    rtn = False
                Else
                    '检查IP数字是否满足IP的取值范围 
                    If arrIP(intLoop) > 255 Or arrIP(intLoop) < 0 Then
                        rtn = False
                    End If
                End If
            Next
        End If
        If rtn = False Then
            MsgBox(getMsgStr("msg003", Sender.tag))
            Sender.Select()

        End If
        Return rtn
    End Function
End Module
