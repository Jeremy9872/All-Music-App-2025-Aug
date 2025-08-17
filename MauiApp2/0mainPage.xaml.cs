using System;
using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnButtonPressed(object sender, EventArgs e)
        {
            FadeButton.Opacity = 0.7; 
        }

        private void OnButtonReleased(object sender, EventArgs e)
        {
            FadeButton.Opacity = 1; 
        }
        private async void OnFadeButtonClicked(object sender, EventArgs e)
        {
            // ??????
            await Task.WhenAll(
                FadeImage.FadeTo(0, 1000),
                FadeButton.FadeTo(0, 1000) 
            );

            // ?????????
            await Navigation.PushAsync(new MauiApp2.Lobby());
        }
        
    }
}

