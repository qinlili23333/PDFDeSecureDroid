using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using System;
using System.Runtime.Versioning;

namespace PDFDeSecureDroid
{
    public partial class MainPage : ContentPage
    {
        PdfDocument pdf = new PdfDocument();

        PdfDocument outpdf = new PdfDocument();
        public MainPage()
        {
            InitializeComponent();
        }
        private async void OnOpenFileClicked(object sender, EventArgs e)
        {
            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, new[] { "application/pdf" } }, // MIME type
                });

            PickOptions options = new()
            {
                PickerTitle = "选择一个PDF文件",
                FileTypes = customFileType,
            };
            try
            {
                var result = await FilePicker.Default.PickAsync(options);
                if (result != null)
                {
                    FileNameLabel.Text = result.FileName;
                    OpenFile.IsEnabled = false;
                    OpenFile.Text = "读取文件中...";
                    SaveFile.IsEnabled = false;
                    DecryptProgress.Progress = 0;
                    await Task.Run(async () =>
                    {
                        Stream fileStream = await result.OpenReadAsync();
                        pdf = PdfReader.Open(fileStream, PdfDocumentOpenMode.Import);
                        int current = 0;
                        foreach (PdfPage page in pdf.Pages)
                        {
                            outpdf.AddPage(page);
                            current++;
                            MainThread.BeginInvokeOnMainThread(() =>
                            {
                                OpenFile.Text = "解密文件中...("+current+"/"+ pdf.Pages.Count + ")";
                                DecryptProgress.Progress = (double)current / (double)pdf.Pages.Count ;
                            });
                        }
                        fileStream.Close();
                    });
                    SaveFile.IsEnabled = true;
                    OpenFile.IsEnabled = true;
                    OpenFile.Text = "打开PDF文件";
                }
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
                FileNameLabel.Text = "出错了: "+ex.Message;
                SaveFile.IsEnabled = false;
            }
        }
        private async void OnSaveFileClicked(object sender, EventArgs e)
        {
        }
    }
}
