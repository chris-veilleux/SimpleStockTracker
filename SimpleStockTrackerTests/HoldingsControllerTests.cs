using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleStockTracker.Controllers;
using SimpleStockTracker.Data;
using SimpleStockTracker.Models;
using System.Security.Principal;

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

        #region "Index Tests"
        [TestMethod]
        public void IndexLoadsView()
        {
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
        #endregion

        #region "Details Tests"
        [TestMethod]
        public void DetailsNoIdLoads404()
        {
            // act
            var result = (ViewResult)controller.Details(null).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DetailsNoHoldingsTableLoads404()
        {
            // arrange
            context.Holding = null;

            // act
            var result = (ViewResult)controller.Details(null).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DetailsInvalidIdLoads404()
        {
            // act
            var result = (ViewResult)controller.Details(1000000).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DetailsValidIdLoadsView()
        {
            // act
            var result = (ViewResult)controller.Details(50).Result;

            // assert
            Assert.AreEqual("Details", result.ViewName);

        }

        [TestMethod]
        public void DetailsValidIdLoadsHolding()
        {
            // act
            var result = (ViewResult)controller.Details(50).Result;

            // assert
            Assert.AreEqual(context.Holding.Find(50), result.Model);

        }
        #endregion

        #region "Create Tests"

        [TestMethod]
        public void GetCreateLoadsView()
        {
            // act
            var result = controller.Create();
            var resultView = (ViewResult)result;

            // assert 
            Assert.AreEqual("Create", resultView.ViewName);
        }

        [TestMethod]
        public void PostCreateLoadsView()
        {
            // arrange        
            var holding = new Holding { };
            controller.ModelState.AddModelError("Model error", "1");

            // act
            var result = controller.Create(holding);
            var resultView = (ViewResult)result.Result;

            // assert         
            Assert.AreEqual("Create", resultView.ViewName);
        }

        #endregion

        #region "Edit Tests"

        [TestMethod]
        public void GetEditNoIdLoads404()
        {
            // act
            var result = (ViewResult)controller.Edit(null).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void GetEditNoHoldingsTableLoads404()
        {
            // arrange
            context.Holding = null;

            // act
            var result = (ViewResult)controller.Edit(null).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void GetEditInvalidIdLoads404()
        {
            // act
            var result = (ViewResult)controller.Edit(1000000).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void GetEditValidIdReutrnsHolding()
        {
            // act
            var result = (ViewResult)controller.Edit(50).Result;

            // assert
            Assert.AreEqual(context.Holding.Find(50), result.Model);
        }

        [TestMethod]
        public void GetEditValidIdLoadsView()
        {
            // act
            var result = (ViewResult)controller.Edit(50).Result;

            // assert
            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod]
        public void PostEditMismatchedIdLoads404()
        {
            // act
            var result = (ViewResult)controller.Edit(null, new Holding { HoldingId = 1, Ticker = "ABCD", AccountId = 20, Account = new Account { AccountId = 20, Name = "Money Account 2", AccountHolder = "Jane Doe", Type = "RRSP" }, Price = 20, Quantity = 5, TradeType = "BUY", TradeDate = new DateTime() }).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void PostEditInvalidHoldingIdLoads404()
        {
            // act
            var result = (ViewResult)controller.Edit(1, new Holding { HoldingId = 1, Ticker = "ABCD", AccountId = 20, Account = new Account { AccountId = 30, Name = "Money Account 2", AccountHolder = "Jane Doe", Type = "RRSP" }, Price = 20, Quantity = 5, TradeType = "BUY", TradeDate = new DateTime() }).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }

        #endregion

        #region "Delete Tests"

        [TestMethod]
        public void GetDeleteNoIdLoads404()
        {
            // act
            var result = (ViewResult)controller.Delete(null).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void GetDeleteNoHoldingsTableLoads404()
        {
            // arrange
            context.Holding = null;

            // act
            var result = (ViewResult)controller.Delete(null).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void GetDeleteInvalidIdLoads404()
        {
            // act
            var result = (ViewResult)controller.Delete(1000000).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void GetDeleteValidIdReutrnsHolding()
        {
            // act
            var result = (ViewResult)controller.Delete(50).Result;

            // assert
            Assert.AreEqual(context.Holding.Find(50), result.Model);
        }

        [TestMethod]
        public void GetDeleteValidIdLoadsView()
        {
            // act
            var result = (ViewResult)controller.Delete(50).Result;

            // assert
            Assert.AreEqual("Delete", result.ViewName);
        }

        [TestMethod]
        public void PostDeleteValidIdDeletesHolding()
        {
            // act
            var result = controller.Delete(50);
            var resultView = (ViewResult)result.Result;
            Holding holding = (Holding)resultView.Model;

            // assert
            Assert.AreEqual(context.Holding.Find(50), holding);
        }

        [TestMethod]
        public void PostDeleteDeleteConfirmedWorks()
        {
            // act
            var result = controller.DeleteConfirmed(50);
            var holding = context.Holding.Find(50);

            // assert
            Assert.AreEqual(holding, null);
        }

        #endregion
    }
}