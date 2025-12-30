using Restaurant_3.Intrefaces;
using System;

namespace Restaurant_3.Abstract
{
    public abstract class CookedFood : IMenuItem
    {
        public ItemState State { get; set; } = ItemState.Requested;

        // Raw item is obtained (before cooking)
        public virtual void Obtain()
        {
            if (State == ItemState.Requested)
               State = ItemState.Obtained;
        }

        // Cooking step (child classes can override if needed)
        public virtual void Cook()
        {
            if (State == ItemState.Obtained)
            {
                State = ItemState.Cooked;
            }
        }
        
        // Prepared food is served to customer
        public virtual void Serve()
        {
            if (State == ItemState.Served)
                throw new InvalidOperationException("Item is already served.");

            else if (State == ItemState.Obtained)
                State = ItemState.Served;
        }
    }
}
