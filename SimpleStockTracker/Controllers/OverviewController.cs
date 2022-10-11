using Microsoft.AspNetCore.Mvc;
using SimpleStockTracker.Models;

namespace SimpleStockTracker.Controllers
{
    public class OverviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Account(string Nickname)
        {
            // if no parameter is provided, redirect the user to the list of nicknames
            if (Nickname == null)
            {
                return RedirectToAction("Index");
            }

            // pass the input parameter for the account nickname to the ViewData object
            ViewData["Nickname"] = Nickname;

            // hardcode a list of holdings to display for now
            var holdings = new List<Holding>();
            for (var i = 0; i < 11; i++)
            {
                holdings.Add(new Holding { HoldingId = i, Ticker = "Ticker #" + i.ToString(), TradeDate = DateTime.Now, TradeType="Buy", Quantity=2, Price=3.21m, Account="RESP" });
            }

            return View(holdings);
        }
    }
}
