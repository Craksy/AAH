using System.Reactive;
using ReactiveUI;

namespace Frontend.ViewModels; 

public class LandingPageViewModel : ReactiveObject{
    private int _carouselIndex;

    public int CarouselIndex {
        get => _carouselIndex;
        set {
            _carouselIndex = value%2;
            this.RaisePropertyChanged();
        }
    }

    public ReactiveCommand<Unit, Unit> NextCommand { get; }
    public string NextButtonText => CarouselIndex == 0 ? "Sign Up" : "Back";

    public LandingPageViewModel(){
        NextCommand = ReactiveCommand.Create(() => {
            CarouselIndex++;
        });
    }
}