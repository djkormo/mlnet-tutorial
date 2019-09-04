using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Website.Pages
{
    public class IndexModel : PageModel
    {
        public class InputForm
        {
            [Display(Name = "Title")]
            public string Title { get; set; }

            [Display(Name = "Description")]
            [DataType(DataType.MultilineText)]
            public string Description { get; set; }
        }

        [BindProperty]
        public InputForm Input { get; set; }
        public string PredictedArea { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            //TODO: Enter your code here.
            return Page();
        }
    }
}