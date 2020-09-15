Public Class TSL
    Private _EquipNo As String
    Private _CutOffTime As String
    Private _Status As Integer

#Region "Property"

    Public Property EquipNo As String
        Get
            Return _EquipNo
        End Get
        Set(value As String)
            _EquipNo = value
            lblEquipNo.Text = value & "号机"
        End Set
    End Property

    Public Property CutOffTime As String
        Get
            Return _CutOffTime
        End Get
        Set(value As String)
            _CutOffTime = value
            lblTime.Text = value
        End Set
    End Property

    Public Property Status As Integer
        Get
            Return _Status
        End Get
        Set(value As Integer)
            _Status = value
            Select Case value
                Case 0
                    Me.BackColor = Color.Red
                Case 1
                    Me.BackColor = Color.Yellow
                Case 2
                    Me.BackColor = Color.White
            End Select
        End Set
    End Property

#End Region


End Class
