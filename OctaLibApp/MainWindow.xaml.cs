using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Shapes;
using OctaLib;
using System;
using System.ComponentModel;
using System.IO;
using Windows.Storage.Pickers;
using WinRT;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OctaLibApp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            for (int i = 0; i < 16; i++) 
            {
                BanksPanel.Children.Add(new PatternRow());
            };
        }

        private async void OnOpenProject(object sender, RoutedEventArgs e)
        {
            var filePicker = new FolderPicker();

            // Get the current window's HWND by passing in the Window object
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);

            // Associate the HWND with the file picker
            WinRT.Interop.InitializeWithWindow.Initialize(filePicker, hwnd);

            // Use file picker like normal!
            var folder = await filePicker.PickSingleFolderAsync();

            if (folder == null)
            {
                return;

            }

            int version = -1;
            try
            {
                version = ProjectUtils.GetVersion(folder.Path);
            }
            catch (FileNotFoundException)
            {

            }
                
            if (version != 19)
            {
                ContentDialog invalidProject = new ContentDialog()
                {
                    XamlRoot = Content.XamlRoot,
                    Title = "Invalid project directory",
                    Content = "No project file found or project version mismatch.",
                    CloseButtonText = "Ok"
                };

                await invalidProject.ShowAsync();
                return;
            }

            TopText.Text = $"Loaded project: {folder.Path}";

            for (int bankNum = 1; bankNum < 17; bankNum++)
            {
                var patterns = new Patterns();
                patterns.Clear();

                var bankNumStr = bankNum.ToString("00");
                var b = File.ReadAllBytes(folder.Path + $"\\bank{bankNumStr}.strd");

                var partNames = new string[4];
                for (int partNum = 0; partNum < 4; partNum++)
                {
                    partNames[partNum] = BankUtils.ReadPartName(b, partNum);
                }

                for (int patternNum = 0; patternNum < 16; patternNum++)
                {
                    var p = new Pattern();
                    p.Status = BankUtils.ReadPatternActiveState(b, patternNum);
                    p.Number = patternNum + 1;
                    p.PartNumber = BankUtils.ReadPatternPart(b, patternNum) + 1;
                    p.PartName = partNames[p.PartNumber - 1];
                    patterns.Add(p);
                }

                var row = BanksPanel.Children[bankNum - 1].As<PatternRow>();
                row.SetContent(patterns);
            }

        }
    }
}
