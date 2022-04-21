using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThreeSixtyPlusAI.Data;
using ThreeSixtyPlusAI.Models;
using ThreeSixtyPlusAI.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ThreeSixtyPlusAI.Pages;

public class ViewThreeSixtyReview : PageModel
{

	private readonly IOpenAI _openAI;

	private readonly ThreeSixtyPlusAIContext _context;

	private readonly IWebHostEnvironment _env;

	public List<Question> QuestionsAsked = new();

	public List<string> AnswererAccessCodes { get; set; } = new List<string>();

	public decimal AnsweredPercentage { get; set; }

	public string Title { get; set; } = null!;

	[BindProperty]
	[Display(Name = "Dry Run - shows the payload that will be sent OpenAI without sending (only available in development mode)")]
	public bool DoDryRun { get; set; }

	public bool IsInDevelopmentMode { get; set; }

	public ViewThreeSixtyReview(
		ThreeSixtyPlusAIContext context,
		IOpenAI openAI,
		IWebHostEnvironment env)
	{
		_context = context;
		_openAI = openAI;
		_env = env;
	}


	public async Task OnGetAsync()
	{

		if (!Utils.GetHasThreeSixtyReview(HttpContext))
		{
			throw new Exception();
		}

		var AccessCode = Utils.GetAccessCode(HttpContext);

		var ThreeSixtyReview = await _context.ThreeSixtyReviews
			.Include(x => x.ThreeSixtyReviewers)
			.Include(x => x.ThreeSixtyReviewQuestions)
			.AsSplitQuery()
			.Where(x => x.AccessCode == AccessCode)
			.FirstOrDefaultAsync() ?? throw new Exception();

		IsInDevelopmentMode = _env.IsDevelopment();

		Title = ThreeSixtyReview.Title;

		var host = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

		AnswererAccessCodes = ThreeSixtyReview.ThreeSixtyReviewers.Select(x => host + "/AnswerThreeSixtyReview/" + x.AccessCode).ToList();

		var ReviewersCount = ThreeSixtyReview.ThreeSixtyReviewers.Count;
		var HasFinished = ThreeSixtyReview.ThreeSixtyReviewers.Where(x => x.HasFinished == true).Count();

		AnsweredPercentage = Decimal.Divide(HasFinished, ReviewersCount) * 100;

		QuestionsAsked = ThreeSixtyReview.ThreeSixtyReviewQuestions.Join(_context.Questions, x => x.QuestionId, y => y.Id, (selectedQuestion, question) => question).ToList();

	}

	public async Task<IActionResult> OnPostAsync()
	{

		if (!Utils.GetHasThreeSixtyReview(HttpContext))
		{
			throw new Exception();
		}

		var AccessCode = Utils.GetAccessCode(HttpContext);

		var ThreeSixtyReview = await _context.ThreeSixtyReviews
			.Where(x => x.AccessCode == AccessCode)
			.FirstOrDefaultAsync() ?? throw new Exception();


		IsInDevelopmentMode = _env.IsDevelopment();

		if(IsInDevelopmentMode && DoDryRun) {
			TempData["payload"] = await _openAI.GeneratePayload(ThreeSixtyReview.Id);
		} else {
			TempData["summary"] = await _openAI.GenerateSummary(ThreeSixtyReview.Id);
		}

		return RedirectToPage("DownloadThreeSixtyReview");

	}


}
