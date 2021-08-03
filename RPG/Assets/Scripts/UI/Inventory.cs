using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject   slotPrefab;
    [SerializeField] private GameObject[] slots;

    private List<Item> allItems    = new List<Item>();
    private List<Item> curItems    = new List<Item>();
    private ItemType   currentType = ItemType.weapon;

    private string     filePath;

    private void Awake() => filePath = Application.dataPath + "/Resources/inventory.txt";

    private void OnEnable() => Load();

    /// <summary>
    /// 슬롯에 아이템들을 나열한다.
    /// </summary>
    private void SlotUpdate()
    {
        curItems.Clear();
        curItems = allItems.FindAll(x => x.type == (int)currentType);

        for ( int i=0; i<slots.Length; ++i )
        {
            bool isExist = i < curItems.Count;
            slots[i].SetActive(isExist);
            slots[i].GetComponent<Slot>().Item = isExist ? curItems[i] : null;
        }
    }

    /// <summary>
    /// 파일로부터 인벤토리 정보를 읽어온다.
    /// </summary>
    private void Load()
    {
        allItems.Clear();

        if ( !File.Exists(filePath) )
        {
            allItems.Add(new Item("체력물약",   100,  "소비시 체력을 100 회복한다.",  0, ItemType.consumable));
            allItems.Add(new Item("마나물약",   10,   "소비시 마나를 50 회복한다.",   1, ItemType.consumable));
            allItems.Add(new Item("경험치물약", 2,    "소비시 경험치를 10 획득한다.", 2, ItemType.consumable));
            allItems.Add(new Item("항아리",     1000, "물약을 만드는데 필요한 재료",  3, ItemType.material));
            Save();
        }
        //string code     = File.ReadAllText(filePath);
        //byte[] bytes    = System.Convert.FromBase64String(code);
        //string jsonData = System.Text.Encoding.UTF8.GetString(bytes);
        string jsonData = File.ReadAllText(filePath);
        allItems          = JsonUtility.FromJson<Serialization<Item>>(jsonData).target;

        SlotUpdate();
    }

    /// <summary>
    /// 인벤토리 정보를 파일로 저장한다.
    /// </summary>
    private void Save()
    {
        string jsonData = JsonUtility.ToJson(new Serialization<Item>(allItems));
        //byte[] bytes    = System.Text.Encoding.UTF8.GetBytes(jsonData);
        //string code     = System.Convert.ToBase64String(bytes);
        //File.WriteAllText(filePath, code);
        File.WriteAllText(filePath, jsonData);
    }

    public void TabClick(int type)
    {
        if ( currentType == (ItemType)type ) { return; }

        currentType = (ItemType)type;

        SlotUpdate();
    }
}
