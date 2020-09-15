Option Strict On
Option Explicit On
'Imports System.Text
'Imports System.Runtime.InteropServices
Module modWinApi

    'アセンブリ名
    Public ReadOnly AssemblyName As String = My.Application.Info.AssemblyName
    'フルパス
    Private ReadOnly FullPath As String = Application.StartupPath & "\" & AssemblyName
    '
    '   Win32 API 参照宣言
    'Public Const STANDARD_RIGHTS_REQUIRED As Integer = &HF0000
    'Public Const SECTION_QUERY As Integer = &H1
    'Public Const SECTION_MAP_WRITE As Integer = &H2
    'Public Const SECTION_MAP_READ As Integer = &H4
    'Public Const SECTION_MAP_EXECUTE As Integer = &H8
    'Public Const SECTION_EXTEND_SIZE As Integer = &H10
    '   Public Const SECTION_ALL_ACCESS As Integer = STANDARD_RIGHTS_REQUIRED Or SECTION_QUERY Or SECTION_MAP_WRITE Or SECTION_MAP_READ Or SECTION_MAP_EXECUTE Or SECTION_EXTEND_SIZE
    '   Public Const FILE_MAP_WRITE As Integer = SECTION_MAP_WRITE
    'Public Const FILE_MAP_READ As Integer = SECTION_MAP_READ
    '   Public Const FILE_MAP_ALL_ACCESS As Integer = SECTION_ALL_ACCESS


    '   Public Declare Function OpenFileMapping Lib "kernel32" Alias "OpenFileMappingA" (ByVal dwDesiredAccess As Integer, ByVal bInheritHandle As Boolean, ByVal lpName As String) As Integer
    'Public Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Integer) As Integer
    'Public Declare Function MapViewOfFile Lib "kernel32" (ByVal hFileMappingObject As Integer, ByVal dwDesiredAccess As Integer, ByVal dwFileOffsetHigh As Integer, ByVal dwFileOffsetLow As Integer, ByVal dwNumberOfBytesToMap As Integer) As Integer
    '   Public Declare Function UnmapViewOfFile Lib "kernel32" (ByVal lpBaseAddress As Integer) As Integer
    '   Public Declare Sub OutputDebugString Lib "kernel32" Alias "OutputDebugStringA" (ByVal lpOutputString As String)

    'Public Declare Function GetDeviceCaps Lib "gdi32" (ByVal hdc As Integer, ByVal nIndex As Integer) As Integer

    '
    ' MPC.DLL 参照宣言
    '
    'Public Declare Function GetByteMem Lib "mpc" (ByVal lAddress As Integer) As Byte
    'Public Declare Function SetByteMem Lib "mpc" (ByVal lAddress As Integer, ByVal bData As Byte) As Byte
    '   Public Declare Function CopyMemoryStoV Lib "mpc" (ByRef lDesAdrs As Integer, ByVal lSrcAdrs As Integer, ByVal lCopyByte As Integer) As Integer
    '   Public Declare Function CopyMemoryVtoS Lib "mpc" (ByVal lDesAdrs As Integer, ByRef lSrcAdrs As Integer, ByVal lCopyByte As Integer) As Integer


    'Public Declare Function GetTickCount Lib "kernel32" () As Integer

    'キー入力チェック
    Public Declare Function GetKeyState Lib "user32" (ByVal nVirtKey As Integer) As Short

    '   'iniファイルを使用する為のAPI
    '   Public Const BUFF_LEN As Integer = 256 '256文字
    '   ' ANSI版[LPSTRのみに対応] GetPrivateProfileString
    '   Public Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" ( _
    '                                ByVal lpApplicationName As String, _
    '                                ByVal lpKeyName As String, _
    '                                ByVal lpDefault As String, _
    '                                ByVal lpReturnedString As String, _
    '                                ByVal nSize As UInt32, _
    '                                ByVal lpFileName As String) As UInt32

    '   ' ANSI版[LPSTRのみに対応] WritePrivateProfileString
    '   Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" ( _
    '                                ByVal lpAppName As String,
    '                                ByVal lpKeyName As String,
    '                                ByVal lpString As String, _
    '                                ByVal lpFileName As String) As Boolean

    ''画面操作
    '   'Public Declare Function CloseWindow Lib "user32" (ByVal hwnd As Integer) As Integer
    '   'Public Declare Function OpenIcon Lib "user32" (ByVal hwnd As Integer) As Integer
    '   'Public Declare Function IsIconic Lib "user32" (ByVal hwnd As Integer) As Integer

    '   'Public Declare Sub Sleep Lib "kernel32" (ByVal dwMilliseconds As Integer)


    '' 機能      : 文字列情報取得処理
    '' 返り値    : Long
    '' 引き数    : szSection  - セクション
    ''             szKey      - キー
    '   '             szDefault  - デフォルト値

    ''             szString   - 文字列バッファ
    ''             szFileName - ファイル名
    '' 機能説明  : 初期化ファイルから文字列情報を取得する。
    '' 備考      : 特になし。
    ''
    '   Public Function GetPrivateProfileString(ByRef szSection As String, ByRef szKey As String, ByRef szDefault As String, ByRef szString As String, ByRef szFileName As String) As Integer

    '       Const LEN_BUFF As Short = 255
    '       Dim szBuff As String

    '       'バッファをクリアする
    '       szBuff = ""
    '       '情報を取得する
    '       GetPrivateProfileString = GetPrivateProfileString(szSection, szKey, szDefault, LEN_BUFF, szFileName)
    '       szString = Left(szBuff, InStr(szBuff, Chr(0)) - 1)

    '   End Function

    Declare Function WritePrivateProfileString Lib "KERNEL32.DLL" Alias "WritePrivateProfileStringA" ( _
                                                        ByVal lpAppName As String, _
                                                        ByVal lpKeyName As String, _
                                                        ByVal lpString As String, _
                                                        ByVal lpFileName As String) As Integer
    Declare Function GetPrivateProfileString Lib "KERNEL32.DLL" Alias "GetPrivateProfileStringA" ( _
                                                        ByVal lpAppName As String, _
                                                        ByVal lpKeyName As String, _
                                                        ByVal lpDefault As String, _
                                                        ByVal lpReturnedString As String, _
                                                        ByVal nSize As Integer, _
                                                        ByVal lpFileName As String) As Integer

    Public Function GetIniFile(ByVal ApName As String, ByVal KeyName As String, ByVal Defaults As String, ByVal Filename As String) As String

        'INIファイルから参照したいキーの値を取得する     
        'ApName   : セクション名  
        'KeyName  : 項目名  
        'Default  : 項目が存在しない場合の初期値      
        'FileName : 参照ファイル名    
        '**************************************** 
        Dim strResult As String = Space(255)

        Call GetPrivateProfileString(ApName, KeyName, Defaults, strResult, Len(strResult), Filename)

        GetIniFile = Microsoft.VisualBasic.Left(strResult, InStr(strResult, Chr(0)) - 1)

    End Function

    Public Sub PutIniFile(ByVal ApName As String, ByVal KeyName As String, ByVal Param As String, ByVal Filename As String)

        'INIファイルに新たなキーの値を書込む       
        '   ※既存のキーがあれば更新・なければ新規作成する     
        'ApName   : セクション名     
        'KeyName  : 項目名     
        'Param    : 更新する値      
        'FileName : 書出ファイル名    
        '****************************************    
        Call WritePrivateProfileString(ApName, KeyName, Param, Filename)

    End Sub

    Public Function GetFullPath() As String
        Return FullPath
    End Function


End Module
