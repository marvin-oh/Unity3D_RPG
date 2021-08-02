using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Text  itemNameText;
    [SerializeField] private Text  itemCountText;
    [SerializeField] private Text  itemDescText;

    [SerializeField] private Sprite[] sprites;

    public Item Item { set; get; }

    private void Update()
    {
        itemImage.sprite   = sprites[Item.itemNum];
        itemNameText.text  = Item.name;
        itemCountText.text = Item.count.ToString();
        itemDescText.text  = Item.desc;
    }
}
