using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 5f;

    private Vector2 movement;
    
    private void FixedUpdate()
    {   
        rb.MovePosition(rb.position + new Vector3(movement.x, 0, movement.y) * (speed * Time.fixedDeltaTime));
        
        if (movement != Vector2.zero)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.y));
        }
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
}
