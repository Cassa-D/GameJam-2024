using System.Threading.Tasks;
using UnityEngine;

namespace Cassa
{
    public static class Utils
    {
        public static async Task Delay(MonoBehaviour obj, int delay, bool invert = false)
        {
            obj.enabled = invert;
            await Task.Delay(delay);
            obj.enabled = !invert;
        }
        
        public static async Task Delay(Collider obj, int delay, bool invert = false)
        {
            obj.enabled = invert;
            await Task.Delay(delay);
            obj.enabled = !invert;
        }
    }
}
