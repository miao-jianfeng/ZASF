Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Media

Friend Class ShowSelectForm


#Region " Constructors "

    Public Sub New()
        InitializeComponent()
        Me.Text = ""
    End Sub

#End Region

#Region " Event handlers "

    Private Sub ShowMsgForm_FormClosing(ByVal sender As Object, _
    ByVal e As System.Windows.Forms.FormClosingEventArgs) _
    Handles Me.FormClosing
        'If the user clicks on the Close Form button in the upper right,
        'it will have the same effect as clicking the accept button
        If e.CloseReason = CloseReason.UserClosing _
        AndAlso Me.AcceptButton IsNot Nothing Then
            Me.DialogResult = CType(Me.AcceptButton, Button).DialogResult
        End If
    End Sub

    Private Sub ShowMsgForm_Load(ByVal sender As Object, _
            ByVal e As System.EventArgs) _
            Handles Me.Load
        'Give the focus to the accept button, providing a 
        'visual clue as to which button it is.
        Dim Btn As Button = TryCast(Me.AcceptButton, Button)

        If Btn IsNot Nothing AndAlso Btn.Visible Then
            Btn.Select()
        Else
            Me.Button1.Select()
        End If
    End Sub

#End Region

End Class