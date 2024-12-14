using OctaLibAvalonia.Models;
using System.Reflection;

namespace OctaLibAvalonia.ViewModels;

public class MainViewModel : ViewModelBase
{
    public Banks Banks {  get; set; }

    public string CurrentVersion
    {
        get
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }

    public MainViewModel()
    {
        Banks = new Banks();
    }

}
