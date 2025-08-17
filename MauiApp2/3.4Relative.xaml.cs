namespace MauiApp2;

public partial class Relative : ContentPage
{
	public Relative()
	{
		InitializeComponent();
	}
    private async void BackClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MauiApp2.DirectionPage());
    }
}
