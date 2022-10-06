using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Frontend.ViewModels;
using ReactiveUI;

namespace Frontend.Views;

public partial class ProfileView : ReactiveUserControl<ProfileViewModel> {
    public ProfileView() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        this.WhenActivated(disposables => { AvaloniaXamlLoader.Load(this); });
        AvaloniaXamlLoader.Load(this);
    }
}