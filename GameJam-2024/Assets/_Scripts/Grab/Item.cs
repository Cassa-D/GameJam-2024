using System;
using UnityEngine;

public class Item : MonoBehaviour
{
     public enum ItemMode
     {
          Default, Thrown, PickedUp
     }
     
     [SerializeField] private int damage = 1;

     [SerializeField] private string defaultLayer;
     [SerializeField] private string thrownLayer;
     [SerializeField] private float streetSpeed;
     
     public GameObject owner;

     private ItemMode mode = ItemMode.Default;
     public ItemMode Mode
     {
          get => mode;
          set
          {
               mode = value;
               switch (value)
               {
                    case ItemMode.Default:
                         GetComponent<Collider>().enabled = true;
                         gameObject.layer = LayerMask.NameToLayer(defaultLayer);
                         break;
                    case ItemMode.Thrown:
                         GetComponent<Collider>().enabled = true;
                         gameObject.layer = LayerMask.NameToLayer(thrownLayer);
                         break;
                    case ItemMode.PickedUp:
                         GetComponent<Collider>().enabled = false;
                         gameObject.layer = LayerMask.NameToLayer(defaultLayer);
                         break;
               }
          }
     }

     private void FixedUpdate()
     {
          if (mode == ItemMode.Default)
          {
               transform.position += new Vector3(-streetSpeed * Time.fixedDeltaTime, 0, 0);
          }
     }

     private void OnCollisionEnter(Collision other)
     {
          if (other.gameObject.CompareTag("Player") && Mode == ItemMode.Thrown && other.gameObject != owner)
          {
               other.gameObject.GetComponent<P_Health>().TakeDamage(damage);
               Destroy(gameObject);
          }

          if (mode == ItemMode.Thrown && other.gameObject.CompareTag("Ground"))
          {
               owner = null;
               Mode = ItemMode.Default;
          }
     }
}
