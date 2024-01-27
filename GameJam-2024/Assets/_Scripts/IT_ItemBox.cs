using UnityEngine;
using Random = UnityEngine.Random;

public class IT_ItemBox : Movable
{
    public void Open(P_Grab player)
    {
        ItemScriptable item = Variables.Instance.BoxItems[Random.Range(0, Variables.Instance.BoxItems.Length)];
        
        Debug.Log("Item: " + item.type);
        
        if (item.type == ItemScriptable.ItemType.Consumable)
        {
            if (item.consumableType == ItemScriptable.ConsumableType.Speed)
            {
                player.GetComponentInParent<P_Movement>().PowerUp(item.consumableValue);
            }
        }
        else
        {
            Throwable itemA = Instantiate(item.itemPrefab, transform.position, Quaternion.identity).GetComponent<Throwable>();
            itemA.Start();
            player.OnGrab(itemA);
        }

        Destroy(gameObject);
    }

    public override bool CanMove()
    {
        return true;
    }
}
