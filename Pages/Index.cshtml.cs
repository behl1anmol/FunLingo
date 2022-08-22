using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FunLingo.Pages;
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    [BindProperty]
    public int NoOfPlayers
    {
        get;set;
    }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {

    }

    public IActionResult OnPostPlayersAdded()
    {
        if (ModelState.IsValid == false)
        {
            return Page();
        }

        // Save Model to Database

        return RedirectToPage("/Game",new { Players = NoOfPlayers, RandomNumber = 0, QId = 0 });
    }



}
