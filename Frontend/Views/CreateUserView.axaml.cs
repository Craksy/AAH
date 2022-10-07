using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Frontend.ViewModels;

namespace Frontend.Views; 

public partial class CreateUserView : ReactiveUserControl<CreateUserViewModel> {
    public CreateUserView() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
    }
}