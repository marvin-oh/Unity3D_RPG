using System.Collections.Generic;

[System.Serializable]
public class Item
{
    public string name;
    public int    count;
    public string desc;
    public int    itemNum;

    public Item(string _name, int _count, string _desc, int _itemNum)
    {
        name    = _name;
        count   = _count;
        desc    = _desc;
        itemNum = _itemNum;
    }

    public Item(Item target) : this(target.name, target.count, target.desc, target.itemNum) { }
}

[System.Serializable]
public class Serialization<T>
{
    public List<T> target;

    public Serialization(List<T> _target) => target = _target;
}
