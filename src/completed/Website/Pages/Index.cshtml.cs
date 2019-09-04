using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using GithubIssueClassifier.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Website.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GithubIssueLabeler _githubIssueLabeler;

        public IndexModel(GithubIssueLabeler githubIssueLabeler)
        {
            _githubIssueLabeler = githubIssueLabeler;
        }

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

        public IActionResult OnPostAsync()
        {
            var issue = new GithubIssue
            {
                Title = Input.Title,
                Description = Input.Description
            };

            var prediction = _githubIssueLabeler.PredictLabel(issue);
            PredictedArea = prediction.Area;

            return Page();
        }
    }
}