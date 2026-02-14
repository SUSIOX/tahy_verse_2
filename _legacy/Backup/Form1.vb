Public Class Form1
    Inherits System.Windows.Forms.Form


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    Public Sub DrawLineFloat(ByVal e As PaintEventArgs)
        ' Create pen.
        Dim blackPen As New Pen(Color.Black, 3)
        ' Create coordinates of points that define line.
        Dim x1 As Single = 100.0F
        Dim y1 As Single = 100.0F
        Dim x2 As Single = 500.0F
        Dim y2 As Single = 100.0F
        ' Draw line to screen.
        e.Graphics.DrawLine(blackPen, x1, y1, x2, y2)
    End Sub

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents btnDrawline As System.Windows.Forms.Button
    Friend WithEvents txtCenterx As System.Windows.Forms.TextBox
    Friend WithEvents txtCentery As System.Windows.Forms.TextBox
    Friend WithEvents txtEndx As System.Windows.Forms.TextBox
    Friend WithEvents txtEndy As System.Windows.Forms.TextBox
    Friend WithEvents pbCircle As System.Windows.Forms.PictureBox
    Friend WithEvents txtMeasure As System.Windows.Forms.TextBox
    Friend WithEvents radRadian As System.Windows.Forms.RadioButton
    Friend WithEvents radDegree As System.Windows.Forms.RadioButton
    Friend WithEvents Timer1 As System.Timers.Timer
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Form1))
        Me.btnDrawline = New System.Windows.Forms.Button()
        Me.txtCenterx = New System.Windows.Forms.TextBox()
        Me.txtCentery = New System.Windows.Forms.TextBox()
        Me.txtEndx = New System.Windows.Forms.TextBox()
        Me.txtEndy = New System.Windows.Forms.TextBox()
        Me.txtMeasure = New System.Windows.Forms.TextBox()
        Me.pbCircle = New System.Windows.Forms.PictureBox()
        Me.radRadian = New System.Windows.Forms.RadioButton()
        Me.radDegree = New System.Windows.Forms.RadioButton()
        Me.Timer1 = New System.Timers.Timer()
        CType(Me.Timer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnDrawline
        '
        Me.btnDrawline.Location = New System.Drawing.Point(97, 184)
        Me.btnDrawline.Name = "btnDrawline"
        Me.btnDrawline.Size = New System.Drawing.Size(64, 24)
        Me.btnDrawline.TabIndex = 0
        Me.btnDrawline.Text = "Graph"
        '
        'txtCenterx
        '
        Me.txtCenterx.Location = New System.Drawing.Point(192, 8)
        Me.txtCenterx.Name = "txtCenterx"
        Me.txtCenterx.Size = New System.Drawing.Size(64, 20)
        Me.txtCenterx.TabIndex = 4
        Me.txtCenterx.Text = "89"
        '
        'txtCentery
        '
        Me.txtCentery.Location = New System.Drawing.Point(272, 8)
        Me.txtCentery.Name = "txtCentery"
        Me.txtCentery.Size = New System.Drawing.Size(64, 20)
        Me.txtCentery.TabIndex = 5
        Me.txtCentery.Text = "88"
        '
        'txtEndx
        '
        Me.txtEndx.Location = New System.Drawing.Point(192, 32)
        Me.txtEndx.Name = "txtEndx"
        Me.txtEndx.Size = New System.Drawing.Size(64, 20)
        Me.txtEndx.TabIndex = 7
        Me.txtEndx.Text = "159"
        '
        'txtEndy
        '
        Me.txtEndy.Location = New System.Drawing.Point(272, 32)
        Me.txtEndy.Name = "txtEndy"
        Me.txtEndy.Size = New System.Drawing.Size(64, 20)
        Me.txtEndy.TabIndex = 6
        Me.txtEndy.Text = "88"
        '
        'txtMeasure
        '
        Me.txtMeasure.Location = New System.Drawing.Point(17, 184)
        Me.txtMeasure.Name = "txtMeasure"
        Me.txtMeasure.Size = New System.Drawing.Size(64, 20)
        Me.txtMeasure.TabIndex = 10
        Me.txtMeasure.Text = "0"
        '
        'pbCircle
        '
        Me.pbCircle.BackColor = System.Drawing.Color.White
        Me.pbCircle.Image = CType(resources.GetObject("pbCircle.Image"), System.Drawing.Bitmap)
        Me.pbCircle.Name = "pbCircle"
        Me.pbCircle.Size = New System.Drawing.Size(178, 176)
        Me.pbCircle.TabIndex = 22
        Me.pbCircle.TabStop = False
        '
        'radRadian
        '
        Me.radRadian.Checked = True
        Me.radRadian.Location = New System.Drawing.Point(18, 216)
        Me.radRadian.Name = "radRadian"
        Me.radRadian.Size = New System.Drawing.Size(64, 16)
        Me.radRadian.TabIndex = 23
        Me.radRadian.TabStop = True
        Me.radRadian.Text = "Radian"
        '
        'radDegree
        '
        Me.radDegree.Location = New System.Drawing.Point(98, 216)
        Me.radDegree.Name = "radDegree"
        Me.radDegree.Size = New System.Drawing.Size(63, 16)
        Me.radDegree.TabIndex = 24
        Me.radDegree.Text = "Degree"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.SynchronizingObject = Me
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(178, 240)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.radDegree, Me.radRadian, Me.pbCircle, Me.txtMeasure, Me.txtEndx, Me.txtEndy, Me.txtCentery, Me.txtCenterx, Me.btnDrawline})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Angle"
        CType(Me.Timer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDrawline.Click
        Dim myPen As New Pen(Color.Black), cenX As Integer, cenY As Integer, endX As Integer, endY As Integer
        Dim myPen2 As New Pen(Color.Black)
        pbCircle.Refresh()
        If radRadian.Checked = True Then
            txtEndx.Text = (159 - (70 - ((Math.Sin(Math.PI / 2 - txtMeasure.Text)) * 70)))
            txtEndy.Text = (88 - (Math.Sin(txtMeasure.Text)) * 70)
        End If
        If radDegree.Checked = True Then
            txtEndx.Text = (159 - (70 - ((Math.Sin(Math.PI / 2 - (txtMeasure.Text * (Math.PI / 180))) * 70))))
            txtEndy.Text = (88 - (Math.Sin((txtMeasure.Text * (Math.PI / 180)))) * 70)
        End If

        cenX = txtCenterx.Text
        cenY = txtCentery.Text
        endX = txtEndx.Text
        endY = txtEndy.Text
        myPen.Width = 2
        myPen2.Width = 3
        pbCircle.CreateGraphics.DrawLine(myPen, cenX, cenY, endX, endY)
        pbCircle.CreateGraphics.DrawLine(myPen2, cenX, cenY, 159, 88)
    End Sub

    Private Sub pbCircle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbCircle.Click

    End Sub

    Private Sub Timer1_Elapsed(ByVal sender As System.Object, ByVal e As System.Timers.ElapsedEventArgs) Handles Timer1.Elapsed
        Dim myPen2 As New Pen(Color.Black), cenX As Integer, cenY As Integer, endX As Integer, endY As Integer
        pbCircle.Refresh()
        cenX = txtCenterx.Text
        cenY = txtCentery.Text
        myPen2.Width = 3
        pbCircle.CreateGraphics.DrawLine(myPen2, cenX, cenY, 159, 88)
        Timer1.Enabled = False
    End Sub
End Class
