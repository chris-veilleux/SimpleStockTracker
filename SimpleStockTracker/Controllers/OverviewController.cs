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

        public IActionResult Account(string Name)
        {
            // if no parameter is provided, redirect the user to the list of names
            if (Name == null)
            {
                return RedirectToAction("Index");
            }

            // pass the input parameter for the account name to the ViewData object
            ViewData["Name"] = Name;

            // hardcode a list of holdings to display for now
            var holdings = new List<Holding>();
            for (var i = 0; i < 11; i++)
            {
                holdings.Add(new Holding { HoldingId = i, Ticker = "Ticker #" + i.ToString(), TradeDate = DateTime.Now, TradeType="Buy", Quantity=2, Price=3.21 });
            }

            return View(holdings);
        }
    }
}
