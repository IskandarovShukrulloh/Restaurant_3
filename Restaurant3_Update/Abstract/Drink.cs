using Restaurant_3.Intrefaces;

namespace Restaurant_3.Abstract
{
    public abstract class Drink : IMenuItem
    {
        public ItemState State { get; set; } = ItemState.Requested;

        public virtual void Obtain()
        {
            if (State == ItemState.Requested)
                State = ItemState.Obtained;
        }

        // Give drink to customer
        public virtual void Serve()
        {
            if (State == ItemState.Obtained)
                State = ItemState.Served;
        }
    }
}
