namespace MauiApp2;

public partial class KeyList : ContentPage
{
	public KeyList()
	{
		InitializeComponent();
	}
    private async void BackClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MauiApp2.DirectionPage());
    }
}
