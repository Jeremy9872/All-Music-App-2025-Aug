using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiApp2;

public partial class AI : ContentPage
{
    private readonly HttpClient _client = new()
    {
        Timeout = TimeSpan.FromMinutes(5) // ??? 5 ??
    };

    public AI()
    {
        InitializeComponent();
    }

    private void OnButtonPressed(object sender, EventArgs e)
    {
        askButton.Opacity = 0.7;
    }

    private void OnButtonReleased(object sender, EventArgs e)
    {
        askButton.Opacity = 1;
    }

    private bool IsMusicRelated(string prompt)
    {
        string[] keywords = { "sharp","flat","music", "chord", "scale", "note", "timeSignature", "melody", "harmony", "interval", "tempo", "pitch", "key signature", "clef", "staff","minor","major" };
        return keywords.Any(k => prompt.Contains(k, StringComparison.OrdinalIgnoreCase));
    }
    private async void OnSendClicked(object sender, EventArgs e)
    {

        var prompt = UserInput.Text;
        if (string.IsNullOrWhiteSpace(prompt))
        {
            ResponseLabel.Text = "Please enter a question.";
            return;
        }

        ResponseLabel.Text = "Thinking...";

        try
        {
            var reply = await AskOllama(prompt);
            if (!IsMusicRelated(prompt))
            {
                reply += "\n\n(This question feels beyond the scope of our course, but I answered it anyway.)";
            }
            ResponseLabel.Text = reply;
        }
        catch (Exception ex)
        {
            ResponseLabel.Text = "Error: " + ex.Message;
        }
    }

    private async Task<string> AskOllama(string prompt)
    {
        var requestData = new
        {
            model = "mistral",
            stream = false, // <<<<<< ?? stream ??,???? JSON ??
            messages = new[]
            {
                new { role = "user", content = prompt }
            }
        };

        var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("http://localhost:11434/api/chat", content);
        var responseString = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(responseString);
        if (doc.RootElement.TryGetProperty("message", out var message))
        {
            return message.GetProperty("content").GetString()?.Trim() ?? "[Empty response]";
        }

        return "[Invalid response format]";
    }
    private async void BackClicked(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new MauiApp2.Lobby());
    }

}

