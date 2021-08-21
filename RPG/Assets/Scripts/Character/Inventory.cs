using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> allItems = new List<Item>();
    private string filePath;

    public int Gold { private set; get; }

    private void Awake() => filePath = Application.dataPath + "/Resources/inventory.txt";

    /// <summary>
    /// 파일로부터 인벤토리 정보를 읽어온다.
    /// </summary>
    public List<Item> Load()
    {
        allItems.Clear();

        if ( !File.Exists(filePath) )
        {
            allItems.Add(new Item("Sword",       1,    "Simple sword", 1,  ItemType.Equipment));
            allItems.Add(new Item("HP Potion",   100,  "HP + 100",     10, ItemType.Consumable));
            allItems.Add(new Item("EXP Potion",  2,    "EXP + 10",     12, ItemType.Consumable));
            allItems.Add(new Item("Jar",         1000, "Just a jar",   20, ItemType.Material));
            Gold = 1234567890;
            Save();
        }
        //string code     = File.ReadAllText(filePath);
        //byte[] bytes    = System.Convert.FromBase64String(code);
        //string jsonData = System.Text.Encoding.UTF8.GetString(bytes);
        string jsonData = File.ReadAllText(filePath);
        Serialization<Item> serialization = JsonUtility.FromJson<Serialization<Item>>(jsonData);
        allItems          = serialization.Target;
        Gold              = serialization.Gold;

        return allItems;
    }

    /// <summary>
    /// 인벤토리 정보를 파일로 저장한다.
    /// </summary>
    private void Save()
    {
        string jsonData = JsonUtility.ToJson(new Serialization<Item>(allItems, Gold));
        //byte[] bytes    = System.Text.Encoding.UTF8.GetBytes(jsonData);
        //string code     = System.Convert.ToBase64String(bytes);
        //File.WriteAllText(filePath, code);
        File.WriteAllText(filePath, jsonData);
    }

    /// <summary>
    /// 아이템 추가
    /// </summary>
    public void AddItem(Item item)
    {
        Load();
        Item carriedItem = allItems.Find(x => x.id == item.id);
        if ( carriedItem != null )
        {
            carriedItem.count += item.count;
        }
        else
        {
            allItems.Add(item);
        }
        Save();
    }

    /// <summary>
    /// Gold 획득
    /// </summary>
    public void IncreaseGold(int gold)
    {
        Load();
        Gold += gold;
        Save();
    }
}
