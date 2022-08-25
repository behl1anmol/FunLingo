using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FunLingo.Pages
{
    public class GameModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private static readonly List<string> _questions = new List<string>();


        [BindProperty(Name="Players",SupportsGet = true)]
        public static string NoOfPlayers
        {
            get;set;
        }

        public static string Id
        {
            get;set;
        }

        public static string RandomNo
        {
            get; set;
        }


        public static List<string> Questions => _questions;

        public GameModel(ILogger<IndexModel> logger)
        {
            _logger = logger;

        }
        public void OnGet()
        {
            NoOfPlayers = Request.Query["Players"];
            ViewData["Players"] = NoOfPlayers;
            RandomNo = Request.Query["RandomNumber"];
            ViewData["RandomNumber"] = RandomNo;
            Id = Request.Query["QId"];

            if(Questions.Count == 0)
            {
                using StreamReader file = new StreamReader("Questions.txt");
                string? ln;

                while ((ln = file.ReadLine()) != null)
                {
                    _questions.Add(ln);
                }
                file.Close();
            }
            ViewData["RandomQuestion"] = Questions.ElementAt(Int16.Parse(Id));


        }
        public IActionResult OnPostGenerateRandomNumber()
        {
            Random rnd = new Random();

            RandomNo =  rnd.Next(1, Int16.Parse(NoOfPlayers)).ToString();

            return RedirectToPage("/Game", new { Players = NoOfPlayers, RandomNumber = RandomNo, QId = Id }); 
        }
        public IActionResult OnPostGenerateRandomQuestion()
        {
            Random rnd = new Random();

            Id = rnd.Next(1, Questions.Count).ToString();

            return RedirectToPage("/Game", new { Players = NoOfPlayers, RandomNumber = RandomNo, QId = Id });
        }
    }
}
