using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThreeSixtyPlusAI.Data;
using System.ComponentModel.DataAnnotations;
using ThreeSixtyPlusAI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ThreeSixtyPlusAI.Pages;

public class NewThreeSixtyReview : PageModel
{

	private readonly ThreeSixtyPlusAIContext _context;

	public List<QuestionCategory> QuestionCategories = new();

	public List<Question> Questions = new();

	public class NewThreeSixtyReviewFormModel
	{

		[BindProperty]
		[Display(Name = "Title")]
		[Required]
		public string TitleInput { get; set; } = null!;

		public List<string> QuestionIds { get; set; } = new List<string>();

	}

	[BindProperty]
	public NewThreeSixtyReviewFormModel NewThreeSixtyReviewForm { get; set; } = new NewThreeSixtyReviewFormModel();

	public NewThreeSixtyReview(ThreeSixtyPlusAIContext context)
	{
		_context = context;
	}

	public async Task OnGetAsync()
	{
		
		if (Utils.GetHasThreeSixtyReview(HttpContext))
		{
			throw new Exception();
		}

		QuestionCategories = await _context.QuestionCategories.ToListAsync();

		Questions = await _context.Questions.ToListAsync();

	}

	public async Task<IActionResult> OnPostAsync()
	{

		if (Utils.GetHasThreeSixtyReview(HttpContext))
		{
			throw new Exception();
		}

		if (!ModelState.IsValid)
		{
			return Page();
		}

		List<ThreeSixtyReviewer> Reviewers = new();

		for (var i = 0; i < 10; i++)
		{
			Reviewers.Add(new ThreeSixtyReviewer
			{
				AccessCode = Utils.GenerateAccessCodeText()
			});
		}

		var AccessCode = Utils.GenerateAccessCodeText();		

		ThreeSixtyReview NewThreeSixtyReview = new()
		{
			CreatedDate = DateTime.UtcNow, // TODO: Offload to postgres
			Title = NewThreeSixtyReviewForm.TitleInput,
			ThreeSixtyReviewQuestions = NewThreeSixtyReviewForm.QuestionIds
				.Where(x => x is not null)
				.Select(x => new ThreeSixtyReviewQuestion
				{
					QuestionId = Guid.Parse(x)
				})
				.ToList(),
			AccessCode = AccessCode,
			ThreeSixtyReviewers = Reviewers

		};

		_context.ThreeSixtyReviews.Add(NewThreeSixtyReview);

		await _context.SaveChangesAsync();

		Utils.SetHasThreeSixtyReview(HttpContext, true);
		Utils.SetAccessCode(HttpContext, AccessCode);

		TempData["AccessCode"] = AccessCode;

		return RedirectToPage("ViewThreeSixtyReview");

	}
}
