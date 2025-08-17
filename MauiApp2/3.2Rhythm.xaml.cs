namespace MauiApp2;

public partial class timeSignature : ContentPage
{
	public timeSignature()
	{
		InitializeComponent();
	}

    private async void BackClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MauiApp2.DirectionPage());
    }
}
