using Tower.Runtime.ToolKit;

namespace Tower.Runtime.GameSystem
{
    public class StoreSystem : SystemBase
    {
        public int Money { get; private set; } = 80;

        public bool CostMoney(int cost)
        {
            if (Money - cost < 0)
            {
                return false; 
            }

            Money -= cost;

            EventKit.GlobalEvent.Trigger(Core.EGlobalEvent.OnUpdateMoneyUI, Money);

            return true;
        }

        public void GetMoney(int value)
        {
            Money += value;
            EventKit.GlobalEvent.Trigger(Core.EGlobalEvent.OnUpdateMoneyUI, Money);
        }
    }
}