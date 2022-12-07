using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleStockTracker.Controllers;
using SimpleStockTracker.Data;
using SimpleStockTracker.Models;

namespace SimpleStockTrackerTests
{
    [TestClass]
    public class HoldingsControllerTests
    {
        // database variable for all tests
        private ApplicationDbContext context;
        HoldingsController controller;

        // runs before each unit test
        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            context = new ApplicationDbContext(options);

            var account = new Account { AccountId = 20, Name = "Money Account", AccountHolder = "John Doe", Type = "RRSP" };
            context.Add(account);

            for (var i = 50; i < 61; i++)
            {
                var holding = new Holding { HoldingId = i, Ticker = "TK-" + i.ToString(), AccountId = 20, Account = account, Price = i / 2, Quantity = i, TradeType = "BUY", TradeDate = new DateTime() };
                context.Add(holding);
            }
            context.SaveChanges();

            controller = new HoldingsController(context);

        }

        [TestMethod]
        public void IndexLoadsView()
        {
            // arrange - done in test initialization

            // act
            var result = (ViewResult)controller.Index().Result;

            // assert
            Assert.AreEqual("Index", result.ViewName);

        }

        [TestMethod]
        public void IndexLoadsHoldings()
        {
            // act
            var result = (ViewResult)controller.Index().Result;
            List<Holding> model = (List<Holding>)result.Model;

            // assert
            CollectionAssert.AreEqual(context.Holding.ToList(), model);
        }
    }
}