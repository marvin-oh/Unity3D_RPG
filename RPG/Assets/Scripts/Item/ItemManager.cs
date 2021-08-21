using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { private set; get; }
    private void Awake() => Instance = this;

    [SerializeField] private List<Weapon> weapons;
    [SerializeField] private List<Sprite> sprites;

    public Weapon GetWeapon(string _name) => weapons.Find(x => x.name == _name);
    public Sprite GetSprite(int id)       => sprites.Find(x => x.name.Contains(id.ToString()));
}
