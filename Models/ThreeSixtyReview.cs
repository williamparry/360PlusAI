using System.ComponentModel.DataAnnotations;

namespace ThreeSixtyPlusAI.Models
{

    public class ThreeSixtyReview
    {

        public Guid Id { get; set; }

        [Required]
        public string AccessCode { get; set; } = null!;

        [Required]
        public string Title { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public List<ThreeSixtyReviewQuestion> ThreeSixtyReviewQuestions { get; set; } = new List<ThreeSixtyReviewQuestion>();

        public List<ThreeSixtyReviewer> ThreeSixtyReviewers { get; set; } = new List<ThreeSixtyReviewer>();
    }

    public class ThreeSixtyReviewQuestion
    {

        public Guid Id { get; set; }

        public Guid QuestionId { get; set; }

        public Guid ThreeSixtyReviewId { get; set; }
        
        public ThreeSixtyReview ThreeSixtyReview { get; set; } = null!;

    }

    public class ThreeSixtyReviewer
    {
        public Guid Id { get; set; }

        public Guid ThreeSixtyReviewId { get; set; }
        public ThreeSixtyReview ThreeSixtyReview { get; set; } = null!;

        [Required]
        public string AccessCode { get; set; } = null!;

        // Could use logic for this, but bool is fine - it's a one-time thing.
        public bool HasFinished { get; set; }

        public List<ThreeSixtyReviewAnswer> ThreeSixtyReviewAnswers { get; set; } = new List<ThreeSixtyReviewAnswer>();

    }

    public class ThreeSixtyReviewAnswer
    {

        public Guid Id { get; set; }

        public Guid ThreeSixtyReviewerId { get; set; }
        public ThreeSixtyReviewer ThreeSixtyReviewer { get; set; } = null!;

        public Guid ThreeSixtyReviewQuestionId { get; set; }

        [Required]
        public string AnswerText { get; set; } = null!;

    }

}