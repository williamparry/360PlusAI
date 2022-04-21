using System.ComponentModel.DataAnnotations;

namespace ThreeSixtyPlusAI.Models
{

	public class QuestionCategory
	{

		public Guid Id { get; set; }

		[Required]
		public string QuestionCategoryTitle { get; set; } = null!;

		[Required]
		public string QuestionCategoryIcon { get; set; } = null!;

	}

}
