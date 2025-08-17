namespace MauiApp2;

public partial class DirectionPage : ContentPage
{
    public DirectionPage()
    {
        InitializeComponent();
    }
    private async void IntervalClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MauiApp2.Interval());
    }

    private async void FoundationCliked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MauiApp2.BasicKnoledge());
    }

    private async void ListClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MauiApp2.KeyList());
    }

    private async void TimeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MauiApp2.timeSignature());
    }

    private async void RelativeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MauiApp2.Relative());
    }

    private void OnButtonPressedInterval(object sender, EventArgs e)
    {
        interval.Opacity = 0.7; 
    }

    private void OnButtonReleasedInterval(object sender, EventArgs e)
    {
        interval.Opacity = 1; 
    }

    private void OnButtonPressedTime(object sender, EventArgs e)
    {
        timeSignature.Opacity = 0.7;
    }

    private void OnButtonReleasedTime(object sender, EventArgs e)
    {
        timeSignature.Opacity = 1;
    }

    private void OnButtonPressedBasic(object sender, EventArgs e)
    {
        basic.Opacity = 0.7; 
    }

    private void OnButtonReleasedBasic(object sender, EventArgs e)
    {
        basic.Opacity = 1; 
    }
    private void OnButtonPressedList(object sender, EventArgs e)
    {
        list.Opacity = 0.7; 
    }

    private void OnButtonReleasedList(object sender, EventArgs e)
    {
        list.Opacity = 1; 
    }

    private void OnButtonPressedRelative(object sender, EventArgs e)
    {
        relative.Opacity = 0.7;
    }

    private void OnButtonReleasedRelative(object sender, EventArgs e)
    {
        relative.Opacity = 1;
    }

    private async void BackClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MauiApp2.Lobby());
    }
}
