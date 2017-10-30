<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.SelectFileButton = New System.Windows.Forms.Button()
        Me.ImagePictureBox = New System.Windows.Forms.PictureBox()
        Me.ResultTextBox = New System.Windows.Forms.TextBox()
        Me.VisionTypeComboBox = New System.Windows.Forms.ComboBox()
        CType(Me.ImagePictureBox,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'SelectFileButton
        '
        Me.SelectFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.SelectFileButton.Location = New System.Drawing.Point(800, 407)
        Me.SelectFileButton.Margin = New System.Windows.Forms.Padding(2)
        Me.SelectFileButton.Name = "SelectFileButton"
        Me.SelectFileButton.Size = New System.Drawing.Size(94, 22)
        Me.SelectFileButton.TabIndex = 2
        Me.SelectFileButton.Text = "Select file..."
        Me.SelectFileButton.UseVisualStyleBackColor = true
        '
        'ImagePictureBox
        '
        Me.ImagePictureBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.ImagePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ImagePictureBox.Location = New System.Drawing.Point(13, 43)
        Me.ImagePictureBox.Name = "ImagePictureBox"
        Me.ImagePictureBox.Size = New System.Drawing.Size(434, 353)
        Me.ImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.ImagePictureBox.TabIndex = 3
        Me.ImagePictureBox.TabStop = false
        '
        'ResultTextBox
        '
        Me.ResultTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.ResultTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ResultTextBox.Location = New System.Drawing.Point(453, 43)
        Me.ResultTextBox.Multiline = true
        Me.ResultTextBox.Name = "ResultTextBox"
        Me.ResultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.ResultTextBox.Size = New System.Drawing.Size(441, 353)
        Me.ResultTextBox.TabIndex = 4
        '
        'VisionTypeComboBox
        '
        Me.VisionTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.VisionTypeComboBox.FormattingEnabled = true
        Me.VisionTypeComboBox.Location = New System.Drawing.Point(13, 12)
        Me.VisionTypeComboBox.Name = "VisionTypeComboBox"
        Me.VisionTypeComboBox.Size = New System.Drawing.Size(434, 21)
        Me.VisionTypeComboBox.TabIndex = 6
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(907, 440)
        Me.Controls.Add(Me.VisionTypeComboBox)
        Me.Controls.Add(Me.ResultTextBox)
        Me.Controls.Add(Me.ImagePictureBox)
        Me.Controls.Add(Me.SelectFileButton)
        Me.Name = "MainForm"
        Me.Text = "Micosoft Cognitive Services with VB - Part 2"
        CType(Me.ImagePictureBox,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub

    Friend WithEvents SelectFileButton As Button
    Friend WithEvents ImagePictureBox As PictureBox
    Friend WithEvents ResultTextBox As TextBox
    Friend WithEvents VisionTypeComboBox As ComboBox
End Class
