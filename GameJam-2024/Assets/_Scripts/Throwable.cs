using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Throwable : Movable
{
     public enum ItemMode
     {
          Default, Thrown, PickedUp
     }

     private string defaultLayer = "Item";
     private string thrownLayer = "Thrown";
     
     private Rigidbody rb;
     private Collider col;
     
     [HideInPlayMode, HideInEditorMode]
     public GameObject owner;
     
     private bool FLAG_stop;

     [ShowInInspector, ReadOnly]
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
                         col.enabled = true;
                         gameObject.layer = LayerMask.NameToLayer(defaultLayer);
                         break;
                    case ItemMode.Thrown:
                         col.enabled = true;
                         gameObject.layer = LayerMask.NameToLayer(thrownLayer);
                         break;
                    case ItemMode.PickedUp:
                         col.enabled = false;
                         gameObject.layer = LayerMask.NameToLayer(defaultLayer);
                         break;
               }
          }
     }

     public void Start()
     {
          rb = GetComponent<Rigidbody>();
          col = GetComponent<Collider>();
     }

     private void Update()
     {
          if (mode == ItemMode.Thrown && FLAG_stop)
          {
               if (rb.velocity.magnitude < 0.1f)
               {
                    FLAG_stop = false;
                    Mode = ItemMode.Default;
               }
          }
     }

     private new void OnCollisionEnter(Collision other)
     {
          base.OnCollisionEnter(other);
          if (other.gameObject.CompareTag("Player") && Mode == ItemMode.Thrown && other.gameObject != owner)
          {
               other.gameObject.GetComponent<P_Health>().TakeDamage(Variables.Instance.DefaultThrowableDamage);
               Destroy(gameObject);
          }
          
          if (other.gameObject.CompareTag("Ground") && Mode == ItemMode.Thrown)
          {
               FLAG_stop = true;
          }
     }

     public override bool CanMove()
     {
          return mode == ItemMode.Default && !FLAG_stop;
     }
}
