namespace MauiApp2;

public partial class Interval : ContentPage
{
    public Interval()
    {
        InitializeComponent();
    }
    private async void BackClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MauiApp2.DirectionPage());
    }
}
