﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Frontend.ViewModels;
using ReactiveUI;

namespace Frontend.Views;

public partial class AuctionPage : ReactiveUserControl<AuctionPageViewModel> {
    public AuctionPage() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        this.WhenActivated(disposables => { AvaloniaXamlLoader.Load(this); });
        AvaloniaXamlLoader.Load(this);
    }
}