using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { private set; get; }
    private void Awake() => Instance = this;

    [SerializeField] private ItemSO       itemSO;
    [SerializeField] private ConsumableSO consumableSO;

    [SerializeField] private List<Weapon> weapons;
    [SerializeField] private List<Shoes>  shoes;
    [SerializeField] private List<Halmet> halmets;
    [SerializeField] private List<Armor>  armors;
    [SerializeField] private List<Sprite> sprites;

    public Item       GetItem(string _name)       => itemSO.Items.Find(x => x.name == _name);
    public Consumable GetConsumable(int id)       => consumableSO.Consumables.Find(x => x.id == id);
    public Consumable GetConsumable(string _name) => GetConsumable(GetItem(_name).id);

    public Weapon GetWeapon(int id)       => weapons.Find(x => x.ID == id);
    public Weapon GetWeapon(string _name) => weapons.Find(x => x.name == _name);
    public Shoes  GetShoes(int id)        => shoes.Find(x => x.ID == id);
    public Shoes  GetShoes(string _name)  => shoes.Find(x => x.name == _name);
    public Halmet GetHalmet(int id)       => halmets.Find(x => x.ID == id);
    public Halmet GetHalmet(string _name) => halmets.Find(x => x.name == _name);
    public Armor  GetArmor(int id)        => armors.Find(x => x.ID == id);
    public Armor  GetArmor(string _name)  => armors.Find(x => x.name == _name);
    public Sprite GetSprite(int id)       => sprites.Find(x => x.name.Contains(id.ToString()));

    public Equipment GetEquipment(string _name)
    {
        List<Equipment> equipments = new List<Equipment>();
        weapons.ForEach(x => equipments.Add(x));
        shoes.ForEach(x => equipments.Add(x));
        halmets.ForEach(x => equipments.Add(x));
        armors.ForEach(x => equipments.Add(x));

        return equipments.Find(x => x.EquipName == _name);
    }
}
