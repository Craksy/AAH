using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Mixins;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.ReactiveUI;
using Avalonia.Styling;
using Frontend.ViewModels;
using ReactiveUI;

namespace Frontend.Views.Controls;

public partial class Card : ReactiveUserControl<CardViewModel> {
    public static readonly StyledProperty<IBrush> BackgroundProperty =
        Border.BackgroundProperty.AddOwner<Card>();

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

    public new IBrush Background {
        get => GetValue(BackgroundProperty);
        set => SetValue(BackgroundProperty, value);
    }

    public Card() {
        InitializeComponent();
    }

    private void InitializeComponent() {
        this.WhenActivated(d => {
            this.Bind(ViewModel, vm => vm.Header, v => v.Header).DisposeWith(d);
            this.Bind(ViewModel, vm => vm.Background, v => v.Background).DisposeWith(d);
        });
        AvaloniaXamlLoader.Load(this);
    }
}