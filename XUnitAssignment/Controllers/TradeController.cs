using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using XUnitAssignment.Models;
using XUnitAssignment.Options;

namespace XUnitAssignment.Controllers
{
	public class TradeController : Controller
	{
		private readonly IFinnhubService _finnhubService;
		private readonly IStockService _stockService;
		private readonly TradingOptions _options;
		private readonly IConfiguration _configuration;

		public TradeController(IFinnhubService finnhubService, IStockService stockService, IOptions<TradingOptions> tradingOptions, IConfiguration configuration)
		{
			_finnhubService = finnhubService;
			_stockService = stockService;
			_options = tradingOptions.Value;
			_configuration = configuration;
		}

		[Route("/Trade/Index")]
		public async Task<IActionResult> Index()
		{
			var defaultStockSymbol = _options.DefaultStockSymbol ?? "MSFT";
			var priceQuoteResponse = await _finnhubService.GetStockPriceQuote(defaultStockSymbol);
			var profileResponse = await _finnhubService.GetCompanyProfile(defaultStockSymbol);

			var stockTrade = new StockTrade()
			{
				StockSymbol = profileResponse["ticker"].ToString(),
				StockName = profileResponse["name"].ToString(),
				Price = Convert.ToDouble(priceQuoteResponse["c"].ToString())
			};

			ViewBag.FinnhubToken = _configuration["FinnhubToken"].ToString();
			return View(stockTrade);
		}
	}
}
