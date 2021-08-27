using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EquipSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private Image         equipImage;
    [SerializeField] private EquipmentType equipmentType;

    private bool isClicked;

    public Item Item { set; get; }


    private void Update()
    {
        Player player = GameManager.Instance.Player;
        if ( player == null ) { return; }

        switch ( equipmentType )
        {
            case EquipmentType.Weapon:
                Item = ItemManager.Instance.GetItem(player.Weapon.EquipName);
                break;
            case EquipmentType.Shoes:
                Item = ItemManager.Instance.GetItem(player.Shoes.EquipName);
                break;
            case EquipmentType.Halmet:
                Item = ItemManager.Instance.GetItem(player.Halmet.EquipName);
                break;
            case EquipmentType.Armor:
                Item = ItemManager.Instance.GetItem(player.Armor.EquipName);
                break;
        }
        if ( Item != null ) { equipImage.sprite = ItemManager.Instance.GetSprite(Item.id); }
    }


    /// <summary>
    /// 마우스가 위에 놓이면 활성화
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData) => GameManager.Instance.ItemInfoPanel.GetComponent<ItemInfoPanel>().Show(Item);

    /// <summary>
    /// 마우스가 벗어나면 비활성화
    /// </summary>
    public void OnPointerExit(PointerEventData eventData) => GameManager.Instance.ItemInfoPanel.SetActive(false);

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

        switch ( equipmentType )
        {
            case EquipmentType.Weapon:
                player.ChangeWeapon("Hand");
                player.Inventory.AddItem(ItemManager.Instance.GetItem(Item.name));
                break;
            case EquipmentType.Shoes:
                player.ChangeShoes("Foot");
                player.Inventory.AddItem(ItemManager.Instance.GetItem(Item.name));
                break;
            case EquipmentType.Halmet:
                player.ChangeHalmet("Head");
                player.Inventory.AddItem(ItemManager.Instance.GetItem(Item.name));
                break;
            case EquipmentType.Armor:
                player.ChangeArmor("Body");
                player.Inventory.AddItem(ItemManager.Instance.GetItem(Item.name));
                break;
        }
    }

    private IEnumerator ReleaseClick()
    {
        yield return new WaitForSeconds(0.2f);

        isClicked = false;
    }
}
