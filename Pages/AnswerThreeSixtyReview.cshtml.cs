using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThreeSixtyPlusAI.Data;
using System.ComponentModel.DataAnnotations;
using ThreeSixtyPlusAI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ThreeSixtyPlusAI.Pages;

public class AnswerThreeSixtyReview : PageModel
{
	private readonly ThreeSixtyPlusAIContext _context;

	[BindProperty(SupportsGet = true)]
	public string AccessCode { get; set; } = null!;

	public string Title { get; set; } = null!;

	public class QuestionAnswerModel
	{

		[Key]
		[BindProperty]
		[Required]
		public Guid ThreeSixtyReviewQuestionId { get; set; }

		public string QuestionText { get; set; } = null!;

		[BindProperty]
		[Required]
		public string Answer { get; set; } = null!;

	}

	[BindProperty]
	public List<QuestionAnswerModel> QuestionAnswerModels { get; set; } = new List<QuestionAnswerModel>();

	public AnswerThreeSixtyReview(ThreeSixtyPlusAIContext context)
	{
		_context = context;
	}

	public async Task OnGetAsync()
	{

		var ThreeSixtyReview = await _context.ThreeSixtyReviews
			.Include(x => x.ThreeSixtyReviewers)
			.Include(x => x.ThreeSixtyReviewQuestions)
			.AsSplitQuery()
			.Where(x => x.ThreeSixtyReviewers.Any(y => y.AccessCode == AccessCode))
			.FirstOrDefaultAsync() ?? throw new Exception();

		QuestionAnswerModels = ThreeSixtyReview.ThreeSixtyReviewQuestions.Join(_context.Questions, x => x.QuestionId, y => y.Id, (selectedQuestion, question) => new QuestionAnswerModel
		{
			ThreeSixtyReviewQuestionId = selectedQuestion.Id,
			QuestionText = question.QuestionText
		}).ToList();

		Title = ThreeSixtyReview.Title;

	}

	public async Task<IActionResult> OnPostAsync()
	{


		if (!ModelState.IsValid)
		{
			return Page();
		}

		var ThreeSixtyReview = await _context.ThreeSixtyReviews
			.Include(x => x.ThreeSixtyReviewers)
			.Where(x => x.ThreeSixtyReviewers.Any(y => y.AccessCode == AccessCode))
			.FirstOrDefaultAsync() ?? throw new Exception();

		var ThreeSixtyReviewer = ThreeSixtyReview.ThreeSixtyReviewers
			.Where(x => x.AccessCode == AccessCode)
			.FirstOrDefault() ?? throw new Exception();

		// Ignore subsequent
		if (!ThreeSixtyReviewer.HasFinished)
		{
			QuestionAnswerModels.ForEach(x =>
			{
				ThreeSixtyReviewer.ThreeSixtyReviewAnswers.Add(new ThreeSixtyReviewAnswer
				{
					ThreeSixtyReviewQuestionId = x.ThreeSixtyReviewQuestionId,
					AnswerText = x.Answer
				});
			});

			ThreeSixtyReviewer.HasFinished = true;

			await _context.SaveChangesAsync();
		}

		TempData["IndexMessage"] = "Thanks for completing the review.";
		return RedirectToPage("Index");

	}
}