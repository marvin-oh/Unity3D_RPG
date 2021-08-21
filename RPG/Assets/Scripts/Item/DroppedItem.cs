using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    [SerializeField] private ItemSO ItemSO;
    [SerializeField] private int    id;

    public int ID => id;

    private void OnTriggerEnter(Collider other)
    {
        if ( other.CompareTag("Player") )
        {
            Item item = ItemSO.Items.Find(x => x.id == id);

            other.GetComponent<Player>().Inventory.AddItem(new Item(item));
            gameObject.SetActive(false);

            GameManager.Instance.Notice("get Item: " + item.name);
        }
    }
}
