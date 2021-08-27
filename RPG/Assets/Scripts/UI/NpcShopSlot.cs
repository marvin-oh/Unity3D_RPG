using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcShopSlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Text  itemNameText;
    [SerializeField] private Text  itemDescText;
    [SerializeField] private Text  itemPriceText;

    public Item Item { set; get; }


    private void Update()
    {
        if ( Item == null ) { return; }
        itemImage.sprite   = ItemManager.Instance.GetSprite(Item.id);
        itemNameText.text  = Item.name;
        itemDescText.text  = Item.desc;
        itemPriceText.text = Item.price.ToString();
    }


    /// <summary>
    /// 아이템 구매
    /// </summary>
    public void BuyBtn()
    {
        Player player = GameManager.Instance.Player;

        if ( player.Inventory.Gold >= Item.price )
        {
            player.Inventory.IncreaseGold(-Item.price);
            player.Inventory.AddItem(Item);
            GameManager.Instance.Notice(player.Name + " bought " + Item.name);
        }
        else
        {
            GameManager.Instance.Notice("Not enough money");
        }
    }
}
