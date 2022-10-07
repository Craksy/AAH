using AutoAuctionProjekt.Classes.Data;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Frontend.ViewModels;
using ReactiveUI;

namespace Frontend.Views; 

public partial class DashboardView : ReactiveUserControl<DashboardViewModel> {
    public DashboardView() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        this.WhenActivated(disposables => {
            ViewModel.CurrentUser = Database.Instance.CurrentUser;
            ViewModel.YourAuctions.Clear();
            ViewModel.YourAuctions.AddRange(Database.Instance.GetYourAuctions());
            ViewModel.RaisePropertyChanged();
        });
        AvaloniaXamlLoader.Load(this);
    }
}