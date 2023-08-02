using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using Xunit;

namespace Tests
{
	public class StockServiceTests
	{

		private readonly IStockService _stockService;

		public StockServiceTests() 
		{
			_stockService = new StockService();
		}

		#region CreateBuyOrder
		
		[Fact]
		public void CreateBuyOrder_RequestIsNullTest()
		{
			BuyOrderRequest? buyOrderRequest = null;

			Assert.Throws<ArgumentNullException>(() => _stockService.CreateBuyOrder(buyOrderRequest));
		}


		[Fact]
		public void CreateBuyOrder_QuantityValidationTest() 
		{
			uint lowOrderQuantity = 0;
			uint highOrderQuantity = 100001;
			var buyOrderRequest = GenerateValidBuyOrderRequest();
			buyOrderRequest.Quantity = lowOrderQuantity;

			var exception = Assert.Throws<ArgumentException>(() => _stockService.CreateBuyOrder(buyOrderRequest));
			Assert.Equal("Quantity value should be between 1 and 100000", exception.Message);

			buyOrderRequest.Quantity = highOrderQuantity;

			exception = Assert.Throws<ArgumentException>(() => _stockService.CreateBuyOrder(buyOrderRequest));
			Assert.Equal("Quantity value should be between 1 and 100000", exception.Message);
		}

		[Fact]
		public void CreateBuyOrder_PriceValidationTest()
		{
			uint lowOrderPrice = 0;
			uint highOrderPrice = 10001;
			var buyOrderRequest = GenerateValidBuyOrderRequest();
			buyOrderRequest.Price = lowOrderPrice;

			var exception = Assert.Throws<ArgumentException>(() => _stockService.CreateBuyOrder(buyOrderRequest));
			Assert.Equal("Price value should be between 1 and 10000", exception.Message);

			buyOrderRequest.Price = highOrderPrice;

			exception = Assert.Throws<ArgumentException>(() => _stockService.CreateBuyOrder(buyOrderRequest));
			Assert.Equal("Price value should be between 1 and 10000", exception.Message);
		}

		[Fact]
		public void CreateBuyOrder_StockSymbolIsNullTest()
		{
			var buyOrderRequest = GenerateValidBuyOrderRequest();
			buyOrderRequest.StockSymbol = null;

			var exception = Assert.Throws<ArgumentException>(() => _stockService.CreateBuyOrder(buyOrderRequest));
			Assert.Equal("Stock symbol cannot be blank", exception.Message);
		}

		[Fact]
		public void CreateBuyOrder_DateAndTimeValidationTest()
		{
			var buyOrderRequest = GenerateValidBuyOrderRequest();
			buyOrderRequest.DateAndTimeOfOrder = DateTime.Parse("1992-12-31");

			var exception = Assert.Throws<ArgumentException>(() => _stockService.CreateBuyOrder(buyOrderRequest));
			Assert.Equal("Date And Time Of Order should be later then 2000-01-01", exception.Message);
		}

		[Fact]
		public void CreateBuyOrder_AddNewOrder() 
		{
			var buyOrderRequest = GenerateValidBuyOrderRequest();

			var responseFromAdd = _stockService.CreateBuyOrder(buyOrderRequest);
			var responseFromGet = _stockService.GetBuyOrders().Single();

			Assert.Equal(responseFromAdd, responseFromGet);
		}

		#endregion

		#region GetAllBuyOrders

		[Fact]
		public void GetAllBuyOrders_EmptyCollectionTest() 
		{
			var response = _stockService.GetBuyOrders();

			Assert.Empty(response);
		}

		[Fact]
		public void GetAllBuyOrders_FilledCollectionTest() 
		{
			var buyOrderRequest_1 = GenerateValidBuyOrderRequest();
			var buyOrderRequest_2 = GenerateValidBuyOrderRequest();
			buyOrderRequest_2.StockSymbol = "APPL";
			buyOrderRequest_2.StockName = "Apple";

			var responseFromAdd_1 = _stockService.CreateBuyOrder(buyOrderRequest_1);
			var responseFromAdd_2 = _stockService.CreateBuyOrder(buyOrderRequest_2);

			var responseFromGet = _stockService.GetBuyOrders();

			Assert.Equal(2, responseFromGet.Count);
			foreach (var responseFromAdd in new[] { responseFromAdd_1, responseFromAdd_2 }) 
			{
				Assert.Contains(responseFromAdd, responseFromGet);
			}
		}
		#endregion

		#region CreateSellOrder

		[Fact]
		public void CreateSellOrder_RequestIsNullTest()
		{
			SellOrderRequest? sellOrderRequest = null;

			Assert.Throws<ArgumentNullException>(() => _stockService.CreateSellOrder(sellOrderRequest));
		}


		[Fact]
		public void CreateSellOrder_QuantityValidationTest()
		{
			uint lowOrderQuantity = 0;
			uint highOrderQuantity = 100001;
			var sellOrderRequest = GenerateValidSellOrderRequest();
			sellOrderRequest.Quantity = lowOrderQuantity;

			var exception = Assert.Throws<ArgumentException>(() => _stockService.CreateSellOrder(sellOrderRequest));
			Assert.Equal("Quantity value should be between 1 and 100000", exception.Message);

			sellOrderRequest.Quantity = highOrderQuantity;

			exception = Assert.Throws<ArgumentException>(() => _stockService.CreateSellOrder(sellOrderRequest));
			Assert.Equal("Quantity value should be between 1 and 100000", exception.Message);
		}

		[Fact]
		public void CreateSellOrder_PriceValidationTest()
		{
			uint lowOrderPrice = 0;
			uint highOrderPrice = 10001;
			var sellOrderRequest = GenerateValidSellOrderRequest();
			sellOrderRequest.Price = lowOrderPrice;

			var exception = Assert.Throws<ArgumentException>(() => _stockService.CreateSellOrder(sellOrderRequest));
			Assert.Equal("Price value should be between 1 and 10000", exception.Message);

			sellOrderRequest.Price = highOrderPrice;

			exception = Assert.Throws<ArgumentException>(() => _stockService.CreateSellOrder(sellOrderRequest));
			Assert.Equal("Price value should be between 1 and 10000", exception.Message);
		}

		[Fact]
		public void CreateSellOrder_StockSymbolIsNullTest()
		{
			var sellOrderRequest = GenerateValidSellOrderRequest();
			sellOrderRequest.StockSymbol = null;

			var exception = Assert.Throws<ArgumentException>(() => _stockService.CreateSellOrder(sellOrderRequest));
			Assert.Equal("Stock symbol cannot be blank", exception.Message);
		}

		[Fact]
		public void CreateSellOrder_DateAndTimeValidationTest()
		{
			var sellOrderRequest = GenerateValidSellOrderRequest();
			sellOrderRequest.DateAndTimeOfOrder = DateTime.Parse("1992-12-31");

			var exception = Assert.Throws<ArgumentException>(() => _stockService.CreateSellOrder(sellOrderRequest));
			Assert.Equal("Date And Time Of Order should be later then 2000-01-01", exception.Message);
		}

		[Fact]
		public void CreateSellOrder_AddNewOrder()
		{
			var sellOrderRequest = GenerateValidSellOrderRequest();

			var responseFromAdd = _stockService.CreateSellOrder(sellOrderRequest);
			var responseFromGet = _stockService.GetSellOrders().Single();

			Assert.Equal(responseFromAdd, responseFromGet);
		}

		#endregion

		#region GetAllSellOrders

		[Fact]
		public void GetAllSellOrders_EmptyCollectionTest()
		{
			var response = _stockService.GetSellOrders();

			Assert.Empty(response);
		}

		[Fact]
		public void GetAllSellOrders_FilledCollectionTest()
		{
			var sellOrderRequest_1 = GenerateValidSellOrderRequest();
			var sellOrderRequest_2 = GenerateValidSellOrderRequest();
			sellOrderRequest_2.StockSymbol = "APPL";
			sellOrderRequest_2.StockName = "Apple";

			var responseFromAdd_1 = _stockService.CreateSellOrder(sellOrderRequest_1);
			var responseFromAdd_2 = _stockService.CreateSellOrder(sellOrderRequest_2);

			var responseFromGet = _stockService.GetSellOrders();

			Assert.Equal(2, responseFromGet.Count);
			foreach (var responseFromAdd in new[] { responseFromAdd_1, responseFromAdd_2 })
			{
				Assert.Contains(responseFromAdd, responseFromGet);
			}
		}
		#endregion

		private BuyOrderRequest GenerateValidBuyOrderRequest() 
		{
			return new BuyOrderRequest()
			{
				StockSymbol = "MSFT",
				StockName = "Microsoft",
				Price = 10,
				DateAndTimeOfOrder = DateTime.Parse("2001-12-31"),
				Quantity = 10
			};
		}

		private SellOrderRequest GenerateValidSellOrderRequest()
		{
			return new SellOrderRequest()
			{
				StockSymbol = "MSFT",
				StockName = "Microsoft",
				Price = 10,
				DateAndTimeOfOrder = DateTime.Parse("2001-12-31"),
				Quantity = 10
			};
		}
	}
}