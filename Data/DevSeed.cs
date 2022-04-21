using Newtonsoft.Json;
using ThreeSixtyPlusAI.Models;

namespace ThreeSixtyPlusAI.Data
{
	public static class DevSeed
	{

		public static async Task Initialize(IServiceProvider services)
		{

			var SeedQuestionsPath = Path.Combine("SeedData", "SeedQuestions.json");
			if (File.Exists(SeedQuestionsPath))
			{
				string readText = await File.ReadAllTextAsync(SeedQuestionsPath);
				var seedData = JsonConvert.DeserializeObject<SeedQuestions>(readText) ?? throw new Exception("No Seed Data");

				var context = services.GetRequiredService<ThreeSixtyPlusAIContext>();

				await context.Database.EnsureDeletedAsync();
				await context.Database.EnsureCreatedAsync();

				// 360 Review Questions

				List<ThreeSixtyReviewQuestion> ThreeSixtyReviewQuestions = new() {
					new ThreeSixtyReviewQuestion {
						Id = Guid.NewGuid(),
						QuestionId = seedData.Questions[0].Id,
					},
					new ThreeSixtyReviewQuestion {
						Id = Guid.NewGuid(),
						QuestionId = seedData.Questions[1].Id
					}
				};

				// Add Reviewers

				List<ThreeSixtyReviewer> ThreeSixtyReviewers = new();

				for (var i = 0; i < 10; i++)
				{
					ThreeSixtyReviewers.Add(new ThreeSixtyReviewer
					{
						AccessCode = Utils.GenerateAccessCodeText()
					});
				}

				ThreeSixtyReviewers[0].ThreeSixtyReviewAnswers = new List<ThreeSixtyReviewAnswer> {
					new ThreeSixtyReviewAnswer {
						ThreeSixtyReviewQuestionId = ThreeSixtyReviewQuestions[0].Id,
						AnswerText = "Pretty good"
					}
				};

				ThreeSixtyReviewers[0].HasFinished = true;

				// Create ThreeSixtyReview

				ThreeSixtyReview ThreeSixtyReview1 = new()
				{
					Title = "My 360 review",
					CreatedDate = DateTime.UtcNow,
					AccessCode = "E",
					ThreeSixtyReviewers = ThreeSixtyReviewers,
					ThreeSixtyReviewQuestions = ThreeSixtyReviewQuestions
				};

				context.ThreeSixtyReviews.Add(ThreeSixtyReview1);

				context.SaveChanges();

			}

		}
	}
}