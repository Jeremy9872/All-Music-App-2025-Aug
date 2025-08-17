using System.Diagnostics;

namespace MauiApp2
{
    public partial class RhythmPractice : ContentPage
    {
        private List<QuestionA> allQuestions;
    private List<QuestionA> selectedQuestions;
    private List<bool> firstAnswerStatus; // ????????
    private List<bool> finalAnswerStatus; // ????????
    private int currentQuestionIndex = 0;
    private int correctCount = 0;
    private Stopwatch stopwatch;
    private List<double> responseTimes;
    private double totalElapsedTime = 0; // ?????

    public RhythmPractice()
    {
        InitializeComponent();
        InitializeQuestions();
        stopwatch = new Stopwatch();
        responseTimes = new List<double>();
    }

    private void InitializeQuestions()
    {
        allQuestions = new List<QuestionA>
            {
                 new QuestionA { Text = "What does the top number '4' in a 4/4 time signature represent?", Options = new List<string> { "Four half notes in a measure", "Four beats per measure", "Quarter note gets four beats", "Each measure is four seconds long" }, CorrectAnswer = "Four beats per measure" },
                 new QuestionA { Text = "What does the bottom number '4' in a 4/4 time signature mean?", Options = new List<string> { "Each beat is a half note", "Each beat is a quarter note", "Each beat is one second", "Each measure has four notes" }, CorrectAnswer = "Each beat is a quarter note" },
                 new QuestionA { Text = "Which of the following is also called 'common time'?", Options = new List<string> { "3/4", "6/8", "4/4", "2/2" }, CorrectAnswer = "4/4" },
                 new QuestionA { Text = "Which time signature has three beats per measure?", Options = new List<string> { "4/4", "3/4", "6/8", "2/2" }, CorrectAnswer = "3/4" },
                 new QuestionA { Text = "In 6/8 time, what kind of note gets one beat?", Options = new List<string> { "Eighth note", "Quarter note", "Half note", "Whole note" }, CorrectAnswer = "Eighth note" },
                 new QuestionA { Text = "What is the main difference between 6/8 and 3/4 time signatures?", Options = new List<string> { "6/8 has six beats, 3/4 has three", "3/4 has eighth notes, 6/8 does not", "6/8 feels like two groups of three, while 3/4 feels like three groups of one", "They are identical in rhythm" }, CorrectAnswer = "6/8 feels like two groups of three, while 3/4 feels like three groups of one" },
                 new QuestionA { Text = "Which of the following time signatures is a compound meter?", Options = new List<string> { "2/4", "4/4", "3/4", "6/8" }, CorrectAnswer = "6/8" },
                 new QuestionA { Text = "What is the beat grouping in 9/8 time?", Options = new List<string> { "Three groups of three eighth notes", "Three groups of two eighth notes", "Nine quarter notes", "Nine sixteenth notes" }, CorrectAnswer = "Three groups of three eighth notes" },
                 new QuestionA { Text = "In 12/8 time, how many eighth notes are in a measure?", Options = new List<string> { "8", "6", "12", "4" }, CorrectAnswer = "12" },
                 new QuestionA { Text = "Which time signature has two beats per measure and each beat is a half note?", Options = new List<string> { "2/4", "4/4", "2/2", "6/8" }, CorrectAnswer = "2/2" },
                 new QuestionA { Text = "Which symbol can be used instead of the 4/4 time signature?", Options = new List<string> { "𝄞", "𝄐", "𝄴", "C" }, CorrectAnswer = "C" },
                 new QuestionA { Text = "What does a time signature of 5/4 indicate?", Options = new List<string> { "5 beats per measure, quarter note gets the beat", "5 quarter notes per second", "5 measures per line", "5 notes in one beat" }, CorrectAnswer = "5 beats per measure, quarter note gets the beat" },
                 new QuestionA { Text = "Which of the following is considered an irregular or asymmetric time signature?", Options = new List<string> { "4/4", "3/4", "6/8", "7/8" }, CorrectAnswer = "7/8" },
                 new QuestionA { Text = "Which time signature is most commonly used in waltz music?", Options = new List<string> { "2/4", "3/4", "4/4", "6/8" }, CorrectAnswer = "3/4" },
                 new QuestionA { Text = "What does it mean if a piece is in 2/2 time?", Options = new List<string> { "There are 2 beats per measure and the half note gets one beat", "Two half notes per second", "Two quarter notes per measure", "Each measure has two notes" }, CorrectAnswer = "There are 2 beats per measure and the half note gets one beat" },
                 new QuestionA { Text = "How many beats are there in a measure of 7/8 time?", Options = new List<string> { "7", "8", "4", "3" }, CorrectAnswer = "7" },
                 new QuestionA { Text = "What type of meter is 4/4 time considered?", Options = new List<string> { "Simple meter", "Compound meter", "Asymmetric meter", "Irregular meter" }, CorrectAnswer = "Simple meter" },
                 new QuestionA { Text = "Which time signature is often associated with marches?", Options = new List<string> { "3/4", "2/4", "6/8", "5/4" }, CorrectAnswer = "2/4" },
                 new QuestionA { Text = "In simple meters, what does the bottom number in the time signature indicate?", Options = new List<string> { "Number of beats per measure", "Which note value represents one beat", "Tempo of the piece", "Number of measures" }, CorrectAnswer = "Which note value represents one beat" },
                 new QuestionA { Text = "Which time signature would most likely have a strong downbeat followed by two weaker beats?", Options = new List<string> { "3/4", "4/4", "6/8", "5/4" }, CorrectAnswer = "3/4" },
                 new QuestionA { Text = "How many beats are in a measure of 12/8 time?", Options = new List<string> { "12", "4", "3", "6" }, CorrectAnswer = "4" },
                 new QuestionA { Text = "In 12/8 time, how are the beats grouped?", Options = new List<string> { "Four groups of three eighth notes", "Three groups of four eighth notes", "Six groups of two eighth notes", "Twelve single beats" }, CorrectAnswer = "Four groups of three eighth notes" },
                 new QuestionA { Text = "Which time signature is also called cut time?", Options = new List<string> { "2/2", "4/4", "6/8", "3/4" }, CorrectAnswer = "2/2" },
                 new QuestionA { Text = "What does the time signature 3/8 indicate?", Options = new List<string> { "Three beats per measure, eighth note gets the beat", "Three measures per beat", "Eight beats per measure, quarter note gets the beat", "Three quarter notes per measure" }, CorrectAnswer = "Three beats per measure, eighth note gets the beat" },
                 new QuestionA { Text = "Which time signature is less common and often used for experimental music?", Options = new List<string> { "5/4", "4/4", "3/4", "6/8" }, CorrectAnswer = "5/4" },
                 new QuestionA { Text = "In compound meter, how many subdivisions are there per beat?", Options = new List<string> { "Two", "Three", "Four", "Six" }, CorrectAnswer = "Three" },
                 new QuestionA { Text = "What kind of note typically receives the beat in a 2/8 time signature?", Options = new List<string> { "Quarter note", "Eighth note", "Half note", "Sixteenth note" }, CorrectAnswer = "Eighth note" },
                 new QuestionA { Text = "Which time signature is used to indicate five beats per measure?", Options = new List<string> { "5/4", "4/4", "3/4", "6/8" }, CorrectAnswer = "5/4" },
                 new QuestionA { Text = "In a 9/8 time signature, how many beats are there per measure?", Options = new List<string> { "Three", "Nine", "Six", "Four" }, CorrectAnswer = "Three" },
                 new QuestionA { Text = "What does a time signature with a 'C' symbol represent?", Options = new List<string> { "Common time (4/4)", "Cut time (2/2)", "Compound time", "Complex time" }, CorrectAnswer = "Common time (4/4)" },
                 new QuestionA { Text = "Which time signature indicates two beats per measure, each a quarter note?", Options = new List<string> { "2/4", "3/4", "4/4", "2/2" }, CorrectAnswer = "2/4" },
             };
    }



    private void OnStartQuizClicked(object sender, EventArgs e)
    {
        if (!int.TryParse(QuestionCountEntryA.Text, out int questionCount) || questionCount <= 0 || questionCount > 26)
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

        QuestionLabelA.IsVisible = true;
        Option1ButtonA.IsVisible = true;
        Option2ButtonA.IsVisible = true;
        Option3ButtonA.IsVisible = true;
        Option4ButtonA.IsVisible = true;
        TimerLabelA.IsVisible = true;
        StartQuizButtonA.IsVisible = false;
        QuestionCountEntryA.IsVisible = false;
        AskingNumberOfQuestionA.IsVisible = false;
        backToLastPage.IsVisible = false;
    }


    private void LoadCurrentQuestion()
    {
        if (currentQuestionIndex < selectedQuestions.Count)
        {
            var question = selectedQuestions[currentQuestionIndex];
            QuestionLabelA.Text = question.Text;

            var options = question.Options;
            Option1ButtonA.Text = options[0];
            Option2ButtonA.Text = options[1];
            Option3ButtonA.Text = options[2];
            Option4ButtonA.Text = options[3];

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
            TimerLabelA.Text = $"Time Elapsed: {stopwatch.Elapsed.TotalSeconds:F2}s";
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

        QuestionLabelA.IsVisible = false;
        Option1ButtonA.IsVisible = false;
        Option2ButtonA.IsVisible = false;
        Option3ButtonA.IsVisible = false;
        Option4ButtonA.IsVisible = false;
        TimerLabelA.IsVisible = false;
        StartQuizButtonA.IsVisible = true;
        QuestionCountEntryA.IsVisible = true;
        AskingNumberOfQuestionA.IsVisible = true;
        backToLastPage.IsVisible = true;
    }

    private void OnNextButtonClicked(object sender, EventArgs e)
    {
        // Logic to load the next question when "Next Question" button is clicked
        currentQuestionIndex++;
        LoadCurrentQuestion();
    }



    public class QuestionA
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }
    }
    }
}


