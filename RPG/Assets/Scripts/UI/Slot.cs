using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Text  itemNameText;
    [SerializeField] private Text  itemCountText;

    [SerializeField] private Sprite[] sprites;

    public Item Item { set; get; }

    private void Update()
    {
        itemImage.sprite   = sprites[Item.id];
        itemNameText.text  = Item.name;
        itemCountText.text = Item.count.ToString();
    }
}
