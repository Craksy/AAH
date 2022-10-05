using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.ReactiveUI;
using Avalonia.Styling;
using AvaloniaPlayground.ViewModels;
using ReactiveUI;

namespace AvaloniaPlayground.Views.Controls;

public partial class Card : ReactiveUserControl<CardViewModel> {
    public new static readonly StyledProperty<Brush> BackgroundProperty =
        AvaloniaProperty.Register<Card, Brush>(nameof(Background));

    public new static readonly StyledProperty<IBrush> ForegroundProperty =
        TextBlock.ForegroundProperty.AddOwner<Card>();

    public static readonly DirectProperty<Card, string> HeaderProperty =
        AvaloniaProperty.RegisterDirect<Card, string>(nameof(Header), o => o.Header, (card, s) => card.Header = s);


    private string _header = string.Empty;

    public string Header {
        get => _header;
        set => SetAndRaise(HeaderProperty, ref _header, value);
    }

    public new IBrush Foreground {
        get => GetValue(ForegroundProperty) ?? Brushes.White;
        set => SetValue(ForegroundProperty, value);
    }

    public new Brush Background {
        get => GetValue(BackgroundProperty);
        set => SetValue(BackgroundProperty, value);
    }

    public Card() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        AvaloniaXamlLoader.Load(this);
        this.WhenActivated(d => { });
    }
}