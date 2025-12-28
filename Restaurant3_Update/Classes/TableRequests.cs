using Restaurant_3.Intrefaces;

public class TableRequests
{
    // Maximum number of customers at one table
    private const int MaxCustomers = 8;

    // Stores orders for each customer (0..7)
    private IMenuItem[][] requests = new IMenuItem[MaxCustomers][];

    // Number of items ordered by each customer
    private int[] requestCount = new int[MaxCustomers];

    // Initialize order list size for a customer
    public void InitSize(int customer, uint size)
    {
        // Check customer index
        if (customer < 0 || customer >= MaxCustomers)
            throw new Exception("Invalid customer number");

        // Order must contain at least one item
        if (size == 0)
            throw new Exception("Order size must be greater than zero");

        // Allocate array for customer's orders
        requests[customer] = new IMenuItem[size];

        // Reset item counter
        requestCount[customer] = 0;
    }

    // Add menu item to customer's order
    public void Add(int customer, IMenuItem item)
    {
        // Validate customer index
        if (customer < 0 || customer >= MaxCustomers)
            throw new Exception("Invalid customer number");

        // Item cannot be null
        if (item == null)
            throw new Exception("Menu item cannot be null");

        // Customer must be initialized before adding items
        if (requests[customer] == null)
            throw new Exception("Customer orders not initialized");

        // Prevent array overflow
        if (requestCount[customer] >= requests[customer].Length)
            throw new Exception("Customer order limit exceeded");

        // Add item to the next free position
        requests[customer][requestCount[customer]] = item;
        requestCount[customer]++;
    }

    // Indexer: get all menu items of the same type
    public IMenuItem[] this[IMenuItem item]
    {
        get
        {
            // Count matching items
            int total = 0;

            for (int c = 0; c < MaxCustomers; c++)
            {
                if (requests[c] == null) continue;

                for (int i = 0; i < requestCount[c]; i++)
                {
                    // Compare item types
                    if (requests[c][i].GetType() == item.GetType())
                        total++;
                }
            }

            // Create result array
            IMenuItem[] result = new IMenuItem[total];
            int index = 0;

            // Fill result array
            for (int c = 0; c < MaxCustomers; c++)
            {
                if (requests[c] == null) continue;

                for (int i = 0; i < requestCount[c]; i++)
                {
                    if (requests[c][i].GetType() == item.GetType())
                        result[index++] = requests[c][i];
                }
            }

            return result;
        }
    }

    // Indexer: get all orders of one customer
    public IMenuItem[] this[int customer]
    {
        get
        {
            // Validate customer index
            if (customer < 0 || customer >= MaxCustomers)
                throw new Exception("Invalid customer number");

            // If customer has no orders, return empty array
            if (requests[customer] == null)
                return Array.Empty<IMenuItem>();

            // Copy only existing items
            IMenuItem[] result = new IMenuItem[requestCount[customer]];

            for (int i = 0; i < requestCount[customer]; i++)
                result[i] = requests[customer][i];

            return result;
        }
    }

    // Check if there is at least one order
    public bool HasRequests()
    {
        for (int c = 0; c < MaxCustomers; c++)
        {
            if (requestCount[c] > 0)
                return true;
        }
        return false;
    }
}
