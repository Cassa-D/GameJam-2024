using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Movable : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (CanMove())
        {
            transform.position += new Vector3(-Variables.Instance.StreetSpeed * Time.fixedDeltaTime, 0, 0);
        }
    }

    public abstract bool CanMove();
    
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Item Destroyer"))
        {
            Destroy(gameObject);
        }
    }
}
