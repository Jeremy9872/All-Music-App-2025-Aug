namespace MauiApp2;

public partial class Practice : ContentPage
{
	public Practice()
	{
		InitializeComponent();
	}
    private async void OnFadeButton1Clicked(object sender, EventArgs e)
    {
        
        await Navigation.PushAsync(new MauiApp2.BOMpractice()); // Navigateing to the practice page of users choise
    }

    private async void OnFadeButton2Clicked(object sender, EventArgs e)
    {
        
        await Navigation.PushAsync(new MauiApp2.RhythmPractice());
    }

    private async void OnFadeButton3Clicked(object sender, EventArgs e)
    {
        
        await Navigation.PushAsync(new MauiApp2.IntervalPractice());
    }

    private async void OnFadeButton4Clicked(object sender, EventArgs e)
    {
        
        await Navigation.PushAsync(new MauiApp2.RelativePractice());
    }

    private void OnButtonPressed1(object sender, EventArgs e)
    {
        BOM.Opacity = 0.7; // Intuitive design, users can visually feel that the button is pressed
    }

    private void OnButtonReleased1(object sender, EventArgs e)
    {
        BOM.Opacity = 1; // Intuitive design, users can visually feel that the button is released
    }

    private void OnButtonPressed2(object sender, EventArgs e)
    {
        Rhy.Opacity = 0.7; 
    }

    private void OnButtonReleased2(object sender, EventArgs e)
    {
        Rhy.Opacity = 1; 
    }
    private void OnButtonPressed3(object sender, EventArgs e)
    {
        Int.Opacity = 0.7; 
    }

    private void OnButtonReleased3(object sender, EventArgs e)
    {
        Int.Opacity = 1; 
    }
    
    private void OnButtonPressed4(object sender, EventArgs e)
    {
        Re.Opacity = 0.7; 
    }

    private void OnButtonReleased4(object sender, EventArgs e)
    {
        Re.Opacity = 1; 
    }

    private async void BackClicked(object sender, EventArgs e)
    {

        
        await Navigation.PushAsync(new MauiApp2.Lobby()); // Navigating back to last page
    }
}
