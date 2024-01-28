using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private Vector2 movement;
    
    [SerializeField] private Transform rotateTransform;
    
    public bool CanWalk = true;
    
    public float speed = 5f;
    public bool CanRotate = true;

    private void Start()
    {
        speed = Variables.Instance.PlayerSpeed;
    }

    private void FixedUpdate()
    {
        if (CanWalk)
        {
            rb.MovePosition(rb.position + new Vector3(movement.x, 0, movement.y) * (speed * Time.fixedDeltaTime));
        }
        
        if (movement != Vector2.zero && CanRotate)
        {
            rotateTransform.rotation = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.y));
        }
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>().normalized;
    }

    public async Task PowerUp(float multiplier)
    {
        speed *= multiplier;
        await Task.Delay(10000);
        speed = Variables.Instance.PlayerSpeed;
    }
}
