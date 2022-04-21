using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThreeSixtyPlusAI.Data;

namespace ThreeSixtyPlusAI.Pages;

public class DownloadThreeSixtyReview : PageModel
{

	private readonly ThreeSixtyPlusAIContext _context;

	public DownloadThreeSixtyReview(
		ThreeSixtyPlusAIContext context)
	{
		_context = context;
	}


	public async Task OnGetAsync()
	{

		if (!Utils.GetHasThreeSixtyReview(HttpContext))
		{
			throw new Exception();
		}

		if(TempData["summary"] is not null) {

			var AccessCode = Utils.GetAccessCode(HttpContext);

			var ThreeSixtyReview = await _context.ThreeSixtyReviews
				.Include(x => x.ThreeSixtyReviewers)
				.Include(x => x.ThreeSixtyReviewQuestions)
				.AsSplitQuery()
				.Where(x => x.AccessCode == AccessCode)
				.FirstOrDefaultAsync() ?? throw new Exception();

			_context.ThreeSixtyReviews.Remove(ThreeSixtyReview);

			await _context.SaveChangesAsync();
			Utils.SetAccessCode(HttpContext, "");
			Utils.SetHasThreeSixtyReview(HttpContext, false);

		}


	}


}
