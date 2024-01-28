using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class P_Health : MonoBehaviour
{
    [ProgressBar(0, 3, Segmented = true, ColorGetter = "GetHealthBarColor")]
    [SerializeField] private int health = 3;

    public int playerIndex { get; set; }

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

    public void TakeDamage(int damage, Transform other = null, float force = 5f, int damageDealerIndex = -1)
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
            if (damageDealerIndex != -1)
            {
                Variables.Instance.Scores[damageDealerIndex]++;
                Variables.Instance.Deaths[playerIndex]++;
            }
            Respawn();
        }
    }

    public async void Respawn()
    {
        // TODO - effects and stuff
        gameObject.SetActive(false);
            
        health = Variables.Instance.PlayerHealth;
        healthBar.fillAmount = health / 3f;
            
        Vector3 spawnPosition = new Vector3(Random.Range(-Variables.Instance.RespawnRange, Variables.Instance.RespawnRange), 0, Random.Range(-Variables.Instance.RespawnRange, Variables.Instance.RespawnRange));
        transform.position = spawnPosition + Variables.Instance.RespawnOffset;
        
        await Task.Delay((int)(Variables.Instance.RespawnTime * 1000));
        gameObject.SetActive(true);
    }
}
