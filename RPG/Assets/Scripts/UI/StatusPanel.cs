using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusPanel : MonoBehaviour
{
    [Header("Stat Panel")]
    [SerializeField] private Text hpText;
    [SerializeField] private Text mpText;
    [SerializeField] private Text expText;
    [SerializeField] private Text atkText;
    [SerializeField] private Text defText;
    [SerializeField] private Text spdText;
    [SerializeField] private Text criText;

    private Player player;


    private void Update() => Show();


    /// <summary>
    /// target의 Status 정보를 보여준다.
    /// </summary>
    public void Show()
    {
        player = GameManager.Instance.Player;

        // Stat Panel Update
        hpText.text  = string.Format("{0:##,##0} / {1:##,##0}", player.Hp, player.MaxHp);
        mpText.text  = string.Format("{0:##,##0} / {1:##,##0}", player.Mp, player.MaxMp);
        expText.text = string.Format("{0:##,##0} / {1:##,##0}", player.Exp, player.MaxExp);
        atkText.text = string.Format("{0:##,##0}", player.Atk);
        defText.text = string.Format("{0:##,##0}", player.Def);
        spdText.text = string.Format("{0:##,##0}", player.Spd);
        criText.text = string.Format("{0:##,##0}%", player.Cri * 100);
    }
}
