Imports System.Drawing.Printing

Module PrintFormStuff
    Private Declare Auto Function BitBlt Lib "gdi32.dll" (ByVal hdcDest As IntPtr, ByVal nXDest As Integer, ByVal nYDest As Integer, ByVal nWidth As Integer, ByVal nHeight As Integer, ByVal hdcSrc As IntPtr, ByVal nXSrc As Integer, ByVal nYSrc As Integer, ByVal dwRop As System.Int32) As Boolean
    Private Const SRCCOPY As Integer = &HCC0020
    Private Declare Auto Function GetWindowDC Lib "user32" Alias "GetWindowDC" (ByVal hwnd As System.IntPtr) As System.IntPtr

    ' Return an image of the form, with or without
    ' decoration (borders, title bar, etc).
    Public Function GetFormImage(ByVal frm As Form, Optional ByVal with_decoration As Boolean = True) As Bitmap
        ' Get this form's Graphics object.
        Dim me_gr As Graphics = frm.CreateGraphics

        ' See how big to make the result bitmap.
        Dim wid, hgt As Integer
        If with_decoration Then
            wid = frm.Width
            hgt = frm.Height
        Else
            wid = frm.ClientSize.Width
            hgt = frm.ClientSize.Height
        End If

        ' Make a Bitmap to hold the image.
        Dim bm As New Bitmap(wid, hgt, me_gr)
        Dim bm_gr As Graphics = me_gr.FromImage(bm)
        Dim bm_hdc As IntPtr = bm_gr.GetHdc

        ' Get the form's hDC. We must do this after 
        ' creating the new Bitmap, which uses me_gr.
        Dim me_hdc As IntPtr
        If with_decoration Then
            me_hdc = GetWindowDC(frm.Handle)
        Else
            me_hdc = me_gr.GetHdc
        End If

        ' BitBlt the form's image onto the Bitmap.
        BitBlt(bm_hdc, 0, 0, wid, hgt, _
            me_hdc, 0, 0, SRCCOPY)

        ' Clean up.
        bm_gr.ReleaseHdc(bm_hdc)
        If Not with_decoration Then
            me_gr.ReleaseHdc(me_hdc)
        End If

        ' Return the result.
        Return bm
    End Function

#Region "PrintingStuff"
    ' Variables used to print.
    Private m_PrintBitmap As Bitmap
    Private WithEvents m_PrintDocument As PrintDocument

    ' Print the form's image.
    Public Sub PrintForm(ByVal frm As Form, Optional ByVal with_decoration As Boolean = True)
        ' Copy the form's image into a bitmap.
        m_PrintBitmap = GetFormImage(frm, with_decoration)

        ' Make a PrintDocument and print.
        m_PrintDocument = New PrintDocument
        m_PrintDocument.Print()
    End Sub

    ' Print the form image.
    Private Sub m_PrintDocument_PrintPage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles m_PrintDocument.PrintPage
        ' Draw the image centered.
        Dim x As Integer = e.MarginBounds.X + _
            (e.MarginBounds.Width - m_PrintBitmap.Width) \ 2
        Dim y As Integer = e.MarginBounds.Y + _
            (e.MarginBounds.Height - m_PrintBitmap.Height) \ 2
        e.Graphics.DrawImage(m_PrintBitmap, x, y)

        ' There's only one page.
        e.HasMorePages = False
    End Sub
#End Region ' PrintingStuff

#Region "SavingStuff"
    ' Save the picture.
    Public Sub SaveFormImage(ByVal frm As Form, ByVal file_name As String, Optional ByVal with_decoration As Boolean = True, Optional ByVal image_format As System.Drawing.Imaging.ImageFormat = Nothing)
        ' Copy the form's image into a bitmap.
        m_PrintBitmap = GetFormImage(frm, with_decoration)

        ' Save the bitmap.
        If image_format Is Nothing Then
            image_format = System.Drawing.Imaging.ImageFormat.Bmp
        End If
        m_PrintBitmap.Save(file_name, image_format)
    End Sub
#End Region ' SavingStuff

End Module
