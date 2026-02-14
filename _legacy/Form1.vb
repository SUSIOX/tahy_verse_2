Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
'Janík Jiří 2020
'zobrazení - grafika
'pictureboxy:GraphicsFun do pbTah1-18
'            pb1 pro základní obrázek řezu
'            pBox do bitmapy  g2  do pb1
'            PictureBox1 obrázek k uložení a tisku
'            pbSiluet   postava


Public Class Form1

    Inherits System.Windows.Forms.Form
    '..............................
    Public bit As Bitmap  'k uložení a tisku
    Public GraphicsFun As System.Drawing.Graphics 'grafika na tah překrývání tahy
    ' Public GraphicsCar As System.Drawing.Graphics 'grafika na tah překrývání čárami
    Public pbTah As Object
    '.................. vloží již dané hodnoty při změně tahu v comboboxu
    Public vDek1 As String
    Public vDek2 As String
    Public vDek3 As String
    Public vDek5 As String
    Public vDek6 As String
    Public vDek7 As String
    Public vDek8 As String
    Public vDek9 As String
    Public vDek10 As String
    Public vDek12 As String
    Public vDek13 As String
    Public vDek14 As String
    Public vDek15 As String
    Public vDek17 As String
    Public vDek18 As String
    '..............
    Public vUva1 As String
    Public vUva2 As String
    Public vUva3 As String
    Public vUva5 As String
    Public vUva6 As String
    Public vUva7 As String
    Public vUva8 As String
    Public vUva9 As String
    Public vUva10 As String
    Public vUva12 As String
    Public vUva13 As String
    Public vUva14 As String
    Public vUva15 As String
    Public vUva17 As String
    Public vUva18 As String
    '..............
    Public vPod1 As String
    Public vPod2 As String
    Public vPod3 As String
    Public vPod5 As String
    Public vPod6 As String
    Public vPod7 As String
    Public vPod8 As String
    Public vPod9 As String
    Public vPod10 As String
    Public vPod12 As String
    Public vPod13 As String
    Public vPod14 As String
    Public vPod15 As String
    Public vPod17 As String
    Public vPod18 As String
    '..............
    Public x1 As Integer
    Public y1 As Integer
    Public x2 As Integer
    Public y2 As Integer
    Public x1u As Integer
    Public y1u As Integer
    Public x2u As Integer
    Public y2u As Integer
    '...........................................
    Public x1b As Integer 'souřadnice čar balkona a 1.řada
    Public y1b As Integer
    Public x2b As Integer
    Public y2b As Integer
    Public x1o As Integer
    Public y1o As Integer
    Public x2o As Integer
    Public y2o As Integer
    '..... souřadnice myši siluety
    Dim nX As Integer
    Dim nY As Integer

    '...........................................
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents ListBox7 As ListBox
    '..........................................
    Public myPenUvaz As New System.Drawing.Pen(System.Drawing.Color.Red, 1)
    Public myPenTah As New System.Drawing.Pen(System.Drawing.Color.Blue, 2)
    Public myPenTyc As New System.Drawing.Pen(System.Drawing.Color.Black, 1)
    Public myPenLano As New System.Drawing.Pen(System.Drawing.Color.Black, 1)
    '.......................
    Public myPenVid0 As New System.Drawing.Pen(System.Drawing.Color.Green, 1) ' čára z 1.řady
    Public myPenVidB As New System.Drawing.Pen(System.Drawing.Color.Green, 1) ' čára z balkonu
    ' Public myPenVid1 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    '......................
    Friend WithEvents pbTah2 As PictureBox
    Friend WithEvents pbTah3 As PictureBox
    Friend WithEvents pbTah6 As PictureBox
    Friend WithEvents pbTah7 As PictureBox
    Friend WithEvents pbTah8 As PictureBox
    Friend WithEvents pbTah9 As PictureBox
    Friend WithEvents pbTah10 As PictureBox
    Friend WithEvents pbTah12 As PictureBox
    Friend WithEvents pbTah13 As PictureBox
    Friend WithEvents pbTah14 As PictureBox
    Friend WithEvents pbTah15 As PictureBox
    Friend WithEvents pbTah17 As PictureBox
    Friend WithEvents pbTah18 As PictureBox
    Friend WithEvents pbTah5 As PictureBox
    Friend WithEvents ComboBoxVyber As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents btnZavesit As Button
    Friend WithEvents ListBox5 As ListBox
    Friend WithEvents btnZobrazTahy As Button
    Friend WithEvents btnVymazVse As Button
    Friend WithEvents ListBoxInfo As ListBox
    Friend WithEvents btnVypnout As Button
    Friend WithEvents btnUlozScreen As Button
    Friend WithEvents ListBoxUvaz As ListBox
    Friend WithEvents ListBoxOdpo As ListBox
    Friend WithEvents ListBoxDek As ListBox
    Friend WithEvents ListBox8 As ListBox
    Friend WithEvents btnOtevritScreen As Button
    Friend WithEvents btnVetsi As Button
    Friend WithEvents btnMensi As Button
    Friend WithEvents PrintDialog1 As PrintDialog
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents pbTah1 As PictureBox
    Friend WithEvents prntDoc As New PrintDocument()  'tisk
    Friend WithEvents PrintDocument2 As PrintDocument
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents txtInsc As TextBox
    Friend WithEvents txtSitua As TextBox
    Friend WithEvents btnlVidit As Button
    Friend WithEvents Labelr1 As Label
    Friend WithEvents Labelr2 As Label
    Friend WithEvents Labelr3 As Label
    Friend WithEvents Labelr5 As Label
    Friend WithEvents Labelr6 As Label
    Friend WithEvents Labelr7 As Label
    Friend WithEvents Labelr8 As Label
    Friend WithEvents Labelr9 As Label
    Friend WithEvents Labelr10 As Label
    Friend WithEvents Labelr12 As Label
    Friend WithEvents Labelr13 As Label
    Friend WithEvents Labelr14 As Label
    Friend WithEvents Labelr15 As Label
    Friend WithEvents Labelr17 As Label
    Friend WithEvents Labelr18 As Label
    Friend WithEvents Labelr0 As Label
    Friend WithEvents txtPozn As TextBox
    Friend WithEvents radTah0 As CheckBox
    Friend WithEvents RadTah1 As CheckBox
    Friend WithEvents RadTah2 As CheckBox
    Friend WithEvents RadTah3 As CheckBox
    Friend WithEvents RadTah5 As CheckBox
    Friend WithEvents RadTah6 As CheckBox
    Friend WithEvents RadTah7 As CheckBox
    Friend WithEvents RadTah8 As CheckBox
    Friend WithEvents RadTah9 As CheckBox
    Friend WithEvents RadTah10 As CheckBox
    Friend WithEvents RadTah12 As CheckBox
    Friend WithEvents RadTah13 As CheckBox
    Friend WithEvents RadTah14 As CheckBox
    Friend WithEvents RadTah15 As CheckBox
    Friend WithEvents RadTah17 As CheckBox
    Friend WithEvents RadTah18 As CheckBox
    Friend WithEvents PrintDocument1 As System.Drawing.Printing.PrintDocument
    '......................................viditelnost 
    Public myPenKryc0 As New System.Drawing.Pen(System.Drawing.Color.White, 2)
    Public myPenKrycB As New System.Drawing.Pen(System.Drawing.Color.White, 2)
    '...
    ' Public myPenKryc1 As New System.Drawing.Pen(System.Drawing.Color.White, 1)
    '...
    Public myPenPuvTah1 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenNovTah1 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenKrycTah1 As New System.Drawing.Pen(System.Drawing.Color.White, 2)
    Public DpuvZacXTah1 As Double
    Public DpuvZacYTah1 As Double
    Public DpuvKonXTah1 As Double
    Public DpuvKonYTah1 As Double
    Public puvZacXTah1 As Integer
    Public puvZacYTah1 As Integer
    Public puvKonXTah1 As Integer
    Public puvKonYTah1 As Integer
    Public AngleTah1 As Double
    Public novKonXTah1 As Integer, novKonYTah1 As Integer
    '...
    ' Public myPenKryc2 As New System.Drawing.Pen(System.Drawing.Color.White, 1)
    '...
    Public myPenPuvTah2 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenNovTah2 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenKrycTah2 As New System.Drawing.Pen(System.Drawing.Color.White, 2)
    Public DpuvZacXTah2 As Double
    Public DpuvZacYTah2 As Double
    Public DpuvKonXTah2 As Double
    Public DpuvKonYTah2 As Double
    Public puvZacXTah2 As Integer
    Public puvZacYTah2 As Integer
    Public puvKonXTah2 As Integer
    Public puvKonYTah2 As Integer
    Public AngleTah2 As Double
    Public novKonXTah2 As Integer, novKonYTah2 As Integer
    '...
    ' Public myPenKryc3 As New System.Drawing.Pen(System.Drawing.Color.White, 1)
    '...
    Public myPenPuvTah3 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenNovTah3 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenKrycTah3 As New System.Drawing.Pen(System.Drawing.Color.White, 2)
    Public DpuvZacXTah3 As Double
    Public DpuvZacYTah3 As Double
    Public DpuvKonXTah3 As Double
    Public DpuvKonYTah3 As Double
    Public puvZacXTah3 As Integer
    Public puvZacYTah3 As Integer
    Public puvKonXTah3 As Integer
    Public puvKonYTah3 As Integer
    Public AngleTah3 As Double
    Public novKonXTah3 As Integer, novKonYTah3 As Integer
    '...
    'Public myPenKryc5 As New System.Drawing.Pen(System.Drawing.Color.White, 1)
    '...
    Public myPenPuvTah5 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenNovTah5 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenKrycTah5 As New System.Drawing.Pen(System.Drawing.Color.White, 2)
    Public DpuvZacXTah5 As Double
    Public DpuvZacYTah5 As Double
    Public DpuvKonXTah5 As Double
    Public DpuvKonYTah5 As Double
    Public puvZacXTah5 As Integer
    Public puvZacYTah5 As Integer
    Public puvKonXTah5 As Integer
    Public puvKonYTah5 As Integer
    Public AngleTah5 As Double
    Public novKonXTah5 As Integer, novKonYTah5 As Integer
    '...
    'Public myPenKryc6 As New System.Drawing.Pen(System.Drawing.Color.White, 1)
    '...
    Public myPenPuvTah6 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenNovTah6 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenKrycTah6 As New System.Drawing.Pen(System.Drawing.Color.White, 2)
    Public DpuvZacXTah6 As Double
    Public DpuvZacYTah6 As Double
    Public DpuvKonXTah6 As Double
    Public DpuvKonYTah6 As Double
    Public puvZacXTah6 As Integer
    Public puvZacYTah6 As Integer
    Public puvKonXTah6 As Integer
    Public puvKonYTah6 As Integer
    Public AngleTah6 As Double
    Public novKonXTah6 As Integer, novKonYTah6 As Integer
    '...
    'Public myPenKryc7 As New System.Drawing.Pen(System.Drawing.Color.White, 1)
    '...
    Public myPenPuvTah7 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenNovTah7 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenKrycTah7 As New System.Drawing.Pen(System.Drawing.Color.White, 2)
    Public DpuvZacXTah7 As Double
    Public DpuvZacYTah7 As Double
    Public DpuvKonXTah7 As Double
    Public DpuvKonYTah7 As Double
    Public puvZacXTah7 As Integer
    Public puvZacYTah7 As Integer
    Public puvKonXTah7 As Integer
    Public puvKonYTah7 As Integer
    Public AngleTah7 As Double
    Public novKonXTah7 As Integer, novKonYTah7 As Integer
    '...
    ' Public myPenKryc8 As New System.Drawing.Pen(System.Drawing.Color.White, 1)
    '...
    Public myPenPuvTah8 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenNovTah8 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenKrycTah8 As New System.Drawing.Pen(System.Drawing.Color.White, 2)
    Public DpuvZacXTah8 As Double
    Public DpuvZacYTah8 As Double
    Public DpuvKonXTah8 As Double
    Public DpuvKonYTah8 As Double
    Public puvZacXTah8 As Integer
    Public puvZacYTah8 As Integer
    Public puvKonXTah8 As Integer
    Public puvKonYTah8 As Integer
    Public AngleTah8 As Double
    Public novKonXTah8 As Integer, novKonYTah8 As Integer
    '...
    ' Public myPenKryc9 As New System.Drawing.Pen(System.Drawing.Color.White, 1)
    '...
    Public myPenPuvTah9 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenNovTah9 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenKrycTah9 As New System.Drawing.Pen(System.Drawing.Color.White, 2)
    Public DpuvZacXTah9 As Double
    Public DpuvZacYTah9 As Double
    Public DpuvKonXTah9 As Double
    Public DpuvKonYTah9 As Double
    Public puvZacXTah9 As Integer
    Public puvZacYTah9 As Integer
    Public puvKonXTah9 As Integer
    Public puvKonYTah9 As Integer
    Public AngleTah9 As Double
    Public novKonXTah9 As Integer, novKonYTah9 As Integer
    '...
    ' Public myPenKryc10 As New System.Drawing.Pen(System.Drawing.Color.White, 1)
    '...
    Public myPenPuvTah10 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenNovTah10 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenKrycTah10 As New System.Drawing.Pen(System.Drawing.Color.White, 2)
    Public DpuvZacXTah10 As Double
    Public DpuvZacYTah10 As Double
    Public DpuvKonXTah10 As Double
    Public DpuvKonYTah10 As Double
    Public puvZacXTah10 As Integer
    Public puvZacYTah10 As Integer
    Public puvKonXTah10 As Integer
    Public puvKonYTah10 As Integer
    Public AngleTah10 As Double
    Public novKonXTah10 As Integer, novKonYTah10 As Integer
    '...
    ' Public myPenKryc12 As New System.Drawing.Pen(System.Drawing.Color.White, 1)
    '...
    Public myPenPuvTah12 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenNovTah12 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenKrycTah12 As New System.Drawing.Pen(System.Drawing.Color.White, 2)
    Public DpuvZacXTah12 As Double
    Public DpuvZacYTah12 As Double
    Public DpuvKonXTah12 As Double
    Public DpuvKonYTah12 As Double
    Public puvZacXTah12 As Integer
    Public puvZacYTah12 As Integer
    Public puvKonXTah12 As Integer
    Public puvKonYTah12 As Integer
    Public AngleTah12 As Double
    Public novKonXTah12 As Integer, novKonYTah12 As Integer
    '...
    ' Public myPenKryc13 As New System.Drawing.Pen(System.Drawing.Color.White, 1)
    '...
    Public myPenPuvTah13 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenNovTah13 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenKrycTah13 As New System.Drawing.Pen(System.Drawing.Color.White, 2)
    Public DpuvZacXTah13 As Double
    Public DpuvZacYTah13 As Double
    Public DpuvKonXTah13 As Double
    Public DpuvKonYTah13 As Double
    Public puvZacXTah13 As Integer
    Public puvZacYTah13 As Integer
    Public puvKonXTah13 As Integer
    Public puvKonYTah13 As Integer
    Public AngleTah13 As Double
    Public novKonXTah13 As Integer, novKonYTah13 As Integer
    '...
    ' Public myPenKryc14 As New System.Drawing.Pen(System.Drawing.Color.White, 1)
    '...
    Public myPenPuvTah14 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenNovTah14 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenKrycTah14 As New System.Drawing.Pen(System.Drawing.Color.White, 2)
    Public DpuvZacXTah14 As Double
    Public DpuvZacYTah14 As Double
    Public DpuvKonXTah14 As Double
    Public DpuvKonYTah14 As Double
    Public puvZacXTah14 As Integer
    Public puvZacYTah14 As Integer
    Public puvKonXTah14 As Integer
    Public puvKonYTah14 As Integer
    Public AngleTah14 As Double
    Public novKonXTah14 As Integer, novKonYTah14 As Integer
    '...
    ' Public myPenKryc15 As New System.Drawing.Pen(System.Drawing.Color.White, 1)
    '...
    Public myPenPuvTah15 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenNovTah15 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenKrycTah15 As New System.Drawing.Pen(System.Drawing.Color.White, 2)
    Public DpuvZacXTah15 As Double
    Public DpuvZacYTah15 As Double
    Public DpuvKonXTah15 As Double
    Public DpuvKonYTah15 As Double
    Public puvZacXTah15 As Integer
    Public puvZacYTah15 As Integer
    Public puvKonXTah15 As Integer
    Public puvKonYTah15 As Integer
    Public AngleTah15 As Double
    Public novKonXTah15 As Integer, novKonYTah15 As Integer
    '...
    ' Public myPenKryc17 As New System.Drawing.Pen(System.Drawing.Color.White, 1)
    '...
    Public myPenPuvTah17 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenNovTah17 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenKrycTah17 As New System.Drawing.Pen(System.Drawing.Color.White, 2)
    Public DpuvZacXTah17 As Double
    Public DpuvZacYTah17 As Double
    Public DpuvKonXTah17 As Double
    Public DpuvKonYTah17 As Double
    Public puvZacXTah17 As Integer
    Public puvZacYTah17 As Integer
    Public puvKonXTah17 As Integer
    Public puvKonYTah17 As Integer
    Public AngleTah17 As Double
    Public novKonXTah17 As Integer, novKonYTah17 As Integer
    '...
    ' Public myPenKryc18 As New System.Drawing.Pen(System.Drawing.Color.White, 1)
    '...
    Public myPenPuvTah18 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenNovTah18 As New System.Drawing.Pen(System.Drawing.Color.Green, 1)
    Public myPenKrycTah18 As New System.Drawing.Pen(System.Drawing.Color.White, 2)
    Public DpuvZacXTah18 As Double
    Public DpuvZacYTah18 As Double
    Public DpuvKonXTah18 As Double
    Public DpuvKonYTah18 As Double
    Public puvZacXTah18 As Integer
    Public puvZacYTah18 As Integer
    Public puvKonXTah18 As Integer
    Public puvKonYTah18 As Integer
    Public AngleTah18 As Double
    Public novKonXTah18 As Integer, novKonYTah18 As Integer
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PaletteBox As PictureBox
    Friend WithEvents Label1k As Label
    Friend WithEvents TrackBar1 As TrackBar
    Friend WithEvents TextBox2k As TextBox
    Friend WithEvents Label2k As Label
    ' Public Schovej As String



    '........kreslení
#Region "enumerations"
    Public Enum dModes
        Line
        Rectangle
        Ellipse
        Brush
        Path
        Text
        Eraser

    End Enum

    Public Enum dStyles
        Outline
        OutlineFilled
        Filled
    End Enum

#End Region
#Region "declarations"
    Dim bmp As Bitmap
    Dim bmp2 As Bitmap
    Dim g2 As Graphics
    Dim clr As Color = Color.Red
    Dim clr2 As Color = Color.Blue
    Private StartX, StartY, EndX, EndY, BoxWidth, BoxHeight As Integer
    Dim mpath As New Drawing2D.GraphicsPath
    Dim dmode As dModes
    Dim xLoc, yLoc As Integer
    Dim dstyle As dStyles = dStyles.Outline
    Dim pWidth As Int16 = 1
    Dim c As New Cursor(GetEmbeddedFile("tahy.ColorPicker.ico"))
    Dim er As New Cursor(GetEmbeddedFile("tahy.Eraser.ico"))
    Dim isDraw As Boolean
    Dim pF, pFOld As PointF
    Dim nPen As New Pen(clr)
    Declare Function HideCaret Lib "user32.dll" (ByVal hwnd As Int32) As Int32
    Dim allow As Boolean
    Declare Function ShowCaret Lib "user32.dll" (ByVal hwnd As Int32) As Int32
    Declare Function CreateCaret Lib "user32.dll" (ByVal hwnd As Int32, ByVal hBitmap As Int32, ByVal nWidth As Int32, ByVal nHeight As Int32) As Int32
    Declare Function SetCaretPos Lib "user32.dll" (ByVal x As Int32, ByVal y As Int32) As Int32
    Friend WithEvents pbox As PictureBox
    Friend WithEvents radBalk As CheckBox
    Friend WithEvents Labelr19 As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents pbSiluet As PictureBox
    Friend WithEvents btnSiluet As Button
    Friend WithEvents PanelZakryj As Panel
    Friend WithEvents btnPrint As Button
    Friend WithEvents Label2p As Label
    Friend WithEvents Label20 As Label
    Friend WithEvents Label21 As Label
    Dim txt As String
#End Region
#Region "properties"
    Public ReadOnly Property Color() As Color
        Get
            Return clr
        End Get
    End Property

    Property penWidth() As Int16
        Get
            Return pWidth
        End Get
        Set(ByVal Value As Int16)
            pWidth = Value
            nPen.Width = pWidth
        End Set
    End Property

    Property DrawStyles() As dStyles
        Get
            Return dstyle
        End Get
        Set(ByVal Value As dStyles)
            dstyle = Value
        End Set
    End Property

#End Region
#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

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

    Friend WithEvents pb1 As PictureBox
    Friend WithEvents Label5 As Label
    Friend WithEvents btnOdvesit As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents btnUvrat As Button
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents ListBox6 As ListBox
    Friend WithEvents btnPosun1 As Button
    Friend WithEvents ListBox4 As ListBox
    Friend WithEvents ListBox3 As ListBox
    Friend WithEvents ListBox2 As ListBox

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer




    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.btnOdvesit = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.btnUvrat = New System.Windows.Forms.Button()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.ListBox2 = New System.Windows.Forms.ListBox()
        Me.ListBox3 = New System.Windows.Forms.ListBox()
        Me.ListBox4 = New System.Windows.Forms.ListBox()
        Me.btnPosun1 = New System.Windows.Forms.Button()
        Me.ListBox6 = New System.Windows.Forms.ListBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.ListBox7 = New System.Windows.Forms.ListBox()
        Me.pbTah2 = New System.Windows.Forms.PictureBox()
        Me.ComboBoxVyber = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.btnZavesit = New System.Windows.Forms.Button()
        Me.ListBox5 = New System.Windows.Forms.ListBox()
        Me.btnZobrazTahy = New System.Windows.Forms.Button()
        Me.btnVymazVse = New System.Windows.Forms.Button()
        Me.ListBoxInfo = New System.Windows.Forms.ListBox()
        Me.btnVypnout = New System.Windows.Forms.Button()
        Me.btnUlozScreen = New System.Windows.Forms.Button()
        Me.ListBoxUvaz = New System.Windows.Forms.ListBox()
        Me.ListBoxOdpo = New System.Windows.Forms.ListBox()
        Me.ListBoxDek = New System.Windows.Forms.ListBox()
        Me.ListBox8 = New System.Windows.Forms.ListBox()
        Me.btnOtevritScreen = New System.Windows.Forms.Button()
        Me.btnVetsi = New System.Windows.Forms.Button()
        Me.btnMensi = New System.Windows.Forms.Button()
        Me.PrintDialog1 = New System.Windows.Forms.PrintDialog()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.PrintDocument2 = New System.Drawing.Printing.PrintDocument()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtInsc = New System.Windows.Forms.TextBox()
        Me.txtSitua = New System.Windows.Forms.TextBox()
        Me.btnlVidit = New System.Windows.Forms.Button()
        Me.Labelr1 = New System.Windows.Forms.Label()
        Me.Labelr2 = New System.Windows.Forms.Label()
        Me.Labelr3 = New System.Windows.Forms.Label()
        Me.Labelr5 = New System.Windows.Forms.Label()
        Me.Labelr6 = New System.Windows.Forms.Label()
        Me.Labelr7 = New System.Windows.Forms.Label()
        Me.Labelr8 = New System.Windows.Forms.Label()
        Me.Labelr9 = New System.Windows.Forms.Label()
        Me.Labelr10 = New System.Windows.Forms.Label()
        Me.Labelr12 = New System.Windows.Forms.Label()
        Me.Labelr13 = New System.Windows.Forms.Label()
        Me.Labelr14 = New System.Windows.Forms.Label()
        Me.Labelr15 = New System.Windows.Forms.Label()
        Me.Labelr17 = New System.Windows.Forms.Label()
        Me.Labelr18 = New System.Windows.Forms.Label()
        Me.Labelr0 = New System.Windows.Forms.Label()
        Me.txtPozn = New System.Windows.Forms.TextBox()
        Me.radTah0 = New System.Windows.Forms.CheckBox()
        Me.RadTah1 = New System.Windows.Forms.CheckBox()
        Me.RadTah2 = New System.Windows.Forms.CheckBox()
        Me.RadTah3 = New System.Windows.Forms.CheckBox()
        Me.RadTah5 = New System.Windows.Forms.CheckBox()
        Me.RadTah6 = New System.Windows.Forms.CheckBox()
        Me.RadTah7 = New System.Windows.Forms.CheckBox()
        Me.RadTah8 = New System.Windows.Forms.CheckBox()
        Me.RadTah9 = New System.Windows.Forms.CheckBox()
        Me.RadTah10 = New System.Windows.Forms.CheckBox()
        Me.RadTah12 = New System.Windows.Forms.CheckBox()
        Me.RadTah13 = New System.Windows.Forms.CheckBox()
        Me.RadTah14 = New System.Windows.Forms.CheckBox()
        Me.RadTah15 = New System.Windows.Forms.CheckBox()
        Me.RadTah17 = New System.Windows.Forms.CheckBox()
        Me.RadTah18 = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1k = New System.Windows.Forms.Label()
        Me.TrackBar1 = New System.Windows.Forms.TrackBar()
        Me.TextBox2k = New System.Windows.Forms.TextBox()
        Me.Label2k = New System.Windows.Forms.Label()
        Me.radBalk = New System.Windows.Forms.CheckBox()
        Me.Labelr19 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.pbSiluet = New System.Windows.Forms.PictureBox()
        Me.pbox = New System.Windows.Forms.PictureBox()
        Me.pbTah1 = New System.Windows.Forms.PictureBox()
        Me.PaletteBox = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.pbTah5 = New System.Windows.Forms.PictureBox()
        Me.pbTah18 = New System.Windows.Forms.PictureBox()
        Me.pbTah17 = New System.Windows.Forms.PictureBox()
        Me.pbTah15 = New System.Windows.Forms.PictureBox()
        Me.pbTah14 = New System.Windows.Forms.PictureBox()
        Me.pbTah13 = New System.Windows.Forms.PictureBox()
        Me.pbTah12 = New System.Windows.Forms.PictureBox()
        Me.pbTah10 = New System.Windows.Forms.PictureBox()
        Me.pbTah9 = New System.Windows.Forms.PictureBox()
        Me.pbTah8 = New System.Windows.Forms.PictureBox()
        Me.pbTah7 = New System.Windows.Forms.PictureBox()
        Me.pbTah6 = New System.Windows.Forms.PictureBox()
        Me.pbTah3 = New System.Windows.Forms.PictureBox()
        Me.pb1 = New System.Windows.Forms.PictureBox()
        Me.btnSiluet = New System.Windows.Forms.Button()
        Me.PanelZakryj = New System.Windows.Forms.Panel()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.Label2p = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        CType(Me.pbTah2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbSiluet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbTah1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PaletteBox, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbTah5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbTah18, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbTah17, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbTah15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbTah14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbTah13, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbTah12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbTah10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbTah9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbTah8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbTah7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbTah6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbTah3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnOdvesit
        '
        Me.btnOdvesit.BackColor = System.Drawing.Color.White
        Me.btnOdvesit.Enabled = False
        Me.btnOdvesit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOdvesit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnOdvesit.Location = New System.Drawing.Point(497, 110)
        Me.btnOdvesit.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.btnOdvesit.Name = "btnOdvesit"
        Me.btnOdvesit.Size = New System.Drawing.Size(40, 26)
        Me.btnOdvesit.TabIndex = 32
        Me.btnOdvesit.Text = " x"
        Me.btnOdvesit.UseVisualStyleBackColor = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label5.Location = New System.Drawing.Point(495, 52)
        Me.Label5.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(42, 14)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "odvěsit"
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Enabled = False
        Me.TextBox1.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.TextBox1.ForeColor = System.Drawing.Color.Blue
        Me.TextBox1.Location = New System.Drawing.Point(63, 110)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(56, 26)
        Me.TextBox1.TabIndex = 34
        Me.TextBox1.Text = "0"
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox2
        '
        Me.TextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox2.Enabled = False
        Me.TextBox2.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.TextBox2.ForeColor = System.Drawing.Color.Red
        Me.TextBox2.Location = New System.Drawing.Point(121, 110)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(56, 26)
        Me.TextBox2.TabIndex = 35
        Me.TextBox2.Text = "0"
        Me.TextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TextBox3
        '
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox3.Cursor = System.Windows.Forms.Cursors.No
        Me.TextBox3.Enabled = False
        Me.TextBox3.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.TextBox3.Location = New System.Drawing.Point(295, 110)
        Me.TextBox3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(56, 26)
        Me.TextBox3.TabIndex = 41
        Me.TextBox3.Text = "0"
        Me.TextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnUvrat
        '
        Me.btnUvrat.BackColor = System.Drawing.Color.White
        Me.btnUvrat.Enabled = False
        Me.btnUvrat.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUvrat.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnUvrat.Location = New System.Drawing.Point(237, 110)
        Me.btnUvrat.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.btnUvrat.Name = "btnUvrat"
        Me.btnUvrat.Size = New System.Drawing.Size(56, 26)
        Me.btnUvrat.TabIndex = 49
        Me.btnUvrat.Text = "¯↑¯"
        Me.btnUvrat.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnUvrat.UseVisualStyleBackColor = False
        '
        'ListBox1
        '
        Me.ListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 14
        Me.ListBox1.Items.AddRange(New Object() {"   zavěsit", "na podlaze"})
        Me.ListBox1.Location = New System.Drawing.Point(175, 52)
        Me.ListBox1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(56, 28)
        Me.ListBox1.TabIndex = 53
        '
        'ListBox2
        '
        Me.ListBox2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBox2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.ItemHeight = 14
        Me.ListBox2.Items.AddRange(New Object() {"    skrýt", "horní úvrať"})
        Me.ListBox2.Location = New System.Drawing.Point(234, 52)
        Me.ListBox2.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(64, 28)
        Me.ListBox2.TabIndex = 54
        '
        'ListBox3
        '
        Me.ListBox3.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBox3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.ListBox3.FormattingEnabled = True
        Me.ListBox3.ItemHeight = 14
        Me.ListBox3.Items.AddRange(New Object() {"   výška", " dekorace", "   "})
        Me.ListBox3.Location = New System.Drawing.Point(63, 52)
        Me.ListBox3.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ListBox3.Name = "ListBox3"
        Me.ListBox3.Size = New System.Drawing.Size(56, 42)
        Me.ListBox3.TabIndex = 55
        '
        'ListBox4
        '
        Me.ListBox4.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBox4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.ListBox4.FormattingEnabled = True
        Me.ListBox4.ItemHeight = 14
        Me.ListBox4.Items.AddRange(New Object() {"  délka", "úvazku", "  "})
        Me.ListBox4.Location = New System.Drawing.Point(128, 52)
        Me.ListBox4.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ListBox4.Name = "ListBox4"
        Me.ListBox4.Size = New System.Drawing.Size(42, 42)
        Me.ListBox4.TabIndex = 56
        '
        'btnPosun1
        '
        Me.btnPosun1.BackColor = System.Drawing.Color.White
        Me.btnPosun1.Enabled = False
        Me.btnPosun1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPosun1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnPosun1.Location = New System.Drawing.Point(368, 110)
        Me.btnPosun1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.btnPosun1.Name = "btnPosun1"
        Me.btnPosun1.Size = New System.Drawing.Size(56, 26)
        Me.btnPosun1.TabIndex = 58
        Me.btnPosun1.Text = "↓↑"
        Me.btnPosun1.UseVisualStyleBackColor = False
        '
        'ListBox6
        '
        Me.ListBox6.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBox6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.ListBox6.FormattingEnabled = True
        Me.ListBox6.ItemHeight = 14
        Me.ListBox6.Items.AddRange(New Object() {"    výška ", "od podlahy", "    "})
        Me.ListBox6.Location = New System.Drawing.Point(294, 52)
        Me.ListBox6.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ListBox6.Name = "ListBox6"
        Me.ListBox6.Size = New System.Drawing.Size(61, 42)
        Me.ListBox6.TabIndex = 59
        '
        'TextBox4
        '
        Me.TextBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox4.Cursor = System.Windows.Forms.Cursors.Default
        Me.TextBox4.Enabled = False
        Me.TextBox4.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.TextBox4.Location = New System.Drawing.Point(426, 110)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(56, 26)
        Me.TextBox4.TabIndex = 61
        Me.TextBox4.Text = "0"
        Me.TextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ListBox7
        '
        Me.ListBox7.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBox7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.ListBox7.FormattingEnabled = True
        Me.ListBox7.ItemHeight = 14
        Me.ListBox7.Items.AddRange(New Object() {"zvednout-spustit", "   od podlahy", "        "})
        Me.ListBox7.Location = New System.Drawing.Point(387, 52)
        Me.ListBox7.Name = "ListBox7"
        Me.ListBox7.Size = New System.Drawing.Size(95, 42)
        Me.ListBox7.TabIndex = 62
        '
        'pbTah2
        '
        Me.pbTah2.BackColor = System.Drawing.Color.White
        Me.pbTah2.ErrorImage = Nothing
        Me.pbTah2.InitialImage = Nothing
        Me.pbTah2.Location = New System.Drawing.Point(844, 28)
        Me.pbTah2.Name = "pbTah2"
        Me.pbTah2.Size = New System.Drawing.Size(10, 17)
        Me.pbTah2.TabIndex = 64
        Me.pbTah2.TabStop = False
        '
        'ComboBoxVyber
        '
        Me.ComboBoxVyber.BackColor = System.Drawing.Color.White
        Me.ComboBoxVyber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxVyber.Enabled = False
        Me.ComboBoxVyber.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ComboBoxVyber.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.ComboBoxVyber.FormattingEnabled = True
        Me.ComboBoxVyber.Items.AddRange(New Object() {"1", "2", "3", "5", "6", "7", "8", "9", "10", "12", "13", "14", "15", "17", "18"})
        Me.ComboBoxVyber.Location = New System.Drawing.Point(10, 110)
        Me.ComboBoxVyber.MaxDropDownItems = 15
        Me.ComboBoxVyber.Name = "ComboBoxVyber"
        Me.ComboBoxVyber.Size = New System.Drawing.Size(47, 24)
        Me.ComboBoxVyber.TabIndex = 83
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label1.Location = New System.Drawing.Point(829, 4)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(12, 15)
        Me.Label1.TabIndex = 86
        Me.Label1.Text = "1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label2.Location = New System.Drawing.Point(844, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(12, 15)
        Me.Label2.TabIndex = 87
        Me.Label2.Text = "2"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label3.Location = New System.Drawing.Point(857, 4)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(12, 15)
        Me.Label3.TabIndex = 88
        Me.Label3.Text = "3"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label4.Location = New System.Drawing.Point(894, 4)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(12, 15)
        Me.Label4.TabIndex = 89
        Me.Label4.Text = "5"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label6.Location = New System.Drawing.Point(909, 4)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(12, 15)
        Me.Label6.TabIndex = 90
        Me.Label6.Text = "6"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label7.Location = New System.Drawing.Point(924, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(12, 15)
        Me.Label7.TabIndex = 91
        Me.Label7.Text = "7"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label8.Location = New System.Drawing.Point(937, 4)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(12, 15)
        Me.Label8.TabIndex = 92
        Me.Label8.Text = "8"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label9.Location = New System.Drawing.Point(951, 4)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(12, 15)
        Me.Label9.TabIndex = 93
        Me.Label9.Text = "9"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label10.Location = New System.Drawing.Point(962, 4)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(17, 15)
        Me.Label10.TabIndex = 94
        Me.Label10.Text = "10"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label11.Location = New System.Drawing.Point(994, 4)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(17, 15)
        Me.Label11.TabIndex = 95
        Me.Label11.Text = "12"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label12.Location = New System.Drawing.Point(1009, 4)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(17, 15)
        Me.Label12.TabIndex = 96
        Me.Label12.Text = "13"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label13.Location = New System.Drawing.Point(1023, 4)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(17, 15)
        Me.Label13.TabIndex = 97
        Me.Label13.Text = "14"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label14.Location = New System.Drawing.Point(1062, 4)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(17, 15)
        Me.Label14.TabIndex = 98
        Me.Label14.Text = "15"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label15.Location = New System.Drawing.Point(1088, 4)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(17, 15)
        Me.Label15.TabIndex = 99
        Me.Label15.Text = "17"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label16.Location = New System.Drawing.Point(1103, 4)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(17, 15)
        Me.Label16.TabIndex = 100
        Me.Label16.Text = "18"
        '
        'btnZavesit
        '
        Me.btnZavesit.BackColor = System.Drawing.Color.White
        Me.btnZavesit.Enabled = False
        Me.btnZavesit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnZavesit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnZavesit.Location = New System.Drawing.Point(179, 110)
        Me.btnZavesit.Name = "btnZavesit"
        Me.btnZavesit.Size = New System.Drawing.Size(56, 26)
        Me.btnZavesit.TabIndex = 60
        Me.btnZavesit.Tag = ""
        Me.btnZavesit.Text = "_↓_"
        Me.btnZavesit.UseVisualStyleBackColor = False
        '
        'ListBox5
        '
        Me.ListBox5.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBox5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.ListBox5.FormattingEnabled = True
        Me.ListBox5.ItemHeight = 14
        Me.ListBox5.Items.AddRange(New Object() {" tah", "číslo"})
        Me.ListBox5.Location = New System.Drawing.Point(10, 52)
        Me.ListBox5.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.ListBox5.Name = "ListBox5"
        Me.ListBox5.Size = New System.Drawing.Size(36, 42)
        Me.ListBox5.TabIndex = 101
        '
        'btnZobrazTahy
        '
        Me.btnZobrazTahy.BackColor = System.Drawing.Color.Silver
        Me.btnZobrazTahy.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnZobrazTahy.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnZobrazTahy.Location = New System.Drawing.Point(10, 17)
        Me.btnZobrazTahy.Name = "btnZobrazTahy"
        Me.btnZobrazTahy.Size = New System.Drawing.Size(170, 26)
        Me.btnZobrazTahy.TabIndex = 102
        Me.btnZobrazTahy.Text = "zapnout kalkulátor"
        Me.btnZobrazTahy.UseVisualStyleBackColor = False
        '
        'btnVymazVse
        '
        Me.btnVymazVse.BackColor = System.Drawing.Color.White
        Me.btnVymazVse.Enabled = False
        Me.btnVymazVse.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnVymazVse.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnVymazVse.Location = New System.Drawing.Point(497, 78)
        Me.btnVymazVse.Name = "btnVymazVse"
        Me.btnVymazVse.Size = New System.Drawing.Size(40, 26)
        Me.btnVymazVse.TabIndex = 103
        Me.btnVymazVse.Text = "vše"
        Me.btnVymazVse.UseVisualStyleBackColor = False
        '
        'ListBoxInfo
        '
        Me.ListBoxInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListBoxInfo.Cursor = System.Windows.Forms.Cursors.No
        Me.ListBoxInfo.Enabled = False
        Me.ListBoxInfo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.ListBoxInfo.FormattingEnabled = True
        Me.ListBoxInfo.ItemHeight = 14
        Me.ListBoxInfo.Location = New System.Drawing.Point(63, 186)
        Me.ListBoxInfo.Name = "ListBoxInfo"
        Me.ListBoxInfo.Size = New System.Drawing.Size(474, 212)
        Me.ListBoxInfo.TabIndex = 105
        '
        'btnVypnout
        '
        Me.btnVypnout.BackColor = System.Drawing.Color.Silver
        Me.btnVypnout.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnVypnout.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnVypnout.Location = New System.Drawing.Point(384, 17)
        Me.btnVypnout.Name = "btnVypnout"
        Me.btnVypnout.Size = New System.Drawing.Size(153, 26)
        Me.btnVypnout.TabIndex = 106
        Me.btnVypnout.Text = "vypnout kalkulátor"
        Me.btnVypnout.UseVisualStyleBackColor = False
        '
        'btnUlozScreen
        '
        Me.btnUlozScreen.BackColor = System.Drawing.Color.White
        Me.btnUlozScreen.Enabled = False
        Me.btnUlozScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUlozScreen.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnUlozScreen.Location = New System.Drawing.Point(10, 545)
        Me.btnUlozScreen.Name = "btnUlozScreen"
        Me.btnUlozScreen.Size = New System.Drawing.Size(93, 26)
        Me.btnUlozScreen.TabIndex = 108
        Me.btnUlozScreen.Text = "uložit výkres"
        Me.btnUlozScreen.UseVisualStyleBackColor = False
        '
        'ListBoxUvaz
        '
        Me.ListBoxUvaz.BackColor = System.Drawing.Color.White
        Me.ListBoxUvaz.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBoxUvaz.Cursor = System.Windows.Forms.Cursors.No
        Me.ListBoxUvaz.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.ListBoxUvaz.ForeColor = System.Drawing.Color.Red
        Me.ListBoxUvaz.FormattingEnabled = True
        Me.ListBoxUvaz.ItemHeight = 14
        Me.ListBoxUvaz.Location = New System.Drawing.Point(121, 84)
        Me.ListBoxUvaz.Name = "ListBoxUvaz"
        Me.ListBoxUvaz.Size = New System.Drawing.Size(58, 14)
        Me.ListBoxUvaz.TabIndex = 110
        Me.ListBoxUvaz.Visible = False
        '
        'ListBoxOdpo
        '
        Me.ListBoxOdpo.BackColor = System.Drawing.Color.White
        Me.ListBoxOdpo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBoxOdpo.Cursor = System.Windows.Forms.Cursors.No
        Me.ListBoxOdpo.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.ListBoxOdpo.FormattingEnabled = True
        Me.ListBoxOdpo.ItemHeight = 14
        Me.ListBoxOdpo.Location = New System.Drawing.Point(426, 84)
        Me.ListBoxOdpo.Name = "ListBoxOdpo"
        Me.ListBoxOdpo.Size = New System.Drawing.Size(58, 14)
        Me.ListBoxOdpo.TabIndex = 111
        Me.ListBoxOdpo.Visible = False
        '
        'ListBoxDek
        '
        Me.ListBoxDek.BackColor = System.Drawing.Color.White
        Me.ListBoxDek.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBoxDek.Cursor = System.Windows.Forms.Cursors.No
        Me.ListBoxDek.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.ListBoxDek.ForeColor = System.Drawing.Color.Blue
        Me.ListBoxDek.ItemHeight = 14
        Me.ListBoxDek.Location = New System.Drawing.Point(63, 84)
        Me.ListBoxDek.Name = "ListBoxDek"
        Me.ListBoxDek.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ListBoxDek.Size = New System.Drawing.Size(58, 14)
        Me.ListBoxDek.TabIndex = 109
        Me.ListBoxDek.Visible = False
        '
        'ListBox8
        '
        Me.ListBox8.BackColor = System.Drawing.Color.White
        Me.ListBox8.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ListBox8.Cursor = System.Windows.Forms.Cursors.No
        Me.ListBox8.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.ListBox8.ForeColor = System.Drawing.Color.Black
        Me.ListBox8.ItemHeight = 14
        Me.ListBox8.Items.AddRange(New Object() {"cm"})
        Me.ListBox8.Location = New System.Drawing.Point(312, 84)
        Me.ListBox8.Name = "ListBox8"
        Me.ListBox8.Size = New System.Drawing.Size(20, 14)
        Me.ListBox8.TabIndex = 112
        Me.ListBox8.Visible = False
        '
        'btnOtevritScreen
        '
        Me.btnOtevritScreen.BackColor = System.Drawing.Color.White
        Me.btnOtevritScreen.Enabled = False
        Me.btnOtevritScreen.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOtevritScreen.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnOtevritScreen.Location = New System.Drawing.Point(444, 545)
        Me.btnOtevritScreen.Name = "btnOtevritScreen"
        Me.btnOtevritScreen.Size = New System.Drawing.Size(93, 26)
        Me.btnOtevritScreen.TabIndex = 114
        Me.btnOtevritScreen.Text = "otevřít výkres"
        Me.btnOtevritScreen.UseVisualStyleBackColor = False
        '
        'btnVetsi
        '
        Me.btnVetsi.BackColor = System.Drawing.Color.White
        Me.btnVetsi.Enabled = False
        Me.btnVetsi.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnVetsi.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnVetsi.Location = New System.Drawing.Point(186, 17)
        Me.btnVetsi.Name = "btnVetsi"
        Me.btnVetsi.Size = New System.Drawing.Size(93, 26)
        Me.btnVetsi.TabIndex = 115
        Me.btnVetsi.Text = "max.zobrazení"
        Me.btnVetsi.UseVisualStyleBackColor = False
        '
        'btnMensi
        '
        Me.btnMensi.BackColor = System.Drawing.Color.White
        Me.btnMensi.Enabled = False
        Me.btnMensi.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMensi.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnMensi.Location = New System.Drawing.Point(285, 17)
        Me.btnMensi.Name = "btnMensi"
        Me.btnMensi.Size = New System.Drawing.Size(93, 26)
        Me.btnMensi.TabIndex = 116
        Me.btnMensi.Text = "min.zobrazení"
        Me.btnMensi.UseVisualStyleBackColor = False
        '
        'PrintDialog1
        '
        Me.PrintDialog1.UseEXDialog = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'PrintDocument2
        '
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label17.Location = New System.Drawing.Point(10, 616)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(67, 15)
        Me.Label17.TabIndex = 122
        Me.Label17.Text = "INSCENACE: "
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label18.Location = New System.Drawing.Point(292, 612)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(50, 15)
        Me.Label18.TabIndex = 123
        Me.Label18.Text = "SITUACE:"
        '
        'txtInsc
        '
        Me.txtInsc.BackColor = System.Drawing.Color.White
        Me.txtInsc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtInsc.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtInsc.Location = New System.Drawing.Point(83, 615)
        Me.txtInsc.Name = "txtInsc"
        Me.txtInsc.Size = New System.Drawing.Size(166, 20)
        Me.txtInsc.TabIndex = 124
        '
        'txtSitua
        '
        Me.txtSitua.BackColor = System.Drawing.Color.White
        Me.txtSitua.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSitua.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtSitua.Location = New System.Drawing.Point(348, 614)
        Me.txtSitua.Name = "txtSitua"
        Me.txtSitua.Size = New System.Drawing.Size(188, 20)
        Me.txtSitua.TabIndex = 125
        Me.txtSitua.Text = " "
        '
        'btnlVidit
        '
        Me.btnlVidit.BackColor = System.Drawing.Color.White
        Me.btnlVidit.Enabled = False
        Me.btnlVidit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnlVidit.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnlVidit.Location = New System.Drawing.Point(63, 142)
        Me.btnlVidit.Name = "btnlVidit"
        Me.btnlVidit.Size = New System.Drawing.Size(74, 38)
        Me.btnlVidit.TabIndex = 126
        Me.btnlVidit.Text = " úhly" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "viditelnosti"
        Me.btnlVidit.UseVisualStyleBackColor = False
        '
        'Labelr1
        '
        Me.Labelr1.AutoSize = True
        Me.Labelr1.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Labelr1.Location = New System.Drawing.Point(267, 144)
        Me.Labelr1.Name = "Labelr1"
        Me.Labelr1.Size = New System.Drawing.Size(12, 15)
        Me.Labelr1.TabIndex = 146
        Me.Labelr1.Text = "1"
        '
        'Labelr2
        '
        Me.Labelr2.AutoSize = True
        Me.Labelr2.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Labelr2.Location = New System.Drawing.Point(285, 144)
        Me.Labelr2.Name = "Labelr2"
        Me.Labelr2.Size = New System.Drawing.Size(12, 15)
        Me.Labelr2.TabIndex = 147
        Me.Labelr2.Text = "2"
        '
        'Labelr3
        '
        Me.Labelr3.AutoSize = True
        Me.Labelr3.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Labelr3.Location = New System.Drawing.Point(303, 144)
        Me.Labelr3.Name = "Labelr3"
        Me.Labelr3.Size = New System.Drawing.Size(12, 15)
        Me.Labelr3.TabIndex = 148
        Me.Labelr3.Text = "3"
        '
        'Labelr5
        '
        Me.Labelr5.AutoSize = True
        Me.Labelr5.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Labelr5.Location = New System.Drawing.Point(321, 144)
        Me.Labelr5.Name = "Labelr5"
        Me.Labelr5.Size = New System.Drawing.Size(12, 15)
        Me.Labelr5.TabIndex = 149
        Me.Labelr5.Text = "5"
        '
        'Labelr6
        '
        Me.Labelr6.AutoSize = True
        Me.Labelr6.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Labelr6.Location = New System.Drawing.Point(339, 144)
        Me.Labelr6.Name = "Labelr6"
        Me.Labelr6.Size = New System.Drawing.Size(12, 15)
        Me.Labelr6.TabIndex = 150
        Me.Labelr6.Text = "6"
        '
        'Labelr7
        '
        Me.Labelr7.AutoSize = True
        Me.Labelr7.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Labelr7.Location = New System.Drawing.Point(357, 144)
        Me.Labelr7.Name = "Labelr7"
        Me.Labelr7.Size = New System.Drawing.Size(12, 15)
        Me.Labelr7.TabIndex = 151
        Me.Labelr7.Text = "7"
        '
        'Labelr8
        '
        Me.Labelr8.AutoSize = True
        Me.Labelr8.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Labelr8.Location = New System.Drawing.Point(375, 144)
        Me.Labelr8.Name = "Labelr8"
        Me.Labelr8.Size = New System.Drawing.Size(12, 15)
        Me.Labelr8.TabIndex = 152
        Me.Labelr8.Text = "8"
        '
        'Labelr9
        '
        Me.Labelr9.AutoSize = True
        Me.Labelr9.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Labelr9.Location = New System.Drawing.Point(393, 144)
        Me.Labelr9.Name = "Labelr9"
        Me.Labelr9.Size = New System.Drawing.Size(12, 15)
        Me.Labelr9.TabIndex = 153
        Me.Labelr9.Text = "9"
        '
        'Labelr10
        '
        Me.Labelr10.AutoSize = True
        Me.Labelr10.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Labelr10.Location = New System.Drawing.Point(408, 144)
        Me.Labelr10.Name = "Labelr10"
        Me.Labelr10.Size = New System.Drawing.Size(17, 15)
        Me.Labelr10.TabIndex = 154
        Me.Labelr10.Text = "10"
        '
        'Labelr12
        '
        Me.Labelr12.AutoSize = True
        Me.Labelr12.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Labelr12.Location = New System.Drawing.Point(426, 144)
        Me.Labelr12.Name = "Labelr12"
        Me.Labelr12.Size = New System.Drawing.Size(17, 15)
        Me.Labelr12.TabIndex = 155
        Me.Labelr12.Text = "12"
        '
        'Labelr13
        '
        Me.Labelr13.AutoSize = True
        Me.Labelr13.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Labelr13.Location = New System.Drawing.Point(444, 144)
        Me.Labelr13.Name = "Labelr13"
        Me.Labelr13.Size = New System.Drawing.Size(17, 15)
        Me.Labelr13.TabIndex = 156
        Me.Labelr13.Text = "13"
        '
        'Labelr14
        '
        Me.Labelr14.AutoSize = True
        Me.Labelr14.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Labelr14.Location = New System.Drawing.Point(462, 144)
        Me.Labelr14.Name = "Labelr14"
        Me.Labelr14.Size = New System.Drawing.Size(17, 15)
        Me.Labelr14.TabIndex = 157
        Me.Labelr14.Text = "14"
        '
        'Labelr15
        '
        Me.Labelr15.AutoSize = True
        Me.Labelr15.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Labelr15.Location = New System.Drawing.Point(480, 144)
        Me.Labelr15.Name = "Labelr15"
        Me.Labelr15.Size = New System.Drawing.Size(17, 15)
        Me.Labelr15.TabIndex = 158
        Me.Labelr15.Text = "15"
        '
        'Labelr17
        '
        Me.Labelr17.AutoSize = True
        Me.Labelr17.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Labelr17.Location = New System.Drawing.Point(498, 144)
        Me.Labelr17.Name = "Labelr17"
        Me.Labelr17.Size = New System.Drawing.Size(17, 15)
        Me.Labelr17.TabIndex = 159
        Me.Labelr17.Text = "17"
        '
        'Labelr18
        '
        Me.Labelr18.AutoSize = True
        Me.Labelr18.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Labelr18.Location = New System.Drawing.Point(516, 144)
        Me.Labelr18.Name = "Labelr18"
        Me.Labelr18.Size = New System.Drawing.Size(17, 15)
        Me.Labelr18.TabIndex = 160
        Me.Labelr18.Text = "18"
        '
        'Labelr0
        '
        Me.Labelr0.AutoSize = True
        Me.Labelr0.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Labelr0.Location = New System.Drawing.Point(250, 144)
        Me.Labelr0.Name = "Labelr0"
        Me.Labelr0.Size = New System.Drawing.Size(19, 15)
        Me.Labelr0.TabIndex = 162
        Me.Labelr0.Text = "tah"
        '
        'txtPozn
        '
        Me.txtPozn.BackColor = System.Drawing.Color.White
        Me.txtPozn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPozn.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.txtPozn.Location = New System.Drawing.Point(10, 577)
        Me.txtPozn.Multiline = True
        Me.txtPozn.Name = "txtPozn"
        Me.txtPozn.Size = New System.Drawing.Size(527, 32)
        Me.txtPozn.TabIndex = 163
        Me.txtPozn.Text = "Pozn."
        '
        'radTah0
        '
        Me.radTah0.AutoSize = True
        Me.radTah0.BackColor = System.Drawing.Color.White
        Me.radTah0.Enabled = False
        Me.radTah0.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.radTah0.Location = New System.Drawing.Point(215, 161)
        Me.radTah0.Name = "radTah0"
        Me.radTah0.Size = New System.Drawing.Size(12, 11)
        Me.radTah0.TabIndex = 164
        Me.radTah0.UseVisualStyleBackColor = False
        '
        'RadTah1
        '
        Me.RadTah1.AutoSize = True
        Me.RadTah1.Enabled = False
        Me.RadTah1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadTah1.Location = New System.Drawing.Point(267, 161)
        Me.RadTah1.Name = "RadTah1"
        Me.RadTah1.Size = New System.Drawing.Size(12, 11)
        Me.RadTah1.TabIndex = 165
        Me.RadTah1.UseVisualStyleBackColor = True
        '
        'RadTah2
        '
        Me.RadTah2.AutoSize = True
        Me.RadTah2.Enabled = False
        Me.RadTah2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadTah2.Location = New System.Drawing.Point(285, 161)
        Me.RadTah2.Name = "RadTah2"
        Me.RadTah2.Size = New System.Drawing.Size(12, 11)
        Me.RadTah2.TabIndex = 166
        Me.RadTah2.UseVisualStyleBackColor = True
        '
        'RadTah3
        '
        Me.RadTah3.AutoSize = True
        Me.RadTah3.Enabled = False
        Me.RadTah3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadTah3.Location = New System.Drawing.Point(303, 161)
        Me.RadTah3.Name = "RadTah3"
        Me.RadTah3.Size = New System.Drawing.Size(12, 11)
        Me.RadTah3.TabIndex = 167
        Me.RadTah3.UseVisualStyleBackColor = True
        '
        'RadTah5
        '
        Me.RadTah5.AutoSize = True
        Me.RadTah5.Enabled = False
        Me.RadTah5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadTah5.Location = New System.Drawing.Point(321, 161)
        Me.RadTah5.Name = "RadTah5"
        Me.RadTah5.Size = New System.Drawing.Size(12, 11)
        Me.RadTah5.TabIndex = 168
        Me.RadTah5.UseVisualStyleBackColor = True
        '
        'RadTah6
        '
        Me.RadTah6.AutoSize = True
        Me.RadTah6.Enabled = False
        Me.RadTah6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadTah6.Location = New System.Drawing.Point(339, 161)
        Me.RadTah6.Name = "RadTah6"
        Me.RadTah6.Size = New System.Drawing.Size(12, 11)
        Me.RadTah6.TabIndex = 169
        Me.RadTah6.UseVisualStyleBackColor = True
        '
        'RadTah7
        '
        Me.RadTah7.AutoSize = True
        Me.RadTah7.Enabled = False
        Me.RadTah7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadTah7.Location = New System.Drawing.Point(357, 161)
        Me.RadTah7.Name = "RadTah7"
        Me.RadTah7.Size = New System.Drawing.Size(12, 11)
        Me.RadTah7.TabIndex = 170
        Me.RadTah7.UseVisualStyleBackColor = True
        '
        'RadTah8
        '
        Me.RadTah8.AutoSize = True
        Me.RadTah8.Enabled = False
        Me.RadTah8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadTah8.Location = New System.Drawing.Point(375, 161)
        Me.RadTah8.Name = "RadTah8"
        Me.RadTah8.Size = New System.Drawing.Size(12, 11)
        Me.RadTah8.TabIndex = 171
        Me.RadTah8.UseVisualStyleBackColor = True
        '
        'RadTah9
        '
        Me.RadTah9.AutoSize = True
        Me.RadTah9.Enabled = False
        Me.RadTah9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadTah9.Location = New System.Drawing.Point(393, 161)
        Me.RadTah9.Name = "RadTah9"
        Me.RadTah9.Size = New System.Drawing.Size(12, 11)
        Me.RadTah9.TabIndex = 172
        Me.RadTah9.UseVisualStyleBackColor = True
        '
        'RadTah10
        '
        Me.RadTah10.AutoSize = True
        Me.RadTah10.Enabled = False
        Me.RadTah10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadTah10.Location = New System.Drawing.Point(411, 161)
        Me.RadTah10.Name = "RadTah10"
        Me.RadTah10.Size = New System.Drawing.Size(12, 11)
        Me.RadTah10.TabIndex = 173
        Me.RadTah10.UseVisualStyleBackColor = True
        '
        'RadTah12
        '
        Me.RadTah12.AutoSize = True
        Me.RadTah12.Enabled = False
        Me.RadTah12.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadTah12.Location = New System.Drawing.Point(429, 161)
        Me.RadTah12.Name = "RadTah12"
        Me.RadTah12.Size = New System.Drawing.Size(12, 11)
        Me.RadTah12.TabIndex = 174
        Me.RadTah12.UseVisualStyleBackColor = True
        '
        'RadTah13
        '
        Me.RadTah13.AutoSize = True
        Me.RadTah13.Enabled = False
        Me.RadTah13.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadTah13.Location = New System.Drawing.Point(447, 161)
        Me.RadTah13.Name = "RadTah13"
        Me.RadTah13.Size = New System.Drawing.Size(12, 11)
        Me.RadTah13.TabIndex = 175
        Me.RadTah13.UseVisualStyleBackColor = True
        '
        'RadTah14
        '
        Me.RadTah14.AutoSize = True
        Me.RadTah14.Enabled = False
        Me.RadTah14.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadTah14.Location = New System.Drawing.Point(465, 161)
        Me.RadTah14.Name = "RadTah14"
        Me.RadTah14.Size = New System.Drawing.Size(12, 11)
        Me.RadTah14.TabIndex = 176
        Me.RadTah14.UseVisualStyleBackColor = True
        '
        'RadTah15
        '
        Me.RadTah15.AutoSize = True
        Me.RadTah15.Enabled = False
        Me.RadTah15.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadTah15.Location = New System.Drawing.Point(483, 161)
        Me.RadTah15.Name = "RadTah15"
        Me.RadTah15.Size = New System.Drawing.Size(12, 11)
        Me.RadTah15.TabIndex = 177
        Me.RadTah15.UseVisualStyleBackColor = True
        '
        'RadTah17
        '
        Me.RadTah17.AutoSize = True
        Me.RadTah17.Enabled = False
        Me.RadTah17.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadTah17.Location = New System.Drawing.Point(501, 161)
        Me.RadTah17.Name = "RadTah17"
        Me.RadTah17.Size = New System.Drawing.Size(12, 11)
        Me.RadTah17.TabIndex = 178
        Me.RadTah17.UseVisualStyleBackColor = True
        '
        'RadTah18
        '
        Me.RadTah18.AutoSize = True
        Me.RadTah18.Enabled = False
        Me.RadTah18.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadTah18.Location = New System.Drawing.Point(519, 161)
        Me.RadTah18.Name = "RadTah18"
        Me.RadTah18.Size = New System.Drawing.Size(12, 11)
        Me.RadTah18.TabIndex = 179
        Me.RadTah18.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Button1.Location = New System.Drawing.Point(202, 440)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(58, 26)
        Me.Button1.TabIndex = 182
        Me.Button1.Text = "elipsa"
        '
        'Button2
        '
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Button2.Location = New System.Drawing.Point(10, 440)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(58, 26)
        Me.Button2.TabIndex = 183
        Me.Button2.Text = "linka"
        '
        'Button3
        '
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Button3.Location = New System.Drawing.Point(74, 440)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(58, 26)
        Me.Button3.TabIndex = 184
        Me.Button3.Text = "čára"
        '
        'Button4
        '
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Button4.Location = New System.Drawing.Point(138, 440)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(58, 26)
        Me.Button4.TabIndex = 185
        Me.Button4.Text = "čtyřúhel"
        '
        'Button5
        '
        Me.Button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Button5.Location = New System.Drawing.Point(266, 440)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(58, 26)
        Me.Button5.TabIndex = 186
        Me.Button5.Text = "dráha"
        '
        'Button7
        '
        Me.Button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button7.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Button7.Location = New System.Drawing.Point(330, 440)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(58, 26)
        Me.Button7.TabIndex = 187
        Me.Button7.Text = "text"
        '
        'Button6
        '
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Button6.Location = New System.Drawing.Point(394, 440)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(58, 26)
        Me.Button6.TabIndex = 188
        Me.Button6.Text = "guma"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Panel3)
        Me.GroupBox1.Controls.Add(Me.Panel2)
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(10, 470)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(134, 54)
        Me.GroupBox1.TabIndex = 189
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "styl"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.AliceBlue
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Location = New System.Drawing.Point(92, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(32, 24)
        Me.Panel3.TabIndex = 15
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.AliceBlue
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Location = New System.Drawing.Point(54, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(32, 24)
        Me.Panel2.TabIndex = 14
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Control
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Location = New System.Drawing.Point(16, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(32, 24)
        Me.Panel1.TabIndex = 13
        '
        'Label1k
        '
        Me.Label1k.AutoSize = True
        Me.Label1k.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label1k.Location = New System.Drawing.Point(411, 493)
        Me.Label1k.Name = "Label1k"
        Me.Label1k.Size = New System.Drawing.Size(29, 14)
        Me.Label1k.TabIndex = 191
        Me.Label1k.Text = "síla :"
        '
        'TrackBar1
        '
        Me.TrackBar1.Enabled = False
        Me.TrackBar1.LargeChange = 1
        Me.TrackBar1.Location = New System.Drawing.Point(446, 490)
        Me.TrackBar1.Maximum = 20
        Me.TrackBar1.Minimum = 1
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(64, 45)
        Me.TrackBar1.TabIndex = 192
        Me.TrackBar1.TickStyle = System.Windows.Forms.TickStyle.None
        Me.TrackBar1.Value = 1
        '
        'TextBox2k
        '
        Me.TextBox2k.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox2k.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.TextBox2k.Location = New System.Drawing.Point(513, 491)
        Me.TextBox2k.Name = "TextBox2k"
        Me.TextBox2k.Size = New System.Drawing.Size(24, 20)
        Me.TextBox2k.TabIndex = 193
        Me.TextBox2k.Text = "1"
        '
        'Label2k
        '
        Me.Label2k.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2k.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label2k.Location = New System.Drawing.Point(372, 408)
        Me.Label2k.Name = "Label2k"
        Me.Label2k.Size = New System.Drawing.Size(165, 26)
        Me.Label2k.TabIndex = 194
        Me.Label2k.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.Label2k.Visible = False
        '
        'radBalk
        '
        Me.radBalk.AutoSize = True
        Me.radBalk.BackColor = System.Drawing.Color.White
        Me.radBalk.Enabled = False
        Me.radBalk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.radBalk.Location = New System.Drawing.Point(158, 161)
        Me.radBalk.Name = "radBalk"
        Me.radBalk.Size = New System.Drawing.Size(12, 11)
        Me.radBalk.TabIndex = 200
        Me.radBalk.UseVisualStyleBackColor = False
        '
        'Labelr19
        '
        Me.Labelr19.AutoSize = True
        Me.Labelr19.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Labelr19.Location = New System.Drawing.Point(144, 144)
        Me.Labelr19.Name = "Labelr19"
        Me.Labelr19.Size = New System.Drawing.Size(47, 15)
        Me.Labelr19.TabIndex = 201
        Me.Labelr19.Text = "z balkonu"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label19.Location = New System.Drawing.Point(202, 144)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(42, 15)
        Me.Label19.TabIndex = 202
        Me.Label19.Text = "z 1.řady"
        '
        'pbSiluet
        '
        Me.pbSiluet.BackColor = System.Drawing.Color.Transparent
        Me.pbSiluet.Cursor = System.Windows.Forms.Cursors.Default
        Me.pbSiluet.Enabled = False
        Me.pbSiluet.Image = Global.tahy.My.Resources.Resources.obraz
        Me.pbSiluet.Location = New System.Drawing.Point(592, 402)
        Me.pbSiluet.Name = "pbSiluet"
        Me.pbSiluet.Size = New System.Drawing.Size(27, 83)
        Me.pbSiluet.TabIndex = 203
        Me.pbSiluet.TabStop = False
        Me.pbSiluet.Visible = False
        '
        'pbox
        '
        Me.pbox.ErrorImage = Nothing
        Me.pbox.Image = Global.tahy.My.Resources.Resources.rezpruh
        Me.pbox.InitialImage = Nothing
        Me.pbox.Location = New System.Drawing.Point(546, 621)
        Me.pbox.Name = "pbox"
        Me.pbox.Size = New System.Drawing.Size(22, 21)
        Me.pbox.TabIndex = 198
        Me.pbox.TabStop = False
        Me.pbox.Visible = False
        '
        'pbTah1
        '
        Me.pbTah1.BackColor = System.Drawing.Color.White
        Me.pbTah1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pbTah1.ErrorImage = Nothing
        Me.pbTah1.Image = CType(resources.GetObject("pbTah1.Image"), System.Drawing.Image)
        Me.pbTah1.InitialImage = Nothing
        Me.pbTah1.Location = New System.Drawing.Point(830, 28)
        Me.pbTah1.Name = "pbTah1"
        Me.pbTah1.Size = New System.Drawing.Size(10, 17)
        Me.pbTah1.TabIndex = 63
        Me.pbTah1.TabStop = False
        '
        'PaletteBox
        '
        Me.PaletteBox.BackColor = System.Drawing.Color.White
        Me.PaletteBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PaletteBox.Enabled = False
        Me.PaletteBox.Location = New System.Drawing.Point(151, 491)
        Me.PaletteBox.Name = "PaletteBox"
        Me.PaletteBox.Size = New System.Drawing.Size(200, 22)
        Me.PaletteBox.TabIndex = 190
        Me.PaletteBox.TabStop = False
        Me.PaletteBox.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.LightGray
        Me.PictureBox1.Enabled = False
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(47, 18)
        Me.PictureBox1.TabIndex = 119
        Me.PictureBox1.TabStop = False
        Me.PictureBox1.Visible = False
        '
        'pbTah5
        '
        Me.pbTah5.BackColor = System.Drawing.Color.White
        Me.pbTah5.Location = New System.Drawing.Point(895, 28)
        Me.pbTah5.Name = "pbTah5"
        Me.pbTah5.Size = New System.Drawing.Size(10, 17)
        Me.pbTah5.TabIndex = 79
        Me.pbTah5.TabStop = False
        '
        'pbTah18
        '
        Me.pbTah18.BackColor = System.Drawing.Color.White
        Me.pbTah18.Location = New System.Drawing.Point(1106, 28)
        Me.pbTah18.Name = "pbTah18"
        Me.pbTah18.Size = New System.Drawing.Size(10, 17)
        Me.pbTah18.TabIndex = 78
        Me.pbTah18.TabStop = False
        '
        'pbTah17
        '
        Me.pbTah17.BackColor = System.Drawing.Color.White
        Me.pbTah17.Location = New System.Drawing.Point(1092, 28)
        Me.pbTah17.Name = "pbTah17"
        Me.pbTah17.Size = New System.Drawing.Size(10, 17)
        Me.pbTah17.TabIndex = 77
        Me.pbTah17.TabStop = False
        '
        'pbTah15
        '
        Me.pbTah15.BackColor = System.Drawing.Color.White
        Me.pbTah15.Location = New System.Drawing.Point(1065, 28)
        Me.pbTah15.Name = "pbTah15"
        Me.pbTah15.Size = New System.Drawing.Size(10, 17)
        Me.pbTah15.TabIndex = 76
        Me.pbTah15.TabStop = False
        '
        'pbTah14
        '
        Me.pbTah14.BackColor = System.Drawing.Color.White
        Me.pbTah14.Location = New System.Drawing.Point(1027, 28)
        Me.pbTah14.Name = "pbTah14"
        Me.pbTah14.Size = New System.Drawing.Size(10, 17)
        Me.pbTah14.TabIndex = 75
        Me.pbTah14.TabStop = False
        '
        'pbTah13
        '
        Me.pbTah13.BackColor = System.Drawing.Color.White
        Me.pbTah13.Location = New System.Drawing.Point(1013, 28)
        Me.pbTah13.Name = "pbTah13"
        Me.pbTah13.Size = New System.Drawing.Size(10, 17)
        Me.pbTah13.TabIndex = 74
        Me.pbTah13.TabStop = False
        '
        'pbTah12
        '
        Me.pbTah12.BackColor = System.Drawing.Color.White
        Me.pbTah12.Location = New System.Drawing.Point(999, 28)
        Me.pbTah12.Name = "pbTah12"
        Me.pbTah12.Size = New System.Drawing.Size(10, 17)
        Me.pbTah12.TabIndex = 73
        Me.pbTah12.TabStop = False
        '
        'pbTah10
        '
        Me.pbTah10.BackColor = System.Drawing.Color.White
        Me.pbTah10.Location = New System.Drawing.Point(966, 28)
        Me.pbTah10.Name = "pbTah10"
        Me.pbTah10.Size = New System.Drawing.Size(10, 17)
        Me.pbTah10.TabIndex = 72
        Me.pbTah10.TabStop = False
        '
        'pbTah9
        '
        Me.pbTah9.BackColor = System.Drawing.Color.White
        Me.pbTah9.Location = New System.Drawing.Point(952, 28)
        Me.pbTah9.Name = "pbTah9"
        Me.pbTah9.Size = New System.Drawing.Size(10, 17)
        Me.pbTah9.TabIndex = 71
        Me.pbTah9.TabStop = False
        '
        'pbTah8
        '
        Me.pbTah8.BackColor = System.Drawing.Color.White
        Me.pbTah8.Location = New System.Drawing.Point(938, 28)
        Me.pbTah8.Name = "pbTah8"
        Me.pbTah8.Size = New System.Drawing.Size(10, 17)
        Me.pbTah8.TabIndex = 70
        Me.pbTah8.TabStop = False
        '
        'pbTah7
        '
        Me.pbTah7.BackColor = System.Drawing.Color.White
        Me.pbTah7.Location = New System.Drawing.Point(924, 28)
        Me.pbTah7.Name = "pbTah7"
        Me.pbTah7.Size = New System.Drawing.Size(10, 17)
        Me.pbTah7.TabIndex = 69
        Me.pbTah7.TabStop = False
        '
        'pbTah6
        '
        Me.pbTah6.BackColor = System.Drawing.Color.White
        Me.pbTah6.Location = New System.Drawing.Point(909, 28)
        Me.pbTah6.Name = "pbTah6"
        Me.pbTah6.Size = New System.Drawing.Size(10, 17)
        Me.pbTah6.TabIndex = 68
        Me.pbTah6.TabStop = False
        '
        'pbTah3
        '
        Me.pbTah3.BackColor = System.Drawing.Color.White
        Me.pbTah3.Location = New System.Drawing.Point(858, 28)
        Me.pbTah3.Name = "pbTah3"
        Me.pbTah3.Size = New System.Drawing.Size(10, 17)
        Me.pbTah3.TabIndex = 65
        Me.pbTah3.TabStop = False
        '
        'pb1
        '
        Me.pb1.BackColor = System.Drawing.Color.White
        Me.pb1.ErrorImage = Nothing
        Me.pb1.Image = CType(resources.GetObject("pb1.Image"), System.Drawing.Image)
        Me.pb1.InitialImage = CType(resources.GetObject("pb1.InitialImage"), System.Drawing.Image)
        Me.pb1.Location = New System.Drawing.Point(546, 22)
        Me.pb1.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.pb1.Name = "pb1"
        Me.pb1.Size = New System.Drawing.Size(706, 620)
        Me.pb1.TabIndex = 25
        Me.pb1.TabStop = False
        '
        'btnSiluet
        '
        Me.btnSiluet.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSiluet.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnSiluet.Location = New System.Drawing.Point(479, 440)
        Me.btnSiluet.Name = "btnSiluet"
        Me.btnSiluet.Size = New System.Drawing.Size(58, 26)
        Me.btnSiluet.TabIndex = 204
        Me.btnSiluet.Text = "silueta"
        '
        'PanelZakryj
        '
        Me.PanelZakryj.Location = New System.Drawing.Point(60, 4)
        Me.PanelZakryj.Name = "PanelZakryj"
        Me.PanelZakryj.Size = New System.Drawing.Size(23, 13)
        Me.PanelZakryj.TabIndex = 206
        Me.PanelZakryj.Visible = False
        '
        'btnPrint
        '
        Me.btnPrint.Enabled = False
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.btnPrint.Location = New System.Drawing.Point(109, 545)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(93, 26)
        Me.btnPrint.TabIndex = 207
        Me.btnPrint.Text = "tisknout"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'Label2p
        '
        Me.Label2p.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2p.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label2p.Location = New System.Drawing.Point(128, 408)
        Me.Label2p.Name = "Label2p"
        Me.Label2p.Size = New System.Drawing.Size(165, 26)
        Me.Label2p.TabIndex = 208
        Me.Label2p.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label20.Location = New System.Drawing.Point(303, 416)
        Me.Label20.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(66, 14)
        Me.Label20.TabIndex = 209
        Me.Label20.Text = "od podlahy :"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(238, Byte))
        Me.Label21.Location = New System.Drawing.Point(61, 416)
        Me.Label21.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(64, 14)
        Me.Label21.TabIndex = 210
        Me.Label21.Text = "od portálu : "
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(1264, 649)
        Me.Controls.Add(Me.PanelZakryj)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.btnSiluet)
        Me.Controls.Add(Me.pbSiluet)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.Labelr19)
        Me.Controls.Add(Me.radBalk)
        Me.Controls.Add(Me.pbox)
        Me.Controls.Add(Me.pbTah1)
        Me.Controls.Add(Me.TextBox2k)
        Me.Controls.Add(Me.TrackBar1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label1k)
        Me.Controls.Add(Me.PaletteBox)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button7)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtPozn)
        Me.Controls.Add(Me.txtSitua)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.txtInsc)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.btnMensi)
        Me.Controls.Add(Me.btnVetsi)
        Me.Controls.Add(Me.btnOtevritScreen)
        Me.Controls.Add(Me.ListBox8)
        Me.Controls.Add(Me.ListBoxOdpo)
        Me.Controls.Add(Me.ListBoxUvaz)
        Me.Controls.Add(Me.ListBoxDek)
        Me.Controls.Add(Me.btnUlozScreen)
        Me.Controls.Add(Me.btnVypnout)
        Me.Controls.Add(Me.ListBoxInfo)
        Me.Controls.Add(Me.btnVymazVse)
        Me.Controls.Add(Me.btnZobrazTahy)
        Me.Controls.Add(Me.ListBox5)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBoxVyber)
        Me.Controls.Add(Me.pbTah5)
        Me.Controls.Add(Me.pbTah18)
        Me.Controls.Add(Me.pbTah17)
        Me.Controls.Add(Me.pbTah15)
        Me.Controls.Add(Me.pbTah14)
        Me.Controls.Add(Me.pbTah13)
        Me.Controls.Add(Me.pbTah12)
        Me.Controls.Add(Me.pbTah10)
        Me.Controls.Add(Me.pbTah9)
        Me.Controls.Add(Me.pbTah8)
        Me.Controls.Add(Me.pbTah7)
        Me.Controls.Add(Me.pbTah6)
        Me.Controls.Add(Me.pbTah3)
        Me.Controls.Add(Me.pbTah2)
        Me.Controls.Add(Me.ListBox7)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.btnZavesit)
        Me.Controls.Add(Me.ListBox6)
        Me.Controls.Add(Me.btnPosun1)
        Me.Controls.Add(Me.ListBox4)
        Me.Controls.Add(Me.ListBox3)
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.btnUvrat)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnOdvesit)
        Me.Controls.Add(Me.btnlVidit)
        Me.Controls.Add(Me.Labelr0)
        Me.Controls.Add(Me.Labelr1)
        Me.Controls.Add(Me.Labelr2)
        Me.Controls.Add(Me.Labelr3)
        Me.Controls.Add(Me.Labelr5)
        Me.Controls.Add(Me.Labelr6)
        Me.Controls.Add(Me.Labelr7)
        Me.Controls.Add(Me.Labelr8)
        Me.Controls.Add(Me.Labelr9)
        Me.Controls.Add(Me.Labelr10)
        Me.Controls.Add(Me.Labelr12)
        Me.Controls.Add(Me.Labelr13)
        Me.Controls.Add(Me.Labelr14)
        Me.Controls.Add(Me.Labelr15)
        Me.Controls.Add(Me.Labelr17)
        Me.Controls.Add(Me.Labelr18)
        Me.Controls.Add(Me.radTah0)
        Me.Controls.Add(Me.RadTah1)
        Me.Controls.Add(Me.RadTah2)
        Me.Controls.Add(Me.RadTah3)
        Me.Controls.Add(Me.RadTah5)
        Me.Controls.Add(Me.RadTah6)
        Me.Controls.Add(Me.RadTah7)
        Me.Controls.Add(Me.RadTah8)
        Me.Controls.Add(Me.RadTah9)
        Me.Controls.Add(Me.RadTah10)
        Me.Controls.Add(Me.RadTah12)
        Me.Controls.Add(Me.RadTah13)
        Me.Controls.Add(Me.RadTah14)
        Me.Controls.Add(Me.RadTah15)
        Me.Controls.Add(Me.RadTah17)
        Me.Controls.Add(Me.RadTah18)
        Me.Controls.Add(Me.pb1)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.Label2k)
        Me.Controls.Add(Me.Label2p)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2, 3, 2, 3)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " "
        Me.TopMost = True
        CType(Me.pbTah2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbSiluet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbTah1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PaletteBox, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbTah5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbTah18, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbTah17, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbTah15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbTah14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbTah13, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbTah12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbTah10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbTah9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbTah8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbTah7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbTah6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbTah3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region
    '........kreslení
    Private ColorMap As Bitmap
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim Form1 As System.Drawing.Graphics
        ComboBoxVyber.SelectedIndex = 0
        Me.KeyPreview = True 'kvůli ESC
        '.........................odkrýt ovládací prvky
        PanelZakryj.Visible = True
        PanelZakryj.Height = 532
        PanelZakryj.Width = 540
        PanelZakryj.Location = New Point(10, 45)

    End Sub
    Private Sub btnZavesit_Click(sender As Object, e As EventArgs) Handles btnZavesit.Click
        '...................omezení výšky tahové věže
        Dim Dek As Integer
        Dim Uva As Integer
        Dek = TextBox1.Text
        Uva = TextBox2.Text
        If (Dek + Uva) > 914 Then
            ' MsgBox(Dek + Uva & "tahová věž má výšku pouze 914 cm")
            MsgBox("tahová věž má výšku pouze 914 cm")
            Exit Sub
        End If
        '........................................

        pbTah.Refresh()

        If TextBox1.Text = 0 Or TextBox1.Text > 700 Then
            MsgBox("zadejte výšku dekorace nebo výšku sufity (150cm)")
            TextBox2.Text = 0
        End If
        '.....................
        If TextBox1.Text = Nothing Then
            MsgBox("zadejte výšku dekorace nebo výšku sufity (150cm)")
            Exit Sub
        End If
        If TextBox2.Text = Nothing Then
            MsgBox("zadejte výšku délku úvazku")
            Exit Sub
        End If
        If TextBox4.Text = Nothing Then
            MsgBox("zadejte výšku zvednutí-spuštění")
            Exit Sub
        End If
        '.....................

        x1 = 5   'x  tah v pb
        y1 = 454 - ((TextBox1.Text) * 0.479) 'vrch kulisy v pb
        x2 = 5  'x  tah v pb    
        y2 = 454  'y úroveň podlahy v pb  
        '...................................'výška pbTah 'VVVV
        Dim TahV As Integer
        Dim objSelectedItem As Object
        objSelectedItem = ComboBoxVyber.SelectedItem
        TahV = objSelectedItem.ToString()
        Select Case TahV
            Case 1
                pbTah1.Height = y2 ' určí výšku pb
                GraphicsFun = pbTah1.CreateGraphics
                pbTah = pbTah1
                pbTah1.Refresh()
                '........
                vDek1 = TextBox1.Text   '1111
                vUva1 = TextBox2.Text
                vPod1 = TextBox4.Text
            Case 2
                pbTah2.Height = y2 ' určí výšku pb
                GraphicsFun = pbTah2.CreateGraphics
                pbTah = pbTah2
                pbTah2.Refresh()
                '...............
                vDek2 = TextBox1.Text   '1111
                vUva2 = TextBox2.Text
                vPod2 = TextBox4.Text
            Case 3
                pbTah3.Height = y2 ' určí výšku pb
                GraphicsFun = pbTah3.CreateGraphics
                pbTah = pbTah3
                pbTah3.Refresh()
                '...............
                vDek3 = TextBox1.Text   '1111
                vUva3 = TextBox2.Text
                vPod3 = TextBox4.Text
            Case 5
                pbTah5.Height = y2 ' určí výšku pb
                GraphicsFun = pbTah5.CreateGraphics
                pbTah = pbTah5
                pbTah5.Refresh()
                '...............
                vDek5 = TextBox1.Text   '1111
                vUva5 = TextBox2.Text
                vPod5 = TextBox4.Text
            Case 6
                pbTah6.Height = y2 ' určí výšku pb
                GraphicsFun = pbTah6.CreateGraphics
                pbTah = pbTah6
                pbTah6.Refresh()
                '...............
                vDek6 = TextBox1.Text   '1111
                vUva6 = TextBox2.Text
                vPod6 = TextBox4.Text
            Case 7
                pbTah7.Height = y2 ' určí výšku pb
                GraphicsFun = pbTah7.CreateGraphics
                pbTah = pbTah7
                pbTah7.Refresh()
                '...............
                vDek7 = TextBox1.Text   '1111
                vUva7 = TextBox2.Text
                vPod7 = TextBox4.Text
            Case 8
                pbTah8.Height = y2 ' určí výšku pb
                GraphicsFun = pbTah8.CreateGraphics
                pbTah = pbTah8
                pbTah8.Refresh()
                '...............
                vDek8 = TextBox1.Text   '1111
                vUva8 = TextBox2.Text
                vPod8 = TextBox4.Text
            Case 9
                pbTah9.Height = y2 ' určí výšku pb
                GraphicsFun = pbTah9.CreateGraphics
                pbTah = pbTah9
                pbTah9.Refresh()
                '...............
                vDek9 = TextBox1.Text   '1111
                vUva9 = TextBox2.Text
                vPod9 = TextBox4.Text
            Case 10
                pbTah10.Height = y2 ' určí výšku pb
                GraphicsFun = pbTah10.CreateGraphics
                pbTah = pbTah10
                pbTah10.Refresh()
                '...............
                vDek10 = TextBox1.Text   '1111
                vUva10 = TextBox2.Text
                vPod10 = TextBox4.Text
            Case 12
                pbTah12.Height = y2 ' určí výšku pb
                GraphicsFun = pbTah12.CreateGraphics
                pbTah = pbTah12
                pbTah12.Refresh()
                '...............
                vDek12 = TextBox1.Text   '1111
                vUva12 = TextBox2.Text
                vPod12 = TextBox4.Text
            Case 13
                pbTah13.Height = y2 ' určí výšku pb
                GraphicsFun = pbTah13.CreateGraphics
                pbTah = pbTah13
                pbTah13.Refresh()
                '...............
                vDek13 = TextBox1.Text   '1111
                vUva13 = TextBox2.Text
                vPod13 = TextBox4.Text
            Case 14
                pbTah14.Height = y2 ' určí výšku pb
                GraphicsFun = pbTah14.CreateGraphics
                pbTah = pbTah14
                pbTah14.Refresh()
                '...............
                vDek14 = TextBox1.Text   '1111
                vUva14 = TextBox2.Text
                vPod14 = TextBox4.Text
            Case 15
                pbTah15.Height = y2 ' určí výšku pb
                GraphicsFun = pbTah15.CreateGraphics
                pbTah = pbTah15
                pbTah15.Refresh()
                '...............
                vDek15 = TextBox1.Text   '1111
                vUva15 = TextBox2.Text
                vPod15 = TextBox4.Text
            Case 17
                pbTah17.Height = y2 ' určí výšku pb
                GraphicsFun = pbTah17.CreateGraphics
                pbTah = pbTah17
                pbTah17.Refresh()
                '...............
                vDek17 = TextBox1.Text   '1111
                vUva17 = TextBox2.Text
                vPod17 = TextBox4.Text
            Case 18
                pbTah18.Height = y2 ' určí výšku pb
                GraphicsFun = pbTah18.CreateGraphics
                pbTah = pbTah18
                pbTah18.Refresh()
                '...............
                vDek18 = TextBox1.Text   '1111
                vUva18 = TextBox2.Text
                vPod18 = TextBox4.Text
        End Select
        '...........................
        GraphicsFun.DrawLine(myPenTah, x1, y1, x2, y2) 'namaluje tah
        If TextBox2.Text = 0 Then
            GraphicsFun.DrawEllipse(myPenTyc, 3, y1 - 3, 3, 3) 'namaluje tahovou tyč1,když nemá úvazek
            GraphicsFun.DrawLine(myPenLano, x1, 0, x2, y1 - 3) 'namaluje lanko
        End If

        If TextBox2.Text <> 0 Then
            x1u = 5  'x  tah od leva v pb
            y1u = (y1 - ((TextBox2.Text) * 0.479)) 'vrch úvazku
            x2u = 5  'x  tah od leva v pb    
            y2u = (y2 - (TextBox1.Text) * 0.479)   'vrch kulisy,spodek úvazku
            GraphicsFun.DrawLine(myPenUvaz, x1u, y1u, x2u, y2u) 'namaluje úvazek
            GraphicsFun.DrawEllipse(myPenTyc, 3, y1u - 3, 3, 3) 'namaluje na ní tahovou tyč
            GraphicsFun.DrawLine(myPenLano, x1, 0, x2, y1u - 3) 'namaluje lanko
        End If
        TextBox3.Text = 0
        TextBox4.Text = 0
        '....................................zapíše info řádek
        Dim Vyber As Integer
        Dim objSelectedItemVyber As Object
        objSelectedItemVyber = ComboBoxVyber.SelectedItem
        Vyber = objSelectedItemVyber.ToString()

        Select Case Vyber
            Case 1
                ListBoxInfo.SelectedIndex = 0
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 1" & "     výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah1.BackColor = System.Drawing.Color.White
            Case 2
                ListBoxInfo.SelectedIndex = 1
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 2" & "     výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah2.BackColor = System.Drawing.Color.White
            Case 3
                ListBoxInfo.SelectedIndex = 2
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 3" & "     výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah3.BackColor = System.Drawing.Color.White
            Case 5
                ListBoxInfo.SelectedIndex = 3
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 5" & "     výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah5.BackColor = System.Drawing.Color.White
            Case 6
                ListBoxInfo.SelectedIndex = 4
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 6" & "     výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah6.BackColor = System.Drawing.Color.White
            Case 7
                ListBoxInfo.SelectedIndex = 5
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 7" & "     výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah7.BackColor = System.Drawing.Color.White
            Case 8
                ListBoxInfo.SelectedIndex = 6
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 8" & "     výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah8.BackColor = System.Drawing.Color.White
            Case 9
                ListBoxInfo.SelectedIndex = 7
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 9" & "     výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah9.BackColor = System.Drawing.Color.White
            Case 10
                ListBoxInfo.SelectedIndex = 8
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 10" & "   výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah10.BackColor = System.Drawing.Color.White
            Case 12
                ListBoxInfo.SelectedIndex = 9
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 12" & "   výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah12.BackColor = System.Drawing.Color.White
            Case 13
                ListBoxInfo.SelectedIndex = 10
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 13" & "   výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah13.BackColor = System.Drawing.Color.White
            Case 14
                ListBoxInfo.SelectedIndex = 11
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 14" & "   výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah14.BackColor = System.Drawing.Color.White
            Case 15
                ListBoxInfo.SelectedIndex = 12
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 15" & "   výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah15.BackColor = System.Drawing.Color.White
            Case 17
                ListBoxInfo.SelectedIndex = 13
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 17" & "   výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah17.BackColor = System.Drawing.Color.White
            Case 18
                ListBoxInfo.SelectedIndex = 14
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 18" & "   výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah18.BackColor = System.Drawing.Color.White
        End Select
        '............. (cm)od podlahy
        ListBox8.Visible = False


        'psaní  limit délky úvazku
        '..............
        On Error Resume Next
        ListBoxUvaz.Visible = True
        ListBoxUvaz.Items.Clear()
        Dim vU As Single
        vU = (914 - TextBox1.Text) ' - TextBox2.Text
        ListBoxUvaz.Items.Add("max." & vU & "cm")
        On Error Resume Next
        '........................

        On Error Resume Next
    End Sub
    Private Sub btnOdvesit_Click(sender As Object, e As EventArgs) Handles btnOdvesit.Click
        '.....................
        If TextBox1.Text = Nothing Then
            MsgBox("zadejte výšku dekorace nebo výšku sufity (150cm)")
            Exit Sub
        End If
        If TextBox2.Text = Nothing Then
            MsgBox("zadejte výšku délku úvazku")
            Exit Sub
        End If
        If TextBox4.Text = Nothing Then
            MsgBox("zadejte výšku zvednutí-spuštění")
            Exit Sub
        End If
        '.....................
        pbTah.Refresh()
        TextBox1.Text = 0
        TextBox2.Text = 0
        TextBox3.Text = 0
        TextBox4.Text = 0
        '......................... 
        pbTah.Refresh()
        pbTah.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13) 'namaluje tah nahoře
        pbTah.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)

        '....................................smaže info řádek
        Dim Odves As Integer
        Dim objSelectedItemVyber As Object
        objSelectedItemVyber = ComboBoxVyber.SelectedItem
        Odves = objSelectedItemVyber.ToString()

        Select Case Odves
            Case 1
                pbTah1.Height = 17
                ListBoxInfo.SelectedIndex = 0
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 1      "
                ListBoxInfo.SelectedIndex = -1
                RadTah1.BackColor = System.Drawing.Color.White
                '......................překryje původní čáru  čáru bílou 2222
                On Error Resume Next
                g2.DrawLine(myPenKrycTah1, puvZacXTah1, puvZacYTah1, puvKonXTah1, puvKonYTah1)
                On Error Resume Next
                '......................překryje novou čáru jako prodloužení bílou 
                DpuvKonXTah1 = puvKonXTah1
                DpuvKonXTah1 = DpuvKonXTah1 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah1))

                DpuvKonYTah1 = puvKonYTah1
                DpuvKonYTah1 = DpuvKonYTah1 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah1))
                novKonXTah1 = DpuvKonXTah1
                novKonYTah1 = DpuvKonYTah1
                On Error Resume Next
                g2.DrawLine(myPenKrycTah1, puvKonXTah1, puvKonYTah1, novKonXTah1, novKonYTah1)
                On Error Resume Next

            '.................................
            Case 2
                pbTah2.Height = 17
                ListBoxInfo.SelectedIndex = 1
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 2      "
                ListBoxInfo.SelectedIndex = -1
                RadTah2.BackColor = System.Drawing.Color.White
                '......................překryje původní čáru  čáru bílou
                On Error Resume Next
                g2.DrawLine(myPenKrycTah2, puvZacXTah2, puvZacYTah2, puvKonXTah2, puvKonYTah2)
                On Error Resume Next
                '......................překryje novou čáru jako prodloužení bílou
                DpuvKonXTah2 = puvKonXTah2
                DpuvKonXTah2 = DpuvKonXTah2 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah2))

                DpuvKonYTah2 = puvKonYTah2
                DpuvKonYTah2 = DpuvKonYTah2 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah2))
                novKonXTah2 = DpuvKonXTah2
                novKonYTah2 = DpuvKonYTah2
                On Error Resume Next
                g2.DrawLine(myPenKrycTah2, puvKonXTah2, puvKonYTah2, novKonXTah2, novKonYTah2)
                On Error Resume Next
            '...........
            Case 3
                pbTah3.Height = 17
                ListBoxInfo.SelectedIndex = 2
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 3      "
                ListBoxInfo.SelectedIndex = -1
                RadTah3.BackColor = System.Drawing.Color.White
                '......................překryje původní čáru  čáru bílou
                On Error Resume Next
                g2.DrawLine(myPenKrycTah3, puvZacXTah3, puvZacYTah3, puvKonXTah3, puvKonYTah3)
                On Error Resume Next
                '......................překryje novou čáru jako prodloužení bílou
                DpuvKonXTah3 = puvKonXTah3
                DpuvKonXTah3 = DpuvKonXTah3 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah3))

                DpuvKonYTah3 = puvKonYTah3
                DpuvKonYTah3 = DpuvKonYTah3 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah3))
                novKonXTah3 = DpuvKonXTah3
                novKonYTah3 = DpuvKonYTah3
                On Error Resume Next
                g2.DrawLine(myPenKrycTah3, puvKonXTah3, puvKonYTah3, novKonXTah3, novKonYTah3)
                On Error Resume Next
            '........
            Case 5
                pbTah5.Height = 17
                ListBoxInfo.SelectedIndex = 3
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 5      "
                ListBoxInfo.SelectedIndex = -1
                RadTah5.BackColor = System.Drawing.Color.White
                '......................překryje původní čáru  čáru bílou
                On Error Resume Next
                g2.DrawLine(myPenKrycTah5, puvZacXTah5, puvZacYTah5, puvKonXTah5, puvKonYTah5)
                On Error Resume Next
                '......................překryje novou čáru jako prodloužení bílou
                DpuvKonXTah5 = puvKonXTah5
                DpuvKonXTah5 = DpuvKonXTah5 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah5))

                DpuvKonYTah5 = puvKonYTah5
                DpuvKonYTah5 = DpuvKonYTah5 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah5))
                novKonXTah5 = DpuvKonXTah5
                novKonYTah5 = DpuvKonYTah5
                g2.DrawLine(myPenKrycTah5, puvKonXTah5, puvKonYTah5, novKonXTah5, novKonYTah5)
            '................
            Case 6
                pbTah6.Height = 17
                ListBoxInfo.SelectedIndex = 4
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 6      "
                ListBoxInfo.SelectedIndex = -1
                RadTah6.BackColor = System.Drawing.Color.White
                '......................překryje původní čáru  čáru bílou
                On Error Resume Next
                g2.DrawLine(myPenKrycTah6, puvZacXTah6, puvZacYTah6, puvKonXTah6, puvKonYTah6)
                On Error Resume Next
                '......................překryje novou čáru jako prodloužení bílou
                DpuvKonXTah6 = puvKonXTah6
                DpuvKonXTah6 = DpuvKonXTah6 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah6))

                DpuvKonYTah6 = puvKonYTah6
                DpuvKonYTah6 = DpuvKonYTah6 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah6))
                novKonXTah6 = DpuvKonXTah6
                novKonYTah6 = DpuvKonYTah6
                On Error Resume Next
                g2.DrawLine(myPenKrycTah6, puvKonXTah6, puvKonYTah6, novKonXTah6, novKonYTah6)
                On Error Resume Next
            '..............
            Case 7
                pbTah7.Height = 17
                ListBoxInfo.SelectedIndex = 5
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 7      "
                ListBoxInfo.SelectedIndex = -1
                RadTah7.BackColor = System.Drawing.Color.White
                '......................překryje původní čáru  čáru bílou
                On Error Resume Next
                g2.DrawLine(myPenKrycTah7, puvZacXTah7, puvZacYTah7, puvKonXTah7, puvKonYTah7)
                On Error Resume Next
                '......................překryje novou čáru jako prodloužení bílou
                DpuvKonXTah7 = puvKonXTah7
                DpuvKonXTah7 = DpuvKonXTah7 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah7))

                DpuvKonYTah7 = puvKonYTah7
                DpuvKonYTah7 = DpuvKonYTah7 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah7))
                novKonXTah7 = DpuvKonXTah7
                novKonYTah7 = DpuvKonYTah7
                On Error Resume Next
                g2.DrawLine(myPenKrycTah7, puvKonXTah7, puvKonYTah7, novKonXTah7, novKonYTah7)
                On Error Resume Next
            '............
            Case 8
                pbTah8.Height = 17
                ListBoxInfo.SelectedIndex = 6
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 8      "
                ListBoxInfo.SelectedIndex = -1
                RadTah8.BackColor = System.Drawing.Color.White
                '......................překryje původní čáru  čáru bílou
                On Error Resume Next
                g2.DrawLine(myPenKrycTah8, puvZacXTah8, puvZacYTah8, puvKonXTah8, puvKonYTah8)
                On Error Resume Next
                '......................překryje novou čáru jako prodloužení bílou
                DpuvKonXTah8 = puvKonXTah8
                DpuvKonXTah8 = DpuvKonXTah8 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah8))

                DpuvKonYTah8 = puvKonYTah8
                DpuvKonYTah8 = DpuvKonYTah8 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah8))
                novKonXTah8 = DpuvKonXTah8
                novKonYTah8 = DpuvKonYTah8
                On Error Resume Next
                g2.DrawLine(myPenKrycTah8, puvKonXTah8, puvKonYTah8, novKonXTah8, novKonYTah8)
                On Error Resume Next
            '..............
            Case 9
                pbTah9.Height = 17
                ListBoxInfo.SelectedIndex = 7
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 9      "
                ListBoxInfo.SelectedIndex = -1
                RadTah9.BackColor = System.Drawing.Color.White
                '......................překryje původní čáru  čáru bílou
                On Error Resume Next
                g2.DrawLine(myPenKrycTah9, puvZacXTah9, puvZacYTah9, puvKonXTah9, puvKonYTah9)
                On Error Resume Next
                '......................překryje novou čáru jako prodloužení bílou
                DpuvKonXTah9 = puvKonXTah9
                DpuvKonXTah9 = DpuvKonXTah9 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah9))

                DpuvKonYTah9 = puvKonYTah9
                DpuvKonYTah9 = DpuvKonYTah9 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah9))
                novKonXTah9 = DpuvKonXTah9
                novKonYTah9 = DpuvKonYTah9
                On Error Resume Next
                g2.DrawLine(myPenKrycTah9, puvKonXTah9, puvKonYTah9, novKonXTah9, novKonYTah9)
                On Error Resume Next
            '................
            Case 10
                pbTah10.Height = 17
                ListBoxInfo.SelectedIndex = 8
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 10    "
                ListBoxInfo.SelectedIndex = -1
                RadTah10.BackColor = System.Drawing.Color.White
                '......................překryje původní čáru  čáru bílou
                On Error Resume Next
                g2.DrawLine(myPenKrycTah10, puvZacXTah10, puvZacYTah10, puvKonXTah10, puvKonYTah10)
                On Error Resume Next
                '......................překryje novou čáru jako prodloužení bílou
                DpuvKonXTah10 = puvKonXTah10
                DpuvKonXTah10 = DpuvKonXTah10 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah10))

                DpuvKonYTah10 = puvKonYTah10
                DpuvKonYTah10 = DpuvKonYTah10 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah10))
                novKonXTah10 = DpuvKonXTah10
                novKonYTah10 = DpuvKonYTah10
                On Error Resume Next
                g2.DrawLine(myPenKrycTah10, puvKonXTah10, puvKonYTah10, novKonXTah10, novKonYTah10)
                On Error Resume Next
            '............
            Case 12
                pbTah12.Height = 17
                ListBoxInfo.SelectedIndex = 9
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 12    "
                ListBoxInfo.SelectedIndex = -1
                RadTah12.BackColor = System.Drawing.Color.White
                '......................překryje původní čáru  čáru bílou
                On Error Resume Next
                g2.DrawLine(myPenKrycTah12, puvZacXTah12, puvZacYTah12, puvKonXTah12, puvKonYTah12)
                On Error Resume Next
                '......................překryje novou čáru jako prodloužení bílou
                DpuvKonXTah12 = puvKonXTah12
                DpuvKonXTah12 = DpuvKonXTah12 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah12))

                DpuvKonYTah12 = puvKonYTah12
                DpuvKonYTah12 = DpuvKonYTah12 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah12))
                novKonXTah12 = DpuvKonXTah12
                novKonYTah12 = DpuvKonYTah12
                On Error Resume Next
                g2.DrawLine(myPenKrycTah12, puvKonXTah12, puvKonYTah12, novKonXTah12, novKonYTah12)
                On Error Resume Next
            '..............
            Case 13
                pbTah13.Height = 17
                ListBoxInfo.SelectedIndex = 10
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 13    "
                ListBoxInfo.SelectedIndex = -1
                RadTah13.BackColor = System.Drawing.Color.White
                '......................překryje původní čáru  čáru bílou
                On Error Resume Next
                g2.DrawLine(myPenKrycTah13, puvZacXTah13, puvZacYTah13, puvKonXTah13, puvKonYTah13)
                On Error Resume Next
                '......................překryje novou čáru jako prodloužení bílou
                DpuvKonXTah13 = puvKonXTah13
                DpuvKonXTah13 = DpuvKonXTah13 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah13))

                DpuvKonYTah13 = puvKonYTah13
                DpuvKonYTah13 = DpuvKonYTah13 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah13))
                novKonXTah13 = DpuvKonXTah13
                novKonYTah13 = DpuvKonYTah13
                On Error Resume Next
                g2.DrawLine(myPenKrycTah13, puvKonXTah13, puvKonYTah13, novKonXTah13, novKonYTah13)
                On Error Resume Next
            '................
            Case 14
                pbTah14.Height = 17
                ListBoxInfo.SelectedIndex = 11
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 14    "
                ListBoxInfo.SelectedIndex = -1
                RadTah14.BackColor = System.Drawing.Color.White
                '......................překryje původní čáru  čáru bílou
                On Error Resume Next
                g2.DrawLine(myPenKrycTah14, puvZacXTah14, puvZacYTah14, puvKonXTah14, puvKonYTah14)
                On Error Resume Next
                '......................překryje novou čáru jako prodloužení bílou
                DpuvKonXTah14 = puvKonXTah14
                DpuvKonXTah14 = DpuvKonXTah14 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah14))

                DpuvKonYTah14 = puvKonYTah14
                DpuvKonYTah14 = DpuvKonYTah14 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah14))
                novKonXTah14 = DpuvKonXTah14
                novKonYTah14 = DpuvKonYTah14
                On Error Resume Next
                g2.DrawLine(myPenKrycTah14, puvKonXTah14, puvKonYTah14, novKonXTah14, novKonYTah14)
                On Error Resume Next
            '..............
            Case 15
                pbTah15.Height = 17
                ListBoxInfo.SelectedIndex = 12
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 15    "
                ListBoxInfo.SelectedIndex = -1
                RadTah15.BackColor = System.Drawing.Color.White
                '......................překryje původní čáru  čáru bílou
                On Error Resume Next
                g2.DrawLine(myPenKrycTah15, puvZacXTah15, puvZacYTah15, puvKonXTah15, puvKonYTah15)
                On Error Resume Next
                '......................překryje novou čáru jako prodloužení bílou
                DpuvKonXTah15 = puvKonXTah15
                DpuvKonXTah15 = DpuvKonXTah15 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah15))

                DpuvKonYTah15 = puvKonYTah15
                DpuvKonYTah15 = DpuvKonYTah15 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah15))
                novKonXTah15 = DpuvKonXTah15
                novKonYTah15 = DpuvKonYTah15
                On Error Resume Next
                g2.DrawLine(myPenKrycTah15, puvKonXTah15, puvKonYTah15, novKonXTah15, novKonYTah15)
                On Error Resume Next
            '...............
            Case 17
                pbTah17.Height = 17
                ListBoxInfo.SelectedIndex = 13
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 17    "
                ListBoxInfo.SelectedIndex = -1
                RadTah17.BackColor = System.Drawing.Color.White
                '......................překryje původní čáru  čáru bílou
                On Error Resume Next
                g2.DrawLine(myPenKrycTah17, puvZacXTah17, puvZacYTah17, puvKonXTah17, puvKonYTah17)
                On Error Resume Next
                '......................překryje novou čáru jako prodloužení bílou
                DpuvKonXTah17 = puvKonXTah17
                DpuvKonXTah17 = DpuvKonXTah17 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah17))

                DpuvKonYTah17 = puvKonYTah17
                DpuvKonYTah17 = DpuvKonYTah17 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah17))
                novKonXTah17 = DpuvKonXTah17
                novKonYTah17 = DpuvKonYTah17
                On Error Resume Next
                g2.DrawLine(myPenKrycTah17, puvKonXTah17, puvKonYTah17, novKonXTah17, novKonYTah17)
                On Error Resume Next
            '..............
            Case 18
                pbTah18.Height = 17
                ListBoxInfo.SelectedIndex = 14
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 18    "
                ListBoxInfo.SelectedIndex = -1
                RadTah18.BackColor = System.Drawing.Color.White
                '......................překryje původní čáru  čáru bílou
                On Error Resume Next
                g2.DrawLine(myPenKrycTah18, puvZacXTah18, puvZacYTah18, puvKonXTah18, puvKonYTah18)
                On Error Resume Next
                '......................překryje novou čáru jako prodloužení bílou
                DpuvKonXTah18 = puvKonXTah18
                DpuvKonXTah18 = DpuvKonXTah18 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah18))

                DpuvKonYTah18 = puvKonYTah18
                DpuvKonYTah18 = DpuvKonYTah18 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah18))
                novKonXTah18 = DpuvKonXTah18
                novKonYTah18 = DpuvKonYTah18
                On Error Resume Next
                g2.DrawLine(myPenKrycTah18, puvKonXTah18, puvKonYTah18, novKonXTah18, novKonYTah18)
                On Error Resume Next
                '...............

        End Select
        ListBoxUvaz.Visible = True
        ListBoxUvaz.Items.Clear()

    End Sub
    Private Sub btnUvrat_Click(sender As Object, e As EventArgs) Handles btnUvrat.Click
        '...................omezení výšky tahové věže
        Dim Dek As Integer
        Dim Uva As Integer
        Dek = TextBox1.Text
        Uva = TextBox2.Text
        If (Dek + Uva) > 914 Then
            ' MsgBox(Dek + Uva & "tahová věž má výšku pouze 914 cm")
            MsgBox("tahová věž má výšku pouze 914 cm")
            Exit Sub
        End If
        '........................................
        Dim vOdpodl As Integer
        pbTah.Refresh()
        If TextBox1.Text = 0 Or TextBox1.Text > 700 Then
            MsgBox("zadejte výšku dekorace nebo výšku sufity (150cm)")
            Exit Sub
        End If
        '.....................
        If TextBox1.Text = Nothing Then
            MsgBox("zadejte výšku dekorace nebo výšku sufity (150cm)")
            Exit Sub
        End If
        If TextBox2.Text = Nothing Then
            MsgBox("zadejte výšku délku úvazku")
            Exit Sub
        End If
        If TextBox4.Text = Nothing Then
            MsgBox("zadejte výšku zvednutí-spuštění")
            Exit Sub
        End If
        '.....................

        If TextBox2.Text = 0 Then
            x1 = 5
            y1 = 16
            x2 = 5
            y2 = 13 + ((TextBox1.Text) * 0.479)
            '...................................'výška pbTah 'VVVV
            Dim TahV2 As Integer
            Dim objSelectedItem As Object
            objSelectedItem = ComboBoxVyber.SelectedItem
            TahV2 = objSelectedItem.ToString()
            Select Case TahV2
                Case 1
                    pbTah1.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah1.CreateGraphics
                    pbTah = pbTah1
                    pbTah1.Refresh()
                    '..............
                    vDek1 = TextBox1.Text   '1111
                    vUva1 = TextBox2.Text
                    vPod1 = TextBox4.Text
                Case 2
                    pbTah2.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah2.CreateGraphics
                    pbTah = pbTah2
                    pbTah2.Refresh()
                    '...............
                    vDek2 = TextBox1.Text   '1111
                    vUva2 = TextBox2.Text
                    vPod2 = TextBox4.Text
                Case 3
                    pbTah3.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah3.CreateGraphics
                    pbTah = pbTah3
                    pbTah3.Refresh()
                    '..............
                    vDek3 = TextBox1.Text   '1111
                    vUva3 = TextBox2.Text
                    vPod3 = TextBox4.Text
                Case 5
                    pbTah5.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah5.CreateGraphics
                    pbTah = pbTah5
                    pbTah5.Refresh()
                    '..............
                    vDek5 = TextBox1.Text   '1111
                    vUva5 = TextBox2.Text
                    vPod5 = TextBox4.Text
                Case 6
                    pbTah6.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah6.CreateGraphics
                    pbTah = pbTah6
                    pbTah6.Refresh()
                    '..............
                    vDek6 = TextBox1.Text   '1111
                    vUva6 = TextBox2.Text
                    vPod6 = TextBox4.Text
                Case 7
                    pbTah7.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah7.CreateGraphics
                    pbTah = pbTah7
                    pbTah7.Refresh()
                    '..............
                    vDek7 = TextBox1.Text   '1111
                    vUva7 = TextBox2.Text
                    vPod7 = TextBox4.Text
                Case 8
                    pbTah8.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah8.CreateGraphics
                    pbTah = pbTah8
                    pbTah8.Refresh()
                    '..............
                    vDek8 = TextBox1.Text   '1111
                    vUva8 = TextBox2.Text
                    vPod8 = TextBox4.Text
                Case 9
                    pbTah9.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah9.CreateGraphics
                    pbTah = pbTah9
                    pbTah9.Refresh()
                    '..............
                    vDek9 = TextBox1.Text   '1111
                    vUva9 = TextBox2.Text
                    vPod9 = TextBox4.Text
                Case 10
                    pbTah10.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah10.CreateGraphics
                    pbTah = pbTah10
                    pbTah10.Refresh()
                    '..............
                    vDek10 = TextBox1.Text   '1111
                    vUva10 = TextBox2.Text
                    vPod10 = TextBox4.Text
                Case 12
                    pbTah12.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah12.CreateGraphics
                    pbTah = pbTah12
                    pbTah12.Refresh()
                    '..............
                    vDek12 = TextBox1.Text   '1111
                    vUva12 = TextBox2.Text
                    vPod12 = TextBox4.Text
                Case 13
                    pbTah13.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah13.CreateGraphics
                    pbTah = pbTah13
                    pbTah13.Refresh()
                    '..............
                    vDek13 = TextBox1.Text   '1111
                    vUva13 = TextBox2.Text
                    vPod13 = TextBox4.Text
                Case 14
                    pbTah14.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah14.CreateGraphics
                    pbTah = pbTah14
                    pbTah14.Refresh()
                    '..............
                    vDek14 = TextBox1.Text   '1111
                    vUva14 = TextBox2.Text
                    vPod14 = TextBox4.Text
                Case 15
                    pbTah15.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah15.CreateGraphics
                    pbTah = pbTah15
                    pbTah15.Refresh()
                    '..............
                    vDek15 = TextBox1.Text   '1111
                    vUva15 = TextBox2.Text
                    vPod15 = TextBox4.Text
                Case 17
                    pbTah17.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah17.CreateGraphics
                    pbTah = pbTah17
                    pbTah17.Refresh()
                    '..............
                    vDek17 = TextBox1.Text   '1111
                    vUva17 = TextBox2.Text
                    vPod17 = TextBox4.Text
                Case 18
                    pbTah18.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah18.CreateGraphics
                    pbTah = pbTah18
                    pbTah18.Refresh()
                    '..............
                    vDek18 = TextBox1.Text   '1111
                    vUva18 = TextBox2.Text
                    vPod18 = TextBox4.Text
            End Select
            '...........................
            GraphicsFun.DrawLine(myPenTah, x1, y1, x2, y2)
            GraphicsFun.DrawEllipse(myPenTyc, 3, y1 - 3, 3, 3)
            GraphicsFun.DrawLine(myPenLano, x1, 0, x2, y1 - 3)
            vOdpodl = (454 - y2 - 3) / 0.479
            TextBox3.Text = vOdpodl

        Else
            x1u = 5
            y1u = 16
            x2u = 5
            y2u = y1u + ((TextBox2.Text) * 0.479)
            '  GraphicsFun.DrawLine(myPenUvaz, x1u, y1u, x2u, y2u)
            x1 = 5
            y1 = y2u
            x2 = 5
            y2 = y1 + ((TextBox1.Text) * 0.479)
            '...................................'výška pbTah 'VVVV
            Dim TahV3 As Integer
            Dim objSelectedItem As Object
            objSelectedItem = ComboBoxVyber.SelectedItem
            TahV3 = objSelectedItem.ToString()
            Select Case TahV3
                Case 1
                    pbTah1.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah1.CreateGraphics
                    pbTah = pbTah1
                    pbTah1.Refresh()
                    '............
                    vDek1 = TextBox1.Text   '1111
                    vUva1 = TextBox2.Text
                    vPod1 = TextBox4.Text
                Case 2
                    pbTah2.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah2.CreateGraphics
                    pbTah = pbTah2
                    pbTah2.Refresh()
                    '................
                    vDek2 = TextBox1.Text   '1111
                    vUva2 = TextBox2.Text
                    vPod2 = TextBox4.Text
                Case 3
                    pbTah3.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah3.CreateGraphics
                    pbTah = pbTah3
                    pbTah3.Refresh()
                    '..............
                    vDek3 = TextBox1.Text   '1111
                    vUva3 = TextBox2.Text
                    vPod3 = TextBox4.Text
                Case 5
                    pbTah5.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah5.CreateGraphics
                    pbTah = pbTah5
                    pbTah5.Refresh()
                    '..............
                    vDek5 = TextBox1.Text   '1111
                    vUva5 = TextBox2.Text
                    vPod5 = TextBox4.Text
                Case 6
                    pbTah6.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah6.CreateGraphics
                    pbTah = pbTah6
                    pbTah6.Refresh()
                    '..............
                    vDek6 = TextBox1.Text   '1111
                    vUva6 = TextBox2.Text
                    vPod6 = TextBox4.Text
                Case 7
                    pbTah7.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah7.CreateGraphics
                    pbTah = pbTah7
                    pbTah7.Refresh()
                    '..............
                    vDek7 = TextBox1.Text   '1111
                    vUva7 = TextBox2.Text
                    vPod7 = TextBox4.Text
                Case 8
                    pbTah8.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah8.CreateGraphics
                    pbTah = pbTah8
                    pbTah8.Refresh()
                    '..............
                    vDek8 = TextBox1.Text   '1111
                    vUva8 = TextBox2.Text
                    vPod8 = TextBox4.Text
                Case 9
                    pbTah9.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah9.CreateGraphics
                    pbTah = pbTah9
                    pbTah9.Refresh()
                    '..............
                    vDek9 = TextBox1.Text   '1111
                    vUva9 = TextBox2.Text
                    vPod9 = TextBox4.Text
                Case 10
                    pbTah10.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah10.CreateGraphics
                    pbTah = pbTah10
                    pbTah10.Refresh()
                    '..............
                    vDek10 = TextBox1.Text   '1111
                    vUva10 = TextBox2.Text
                    vPod10 = TextBox4.Text
                Case 12
                    pbTah12.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah12.CreateGraphics
                    pbTah = pbTah12
                    pbTah12.Refresh()
                    '..............
                    vDek12 = TextBox1.Text   '1111
                    vUva12 = TextBox2.Text
                    vPod12 = TextBox4.Text
                Case 13
                    pbTah13.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah13.CreateGraphics
                    pbTah = pbTah13
                    pbTah13.Refresh()
                    '..............
                    vDek13 = TextBox1.Text   '1111
                    vUva13 = TextBox2.Text
                    vPod13 = TextBox4.Text
                Case 14
                    pbTah14.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah14.CreateGraphics
                    pbTah = pbTah14
                    pbTah14.Refresh()
                    '..............
                    vDek14 = TextBox1.Text   '1111
                    vUva14 = TextBox2.Text
                    vPod14 = TextBox4.Text
                Case 15
                    pbTah15.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah15.CreateGraphics
                    pbTah = pbTah15
                    pbTah15.Refresh()
                    '..............
                    vDek15 = TextBox1.Text   '1111
                    vUva15 = TextBox2.Text
                    vPod15 = TextBox4.Text
                Case 17
                    pbTah17.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah17.CreateGraphics
                    pbTah = pbTah17
                    pbTah17.Refresh()
                    '..............
                    vDek17 = TextBox1.Text   '1111
                    vUva17 = TextBox2.Text
                    vPod17 = TextBox4.Text
                Case 18
                    pbTah18.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah18.CreateGraphics
                    pbTah = pbTah18
                    pbTah18.Refresh()
                    '..............
                    vDek18 = TextBox1.Text   '1111
                    vUva18 = TextBox2.Text
                    vPod18 = TextBox4.Text
            End Select
            '............................
            GraphicsFun.DrawLine(myPenUvaz, x1u, y1u, x2u, y2u)
            '...........................
            GraphicsFun.DrawLine(myPenTah, x1, y1, x2, y2)
            GraphicsFun.DrawEllipse(myPenTyc, 3, y1u - 3, 3, 3)
            GraphicsFun.DrawLine(myPenLano, x1, 0, x2, y1u - 3)
            '...........................
            vOdpodl = (454 - y2) / 0.479
            TextBox3.Text = vOdpodl

        End If
        '....................................zapíše info řádek
        Dim Uvrat As Integer
        Dim objSelectedItemVyber As Object
        objSelectedItemVyber = ComboBoxVyber.SelectedItem
        Uvrat = objSelectedItemVyber.ToString()

        Select Case Uvrat
            Case 1
                ListBoxInfo.SelectedIndex = 0
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 1" & "     výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah1.BackColor = System.Drawing.Color.Gray
            Case 2
                ListBoxInfo.SelectedIndex = 1
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 2" & "     výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah2.BackColor = System.Drawing.Color.Gray
            Case 3
                ListBoxInfo.SelectedIndex = 2
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 3" & "     výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah3.BackColor = System.Drawing.Color.Gray
            Case 5
                ListBoxInfo.SelectedIndex = 3
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 5" & "     výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah5.BackColor = System.Drawing.Color.Gray
            Case 6
                ListBoxInfo.SelectedIndex = 4
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 6" & "     výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah6.BackColor = System.Drawing.Color.Gray
            Case 7
                ListBoxInfo.SelectedIndex = 5
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 7" & "     výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah7.BackColor = System.Drawing.Color.Gray
            Case 8
                ListBoxInfo.SelectedIndex = 6
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 8" & "     výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah8.BackColor = System.Drawing.Color.Gray
            Case 9
                ListBoxInfo.SelectedIndex = 7
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 9" & "     výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah9.BackColor = System.Drawing.Color.Gray
            Case 10
                ListBoxInfo.SelectedIndex = 8
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 10" & "   výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah10.BackColor = System.Drawing.Color.Gray
            Case 12
                ListBoxInfo.SelectedIndex = 9
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 12" & "   výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah12.BackColor = System.Drawing.Color.Gray
            Case 13
                ListBoxInfo.SelectedIndex = 10
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 13" & "   výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah13.BackColor = System.Drawing.Color.Gray
            Case 14
                ListBoxInfo.SelectedIndex = 11
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 14" & "   výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah14.BackColor = System.Drawing.Color.Gray
            Case 15
                ListBoxInfo.SelectedIndex = 12
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 15" & "   výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah15.BackColor = System.Drawing.Color.Gray
            Case 17
                ListBoxInfo.SelectedIndex = 13
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 17" & "   výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah17.BackColor = System.Drawing.Color.Gray
            Case 18
                ListBoxInfo.SelectedIndex = -1
                ListBoxInfo.SelectedIndex = 14
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 18" & "   výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
                RadTah18.BackColor = System.Drawing.Color.Gray
        End Select
        '..................... 

        TextBox4.Text = 0
        '.........................(cm)od podlahy
        If TextBox3.Text > 0 Then
            ListBox8.Visible = True
        End If

        Dim cTah As Integer
        Dim objSelectedItemTah As Object
        objSelectedItemTah = ComboBoxVyber.SelectedItem
        cTah = objSelectedItemTah.ToString()

        Select Case cTah

           '..............přizavěšení určí úhel a data          
            Case 1
                ComboBoxVyber.SelectedItem = 1
                puvZacXTah1 = -80 'hlava diváka od pb1
                puvZacYTah1 = 473 'hlava diváka od vrchu pb1  
                puvKonXTah1 = 830 - 546 + 5 'kraj pbTah1-kraj pb1 + 5 střed pbTah1     
                puvKonYTah1 = y2 + 6
                DpuvZacXTah1 = puvZacXTah1
                DpuvZacYTah1 = puvZacYTah1
                DpuvKonXTah1 = puvKonXTah1
                DpuvKonYTah1 = puvKonYTah1
                AngleTah1 = -(Math.Atan2(DpuvKonYTah1 - DpuvZacYTah1, DpuvKonXTah1 - DpuvZacXTah1) * 180 / Math.PI) '+ 180  '0 stupňů je vodorovně,90 je svisle+180 je 180 vodorovně
                RadTah1.Enabled = True

                '..........................................
            Case 2
                ComboBoxVyber.SelectedItem = 2
                puvZacXTah2 = -80
                puvZacYTah2 = 473
                puvKonXTah2 = 844 - 546 + 5
                puvKonYTah2 = y2 + 6
                DpuvZacXTah2 = puvZacXTah2
                DpuvZacYTah2 = puvZacYTah2
                DpuvKonXTah2 = puvKonXTah2
                DpuvKonYTah2 = puvKonYTah2
                AngleTah2 = -(Math.Atan2(DpuvKonYTah2 - DpuvZacYTah2, DpuvKonXTah2 - DpuvZacXTah2) * 180 / Math.PI)
                RadTah2.Enabled = True
                '..........................................
            Case 3
                ComboBoxVyber.SelectedItem = 3
                puvZacXTah3 = -80
                puvZacYTah3 = 473
                puvKonXTah3 = 858 - 546 + 5
                puvKonYTah3 = y2 + 6
                DpuvZacXTah3 = puvZacXTah3
                DpuvZacYTah3 = puvZacYTah3
                DpuvKonXTah3 = puvKonXTah3
                DpuvKonYTah3 = puvKonYTah3
                AngleTah3 = -(Math.Atan2(DpuvKonYTah3 - DpuvZacYTah3, DpuvKonXTah3 - DpuvZacXTah3) * 180 / Math.PI)
                RadTah3.Enabled = True
                '..........................................
            Case 5
                ComboBoxVyber.SelectedItem = 5
                puvZacXTah5 = -80
                puvZacYTah5 = 473
                puvKonXTah5 = 895 - 546 + 5
                puvKonYTah5 = y2 + 6
                DpuvZacXTah5 = puvZacXTah5
                DpuvZacYTah5 = puvZacYTah5
                DpuvKonXTah5 = puvKonXTah5
                DpuvKonYTah5 = puvKonYTah5
                AngleTah5 = -(Math.Atan2(DpuvKonYTah5 - DpuvZacYTah5, DpuvKonXTah5 - DpuvZacXTah5) * 180 / Math.PI)
                RadTah5.Enabled = True
                '..........................................
            Case 6
                ComboBoxVyber.SelectedItem = 6
                puvZacXTah6 = -80
                puvZacYTah6 = 473
                puvKonXTah6 = 909 - 546 + 5
                puvKonYTah6 = y2 + 6
                DpuvZacXTah6 = puvZacXTah6
                DpuvZacYTah6 = puvZacYTah6
                DpuvKonXTah6 = puvKonXTah6
                DpuvKonYTah6 = puvKonYTah6
                AngleTah6 = -(Math.Atan2(DpuvKonYTah6 - DpuvZacYTah6, DpuvKonXTah6 - DpuvZacXTah6) * 180 / Math.PI)
                RadTah6.Enabled = True
                '..........................................
            Case 7
                ComboBoxVyber.SelectedItem = 7
                puvZacXTah7 = -80
                puvZacYTah7 = 473
                puvKonXTah7 = 924 - 546 + 5
                puvKonYTah7 = y2 + 6
                DpuvZacXTah7 = puvZacXTah7
                DpuvZacYTah7 = puvZacYTah7
                DpuvKonXTah7 = puvKonXTah7
                DpuvKonYTah7 = puvKonYTah7
                AngleTah7 = -(Math.Atan2(DpuvKonYTah7 - DpuvZacYTah7, DpuvKonXTah7 - DpuvZacXTah7) * 180 / Math.PI)
                RadTah7.Enabled = True
                '..........................................
            Case 8
                ComboBoxVyber.SelectedItem = 8
                puvZacXTah8 = -80
                puvZacYTah8 = 473
                puvKonXTah8 = 938 - 546 + 5
                puvKonYTah8 = y2 + 6
                DpuvZacXTah8 = puvZacXTah8
                DpuvZacYTah8 = puvZacYTah8
                DpuvKonXTah8 = puvKonXTah8
                DpuvKonYTah8 = puvKonYTah8
                AngleTah8 = -(Math.Atan2(DpuvKonYTah8 - DpuvZacYTah8, DpuvKonXTah8 - DpuvZacXTah8) * 180 / Math.PI)
                RadTah8.Enabled = True
                '..........................................
            Case 9
                ComboBoxVyber.SelectedItem = 9
                puvZacXTah9 = -80
                puvZacYTah9 = 473
                puvKonXTah9 = 952 - 546 + 5
                puvKonYTah9 = y2 + 6
                DpuvZacXTah9 = puvZacXTah9
                DpuvZacYTah9 = puvZacYTah9
                DpuvKonXTah9 = puvKonXTah9
                DpuvKonYTah9 = puvKonYTah9
                AngleTah9 = -(Math.Atan2(DpuvKonYTah9 - DpuvZacYTah9, DpuvKonXTah9 - DpuvZacXTah9) * 180 / Math.PI)
                RadTah9.Enabled = True
                '..........................................
            Case 10
                ComboBoxVyber.SelectedItem = 10
                puvZacXTah10 = -80
                puvZacYTah10 = 473
                puvKonXTah10 = 966 - 546 + 5
                puvKonYTah10 = y2 + 6
                DpuvZacXTah10 = puvZacXTah10
                DpuvZacYTah10 = puvZacYTah10
                DpuvKonXTah10 = puvKonXTah10
                DpuvKonYTah10 = puvKonYTah10
                AngleTah10 = -(Math.Atan2(DpuvKonYTah10 - DpuvZacYTah10, DpuvKonXTah10 - DpuvZacXTah10) * 180 / Math.PI)
                RadTah10.Enabled = True
                '..........................................
            Case 12
                ComboBoxVyber.SelectedItem = 12
                puvZacXTah12 = -80
                puvZacYTah12 = 473
                puvKonXTah12 = 999 - 546 + 5
                puvKonYTah12 = y2 + 6
                DpuvZacXTah12 = puvZacXTah12
                DpuvZacYTah12 = puvZacYTah12
                DpuvKonXTah12 = puvKonXTah12
                DpuvKonYTah12 = puvKonYTah12
                AngleTah12 = -(Math.Atan2(DpuvKonYTah12 - DpuvZacYTah12, DpuvKonXTah12 - DpuvZacXTah12) * 180 / Math.PI)
                RadTah12.Enabled = True
                '..........................................
            Case 13
                ComboBoxVyber.SelectedItem = 13
                puvZacXTah13 = -80
                puvZacYTah13 = 473
                puvKonXTah13 = 1013 - 546 + 5
                puvKonYTah13 = y2 + 6
                DpuvZacXTah13 = puvZacXTah13
                DpuvZacYTah13 = puvZacYTah13
                DpuvKonXTah13 = puvKonXTah13
                DpuvKonYTah13 = puvKonYTah13
                AngleTah13 = -(Math.Atan2(DpuvKonYTah13 - DpuvZacYTah13, DpuvKonXTah13 - DpuvZacXTah13) * 180 / Math.PI)
                RadTah13.Enabled = True
                '..........................................
            Case 14
                ComboBoxVyber.SelectedItem = 14
                puvZacXTah14 = -80
                puvZacYTah14 = 473
                puvKonXTah14 = 1027 - 546 + 5
                puvKonYTah14 = y2 + 6
                DpuvZacXTah14 = puvZacXTah14
                DpuvZacYTah14 = puvZacYTah14
                DpuvKonXTah14 = puvKonXTah14
                DpuvKonYTah14 = puvKonYTah14
                AngleTah14 = -(Math.Atan2(DpuvKonYTah14 - DpuvZacYTah14, DpuvKonXTah14 - DpuvZacXTah14) * 180 / Math.PI)
                RadTah14.Enabled = True
                '..........................................
            Case 15
                ComboBoxVyber.SelectedItem = 15
                puvZacXTah15 = -80
                puvZacYTah15 = 473
                puvKonXTah15 = 1065 - 546 + 5
                puvKonYTah15 = y2 + 6
                DpuvZacXTah15 = puvZacXTah15
                DpuvZacYTah15 = puvZacYTah15
                DpuvKonXTah15 = puvKonXTah15
                DpuvKonYTah15 = puvKonYTah15
                AngleTah15 = -(Math.Atan2(DpuvKonYTah15 - DpuvZacYTah15, DpuvKonXTah15 - DpuvZacXTah15) * 180 / Math.PI)
                RadTah15.Enabled = True
                '..........................................
            Case 17
                ComboBoxVyber.SelectedItem = 17
                puvZacXTah17 = -80
                puvZacYTah17 = 473
                puvKonXTah17 = 1092 - 546 + 5
                puvKonYTah17 = y2 + 6
                DpuvZacXTah17 = puvZacXTah17
                DpuvZacYTah17 = puvZacYTah17
                DpuvKonXTah17 = puvKonXTah17
                DpuvKonYTah17 = puvKonYTah17
                AngleTah17 = -(Math.Atan2(DpuvKonYTah17 - DpuvZacYTah17, DpuvKonXTah17 - DpuvZacXTah17) * 180 / Math.PI)
                RadTah17.Enabled = True
                '..........................................
            Case 18
                ComboBoxVyber.SelectedItem = 18
                puvZacXTah18 = -80
                puvZacYTah18 = 473
                puvKonXTah18 = 1106 - 546 + 5
                puvKonYTah18 = y2 + 6
                DpuvZacXTah18 = puvZacXTah18
                DpuvZacYTah18 = puvZacYTah18
                DpuvKonXTah18 = puvKonXTah18
                DpuvKonYTah18 = puvKonYTah18
                AngleTah18 = -(Math.Atan2(DpuvKonYTah18 - DpuvZacYTah18, DpuvKonXTah18 - DpuvZacXTah18) * 180 / Math.PI)
                RadTah18.Enabled = True
                '..........................................
        End Select

    End Sub
    Private Sub btnPosun1_Click(sender As Object, e As EventArgs) Handles btnPosun1.Click
        '...................omezení výšky tahové věže
        Dim Dek As Integer
        Dim Uva As Integer
        Dim Pod As Integer
        Dek = TextBox1.Text
        Uva = TextBox2.Text
        Pod = TextBox4.Text
        If (Dek + Uva + Pod) > 914 Then
            ' MsgBox(Dek + Uva & "tahová věž má výšku pouze 914 cm")
            MsgBox("tahová věž má výšku pouze 914 cm")
            Exit Sub
        End If
        '........................................
        pbTah.Refresh()
        '.....................
        If TextBox1.Text = Nothing Then
            MsgBox("zadejte výšku dekorace nebo výšku sufity (150cm)")
            Exit Sub
        End If
        If TextBox2.Text = Nothing Then
            MsgBox("zadejte výšku délku úvazku")
            Exit Sub
        End If
        If TextBox4.Text = Nothing Then
            MsgBox("zadejte výšku zvednutí-spuštění")
            Exit Sub
        End If
        '.....................
        If TextBox1.Text = 0 Or TextBox1.Text > 700 Then
            MsgBox("zadejte výšku dekorace nebo výšku sufity (150cm)")
        End If

        If TextBox2.Text = 0 Then

            x1 = 5
            x2 = 5
            y2 = 454 - ((TextBox4.Text) * 0.479)
            y1 = y2 - ((TextBox1.Text) * 0.479)
            '..........................
            Dim TahV4 As Integer
            Dim objSelectedItem As Object
            objSelectedItem = ComboBoxVyber.SelectedItem
            TahV4 = objSelectedItem.ToString()
            Select Case TahV4
                Case 1
                    pbTah1.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah1.CreateGraphics
                    pbTah = pbTah1
                    pbTah1.Refresh()
                    RadTah1.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek1 = TextBox1.Text   '1111
                    vUva1 = TextBox2.Text
                    vPod1 = TextBox4.Text
                Case 2
                    pbTah2.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah2.CreateGraphics
                    pbTah = pbTah2
                    pbTah2.Refresh()
                    RadTah2.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek2 = TextBox1.Text   '1111
                    vUva2 = TextBox2.Text
                    vPod2 = TextBox4.Text
                Case 3
                    pbTah3.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah3.CreateGraphics
                    pbTah = pbTah3
                    pbTah3.Refresh()
                    RadTah3.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek3 = TextBox1.Text   '1111
                    vUva3 = TextBox2.Text
                    vPod3 = TextBox4.Text
                Case 5
                    pbTah5.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah5.CreateGraphics
                    pbTah = pbTah5
                    pbTah5.Refresh()
                    RadTah5.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek5 = TextBox1.Text   '1111
                    vUva5 = TextBox2.Text
                    vPod5 = TextBox4.Text
                Case 6
                    pbTah6.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah6.CreateGraphics
                    pbTah = pbTah6
                    pbTah6.Refresh()
                    RadTah6.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek6 = TextBox1.Text   '1111
                    vUva6 = TextBox2.Text
                    vPod6 = TextBox4.Text
                Case 7
                    pbTah7.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah7.CreateGraphics
                    pbTah = pbTah7
                    pbTah7.Refresh()
                    RadTah7.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek7 = TextBox1.Text   '1111
                    vUva7 = TextBox2.Text
                    vPod7 = TextBox4.Text
                Case 8
                    pbTah8.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah8.CreateGraphics
                    pbTah = pbTah8
                    pbTah8.Refresh()
                    RadTah8.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek8 = TextBox1.Text   '1111
                    vUva8 = TextBox2.Text
                    vPod8 = TextBox4.Text
                Case 9
                    pbTah9.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah9.CreateGraphics
                    pbTah = pbTah9
                    pbTah9.Refresh()
                    RadTah9.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek9 = TextBox1.Text   '1111
                    vUva9 = TextBox2.Text
                    vPod9 = TextBox4.Text
                Case 10
                    pbTah10.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah10.CreateGraphics
                    pbTah = pbTah10
                    pbTah10.Refresh()
                    RadTah10.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek10 = TextBox1.Text   '1111
                    vUva10 = TextBox2.Text
                    vPod10 = TextBox4.Text
                Case 12
                    pbTah12.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah12.CreateGraphics
                    pbTah = pbTah12
                    pbTah12.Refresh()
                    RadTah12.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek12 = TextBox1.Text   '1111
                    vUva12 = TextBox2.Text
                    vPod12 = TextBox4.Text
                Case 13
                    pbTah13.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah13.CreateGraphics
                    pbTah = pbTah13
                    pbTah13.Refresh()
                    RadTah13.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek13 = TextBox1.Text   '1111
                    vUva13 = TextBox2.Text
                    vPod13 = TextBox4.Text
                Case 14
                    pbTah14.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah14.CreateGraphics
                    pbTah = pbTah14
                    pbTah14.Refresh()
                    RadTah14.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek14 = TextBox1.Text   '1111
                    vUva14 = TextBox2.Text
                    vPod14 = TextBox4.Text
                Case 15
                    pbTah15.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah15.CreateGraphics
                    pbTah = pbTah15
                    pbTah15.Refresh()
                    RadTah15.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek15 = TextBox1.Text   '1111
                    vUva15 = TextBox2.Text
                    vPod15 = TextBox4.Text
                Case 17
                    pbTah17.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah17.CreateGraphics
                    pbTah = pbTah17
                    pbTah17.Refresh()
                    RadTah17.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek17 = TextBox1.Text   '1111
                    vUva17 = TextBox2.Text
                    vPod17 = TextBox4.Text
                Case 18
                    pbTah18.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah18.CreateGraphics
                    pbTah = pbTah18
                    pbTah18.Refresh()
                    RadTah18.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek18 = TextBox1.Text   '1111
                    vUva18 = TextBox2.Text
                    vPod18 = TextBox4.Text
            End Select
            '...................
            GraphicsFun.DrawLine(myPenTah, x1, y1, x2, y2)
            GraphicsFun.DrawEllipse(myPenTyc, 3, y1 - 3, 3, 3)
            GraphicsFun.DrawLine(myPenLano, x1, 0, x2, y1 - 3)
        Else
            x1 = 5
            x2 = 5
            y2 = 454 - ((TextBox4.Text) * 0.479)
            y1 = y2 - ((TextBox1.Text) * 0.479)

            x1u = 5
            y2u = y1
            y1u = y1 - ((TextBox2.Text) * 0.479)
            x2u = 5
            '..........................
            Dim TahV5 As Integer
            Dim objSelectedItem As Object
            objSelectedItem = ComboBoxVyber.SelectedItem
            TahV5 = objSelectedItem.ToString()
            Select Case TahV5
                Case 1
                    pbTah1.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah1.CreateGraphics
                    pbTah = pbTah1
                    pbTah1.Refresh()
                    RadTah1.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek1 = TextBox1.Text   '1111
                    vUva1 = TextBox2.Text
                    vPod1 = TextBox4.Text
                Case 2
                    pbTah2.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah2.CreateGraphics
                    pbTah = pbTah2
                    pbTah2.Refresh()
                    RadTah2.BackColor = System.Drawing.Color.Gray
                    '...............
                    vDek2 = TextBox1.Text   '1111
                    vUva2 = TextBox2.Text
                    vPod2 = TextBox4.Text
                Case 3
                    pbTah3.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah3.CreateGraphics
                    pbTah = pbTah3
                    pbTah3.Refresh()
                    RadTah3.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek3 = TextBox1.Text   '1111
                    vUva3 = TextBox2.Text
                    vPod3 = TextBox4.Text
                Case 5
                    pbTah5.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah5.CreateGraphics
                    pbTah = pbTah5
                    pbTah5.Refresh()
                    RadTah5.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek5 = TextBox1.Text   '1111
                    vUva5 = TextBox2.Text
                    vPod5 = TextBox4.Text
                Case 6
                    pbTah6.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah6.CreateGraphics
                    pbTah = pbTah6
                    pbTah6.Refresh()
                    RadTah6.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek6 = TextBox1.Text   '1111
                    vUva6 = TextBox2.Text
                    vPod6 = TextBox4.Text
                Case 7
                    pbTah7.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah7.CreateGraphics
                    pbTah = pbTah7
                    pbTah7.Refresh()
                    RadTah7.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek7 = TextBox1.Text   '1111
                    vUva7 = TextBox2.Text
                    vPod7 = TextBox4.Text
                Case 8
                    pbTah8.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah8.CreateGraphics
                    pbTah = pbTah8
                    pbTah8.Refresh()
                    RadTah8.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek8 = TextBox1.Text   '1111
                    vUva8 = TextBox2.Text
                    vPod8 = TextBox4.Text
                Case 9
                    pbTah9.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah9.CreateGraphics
                    pbTah = pbTah9
                    pbTah9.Refresh()
                    RadTah9.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek9 = TextBox1.Text   '1111
                    vUva9 = TextBox2.Text
                    vPod9 = TextBox4.Text
                Case 10
                    pbTah10.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah10.CreateGraphics
                    pbTah = pbTah10
                    pbTah10.Refresh()
                    RadTah10.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek10 = TextBox1.Text   '1111
                    vUva10 = TextBox2.Text
                    vPod10 = TextBox4.Text
                Case 12
                    pbTah12.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah12.CreateGraphics
                    pbTah = pbTah12
                    pbTah12.Refresh()
                    RadTah12.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek12 = TextBox1.Text   '1111
                    vUva12 = TextBox2.Text
                    vPod12 = TextBox4.Text
                Case 13
                    pbTah13.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah13.CreateGraphics
                    pbTah = pbTah13
                    pbTah13.Refresh()
                    RadTah13.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek13 = TextBox1.Text   '1111
                    vUva13 = TextBox2.Text
                    vPod13 = TextBox4.Text
                Case 14
                    pbTah14.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah14.CreateGraphics
                    pbTah = pbTah14
                    pbTah14.Refresh()
                    RadTah14.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek14 = TextBox1.Text   '1111
                    vUva14 = TextBox2.Text
                    vPod14 = TextBox4.Text
                Case 15
                    pbTah15.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah15.CreateGraphics
                    pbTah = pbTah15
                    pbTah15.Refresh()
                    RadTah15.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek15 = TextBox1.Text   '1111
                    vUva15 = TextBox2.Text
                    vPod15 = TextBox4.Text
                Case 17
                    pbTah17.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah17.CreateGraphics
                    pbTah = pbTah17
                    pbTah17.Refresh()
                    RadTah17.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek17 = TextBox1.Text   '1111
                    vUva17 = TextBox2.Text
                    vPod17 = TextBox4.Text
                Case 18
                    pbTah18.Height = y2 ' určí výšku pb
                    GraphicsFun = pbTah18.CreateGraphics
                    pbTah = pbTah18
                    pbTah18.Refresh()
                    RadTah18.BackColor = System.Drawing.Color.Gray
                    '..............
                    vDek18 = TextBox1.Text   '1111
                    vUva18 = TextBox2.Text
                    vPod18 = TextBox4.Text
            End Select
            '...................
            GraphicsFun.DrawLine(myPenTah, x1, y1, x2, y2)
            GraphicsFun.DrawLine(myPenUvaz, x1u, y1u, x2u, y2u)
            GraphicsFun.DrawEllipse(myPenTyc, 3, y1u - 3, 3, 3)
            GraphicsFun.DrawLine(myPenLano, x1, 0, x2, y1u - 3)

        End If
        '................................. info

        Dim vOdpodlPosun As Integer
        vOdpodlPosun = TextBox4.Text
        TextBox3.Text = vOdpodlPosun
        Dim Posun As Integer
        Dim objSelectedItemVyber As Object
        objSelectedItemVyber = ComboBoxVyber.SelectedItem
        Posun = objSelectedItemVyber.ToString()

        Select Case Posun
            Case 1
                ListBoxInfo.SelectedIndex = 0
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 1" & "     výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
            Case 2
                ListBoxInfo.SelectedIndex = 1
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 2" & "     výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
            Case 3
                ListBoxInfo.SelectedIndex = 2
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 3" & "     výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
            Case 5
                ListBoxInfo.SelectedIndex = 3
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 5" & "     výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
            Case 6
                ListBoxInfo.SelectedIndex = 4
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 6" & "     výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
            Case 7
                ListBoxInfo.SelectedIndex = 5
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 7" & "     výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
            Case 8
                ListBoxInfo.SelectedIndex = 6
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 8" & "     výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
            Case 9
                ListBoxInfo.SelectedIndex = 7
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 9" & "     výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
            Case 10
                ListBoxInfo.SelectedIndex = 8
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 10" & "   výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
            Case 12
                ListBoxInfo.SelectedIndex = 9
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 12" & "   výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
            Case 13
                ListBoxInfo.SelectedIndex = 10
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 13" & "   výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
            Case 14
                ListBoxInfo.SelectedIndex = 11
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 14" & "   výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
            Case 15
                ListBoxInfo.SelectedIndex = 12
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 15" & "   výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
            Case 17
                ListBoxInfo.SelectedIndex = 13
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 17" & "   výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
            Case 18
                ListBoxInfo.SelectedIndex = -1
                ListBoxInfo.SelectedIndex = 14
                ListBoxInfo.Items(ListBoxInfo.SelectedIndex) = "  Tah 18" & "   výška dekorace: " & TextBox1.Text & " cm" & "     délka úvazku: " &
        TextBox2.Text & " cm" & "      dekorace od podlahy: " & TextBox3.Text & " cm"
                ListBoxInfo.SelectedIndex = -1
        End Select
        '..............
        TextBox4.Text = 0

        Dim vybTah As Integer
        Dim objSelectedItemTah As Object
        objSelectedItemTah = ComboBoxVyber.SelectedItem
        vybTah = objSelectedItemVyber.ToString()

        Select Case vybTah


        '..............přizavěšení určí úhel a data 
            Case 1
                ComboBoxVyber.SelectedItem = 1
                puvZacXTah1 = -80 'hlava diváka od pb1
                puvZacYTah1 = 473 'hlava diváka od vrchu pb1  
                puvKonXTah1 = 830 - 546 + 5 'kraj pbTah1-kraj pb1 + 5 střed pbTah1     
                puvKonYTah1 = y2 + 6
                DpuvZacXTah1 = puvZacXTah1
                DpuvZacYTah1 = puvZacYTah1
                DpuvKonXTah1 = puvKonXTah1
                DpuvKonYTah1 = puvKonYTah1
                AngleTah1 = -(Math.Atan2(DpuvKonYTah1 - DpuvZacYTah1, DpuvKonXTah1 - DpuvZacXTah1) * 180 / Math.PI) '+ 180  '0 stupňů je vodorovně,90 je svisle+180 je 180 vodorovně
                RadTah1.Enabled = True
                '..........................................
            Case 2
                ComboBoxVyber.SelectedItem = 2
                puvZacXTah2 = -80
                puvZacYTah2 = 473
                puvKonXTah2 = 844 - 546 + 5
                puvKonYTah2 = y2 + 6
                DpuvZacXTah2 = puvZacXTah2
                DpuvZacYTah2 = puvZacYTah2
                DpuvKonXTah2 = puvKonXTah2
                DpuvKonYTah2 = puvKonYTah2
                AngleTah2 = -(Math.Atan2(DpuvKonYTah2 - DpuvZacYTah2, DpuvKonXTah2 - DpuvZacXTah2) * 180 / Math.PI)
                RadTah2.Enabled = True
                '..........................................
            Case 3
                ComboBoxVyber.SelectedItem = 3
                puvZacXTah3 = -80
                puvZacYTah3 = 473
                puvKonXTah3 = 858 - 546 + 5
                puvKonYTah3 = y2 + 6
                DpuvZacXTah3 = puvZacXTah3
                DpuvZacYTah3 = puvZacYTah3
                DpuvKonXTah3 = puvKonXTah3
                DpuvKonYTah3 = puvKonYTah3
                AngleTah3 = -(Math.Atan2(DpuvKonYTah3 - DpuvZacYTah3, DpuvKonXTah3 - DpuvZacXTah3) * 180 / Math.PI)
                RadTah3.Enabled = True
                '..........................................
            Case 5
                ComboBoxVyber.SelectedItem = 5
                puvZacXTah5 = -80
                puvZacYTah5 = 473
                puvKonXTah5 = 895 - 546 + 5
                puvKonYTah5 = y2 + 6
                DpuvZacXTah5 = puvZacXTah5
                DpuvZacYTah5 = puvZacYTah5
                DpuvKonXTah5 = puvKonXTah5
                DpuvKonYTah5 = puvKonYTah5
                AngleTah5 = -(Math.Atan2(DpuvKonYTah5 - DpuvZacYTah5, DpuvKonXTah5 - DpuvZacXTah5) * 180 / Math.PI)
                RadTah5.Enabled = True
                '..........................................
            Case 6
                ComboBoxVyber.SelectedItem = 6
                puvZacXTah6 = -80
                puvZacYTah6 = 473
                puvKonXTah6 = 909 - 546 + 5
                puvKonYTah6 = y2 + 6
                DpuvZacXTah6 = puvZacXTah6
                DpuvZacYTah6 = puvZacYTah6
                DpuvKonXTah6 = puvKonXTah6
                DpuvKonYTah6 = puvKonYTah6
                AngleTah6 = -(Math.Atan2(DpuvKonYTah6 - DpuvZacYTah6, DpuvKonXTah6 - DpuvZacXTah6) * 180 / Math.PI)
                RadTah6.Enabled = True
                '..........................................
            Case 7
                ComboBoxVyber.SelectedItem = 7
                puvZacXTah7 = -80
                puvZacYTah7 = 473
                puvKonXTah7 = 924 - 546 + 5
                puvKonYTah7 = y2 + 6
                DpuvZacXTah7 = puvZacXTah7
                DpuvZacYTah7 = puvZacYTah7
                DpuvKonXTah7 = puvKonXTah7
                DpuvKonYTah7 = puvKonYTah7
                AngleTah7 = -(Math.Atan2(DpuvKonYTah7 - DpuvZacYTah7, DpuvKonXTah7 - DpuvZacXTah7) * 180 / Math.PI)
                RadTah7.Enabled = True
                '..........................................
            Case 8
                ComboBoxVyber.SelectedItem = 8
                puvZacXTah8 = -80
                puvZacYTah8 = 473
                puvKonXTah8 = 938 - 546 + 5
                puvKonYTah8 = y2 + 6
                DpuvZacXTah8 = puvZacXTah8
                DpuvZacYTah8 = puvZacYTah8
                DpuvKonXTah8 = puvKonXTah8
                DpuvKonYTah8 = puvKonYTah8
                AngleTah8 = -(Math.Atan2(DpuvKonYTah8 - DpuvZacYTah8, DpuvKonXTah8 - DpuvZacXTah8) * 180 / Math.PI)
                RadTah8.Enabled = True
                '..........................................
            Case 9
                ComboBoxVyber.SelectedItem = 9
                puvZacXTah9 = -80
                puvZacYTah9 = 473
                puvKonXTah9 = 952 - 546 + 5
                puvKonYTah9 = y2 + 6
                DpuvZacXTah9 = puvZacXTah9
                DpuvZacYTah9 = puvZacYTah9
                DpuvKonXTah9 = puvKonXTah9
                DpuvKonYTah9 = puvKonYTah9
                AngleTah9 = -(Math.Atan2(DpuvKonYTah9 - DpuvZacYTah9, DpuvKonXTah9 - DpuvZacXTah9) * 180 / Math.PI)
                RadTah9.Enabled = True
                '..........................................
            Case 10
                ComboBoxVyber.SelectedItem = 10
                puvZacXTah10 = -80
                puvZacYTah10 = 473
                puvKonXTah10 = 966 - 546 + 5
                puvKonYTah10 = y2 + 6
                DpuvZacXTah10 = puvZacXTah10
                DpuvZacYTah10 = puvZacYTah10
                DpuvKonXTah10 = puvKonXTah10
                DpuvKonYTah10 = puvKonYTah10
                AngleTah10 = -(Math.Atan2(DpuvKonYTah10 - DpuvZacYTah10, DpuvKonXTah10 - DpuvZacXTah10) * 180 / Math.PI)
                RadTah10.Enabled = True
                '..........................................
            Case 12
                ComboBoxVyber.SelectedItem = 12
                puvZacXTah12 = -80
                puvZacYTah12 = 473
                puvKonXTah12 = 999 - 546 + 5
                puvKonYTah12 = y2 + 6
                DpuvZacXTah12 = puvZacXTah12
                DpuvZacYTah12 = puvZacYTah12
                DpuvKonXTah12 = puvKonXTah12
                DpuvKonYTah12 = puvKonYTah12
                AngleTah12 = -(Math.Atan2(DpuvKonYTah12 - DpuvZacYTah12, DpuvKonXTah12 - DpuvZacXTah12) * 180 / Math.PI)
                RadTah12.Enabled = True
                '..........................................
            Case 13
                ComboBoxVyber.SelectedItem = 13
                puvZacXTah13 = -80
                puvZacYTah13 = 473
                puvKonXTah13 = 1013 - 546 + 5
                puvKonYTah13 = y2 + 6
                DpuvZacXTah13 = puvZacXTah13
                DpuvZacYTah13 = puvZacYTah13
                DpuvKonXTah13 = puvKonXTah13
                DpuvKonYTah13 = puvKonYTah13
                AngleTah13 = -(Math.Atan2(DpuvKonYTah13 - DpuvZacYTah13, DpuvKonXTah13 - DpuvZacXTah13) * 180 / Math.PI)
                RadTah13.Enabled = True
                '..........................................
            Case 14
                ComboBoxVyber.SelectedItem = 14
                puvZacXTah14 = -80
                puvZacYTah14 = 473
                puvKonXTah14 = 1027 - 546 + 5
                puvKonYTah14 = y2 + 6
                DpuvZacXTah14 = puvZacXTah14
                DpuvZacYTah14 = puvZacYTah14
                DpuvKonXTah14 = puvKonXTah14
                DpuvKonYTah14 = puvKonYTah14
                AngleTah14 = -(Math.Atan2(DpuvKonYTah14 - DpuvZacYTah14, DpuvKonXTah14 - DpuvZacXTah14) * 180 / Math.PI)
                RadTah14.Enabled = True
                '..........................................
            Case 15
                ComboBoxVyber.SelectedItem = 15
                puvZacXTah15 = -80
                puvZacYTah15 = 473
                puvKonXTah15 = 1065 - 546 + 5
                puvKonYTah15 = y2 + 6
                DpuvZacXTah15 = puvZacXTah15
                DpuvZacYTah15 = puvZacYTah15
                DpuvKonXTah15 = puvKonXTah15
                DpuvKonYTah15 = puvKonYTah15
                AngleTah15 = -(Math.Atan2(DpuvKonYTah15 - DpuvZacYTah15, DpuvKonXTah15 - DpuvZacXTah15) * 180 / Math.PI)
                RadTah15.Enabled = True
                '..........................................
            Case 17
                ComboBoxVyber.SelectedItem = 17
                puvZacXTah17 = -80
                puvZacYTah17 = 473
                puvKonXTah17 = 1092 - 546 + 5
                puvKonYTah17 = y2 + 6
                DpuvZacXTah17 = puvZacXTah17
                DpuvZacYTah17 = puvZacYTah17
                DpuvKonXTah17 = puvKonXTah17
                DpuvKonYTah17 = puvKonYTah17
                AngleTah17 = -(Math.Atan2(DpuvKonYTah17 - DpuvZacYTah17, DpuvKonXTah17 - DpuvZacXTah17) * 180 / Math.PI)
                RadTah17.Enabled = True
                '..........................................
            Case 18
                ComboBoxVyber.SelectedItem = 18
                puvZacXTah18 = -80
                puvZacYTah18 = 473
                puvKonXTah18 = 1106 - 546 + 5
                puvKonYTah18 = y2 + 6
                DpuvZacXTah18 = puvZacXTah18
                DpuvZacYTah18 = puvZacYTah18
                DpuvKonXTah18 = puvKonXTah18
                DpuvKonYTah18 = puvKonYTah18
                AngleTah18 = -(Math.Atan2(DpuvKonYTah18 - DpuvZacYTah18, DpuvKonXTah18 - DpuvZacXTah18) * 180 / Math.PI)
                RadTah18.Enabled = True
                '..........................................
        End Select

    End Sub
    Private Sub ComboBoxVyber_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxVyber.SelectedIndexChanged
        Dim Tah As Integer
        Dim objSelectedItem As Object
        objSelectedItem = ComboBoxVyber.SelectedItem
        Tah = objSelectedItem.ToString()
        '.....................
        If TextBox1.Text = Nothing Then
            MsgBox("zadejte výšku dekorace nebo výšku sufity (150cm)")
            Exit Sub
        End If
        If TextBox2.Text = Nothing Then
            MsgBox("zadejte výšku délku úvazku")
            Exit Sub
        End If
        If TextBox4.Text = Nothing Then
            MsgBox("zadejte výšku zvednutí-spuštění")
            Exit Sub
        End If
        '.....................
        Dim v As Integer
        Select Case Tah
            Case 1
                GraphicsFun = pbTah1.CreateGraphics
                pbTah = pbTah1
                '.........
                If pbTah1.Height > 17 Then
                    v = (454 - pbTah1.Height) / 0.479
                    TextBox3.Text = v
                End If
                '............
                'vDek1 , vUva1 , vPod1, As String  vloží již známé hodnoty při změně tahu v comboboxu
                If vDek1 Is Nothing Then '1111
                    vDek1 = 0
                Else
                    TextBox1.Text = vDek1
                End If
                If vUva1 Is Nothing Then
                    vUva1 = 0
                Else
                    TextBox2.Text = vUva1
                End If
                If vPod1 Is Nothing Then
                    vPod1 = 0
                Else
                    TextBox4.Text = vPod1
                End If
                '.............
            Case 2
                GraphicsFun = pbTah2.CreateGraphics
                pbTah = pbTah2
                '.........
                If pbTah2.Height > 17 Then
                    v = (454 - pbTah2.Height) / 0.479
                    TextBox3.Text = v
                End If
                '............
                If vDek2 Is Nothing Then '1111
                    vDek2 = 0
                Else
                    TextBox1.Text = vDek2
                End If
                If vUva2 Is Nothing Then
                    vUva2 = 0
                Else
                    TextBox2.Text = vUva2
                End If
                If vPod2 Is Nothing Then
                    vPod2 = 0
                Else
                    TextBox4.Text = vPod2
                End If
                '.............
            Case 3
                GraphicsFun = pbTah3.CreateGraphics
                pbTah = pbTah3
                '.........
                If pbTah3.Height > 17 Then
                    v = (454 - pbTah3.Height) / 0.479
                    TextBox3.Text = v
                End If
                '............
                If vDek3 Is Nothing Then '1111
                    vDek3 = 0
                Else
                    TextBox1.Text = vDek3
                End If
                If vUva3 Is Nothing Then
                    vUva3 = 0
                Else
                    TextBox2.Text = vUva3
                End If
                If vPod3 Is Nothing Then
                    vPod3 = 0
                Else
                    TextBox4.Text = vPod3
                End If
                '.............

            Case 5
                GraphicsFun = pbTah5.CreateGraphics
                pbTah = pbTah5
                '.........
                If pbTah5.Height > 17 Then
                    v = (454 - pbTah5.Height) / 0.479
                    TextBox3.Text = v
                End If
                '............
                If vDek5 Is Nothing Then '1111
                    vDek5 = 0
                Else
                    TextBox1.Text = vDek5
                End If
                If vUva5 Is Nothing Then
                    vUva5 = 0
                Else
                    TextBox2.Text = vUva5
                End If
                If vPod5 Is Nothing Then
                    vPod5 = 0
                Else
                    TextBox4.Text = vPod5
                End If
                '.............

            Case 6
                GraphicsFun = pbTah6.CreateGraphics
                pbTah = pbTah6
                '.........
                If pbTah6.Height > 17 Then
                    v = (454 - pbTah6.Height) / 0.479
                    TextBox3.Text = v
                End If
                '............
                If vDek6 Is Nothing Then '1111
                    vDek6 = 0
                Else
                    TextBox1.Text = vDek6
                End If
                If vUva6 Is Nothing Then
                    vUva6 = 0
                Else
                    TextBox2.Text = vUva6
                End If
                If vPod6 Is Nothing Then
                    vPod6 = 0
                Else
                    TextBox4.Text = vPod6
                End If
                '.............

            Case 7
                GraphicsFun = pbTah7.CreateGraphics
                pbTah = pbTah7
                '.........
                If pbTah7.Height > 17 Then
                    v = (454 - pbTah7.Height) / 0.479
                    TextBox3.Text = v
                End If
                '............
                If vDek7 Is Nothing Then '1111
                    vDek7 = 0
                Else
                    TextBox1.Text = vDek7
                End If
                If vUva7 Is Nothing Then
                    vUva7 = 0
                Else
                    TextBox2.Text = vUva7
                End If
                If vPod7 Is Nothing Then
                    vPod7 = 0
                Else
                    TextBox4.Text = vPod7
                End If
                '.............

            Case 8
                GraphicsFun = pbTah8.CreateGraphics
                pbTah = pbTah8
                '.........
                If pbTah8.Height > 17 Then
                    v = (454 - pbTah8.Height) / 0.479
                    TextBox3.Text = v
                End If
                '............
                If vDek8 Is Nothing Then '1111
                    vDek8 = 0
                Else
                    TextBox1.Text = vDek8
                End If
                If vUva8 Is Nothing Then
                    vUva8 = 0
                Else
                    TextBox2.Text = vUva8
                End If
                If vPod8 Is Nothing Then
                    vPod8 = 0
                Else
                    TextBox4.Text = vPod8
                End If
                '.............
            Case 9
                GraphicsFun = pbTah9.CreateGraphics
                pbTah = pbTah9
                '.........
                If pbTah9.Height > 17 Then
                    v = (454 - pbTah9.Height) / 0.479
                    TextBox3.Text = v
                End If
                '............
                If vDek9 Is Nothing Then '1111
                    vDek9 = 0
                Else
                    TextBox1.Text = vDek9
                End If
                If vUva9 Is Nothing Then
                    vUva9 = 0
                Else
                    TextBox2.Text = vUva9
                End If
                If vPod9 Is Nothing Then
                    vPod9 = 0
                Else
                    TextBox4.Text = vPod9
                End If
                '.............

            Case 10
                GraphicsFun = pbTah10.CreateGraphics
                pbTah = pbTah10
                '.........
                If pbTah10.Height > 17 Then
                    v = (454 - pbTah10.Height) / 0.479
                    TextBox3.Text = v
                End If
                '............
                If vDek10 Is Nothing Then '1111
                    vDek10 = 0
                Else
                    TextBox1.Text = vDek10
                End If
                If vUva10 Is Nothing Then
                    vUva10 = 0
                Else
                    TextBox2.Text = vUva10
                End If
                If vPod10 Is Nothing Then
                    vPod10 = 0
                Else
                    TextBox4.Text = vPod10
                End If
                '.............
            Case 12
                GraphicsFun = pbTah12.CreateGraphics
                pbTah = pbTah12
                '.........
                If pbTah12.Height > 17 Then
                    v = (454 - pbTah12.Height) / 0.479
                    TextBox3.Text = v
                End If
                '............
                If vDek12 Is Nothing Then '1111
                    vDek12 = 0
                Else
                    TextBox1.Text = vDek12
                End If
                If vUva12 Is Nothing Then
                    vUva12 = 0
                Else
                    TextBox2.Text = vUva12
                End If
                If vPod12 Is Nothing Then
                    vPod12 = 0
                Else
                    TextBox4.Text = vPod12
                End If
                '.............

            Case 13
                GraphicsFun = pbTah13.CreateGraphics
                pbTah = pbTah13
                '.........
                If pbTah13.Height > 17 Then
                    v = (454 - pbTah13.Height) / 0.479
                    TextBox3.Text = v
                End If
                '............
                If vDek13 Is Nothing Then '1111
                    vDek13 = 0
                Else
                    TextBox1.Text = vDek13
                End If
                If vUva13 Is Nothing Then
                    vUva13 = 0
                Else
                    TextBox2.Text = vUva13
                End If
                If vPod13 Is Nothing Then
                    vPod13 = 0
                Else
                    TextBox4.Text = vPod13
                End If
                '.............

            Case 14
                GraphicsFun = pbTah14.CreateGraphics
                pbTah = pbTah14
                '.........
                If pbTah14.Height > 17 Then
                    v = (454 - pbTah14.Height) / 0.479
                    TextBox3.Text = v
                End If
                '............
                If vDek14 Is Nothing Then '1111
                    vDek14 = 0
                Else
                    TextBox1.Text = vDek14
                End If
                If vUva14 Is Nothing Then
                    vUva14 = 0
                Else
                    TextBox2.Text = vUva14
                End If
                If vPod14 Is Nothing Then
                    vPod14 = 0
                Else
                    TextBox4.Text = vPod14
                End If
                '.............

            Case 15
                GraphicsFun = pbTah15.CreateGraphics
                pbTah = pbTah15
                '.........
                If pbTah15.Height > 17 Then
                    v = (454 - pbTah15.Height) / 0.479
                    TextBox3.Text = v
                End If
                '............
                If vDek15 Is Nothing Then '1111
                    vDek15 = 0
                Else
                    TextBox1.Text = vDek15
                End If
                If vUva15 Is Nothing Then
                    vUva15 = 0
                Else
                    TextBox2.Text = vUva15
                End If
                If vPod15 Is Nothing Then
                    vPod15 = 0
                Else
                    TextBox4.Text = vPod15
                End If
                '.............

            Case 17
                GraphicsFun = pbTah17.CreateGraphics
                pbTah = pbTah17
                '.........
                If pbTah17.Height > 17 Then
                    v = (454 - pbTah17.Height) / 0.479
                    TextBox3.Text = v
                End If
                '............
                If vDek17 Is Nothing Then '1111
                    vDek17 = 0
                Else
                    TextBox1.Text = vDek17
                End If
                If vUva17 Is Nothing Then
                    vUva17 = 0
                Else
                    TextBox2.Text = vUva17
                End If
                If vPod17 Is Nothing Then
                    vPod17 = 0
                Else
                    TextBox4.Text = vPod17
                End If
                '.............

            Case 18
                GraphicsFun = pbTah18.CreateGraphics
                pbTah = pbTah18
                '.........
                If pbTah18.Height > 17 Then
                    v = (454 - pbTah18.Height) / 0.479
                    TextBox3.Text = v
                End If
                '............
                If vDek18 Is Nothing Then '1111
                    vDek18 = 0
                Else
                    TextBox1.Text = vDek18
                End If
                If vUva18 Is Nothing Then
                    vUva18 = 0
                Else
                    TextBox2.Text = vUva18
                End If
                If vPod18 Is Nothing Then
                    vPod18 = 0
                Else
                    TextBox4.Text = vPod18
                End If
                '.............


        End Select

    End Sub

    Private Sub btnZobrazTahy_Click(sender As Object, e As EventArgs) Handles btnZobrazTahy.Click

        '.........................zapnout  ovl.prvky
        PanelZakryj.Visible = False
        PanelZakryj.Height = 13
        PanelZakryj.Width = 23
        PanelZakryj.Location = New Point(60, 4)
        TrackBar1.Enabled = True
        btnVymazVse.Enabled = True
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Enabled = True
        btnZavesit.Enabled = True
        btnZavesit.BackColor = System.Drawing.Color.Silver
        btnUvrat.Enabled = True
        btnUvrat.BackColor = System.Drawing.Color.Silver
        btnPosun1.Enabled = True
        btnPosun1.BackColor = System.Drawing.Color.Silver
        btnOdvesit.Enabled = True
        btnOdvesit.BackColor = System.Drawing.Color.Silver
        ComboBoxVyber.Enabled = True
        ListBoxInfo.Enabled = True
        btnVymazVse.Enabled = True
        btnVymazVse.BackColor = System.Drawing.Color.Silver
        btnUlozScreen.Enabled = True
        btnUlozScreen.BackColor = System.Drawing.Color.Silver
        btnPrint.Enabled = True
        btnPrint.BackColor = System.Drawing.Color.Silver
        btnVetsi.Enabled = True
        btnVetsi.BackColor = System.Drawing.Color.Silver
        btnMensi.Enabled = True
        btnMensi.BackColor = System.Drawing.Color.Silver
        btnVypnout.Enabled = True
        btnVypnout.BackColor = System.Drawing.Color.Silver

        btnOtevritScreen.Enabled = True
        btnOtevritScreen.BackColor = System.Drawing.Color.Silver
        btnlVidit.Enabled = True
        btnlVidit.BackColor = System.Drawing.Color.Silver
        'btnViditBalk.Enabled = True
        'btnViditBalk.BackColor = System.Drawing.Color.Silver
        ' txtInsc.Enabled = True
        'txtSitua.Enabled = True
        '........................
        radTah0.Enabled = True
        '  radTah0.Visible = True
        radTah0.Checked = True
        radTah0.BackColor = System.Drawing.Color.Gray
        ' Labelr0.Visible = True
        ' Labelr19.Visible = True
        ' Labelr19.Visible = True
        radBalk.Enabled = True
        ' radBalk.Visible = True
        radBalk.Checked = True
        radBalk.BackColor = System.Drawing.Color.Gray



        'RadTah1.Enabled = True až po zavěšení dekorace
        'RadTah1.Visible = True
        ' Labelr1.Visible = True

        ' RadTah2.Enabled = True
        ' RadTah2.Visible = True
        'Labelr2.Visible = True

        ' RadTah3.Enabled = True
        'RadTah3.Visible = True
        'Labelr3.Visible = True

        ' RadTah5.Enabled = True
        ' RadTah5.Visible = True
        ' Labelr5.Visible = True

        ' RadTah6.Enabled = True
        'RadTah6.Visible = True
        ' Labelr6.Visible = True

        'RadTah7.Enabled = True
        ' RadTah7.Visible = True
        ' Labelr7.Visible = True

        ' RadTah8.Enabled = True
        ' RadTah8.Visible = True
        'Labelr8.Visible = True

        'RadTah9.Enabled = True
        'RadTah9.Visible = True
        ' Labelr9.Visible = True

        'RadTah10.Enabled = True
        'RadTah10.Visible = True
        'Labelr10.Visible = True

        ' RadTah12.Enabled = True
        ' RadTah12.Visible = True
        ' Labelr12.Visible = True

        'RadTah13.Enabled = True
        ' RadTah13.Visible = True
        ' Labelr13.Visible = True

        ' RadTah14.Enabled = True
        'RadTah14.Visible = True
        'Labelr14.Visible = True

        ' RadTah15.Enabled = True
        ' RadTah15.Visible = True
        'Labelr15.Visible = True

        ' RadTah17.Enabled = True
        ' RadTah17.Visible = True
        ' Labelr17.Visible = True

        ' RadTah18.Enabled = True
        ' RadTah18.Visible = True
        'Labelr18.Visible = True


        ' txtPozn.Enabled = True



        '.........
        pbTah1.Refresh()
        pbTah1.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah1.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah2.Refresh()
        pbTah2.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah2.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah3.Refresh()
        pbTah3.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah3.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah5.Refresh()
        pbTah5.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah5.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah6.Refresh()
        pbTah6.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah6.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah7.Refresh()
        pbTah7.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah7.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah8.Refresh()
        pbTah8.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah8.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah9.Refresh()
        pbTah9.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah9.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah10.Refresh()
        pbTah10.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah10.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah12.Refresh()
        pbTah12.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah12.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah13.Refresh()
        pbTah13.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah13.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah14.Refresh()
        pbTah14.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah14.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah15.Refresh()
        pbTah15.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah15.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah17.Refresh()
        pbTah17.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah17.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah18.Refresh()
        pbTah18.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah18.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................

        ListBoxInfo.Items.Add("  Tah 1      ")
        ListBoxInfo.Items.Add("  Tah 2      ")
        ListBoxInfo.Items.Add("  Tah 3      ")
        ListBoxInfo.Items.Add("  Tah 5      ")
        ListBoxInfo.Items.Add("  Tah 6      ")
        ListBoxInfo.Items.Add("  Tah 7      ")
        ListBoxInfo.Items.Add("  Tah 8      ")
        ListBoxInfo.Items.Add("  Tah 9      ")
        ListBoxInfo.Items.Add("  Tah 10    ")
        ListBoxInfo.Items.Add("  Tah 12    ")
        ListBoxInfo.Items.Add("  Tah 13    ")
        ListBoxInfo.Items.Add("  Tah 14    ")
        ListBoxInfo.Items.Add("  Tah 15    ")
        ListBoxInfo.Items.Add("  Tah 17    ")
        ListBoxInfo.Items.Add("  Tah 18    ")
        '...................
        If TextBox1.Text = 0 Then
            ListBoxDek.Visible = True
            ListBoxDek.Items.Clear()
            ListBoxDek.Items.Add("max.700cm")
        End If
        '...........................
        btnZobrazTahy.Enabled = False
        btnZobrazTahy.BackColor = System.Drawing.Color.White
        btnZobrazTahy.Focus()
        '........................hned zapnuté kreslení
        '.....vypnout kreslení v základu a tady ho zapnout
        ' btnVlozKres.Enabled = True
        Label2k.Visible = True
        PaletteBox.Visible = True
        PaletteBox.Enabled = True
        '............ tlačítka
        Button1.BackColor = Color.Silver
        Button2.BackColor = Color.Silver
        Button3.BackColor = Color.Silver
        Button4.BackColor = Color.Silver
        Button5.BackColor = Color.Silver
        Button6.BackColor = Color.Silver
        Button7.BackColor = Color.Silver
        btnSiluet.BackColor = Color.Silver
        '.................
        pbox.Visible = True
        pbox.Enabled = True
        CreatePalette()
        'nastaví pbox k malování  
        pbox.Font = New Font("Arial", 13)
        pbox.Left = 0
        pbox.Top = 0
        pbox.Width = 706
        pbox.Height = 620
        pbox.Parent = pb1 'vytvoří ho stejný nadřazený!!!
        pbox.BackColor = Color.Transparent 'zprůhledního pro obrázek pb1 a pbTahů '''

        bmp2 = New Bitmap(pbox.Width, pbox.Height) 'čistá bílá plocha
        bmp2.MakeTransparent()  'zajímavé !!!!! ale nelze kreslit
        ' bmp2 = pbox.Image   'nebo uožená nová , podložit do pb1 apod.
        g2 = Graphics.FromImage(bmp2)
        g2.SmoothingMode = SmoothingMode.AntiAlias



    End Sub
    Private Sub btnVymazVse_Click(sender As Object, e As EventArgs) Handles btnVymazVse.Click
        '......................... zobrazit tahy nahoře
        pbTah1.Refresh()
        pbTah1.Height = 17
        pbTah1.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah1.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah2.Refresh()
        pbTah2.Height = 17
        pbTah2.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah2.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah3.Refresh()
        pbTah3.Height = 17
        pbTah3.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah3.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah5.Refresh()
        pbTah5.Height = 17
        pbTah5.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah5.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah6.Refresh()
        pbTah6.Height = 17
        pbTah6.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah6.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah7.Refresh()
        pbTah7.Height = 17
        pbTah7.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah7.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah8.Refresh()
        pbTah8.Height = 17
        pbTah8.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah8.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah9.Refresh()
        pbTah9.Height = 17
        pbTah9.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah9.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah10.Refresh()
        pbTah10.Height = 17
        pbTah10.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah10.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah12.Refresh()
        pbTah12.Height = 17
        pbTah12.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah12.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah13.Refresh()
        pbTah13.Height = 17
        pbTah13.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah13.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah14.Refresh()
        pbTah14.Height = 17
        pbTah14.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah14.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah15.Refresh()
        pbTah15.Height = 17
        pbTah15.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah15.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah17.Refresh()
        pbTah17.Height = 17
        pbTah17.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah17.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        pbTah18.Refresh()
        pbTah18.Height = 17
        pbTah18.CreateGraphics.DrawLine(myPenLano, 5, 0, 5, 13)
        pbTah18.CreateGraphics.DrawEllipse(myPenLano, 3, 13, 3, 3)
        '...............................
        ListBoxInfo.Items.Clear() 'celý
        ListBoxInfo.Items.Add("  Tah 1      ")
        ListBoxInfo.Items.Add("  Tah 2      ")
        ListBoxInfo.Items.Add("  Tah 3      ")
        ListBoxInfo.Items.Add("  Tah 5      ")
        ListBoxInfo.Items.Add("  Tah 6      ")
        ListBoxInfo.Items.Add("  Tah 7      ")
        ListBoxInfo.Items.Add("  Tah 8      ")
        ListBoxInfo.Items.Add("  Tah 9      ")
        ListBoxInfo.Items.Add("  Tah 10    ")
        ListBoxInfo.Items.Add("  Tah 12    ")
        ListBoxInfo.Items.Add("  Tah 13    ")
        ListBoxInfo.Items.Add("  Tah 14    ")
        ListBoxInfo.Items.Add("  Tah 15    ")
        ListBoxInfo.Items.Add("  Tah 17    ")
        ListBoxInfo.Items.Add("  Tah 18    ")
        '.....................
        pbTah.Refresh()
        TextBox1.Text = 0
        TextBox2.Text = 0
        TextBox3.Text = 0
        TextBox4.Text = 0
        '.........................
        ComboBoxVyber.SelectedIndex = 0
        '..............................
        RadTah1.BackColor = System.Drawing.Color.White
        RadTah2.BackColor = System.Drawing.Color.White
        RadTah3.BackColor = System.Drawing.Color.White
        RadTah5.BackColor = System.Drawing.Color.White
        RadTah6.BackColor = System.Drawing.Color.White
        RadTah7.BackColor = System.Drawing.Color.White
        RadTah8.BackColor = System.Drawing.Color.White
        RadTah9.BackColor = System.Drawing.Color.White
        RadTah10.BackColor = System.Drawing.Color.White
        RadTah12.BackColor = System.Drawing.Color.White
        RadTah13.BackColor = System.Drawing.Color.White
        RadTah14.BackColor = System.Drawing.Color.White
        RadTah15.BackColor = System.Drawing.Color.White
        RadTah17.BackColor = System.Drawing.Color.White
        RadTah18.BackColor = System.Drawing.Color.White
        '...............
        radTah0.Checked = True
        radBalk.Checked = True
        pb1.Refresh()
    End Sub
    Private Sub btnSelect_Click(sender As Object, e As EventArgs)
        ListBoxInfo.Items.Add("Tah 1") 'vložení
        ListBoxInfo.Items.Add("Tah 2")
        ListBoxInfo.Items.Add("Tah 3")
        ListBoxInfo.Items.Add("Tah 5")
        ListBoxInfo.Items.Add("Tah 6")
        ListBoxInfo.Items.Add("Tah 7")
        ListBoxInfo.Items.Add("Tah 8")
        ListBoxInfo.Items.Add("Tah 9")
        ListBoxInfo.Items.Add("Tah 10")
        ListBoxInfo.Items.Add("Tah 12")
        ListBoxInfo.Items.Add("Tah 13")
        ListBoxInfo.Items.Add("Tah 14")
        ListBoxInfo.Items.Add("Tah 15")
        ListBoxInfo.Items.Add("Tah 17")
        ListBoxInfo.Items.Add("Tah 18")
    End Sub
    Private Sub btnVypnout_Click(sender As Object, e As EventArgs) Handles btnVypnout.Click
        '............................
        Dim ask As MsgBoxResult = MsgBox("Vypnout kalkulátor ? Výkres bude smazán, pokud nebyl uložen.", MsgBoxStyle.YesNo)
        If ask = MsgBoxResult.Yes Then
            End
        End If
        If ask = MsgBoxResult.No Then
            Exit Sub
        End If
        '............................
        On Error Resume Next
    End Sub


    Private Sub btnUlozScreen_Click(sender As Object, e As EventArgs) Handles btnUlozScreen.Click
        '............netištěné neviditelné

        btnZobrazTahy.Visible = False
        btnVetsi.Visible = False
        btnMensi.Visible = False
        btnVypnout.Visible = False
        btnZobrazTahy.Visible = False
        btnZobrazTahy.Visible = False
        ListBox1.Visible = False
        ListBox2.Visible = False
        ListBox3.Visible = False
        ListBox4.Visible = False
        ListBox5.Visible = False
        ListBox6.Visible = False
        ListBox7.Visible = False
        ListBox8.Visible = False
        ListBoxDek.Visible = False
        ListBoxUvaz.Visible = False
        ListBoxOdpo.Visible = False
        Label5.Visible = False
        ComboBoxVyber.Visible = False
        TextBox1.Visible = False
        TextBox2.Visible = False
        TextBox3.Visible = False
        TextBox4.Visible = False
        btnZavesit.Visible = False
        btnUvrat.Visible = False
        btnPosun1.Visible = False
        btnOdvesit.Visible = False
        btnUlozScreen.Visible = False
        btnOtevritScreen.Visible = False
        'btnPrintScreen.Visible = False
        btnVymazVse.Visible = False
        '.................
        btnPrint.Visible = False
        Label2k.Visible = False
        Label2p.Visible = False
        Label20.Visible = False
        Label21.Visible = False

        btnlVidit.Visible = False
        'btnViditBalk.Visible = False
        radBalk.Visible = False
        radTah0.Visible = False
        Labelr0.Visible = False
        RadTah1.Visible = False
        Labelr1.Visible = False
        RadTah2.Visible = False
        Labelr2.Visible = False
        RadTah3.Visible = False
        Labelr3.Visible = False
        RadTah5.Visible = False
        Labelr5.Visible = False
        RadTah6.Visible = False
        Labelr6.Visible = False
        RadTah7.Visible = False
        Labelr7.Visible = False
        RadTah8.Visible = False
        Labelr8.Visible = False
        RadTah9.Visible = False
        Labelr9.Visible = False
        RadTah10.Visible = False
        Labelr10.Visible = False
        RadTah12.Visible = False
        Labelr12.Visible = False
        RadTah13.Visible = False
        Labelr13.Visible = False
        RadTah14.Visible = False
        Labelr14.Visible = False
        RadTah15.Visible = False
        Labelr15.Visible = False
        RadTah17.Visible = False
        Labelr17.Visible = False
        RadTah18.Visible = False
        Labelr18.Visible = False
        '...........................
        Labelr19.Visible = False
        Label19.Visible = False
        radBalk.Visible = False
        'btnZapKres.Visible = False
        ' btnVypKres.Visible = False
        Button1.Visible = False
        Button2.Visible = False
        Button3.Visible = False
        Button4.Visible = False
        Button5.Visible = False
        Button6.Visible = False
        Button7.Visible = False
        btnSiluet.Visible = False
        GroupBox1.Visible = False
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False

        PaletteBox.Visible = False
        PanelZakryj.Visible = True
        PanelZakryj.Height = PaletteBox.Height + 4
        PanelZakryj.Width = PaletteBox.Width + 48
        PanelZakryj.Location = New Point(PaletteBox.Location.X - 2, PaletteBox.Location.Y - 2)

        Label1k.Visible = False
        TrackBar1.Visible = False
        TextBox2k.Visible = False
        'btnVlozKres.Visible = False
        '...............
        bit = captureScreen(Me.Location.X, Me.Location.Y, Me.Width, Me.Height)
        Dim SaveFileAs As New SaveFileDialog
        SaveFileAs.Title = "Ulož jako"
        SaveFileAs.FileName = "*.jpg"
        SaveFileAs.Filter = "Jpeg |*.jpg"
        If SaveFileAs.ShowDialog() = Windows.Forms.DialogResult.OK Then
            bit.Save(SaveFileAs.FileName, Imaging.ImageFormat.Jpeg)

        End If
        '................... netištěné zpět
        btnZobrazTahy.Visible = True
        btnVetsi.Visible = True
        btnMensi.Visible = True
        btnVypnout.Visible = True
        btnZobrazTahy.Visible = True
        btnZobrazTahy.Visible = True
        ListBox1.Visible = True
        ListBox2.Visible = True
        ListBox3.Visible = True
        ListBox4.Visible = True
        ListBox5.Visible = True
        ListBox6.Visible = True
        ListBox7.Visible = True
        ListBox8.Visible = True
        ListBoxDek.Visible = True
        ListBoxUvaz.Visible = True
        ListBoxOdpo.Visible = True
        Label5.Visible = True
        ComboBoxVyber.Visible = True
        TextBox1.Visible = True
        TextBox2.Visible = True
        TextBox3.Visible = True
        TextBox4.Visible = True
        btnZavesit.Visible = True
        btnUvrat.Visible = True
        btnPosun1.Visible = True
        btnOdvesit.Visible = True
        btnUlozScreen.Visible = True
        btnOtevritScreen.Visible = True
        ' btnPrintScreen.Visible = True
        btnVymazVse.Visible = True
        '.................
        Label2k.Visible = True
        Label2p.Visible = True
        Label20.Visible = True
        Label21.Visible = True
        btnPrint.Visible = True
        btnlVidit.Visible = True
        ' btnViditBalk.Visible = True
        radBalk.Visible = True
        radTah0.Visible = True
        Labelr0.Visible = True
        RadTah1.Visible = True
        Labelr1.Visible = True
        RadTah2.Visible = True
        Labelr2.Visible = True
        RadTah3.Visible = True
        Labelr3.Visible = True
        RadTah5.Visible = True
        Labelr5.Visible = True
        RadTah6.Visible = True
        Labelr6.Visible = True
        RadTah7.Visible = True
        Labelr7.Visible = True
        RadTah8.Visible = True
        Labelr8.Visible = True
        RadTah9.Visible = True
        Labelr9.Visible = True
        RadTah10.Visible = True
        Labelr10.Visible = True
        RadTah12.Visible = True
        Labelr12.Visible = True
        RadTah13.Visible = True
        Labelr13.Visible = True
        RadTah14.Visible = True
        Labelr14.Visible = True
        RadTah15.Visible = True
        Labelr15.Visible = True
        RadTah17.Visible = True
        Labelr17.Visible = True
        RadTah18.Visible = True
        Labelr18.Visible = True
        '...............
        Labelr19.Visible = True
        Label19.Visible = True
        radBalk.Visible = True
        'btnZapKres.Visible = True
        ' btnVypKres.Visible = True
        Button1.Visible = True
        Button2.Visible = True
        Button3.Visible = True
        Button4.Visible = True
        Button5.Visible = True
        Button6.Visible = True
        Button7.Visible = True
        btnSiluet.Visible = True
        GroupBox1.Visible = True
        Panel1.Visible = True
        Panel2.Visible = True
        Panel3.Visible = True

        PaletteBox.Visible = True
        PanelZakryj.Visible = False
        PanelZakryj.Height = 13
        PanelZakryj.Width = 23
        PanelZakryj.Location = New Point(60, PaletteBox.Location.Y - 4)

        Label1k.Visible = True
        TrackBar1.Visible = True
        TextBox2k.Visible = True
        'btnVlozKres.Visible = True

    End Sub
    Private Sub btnOtevritScreen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOtevritScreen.Click
        '...................
        'btnPrintScreen.Enabled = True
        'btnPrintScreen.Visible = True
        'btnPrintScreen.BackColor = System.Drawing.Color.Silver
        'btnZavritScreen.Visible = True
        'btnZavritScreen.Enabled = True
        'btnZavritScreen.BackColor = System.Drawing.Color.Silver
        '...........................

        OpenFileDialog1.Filter = "JPEG files (*.jpg)|*.jpg"
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
            '...................
            PictureBox1.Width = 1264
            PictureBox1.Height = 649
            PictureBox1.Visible = True
            PictureBox1.Enabled = True

            'Get the image name
            'Create a new Bitmap and display it
            PictureBox1.Image = System.Drawing.Bitmap.FromFile(OpenFileDialog1.FileName)
            '........................


        End If

        'vypnutí klávesou ESC
        ' Me.KeyPreview = True
    End Sub
    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then End 'vypnutí klávesou ESC zavřít

    End Sub

    Private Sub PrintDocument2_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument2.PrintPage

        Dim ImgX As Integer
        Dim ImgY As Integer
        Dim PSizeX As Integer
        Dim PSizeY As Integer
        Dim ScaleX As Double
        Dim ScaleY As Double
        Dim RecX As Integer
        Dim RecY As Integer
        Dim ScaleM As Double
        ImgX = Me.PictureBox1.Image.Height
        ImgY = Me.PictureBox1.Image.Width
        If Me.PrintDocument2.DefaultPageSettings.Landscape = False Then
            PSizeX = Me.PrintDocument2.DefaultPageSettings.PaperSize.Height
            PSizeY = Me.PrintDocument2.DefaultPageSettings.PaperSize.Width
        Else
            PSizeX = Me.PrintDocument2.DefaultPageSettings.PaperSize.Width
            PSizeY = Me.PrintDocument2.DefaultPageSettings.PaperSize.Height
        End If
        ScaleX = PSizeX / ImgX
        ScaleY = PSizeY / ImgY
        If ScaleX < ScaleY Then
            ScaleM = ScaleX
        Else : ScaleM = ScaleY
        End If
        RecY = ImgY * ScaleM
        RecX = ImgX * ScaleM
        'zmenšení v proporcích
        Dim zmW As Integer 'šířka k tisku
        Dim zmH As Integer 'výška k tisku
        zmH = RecY * 0.98 'šířka nová menší
        zmW = RecX * 0.98 'výška nová menší

        '.................Print Dialog Code
        e.Graphics.DrawImage(PictureBox1.Image, 0, 0, zmH, zmW)

    End Sub

    Private Sub btnVetsi_Click(sender As Object, e As EventArgs) Handles btnVetsi.Click

        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Maximized
        End If


    End Sub
    Private Sub btnMensi_Click(sender As Object, e As EventArgs) Handles btnMensi.Click
        If Me.WindowState = FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Normal
        End If

    End Sub
    Function captureScreen(ByVal locX As Integer, ByVal locY As Integer, ByVal width As Integer, ByVal height As Integer) As Bitmap
        Dim NewImage As New Bitmap(width, height)
        Using g As Graphics = Graphics.FromImage(NewImage)
            g.CopyFromScreen(locX, locY, 0, 0, NewImage.Size)
        End Using
        Return NewImage
    End Function

    Private Sub btnlVidit_Click(sender As Object, e As EventArgs) Handles btnlVidit.Click
        ' GraphicsCar = pb1.CreateGraphics
        'pb1.Refresh()
        pbox.Image = bmp2
        '............čára viditelnosti z balkonu
        If radBalk.Checked = True Then
            x1b = -80 'hlava diváka na balkonu
            y1b = 209 'hlava diváka od vrchu pb1      
            x2b = 613 'x u horizontu 
            y2b = 272 'výška na horizontu

            ' GraphicsCar.DrawLine(myPenVidB, x1b, y1b, x2b, y2b) 'namaluje čáru viditelnosti z balkonu '!!!!!!!!!!!!! vyměnit u všech čar, zrušit GraphicsCar
            ' pbox.Image = bmp2
            g2.DrawLine(myPenVidB, x1b, y1b, x2b, y2b) 'namaluje čáru viditelnosti z balkonu
        Else
            '......................překryje novou čáru jako prodloužení bílou
            'GraphicsCar.DrawLine(myPenKrycB, x1b, y1b, x2b, y2b)
            g2.DrawLine(myPenKrycB, x1b, y1b, x2b, y2b)
        End If
        radBalk.Checked = False
        '............čáry viditelnosti z 1.řady
        If radTah0.Checked = True Then
            x1o = -80 'hlava diváka od pb1
            y1o = 473 'hlava diváka od vrchu pb1      
            x2o = 413 'x spodek mostu   
            y2o = 28
            g2.DrawLine(myPenVid0, x1o, y1o, x2o, y2o) 'namaluje čáru viditelnosti harlekýn
        Else
            '......................překryje novou čáru jako prodloužení bílou
            g2.DrawLine(myPenKryc0, x1o, y1o, x2o, y2o)
        End If
        radTah0.Checked = False
        '.........................................
        If RadTah1.Checked = True Then
            'namaluje čáru viditelnosti
            g2.DrawLine(myPenPuvTah1, puvZacXTah1, puvZacYTah1, puvKonXTah1, puvKonYTah1)
            '...................... čáru jako prodloužení
            DpuvKonXTah1 = puvKonXTah1
            DpuvKonXTah1 = DpuvKonXTah1 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah1))
            DpuvKonYTah1 = puvKonYTah1
            DpuvKonYTah1 = DpuvKonYTah1 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah1))
            '......
            novKonXTah1 = DpuvKonXTah1
            novKonYTah1 = DpuvKonYTah1
            g2.DrawLine(myPenNovTah1, puvKonXTah1, puvKonYTah1, novKonXTah1, novKonYTah1)
        Else
            '......................překryje původní čáru  čáru bílou '2222
            g2.DrawLine(myPenKrycTah1, puvZacXTah1, puvZacYTah1, puvKonXTah1, puvKonYTah1)
            '......................překryje novou čáru jako prodloužení bílou
            DpuvKonXTah1 = puvKonXTah1
            DpuvKonXTah1 = DpuvKonXTah1 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah1))

            DpuvKonYTah1 = puvKonYTah1
            DpuvKonYTah1 = DpuvKonYTah1 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah1))
            novKonXTah1 = DpuvKonXTah1
            novKonYTah1 = DpuvKonYTah1
            g2.DrawLine(myPenKrycTah1, puvKonXTah1, puvKonYTah1, novKonXTah1, novKonYTah1)
            '.................................
        End If
        RadTah1.Checked = False

        '.........................................
        If RadTah2.Checked = True Then
            'namaluje čáru viditelnosti
            g2.DrawLine(myPenPuvTah2, puvZacXTah2, puvZacYTah2, puvKonXTah2, puvKonYTah2)
            '...................... čáru jako prodloužení
            '50=délka prodlužující čáry
            DpuvKonXTah2 = puvKonXTah2
            DpuvKonXTah2 = DpuvKonXTah2 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah2))
            DpuvKonYTah2 = puvKonYTah2
            DpuvKonYTah2 = DpuvKonYTah2 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah2))
            '......
            novKonXTah2 = DpuvKonXTah2
            novKonYTah2 = DpuvKonYTah2
            g2.DrawLine(myPenNovTah2, puvKonXTah2, puvKonYTah2, novKonXTah2, novKonYTah2)
        Else
            '......................překryje původní čáru  čáru bílou
            g2.DrawLine(myPenKrycTah2, puvZacXTah2, puvZacYTah2, puvKonXTah2, puvKonYTah2)
            '......................překryje novou čáru jako prodloužení bílou
            '50=délka prodlužující čáry
            DpuvKonXTah2 = puvKonXTah2
            DpuvKonXTah2 = DpuvKonXTah2 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah2))

            DpuvKonYTah2 = puvKonYTah2
            DpuvKonYTah2 = DpuvKonYTah2 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah2))
            novKonXTah2 = DpuvKonXTah2
            novKonYTah2 = DpuvKonYTah2
            g2.DrawLine(myPenKrycTah2, puvKonXTah2, puvKonYTah2, novKonXTah2, novKonYTah2)
            '...........
        End If
        RadTah2.Checked = False
        '.........................................
        If RadTah3.Checked = True Then
            'namaluje čáru viditelnosti
            g2.DrawLine(myPenPuvTah3, puvZacXTah3, puvZacYTah3, puvKonXTah3, puvKonYTah3)
            '...................... čáru jako prodloužení
            DpuvKonXTah3 = puvKonXTah3
            DpuvKonXTah3 = DpuvKonXTah3 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah3))
            DpuvKonYTah3 = puvKonYTah3
            DpuvKonYTah3 = DpuvKonYTah3 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah3))
            '......
            novKonXTah3 = DpuvKonXTah3
            novKonYTah3 = DpuvKonYTah3
            g2.DrawLine(myPenNovTah3, puvKonXTah3, puvKonYTah3, novKonXTah3, novKonYTah3)
        Else
            '......................překryje původní čáru  čáru bílou
            g2.DrawLine(myPenKrycTah3, puvZacXTah3, puvZacYTah3, puvKonXTah3, puvKonYTah3)
            '......................překryje novou čáru jako prodloužení bílou
            DpuvKonXTah3 = puvKonXTah3
            DpuvKonXTah3 = DpuvKonXTah3 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah3))

            DpuvKonYTah3 = puvKonYTah3
            DpuvKonYTah3 = DpuvKonYTah3 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah3))
            novKonXTah3 = DpuvKonXTah3
            novKonYTah3 = DpuvKonYTah3
            g2.DrawLine(myPenKrycTah3, puvKonXTah3, puvKonYTah3, novKonXTah3, novKonYTah3)
            '........
        End If
        RadTah3.Checked = False
        '.........................................
        If RadTah5.Checked = True Then
            'namaluje čáru viditelnosti
            g2.DrawLine(myPenPuvTah5, puvZacXTah5, puvZacYTah5, puvKonXTah5, puvKonYTah5)
            '...................... čáru jako prodloužení
            DpuvKonXTah5 = puvKonXTah5
            DpuvKonXTah5 = DpuvKonXTah5 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah5))
            DpuvKonYTah5 = puvKonYTah5
            DpuvKonYTah5 = DpuvKonYTah5 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah5))
            '......
            novKonXTah5 = DpuvKonXTah5
            novKonYTah5 = DpuvKonYTah5
            g2.DrawLine(myPenNovTah5, puvKonXTah5, puvKonYTah5, novKonXTah5, novKonYTah5)
        Else
            '......................překryje původní čáru  čáru bílou
            g2.DrawLine(myPenKrycTah5, puvZacXTah5, puvZacYTah5, puvKonXTah5, puvKonYTah5)
            '......................překryje novou čáru jako prodloužení bílou
            DpuvKonXTah5 = puvKonXTah5
            DpuvKonXTah5 = DpuvKonXTah5 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah5))

            DpuvKonYTah5 = puvKonYTah5
            DpuvKonYTah5 = DpuvKonYTah5 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah5))
            novKonXTah5 = DpuvKonXTah5
            novKonYTah5 = DpuvKonYTah5
            g2.DrawLine(myPenKrycTah5, puvKonXTah5, puvKonYTah5, novKonXTah5, novKonYTah5)
            '................
        End If
        RadTah5.Checked = False
        '.........................................
        If RadTah6.Checked = True Then
            'namaluje čáru viditelnosti
            g2.DrawLine(myPenPuvTah6, puvZacXTah6, puvZacYTah6, puvKonXTah6, puvKonYTah6)
            '...................... čáru jako prodloužení
            DpuvKonXTah6 = puvKonXTah6
            DpuvKonXTah6 = DpuvKonXTah6 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah6))
            DpuvKonYTah6 = puvKonYTah6
            DpuvKonYTah6 = DpuvKonYTah6 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah6))
            '......
            novKonXTah6 = DpuvKonXTah6
            novKonYTah6 = DpuvKonYTah6
            g2.DrawLine(myPenNovTah6, puvKonXTah6, puvKonYTah6, novKonXTah6, novKonYTah6)
        Else
            '......................překryje původní čáru  čáru bílou
            g2.DrawLine(myPenKrycTah6, puvZacXTah6, puvZacYTah6, puvKonXTah6, puvKonYTah6)
            '......................překryje novou čáru jako prodloužení bílou
            DpuvKonXTah6 = puvKonXTah6
            DpuvKonXTah6 = DpuvKonXTah6 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah6))

            DpuvKonYTah6 = puvKonYTah6
            DpuvKonYTah6 = DpuvKonYTah6 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah6))
            novKonXTah6 = DpuvKonXTah6
            novKonYTah6 = DpuvKonYTah6
            g2.DrawLine(myPenKrycTah6, puvKonXTah6, puvKonYTah6, novKonXTah6, novKonYTah6)
            '..............
        End If
        RadTah6.Checked = False
        '.........................................
        If RadTah7.Checked = True Then
            'namaluje čáru viditelnosti
            g2.DrawLine(myPenPuvTah7, puvZacXTah7, puvZacYTah7, puvKonXTah7, puvKonYTah7)
            '...................... čáru jako prodloužení
            DpuvKonXTah7 = puvKonXTah7
            DpuvKonXTah7 = DpuvKonXTah7 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah7))
            DpuvKonYTah7 = puvKonYTah7
            DpuvKonYTah7 = DpuvKonYTah7 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah7))
            '......
            novKonXTah7 = DpuvKonXTah7
            novKonYTah7 = DpuvKonYTah7
            g2.DrawLine(myPenNovTah7, puvKonXTah7, puvKonYTah7, novKonXTah7, novKonYTah7)
        Else
            '......................překryje původní čáru  čáru bílou
            g2.DrawLine(myPenKrycTah7, puvZacXTah7, puvZacYTah7, puvKonXTah7, puvKonYTah7)
            '......................překryje novou čáru jako prodloužení bílou
            DpuvKonXTah7 = puvKonXTah7
            DpuvKonXTah7 = DpuvKonXTah7 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah7))

            DpuvKonYTah7 = puvKonYTah7
            DpuvKonYTah7 = DpuvKonYTah7 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah7))
            novKonXTah7 = DpuvKonXTah7
            novKonYTah7 = DpuvKonYTah7
            g2.DrawLine(myPenKrycTah7, puvKonXTah7, puvKonYTah7, novKonXTah7, novKonYTah7)
            '............
        End If
        RadTah7.Checked = False
        '.........................................
        If RadTah8.Checked = True Then
            'namaluje čáru viditelnosti
            g2.DrawLine(myPenPuvTah8, puvZacXTah8, puvZacYTah8, puvKonXTah8, puvKonYTah8)
            '...................... čáru jako prodloužení
            DpuvKonXTah8 = puvKonXTah8
            DpuvKonXTah8 = DpuvKonXTah8 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah8))
            DpuvKonYTah8 = puvKonYTah8
            DpuvKonYTah8 = DpuvKonYTah8 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah8))
            '......
            novKonXTah8 = DpuvKonXTah8
            novKonYTah8 = DpuvKonYTah8
            g2.DrawLine(myPenNovTah8, puvKonXTah8, puvKonYTah8, novKonXTah8, novKonYTah8)
        Else
            '......................překryje původní čáru  čáru bílou
            g2.DrawLine(myPenKrycTah8, puvZacXTah8, puvZacYTah8, puvKonXTah8, puvKonYTah8)
            '......................překryje novou čáru jako prodloužení bílou
            DpuvKonXTah8 = puvKonXTah8
            DpuvKonXTah8 = DpuvKonXTah8 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah8))

            DpuvKonYTah8 = puvKonYTah8
            DpuvKonYTah8 = DpuvKonYTah8 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah8))
            novKonXTah8 = DpuvKonXTah8
            novKonYTah8 = DpuvKonYTah8
            g2.DrawLine(myPenKrycTah8, puvKonXTah8, puvKonYTah8, novKonXTah8, novKonYTah8)
            '..............
        End If
        RadTah8.Checked = False
        '.........................................
        If RadTah9.Checked = True Then
            'namaluje čáru viditelnosti
            g2.DrawLine(myPenPuvTah9, puvZacXTah9, puvZacYTah9, puvKonXTah9, puvKonYTah9)
            '...................... čáru jako prodloužení
            DpuvKonXTah9 = puvKonXTah9
            DpuvKonXTah9 = DpuvKonXTah9 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah9))
            DpuvKonYTah9 = puvKonYTah9
            DpuvKonYTah9 = DpuvKonYTah9 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah9))
            '......
            novKonXTah9 = DpuvKonXTah9
            novKonYTah9 = DpuvKonYTah9
            g2.DrawLine(myPenNovTah9, puvKonXTah9, puvKonYTah9, novKonXTah9, novKonYTah9)
        Else
            '......................překryje původní čáru  čáru bílou
            g2.DrawLine(myPenKrycTah9, puvZacXTah9, puvZacYTah9, puvKonXTah9, puvKonYTah9)
            '......................překryje novou čáru jako prodloužení bílou
            DpuvKonXTah9 = puvKonXTah9
            DpuvKonXTah9 = DpuvKonXTah9 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah9))

            DpuvKonYTah9 = puvKonYTah9
            DpuvKonYTah9 = DpuvKonYTah9 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah9))
            novKonXTah9 = DpuvKonXTah9
            novKonYTah9 = DpuvKonYTah9
            g2.DrawLine(myPenKrycTah9, puvKonXTah9, puvKonYTah9, novKonXTah9, novKonYTah9)
            '................
        End If
        RadTah9.Checked = False
        '.........................................
        If RadTah10.Checked = True Then
            'namaluje čáru viditelnosti
            g2.DrawLine(myPenPuvTah10, puvZacXTah10, puvZacYTah10, puvKonXTah10, puvKonYTah10)
            '...................... čáru jako prodloužení
            DpuvKonXTah10 = puvKonXTah10
            DpuvKonXTah10 = DpuvKonXTah10 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah10))
            DpuvKonYTah10 = puvKonYTah10
            DpuvKonYTah10 = DpuvKonYTah10 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah10))
            '......
            novKonXTah10 = DpuvKonXTah10
            novKonYTah10 = DpuvKonYTah10
            g2.DrawLine(myPenNovTah10, puvKonXTah10, puvKonYTah10, novKonXTah10, novKonYTah10)
        Else
            '......................překryje původní čáru  čáru bílou
            g2.DrawLine(myPenKrycTah10, puvZacXTah10, puvZacYTah10, puvKonXTah10, puvKonYTah10)
            '......................překryje novou čáru jako prodloužení bílou
            DpuvKonXTah10 = puvKonXTah10
            DpuvKonXTah10 = DpuvKonXTah10 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah10))

            DpuvKonYTah10 = puvKonYTah10
            DpuvKonYTah10 = DpuvKonYTah10 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah10))
            novKonXTah10 = DpuvKonXTah10
            novKonYTah10 = DpuvKonYTah10
            g2.DrawLine(myPenKrycTah10, puvKonXTah10, puvKonYTah10, novKonXTah10, novKonYTah10)
            '............
        End If
        RadTah10.Checked = False
        '.........................................
        If RadTah12.Checked = True Then
            'namaluje čáru viditelnosti
            g2.DrawLine(myPenPuvTah12, puvZacXTah12, puvZacYTah12, puvKonXTah12, puvKonYTah12)
            '...................... čáru jako prodloužení
            DpuvKonXTah12 = puvKonXTah12
            DpuvKonXTah12 = DpuvKonXTah12 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah12))
            DpuvKonYTah12 = puvKonYTah12
            DpuvKonYTah12 = DpuvKonYTah12 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah12))
            '......
            novKonXTah12 = DpuvKonXTah12
            novKonYTah12 = DpuvKonYTah12
            g2.DrawLine(myPenNovTah12, puvKonXTah12, puvKonYTah12, novKonXTah12, novKonYTah12)
        Else
            '......................překryje původní čáru  čáru bílou
            g2.DrawLine(myPenKrycTah12, puvZacXTah12, puvZacYTah12, puvKonXTah12, puvKonYTah12)
            '......................překryje novou čáru jako prodloužení bílou
            DpuvKonXTah12 = puvKonXTah12
            DpuvKonXTah12 = DpuvKonXTah12 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah12))

            DpuvKonYTah12 = puvKonYTah12
            DpuvKonYTah12 = DpuvKonYTah12 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah12))
            novKonXTah12 = DpuvKonXTah12
            novKonYTah12 = DpuvKonYTah12
            g2.DrawLine(myPenKrycTah12, puvKonXTah12, puvKonYTah12, novKonXTah12, novKonYTah12)
            '..............
        End If
        RadTah12.Checked = False
        '.........................................
        If RadTah13.Checked = True Then
            'namaluje čáru viditelnosti
            g2.DrawLine(myPenPuvTah13, puvZacXTah13, puvZacYTah13, puvKonXTah13, puvKonYTah13)
            '...................... čáru jako prodloužení
            DpuvKonXTah13 = puvKonXTah13
            DpuvKonXTah13 = DpuvKonXTah13 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah13))
            DpuvKonYTah13 = puvKonYTah13
            DpuvKonYTah13 = DpuvKonYTah13 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah13))
            '......
            novKonXTah13 = DpuvKonXTah13
            novKonYTah13 = DpuvKonYTah13
            g2.DrawLine(myPenNovTah13, puvKonXTah13, puvKonYTah13, novKonXTah13, novKonYTah13)
        Else
            '......................překryje původní čáru  čáru bílou
            g2.DrawLine(myPenKrycTah13, puvZacXTah13, puvZacYTah13, puvKonXTah13, puvKonYTah13)
            '......................překryje novou čáru jako prodloužení bílou
            DpuvKonXTah13 = puvKonXTah13
            DpuvKonXTah13 = DpuvKonXTah13 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah13))

            DpuvKonYTah13 = puvKonYTah13
            DpuvKonYTah13 = DpuvKonYTah13 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah13))
            novKonXTah13 = DpuvKonXTah13
            novKonYTah13 = DpuvKonYTah13
            g2.DrawLine(myPenKrycTah13, puvKonXTah13, puvKonYTah13, novKonXTah13, novKonYTah13)
            '................
        End If
        RadTah13.Checked = False
        '.........................................
        If RadTah14.Checked = True Then
            'namaluje čáru viditelnosti
            g2.DrawLine(myPenPuvTah14, puvZacXTah14, puvZacYTah14, puvKonXTah14, puvKonYTah14)
            '...................... čáru jako prodloužení
            DpuvKonXTah14 = puvKonXTah14
            DpuvKonXTah14 = DpuvKonXTah14 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah14))
            DpuvKonYTah14 = puvKonYTah14
            DpuvKonYTah14 = DpuvKonYTah14 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah14))
            '......
            novKonXTah14 = DpuvKonXTah14
            novKonYTah14 = DpuvKonYTah14
            g2.DrawLine(myPenNovTah14, puvKonXTah14, puvKonYTah14, novKonXTah14, novKonYTah14)
        Else
            '......................překryje původní čáru  čáru bílou
            g2.DrawLine(myPenKrycTah14, puvZacXTah14, puvZacYTah14, puvKonXTah14, puvKonYTah14)
            '......................překryje novou čáru jako prodloužení bílou
            DpuvKonXTah14 = puvKonXTah14
            DpuvKonXTah14 = DpuvKonXTah14 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah14))

            DpuvKonYTah14 = puvKonYTah14
            DpuvKonYTah14 = DpuvKonYTah14 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah14))
            novKonXTah14 = DpuvKonXTah14
            novKonYTah14 = DpuvKonYTah14
            g2.DrawLine(myPenKrycTah14, puvKonXTah14, puvKonYTah14, novKonXTah14, novKonYTah14)
            '..............
        End If
        RadTah14.Checked = False
        '.........................................
        If RadTah15.Checked = True Then
            'namaluje čáru viditelnosti
            g2.DrawLine(myPenPuvTah15, puvZacXTah15, puvZacYTah15, puvKonXTah15, puvKonYTah15)
            '...................... čáru jako prodloužení
            DpuvKonXTah15 = puvKonXTah15
            DpuvKonXTah15 = DpuvKonXTah15 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah15))
            DpuvKonYTah15 = puvKonYTah15
            DpuvKonYTah15 = DpuvKonYTah15 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah15))
            '......
            novKonXTah15 = DpuvKonXTah15
            novKonYTah15 = DpuvKonYTah15
            g2.DrawLine(myPenNovTah15, puvKonXTah15, puvKonYTah15, novKonXTah15, novKonYTah15)
        Else
            '......................překryje původní čáru  čáru bílou
            g2.DrawLine(myPenKrycTah15, puvZacXTah15, puvZacYTah15, puvKonXTah15, puvKonYTah15)
            '......................překryje novou čáru jako prodloužení bílou
            DpuvKonXTah15 = puvKonXTah15
            DpuvKonXTah15 = DpuvKonXTah15 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah15))

            DpuvKonYTah15 = puvKonYTah15
            DpuvKonYTah15 = DpuvKonYTah15 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah15))
            novKonXTah15 = DpuvKonXTah15
            novKonYTah15 = DpuvKonYTah15
            g2.DrawLine(myPenKrycTah15, puvKonXTah15, puvKonYTah15, novKonXTah15, novKonYTah15)
            '...............
        End If
        RadTah15.Checked = False
        '.........................................
        If RadTah17.Checked = True Then
            'namaluje čáru viditelnosti
            g2.DrawLine(myPenPuvTah17, puvZacXTah17, puvZacYTah17, puvKonXTah17, puvKonYTah17)
            '...................... čáru jako prodloužení
            DpuvKonXTah17 = puvKonXTah17
            DpuvKonXTah17 = DpuvKonXTah17 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah17))
            DpuvKonYTah17 = puvKonYTah17
            DpuvKonYTah17 = DpuvKonYTah17 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah17))
            '......
            novKonXTah17 = DpuvKonXTah17
            novKonYTah17 = DpuvKonYTah17
            g2.DrawLine(myPenNovTah17, puvKonXTah17, puvKonYTah17, novKonXTah17, novKonYTah17)
        Else
            '......................překryje původní čáru  čáru bílou
            g2.DrawLine(myPenKrycTah17, puvZacXTah17, puvZacYTah17, puvKonXTah17, puvKonYTah17)
            '......................překryje novou čáru jako prodloužení bílou
            DpuvKonXTah17 = puvKonXTah17
            DpuvKonXTah17 = DpuvKonXTah17 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah17))

            DpuvKonYTah17 = puvKonYTah17
            DpuvKonYTah17 = DpuvKonYTah17 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah17))
            novKonXTah17 = DpuvKonXTah17
            novKonYTah17 = DpuvKonYTah17
            g2.DrawLine(myPenKrycTah17, puvKonXTah17, puvKonYTah17, novKonXTah17, novKonYTah17)
            '..............
        End If
        RadTah17.Checked = False
        '.........................................
        If RadTah18.Checked = True Then
            'namaluje čáru viditelnosti
            g2.DrawLine(myPenPuvTah18, puvZacXTah18, puvZacYTah18, puvKonXTah18, puvKonYTah18)
            '...................... čáru jako prodloužení
            DpuvKonXTah18 = puvKonXTah18
            DpuvKonXTah18 = DpuvKonXTah18 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah18))
            DpuvKonYTah18 = puvKonYTah18
            DpuvKonYTah18 = DpuvKonYTah18 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah18))
            '......
            novKonXTah18 = DpuvKonXTah18
            novKonYTah18 = DpuvKonYTah18
            g2.DrawLine(myPenNovTah18, puvKonXTah18, puvKonYTah18, novKonXTah18, novKonYTah18)
        Else
            '......................překryje původní čáru  čáru bílou
            g2.DrawLine(myPenKrycTah18, puvZacXTah18, puvZacYTah18, puvKonXTah18, puvKonYTah18)
            '......................překryje novou čáru jako prodloužení bílou
            DpuvKonXTah18 = puvKonXTah18
            DpuvKonXTah18 = DpuvKonXTah18 + 95 * Math.Cos(degreesToRadians(360.0 - AngleTah18))

            DpuvKonYTah18 = puvKonYTah18
            DpuvKonYTah18 = DpuvKonYTah18 + 95 * Math.Sin(degreesToRadians(360.0 - AngleTah18))
            novKonXTah18 = DpuvKonXTah18
            novKonYTah18 = DpuvKonYTah18
            g2.DrawLine(myPenKrycTah18, puvKonXTah18, puvKonYTah18, novKonXTah18, novKonYTah18)
            '...............
        End If
        RadTah18.Checked = False


    End Sub
    Private Function degreesToRadians(ByVal degrees As Double) As Double
        Return (Math.PI / 180.0) * degrees
    End Function
    '............. ošetření čísel v text boxech při psaní
    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Char.IsNumber(e.KeyChar) Then
            Else
                TextBox1.Text = 0
                e.Handled = True

            End If
        End If
        ' ....................
        If Char.IsNumber(e.KeyChar) Then
            Dim newtext As String = TextBox1.Text.Insert(TextBox1.SelectionStart, e.KeyChar.ToString)
            If Not IsNumeric(newtext) OrElse CInt(newtext) > 700 OrElse CInt(newtext) < 0 Then
                TextBox1.Text = 0
                e.Handled = True
                MessageBox.Show("zadejte hodnotu v rozsahu 0-700")
            End If
        End If
    End Sub
    Private Sub TextBox2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Char.IsNumber(e.KeyChar) Then
            Else
                TextBox2.Text = 0
                e.Handled = True

            End If
        End If
        ' ....................
        If Char.IsNumber(e.KeyChar) Then
            Dim newtext As String = TextBox2.Text.Insert(TextBox2.SelectionStart, e.KeyChar.ToString)
            If Not IsNumeric(newtext) OrElse CInt(newtext) > 700 OrElse CInt(newtext) < 0 Then
                TextBox2.Text = 0
                e.Handled = True
                MessageBox.Show("zadejte hodnotu v rozahu 0-700")
            End If
        End If
    End Sub
    Private Sub TextBox4_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Char.IsNumber(e.KeyChar) Then
            Else
                TextBox4.Text = 0
                e.Handled = True

            End If
        End If
        ' ....................
        If Char.IsNumber(e.KeyChar) Then
            Dim newtext As String = TextBox4.Text.Insert(TextBox4.SelectionStart, e.KeyChar.ToString)
            If Not IsNumeric(newtext) OrElse CInt(newtext) > 700 OrElse CInt(newtext) < 0 Then
                TextBox4.Text = 0
                e.Handled = True
                MessageBox.Show("zadejte hodnotu v rozahu 0-700")
            End If
        End If
    End Sub
    '......kreslení !!!

    Sub CreatePalette() 'vytvoření palety barev pro kreslení !!!
        bmp = New Bitmap(PaletteBox.Width, PaletteBox.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)

        Dim rect1 As New Rectangle(0, 0, PaletteBox.Width, PaletteBox.Height)
        Dim lbrush As New LinearGradientBrush(rect1, Color.FromArgb(255, 255, 255, 255), Color.FromArgb(255, 0, 0, 0), LinearGradientMode.Vertical)
        g.FillRectangle(lbrush, rect1)


        Dim rect As New Rectangle(0, 1, 20, 20)
        g.CompositingMode = CompositingMode.SourceOver
        lbrush = New LinearGradientBrush(rect, Color.FromArgb(255, 0, 0, 0), Color.FromArgb(0, 0, 0, 0), LinearGradientMode.Horizontal)
        g.FillRectangle(lbrush, rect)

        rect.Offset(10, 0)
        lbrush = New LinearGradientBrush(rect, Color.FromArgb(0, 0, 0, 0), Color.FromArgb(255, 255, 0, 0), LinearGradientMode.Horizontal)
        g.FillRectangle(lbrush, rect)
        rect.Offset(20, 0)
        lbrush = New LinearGradientBrush(rect, Color.FromArgb(255, 255, 0, 0), Color.FromArgb(0, 0, 0, 0), LinearGradientMode.Horizontal)
        g.FillRectangle(lbrush, rect)


        rect.Offset(10, 0)
        lbrush = New LinearGradientBrush(rect, Color.FromArgb(0, 0, 0, 0), Color.FromArgb(255, 255, 255, 0), LinearGradientMode.Horizontal)
        g.FillRectangle(lbrush, rect)
        rect.Offset(20, 0)
        lbrush = New LinearGradientBrush(rect, Color.FromArgb(255, 255, 255, 0), Color.FromArgb(0, 0, 0, 0), LinearGradientMode.Horizontal)
        g.FillRectangle(lbrush, rect)

        rect.Offset(10, 0)
        lbrush = New LinearGradientBrush(rect, Color.FromArgb(0, 0, 0, 0), Color.FromArgb(255, 0, 255, 0), LinearGradientMode.Horizontal)
        g.FillRectangle(lbrush, rect)
        rect.Offset(20, 0)
        lbrush = New LinearGradientBrush(rect, Color.FromArgb(255, 0, 255, 0), Color.FromArgb(0, 0, 0, 0), LinearGradientMode.Horizontal)
        g.FillRectangle(lbrush, rect)

        rect.Offset(10, 0)
        lbrush = New LinearGradientBrush(rect, Color.FromArgb(0, 0, 0, 0), Color.FromArgb(255, 0, 255, 255), LinearGradientMode.Horizontal)
        g.FillRectangle(lbrush, rect)
        rect.Offset(20, 0)
        lbrush = New LinearGradientBrush(rect, Color.FromArgb(255, 0, 255, 255), Color.FromArgb(0, 0, 0, 0), LinearGradientMode.Horizontal)
        g.FillRectangle(lbrush, rect)

        rect.Offset(10, 0)
        lbrush = New LinearGradientBrush(rect, Color.FromArgb(0, 0, 0, 0), Color.FromArgb(255, 0, 0, 255), LinearGradientMode.Horizontal)
        g.FillRectangle(lbrush, rect)
        rect.Offset(20, 0)
        lbrush = New LinearGradientBrush(rect, Color.FromArgb(255, 0, 0, 255), Color.FromArgb(0, 0, 0, 0), LinearGradientMode.Horizontal)
        g.FillRectangle(lbrush, rect)

        rect.Offset(10, 0)
        lbrush = New LinearGradientBrush(rect, Color.FromArgb(0, 0, 0, 0), Color.FromArgb(255, 255, 0, 255), LinearGradientMode.Horizontal)
        g.FillRectangle(lbrush, rect)
        rect.Offset(20, 0)
        lbrush = New LinearGradientBrush(rect, Color.FromArgb(255, 255, 0, 255), Color.FromArgb(0, 0, 0, 0), LinearGradientMode.Horizontal)
        g.FillRectangle(lbrush, rect)

        rect.Offset(10, 0)
        lbrush = New LinearGradientBrush(rect, Color.FromArgb(0, 0, 0, 0), Color.FromArgb(255, 255, 0, 0), LinearGradientMode.Horizontal)
        g.FillRectangle(lbrush, rect)
        rect.Offset(20, 0)
        lbrush = New LinearGradientBrush(rect, Color.FromArgb(255, 255, 0, 0), Color.FromArgb(0, 0, 0, 0), LinearGradientMode.Horizontal)
        g.FillRectangle(lbrush, rect)

        rect.Offset(10, 0)
        lbrush = New LinearGradientBrush(rect, Color.FromArgb(0, 0, 0, 0), Color.FromArgb(255, 255, 255, 255), LinearGradientMode.Horizontal)
        g.FillRectangle(lbrush, rect)
        PaletteBox.Image = bmp
    End Sub

#Region "Drawing Routines"'HHH
    Sub DrawPath(ByVal e) 'path
        EndX = e.x
        EndY = e.y

        pbox.Refresh()
        pbox.CreateGraphics.DrawLine(nPen, StartX, StartY, EndX, EndY)
    End Sub

    Sub DrawBrush(ByVal e) 'brush
        mpath.AddLine(e.X, e.Y, e.X, e.Y)
        pbox.CreateGraphics.DrawPath(nPen, mpath)
    End Sub

    Sub DrawLine(ByVal e) 'line

        EndX = e.x
        EndY = e.y
        pbox.Refresh()
        pbox.CreateGraphics.DrawLine(nPen, StartX, StartY, EndX, EndY)

    End Sub

    Sub DrawRectangle(ByVal e As System.Windows.Forms.MouseEventArgs)
        xLoc = 0
        yLoc = 0

        If e.X > StartX Then
            BoxWidth = e.X - StartX
            xLoc = StartX
        Else
            BoxWidth = StartX - e.X
            xLoc = e.X
        End If

        If e.Y > StartY Then
            BoxHeight = e.Y - StartY
            yLoc = StartY
        Else
            BoxHeight = StartY - e.Y
            yLoc = e.Y
        End If

        pbox.Refresh()
        Select Case DrawStyles
            Case dStyles.Filled
                pbox.CreateGraphics.FillRectangle(New SolidBrush(clr2), xLoc, yLoc, BoxWidth, BoxHeight)
            Case dStyles.Outline
                pbox.CreateGraphics.DrawRectangle(nPen, xLoc, yLoc, BoxWidth, BoxHeight)
            Case dStyles.OutlineFilled
                pbox.CreateGraphics.FillRectangle(New SolidBrush(clr2), xLoc, yLoc, BoxWidth, BoxHeight)
                pbox.CreateGraphics.DrawRectangle(nPen, xLoc, yLoc, BoxWidth, BoxHeight)
        End Select

    End Sub

    Sub DrawEllipse(ByVal e As System.Windows.Forms.MouseEventArgs) 'ellipse
        xLoc = 0
        yLoc = 0

        If e.X > StartX Then
            BoxWidth = e.X - StartX
            xLoc = StartX
        Else
            BoxWidth = StartX - e.X
            xLoc = e.X
        End If

        If e.Y > StartY Then
            BoxHeight = e.Y - StartY
            yLoc = StartY
        Else
            BoxHeight = StartY - e.Y
            yLoc = e.Y
        End If

        pbox.Refresh()
        Select Case DrawStyles
            Case dStyles.Filled
                pbox.CreateGraphics.FillEllipse(New SolidBrush(clr2), xLoc, yLoc, BoxWidth, BoxHeight)
            Case dStyles.Outline
                pbox.CreateGraphics.DrawEllipse(nPen, xLoc, yLoc, BoxWidth, BoxHeight)
            Case dStyles.OutlineFilled
                pbox.CreateGraphics.FillEllipse(New SolidBrush(clr2), xLoc, yLoc, BoxWidth, BoxHeight)
                pbox.CreateGraphics.DrawEllipse(nPen, xLoc, yLoc, BoxWidth, BoxHeight)
        End Select

    End Sub

    Sub Eraser(ByVal e As System.Windows.Forms.MouseEventArgs) 'guma 
        pbox.Refresh()
        pbox.CreateGraphics.FillRectangle(Brushes.White, e.X - 1, e.Y, penWidth, penWidth)
        pbox.CreateGraphics.DrawRectangle(Pens.Black, e.X - 1, e.Y, penWidth, penWidth)
    End Sub
#End Region

#Region "Events"

    'barvy z palette boxu -obrys výplň 'picks forecolor and backcolor!!!
    Private Sub PaletteBox_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PaletteBox.MouseUp
        On Error Resume Next 'barvy z palette boxu -obrys výplň 'picks forecolor and backcolor   'on left click sets forecolor, on right click sets backcolor

        If e.Button = MouseButtons.Left Then
            clr = Color.FromArgb(bmp.GetPixel(e.X, e.Y).ToArgb)
        Else
            clr2 = Color.FromArgb(bmp.GetPixel(e.X, e.Y).ToArgb)
        End If
        Dim rc As New Rectangle(PaletteBox.Left + PaletteBox.Width + 3, PaletteBox.Top + 3, 40, 16)
        Me.Invalidate(rc)
        nPen = New Pen(clr)
        nPen.Width = penWidth
        If dModes.Text Then
            pbox.Refresh()
            pbox.CreateGraphics.DrawString(txt, Me.Font, New SolidBrush(clr), pF.X, pF.Y)

        End If
    End Sub



    'Private Sub btnVlozKres_Click(sender As Object, e As EventArgs) Handles btnVlozKres.Click 'vypnutí kreslení !!!

    '........................... vložení kreslení do pb1
    'Dim bmpKres As New Bitmap(pbox.Width, pbox.Height)
    '  pbox.DrawToBitmap(bmpKres, New Rectangle(0, 0, pbox.Width, pbox.Height))
    ' pb1.Image = bmpKres
    '....................... asi vyčistit pbox



    '.....................

    ' pbox.Enabled = False
    ' pbox.Visible = False

    'btnVlozKres.Enabled = False
    'Label2k.Visible = False
    'PaletteBox.Visible = False
    'PaletteBox.Enabled = False
    'End Sub

    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint

        ' If Schovej = "ano" Then
        '_e.Graphics.FillRectangle(Brushes.White, PaletteBox.Left - 2, PaletteBox.Top - 1, PaletteBox.Width + 45, PaletteBox.Height + 2)

        '  Else
        e.Graphics.FillRectangle(Brushes.White, PaletteBox.Left - 2, PaletteBox.Top - 1, PaletteBox.Width + 45, PaletteBox.Height + 2)
        e.Graphics.DrawRectangle(New Pen(Color.FromArgb(153, 204, 255)), PaletteBox.Left - 2, PaletteBox.Top - 1, PaletteBox.Width + 46, PaletteBox.Height + 2)

        e.Graphics.FillRectangle(New SolidBrush(clr), PaletteBox.Left + PaletteBox.Width + 4, PaletteBox.Top + 4, 15, 15)
        e.Graphics.DrawRectangle(New Pen(Color.FromArgb(153, 204, 255)), PaletteBox.Left + PaletteBox.Width + 2, PaletteBox.Top + 2, 18, 18)
        e.Graphics.FillRectangle(New SolidBrush(clr2), PaletteBox.Left + PaletteBox.Width + 25, PaletteBox.Top + 4, 15, 15)
        e.Graphics.DrawRectangle(New Pen(Color.FromArgb(153, 204, 255)), PaletteBox.Left + PaletteBox.Width + 23, PaletteBox.Top + 2, 18, 18)
        ' End If
    End Sub

    Private Sub pbSiluet_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbSiluet.MouseDown
        '...................silueta
        nX = pbSiluet.Location.X
        nY = pbSiluet.Location.Y

        If MouseButtons.HasFlag(MouseButtons.Left) = True Then
            pbSiluet.Cursor = Cursors.Hand
        Else
            pbSiluet.Cursor = Cursors.Default
        End If
    End Sub
    Private Sub pbSiluet_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbSiluet.MouseMove
        '....................
        If pbSiluet.Cursor = Cursors.Hand = True Then
            ' pbSiluet.Location = New Point(nX + e.X, nY + e.Y)
            On Error Resume Next
        End If
    End Sub
    Private Sub pbSiluet_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbSiluet.MouseUp
        pbSiluet.Location = New Point(nX + e.X, nY + e.Y)
        On Error Resume Next

    End Sub

    Private Sub btnSiluet_Click(sender As Object, e As EventArgs) Handles btnSiluet.Click
        If btnSiluet.BackColor = Color.Silver Then
            pbSiluet.Enabled = True
            pbSiluet.Visible = True
        Else
            pbSiluet.Enabled = False
            pbSiluet.Visible = False
        End If

    End Sub



    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        'načíst do pictureboxu1 obrázek 
        '....................
        Dim PrintDialog1 As New PrintDialog()
        Try
            If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                PrintDocument2.PrinterSettings = PrintDialog1.PrinterSettings
                PrintDocument2.Print()
                '  prntDoc.Print()
            End If

        Catch ex As Exception
            'Display error message
            MessageBox.Show(ex.Message)
        End Try
    End Sub









    'začne malovat,když se zmáčkne tlačítko myši- neviditelně
    Private Sub pbox_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbox.MouseDown
        If e.Button = MouseButtons.Left Then
            isDraw = True
            StartX = e.X
            StartY = e.Y
            If dmode = dModes.Text Then 'if text mode is selected it creates a caret on the clicked position 

                pF = New PointF(e.X, e.Y - FontHeight / 2)
                If pF.Equals(pFOld) Then
                Else
                    allow = True
                End If
                pbox.Focus()
                CreateCaret(pbox.Handle.ToInt32, 0, 2, Me.FontHeight)
                SetCaretPos(pF.X, pF.Y)
                ShowCaret(pbox.Handle.ToInt32)
            End If
        End If
    End Sub

    'při pohybu myši dává souřadnice
    Private Sub pbox_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbox.MouseMove


        Dim MOP As Integer 'souřadnice výšky
        MOP = -(((e).Y / 0.479) - 914) + 48 'od spodu pbox po podlahu     'Label2k.Text = e.X & ":" & e.Y 'AAA původní i s x
        Select Case MOP
            Case < 0
                Label2k.Text = ""
            Case > 915
                Label2k.Text = ""
            Case Else
                Label2k.Text = "výška: " & MOP & " cm"
        End Select
        '....................................

        Dim MOP2 As Integer 'souřadnice šířky
        MOP2 = (e.X / 0.479) - 505
        Select Case MOP2
            Case < -500
                Label2p.Text = ""
            Case > 960
                Label2p.Text = ""
            Case Else
                Label2p.Text = "délka: " & MOP2 & " cm"
        End Select
        '....................................


        If isDraw Then
            Select Case dmode
                Case dModes.Ellipse
                    DrawEllipse(e)
                Case dModes.Line
                    DrawLine(e)
                Case dModes.Brush
                    DrawBrush(e)
                Case dModes.Rectangle
                    DrawRectangle(e)
                Case dModes.Path
                    DrawPath(e)
                Case dModes.Eraser
                    Eraser(e)
                    g2.FillRectangle(Brushes.White, e.X, e.Y, pWidth, pWidth)
            End Select
        End If
    End Sub



    'prakticky namaluje při puštění tlačítka myši
    Private Sub pbox_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pbox.MouseUp
        isDraw = False
        Select Case dmode
            Case dModes.Ellipse
                Select Case DrawStyles
                    Case dStyles.Filled
                        g2.FillEllipse(New SolidBrush(clr2), xLoc, yLoc, BoxWidth, BoxHeight)
                    Case dStyles.Outline
                        g2.DrawEllipse(nPen, xLoc, yLoc, BoxWidth, BoxHeight)
                    Case dStyles.OutlineFilled
                        g2.FillEllipse(New SolidBrush(clr2), xLoc, yLoc, BoxWidth, BoxHeight)
                End Select
            Case dModes.Line
                g2.DrawLine(nPen, StartX, StartY, EndX, EndY)
            Case dModes.Brush
                g2.DrawPath(nPen, mpath)
                mpath.Reset()
            Case dModes.Rectangle
                Select Case DrawStyles
                    Case dStyles.Filled
                        g2.FillRectangle(New SolidBrush(clr2), xLoc, yLoc, BoxWidth, BoxHeight)
                    Case dStyles.Outline
                        g2.DrawRectangle(nPen, xLoc, yLoc, BoxWidth, BoxHeight)
                    Case dStyles.OutlineFilled
                        g2.FillRectangle(New SolidBrush(clr2), xLoc, yLoc, BoxWidth, BoxHeight)
                        g2.DrawRectangle(nPen, xLoc, yLoc, BoxWidth, BoxHeight)
                End Select
            Case dModes.Path
                mpath.AddLine(StartX, StartY, e.X, e.Y)
                g2.DrawPath(nPen, mpath)
                'mpath.Reset()
            Case dModes.Text
                If allow Then g2.DrawString(txt, Me.Font, New SolidBrush(clr), pFOld)
                allow = False
                txt = ""
        End Select
        pbox.Image = bmp2
    End Sub

    'if in text mode, writes text on the bitmap and moves the caret
    Private Sub pbox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles pbox.KeyPress
        txt += e.KeyChar
        Me.Refresh()
        On Error Resume Next
        If e.KeyChar = Chr(8) Then 'backspace
            txt = txt.Substring(0, txt.Length - 2)
            e.Handled = True
        End If
        g2.PageUnit = GraphicsUnit.Pixel
        SetCaretPos(pF.X + g2.MeasureString(txt, New Font("arial", 8)).Width, pF.Y)
        pbox.CreateGraphics.DrawString(txt, Me.Font, New SolidBrush(clr), pF.X, pF.Y)
        pFOld = pF
    End Sub
#End Region


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        dmode = dModes.Ellipse
        mpath.Reset()
        hide_Caret()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        dmode = dModes.Line
        mpath.Reset()
        hide_Caret()


    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        dmode = dModes.Brush
        mpath.Reset()
        hide_Caret()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        dmode = dModes.Rectangle
        mpath.Reset()
        hide_Caret()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        dmode = dModes.Path
        hide_Caret()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        dmode = dModes.Eraser
        hide_Caret()
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        dmode = dModes.Text
        mpath.Reset()

    End Sub


#Region "Cursors"
    'Sets the appropriate cursor
    Private Sub pbox_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbox.MouseEnter
        Select Case dmode
            Case dModes.Ellipse
                pbox.Cursor = Cursors.Cross
            Case dModes.Line
                pbox.Cursor = Cursors.Cross
            Case dModes.Brush
                pbox.Cursor = Cursors.Cross
            Case dModes.Rectangle
                pbox.Cursor = Cursors.Cross
            Case dModes.Path
                pbox.Cursor = Cursors.Cross
            Case dModes.Eraser
                pbox.Cursor = er
            Case dModes.Text
                pbox.Cursor = Cursors.IBeam
            Case Else
                pbox.Cursor = Cursors.Default
        End Select
    End Sub

    Private Sub pbox_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbox.MouseLeave
        pbox.Cursor = Cursors.Default
    End Sub

    Private Sub PaletteBox_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles PaletteBox.MouseEnter
        PaletteBox.Cursor = c
    End Sub

#End Region

    Function GetEmbeddedFile(ByVal strname As String) As System.IO.Stream
        Return System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceStream(strname)
    End Function

#Region "Brush Width" 'sets the brush width
    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        TextBox2k.Text = TrackBar1.Value
    End Sub

    Private Sub TextBox2k_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2k.TextChanged
        penWidth = CInt(TextBox2k.Text)
        TrackBar1.Value = penWidth
    End Sub
#End Region

#Region "Drawing Styles" 'výplně , obrysy
    Private Sub Panel1_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel1.Click
        DrawStyles = dStyles.Outline
        GroupBox1.Refresh()
        GroupBox1.CreateGraphics.DrawRectangle(Pens.Blue, Panel1.Left - 4, Panel1.Top - 4, Panel1.Width + 7, Panel1.Height + 7)
    End Sub

    Private Sub Panel2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel2.Click
        DrawStyles = dStyles.OutlineFilled
        GroupBox1.Refresh()
        GroupBox1.CreateGraphics.DrawRectangle(Pens.Blue, Panel2.Left - 4, Panel2.Top - 4, Panel2.Width + 7, Panel2.Height + 7)
    End Sub

    Private Sub Panel3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Panel3.Click
        DrawStyles = dStyles.Filled
        GroupBox1.Refresh()
        GroupBox1.CreateGraphics.DrawRectangle(Pens.Blue, Panel3.Left - 4, Panel3.Top - 4, Panel3.Width + 7, Panel3.Height + 7)
    End Sub
#End Region

    Function hide_Caret() 'hides the caret skrýt kursor  křížový
        If txt <> "" Then g2.DrawString(txt, Me.Font, New SolidBrush(clr), pFOld)
        pbox.Image = bmp2
        allow = False
        HideCaret(pbox.Handle.ToInt32)
        txt = ""
        Label2k.Text = ""
    End Function





End Class

