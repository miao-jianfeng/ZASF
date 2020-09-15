Module mdl_EquipCooperateCommand
#Region "定義"
    Public Class CommandIdentifier
        Public identifier As Char
        Public size As Integer
        Public type As String
    End Class

    Public Const COMMAND_IT As String = "IT" 'コマンドIT（着工処理結果報告）
    Public Const COMMAND_IU As String = "IU" 'コマンドIU（計上処理結果報告）
    Public Const COMMAND_LN As String = "LN" 'コマンドLN（ICタグ着工指示完了報告（組立工程））
    Public Const COMMAND_LH As String = "LH" 'コマンドLH（ロット完了報告回答）
    Public Const COMMAND_LC As String = "LC" 'コマンドLC（ICタグ着工報告（組立工程））
    Public Const COMMAND_LF As String = "LF" 'コマンドLF（ロット完了報告）
    Public Const COMMAND_LD As String = "LD" 'コマンドLD（ICタグ着工指示完了報告（組立工程））
    Public Const COMMAND_LG As String = "LG" 'コマンドLG（ロット完了報告回答）
    Public Const COMMAND_TV As String = "TV" 'コマンドTV (ICタグ着工報告（TEST工程）)
    Public Const COMMAND_TW As String = "TW" 'コマンドTV (ICタグ完了報告（TEST工程）)
    Public Const COMMAND_MT As String = "MT" 'コマンドMT (オンライン確認)
    Public Const COMMAND_MA As String = "MA" 'コマンドMA (オンライン確認応答)
    Public Const COMMAND_RL As String = "RL" 'コマンドRL（ロットエンド報告回答）
    Public Const COMMAND_EY As String = "EY" 'コマンドEY（製造完成報告 ロットNo有り SF <- MPC-Net（レシピ照合））'ADD MIAO 2019/2/20
    Public Const COMMAND_EZ As String = "EZ" 'コマンドEZ（製造完成報告 ロットNo無し SF <- MPC-Net（レシピ照合））'ADD MIAO 2019/2/20

    Public CMD_RACK_SEPARATOR_LIST As List(Of String) = New List(Of String)(New String() {"A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "R", "S", "T", "U", "V", "W"})

    Public SYNC_COMMAND As List(Of String) = New List(Of String)(New String() {COMMAND_LC, COMMAND_TV})
    Public Const TEMP_ENV_VARIABLE As String = "%temp%"
    Public Const RESPONSE_COMMAND_OK As Integer = 0 '応答コマンドresult OK
    Public Const RESPONSE_COMMAND_NG As Integer = 1 '応答コマンドresult NG
    Public Const COMMAND_DATA_TYPE_STRING = "STR"
    Public Const COMMAND_DATA_TYPE_INTEGER = "INT"

    Public Const COMMAND_REMOVABLE_ON = 0   '着脱モード：挿入
    Public Const COMMAND_REMOVABLE_OFF = 1  '着脱モード：抜き

    Public Const COMMAND_RESULT_OK = 0

    Private Const ENCODING As String = "SHIFT-JIS"

    Public Const CMD_ANALIZE_RESULT_SUCCESS = 0
    Public Const CMD_ANALIZE_RESULT_ERROR = 1

    ''' <summary>
    ''' LDコマンド結果
    ''' </summary>
    Private COMMAND_RESULT_DICTIONARY = New Dictionary(Of Integer, String) From {
        {COMMAND_RESULT_OK, "OK"},
        {9, "パラメータ照合結果NG"},
        {900, "設備条件異常"},
        {901, "設備設定品種異常"},
        {902, "設備パラメータ異常"},
        {903, "インタロック指示異常"},
        {904, "設備要求受付不可"},
        {905, "処理タイムオーバー"},
        {906, "Ftpエラー"},
        {907, "パラメータ照合Master無し"},
        {908, "設備データ取得異常"}，
        {909, "母体ロット　データ異常"},'ADD MIAO 2019/2/27
        {910, "端数ロット　データ異常"}'ADD MIAO 2019/2/27
    }

    ''' <summary>
    ''' コマンド結果文字列取得
    ''' </summary>
    ''' <param name="code">エラーコード</param>
    ''' <returns></returns>
    Public Function getCommandResultString(code As Integer) As String
        Dim retStr = String.Empty
        Try
            retStr = COMMAND_RESULT_DICTIONARY(code)
        Catch ex As Exception
            retStr = "OTHER ERROR"
        End Try
        Return retStr
    End Function

    ''' <summary>
    ''' コマンドID識別子
    ''' </summary>
    Public IDENTIFIER_COMMANDID As CommandIdentifier = New CommandIdentifier With {.size = 2}
    ''' <summary>
    ''' 装置ID識別子
    ''' </summary>
    Public IDENTIFIER_EQUIPID As CommandIdentifier = New CommandIdentifier With {.identifier = "E"c, .size = 30, .type = COMMAND_DATA_TYPE_STRING}
    ''' <summary>
    ''' 作業者No識別子
    ''' </summary>
    Public IDENTIFIER_OPERATORNO As CommandIdentifier = New CommandIdentifier With {.identifier = "S"c, .size = 30, .type = COMMAND_DATA_TYPE_STRING}
    ''' <summary>
    ''' ラック数識別子
    ''' </summary>
    Public IDENTIFIER_RACKNUM As CommandIdentifier = New CommandIdentifier With {.identifier = "R"c, .size = 2, .type = COMMAND_DATA_TYPE_INTEGER}
    Public IDENTIFIER_RACKNUM_MAX As Integer = 5
    ''' <summary>
    ''' ラックNo識別子
    ''' </summary>
    Public IDENTIFIER_RACKNO As CommandIdentifier = New CommandIdentifier With {.identifier = "A"c, .size = 20, .type = COMMAND_DATA_TYPE_STRING}
    ''' <summary>
    ''' 良品数識別子
    ''' </summary>
    Public IDENTIFIER_COMPNUM As CommandIdentifier = New CommandIdentifier With {.identifier = "G"c, .size = 8, .type = COMMAND_DATA_TYPE_INTEGER}
    ''' <summary>
    ''' 不良品数識別子
    ''' </summary>
    Public IDENTIFIER_BADNUM As CommandIdentifier = New CommandIdentifier With {.identifier = "N"c, .size = 8, .type = COMMAND_DATA_TYPE_INTEGER}
    ''' <summary>
    ''' 総数識別子
    ''' </summary>
    Public IDENTIFIER_ALLNUM As CommandIdentifier = New CommandIdentifier With {.identifier = "T"c, .size = 8, .type = COMMAND_DATA_TYPE_INTEGER}
    ''' <summary>
    ''' フレーム数識別子
    ''' </summary>
    Public IDENTIFIER_FRAMENUM As CommandIdentifier = New CommandIdentifier With {.identifier = "F"c, .size = 5, .type = COMMAND_DATA_TYPE_INTEGER}
    ''' <summary>
    ''' パターン識別子
    ''' </summary>
    Public IDENTIFIER_PATTERN As CommandIdentifier = New CommandIdentifier With {.identifier = "M"c, .size = 2, .type = COMMAND_DATA_TYPE_INTEGER}
    ''' <summary>
    ''' モード識別子
    ''' </summary>
    Public IDENTIFIER_MODE As CommandIdentifier = New CommandIdentifier With {.identifier = "P"c, .size = 2, .type = COMMAND_DATA_TYPE_INTEGER}
    ''' <summary>
    ''' 品名識別子
    ''' </summary>
    Public IDENTIFIER_NAME As CommandIdentifier = New CommandIdentifier With {.identifier = "P"c, .size = 60, .type = COMMAND_DATA_TYPE_STRING}
    ''' <summary>
    ''' ロットNo識別子
    ''' </summary>
    Public IDENTIFIER_LOTNO As CommandIdentifier = New CommandIdentifier With {.identifier = "L"c, .size = 30, .type = COMMAND_DATA_TYPE_STRING}
    ''' <summary>
    ''' 週コード識別子
    ''' </summary>
    Public IDENTIFIER_WEEKCODE As CommandIdentifier = New CommandIdentifier With {.identifier = "C"c, .size = 30, .type = COMMAND_DATA_TYPE_STRING}
    ''' <summary>
    ''' パッケージ識別子
    ''' </summary>
    Public IDENTIFIER_PACKAGE As CommandIdentifier = New CommandIdentifier With {.identifier = "C"c, .size = 60, .type = COMMAND_DATA_TYPE_STRING}
    ''' <summary>
    ''' 目抜け数（×印数）識別子
    ''' </summary>
    Public IDENTIFIER_CROSSNUM As CommandIdentifier = New CommandIdentifier With {.identifier = "M"c, .size = 5, .type = COMMAND_DATA_TYPE_INTEGER}
    ''' <summary>
    ''' ウエハロットNo/拡散ロットNo識別子
    ''' </summary>
    Public IDENTIFIER_WAFERLOT As CommandIdentifier = New CommandIdentifier With {.identifier = "W"c, .size = 30, .type = COMMAND_DATA_TYPE_STRING}
    ''' <summary>
    ''' PBレイアウト識別子
    ''' </summary>
    Public IDENTIFIER_PBLAYOUT As CommandIdentifier = New CommandIdentifier With {.identifier = "P"c, .size = 30, .type = COMMAND_DATA_TYPE_STRING}
    ''' <summary>
    ''' 着脱モード識別子
    ''' </summary>
    Public IDENTIFIER_REMOVABLE As CommandIdentifier = New CommandIdentifier With {.identifier = "P"c, .size = 2, .type = COMMAND_DATA_TYPE_INTEGER}
    ''' <summary>
    ''' 連続着工転送
    ''' </summary>
    Public IDENTIFIER_CONTINUOUS As CommandIdentifier = New CommandIdentifier With {.identifier = "C"c, .size = 1, .type = COMMAND_DATA_TYPE_INTEGER}
    ''' <summary>
    ''' バーイン情報
    ''' </summary>
    Public IDENTIFIER_BIINFO As CommandIdentifier = New CommandIdentifier With {.identifier = "B"c, .size = 2, .type = COMMAND_DATA_TYPE_INTEGER}
    ''' <summary>
    ''' トレイ型名識別子
    ''' </summary>
    Public IDENTIFIER_TRAYTYPE As CommandIdentifier = New CommandIdentifier With {.identifier = "T"c, .size = 30, .type = COMMAND_DATA_TYPE_STRING}
    ''' <summary>
    ''' トレイ製品搭載数識別子
    ''' </summary>
    Public IDENTIFIER_TRAYPRODUCTNUM As CommandIdentifier = New CommandIdentifier With {.identifier = "N"c, .size = 5, .type = COMMAND_DATA_TYPE_INTEGER}
    ''' <summary>
    ''' 母体/棚端数品識別子
    ''' </summary>
    Public IDENTIFIER_PRODUCTTYPE As CommandIdentifier = New CommandIdentifier With {.identifier = "H"c, .size = 2, .type = COMMAND_DATA_TYPE_INTEGER}
    ''' <summary>
    ''' マーク図面識別子
    ''' </summary>
    Public IDENTIFIER_MARK As CommandIdentifier = New CommandIdentifier With {.identifier = "M"c, .size = 30, .type = COMMAND_DATA_TYPE_STRING}
    ''' <summary>
    ''' 治工具識別子
    ''' </summary>
    Public IDENTIFIER_EQUIPMAT As CommandIdentifier = New CommandIdentifier With {.identifier = "M"c, .size = 30, .type = COMMAND_DATA_TYPE_STRING}
    ''' <summary>
    ''' カバーテープ名識別子
    ''' </summary>
    Public IDENTIFIER_COVERTAPE As CommandIdentifier = New CommandIdentifier With {.identifier = "C"c, .size = 30, .type = COMMAND_DATA_TYPE_STRING}
    ''' <summary>
    ''' エンボスキャリアテープ名識別子
    ''' </summary>
    Public IDENTIFIER_CARRIERTAPE As CommandIdentifier = New CommandIdentifier With {.identifier = "E"c, .size = 30, .type = COMMAND_DATA_TYPE_STRING}
    ''' <summary>
    ''' リール型名識別子
    ''' </summary>
    Public IDENTIFIER_REELTYPE As CommandIdentifier = New CommandIdentifier With {.identifier = "R"c, .size = 30, .type = COMMAND_DATA_TYPE_STRING}
    ''' <summary>
    ''' リール（カートン）収納数識別子
    ''' </summary>
    Public IDENTIFIER_REELNUM As CommandIdentifier = New CommandIdentifier With {.identifier = "S"c, .size = 5, .type = COMMAND_DATA_TYPE_INTEGER}
    ''' <summary>
    ''' 内装箱型名識別子
    ''' </summary>
    Public IDENTIFIER_INNERBOXTYPE As CommandIdentifier = New CommandIdentifier With {.identifier = "B"c, .size = 30, .type = COMMAND_DATA_TYPE_STRING}
    ''' <summary>
    ''' 結果識別子
    ''' </summary>
    Public IDENTIFIER_RESULT As CommandIdentifier = New CommandIdentifier With {.identifier = "R"c, .size = 3, .type = COMMAND_DATA_TYPE_INTEGER}
    ''' <summary>
    ''' 予備(IT/IU)
    ''' </summary>
    Public IDENTIFIER_RESERVE As CommandIdentifier = New CommandIdentifier With {.identifier = "S"c, .size = 30, .type = COMMAND_DATA_TYPE_STRING}

    ''' <summary>
    ''' (TV)良品数識別子
    ''' </summary>
    Public IDENTIFIER_COMPNUM_TV As CommandIdentifier = New CommandIdentifier With {.identifier = "G"c, .size = 5, .type = COMMAND_DATA_TYPE_INTEGER}

    ''' <summary>
    ''' (TV)不良品数識別子
    ''' </summary>
    Public IDENTIFIER_BADNUM_TV As CommandIdentifier = New CommandIdentifier With {.identifier = "N"c, .size = 5, .type = COMMAND_DATA_TYPE_INTEGER}

    Public Class CommandWatchList
        Public command As String
        Public eof_path As String
        Public command_path As String
    End Class

#End Region

    Private Function makeEofFilename(command As String, result As String) As String
        If result = RESPONSE_COMMAND_OK Then
            Return command + "-OK"
        Else
            Return command + "-NG"
        End If
    End Function
    ''' <summary>
    ''' IT/IUコマンドを作成します。
    ''' </summary>
    ''' <param name="commandName"></param>
    Public Sub createResponseCommand(ByVal commandName, ByVal result)

        createCommandEOF(commandName, result)

    End Sub

    ''' <summary>
    ''' IT/IUコマンドの通信文字列を作成します。
    ''' </summary>
    ''' <param name="commandName">コマンド</param>
    ''' <param name="result">結果</param>
    ''' <param name="equipId">装置ID</param>
    ''' <returns>IT/IUコマンド文字列</returns>
    Public Function createResponseCommandStr(ByVal commandName As String, ByVal result As String, ByVal equipId As String) As String
        Dim dataString = String.Empty
        Try
            Dim list As New List(Of String)
            'コマンドID
            list.Add(commandName)
            '装置ID
            list.Add(writeCommandData(equipId, IDENTIFIER_EQUIPID))
            '結果
            list.Add(writeCommandData(result, IDENTIFIER_RESULT))
            '予備
            list.Add(writeCommandData("", IDENTIFIER_RESERVE))
            'listの文字列を連結
            dataString = String.Join("", list.ToArray())
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try
        Return dataString
    End Function

    Private Function makeFilename(command As String, extension As String) As String
        '環境変数の値を取得
        Dim tempPath = getEnvValue()

        If String.IsNullOrEmpty(tempPath) Then
            MsgBox(("MSG00174"))
            Return String.Empty
        End If

        'ファイル名作成
        Dim fileName = IO.Path.Combine(tempPath, command + extension)
        '存在チェック
        If System.IO.File.Exists(fileName) Then
            MsgBox(("MSG00175"))
            Return String.Empty
        End If
        Return fileName
    End Function

    Private Function writeCommandData(data As String, ID As CommandIdentifier) As String
        Return ID.identifier + IIf(ID.type = COMMAND_DATA_TYPE_STRING, data.PadRight(ID.size), data.PadLeft(ID.size, "0"c))
    End Function

    ''' <summary>
    ''' LCコマンドを作成します。
    ''' </summary>
    Public Function createLCCommand(command As String, equipId As String, name As String, operatorId As String, lotNo As String, compnum As String, badnum As String, framenum As String, package As String, crossnum As String, waferNo As String, pbLayout As String) As Boolean
        'listの文字列を連結
        Dim dataString = createLCCommandStr(command, equipId, name, operatorId, lotNo, compnum, badnum, framenum, package, crossnum, waferNo, pbLayout)

        ' ファイル名取得
        Dim fileName = makeFilename(command, ".txt")
        If String.IsNullOrEmpty(fileName) Then
            Return False
        End If
        'ファイルの書き出し
        Try
            System.IO.File.WriteAllText(fileName, dataString, System.Text.Encoding.GetEncoding(ENCODING))
            createCommandEOF(command)
            MsgBox("createLCCommand: " + command)
        Catch ex As Exception
            MsgBox(("MSG00176"))
            Return False
        End Try
        Return True
    End Function
    ''' <summary>
    ''' LCコマンド文字列を作成します。
    ''' </summary>
    Public Function createLCCommandStr(command As String, equipId As String, name As String, operatorId As String, lotNo As String, compnum As String, badnum As String, framenum As String, package As String, crossnum As String, waferNo As String, pbLayout As String) As String
        Dim dataString = String.Empty
        Try
            Dim list As New List(Of String)
            'コマンドID
            list.Add(command)
            '装置ID
            list.Add(writeCommandData(equipId, IDENTIFIER_EQUIPID))
            'パターン
            list.Add(writeCommandData("test1", IDENTIFIER_PATTERN))
            'モード
            list.Add(writeCommandData("test2", IDENTIFIER_MODE))
            '製品名/ 品名
            list.Add(writeCommandData(name, IDENTIFIER_NAME))
            '作業者No
            list.Add(writeCommandData(operatorId, IDENTIFIER_OPERATORNO))
            '組立ロットNo
            list.Add(writeCommandData(lotNo, IDENTIFIER_LOTNO))
            '着工数（良品数）
            list.Add(writeCommandData(compnum, IDENTIFIER_COMPNUM_TV))
            '着工数（不良数）
            list.Add(writeCommandData(badnum, IDENTIFIER_BADNUM_TV))
            'フレーム数
            list.Add(writeCommandData(framenum, IDENTIFIER_FRAMENUM))
            '週コード
            list.Add(writeCommandData("", IDENTIFIER_WEEKCODE))
            'パッケージ名/ 製品外形
            list.Add(writeCommandData(package, IDENTIFIER_PACKAGE))
            '目抜け数
            list.Add(writeCommandData(crossnum, IDENTIFIER_CROSSNUM))
            'ウエハロットNo/ 拡散ロットNo
            list.Add(writeCommandData(waferNo, IDENTIFIER_WAFERLOT))
            'PBレイアウト
            list.Add(writeCommandData(pbLayout, IDENTIFIER_PBLAYOUT))
            '着脱モード
            list.Add(writeCommandData(COMMAND_REMOVABLE_ON, IDENTIFIER_REMOVABLE))
            '連続着工転送
            list.Add(writeCommandData(COMMAND_REMOVABLE_ON, IDENTIFIER_CONTINUOUS))
            'バーイン情報
            list.Add(writeCommandData(COMMAND_REMOVABLE_OFF, IDENTIFIER_BIINFO))

            'listの文字列を連結
            dataString = String.Join("", list.ToArray())
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try
        Return dataString
    End Function

    Public Function createLFCommand(command As String, equipId As String, lotNo As String, name As String) As Boolean
        Dim list As New List(Of String)
        'コマンドID
        list.Add(command)
        '装置ID
        list.Add(writeCommandData(equipId, IDENTIFIER_EQUIPID))
        '組立ロットNo
        list.Add(writeCommandData(lotNo, IDENTIFIER_LOTNO))
        '製品名/ 品名
        list.Add(writeCommandData(name, IDENTIFIER_NAME))
        '予備
        list.Add("R")
        list.Add("00000")
        '予備
        list.Add("S")
        list.Add(Space(30))

        'listの文字列を連結
        Dim dataString = String.Join("", list.ToArray())

        ' ファイル名取得
        Dim fileName = makeFilename(command, ".txt")
        If String.IsNullOrEmpty(fileName) Then
            Return False
        End If
        'ファイルの書き出し
        Try
            System.IO.File.WriteAllText(fileName, dataString, System.Text.Encoding.GetEncoding(ENCODING))
            createCommandEOF(command)
            MsgBox("createLFCommand: " + command)
        Catch ex As Exception
            MsgBox(("MSG00176"))
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' TVコマンドを作成します。
    ''' </summary>
    Public Function createTVCommand(command As String, equipId As String, name As String, operatorId As String,
                                    lotNo As String, compnum As String, badnum As String, package As String,
                                    waferNo As String, crossnum As String, trayType As String, productType As String, weekcode As String,
                                    trayQty As String, markChart As String, equipMat As String, coverTape As String,
                                    carrierTape As String, reelType As String, reelQty As String, boxType As String) As Boolean
        'listの文字列を連結
        Dim dataString = createTVCommandStr(command, equipId, name, operatorId, lotNo, compnum, badnum, package, waferNo, crossnum, trayType, productType,
                                            weekcode, trayQty, markChart, equipMat, coverTape, carrierTape, reelType, reelQty, boxType)

        ' ファイル名取得
        Dim fileName = makeFilename(command, ".txt")
        If String.IsNullOrEmpty(fileName) Then
            Return False
        End If
        'ファイルの書き出し
        Try
            System.IO.File.WriteAllText(fileName, dataString, System.Text.Encoding.GetEncoding(ENCODING))
            createCommandEOF(command)
            MsgBox("createTVCommand: " + command)
        Catch ex As Exception
            MsgBox(("MSG00176"))
            Return False
        End Try
        Return True
    End Function

    ''' <summary>
    ''' TVコマンド文字列を作成します。
    ''' </summary>
    Public Function createTVCommandStr(command As String, equipId As String, name As String, operatorId As String,
                                       lotNo As String, compnum As String, badnum As String, package As String, waferNo As String,
                                       crossnum As String, trayType As String, productType As String, weekcode As String,
                                       trayQty As String, markChart As String, equipMat As String, coverTape As String,
                                       carrierTape As String, reelType As String, reelQty As String, boxType As String) As String
        Dim dataString = String.Empty
        Try
            Dim list As New List(Of String)
            'コマンドID
            list.Add(command)
            '装置ID
            list.Add(writeCommandData(equipId, IDENTIFIER_EQUIPID))
            'パターン
            list.Add(writeCommandData("test1", IDENTIFIER_PATTERN))
            'モード
            list.Add(writeCommandData("test2", IDENTIFIER_MODE))
            '製品名/ 品名
            list.Add(writeCommandData(name, IDENTIFIER_NAME))
            '作業者No
            list.Add(writeCommandData(operatorId, IDENTIFIER_OPERATORNO))
            '組立ロットNo
            list.Add(writeCommandData(lotNo, IDENTIFIER_LOTNO))
            '着工数（良品数）
            list.Add(writeCommandData(compnum, IDENTIFIER_COMPNUM_TV))
            '着工数（不良数）
            list.Add(writeCommandData(badnum, IDENTIFIER_BADNUM_TV))
            '週コード
            list.Add(writeCommandData(weekcode, IDENTIFIER_WEEKCODE))
            'パッケージ名/ 製品外形
            list.Add(writeCommandData(package, IDENTIFIER_PACKAGE))
            'ウエハロットNo/拡散ロットNo
            list.Add(writeCommandData(waferNo, IDENTIFIER_WAFERLOT))
            '目抜け数
            list.Add(writeCommandData(crossnum, IDENTIFIER_CROSSNUM))
            'トレイ型名
            list.Add(writeCommandData(trayType, IDENTIFIER_TRAYTYPE))
            'トレイ製品搭載数
            list.Add(writeCommandData(trayQty, IDENTIFIER_TRAYPRODUCTNUM))
            '母体/棚端数品
            list.Add(writeCommandData(productType, IDENTIFIER_PRODUCTTYPE))
            'マーク図面
            list.Add(writeCommandData(markChart, IDENTIFIER_MARK))
            '治工具
            list.Add(writeCommandData(equipMat, IDENTIFIER_EQUIPMAT))
            'カバーテープ名
            list.Add(writeCommandData(coverTape, IDENTIFIER_COVERTAPE))
            'エンボスキャリアテープ名
            list.Add(writeCommandData(carrierTape, IDENTIFIER_CARRIERTAPE))
            'リール型名
            list.Add(writeCommandData(reelType, IDENTIFIER_REELTYPE))
            'リール(カートン)収納数
            list.Add(writeCommandData(reelQty, IDENTIFIER_REELNUM))
            '内装箱型名
            list.Add(writeCommandData(boxType, IDENTIFIER_INNERBOXTYPE))

            'listの文字列を連結
            dataString = String.Join("", list.ToArray())
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try
        Return dataString
    End Function

    ''' <summary>
    ''' MTコマンド文字列を作成します。
    ''' </summary>
    Public Function createMTCommandStr(command As String, equipId As String) As String
        Dim dataString = String.Empty
        Try
            Dim list As New List(Of String)
            'コマンドID
            list.Add(command)
            '装置ID
            list.Add(writeCommandData(equipId, IDENTIFIER_EQUIPID))
            'listの文字列を連結
            dataString = String.Join("", list.ToArray())
        Catch ex As Exception

            Debug.WriteLine(ex.Message)
        End Try
        Return dataString
    End Function

    Public Function getEnvValue() As String
        Dim value As String = String.Empty
        Try
            '環境変数の値を取得
            value = System.Environment.ExpandEnvironmentVariables(TEMP_ENV_VARIABLE)
        Catch ex As Exception
            value = String.Empty
            Return value
        End Try

        Return value
    End Function

    Public Sub createCommandEOF(commandName As String, result As String)

        Dim fileName = makeFilename(makeEofFilename(commandName, result), ".eof")
        If String.IsNullOrEmpty(fileName) Then
            Exit Sub
        End If
        'ファイルの書き出し
        Try
            Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding(ENCODING)
            System.IO.File.WriteAllText(fileName, "", enc)
            MsgBox("createCommandEOF: " + fileName)
        Catch ex As Exception
            MsgBox(("MSG00176"))
        End Try
    End Sub

    Public Sub createCommandEOF(commandName As String)

        Dim fileName = makeFilename(commandName, ".eof")
        If String.IsNullOrEmpty(fileName) Then
            Exit Sub
        End If
        'ファイルの書き出し
        Try
            Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding(ENCODING)
            System.IO.File.WriteAllText(fileName, "", enc)
            MsgBox("createCommandEOF")
        Catch ex As Exception
            MsgBox(("MSG00176"))
        End Try
    End Sub

    Private Function GetCommandData(inData As String, Command As CommandIdentifier) As String
        Dim ret = String.Empty
        ' 識別子判定
        If Command.identifier = vbNullChar Then
            ' 識別子のない場合はそのまま読みだす
            Return inData.Substring(0, Command.size)
        End If
        If inData(0).Equals(Command.identifier) Then
            ' データ読み出し
            ret = inData.Substring(1, Command.size)
        Else
            Debug.WriteLine("識別子 ERROR : " + inData(0))
            Throw New Exception("識別子 ERROR : " + inData(0))
        End If
        Return IIf(Command.type = COMMAND_DATA_TYPE_STRING, ret.TrimEnd, ret)
    End Function
    Public Sub DeleteWatchFile(item As CommandWatchList)
        MsgBox("DeleteWatchFile")
        Try
            IO.File.Delete(item.command_path)
            IO.File.Delete(item.eof_path)
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
        End Try
    End Sub

    Private Function GetRackList(list As List(Of String), readNum As Integer, rackNum As Integer, reader As String) As String
        list.Clear()
        For i = 0 To readNum - 1
            ' ラックNo読み出し
            If reader(0).Equals(Chr(Asc(CMD_RACK_SEPARATOR_LIST(i)))) Then
                ' データ読み出し
                Dim readRackNo = reader.Substring(1, IDENTIFIER_RACKNO.size).TrimEnd
                If rackNum > i And String.IsNullOrEmpty(readRackNo) Then
                    Debug.WriteLine("READ is NULL : " + readRackNo)
                    Throw New Exception("READ is NULL : " + readRackNo)
                End If
                If readRackNo = "ERROR" Then
                    'by xulei 2018/11/2
                    'Debug.WriteLine("READ ERROR : " + readRackNo)
                    'Throw New Exception("READ ERROR : " + readRackNo)
                    Exit For
                    'by xulei 2018/11/2
                End If
                list.Add(reader.Substring(1, IDENTIFIER_RACKNO.size).TrimEnd)
                reader = reader.Substring(IDENTIFIER_RACKNO.size + 1)
            Else
                Debug.WriteLine("識別子 ERROR : " + reader(0))
                Throw New Exception("識別子 ERROR : " + reader(0))
            End If
        Next

        Return reader
    End Function

    Private Function CommandAnalizeLN(commandRead As String) As receiveCommandData
        'msgbox("CommandAnalizeLN")
        'msgbox(commandRead)

        Dim ret = New receiveCommandData
        ret.cmdAnalizeResult = CMD_ANALIZE_RESULT_ERROR

        Try
            Dim reader = commandRead
            ret.commandId = GetCommandData(reader, IDENTIFIER_COMMANDID)
            reader = reader.Substring(IDENTIFIER_COMMANDID.size)
            ' 装置ID読み出し
            ret.equipId = GetCommandData(reader, IDENTIFIER_EQUIPID)
            reader = reader.Substring(IDENTIFIER_EQUIPID.size + 1)
            ' 作業者No読み出し
            ret.operatorNo = GetCommandData(reader, IDENTIFIER_OPERATORNO)
            reader = reader.Substring(IDENTIFIER_OPERATORNO.size + 1)
            ' ラック数読み出し
            ret.LrackNum = GetCommandData(reader, IDENTIFIER_RACKNUM)
            reader = reader.Substring(IDENTIFIER_RACKNUM.size + 1)
            ' ラックNo読み出し
            reader = GetRackList(ret.LrackNoList, IDENTIFIER_RACKNUM_MAX, ret.LrackNum, reader)
            ret.cmdAnalizeResult = CMD_ANALIZE_RESULT_SUCCESS
            Return ret
        Catch ex As Exception
            MsgBox(("MSG00169"))
            Return ret
        End Try
    End Function

    Private Function CommandAnalizeLH(commandRead As String) As receiveCommandData
        'msgbox("CommandAnalizeLH")
        'msgbox(commandRead)
        Dim ret = New receiveCommandData
        ret.cmdAnalizeResult = CMD_ANALIZE_RESULT_ERROR

        Try
            Dim reader = commandRead
            ret.commandId = GetCommandData(reader, IDENTIFIER_COMMANDID)
            reader = reader.Substring(IDENTIFIER_COMMANDID.size)
            ' 装置ID読み出し
            ret.equipId = GetCommandData(reader, IDENTIFIER_EQUIPID)
            reader = reader.Substring(IDENTIFIER_EQUIPID.size + 1)
            ' 作業者No読み出し
            ret.operatorNo = GetCommandData(reader, IDENTIFIER_OPERATORNO)
            reader = reader.Substring(IDENTIFIER_OPERATORNO.size + 1)
            ' 良品数読み出し
            ret.compNum = GetCommandData(reader, IDENTIFIER_COMPNUM)
            reader = reader.Substring(IDENTIFIER_COMPNUM.size + 1)
            ' 不良品数読み出し
            ret.badNum = GetCommandData(reader, IDENTIFIER_BADNUM)
            reader = reader.Substring(IDENTIFIER_BADNUM.size + 1)
            ' 全数読み出し
            ret.allNum = GetCommandData(reader, IDENTIFIER_ALLNUM)
            reader = reader.Substring(IDENTIFIER_ALLNUM.size + 1)
            ' フレーム数読み出し
            ret.frameNum = GetCommandData(reader, IDENTIFIER_FRAMENUM)
            reader = reader.Substring(IDENTIFIER_FRAMENUM.size + 1)
            ' ラック数読み出し
            ret.LrackNum = GetCommandData(reader, IDENTIFIER_RACKNUM)
            reader = reader.Substring(IDENTIFIER_RACKNUM.size + 1)
            ' LラックNo読み出し
            reader = GetRackList(ret.LrackNoList, IDENTIFIER_RACKNUM_MAX, ret.LrackNum, reader)
            ' ラック数読み出し
            ret.UrackNum = GetCommandData(reader, IDENTIFIER_RACKNUM)
            reader = reader.Substring(IDENTIFIER_RACKNUM.size + 1)
            ' UラックNo読み出し
            reader = GetRackList(ret.UrackNoList, IDENTIFIER_RACKNUM_MAX, ret.UrackNum, reader)
            ret.cmdAnalizeResult = CMD_ANALIZE_RESULT_SUCCESS
            Return ret
        Catch ex As Exception
            MsgBox(("MSG00169"))
            Return ret
        End Try
    End Function

    Private Function CommandAnalizeLD(commandRead As String) As receiveCommandData

        Dim ret = New receiveCommandData
        ret.cmdAnalizeResult = CMD_ANALIZE_RESULT_ERROR

        Try
            Dim reader = commandRead
            ret.commandId = GetCommandData(reader, IDENTIFIER_COMMANDID)
            reader = reader.Substring(IDENTIFIER_COMMANDID.size)
            ' 装置ID読み出し
            ret.equipId = GetCommandData(reader, IDENTIFIER_EQUIPID)
            reader = reader.Substring(IDENTIFIER_EQUIPID.size + 1)
            ' 結果読み出し
            ret.result = GetCommandData(reader, IDENTIFIER_RESULT)
            reader = reader.Substring(IDENTIFIER_RESULT.size + 1)

            ret.cmdAnalizeResult = CMD_ANALIZE_RESULT_SUCCESS

            Return ret
        Catch ex As Exception
            MsgBox(("MSG00169"))
            Return ret
        End Try
    End Function

    Private Function CommandAnalizeLG(commandRead As String) As receiveCommandData

        Dim ret = New receiveCommandData
        ret.cmdAnalizeResult = CMD_ANALIZE_RESULT_ERROR

        Try
            Dim reader = commandRead
            ret.commandId = GetCommandData(reader, IDENTIFIER_COMMANDID)
            reader = reader.Substring(IDENTIFIER_COMMANDID.size)
            ' 装置ID読み出し
            ret.equipId = GetCommandData(reader, IDENTIFIER_EQUIPID)
            reader = reader.Substring(IDENTIFIER_EQUIPID.size + 1)
            ' 良品数読み出し
            ret.compNum = GetCommandData(reader, IDENTIFIER_COMPNUM)
            reader = reader.Substring(IDENTIFIER_COMPNUM.size + 1)
            ' 不良品数読み出し
            ret.badNum = GetCommandData(reader, IDENTIFIER_BADNUM)
            reader = reader.Substring(IDENTIFIER_BADNUM.size + 1)
            ' 全数読み出し
            ret.allNum = GetCommandData(reader, IDENTIFIER_ALLNUM)
            reader = reader.Substring(IDENTIFIER_ALLNUM.size + 1)
            ' フレーム数読み出し
            ret.frameNum = GetCommandData(reader, IDENTIFIER_FRAMENUM)
            reader = reader.Substring(IDENTIFIER_FRAMENUM.size + 1)

            ret.cmdAnalizeResult = CMD_ANALIZE_RESULT_SUCCESS

            Return ret
        Catch ex As Exception
            MsgBox(("MSG00169"))
            Return ret
        End Try
    End Function

    Private Function CommandAnalizeTW(commandRead As String) As receiveCommandData

        Dim ret = New receiveCommandData
        ret.cmdAnalizeResult = CMD_ANALIZE_RESULT_ERROR

        Try
            Dim reader = commandRead
            ret.commandId = GetCommandData(reader, IDENTIFIER_COMMANDID)
            reader = reader.Substring(IDENTIFIER_COMMANDID.size)
            MsgBox("TW1：" & ret.commandId)

            ' 装置ID読み出し
            ret.equipId = GetCommandData(reader, IDENTIFIER_EQUIPID)
            reader = reader.Substring(IDENTIFIER_EQUIPID.size + 1)
            MsgBox("TW2：" & ret.equipId)
            ' 結果読み出し
            ret.result = GetCommandData(reader, IDENTIFIER_RESULT)
            reader = reader.Substring(IDENTIFIER_RESULT.size + 1)
            MsgBox("TW3：" & ret.result)
            ret.cmdAnalizeResult = CMD_ANALIZE_RESULT_SUCCESS
            Return ret
        Catch ex As Exception
            MsgBox(("MSG00169"))
            Return ret
        End Try
    End Function

    Private Function CommandAnalizeMA(commandRead As String) As receiveCommandData
        Dim ret = New receiveCommandData
        ret.cmdAnalizeResult = CMD_ANALIZE_RESULT_ERROR

        Try
            Dim reader = commandRead
            ret.commandId = GetCommandData(reader, IDENTIFIER_COMMANDID)
            reader = reader.Substring(IDENTIFIER_COMMANDID.size)
            ' 装置ID読み出し
            ret.equipId = GetCommandData(reader, IDENTIFIER_EQUIPID)
            reader = reader.Substring(IDENTIFIER_EQUIPID.size + 1)
            '' 結果読み出し
            'ret.result = GetCommandData(reader, IDENTIFIER_RESULT)
            'reader = reader.Substring(IDENTIFIER_RESULT.size + 1)

            ret.cmdAnalizeResult = CMD_ANALIZE_RESULT_SUCCESS

            Return ret
        Catch ex As Exception
            MsgBox(("MSG00169"))
            Return ret
        End Try
    End Function

    Private Function CommandAnalizeRL(commandRead As String) As receiveCommandData

        Dim ret = New receiveCommandData
        ret.cmdAnalizeResult = CMD_ANALIZE_RESULT_ERROR

        Try
            Dim reader = commandRead
            ret.commandId = GetCommandData(reader, IDENTIFIER_COMMANDID)
            reader = reader.Substring(IDENTIFIER_COMMANDID.size)
            ' 装置ID読み出し
            ret.equipId = GetCommandData(reader, IDENTIFIER_EQUIPID)
            reader = reader.Substring(IDENTIFIER_EQUIPID.size + 1)

            ret.cmdAnalizeResult = CMD_ANALIZE_RESULT_SUCCESS

            Return ret
        Catch ex As Exception
            MsgBox(("MSG00169"))
            Return ret
        End Try
    End Function


    Private Function CommandAnalizeEZ(commandRead As String) As receiveCommandData

        Dim ret = New receiveCommandData
        ret.cmdAnalizeResult = CMD_ANALIZE_RESULT_ERROR

        Try
            Dim reader = commandRead
            ret.commandId = GetCommandData(reader, IDENTIFIER_COMMANDID)
            reader = reader.Substring(IDENTIFIER_COMMANDID.size)
            ' 装置ID読み出し
            ret.equipId = GetCommandData(reader, IDENTIFIER_EQUIPID)
            reader = reader.Substring(IDENTIFIER_EQUIPID.size + 1)
            ' 作業者No読み出し
            ret.operatorNo = GetCommandData(reader, IDENTIFIER_OPERATORNO)
            reader = reader.Substring(IDENTIFIER_OPERATORNO.size + 1)
            ' 良品数読み出し
            ret.compNum = GetCommandData(reader, IDENTIFIER_COMPNUM)
            reader = reader.Substring(IDENTIFIER_COMPNUM.size + 1)
            ' 不良品数読み出し
            ret.badNum = GetCommandData(reader, IDENTIFIER_BADNUM)
            reader = reader.Substring(IDENTIFIER_BADNUM.size + 1)
            ' 全数読み出し
            ret.allNum = GetCommandData(reader, IDENTIFIER_ALLNUM)
            reader = reader.Substring(IDENTIFIER_ALLNUM.size + 1)
            ' フレーム数読み出し
            ret.frameNum = GetCommandData(reader, IDENTIFIER_FRAMENUM)
            reader = reader.Substring(IDENTIFIER_FRAMENUM.size + 1)

            ret.cmdAnalizeResult = CMD_ANALIZE_RESULT_SUCCESS

            Return ret
        Catch ex As Exception
            MsgBox(("MSG00169"))
            Return ret
        End Try
    End Function

    Public Function CommandAnalize(command As String, filepath As String) As receiveCommandData
        MsgBox(String.Format("CommandAnalize in. command:{0} filepath:{1}", command, filepath))
        Debug.WriteLine(String.Format("CommandAnalize in. command:{0} filepath:{1}", command, filepath))
        Try
            Dim readData = String.Empty
            Using sr As New System.IO.StreamReader(filepath)
                readData = sr.ReadToEnd()
            End Using

            Debug.WriteLine("read start --> ")
            Debug.WriteLine(readData)
            Debug.WriteLine("<-- read end")

            ' 受信コマンド解析
            Dim Data = CommandAnalize(readData)

            ' 受信コマンド解析に失敗した場合
            If Data IsNot Nothing AndAlso Data.cmdAnalizeResult = CMD_ANALIZE_RESULT_ERROR Then
                ' コマンドID読み出し(2byte)
                Dim commandId = Data.commandId
                ' 読み出したコマンドが一致しているので正常と判断
                Select Case commandId
                    Case COMMAND_LN
                        createResponseCommand(COMMAND_IT, RESPONSE_COMMAND_NG)
                    Case COMMAND_LH
                        createResponseCommand(COMMAND_IU, RESPONSE_COMMAND_NG)
                    Case COMMAND_LD
                        createResponseCommand(COMMAND_IT, RESPONSE_COMMAND_NG)
                    Case COMMAND_LG
                        createResponseCommand(COMMAND_IU, RESPONSE_COMMAND_NG)
                    Case Else
                End Select
                Return Nothing
            End If

            Return Data

        Catch ex As Exception
            MsgBox(("MSG00170"))
        End Try
        Return Nothing
    End Function

    ''' <summary>
    ''' 受信データ文字列の解析処理
    ''' </summary>
    ''' <param name="receiveData">受信データ文字列</param>
    ''' <returns></returns>
    Public Function CommandAnalize(receiveData As String) As receiveCommandData
        'msgbox(String.Format("CommandAnalize in. receiveData:{0}", receiveData))
        Debug.WriteLine("CommandAnalize Data : " + receiveData)
        Try
            Dim readData = receiveData

            ' コマンドID読み出し(2byte)
            Dim commandId = GetCommandData(readData, IDENTIFIER_COMMANDID)
            ' 読み出したコマンドが一致しているので正常と判断
            Select Case commandId
                Case COMMAND_LN
                    Return CommandAnalizeLN(readData)
                Case COMMAND_LH
                    Return CommandAnalizeLH(readData)
                Case COMMAND_LD
                    Return CommandAnalizeLD(readData)
                Case COMMAND_LG
                    Return CommandAnalizeLG(readData)
                Case COMMAND_TW
                    Return CommandAnalizeTW(readData)
                Case COMMAND_MA
                    Return CommandAnalizeMA(readData)
                Case COMMAND_RL
                    Return CommandAnalizeRL(readData)
                Case COMMAND_EZ
                    Return CommandAnalizeEZ(readData)
                Case Else
                    Return Nothing
            End Select
        Catch ex As Exception
            MsgBox(("MSG00170"))
        End Try
        Return Nothing
    End Function

    Public Sub viewDebugInfo(receiveData As receiveCommandData)
        Debug.WriteLine(String.Format("CommandReceive in --"))
        Debug.WriteLine(String.Format(" commandId : {0} ", receiveData.commandId))
        Debug.WriteLine(String.Format(" equipId : {0} ", receiveData.equipId))
        Debug.WriteLine(String.Format(" operatorNo : {0} ", receiveData.operatorNo))
        Debug.WriteLine(String.Format(" LrackNum : {0} ", receiveData.LrackNum))
        For Each item In receiveData.LrackNoList
            Debug.WriteLine(String.Format("  LrackNo : {0} ", item))
        Next
        Debug.WriteLine(String.Format(" UrackNum : {0} ", receiveData.UrackNum))
        For Each item In receiveData.UrackNoList
            Debug.WriteLine(String.Format("  UrackNo : {0} ", item))
        Next
        Debug.WriteLine(String.Format(" compNum : {0} ", receiveData.compNum))
        Debug.WriteLine(String.Format(" badNum : {0} ", receiveData.badNum))
        Debug.WriteLine(String.Format(" allNum : {0} ", receiveData.allNum))
        Debug.WriteLine(String.Format(" frameNum : {0} ", receiveData.frameNum))
        Debug.WriteLine(String.Format(" result : {0} ", receiveData.result))
    End Sub

    ''' <summary>
    ''' コマンドIDを取得する
    ''' </summary>
    ''' <param name="receiveData">受信データ</param>
    ''' <returns>コマンドID</returns>
    Public Function getCommandId(receiveData As String) As String
        Return GetCommandData(receiveData, IDENTIFIER_COMMANDID)
    End Function
    ''' <summary>
    ''' 装置IDを取得する
    ''' </summary>
    ''' <param name="receiveData">受信データ</param>
    ''' <returns>装置ID</returns>
    Public Function getEquipId(receiveData As String) As String
        Dim reader = receiveData
        reader = reader.Substring(IDENTIFIER_COMMANDID.size)
        Return GetCommandData(reader, IDENTIFIER_EQUIPID)
    End Function

End Module

Public Class receiveCommandData
    ''' <summary>
    ''' コマンドID
    ''' </summary>
    Public commandId As String
    ''' <summary>
    ''' 装置ID
    ''' </summary>
    Public equipId As String
    ''' <summary>
    ''' 作業者No
    ''' </summary>
    Public operatorNo As String
    ''' <summary>
    ''' ラック数
    ''' </summary>
    Public LrackNum As Integer
    Public UrackNum As Integer
    ''' <summary>
    ''' ラックNo
    ''' </summary>
    Public LrackNoList As List(Of String) = New List(Of String)
    Public UrackNoList As List(Of String) = New List(Of String)
    ''' <summary>
    ''' 良品数
    ''' </summary>
    Public compNum As Integer
    ''' <summary>
    ''' 不良品数
    ''' </summary>
    Public badNum As Integer
    ''' <summary>
    ''' 総数
    ''' </summary>
    Public allNum As Integer
    ''' <summary>
    ''' フレーム数
    ''' </summary>
    Public frameNum As Integer
    ''' <summary>
    ''' 結果
    ''' </summary>
    Public result As Integer
    ''' <summary>
    ''' コマンド解析結果
    ''' </summary>
    Public cmdAnalizeResult As Integer
End Class
