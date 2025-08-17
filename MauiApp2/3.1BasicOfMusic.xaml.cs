namespace MauiApp2;

public partial class BasicKnoledge : ContentPage
{
    public BasicKnoledge()
    {
        InitializeComponent();
    }
    private async void BackClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MauiApp2.DirectionPage());
    }
}
