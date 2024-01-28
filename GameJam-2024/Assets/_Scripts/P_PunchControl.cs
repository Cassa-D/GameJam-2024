using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_PunchControl : MonoBehaviour
{
    private Collider col;
    private Task punchTask;
    private Task punchCooldownTask;
    
    private void Start()
    {
        col = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<P_Health>().TakeDamage(Variables.Instance.PunchDamage, transform.parent, Variables.Instance.PunchForce, GetComponentInParent<P_Health>().playerIndex);
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

    public void OnPunch(InputAction.CallbackContext context)
    {
        if (!enabled) return;

        if (punchTask is { IsCompleted: false } || punchCooldownTask is { IsCompleted: false })
        {
            return;
        }

        if (context.started)
        {
            punchTask = Punch();
            punchCooldownTask = Task.Delay(Variables.Instance.PunchCooldown);
        }
    }

    private async Task Punch()
    {
        GetComponentInParent<P_Movement>().CanWalk = false;
        GetComponentInParent<P_Movement>().CanRotate = false;
        col.enabled = true;

        await Task.Delay(Variables.Instance.PunchDuration);

        GetComponentInParent<P_Movement>().CanWalk = true;
        GetComponentInParent<P_Movement>().CanRotate = true;
        col.enabled = false;
    }
}
