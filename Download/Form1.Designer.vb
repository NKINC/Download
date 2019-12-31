<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.btnDownloadVideo = New System.Windows.Forms.Button()
        Me.txtYoutubeDownloadURL = New System.Windows.Forms.TextBox()
        Me.txtFileName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.btnSelectSaveFile = New System.Windows.Forms.Button()
        Me.btnOpenSaveFolder = New System.Windows.Forms.Button()
        Me.txtYoutubeURL = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnInfo = New System.Windows.Forms.Button()
        Me.cmbFormat = New System.Windows.Forms.ComboBox()
        Me.cmbResolution = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnDownloadVideo
        '
        Me.btnDownloadVideo.Location = New System.Drawing.Point(15, 171)
        Me.btnDownloadVideo.Name = "btnDownloadVideo"
        Me.btnDownloadVideo.Size = New System.Drawing.Size(724, 23)
        Me.btnDownloadVideo.TabIndex = 0
        Me.btnDownloadVideo.Text = "Download using File Async"
        Me.btnDownloadVideo.UseVisualStyleBackColor = True
        '
        'txtYoutubeDownloadURL
        '
        Me.txtYoutubeDownloadURL.Location = New System.Drawing.Point(15, 145)
        Me.txtYoutubeDownloadURL.Name = "txtYoutubeDownloadURL"
        Me.txtYoutubeDownloadURL.Size = New System.Drawing.Size(724, 20)
        Me.txtYoutubeDownloadURL.TabIndex = 1
        '
        'txtFileName
        '
        Me.txtFileName.Location = New System.Drawing.Point(15, 72)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.Size = New System.Drawing.Size(447, 20)
        Me.txtFileName.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 264)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(140, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Status: select a youtube link"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(1, 358)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(750, 23)
        Me.ProgressBar1.TabIndex = 4
        '
        'btnSelectSaveFile
        '
        Me.btnSelectSaveFile.Location = New System.Drawing.Point(468, 69)
        Me.btnSelectSaveFile.Name = "btnSelectSaveFile"
        Me.btnSelectSaveFile.Size = New System.Drawing.Size(79, 23)
        Me.btnSelectSaveFile.TabIndex = 5
        Me.btnSelectSaveFile.Text = "Save as file"
        Me.btnSelectSaveFile.UseVisualStyleBackColor = True
        '
        'btnOpenSaveFolder
        '
        Me.btnOpenSaveFolder.Location = New System.Drawing.Point(553, 69)
        Me.btnOpenSaveFolder.Name = "btnOpenSaveFolder"
        Me.btnOpenSaveFolder.Size = New System.Drawing.Size(108, 23)
        Me.btnOpenSaveFolder.TabIndex = 6
        Me.btnOpenSaveFolder.Text = "Open file location"
        Me.btnOpenSaveFolder.UseVisualStyleBackColor = True
        '
        'txtYoutubeURL
        '
        Me.txtYoutubeURL.Location = New System.Drawing.Point(15, 28)
        Me.txtYoutubeURL.Name = "txtYoutubeURL"
        Me.txtYoutubeURL.Size = New System.Drawing.Size(321, 20)
        Me.txtYoutubeURL.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Youtube URL:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Save As file path"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 129)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Download URL:"
        '
        'btnInfo
        '
        Me.btnInfo.Location = New System.Drawing.Point(399, 25)
        Me.btnInfo.Name = "btnInfo"
        Me.btnInfo.Size = New System.Drawing.Size(63, 23)
        Me.btnInfo.TabIndex = 11
        Me.btnInfo.Text = "Info"
        Me.btnInfo.UseVisualStyleBackColor = True
        '
        'cmbFormat
        '
        Me.cmbFormat.FormattingEnabled = True
        Me.cmbFormat.Items.AddRange(New Object() {"mp4"})
        Me.cmbFormat.Location = New System.Drawing.Point(468, 27)
        Me.cmbFormat.Name = "cmbFormat"
        Me.cmbFormat.Size = New System.Drawing.Size(65, 21)
        Me.cmbFormat.TabIndex = 12
        '
        'cmbResolution
        '
        Me.cmbResolution.FormattingEnabled = True
        Me.cmbResolution.Location = New System.Drawing.Point(539, 28)
        Me.cmbResolution.Name = "cmbResolution"
        Me.cmbResolution.Size = New System.Drawing.Size(132, 21)
        Me.cmbResolution.TabIndex = 13
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(465, 11)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(42, 13)
        Me.Label5.TabIndex = 14
        Me.Label5.Text = "Format:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(536, 12)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(60, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Resolution:"
        '
        'btnLoad
        '
        Me.btnLoad.Location = New System.Drawing.Point(676, 28)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(63, 23)
        Me.btnLoad.TabIndex = 16
        Me.btnLoad.Text = "Load"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(15, 198)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(724, 23)
        Me.Button1.TabIndex = 17
        Me.Button1.Text = "Download using Youtube Downloader"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(342, 25)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(51, 23)
        Me.Button2.TabIndex = 18
        Me.Button2.Text = "Paste"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(15, 225)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(724, 23)
        Me.Button3.TabIndex = 19
        Me.Button3.Text = "Download with TubeNinja (opens web browser)"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(15, 103)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(724, 23)
        Me.Button4.TabIndex = 20
        Me.Button4.Text = "Search Youtube for videos (opens web browser)"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(93, 12)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(50, 13)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "open link"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(751, 379)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmbResolution)
        Me.Controls.Add(Me.cmbFormat)
        Me.Controls.Add(Me.btnInfo)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtYoutubeURL)
        Me.Controls.Add(Me.btnOpenSaveFolder)
        Me.Controls.Add(Me.btnSelectSaveFile)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtFileName)
        Me.Controls.Add(Me.txtYoutubeDownloadURL)
        Me.Controls.Add(Me.btnDownloadVideo)
        Me.Controls.Add(Me.Button3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Download - Youtube"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnDownloadVideo As Button
    Friend WithEvents txtYoutubeDownloadURL As TextBox
    Friend WithEvents txtFileName As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents btnSelectSaveFile As Button
    Friend WithEvents btnOpenSaveFolder As Button
    Friend WithEvents txtYoutubeURL As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents btnInfo As Button
    Friend WithEvents cmbFormat As ComboBox
    Friend WithEvents cmbResolution As ComboBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents btnLoad As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Label7 As Label
End Class
