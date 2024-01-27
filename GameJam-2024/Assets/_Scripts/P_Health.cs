using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class P_Health : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private Image healthBar;
    [SerializeField, Tooltip("In milliseconds.")] private int stunTime = 500;
    public Color playerColor;
    
    private Rigidbody rb;
    
    private void Start()
    {
        healthBar.color = playerColor;
        rb = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        healthBar.fillAmount = health / 3f;
    }

    private Task stunTask;
    [SerializeField] private float DEBUG_force = 5f;

    public void TakeDamage(int damage, Transform other = null)
    {
        if (stunTask is { IsCompleted: false })
        {
            return;
        }
        
        if (other != null)
        {
            Debug.Log("Player hit!");
            Vector3 direction = (transform.position - other.position).normalized;
            rb.AddForce(direction * DEBUG_force, ForceMode.Impulse);
            stunTask = Cassa.Utils.Delay(GetComponent<PlayerMovement>(), stunTime);
        }

        health -= damage;

        if (health <= 0)
        {
            Debug.Log("Player died!");
            // Destroy(gameObject);
        }
    }
}
