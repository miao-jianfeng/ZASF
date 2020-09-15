Public Class LHJ
    Public Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。

    End Sub

    Private Sub LHJ_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private _equipmentNo As String
    Private _Temperature As String
    Private _Status As Boolean
    Private _networkStatus As Boolean
    Private _CutOffTime As String


#Region "Property"

    Public Property Temperature As String
        Get
            Return _Temperature
        End Get
        Set(value As String)
            _Temperature = value
            btnTemperature.Text = value
        End Set
    End Property

    Public Property EquipmentNo As String
        Get
            Return _equipmentNo
        End Get
        Set(value As String)
            _equipmentNo = value
            gpNo.Text = value
        End Set
    End Property

    Public Property Status As Boolean
        Get
            Return _Status
        End Get
        Set(value As Boolean)
            _Status = value
            setControlColor()
        End Set
    End Property

    Public Property NetworkStatus As Boolean
        Get
            Return _networkStatus
        End Get
        Set(value As Boolean)
            _networkStatus = value
            If value = True Then
                gpNo.CustomHeaderButtons.Item(0).Properties.Visible = True
                gpNo.CustomHeaderButtons.Item(1).Properties.Visible = False
                btnMPa.ForeColor = Color.White
                btnTemperature.ForeColor = Color.White
            Else
                gpNo.CustomHeaderButtons.Item(0).Properties.Visible = False
                gpNo.CustomHeaderButtons.Item(1).Properties.Visible = True
                btnMPa.Text = String.Empty
                btnTemperature.Text = String.Empty
                lblCutOffTime.Text = String.Empty
                btnMPa.BackColor = Color.Silver
                btnTemperature.BackColor = Color.Silver
            End If


        End Set
    End Property

    Public Property CutOffTime As String
        Get
            Return _CutOffTime
        End Get
        Set(value As String)
            _CutOffTime = value
            lblCutOffTime.Text = value
        End Set
    End Property

#End Region

    Private Sub setControlColor()

        If _Status = True Then
            btnMPa.ForeColor = Color.White
            btnTemperature.ForeColor = Color.White
            btnMPa.BackColor = Color.DeepSkyBlue
            btnTemperature.BackColor = Color.DeepSkyBlue
        Else
            btnMPa.ForeColor = Color.Red
            btnTemperature.ForeColor = Color.Red
            btnMPa.BackColor = Color.Yellow
            btnTemperature.BackColor = Color.Yellow
        End If


    End Sub


End Class
