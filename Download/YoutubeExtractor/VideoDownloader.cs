using System;
using System.IO;
using System.Net;

namespace YoutubeExtractor
{
    /// <summary>
    /// Provides a method to download a video from YouTube.
    /// </summary>
    public class VideoDownloader : Downloader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VideoDownloader"/> class.
        /// </summary>
        /// <param name="video">The video to download.</param>
        /// <param name="savePath">The path to save the video.</param>
        /// <param name="bytesToDownload">An optional value to limit the number of bytes to download.</param>
        /// <exception cref="ArgumentNullException"><paramref name="video"/> or <paramref name="savePath"/> is <c>null</c>.</exception>
        public VideoDownloader(VideoInfo video, string savePath, int? bytesToDownload = null)
            : base(video, savePath, bytesToDownload)
        { }

        /// <summary>
        /// Occurs when the downlaod progress of the video file has changed.
        /// </summary>
        public event EventHandler<ProgressEventArgs> DownloadProgressChanged;
        /// <summary>
        /// 
        /// </summary>
        public bool boolCancelAsync;
        /// <summary>
        /// 
        /// </summary>
        public void CancelAsync()
        {
            boolCancelAsync = true;
        }
        /// <summary>
        /// Starts the video download.
        /// </summary>
        /// <exception cref="IOException">The video file could not be saved.</exception>
        /// <exception cref="WebException">An error occured while downloading the video.</exception>
        public override void Execute()
        {
            boolCancelAsync = false;
            this.OnDownloadStarted(EventArgs.Empty);

            var request = (HttpWebRequest)WebRequest.Create(this.Video.DownloadUrl);
            //string agentUser = request.Headers["User-Agent"];
            //request.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 6.1; rv:45.0) Gecko/20100101 Firefox/45.0";
            //request.Headers["DNT"] = "1";
            //request.Headers["Acrobat-Version"] = "15.10.0";
            if (this.BytesToDownload.HasValue)
            {
                request.AddRange(0, this.BytesToDownload.Value - 1);
            }
            // the following code is alternative, you may implement the function after your needs
            using (WebResponse response = request.GetResponse())
            {
                using (Stream source = response.GetResponseStream())
                {
                    using (FileStream target = File.Open(this.SavePath, FileMode.Create, FileAccess.Write))
                    {
                        int bufferSize = 1024 * 8;
                        var buffer = new byte[bufferSize];
                        bool cancel = false;
                        int bytes;
                        int copiedBytes = 0;

                        while (!cancel && (bytes = source.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            target.Write(buffer, 0, bytes);

                            copiedBytes += bytes;

                            var eventArgs = new ProgressEventArgs((copiedBytes * 1.0 / response.ContentLength) * 100);

                            if (this.DownloadProgressChanged != null)
                            {
                                this.DownloadProgressChanged(this, eventArgs);

                                if (eventArgs.Cancel | boolCancelAsync == true)
                                {
                                    cancel = true;
                                }
                            }
                        }
                    }
                }
            }

            this.OnDownloadFinished(EventArgs.Empty);
        }
    }
}