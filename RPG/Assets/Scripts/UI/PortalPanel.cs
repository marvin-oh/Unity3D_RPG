using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalPanel : MonoBehaviour
{
    [SerializeField] private Text         portalNameText;
    [SerializeField] private GameObject[] slots;


    /// <summary>
    /// Portal의 정보를 기반으로 패널 설정
    /// </summary>
    public void Setup(Portal portal)
    {
        portalNameText.text = portal.Name;

        List<Portal> portalsToGo = GameManager.Instance.Portals.FindAll(x => x != portal);
        for ( int i=0; i<slots.Length; ++i )
        {
            bool isExist = i < portalsToGo.Count;
            slots[i].SetActive(isExist);
            slots[i].GetComponent<PortalSlot>().Portal = isExist ? portalsToGo[i] : null;
        }
    }
}
