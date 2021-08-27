using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalSlot : MonoBehaviour
{
    [SerializeField] private Text portalNameText;
    [SerializeField] private Text portalDescText;
    [SerializeField] private Text portalPriceText;
    private Player player;

    public Portal Portal { set; get; }


    private void Update()
    {
        if ( Portal == null ) { return; }
        portalNameText.text  = Portal.Name;
        portalDescText.text  = Portal.Desc;
        portalPriceText.text = Portal.Price.ToString();
    }


    /// <summary>
    /// 포탈 이용
    /// </summary>
    public void MoveBtn()
    {
        player = GameManager.Instance.Player;

        if ( player.Inventory.Gold >= Portal.Price )
        {
            player.Inventory.IncreaseGold(-Portal.Price);
            StartCoroutine(Transfer(player.gameObject, Portal.Location));
            GameManager.Instance.Notice(player.Name + " use " + Portal.Name + " Portal");
        }
        else
        {
            GameManager.Instance.Notice("Not enough money");
        }
    }

    /// <summary>
    /// Player를 지정한 위치로 이동시킨다.
    /// </summary>
    private IEnumerator Transfer(GameObject player, Vector3 position)
    {
        while ( true )
        {
            float distance = Vector3.Distance(player.transform.position, position);
            if (distance <= 0.1) { break; }

            player.transform.position = position;

            yield return null;
        }
    }
}
