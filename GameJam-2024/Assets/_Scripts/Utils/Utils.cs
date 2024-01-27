using System.Threading.Tasks;
using UnityEngine;

namespace Cassa
{
    public static class Utils
    {
        public static async Task Delay(MonoBehaviour obj, int delay)
        {
            obj.enabled = true;
            await Task.Delay(delay);
            obj.enabled = false;
        }
        
        public static async Task Delay(Collider obj, int delay)
        {
            obj.enabled = true;
            await Task.Delay(delay);
            obj.enabled = false;
        }
    }
}
