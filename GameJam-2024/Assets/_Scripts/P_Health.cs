using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class P_Health : MonoBehaviour
{
    [ProgressBar(0, 3, Segmented = true, ColorGetter = "GetHealthBarColor")]
    [SerializeField] private int health = 3;
    
    private Color GetHealthBarColor()
    {
        switch (health)
        {
            case 3:
                return Color.green;
            case 2:
                return Color.yellow;
            case 1:
                return Color.red;
            default:
                return Color.black;
        }
    }
    
    [SerializeField] private Image healthBar;
    public Color playerColor;
    
    private Rigidbody rb;

    private Task stunTask;
    
    private void Start()
    {
        healthBar.color = playerColor;
        rb = GetComponent<Rigidbody>();

        health = Variables.Instance.PlayerHealth;
    }
    
    private void Update()
    {
        healthBar.fillAmount = health / 3f;
    }

    public void TakeDamage(int damage, Transform other = null, float force = 5f)
    {
        if (stunTask is { IsCompleted: false })
        {
            return;
        }
        
        if (other != null)
        {
            Vector3 direction = (transform.position - other.position).normalized;
            rb.AddForce(direction * force, ForceMode.Impulse);
            stunTask = Cassa.Utils.Delay(GetComponent<P_Movement>(), Variables.Instance.StunTime);
        }

        health -= damage;

        if (health <= 0)
        {
            Debug.Log("Player died!");
            // Destroy(gameObject);
        }
    }
}
