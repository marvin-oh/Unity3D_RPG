using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType { Weapon, Shoes, Halmet, Armor }

public class Equipment : MonoBehaviour
{
    [SerializeField] protected string        equipName;
    [SerializeField] protected EquipmentType equipmentType;

    public string        EquipName     => equipName;
    public EquipmentType EquipmentType => equipmentType;

    public int ID => ItemManager.Instance.GetItem(equipName).id;
}
