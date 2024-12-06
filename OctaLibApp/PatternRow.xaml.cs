using Microsoft.UI.Xaml.Controls;
using OctaLib;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OctaLibApp
{
    public sealed partial class PatternRow : UserControl
    {
        public PatternRow()
        {
            this.InitializeComponent();

        }

        public void SetContent(Patterns patterns)
        {
            PatternGrid.ItemsSource = patterns;
        }

    }
}
