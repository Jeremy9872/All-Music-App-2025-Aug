using Microsoft.Maui.Controls;
using System.Diagnostics;
using System.Linq;

namespace MauiApp2
{
    public partial class BOMpractice : ContentPage
    {
        private List<Question> allQuestions;
        private List<Question> selectedQuestions;
        private List<bool> firstAnswerStatus; // ????????
        private List<bool> finalAnswerStatus; // ????????
        private int currentQuestionIndex = 0;
        private int correctCount = 0;
        private Stopwatch stopwatch;
        private List<double> responseTimes;
        private double totalElapsedTime = 0; // ?????

        public BOMpractice()
        {
            InitializeComponent();
            InitializeQuestions();
            stopwatch = new Stopwatch();
            responseTimes = new List<double>();
        }

        private void InitializeQuestions()
        {
            allQuestions = new List<Question>
            {
                new Question { Text = "How many beats is an eighth rest worth?", Options = new List<string> { "1 beat", "2 beats", "1/2 beat", "1/4 beat" }, CorrectAnswer = "1/2 beat" },
                new Question { Text = "Which of the following symbols means “1 beat of silence”?", Options = new List<string> { "??", "??", "??", "??" }, CorrectAnswer = "??" },
                new Question { Text = "What symbol raises pitch by a half step?", Options = new List<string> { "?", "?", "?", "??" }, CorrectAnswer = "?" },
                new Question { Text = "What symbol lowers pitch by a half step?", Options = new List<string> { "?", "?", "?", "??" }, CorrectAnswer = "?" },
                new Question { Text = "What does the natural (?) symbol do?", Options = new List<string> { "Doubles the pitch", "Lowers the pitch", "Cancels previous sharps or flats", "Adds 2 beats" }, CorrectAnswer = "Cancels previous sharps or flats" },
                new Question { Text = "Which note gets 4 beats?", Options = new List<string> { "Quarter note", "Half note", "Whole note", "Eighth note" }, CorrectAnswer = "Whole note" },
                new Question { Text = "Which note gets 2 beats?", Options = new List<string> { "Whole note", "Half note", "Quarter note", "Eighth note" }, CorrectAnswer = "Half note" },
                new Question { Text = "Which note gets 1 beat?", Options = new List<string> { "Half note", "Quarter note", "Whole note", "Sixteenth note" }, CorrectAnswer = "Quarter note" },
                new Question { Text = "Which note gets 1/2 beat?", Options = new List<string> { "Quarter note", "Whole note", "Eighth note", "Sixteenth note" }, CorrectAnswer = "Eighth note" },
                new Question { Text = "Which note gets 1/4 beat?", Options = new List<string> { "Eighth note", "Quarter note", "Whole note", "Sixteenth note" }, CorrectAnswer = "Sixteenth note" },
            };
        }

        private void OnStartQuizClicked(object sender, EventArgs e)
        {
            if (!int.TryParse(QuestionCountEntry.Text, out int questionCount) || questionCount <= 0 || questionCount > 26)
            {
                DisplayAlert("Error", "Enter a number bwtween 1 ~ 26", "OK");
                return;
            }

            var random = new Random();
            selectedQuestions = allQuestions.OrderBy(x => random.Next()).Take(questionCount).ToList();

            // ??????????
            foreach (var question in selectedQuestions)
            {
                // ??????
                question.Options = question.Options.OrderBy(x => random.Next()).ToList();

                // ?? CorrectAnswer ???????????
                question.CorrectAnswer = question.Options.First(o => o == question.CorrectAnswer);
            }

            // ????????????
            firstAnswerStatus = selectedQuestions.Select(q => false).ToList(); // ???????
            finalAnswerStatus = selectedQuestions.Select(q => false).ToList(); // ??????

            currentQuestionIndex = 0;
            correctCount = 0;
            responseTimes.Clear();
            totalElapsedTime = 0; // Reset total time when starting new quiz

            LoadCurrentQuestion();

            QuestionLabel.IsVisible = true;
            Option1Button.IsVisible = true;
            Option2Button.IsVisible = true;
            Option3Button.IsVisible = true;
            Option4Button.IsVisible = true;
            TimerLabel.IsVisible = true;
            StartQuizButton.IsVisible = false;
            QuestionCountEntry.IsVisible = false;
            AskingNumberOfQuestion.IsVisible = false;
            backToLastPage.IsVisible = false;
        }

        private async void BackClicked(object sender, EventArgs e)
        {

            // ?????????
            await Navigation.PushAsync(new MauiApp2.Practice());
        }
        private void LoadCurrentQuestion()
        {
            if (currentQuestionIndex < selectedQuestions.Count)
            {
                var question = selectedQuestions[currentQuestionIndex];
                QuestionLabel.Text = question.Text;

                var options = question.Options;
                Option1Button.Text = options[0];
                Option2Button.Text = options[1];
                Option3Button.Text = options[2];
                Option4Button.Text = options[3];

                stopwatch.Restart();
                UpdateTimer();
            }
            else
            {
                FinishQuiz();
            }
        }

        private async void UpdateTimer()
        {
            while (stopwatch.IsRunning)
            {
                await Task.Delay(100);
                TimerLabel.Text = $"Time Elapsed: {stopwatch.Elapsed.TotalSeconds:F2}s";
            }
        }

        private void OnOptionClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            stopwatch.Stop();
            responseTimes.Add(stopwatch.Elapsed.TotalSeconds);

            totalElapsedTime += stopwatch.Elapsed.TotalSeconds; // ???????

            var selectedAnswer = button.Text;
            var correctAnswer = selectedQuestions[currentQuestionIndex].CorrectAnswer;

            // ??????????
            if (!firstAnswerStatus[currentQuestionIndex]) // ????????????
            {
                firstAnswerStatus[currentQuestionIndex] = selectedAnswer == correctAnswer; // ?????????
            }

            // ?????????
            if (selectedAnswer == correctAnswer)
            {
                correctCount++;
                finalAnswerStatus[currentQuestionIndex] = true; // ????
                DisplayAlert("Correct!", "You answered correctly.", "OK");
            }
            else
            {
                finalAnswerStatus[currentQuestionIndex] = false; // ????
                DisplayAlert("Wrong!", $"The correct answer is {correctAnswer}.", "OK");
            }

            // ??????????
            currentQuestionIndex++;
            LoadCurrentQuestion();
        }

        private void FinishQuiz()
        {
            var averageTime = responseTimes.Average();
            var accuracy = (double)firstAnswerStatus.Count(status => status) / selectedQuestions.Count * 100; // ???????????????

            DisplayAlert(
                "Quiz Finished!",
                $"Average Time: {averageTime:F2}s\nTotal Time: {totalElapsedTime:F2}s\nAccuracy: {accuracy:F2}%",
                "OK"
            );

            QuestionLabel.IsVisible = false;
            Option1Button.IsVisible = false;
            Option2Button.IsVisible = false;
            Option3Button.IsVisible = false;
            Option4Button.IsVisible = false;
            TimerLabel.IsVisible = false;
            StartQuizButton.IsVisible = true;
            QuestionCountEntry.IsVisible = true;
            AskingNumberOfQuestion.IsVisible = true;
            backToLastPage.IsVisible = true;
        }

        private void OnNextButtonClicked(object sender, EventArgs e)
        {
            // Logic to load the next question when "Next Question" button is clicked
            currentQuestionIndex++;
            LoadCurrentQuestion();
        }
    }

    public class Question
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }
        
    }
}










