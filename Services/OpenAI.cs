using ThreeSixtyPlusAI.Data;
using ThreeSixtyPlusAI.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace ThreeSixtyPlusAI.Services
{

    public interface IOpenAI
    {
        Task<string> GenerateSummary(Guid ThreeSixtyReviewId);
        Task<string> GeneratePayload(Guid ThreeSixtyReviewId);
    }

    #pragma warning disable IDE1006
    public class OpenAIResponseChoice
    {
        public string text { get; set; } = null!;
        public int index { get; set; }
        public string? logprobs { get; set; }
        public string finish_reason { get; set; } = null!;
    }

    public class OpenAIRequest
    {
        public string prompt { get; set; } = null!;
        public decimal temperature { get; set; }
        public int max_tokens { get; set; }
    }

    public class OpenAIResponse
    {
        public string id { get; set; } = null!;
        [JsonProperty("object")]
        public string response_object { get; set; } = null!;
        public float created { get; set; }
        public string model { get; set; } = null!;
        public List<OpenAIResponseChoice> choices = new();
    }

    #pragma warning restore IDE1006

    public class OpenAI : IOpenAI
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ThreeSixtyPlusAIContext _context;
        private readonly string _openAIAPIKey;

        public OpenAI(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            ThreeSixtyPlusAIContext context)
        {
            _httpClientFactory = httpClientFactory;
            _openAIAPIKey = configuration["OpenAIAPIKey"];
            _context = context;

        }

        public class QuestionForAnswer
        {
            public string QuestionText { get; set; } = null!;
            public Guid ThreeSixtyReviewQuestionId { get; set; }
            public Guid QuestionId { get; set; }
        }

        public async Task<string> GeneratePayload(Guid ThreeSixtyReviewId) {
            var ThreeSixtyReview = await _context.ThreeSixtyReviews
                .Include(x => x.ThreeSixtyReviewers)
                .ThenInclude(x => x.ThreeSixtyReviewAnswers)
                .Include(x => x.ThreeSixtyReviewQuestions)
                .AsSplitQuery()
                .Where(x => x.Id == ThreeSixtyReviewId)
                .FirstOrDefaultAsync() ?? throw new Exception();

            var QuestionsAsked = ThreeSixtyReview.ThreeSixtyReviewQuestions
                .Join(_context.Questions, x => x.QuestionId, y => y.Id, (threeSixtyReviewQuestion, question) => new QuestionForAnswer
                {
                    QuestionId = question.Id,
                    QuestionText = question.QuestionText,
                    ThreeSixtyReviewQuestionId = threeSixtyReviewQuestion.Id
                })
                .ToList();

            var AllAnswers = new List<ThreeSixtyReviewAnswer>();

            ThreeSixtyReview.ThreeSixtyReviewers
                    .Where(reviewer => reviewer.HasFinished)
                    .ToList().ForEach(x =>
                    {
                        AllAnswers.AddRange(x.ThreeSixtyReviewAnswers);
                    });

            // Convert to JSON payload and send

            string payload = "This is a 360 degree review of me:\n";

            QuestionsAsked.ForEach(question =>
            {

                payload += "Question:" + question.QuestionText;
                payload += "\nAnswers:";
                // TODO: This could be better

                // Get the answers from each reviewer

                var answersForQuestion = AllAnswers.Where(x => x.ThreeSixtyReviewQuestionId == question.ThreeSixtyReviewQuestionId).ToList();

                if (answersForQuestion.Count > 0)
                {
                    answersForQuestion.ForEach(answer =>
                        {
                            payload += "\n-" + answer.AnswerText + (!answer.AnswerText.EndsWith(".") ? "." : "");
                        });
                }
                else
                {
                    payload += "(No answers).";
                }

                payload += "\n";

            });

            payload += "Tell me helpful feedback based on this review in the form of advice:";

            return payload;
        }

        public async Task<string> GenerateSummary(Guid ThreeSixtyReviewId)
        {

            string payload = await GeneratePayload(ThreeSixtyReviewId);

            var httpClient = _httpClientFactory.CreateClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _openAIAPIKey);

            var body = JsonConvert.SerializeObject(new OpenAIRequest
            {
                prompt = payload,
                temperature = 0.7M,
                max_tokens = 1536
            });
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            var httpResponseMessage = await httpClient.PostAsync("https://api.openai.com/v1/engines/text-davinci-002/completions", content);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string json = await httpResponseMessage.Content.ReadAsStringAsync();
            
                var response = JsonConvert.DeserializeObject<OpenAIResponse>(json);

                if(response is not null) {
                    return response.choices[0].text;
                }
                
            }

            throw new Exception();

        }
    }

}
