using System.ComponentModel.DataAnnotations;

namespace ThreeSixtyPlusAI.Models
{

	public class Question
	{

		public Guid Id { get; set; }
		public Guid QuestionCategoryId { get; set; }
		public QuestionCategory QuestionCategory { get; set; } = null!;
		[Required]
		public string QuestionText { get; set; } = null!;

	}

}