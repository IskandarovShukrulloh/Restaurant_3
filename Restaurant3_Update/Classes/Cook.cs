using Restaurant_3.Intrefaces;
using Restaurant_3.Abstract;

namespace Restaurant_3.Classes
{
    public class Cook
    {
        // Process all food requests (chicken and eggs)
        public static string Process(TableRequests requests)
        {
            // Get all chicken requests
            IMenuItem[] chickenItems = requests[new Chicken()];

            // Process each chicken
            for (int i = 0; i < chickenItems.Length; i++)
            {
                Chicken chicken = (Chicken)chickenItems[i];

                chicken.Obtain();
                chicken.CutUp();
                chicken.Cook();
            }

            // Get all egg requests
            IMenuItem[] eggItems = requests[new Egg()];

            // Process each egg
            for (int i = 0; i < eggItems.Length; i++)
            {
                Egg egg = (Egg)eggItems[i];

                try
                {
                    egg.Obtain();
                    egg.Crack();
                    egg.Cook();
                }
                finally
                {
                    // Shells must be discarded even if an exception occurs
                    egg.Dispose();
                }
            }

            return "Requests are sent to cook!";
        }
    }
}
