using OctaLibAvalonia.Models;
using ReactiveUI;

namespace OctaLibAvalonia.ViewModels;

public class MainViewModel : ViewModelBase
{
    public Banks Banks {  get; set; }

    public MainViewModel()
    {
        Banks = new Banks();
    }

}
