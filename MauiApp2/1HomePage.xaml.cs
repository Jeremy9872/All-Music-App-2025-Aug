namespace MauiApp2;

public partial class Lobby : ContentPage
{
	public Lobby()
	{
		InitializeComponent();
    }
    private async void derectionClicked(object sender, EventArgs e)
    {
        
        // ?????????
        await Navigation.PushAsync(new MauiApp2.DirectionPage());
    }

    private async void practiceClicked(object sender, EventArgs e)
    {
        
        // ?????????
        await Navigation.PushAsync(new MauiApp2.Practice());
    }

    private async void AIClicked(object sender, EventArgs e)
    {

        // ?????????
        await Navigation.PushAsync(new MauiApp2.AI());
    }


    private void OnButtonPressed1(object sender, EventArgs e)
    {
        direction.Opacity = 0.7; // ????????
    }

    private void OnButtonReleased1(object sender, EventArgs e)
    {
        direction.Opacity = 1; // ??????????
    }

    private void OnButtonPressed2(object sender, EventArgs e)
    {
        practice.Opacity = 0.7; // ????????
    }

    private void OnButtonReleased2(object sender, EventArgs e)
    {
        practice.Opacity = 1; // ??????????
    }

    private void OnButtonPressed3(object sender, EventArgs e)
    {
        AI.Opacity = 0.7; // ????????
    }

    private void OnButtonReleased3(object sender, EventArgs e)
    {
        AI.Opacity = 1; // ??????????
    }

}
