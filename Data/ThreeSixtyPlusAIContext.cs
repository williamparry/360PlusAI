using Microsoft.EntityFrameworkCore;
using ThreeSixtyPlusAI.Models;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ThreeSixtyPlusAI.Data
{
	public class ThreeSixtyPlusAIContext : DbContext, IDataProtectionKeyContext
	{

		public DbSet<DataProtectionKey> DataProtectionKeys { get; set; } = null!;

		public DbSet<QuestionCategory> QuestionCategories { get; set; } = null!;

		public DbSet<Question> Questions { get; set; } = null!;

		public DbSet<ThreeSixtyReview> ThreeSixtyReviews { get; set; } = null!;

		public ThreeSixtyPlusAIContext(DbContextOptions<ThreeSixtyPlusAIContext> options)
		   : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("main");

			var SeedQuestionsPath = Path.Combine("SeedData", "SeedQuestions.json");

			if (File.Exists(SeedQuestionsPath))
			{

				// TODO: Tried to use System.Text.Json, but Newtownsoft just does it easily
				string seedDataJSON = File.ReadAllText(SeedQuestionsPath);
				var seedData = JsonConvert.DeserializeObject<SeedQuestions>(seedDataJSON);

				if(seedData is not null) {
					
					modelBuilder.Entity<QuestionCategory>().HasData(
						seedData.QuestionCategories
					);

					modelBuilder.Entity<Question>().HasData(
						seedData.Questions
					);

				}

			}

			base.OnModelCreating(modelBuilder);
		}
	}
}