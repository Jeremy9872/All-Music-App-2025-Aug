using Microsoft.Maui.Controls;
using System.Diagnostics;
using System.Linq;

namespace MauiApp2
{
    public partial class IntervalPractice : ContentPage
    {
        private List<QuestionB> allQuestions;
        private List<QuestionB> selectedQuestions;
        private List<bool> firstAnswerStatus; // ????????
        private List<bool> finalAnswerStatus; // ????????
        private int currentQuestionIndex = 0;
        private int correctCount = 0;
        private Stopwatch stopwatch;
        private List<double> responseTimes;
        private double totalElapsedTime = 0; // ?????

        public IntervalPractice()
        {
            InitializeComponent();
            InitializeQuestions();
            stopwatch = new Stopwatch();
            responseTimes = new List<double>();
        }

        private void InitializeQuestions()
        {
            allQuestions = new List<QuestionB>
            {
                 new QuestionB { Text = "What interval is from C to E?", Options = new List<string> { "Second", "Third", "Fourth", "Fifth" }, CorrectAnswer = "Third" },
                 new QuestionB { Text = "What interval is from D to G?", Options = new List<string> { "Fourth", "Fifth", "Sixth", "Third" }, CorrectAnswer = "Fourth" },
                 new QuestionB { Text = "What interval is from E to B?", Options = new List<string> { "Fifth", "Sixth", "Fourth", "Third" }, CorrectAnswer = "Fifth" },
                 new QuestionB { Text = "What interval is from F to A?", Options = new List<string> { "Second", "Third", "Fourth", "Sixth" }, CorrectAnswer = "Third" },
                 new QuestionB { Text = "What interval is from G to D?", Options = new List<string> { "Fourth", "Fifth", "Sixth", "Seventh" }, CorrectAnswer = "Fifth" },
                 new QuestionB { Text = "What interval is from A to E?", Options = new List<string> { "Third", "Fourth", "Fifth", "Sixth" }, CorrectAnswer = "Fifth" },
                 new QuestionB { Text = "What interval is from B to F#", Options = new List<string> { "Fifth", "Sixth", "Seventh", "Fourth" }, CorrectAnswer = "Fifth" },
                 new QuestionB { Text = "What interval is from C to G?", Options = new List<string> { "Fourth", "Fifth", "Sixth", "Seventh" }, CorrectAnswer = "Fifth" },
                 new QuestionB { Text = "What interval is from D to F#", Options = new List<string> { "Third", "Fourth", "Fifth", "Sixth" }, CorrectAnswer = "Third" },
                 new QuestionB { Text = "What interval is from E to G?", Options = new List<string> { "Second", "Third", "Fourth", "Fifth" }, CorrectAnswer = "Third" },
                 new QuestionB { Text = "What interval is from F to C?", Options = new List<string> { "Fifth", "Sixth", "Fourth", "Third" }, CorrectAnswer = "Fifth" },
                 new QuestionB { Text = "What interval is from G to B?", Options = new List<string> { "Second", "Third", "Fourth", "Fifth" }, CorrectAnswer = "Third" },
                 new QuestionB { Text = "What interval is from A to D?", Options = new List<string> { "Fourth", "Fifth", "Sixth", "Seventh" }, CorrectAnswer = "Fourth" },
                 new QuestionB { Text = "What interval is from B to E?", Options = new List<string> { "Fourth", "Fifth", "Sixth", "Seventh" }, CorrectAnswer = "Fourth" },
                 new QuestionB { Text = "What interval is from C to F?", Options = new List<string> { "Third", "Fourth", "Fifth", "Sixth" }, CorrectAnswer = "Fourth" },
                 new QuestionB { Text = "What interval is from D to A?", Options = new List<string> { "Fourth", "Fifth", "Sixth", "Seventh" }, CorrectAnswer = "Fifth" },
                 new QuestionB { Text = "What interval is from E to C?", Options = new List<string> { "Sixth", "Seventh", "Fifth", "Fourth" }, CorrectAnswer = "Sixth" },
                 new QuestionB { Text = "What interval is from F to D?", Options = new List<string> { "Sixth", "Seventh", "Fifth", "Fourth" }, CorrectAnswer = "Sixth" },
                 new QuestionB { Text = "What interval is from G to E?", Options = new List<string> { "Sixth", "Seventh", "Fifth", "Fourth" }, CorrectAnswer = "Sixth" },
                 new QuestionB { Text = "What interval is from A to F?", Options = new List<string> { "Sixth", "Seventh", "Fifth", "Fourth" }, CorrectAnswer = "Sixth" },
                 new QuestionB { Text = "What interval is from B to G?", Options = new List<string> { "Sixth", "Seventh", "Fifth", "Fourth" }, CorrectAnswer = "Sixth" },
                 new QuestionB { Text = "What interval is from C to A?", Options = new List<string> { "Sixth", "Seventh", "Fifth", "Fourth" }, CorrectAnswer = "Sixth" },
                 new QuestionB { Text = "What interval is from D to B?", Options = new List<string> { "Sixth", "Seventh", "Fifth", "Fourth" }, CorrectAnswer = "Sixth" },
                 new QuestionB { Text = "What interval is from E to C#", Options = new List<string> { "Sixth", "Seventh", "Fifth", "Fourth" }, CorrectAnswer = "Sixth" },
                 new QuestionB { Text = "What interval is from F to D#", Options = new List<string> { "Sixth", "Seventh", "Fifth", "Fourth" }, CorrectAnswer = "Sixth" },
                 new QuestionB { Text = "What interval is from G to E#", Options = new List<string> { "Sixth", "Seventh", "Fifth", "Fourth" }, CorrectAnswer = "Sixth" },
                 new QuestionB { Text = "What interval is from A to G#", Options = new List<string> { "Sixth", "Seventh", "Fifth", "Fourth" }, CorrectAnswer = "Sixth" },
                 new QuestionB { Text = "What interval is from B to A#", Options = new List<string> { "Sixth", "Seventh", "Fifth", "Fourth" }, CorrectAnswer = "Sixth" },
                 new QuestionB { Text = "What interval is from C to B?", Options = new List<string> { "Seventh", "Sixth", "Fifth", "Fourth" }, CorrectAnswer = "Seventh" },
                 new QuestionB { Text = "What interval is from D to C#", Options = new List<string> { "Seventh", "Sixth", "Fifth", "Fourth" }, CorrectAnswer = "Seventh" },
                 new QuestionB { Text = "What interval is from E to D#", Options = new List<string> { "Seventh", "Sixth", "Fifth", "Fourth" }, CorrectAnswer = "Seventh" },
            };
        }



        private void OnStartQuizClicked(object sender, EventArgs e)
        {
            if (!int.TryParse(QuestionCountEntryB.Text, out int questionCount) || questionCount <= 0 || questionCount > 26)
            {
                DisplayAlert("Error", "We only have 26 questions.", "OK");
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

            QuestionLabelB.IsVisible = true;
            Option1ButtonB.IsVisible = true;
            Option2ButtonB.IsVisible = true;
            Option3ButtonB.IsVisible = true;
            Option4ButtonB.IsVisible = true;
            TimerLabelB.IsVisible = true;
            StartQuizButtonB.IsVisible = false;
            QuestionCountEntryB.IsVisible = false;
            AskingNumberOfQuestionB.IsVisible = false;
            backToLastPage.IsVisible = false;
        }


        private void LoadCurrentQuestion()
        {
            if (currentQuestionIndex < selectedQuestions.Count)
            {
                var question = selectedQuestions[currentQuestionIndex];
                QuestionLabelB.Text = question.Text;

                var options = question.Options;
                Option1ButtonB.Text = options[0];
                Option2ButtonB.Text = options[1];
                Option3ButtonB.Text = options[2];
                Option4ButtonB.Text = options[3];

                stopwatch.Restart();
                UpdateTimer();
            }
            else
            {
                FinishQuiz();
            }
        }

        private async void BackClicked(object sender, EventArgs e)
        {

            // ?????????
            await Navigation.PushAsync(new MauiApp2.Practice());
        }
        private async void UpdateTimer()
        {
            while (stopwatch.IsRunning)
            {
                await Task.Delay(100);
                TimerLabelB.Text = $"Time Elapsed: {stopwatch.Elapsed.TotalSeconds:F2}s";
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

            // ????????
            currentQuestionIndex++;
            LoadCurrentQuestion();
        }

        private void FinishQuiz()
        {
            var averageTime = responseTimes.Average();
            var accuracy = (double)firstAnswerStatus.Count(status => status) / selectedQuestions.Count * 100; // ????????

            DisplayAlert(
                "Quiz Finished!",
                $"Average Time: {averageTime:F2}s\nTotal Time: {totalElapsedTime:F2}s\nAccuracy: {accuracy:F2}%",
                "OK"
            );

            QuestionLabelB.IsVisible = false;
            Option1ButtonB.IsVisible = false;
            Option2ButtonB.IsVisible = false;
            Option3ButtonB.IsVisible = false;
            Option4ButtonB.IsVisible = false;
            TimerLabelB.IsVisible = false;
            StartQuizButtonB.IsVisible = true;
            QuestionCountEntryB.IsVisible = true;
            AskingNumberOfQuestionB.IsVisible = true;
            backToLastPage.IsVisible = true;
        }

        private void OnNextButtonClicked(object sender, EventArgs e)
        {
            // Logic to load the next question when "Next Question" button is clicked
            currentQuestionIndex++;
            LoadCurrentQuestion();
        }
    }

    public class QuestionB
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
