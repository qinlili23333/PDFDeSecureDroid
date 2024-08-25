using System.Runtime.Versioning;

namespace PDFDeSecureDroid
{
    public partial class MainPage : ContentPage
    {
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
                PickerTitle = "Please select a PDF file",
                FileTypes = customFileType,
            };
            try
            {
                var result = await FilePicker.Default.PickAsync(options);
                if (result != null)
                {
                    FileNameLabel.Text = result.FileName;
                }

                return;
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }
        }
        private async void OnSaveFileClicked(object sender, EventArgs e)
        {
        }
    }
}
