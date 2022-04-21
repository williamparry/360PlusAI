using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThreeSixtyPlusAI.Data;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ThreeSixtyPlusAI.Pages;

public class IndexModel : PageModel
{

	private readonly ThreeSixtyPlusAIContext _context;

	[BindProperty]
	[Display(Name = "Access Code")]
	[Required]
	public string AccessCodeInput { get; set; } = null!;

	public IndexModel(ThreeSixtyPlusAIContext context)
	{
		_context = context;
	}

	public async Task<IActionResult> OnPostAsync()
	{

		if (!ModelState.IsValid)
		{
			return Page();
		}

		bool ThreeSixtyReviewExists = await _context.ThreeSixtyReviews
            .Where(x => x.AccessCode == AccessCodeInput)
            .AnyAsync();

        if (!ThreeSixtyReviewExists)
        {
            ModelState.AddModelError("InvalidAccessCode", "Invalid Access Code.");
            return Page();
        }
		else
		{
			Utils.SetAccessCode(HttpContext, AccessCodeInput);
			Utils.SetHasThreeSixtyReview(HttpContext, true);
			return RedirectToPage("ViewThreeSixtyReview");
		}

	}
}
