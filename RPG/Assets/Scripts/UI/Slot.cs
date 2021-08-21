using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Text  itemNameText;
    [SerializeField] private Text  itemCountText;

    public Item Item { set; get; }

    private void Update()
    {
        if ( Item == null ) { return; }
        itemImage.sprite   = ItemManager.Instance.GetSprite(Item.id);
        itemNameText.text  = Item.name;
        itemCountText.text = Item.count.ToString();
    }
}
