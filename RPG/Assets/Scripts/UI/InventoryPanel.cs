using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] slots;
    [SerializeField] private Text         goldText;

    private List<Item> curItems    = new List<Item>();
    private ItemType   currentType = ItemType.Equipment;
    private Player     target;

    /// <summary>
    /// target의 아이템들을 슬롯에 나열한다.
    /// </summary>
    public void Show(Player _target)
    {
        target = _target;

        List<Item> allItems = target.Inventory.Load();

        curItems.Clear();
        curItems = allItems.FindAll(x => x.type == currentType);

        for (int i = 0; i < slots.Length; ++i)
        {
            bool isExist = i < curItems.Count;
            slots[i].SetActive(isExist);
            slots[i].GetComponent<Slot>().Item = isExist ? curItems[i] : null;
        }

        goldText.text = string.Format("{0:##,##0}", target.Inventory.Gold);
    }

    /// <summary>
    /// 탭 메뉴 클릭시 인벤토리 갱신
    /// </summary>
    public void TabClick(int type)
    {
        if ( currentType != (ItemType)type )
        {
            currentType = (ItemType)type;

            Show(target);
        }
    }
}
