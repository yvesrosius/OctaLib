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

            //var row = BanksPanel.Children[0].As<PatternRow>();

        }
    }
}
