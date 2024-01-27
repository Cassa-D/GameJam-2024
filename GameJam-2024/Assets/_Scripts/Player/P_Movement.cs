using UnityEngine;
using UnityEngine.InputSystem;

public class P_Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed = 5f;

    private Vector2 movement;
    
    [SerializeField] private Transform rotateTransform;
    
    public bool CanWalk = true;
    
    private void FixedUpdate()
    {
        if (CanWalk)
        {
            rb.MovePosition(rb.position + new Vector3(movement.x, 0, movement.y) * (speed * Time.fixedDeltaTime));
        }
        
        if (movement != Vector2.zero)
        {
            rotateTransform.rotation = Quaternion.LookRotation(new Vector3(movement.x, 0, movement.y));
        }
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }
}
