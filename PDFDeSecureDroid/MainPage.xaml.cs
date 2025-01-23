using Android.App;
using Android.Content;
using Android.Widget;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace PDFDeSecureDroid
{
    public partial class MainPage : ContentPage
    {
        PdfDocument pdf;

        PdfDocument outpdf;
        static readonly int SaveCallback = 48;


        public MainPage()
        {
            InitializeComponent();
            (Platform.CurrentActivity as MainActivity).Callback = OnActivityResult;
        }

        private static readonly string[] pdftype = ["application/pdf"];
        private async void OnOpenFileClicked(object sender, EventArgs e)
        {
            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, pdftype }, // MIME type
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
                                OpenFile.Text = "解密文件中...(" + current + "/" + pdf.Pages.Count + ")";
                                DecryptProgress.Progress = current / (double)pdf.Pages.Count;
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
                FileNameLabel.Text = "出错了: " + ex.Message;
                SaveFile.IsEnabled = false;
            }
        }


        private void OnSaveFileClicked(object sender, EventArgs e)
        {
            var intent = new Intent(Intent.ActionCreateDocument);
            intent.AddCategory(Intent.CategoryOpenable);
            intent.SetType("application/pdf");
            intent.PutExtra(Intent.ExtraTitle, "Decrypted.pdf");
            Platform.CurrentActivity?.StartActivityForResult(intent, SaveCallback);
        }
        public async void OnActivityResult(int requestCode, Result resultCode, Intent? data)
        {
            if (requestCode == SaveCallback)
            {
                if ((resultCode == Result.Ok) && (data != null))
                {
                    if (data.Data != null)
                    {
                        OpenFile.IsEnabled = false;
                        SaveFile.Text = "保存文件中...";
                        SaveFile.IsEnabled = false;
                        await Task.Run(() =>
                        {
                            Stream buffer = new MemoryStream();
                            outpdf.Save(buffer, false);
                            Stream outputStream = Platform.CurrentActivity.ContentResolver.OpenOutputStream(data.Data);
                            buffer.CopyTo(outputStream);
                            outputStream.Close();
                            buffer.Close();
                            outpdf.Dispose();
                            pdf.Dispose();
                        });
                        SaveFile.IsEnabled = true;
                        OpenFile.IsEnabled = true;
                        SaveFile.Text = "保存解密版本";
                        Toast success = new(Platform.CurrentActivity);
                        success.SetText("解密成功！");
                        success.Show();
                    }
                }
            }
        }
    }
}
