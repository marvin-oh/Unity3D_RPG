using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    [SerializeField] private string itemName;   // 이름
    [SerializeField] private int    count;      // 개수
    [SerializeField] private string desc;       // 설명
    [SerializeField] private int    itemID;     // ID
    [SerializeField] private int    itemType;   // 타입

    public int ID => itemID;

    private void OnTriggerEnter(Collider other)
    {
        if ( other.CompareTag("Player") )
        {
            print("get DroppedItem: " + itemName);

            other.GetComponent<Player>().Inventory.AddItem(new Item(itemName, count, desc, itemID, itemType));
            gameObject.SetActive(false);
        }
    }
}
