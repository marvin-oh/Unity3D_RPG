using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcShopPanel : MonoBehaviour
{
    [SerializeField] private Text         npcNameText;
    [SerializeField] private GameObject[] slots;


    private void OnEnable()
    {
        // 인벤토리와 함께 활성화
        GameManager.Instance.InventoryPanel.SetActive(true);
        GameManager.Instance.InventoryPanel.GetComponent<InventoryPanel>().Show();
    }


    /// <summary>
    /// NPC 및 Player 정보 설정
    /// </summary>
    public void Setup(NPC npc)
    {
        npcNameText.text = npc.Name;

        for ( int i=0; i<slots.Length; ++i )
        {
            bool isExist = i < npc.Items.Count;
            slots[i].SetActive(isExist);
            slots[i].GetComponent<NpcShopSlot>().Item = isExist ? ItemManager.Instance.GetItem(npc.Items[i]) : null;
        }
    }
}
