using UnityEngine;
using UnityEngine.InputSystem;

public class P_Grab : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;
    
    [SerializeField] private P_Movement movement;
    [SerializeField] private P_PunchControl punchControl;
    
    private Throwable grabbedObject;
    private Throwable closestObject;
    
    private IT_ItemBox itemBox;

    private void OnTriggerEnter(Collider other)
    {
        if (grabbedObject != null) return;
        
        if (other.TryGetComponent(out Throwable item))
        {
            if (item.Mode != Throwable.ItemMode.Default) return;

            closestObject = item;
        }
        
        if (other.TryGetComponent(out IT_ItemBox itemBox))
        {
            this.itemBox = itemBox;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (grabbedObject != null) return;
        
        if (other.TryGetComponent(out Throwable item))
        {
            if (item.Mode != Throwable.ItemMode.Default) return;

            closestObject = null;
        }
        
        if (other.TryGetComponent(out IT_ItemBox _))
        {
            itemBox = null;
        }
    }
    
    public void OnGrab(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (closestObject != null)
            {
                GrabObject();
                return;
            }
        
            if (itemBox != null)
            {
                itemBox.Open(this);
                itemBox = null;
            }
        }

        if (context.canceled)
        {
            Thrown();
        }
    }
    
    public void OnGrab(Throwable obj)
    {
        if (grabbedObject != null) return;
        
        closestObject = obj;
        GrabObject();
    }

    private void GrabObject()
    {
        if (closestObject != null)
        {
            grabbedObject = closestObject;
            closestObject = null;
            
            grabbedObject.transform.SetParent(grabPoint);
            grabbedObject.transform.localPosition = Vector3.zero;
            grabbedObject.transform.localRotation = Quaternion.identity;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            grabbedObject.GetComponent<Throwable>().Mode = Throwable.ItemMode.PickedUp;
            grabbedObject.GetComponent<Throwable>().owner = gameObject.GetComponentInParent<P_Health>().gameObject;

            movement.CanWalk = false;
            punchControl.enabled = false;
        }
    }

    private void Thrown()
    {
        if (grabbedObject != null)
        {
            grabbedObject.transform.SetParent(null);
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject.GetComponent<Rigidbody>().AddForce(transform.forward * Variables.Instance.ThrowForce, ForceMode.Impulse);
            grabbedObject.GetComponent<Throwable>().Mode = Throwable.ItemMode.Thrown;
            grabbedObject = null;

            movement.CanWalk = true;
            punchControl.enabled = true;
        }
    }
}
