using System.Collections.Generic;

public enum ItemType { weapon=0, consumable, material }

[System.Serializable]
public class Item
{
    public string name;     // 이름
    public int    count;    // 개수
    public string desc;     // 설명
    public int    id;       // ID
    public int    type;     // 타입

    public Item(string _name, int _count, string _desc, int _id, int _type)
    {
        name    = _name;
        count   = _count;
        desc    = _desc;
        id      = _id;
        type    = _type;
    }

    public Item(Item target) 
        : this(target.name, target.count, target.desc, target.id, target.type) { }

    public Item(string _name, int _count, string _desc, int _id, ItemType _type) 
        : this(_name, _count, _desc, _id, (int)_type) { }
}
