using Entities;

namespace ServiceContracts.DTO
{
	public class SellOrderResponse
	{
		public Guid SellOrderID { get; set; }

		public string? StockSymbol { get; set; }

		public string? StockName { get; set; }

		public DateTime? DateAndTimeOfOrder { get; set; }

		public uint Quantity { get; set; }

		public double Price { get; set; }

		public double TradeAmount { get; set; }

		public override bool Equals(object? obj)
		{
			if (obj == null) return false;

			if (obj.GetType() != typeof(SellOrderResponse)) return false;

			SellOrderResponse other = (SellOrderResponse)obj;

			return SellOrderID == other.SellOrderID
				&& StockSymbol == other.StockSymbol
				&& StockName == other.StockName
				&& DateAndTimeOfOrder == other.DateAndTimeOfOrder
				&& Quantity == other.Quantity
				&& Price == other.Price;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}

	public static class SellOrderExtensions
	{
		public static SellOrderResponse ToSellOrderResponse(this SellOrder buyOrder)
		{
			return new SellOrderResponse
			{
				SellOrderID = buyOrder.SellOrderID,
				StockSymbol = buyOrder.StockSymbol,
				StockName = buyOrder.StockName,
				DateAndTimeOfOrder = buyOrder.DateAndTimeOfOrder,
				Quantity = buyOrder.Quantity,
				Price = buyOrder.Price
			};
		}
	}
}
