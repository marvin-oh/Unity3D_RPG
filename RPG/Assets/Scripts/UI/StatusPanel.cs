using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusPanel : MonoBehaviour
{
    [Header("Equip Panel")]
    [SerializeField] private Image  weaponImage;
    [SerializeField] private Image  footImage;
    [SerializeField] private Image  headImage;
    [SerializeField] private Image  bodyImage;
    [SerializeField] private Sprite blankSprite;

    [Header("Stat Panel")]
    [SerializeField] private Text hpText;
    [SerializeField] private Text atkText;
    [SerializeField] private Text defText;
    [SerializeField] private Text spdText;
    [SerializeField] private Text criText;

    private Player target;

    /// <summary>
    /// target의 Status 정보를 보여준다.
    /// </summary>
    public void Show(Player _target)
    {
        target = _target;

        // Equip Panel Update
        weaponImage.sprite = ItemManager.Instance.GetSprite(target.Weapon.ID);
        //footImage.sprite   = ItemManager.Instance.GetSprite(target.Shoes.ID);
        //headImage.sprite   = ItemManager.Instance.GetSprite(target.Helmet.ID);
        //bodyImage.sprite   = ItemManager.Instance.GetSprite(target.Armor.ID);

        // Stat Panel Update
        hpText.text  = string.Format("{0:##,##0} / {1:##,##0}", target.Hp, target.MaxHp);
        atkText.text = string.Format("{0:##,##0}", target.Atk);
        defText.text = string.Format("{0:##,##0}", target.Def);
        spdText.text = string.Format("{0:##,##0}", target.Spd);
        criText.text = string.Format("{0:##,##0}%", target.Cri * 100);
    }
}
