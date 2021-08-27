using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ItemType { Equipment=0, Consumable, Material }

[System.Serializable]
public class Item
{
    public ItemType type;   // 타입
    public int      id;     // ID
    public string   name;   // 이름
    public string   desc;   // 설명
    public int      price;  // 가격
    public int      count;  // 개수

    public Item(ItemType _type, int _id, string _name, string _desc, int _price, int _count)
    {
        type  = _type;
        id    = _id;
        name  = _name;
        desc  = _desc;
        price = _price;
        count = _count;
    }

    public Item(Item target)
        : this(target.type, target.id, target.name, target.desc, target.price, target.count) { }

    public Item(Item target, int _count)
        : this(target.type, target.id, target.name, target.desc, target.price, target.count * _count) { }
}

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Object/ItemSO")]
public class ItemSO : ScriptableObject
{
    [SerializeField] private List<Item> items;

    public List<Item> Items { get => items; }
}
