using Tower.Runtime.Core;
using Tower.Runtime.GameSystem;

namespace Tower.Runtime.ToolKit
{
    public class SystemKit
    {
        public static T GetSystem<T>() where T : class, ISystem
        {
           return SystemManager.Instance?.Get<T>();
        }
    }
}