using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    private Player player;
    private string filePath;

    public List<Item> Items => items;
    public int  Gold { private set; get; }


    private void Awake() => filePath = Application.dataPath + "/Resources/DB_inventory.txt";

    private void OnEnable()
    {
        player   = GetComponentInParent<Player>();

        Load();
    }


    /// <summary>
    /// 파일로부터 인벤토리 정보를 읽어온다.
    /// </summary>
    public List<Item> Load()
    {
        if ( !File.Exists(filePath) )
        {
            player.Inventory.IncreaseGold(123456789);
            player.Inventory.AddItem(new Item(ItemManager.Instance.GetItem("Sword0"), 2));
            player.Inventory.AddItem(new Item(ItemManager.Instance.GetItem("Shoes0"), 2));
            player.Inventory.AddItem(new Item(ItemManager.Instance.GetItem("Halmet0"), 2));
            player.Inventory.AddItem(new Item(ItemManager.Instance.GetItem("Armor0"), 2));
            player.Inventory.AddItem(new Item(ItemManager.Instance.GetItem("HP Potion"), 100));
            player.Inventory.AddItem(new Item(ItemManager.Instance.GetItem("MP Potion"), 3));
            player.Inventory.AddItem(new Item(ItemManager.Instance.GetItem("EXP Potion"), 3));
            player.Inventory.AddItem(new Item(ItemManager.Instance.GetItem("Jar"), 1000));
            Save();
        }

        //string code     = File.ReadAllText(filePath);
        //byte[] bytes    = System.Convert.FromBase64String(code);
        //string jsonData = System.Text.Encoding.UTF8.GetString(bytes);
        string jsonData = File.ReadAllText(filePath);
        InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(jsonData);

        items.Clear();
        items = inventoryData.Items;
        Gold  = inventoryData.Gold;

        return items;
    }

    /// <summary>
    /// 인벤토리 정보를 파일로 저장한다.
    /// </summary>
    private void Save()
    {
        items.RemoveAll(x => x.count <= 0);

        string jsonData = JsonUtility.ToJson(new InventoryData(this));
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
        Item carriedItem = items.Find(x => x.id == item.id);
        if ( carriedItem != null )
        {
            carriedItem.count += item.count;
        }
        else
        {
            items.Add(new Item(item));
        }
        Save();
    }

    /// <summary>
    /// Gold 획득
    /// </summary>
    public void IncreaseGold(int gold)
    {
        Gold += gold;
        Save();
    }

    /// <summary>
    /// Consumable 아이템 사용
    /// </summary>
    public void UseConsumable(Item item)
    {
        Item carriedItem = items.Find(x => x.id == item.id);
        if ( carriedItem != null )
        {
            carriedItem.count--;
            player.UseConsumable(ItemManager.Instance.GetConsumable(item.id));
        }
        Save();
    }

    /// <summary>
    /// 장비 장착/교체
    /// </summary>
    public void Equip(Item item)
    {
        Item carriedItem = items.Find(x => x.id == item.id);
        if ( carriedItem != null )
        {
            carriedItem.count--;

            Equipment equipment = ItemManager.Instance.GetEquipment(item.name);
            switch ( equipment.EquipmentType )
            {
                case EquipmentType.Weapon:
                    AddItem(ItemManager.Instance.GetItem(player.Weapon.EquipName));
                    player.ChangeWeapon(equipment.EquipName);
                    break;
                case EquipmentType.Shoes:
                    AddItem(ItemManager.Instance.GetItem(player.Shoes.EquipName));
                    player.ChangeShoes(equipment.EquipName);
                    break;
                case EquipmentType.Halmet:
                    AddItem(ItemManager.Instance.GetItem(player.Halmet.EquipName));
                    player.ChangeHalmet(equipment.EquipName);
                    break;
                case EquipmentType.Armor:
                    AddItem(ItemManager.Instance.GetItem(player.Armor.EquipName));
                    player.ChangeArmor(equipment.EquipName);
                    break;
            }
        }
        Save();
    }
}

[System.Serializable]
public class InventoryData
{
    public int        Gold;
    public List<Item> Items;

    public InventoryData(Inventory inventory)
    {
        Gold  = inventory.Gold;
        Items = inventory.Items;
    }
}
