using LinguisticsEngine.Model;
using System.Text.Json;
using Microsoft.Office.Interop.Word;



Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();


FeedbackGenerator fb = new FeedbackGenerator();

if (args.Length == 0)
{
    Console.WriteLine("Please Provide Atleast 1 Name");
    Environment.Exit(-1);
}

Output output = new Output();

foreach (var name in args)
{
    User user = new User();
    if (name.Contains("/"))
    {
        user.Pronoun = name.Split('/').Last();
    }

    var nameSplit = name.Split(' ');
    user.FirstName = nameSplit[0];
    user.LastName = nameSplit[1];

    UserFeedback userFeedback = new UserFeedback
    {
        User = user
    };

    for (int i = 0; i < 10; i++)
    {
        // Add positive feedback
        userFeedback.Feedbacks.Add(new Feedback
        {
            Positive = true,
            FeedbackString = fb.GeneratePositiveFeedback(user)
        });
    }
    output.UserFeedbacks.Add(userFeedback);
}

Console.WriteLine(JsonSerializer.Serialize(output));



public class FeedbackGenerator
{
    public List<string> Verbs;
    public List<string> PositiveAdjectives;
    public FeedbackGenerator()
    {
        try
        {
            Verbs = new(File.ReadAllLines("verbs.txt"));
            PositiveAdjectives = new(File.ReadAllLines("positiveAdjective.txt"));

        }
        catch (Exception ex)
        {

        }
    }

    public string GeneratePositiveFeedback(User user)
    {
        Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
        string feedback = user.FirstName;

        Random rng = new Random();

        while (true)
        {
            string verb = Verbs[rng.Next(Verbs.Count - 1)];
            string adjective = PositiveAdjectives[rng.Next(PositiveAdjectives.Count - 1)];

            feedback = $"{user.FirstName} {verb} {adjective}";

            Console.WriteLine(feedback);

            if (word.CheckGrammar(feedback))
            {
                break;
            }
        }


        return feedback;
    }
}