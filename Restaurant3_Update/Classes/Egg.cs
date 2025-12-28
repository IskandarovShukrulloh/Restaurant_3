using Restaurant_3.Abstract;
using System;

namespace Restaurant_3.Classes
{
    public class Egg : CookedFood, IDisposable
    {
        // Egg quality (1..100)
        private int? quality;

        // Indicates that shell was discarded
        private bool disposed = false;

        // Random generator for quality
        private static readonly Random rand = new Random();

        public int? Quality
        {
            get => quality;
            private set => quality = value;
        }

        // Generate egg quality if not generated yet
        public int GetQuality()
        {
            if (Quality == null)
                Quality = rand.Next(1, 101);

            return Quality.Value;
        }

        // Crack the egg (check if rotten)
        public void Crack()
        {
            if (State != ItemState.Obtained)
                return;

            if (Quality < 25)
                throw new Exception("Egg is rotten!");
        }

        // Discard egg shells
        public void DiscardShell()
        {
            disposed = true;
        }

        // Dispose egg (shells must be discarded)
        public void Dispose()
        {
            DiscardShell();
        }
    }
}
