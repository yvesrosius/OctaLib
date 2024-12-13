using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using DialogHostAvalonia;
using OctaLib;
using OctaLibAvalonia.Models;
using System.Diagnostics;
using System.IO;

namespace OctaLibAvalonia.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    private async void OnOpenProject(object sender, RoutedEventArgs e)
    {
        var options = new FolderPickerOpenOptions();
        options.Title = "Select project folder";

        var topLevel = TopLevel.GetTopLevel(this);
        var result = topLevel.StorageProvider.OpenFolderPickerAsync(options);

        if (result == null)
        {
            return;
        }
        Trace.WriteLine($"Selected folder {result.Result[0].Path.LocalPath}");

        int version = -1;
        var folder = result.Result[0].Path.LocalPath;
        try
        {
            version = ProjectUtils.GetVersion(folder);
        }
        catch (FileNotFoundException)
        {

        }

        if (version != 19)
        {
            await DialogHost.Show(topLevel.Resources["ProjectError"], "MainDialogHost");
            return;
        }

        TopText.Text = $"Loaded project: {folder}";

        var banks = new Banks();

        for (int bankNum = 1; bankNum < 17; bankNum++)
        {
            var patterns = new Patterns();
            patterns.Clear();

            var bankNumStr = bankNum.ToString("00");
            var b = File.ReadAllBytes(folder + $"\\bank{bankNumStr}.strd");

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

            banks[bankNum - 1].Patterns = patterns;

        }

        BankItems.ItemsSource = banks;
    }

}
