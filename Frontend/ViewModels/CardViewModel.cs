using Avalonia.Media;
using Frontend.Views.Controls;
using ReactiveUI;

namespace Frontend.ViewModels;

public class CardViewModel : ReactiveObject {
    public string Header { get; set; }
    public IBrush Background { get; set; }
    
    public CardViewModel() {
    }
}