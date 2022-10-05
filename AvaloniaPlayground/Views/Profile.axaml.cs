using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaPlayground.ViewModels;
using ReactiveUI;

namespace AvaloniaPlayground.Views;

public partial class Profile : ReactiveUserControl<ProfileViewModel> {
    public Profile() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        this.WhenActivated(disposables => { AvaloniaXamlLoader.Load(this); });
        AvaloniaXamlLoader.Load(this);
    }
}