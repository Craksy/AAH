using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Frontend.ViewModels;

namespace Frontend.Views; 

public partial class LandingPageView : ReactiveUserControl<LandingPageViewModel> {
    public LandingPageView() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }
}