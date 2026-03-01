using System;
using Reqnroll;

namespace ReqnrollQuickstart.Specs.StepDefinitions
{
    [Binding]
    public class PriceCalculationStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly PriceCalculator _priceCalculator = new();
        private readonly Dictionary<string,int> _basket= new();
        private decimal _calculatedPrice ;

        public PriceCalculationStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given("the client started shopping")]
        public void GivenTheClientStartedShopping()
        {
            _basket.Clear();
            _calculatedPrice = 0.0m;
        }

        [Given("the client added {int} pcs of {string} to the basket")]
        public void GivenTheClientAddedPcsOfToTheBasket(int quantity, string product)
        {
            _basket.Add(product, quantity);
        }

        [Given("the client added")]
        public void GivenTheClientAdded(DataTable itemsTable)
        {
            var items = itemsTable.CreateSet<(string product, int quantity)>();
            foreach (var item in items)
            {
                _basket.Add(item.product, item.quantity);
            }
        }


        [When("the basket is prepared")]
        public void WhenTheBasketIsPrepared()
        {
            _calculatedPrice = _priceCalculator.CalculatePrice(_basket);
        }

        [Then("the basket price should be ${float}")]
        public void ThenTheBasketPriceShouldBe(Decimal expectedPrice)
        {
            Assert.AreEqual(expectedPrice, _calculatedPrice);
        }

        [Then("the basket price should not be %{float}")]
        public void ThenTheBasketPriceShouldNotBe(Decimal expectedPrice)
        {
            Assert.AreNotEqual(expectedPrice, _calculatedPrice);
        }

    }
}
