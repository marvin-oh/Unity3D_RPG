using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Text  itemNameText;
    [SerializeField] private Text  itemCountText;

    private bool isClicked;

    public Item Item { set; get; }


    private void Update()
    {
        if ( Item == null ) { return; }
        itemImage.sprite   = ItemManager.Instance.GetSprite(Item.id);
        itemNameText.text  = Item.name;
        itemCountText.text = Item.count.ToString();
    }

    private void OnDisable() => GameManager.Instance.ItemInfoPanel.SetActive(false);


    /// <summary>
    /// 마우스가 위에 놓이면 활성화
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData) => GameManager.Instance.ItemInfoPanel.GetComponent<ItemInfoPanel>().Show(Item);

    /// <summary>
    /// 마우스가 벗어나면 비활성화
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)  => GameManager.Instance.ItemInfoPanel.SetActive(false);

    /// <summary>
    /// 클릭시 사용
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        // 더블클릭 체크
        if ( !isClicked )
        {
            isClicked = true;
            StartCoroutine(ReleaseClick());
            return;
        }
        else
        {
            isClicked = false;
        }

        Player player = GameManager.Instance.Player;

        if ( Item.type == ItemType.Equipment )
        {
            player.Inventory.Equip(Item);
        }
        else if ( Item.type == ItemType.Consumable )
        {
            player.Inventory.UseConsumable(Item);
        }
    }


    private IEnumerator ReleaseClick()
    {
        yield return new WaitForSeconds(0.2f);

        isClicked = false;
    }
}
