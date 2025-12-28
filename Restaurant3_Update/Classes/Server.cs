using Restaurant_3.Abstract;
using Restaurant_3.Intrefaces;

namespace Restaurant_3.Classes
{
	public class Server
	{
		// Index of the current customer (0..7)
		private byte currentCustomer = 0;

		// Indicates whether orders were cooked
		private bool cooked = false;

		// Stores and manages all customer orders
		private TableRequests tableRequests = new();

		public void ReceiveRequest(uint chickenQty, uint eggQty, Drink? drinkName)
		{
			// Check that the order is not empty
			if (chickenQty == 0 && eggQty == 0 && drinkName == null)
				throw new Exception("Null order!");

			// Check maximum number of customers
			if (currentCustomer >= 8)
				throw new Exception("Table is full (max 8 customers). No more orders allowed.");

			// Calculate required order size
			uint size = chickenQty + eggQty + (drinkName != null ? 1u : 0u);

			// Initialize order storage for current customer
			tableRequests.InitSize(currentCustomer, size);

			// Add chicken orders
			for (int c = 0; c < chickenQty; c++)
				tableRequests.Add(currentCustomer, new Chicken());

			// Add egg orders
			for (int e = 0; e < eggQty; e++)
				tableRequests.Add(currentCustomer, new Egg());

			// Add drink order if selected
			if (drinkName != null)
				tableRequests.Add(currentCustomer, drinkName);

			// Move to next customer
			currentCustomer++;
		}

		public string Send()
		{
			// Check that there is at least one order
			if (currentCustomer == 0)
				throw new Exception("There are no orders!");

			// Mark orders as cooked
			cooked = true;

			// Send all orders to the cook
			return Cook.Process(tableRequests);
		}

		public string Serve()
		{
			// Validate that orders were cooked and exist
			if (!cooked || !tableRequests.HasRequests())
				return "No orders or not cooked yet!";

			string result = "";

			// Process orders for each customer
			for (byte i = 0; i < currentCustomer; i++)
			{
				// Get orders of current customer
				IMenuItem[] items = tableRequests[i];

				// Skip if customer has no orders
				if (items.Length == 0)
					continue;

				int chickenCount = 0;
				int eggCount = 0;
				Drink? drink = null;

				// Count ordered items
				foreach (var item in items)
				{
					if (item is Chicken)
						chickenCount++;
					else if (item is Egg)
						eggCount++;
					else if (item is Drink d)
					{
						// Prepare drink
						d.Obtain();
						drink = d;
					}
				}

				// Add serving result for current customer
				result += $"Customer {i} is served {chickenCount} chicken, {eggCount} egg. {drink}\n";
			}

			// Reset server state for next session
			currentCustomer = 0;
			cooked = false;

			result += "Please enjoy your meal!";
			return result;
		}
	}
}
