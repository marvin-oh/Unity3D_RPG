using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcTalkPanel : MonoBehaviour
{
    [SerializeField] private Text npcNameText;
    [SerializeField] private Text dialogText;
    [SerializeField] private GameObject npcShopBtn;
    [SerializeField] private GameObject npcQuestBtn;
    private List<string> dialogs;


    private void OnEnable() => dialogText.text = dialogs != null ? dialogs[Random.Range(0, dialogs.Count)] : "";


    public void Setup(NPC npc)
    {
        npcNameText.text = npc.Name;
        dialogs          = npc.Dialogs;
        npcShopBtn.SetActive(npc.Shop);
        npcQuestBtn.SetActive(npc.Quest);
    }
}
