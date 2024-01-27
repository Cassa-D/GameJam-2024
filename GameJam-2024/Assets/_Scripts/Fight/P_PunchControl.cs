using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_PunchControl : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField][Tooltip("Delay for punch to turn off. In milliseconds")] private int punchDelay = 500;
    
    private Collider col;
    
    private void Start()
    {
        col = GetComponent<Collider>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<P_Health>().TakeDamage(damage, transform.parent);
        }
    }
    
    private void OnDrawGizmos()
    {
        if (col != null && col.enabled)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, transform.localScale.x);
        }
    }

    private Task punchTask;
    
    public void OnPunch(InputAction.CallbackContext context)
    {
        if (!enabled) return;
        
        if (punchTask is { IsCompleted: false })
        {
            return;
        }
        
        if (context.started)
        {
            punchTask = Cassa.Utils.Delay(col, punchDelay, true);
        }
    }
}
