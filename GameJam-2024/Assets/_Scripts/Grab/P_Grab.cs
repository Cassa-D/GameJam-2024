using UnityEngine;
using UnityEngine.InputSystem;

public class P_Grab : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;
    [SerializeField] private float throwForce = 5f;
    
    [SerializeField] private P_Movement movement;
    [SerializeField] private P_PunchControl punchControl;
    
    private Item grabbedObject;
    private Item closestObject;

    private void OnTriggerEnter(Collider other)
    {
        if (grabbedObject != null) return;
        
        if (other.TryGetComponent(out Item item))
        {
            if (item.Mode != Item.ItemMode.Default) return;

            closestObject = item;
        }
    }
    
    public void OnGrab(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (closestObject != null)
            {
                grabbedObject = closestObject;
                closestObject = null;
                grabbedObject.transform.SetParent(grabPoint);
                grabbedObject.transform.localPosition = Vector3.zero;
                grabbedObject.transform.localRotation = Quaternion.identity;
                grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                grabbedObject.GetComponent<Item>().Mode = Item.ItemMode.PickedUp;
                grabbedObject.GetComponent<Item>().owner = gameObject.GetComponentInParent<P_Health>().gameObject;
                
                movement.CanWalk = false;
                punchControl.enabled = false;
            }
        }

        if (context.canceled)
        {
            if (grabbedObject != null)
            {
                grabbedObject.transform.SetParent(null);
                grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                grabbedObject.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                grabbedObject.GetComponent<Item>().Mode = Item.ItemMode.Thrown;
                grabbedObject = null;
                
                movement.CanWalk = true;
                punchControl.enabled = true;
            }
        }
    }
}
