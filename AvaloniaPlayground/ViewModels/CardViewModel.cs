using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.ReactiveUI;
using AvaloniaPlayground.Views.Controls;
using ReactiveUI;

namespace AvaloniaPlayground.ViewModels;

public class CardViewModel : ReactiveObject {
    public CardViewModel() {
        Card = new Card();
    }

    public Card Card { get; set; }
}