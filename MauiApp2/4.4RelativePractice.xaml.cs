using Microsoft.Maui.Controls;
using System.Diagnostics;
using System.Linq;

namespace MauiApp2
{
    public partial class RelativePractice : ContentPage
    {
        private List<QuestionC> allQuestions;
        private List<QuestionC> selectedQuestions;
        private List<bool> firstAnswerStatus; 
        private List<bool> finalAnswerStatus; 
        private int currentQuestionIndex = 0;
        private int correctCount = 0;
        private Stopwatch stopwatch;
        private List<double> responseTimes;
        private double totalElapsedTime = 0; 

        public RelativePractice()
        {
            InitializeComponent();
            InitializeQuestions();
            stopwatch = new Stopwatch();
            responseTimes = new List<double>();
        }

        private void InitializeQuestions()
        {
            allQuestions = new List<QuestionC>
            {
                new QuestionC { Text = "What is the relative minor of C Major?", Options = new List<string> { "A minor", "D minor", "G minor", "F minor" }, CorrectAnswer = "A minor" },
                new QuestionC { Text = "What is the relative minor of G Major?", Options = new List<string> { "E minor", "A minor", "D minor", "B minor" }, CorrectAnswer = "E minor" },
                new QuestionC { Text = "What is the relative minor of D Major?", Options = new List<string> { "B minor", "G minor", "E minor", "C♯ minor" }, CorrectAnswer = "B minor" },
                new QuestionC { Text = "What is the relative minor of A Major?", Options = new List<string> { "F♯ minor", "D minor", "E minor", "C minor" }, CorrectAnswer = "F♯ minor" },
                new QuestionC { Text = "What is the relative minor of E Major?", Options = new List<string> { "C♯ minor", "G♯ minor", "A minor", "B minor" }, CorrectAnswer = "C♯ minor" },
                new QuestionC { Text = "What is the relative minor of B Major?", Options = new List<string> { "G♯ minor", "C♯ minor", "F♯ minor", "D♯ minor" }, CorrectAnswer = "G♯ minor" },
                new QuestionC { Text = "What is the relative minor of F Major?", Options = new List<string> { "D minor", "E minor", "A minor", "G minor" }, CorrectAnswer = "D minor" },
                new QuestionC { Text = "What is the relative minor of B♭ Major?", Options = new List<string> { "G minor", "F minor", "C minor", "D minor" }, CorrectAnswer = "G minor" },
                new QuestionC { Text = "What is the relative minor of E♭ Major?", Options = new List<string> { "C minor", "B minor", "G minor", "F minor" }, CorrectAnswer = "C minor" },
                new QuestionC { Text = "What is the relative minor of A♭ Major?", Options = new List<string> { "F minor", "D minor", "C♯ minor", "E minor" }, CorrectAnswer = "F minor" },
                new QuestionC { Text = "What is the relative minor of D♭ Major?", Options = new List<string> { "B♭ minor", "F♯ minor", "A minor", "E minor" }, CorrectAnswer = "B♭ minor" },
                new QuestionC { Text = "What is the relative major of A minor?", Options = new List<string> { "C Major", "G Major", "F Major", "D Major" }, CorrectAnswer = "C Major" },
                new QuestionC { Text = "What is the relative major of E minor?", Options = new List<string> { "G Major", "C Major", "A Major", "D Major" }, CorrectAnswer = "G Major" },
                new QuestionC { Text = "What is the relative major of B minor?", Options = new List<string> { "D Major", "E Major", "B♭ Major", "A Major" }, CorrectAnswer = "D Major" },
                new QuestionC { Text = "What is the relative major of F♯ minor?", Options = new List<string> { "A Major", "D Major", "E♭ Major", "G Major" }, CorrectAnswer = "A Major" },
                new QuestionC { Text = "What is the relative major of C♯ minor?", Options = new List<string> { "E Major", "B Major", "A Major", "C Major" }, CorrectAnswer = "E Major" },
                new QuestionC { Text = "What is the relative major of G♯ minor?", Options = new List<string> { "B Major", "E Major", "A♭ Major", "D♭ Major" }, CorrectAnswer = "B Major" },
                new QuestionC { Text = "What is the relative major of D minor?", Options = new List<string> { "F Major", "B♭ Major", "C Major", "E♭ Major" }, CorrectAnswer = "F Major" },
                new QuestionC { Text = "What is the relative major of G minor?", Options = new List<string> { "B♭ Major", "E♭ Major", "A Major", "F Major" }, CorrectAnswer = "B♭ Major" },
                new QuestionC { Text = "What is the relative major of C minor?", Options = new List<string> { "E♭ Major", "G Major", "C♯ Major", "B♭ Major" }, CorrectAnswer = "E♭ Major" },
                new QuestionC { Text = "What is the relative major of F minor?", Options = new List<string> { "A♭ Major", "C Major", "D♭ Major", "F♯ Major" }, CorrectAnswer = "A♭ Major" },
                new QuestionC { Text = "What is the relative major of B♭ minor?", Options = new List<string> { "D♭ Major", "B Major", "E♭ Major", "A♭ Major" }, CorrectAnswer = "D♭ Major" },

            };
        }



        private void OnStartQuizClicked(object sender, EventArgs e)
        {
            if (!int.TryParse(QuestionCountEntryC.Text, out int questionCount) || questionCount <= 0 || questionCount > 26)
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

            QuestionLabelC.IsVisible = true;
            Option1ButtonC.IsVisible = true;
            Option2ButtonC.IsVisible = true;
            Option3ButtonC.IsVisible = true;
            Option4ButtonC.IsVisible = true;
            TimerLabelC.IsVisible = true;
            StartQuizButtonC.IsVisible = false;
            QuestionCountEntryC.IsVisible = false;
            AskingNumberOfQuestionC.IsVisible = false;
            backToLastPage.IsVisible = false;
        }


        private void LoadCurrentQuestion()
        {
            if (currentQuestionIndex < selectedQuestions.Count)
            {
                var question = selectedQuestions[currentQuestionIndex];
                QuestionLabelC.Text = question.Text;

                var options = question.Options;
                Option1ButtonC.Text = options[0];
                Option2ButtonC.Text = options[1];
                Option3ButtonC.Text = options[2];
                Option4ButtonC.Text = options[3];

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
                TimerLabelC.Text = $"Time Elapsed: {stopwatch.Elapsed.TotalSeconds:F2}s";
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

            QuestionLabelC.IsVisible = false;
            Option1ButtonC.IsVisible = false;
            Option2ButtonC.IsVisible = false;
            Option3ButtonC.IsVisible = false;
            Option4ButtonC.IsVisible = false;
            TimerLabelC.IsVisible = false;
            StartQuizButtonC.IsVisible = true;
            QuestionCountEntryC.IsVisible = true;
            AskingNumberOfQuestionC.IsVisible = true;
            backToLastPage.IsVisible = true;
        }

        private void OnNextButtonClicked(object sender, EventArgs e)
        {
            // Logic to load the next question when "Next Question" button is clicked
            currentQuestionIndex++;
            LoadCurrentQuestion();
        }
    }

    public class QuestionC
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
