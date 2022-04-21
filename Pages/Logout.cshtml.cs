using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ThreeSixtyPlusAI.Data;

namespace ThreeSixtyPlusAI.Pages;

public class LogoutModel : PageModel
{
    
    public LogoutModel()
    {
    }

    public IActionResult OnGet()
    {
        Utils.SetAccessCode(HttpContext, "");
        Utils.SetHasThreeSixtyReview(HttpContext, false);
        return RedirectToPage("Index");
    }

}
