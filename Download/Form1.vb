Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Text.RegularExpressions
Imports YoutubeExtractor
Public Class Form1
    Dim wc As New System.Net.WebClient()
    Private Shared Function ValidateRemoteCertificate(ByVal sender As Object, ByVal certificate As System.Security.Cryptography.X509Certificates.X509Certificate, ByVal chain As System.Security.Cryptography.X509Certificates.X509Chain, ByVal sslPolicyErrors As System.Net.Security.SslPolicyErrors) As Boolean
        If sslPolicyErrors = System.Net.Security.SslPolicyErrors.None Then
            Return True
        End If
        Return False
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnDownloadVideo.Click
        Try
            If btnDownloadVideo.Text = "Stop" Then
                wc.CancelAsync()
                btnDownloadVideo.Text = "Download File Async"
                ProgressBar1.Value = 100
            Else
                ProgressBar1.Value = 0
                btnDownloadVideo.Text = "Stop"
                wc = New System.Net.WebClient()
                AddHandler wc.DownloadFileCompleted, AddressOf AsyncCompletedEventHandler
                AddHandler wc.DownloadProgressChanged, AddressOf DownloadProgressChangedEventHandler
                Try
                    If Not System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(txtFileName.Text)) Then
                        System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(txtFileName.Text))
                    End If
                Catch ex2 As Exception
                    Err.Clear()
                End Try
                If System.IO.File.Exists(txtFileName.Text) Then
                    System.IO.File.Delete(txtFileName.Text)
                End If
                System.Net.ServicePointManager.ServerCertificateValidationCallback = AddressOf ValidateRemoteCertificate
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls
                wc.DownloadFileAsync(New Uri(txtYoutubeDownloadURL.Text), txtFileName.Text)
            End If
        Catch ex As Exception
            Label1.Text = "Err: " & ex.Message & Environment.NewLine & ex.StackTrace.ToString()
            Err.Clear()
        End Try
    End Sub
    Public strFilePathSaveAs As String = ""
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnSelectSaveFile.Click
        Try
            Dim fn As String = txtFileName.Text
            Dim fs As New SaveFileDialog()
            fs.Filter = cmbFormat.Text & "|" & cmbFormat.Text & "|*.*|*.*" '".mp4|.mp4|*.*|*.*"
            fs.FilterIndex = 0
            'fs.DefaultExt = "mp4"
            Dim initialDirectory As String = GetSetting(Application.ProductName, "Settings", "InitialDirectory", CStr(Application.StartupPath))
            If Not String.IsNullOrEmpty(fn) Then
                If Directory.Exists(Path.GetDirectoryName(fn)) Then
                    fs.InitialDirectory = Path.GetDirectoryName(fn)
                Else
                    'fs.InitialDirectory = Application.StartupPath
                End If
                fs.FileName = System.IO.Path.GetFileName(txtFileName.Text)
            Else
                fs.InitialDirectory = initialDirectory
            End If
            Try
                If System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(txtFileName.Text)) Then
                    fs.InitialDirectory = System.IO.Path.GetDirectoryName(txtFileName.Text)
                End If
            Catch ex2 As Exception
                Err.Clear()
            End Try
            Select Case fs.ShowDialog(Me)
                Case DialogResult.OK, DialogResult.Yes
                    strFilePathSaveAs = fs.FileName & ""
                    txtFileName.Text = fs.FileName & ""
                    Try
                        SaveSetting(Application.ProductName, "Settings", "InitialDirectory", CStr(Path.GetDirectoryName(fs.FileName)))
                    Catch ex As Exception
                        Err.Clear()
                    End Try
                Case Else

            End Select
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles btnOpenSaveFolder.Click
        Try
            If Not System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(txtFileName.Text)) Then
                Process.Start(Application.StartupPath.TrimEnd("\"c) & "\"c)
            Else
                Process.Start(System.IO.Path.GetDirectoryName(txtFileName.Text))
            End If
        Catch ex2 As Exception
            Err.Clear()
        End Try
    End Sub
    Public videoInfos As System.Collections.Generic.List(Of VideoInfo) = Nothing
    Public video As VideoInfo = Nothing
    Public videoInfosTemp1 As New System.Collections.Generic.List(Of strucVideo)
    Public Structure strucVideo
        Public Id As String
        Public Title As String
        Public Description As String
        Public Format As String
        Public Author As String
        Public Duration As TimeSpan
        Public Keywords As List(Of String)
        Public UploadDate As DateTimeOffset
        Public Width As Integer
        Public Height As Integer
        Public VideoQuality As YoutubeExplode.Models.MediaStreams.VideoQuality
        Public VideoQualityLabel As String
        Public VideoEncoding As YoutubeExplode.Models.MediaStreams.VideoEncoding
        Public MuxStreamInfoObject As YoutubeExplode.Models.MediaStreams.MuxedStreamInfo
    End Structure
    Public Function getStrucVideoMax(ByRef strucVideoObjects As System.Collections.Generic.List(Of strucVideo)) As strucVideo
        If Not strucVideoObjects Is Nothing Then
            If strucVideoObjects.Count > 0 Then
                Dim strucVideoMax As New strucVideo()
                Try
                    For Each strucVideoObj As strucVideo In strucVideoObjects
                        If String.IsNullOrEmpty(strucVideoMax.Id & "") Then
                            If strucVideoObj.Format.ToString().ToLower().Contains(cmbFormat.Items(cmbFormat.SelectedIndex).ToString().ToLower().TrimStart(".")) Then
                                strucVideoMax = strucVideoObj
                            End If
                        Else
                            If strucVideoObj.Format.ToString().ToLower().Contains(cmbFormat.Items(cmbFormat.SelectedIndex).ToString().ToLower().TrimStart(".")) Then
                                If strucVideoObj.MuxStreamInfoObject.Resolution.Height >= strucVideoMax.MuxStreamInfoObject.Resolution.Height Then
                                    strucVideoMax = strucVideoObj
                                End If
                            End If
                        End If
                    Next
                Catch ex As Exception
                    strucVideoMax = strucVideoObjects(0)
                    Err.Clear()
                End Try
                Return strucVideoMax
            End If
        End If
        Return Nothing
    End Function
    Public Function getStrucVideo(ByRef strucVideoObjects As System.Collections.Generic.List(Of strucVideo), recommendedHeight As Integer) As strucVideo
        If Not strucVideoObjects Is Nothing Then
            If strucVideoObjects.Count > 0 Then
                Dim strucVideoMax As New strucVideo()
                Try
                    For Each strucVideoObj As strucVideo In strucVideoObjects
                        If String.IsNullOrEmpty(strucVideoMax.Id & "") Then
                            If strucVideoObj.Format.ToString().ToLower().Contains(cmbFormat.Items(cmbFormat.SelectedIndex).ToString().ToLower().TrimStart(".")) Then
                                strucVideoMax = strucVideoObj
                            End If
                        Else
                            If strucVideoObj.Format.ToString().ToLower().Contains(cmbFormat.Items(cmbFormat.SelectedIndex).ToString().ToLower().TrimStart(".")) Then
                                If strucVideoObj.MuxStreamInfoObject.Resolution.Height >= recommendedHeight Then
                                    strucVideoMax = strucVideoObj
                                End If
                            End If
                        End If
                    Next
                Catch ex As Exception
                    strucVideoMax = strucVideoObjects(0)
                    Err.Clear()
                End Try
                Return strucVideoMax
            End If
        End If
        Return Nothing
    End Function

    Public Async Function getVideosAsyncYoutubeExplode() As Task(Of System.Collections.Generic.List(Of strucVideo))
        Try
            enableButtons()
            Dim link As String = New Uri(txtYoutubeURL.Text.Trim()).ToString()
            Dim id As String = YoutubeExplode.YoutubeClient.ParseVideoId(txtYoutubeURL.Text.Trim())
            Dim client As New YoutubeExplode.YoutubeClient()
            Dim videoTask As Task(Of YoutubeExplode.Models.Video) = client.GetVideoAsync(id)
            Dim video As YoutubeExplode.Models.Video = Await videoTask

            cmbFormat.Items.Clear()
            cmbResolution.Items.Clear()
            Dim resHighest As Integer = 0

            Dim title As String = video.Title
            Dim author As String = video.Author
            Dim duration As TimeSpan = video.Duration
            Dim streamInfoSetTask As Task(Of YoutubeExplode.Models.MediaStreams.MediaStreamInfoSet) = client.GetVideoMediaStreamInfosAsync(video.Id)
            Dim streamInfoSet As YoutubeExplode.Models.MediaStreams.MediaStreamInfoSet = Await streamInfoSetTask
            Dim mediaStreamInfo As YoutubeExplode.Models.MediaStreams.MediaStreamInfo = YoutubeExplode.Models.MediaStreams.Extensions.WithHighestVideoQuality(streamInfoSet.Muxed)
            Dim streamInfo As YoutubeExplode.Models.MediaStreams.MediaStreamInfo = Nothing
            Dim strUR1 As String = mediaStreamInfo.Url
            Dim mediaFormats As New List(Of String)
            mediaFormats.Add(".*")
            videoInfosTemp1 = New System.Collections.Generic.List(Of strucVideo)
            For Each streamResolution As YoutubeExplode.Models.MediaStreams.MuxedStreamInfo In streamInfoSet.Muxed.ToArray
                Dim mediaFormat As String = YoutubeExplode.Models.MediaStreams.Extensions.GetFileExtension(streamResolution.Container).ToString.ToLower()
                If mediaFormats.Contains(mediaFormat) Or mediaFormats.Contains(".*") Then
                    Dim videStruc As New strucVideo()
                    videStruc.Title = title
                    videStruc.Format = mediaFormat
                    videStruc.MuxStreamInfoObject = streamResolution
                    videStruc.Description = video.Description
                    videStruc.Author = video.Author
                    videStruc.Duration = video.Duration
                    videStruc.Id = video.Id
                    videStruc.Width = streamResolution.Resolution.Width
                    videStruc.VideoQualityLabel = streamResolution.VideoQualityLabel
                    videStruc.Height = streamResolution.Resolution.Height
                    videStruc.Keywords = video.Keywords.ToList
                    videStruc.UploadDate = video.UploadDate
                    videStruc.VideoQuality = streamResolution.VideoQuality
                    videStruc.VideoEncoding = streamResolution.VideoEncoding
                    videoInfosTemp1.Add(videStruc)
                    Dim vidInfo As New YoutubeExtractor.VideoInfo(YoutubeExtractor.VideoType.Mp4, title, streamResolution.Url, streamResolution.Resolution.Height)
                    Dim streamMaxResolution As YoutubeExplode.Models.MediaStreams.MuxedStreamInfo = Nothing
                    If Not cmbFormat.Items.Contains(mediaFormat) Then
                        cmbFormat.Items.Add(mediaFormat)
                    End If
                    If cmbFormat.Items.Count > 0 Then
                        If cmbFormat.Items.Contains("mp4") Then
                            cmbFormat.SelectedItem = "mp4"
                        ElseIf cmbFormat.SelectedIndex < 0 And cmbFormat.Items.Count > 0 Then
                            cmbFormat.SelectedIndex = 0
                        End If
                    End If
                    If streamInfo Is Nothing Then
                        streamInfo = streamResolution
                        streamMaxResolution = streamInfo
                    ElseIf streamResolution.Resolution.Height >= streamResolution.Resolution.Height Then
                        streamInfo = streamResolution
                        streamMaxResolution = streamInfo
                    End If
                    If Not cmbResolution.Items.Contains(streamResolution.VideoQualityLabel.ToString()) Then
                        cmbResolution.Items.Add(streamResolution.VideoQualityLabel.ToString())
                        If CInt(streamResolution.Resolution.Height.ToString()) >= resHighest Then
                            resHighest = CInt(streamResolution.Resolution.Height.ToString())
                            cmbResolution.SelectedIndex = cmbResolution.Items.Count - 1
                        End If
                    End If
                End If
            Next
            Return videoInfosTemp1
        Catch ex As Exception
            Err.Clear()
        End Try
        Return Nothing
    End Function
    Private Async Sub btnInfo_Click(sender As Object, e As EventArgs) Handles btnInfo.Click
        Try
            videoInfosTemp1 = Await getVideosAsyncYoutubeExplode()
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Private Sub DownloadAudio(ByVal videoInfos As IEnumerable(Of VideoInfo))
        video = videoInfos.Where(Function(info) info.CanExtractAudio).OrderByDescending(Function(info) info.AudioBitrate).First()
        If video.RequiresDecryption Then
            DownloadUrlResolver.DecryptDownloadUrl(video)
        End If
    End Sub
    Private Function getVideo(ByVal videoInfos As IEnumerable(Of VideoInfo)) As VideoInfo
        Dim videoType As YoutubeExtractor.VideoType = VideoType.Unknown
        Dim videoResolution As Integer = CInt(cmbResolution.Text) + 0
        If videoResolution <= 0 Then
            videoResolution = 1080
        End If
        Select Case CStr("."c & cmbFormat.Text.ToString.ToLower.TrimStart("."))
            Case ".mp4"
                videoType = VideoType.Mp4
            Case ".webm"
                videoType = VideoType.WebM
            Case ".swf"
                videoType = VideoType.Flash
            Case Else
                videoType = VideoType.Mp4
        End Select

        Return videoInfos.First(Function(info) info.VideoType = videoType AndAlso info.Resolution = videoResolution)
    End Function
    Public videoDownloader As VideoDownloader = Nothing
    Private Sub DownloadVideo(ByVal videoInfos As IEnumerable(Of VideoInfo))
        Dim videoType As YoutubeExtractor.VideoType = VideoType.Unknown
        Dim videoResolution As Integer = CInt(cmbResolution.Text) + 0
        If videoResolution <= 0 Then
            videoResolution = 1080
        End If
        Select Case CStr("."c & cmbFormat.Text.ToString.ToLower.TrimStart("."))
            Case ".mp4"
                videoType = VideoType.Mp4
            Case ".webm"
                videoType = VideoType.WebM
            Case ".swf"
                videoType = VideoType.Flash
            Case Else
                videoType = VideoType.Unknown
        End Select
        video = videoInfos.First(Function(info) info.VideoType = videoType AndAlso info.Resolution = videoResolution)
        If video.RequiresDecryption Then
            DownloadUrlResolver.DecryptDownloadUrl(video)
        End If
        videoDownloader = New VideoDownloader(video, txtFileName.Text)
        AddHandler videoDownloader.DownloadProgressChanged, AddressOf YoutubeDownloadProgressChangedEventHandler
        videoDownloader.Execute()
    End Sub

    Private Function RemoveIllegalPathCharacters(ByVal path As String) As String
        Dim regexSearch As String = New String(System.IO.Path.GetInvalidFileNameChars()) & New String(System.IO.Path.GetInvalidPathChars())
        Dim r = New Regex(String.Format("[{0}]", Regex.Escape(regexSearch)))
        path = r.Replace(path, "-"c).ToString().Replace(" ", "-").Replace("--", "-").Replace("--", "-").Replace("--", "-")
        r = New Regex("[^a-zA-Z0-9]")
        Return r.Replace(path, "-"c).ToString().Replace(" ", "-").Replace(" ", "-").Replace("--", "-").Replace("--", "-").Replace("--", "-").Replace("-.", "."c).TrimEnd("-"c)
    End Function
    Private Sub YoutubeDownloadProgressChangedEventHandler(ByVal sender As Object, ByVal e As ProgressEventArgs)
        If cancelYouTubeAsyncDownload Then
            Button1.Text = "Download Youtube File"
            e.Cancel = True
        End If
        ProgressBar1.Value = e.ProgressPercentage
        Label1.Text = "Downloading: " & IIf(ProgressBar1.Value < 10, "0", "") & ProgressBar1.Value & "% - " & CInt(CSng(videoBytesLength * (e.ProgressPercentage / 100)) / 1024 / 1024) & " MB" & " - " & txtFileName.Text
        Me.Update()
        Application.DoEvents()
        If ProgressBar1.Value = 100 Then
            Button1.Text = "Download Youtube File"
        End If
    End Sub
    Private Sub DownloadProgressChangedEventHandler(ByVal sender As Object, ByVal e As System.Net.DownloadProgressChangedEventArgs)
        ProgressBar1.Value = e.ProgressPercentage
        Label1.Text = "Downloading: " & IIf(ProgressBar1.Value < 10, "0", "") & ProgressBar1.Value & "% - " & CInt(CSng(videoBytesLength * (e.ProgressPercentage / 100)) / 1024 / 1024) & " MB" & " - " & txtFileName.Text
    End Sub

    Private Sub AsyncCompletedEventHandler(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        'MessageBox.Show("Download completed!")
        Label1.Text = "100% Download completed! - " & txtFileName.Text & ""
        Using fs1 As FileStream = System.IO.File.OpenRead(txtFileName.Text)
            Label1.Text &= Environment.NewLine & "File Size: " & CInt(fs1.Length / 1024 / 1024) & " MB"
            ProgressBar1.Value = 100
            btnDownloadVideo.Text = "Download using File Async"
            fs1.Close()
            fs1.Dispose()
        End Using
        If Not e.Error Is Nothing Then
            If e.Error.Message.ToLower = "The remote server returned an error: (403) Forbidden.".ToLower Then
                Label1.Text = "Download Error: " & " - " & txtFileName.Text & Environment.NewLine & e.Error.Message
                If System.IO.File.Exists(txtFileName.Text) Then
                    System.IO.File.Delete(txtFileName.Text)
                End If
            End If
        End If
    End Sub
    Private Sub ProgressBar1_Click(sender As Object, e As EventArgs) Handles ProgressBar1.Click

    End Sub

    Public videoBytesLength As Integer = 0
    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Try
            videoBytesLength = 0
            enableButtons()
            Dim videoMax As strucVideo = getStrucVideoMax(videoInfosTemp1)
            Dim initialDirectory As String = GetSetting(Application.ProductName, "Settings", "InitialDirectory", CStr(Application.StartupPath)).ToString().TrimEnd("\"c) & "\"
            txtFileName.Text = initialDirectory & RemoveIllegalPathCharacters(videoMax.Title) & "."c & cmbFormat.Items(cmbFormat.SelectedIndex).ToString.TrimStart("."c) & ""
            txtYoutubeDownloadURL.Text = videoMax.MuxStreamInfoObject.Url.ToString()
            txtYoutubeDownloadURL.ForeColor = Color.DarkGreen
            btnDownloadVideo.Visible = True
            Button1.Visible = True
            Button3.Visible = True
            Label1.Text = "Status: Success - Click a download button to procceed."
            Label1.Text &= Environment.NewLine & "File Size: " & CInt(videoMax.MuxStreamInfoObject.Size / 1024 / 1024) & " MB"
            videoBytesLength = CInt(videoMax.MuxStreamInfoObject.Size)

        Catch ex As Exception
            Label1.Text = "Status: Error - " & ex.Message.ToString() & Environment.NewLine & "info: " & ex.StackTrace.ToString()
            Err.Clear()
        End Try
    End Sub
    Public cancelYouTubeAsyncDownload As Boolean = False
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If Button1.Text = "Stop" Then
                cancelYouTubeAsyncDownload = True
                Button1.Text = "Download Youtube File"
                ProgressBar1.Value = 100
            Else
                ProgressBar1.Value = 0
                cancelYouTubeAsyncDownload = False
                Button1.Text = "Stop"
                DownloadVideo(videoInfos)
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
    Public Sub enableButtons()
        btnDownloadVideo.Visible = True
        Button1.Visible = True
        Button3.Visible = True
    End Sub
    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            If Clipboard.ContainsText Then
                txtYoutubeURL.Text = Clipboard.GetText()
            Else
                txtYoutubeURL.Text = ""
            End If
            btnInfo_Click(Me, New EventArgs())
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub

    Public Shared Function isUrlValid(ByVal s As String) As Boolean
        Try
            Dim u As Uri
            If Uri.TryCreate(s, UriKind.Absolute, u) Then
                If Uri.IsWellFormedUriString(s, UriKind.Absolute) Then
                    If u.Host.ToString.ToLower.Contains("youtube.com".ToLower) Then
                        Return True
                    End If
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
        Return False
    End Function
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            If Clipboard.ContainsText Then
                If isUrlValid(Clipboard.GetText) Then
                    Dim strURL As String = Clipboard.GetText.ToString()
                    If Not String.IsNullOrEmpty(strURL & "") Then
                        txtYoutubeURL.Text = strURL
                        btnInfo_Click(Me, New EventArgs())
                    End If
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Process.Start("https://www.tubeninja.net/?url=" & txtYoutubeURL.Text)
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            Dim inputSearchString As String = InputBox("Type search words:", "Search Youtube:", "")
            If Not String.IsNullOrEmpty(inputSearchString.Trim()) Then
                inputSearchString = inputSearchString.ToString().Replace(" ", "+").Replace("&", "&amp;")
                Process.Start("https://www.youtube.com/results?search_query=" & inputSearchString)
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub

    Private Sub cmbResolution_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbResolution.SelectedIndexChanged
        Try
            If cmbResolution.Items.Count > 0 Then
                If cmbFormat.Items.Count > 0 Then
                    Try
                        Dim selRes As Integer = 0
                        selRes = CInt((cmbResolution.Items(cmbResolution.SelectedIndex).ToString).Trim("p"c).Trim())
                        If selRes > 0 Then
                            Dim videoMax As strucVideo = getStrucVideo(videoInfosTemp1, selRes)
                            Dim initialDirectory As String = GetSetting(Application.ProductName, "Settings", "InitialDirectory", CStr(Application.StartupPath)).ToString().TrimEnd("\"c) & "\"
                            txtFileName.Text = initialDirectory & RemoveIllegalPathCharacters(videoMax.Title) & "."c & cmbFormat.Items(cmbFormat.SelectedIndex).ToString.TrimStart("."c) & ""
                            txtYoutubeDownloadURL.Text = videoMax.MuxStreamInfoObject.Url.ToString()
                            txtYoutubeDownloadURL.ForeColor = Color.DarkGreen
                            Dim selIndex As Integer = -1
                            btnDownloadVideo.Visible = True
                            Button1.Visible = True
                            Button3.Visible = True
                            Label1.Text = "Status: Success - Click a download button to procceed."
                            Label1.Text &= Environment.NewLine & "File Size: " & CInt(videoMax.MuxStreamInfoObject.Size / 1024 / 1024) & " MB"
                            videoBytesLength = CInt(videoMax.MuxStreamInfoObject.Size)
                        End If
                    Catch ex2 As Exception
                        Err.Clear()
                    End Try
                End If
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub

    Private Sub txtYoutubeURL_TextChanged(sender As Object, e As EventArgs) Handles txtYoutubeURL.TextChanged

    End Sub

    Private Sub cmbFormat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFormat.SelectedIndexChanged
        Try
            Dim videoMax As strucVideo = getStrucVideoMax(videoInfosTemp1)
            Dim initialDirectory As String = GetSetting(Application.ProductName, "Settings", "InitialDirectory", CStr(Application.StartupPath)).ToString().TrimEnd("\"c) & "\"
            txtFileName.Text = initialDirectory & RemoveIllegalPathCharacters(videoMax.Title) & "."c & cmbFormat.Items(cmbFormat.SelectedIndex).ToString.TrimStart("."c) & ""
            txtYoutubeDownloadURL.Text = videoMax.MuxStreamInfoObject.Url.ToString()
            txtYoutubeDownloadURL.ForeColor = Color.DarkGreen
            Dim selIndex As Integer = -1
            For Each resolutionSel As String In cmbResolution.Items
                selIndex += 1
                If CInt(videoMax.Height) = CInt(resolutionSel.Trim("p"c).Trim()) Then
                    cmbResolution.SelectedIndex = selIndex
                    Exit For
                End If
            Next
            btnDownloadVideo.Visible = True
            Button1.Visible = True
            Button3.Visible = True
            Label1.Text = "Status: Success - Click a download button to procceed."
            Label1.Text &= Environment.NewLine & "File Size: " & CInt(videoMax.MuxStreamInfoObject.Size / 1024 / 1024) & " MB"
            videoBytesLength = CInt(videoMax.MuxStreamInfoObject.Size)
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        Try
            If Not String.IsNullOrEmpty(txtYoutubeURL.Text & "") Then
                Process.Start(txtYoutubeURL.Text)
            Else
                txtYoutubeURL.Text = "https://www.youtube.com/watch?v=BBgghnQF6E4"
                Process.Start(txtYoutubeURL.Text)
                btnInfo_Click(Me, New EventArgs)
            End If
        Catch ex As Exception
            Err.Clear()
        End Try
    End Sub
End Class
