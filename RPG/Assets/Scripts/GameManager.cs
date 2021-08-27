using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }
    private void Awake() => Instance = this;

    [Header("Notice")]
    [SerializeField] private Text noticeText;
    private bool         onNotice;
    private List<string> buffer = new List<string>();

    [Header("Player")]
    [SerializeField] private GameObject statusPanel;
    [SerializeField] private GameObject inventoryPanel;

    [Header("NPC")]
    [SerializeField] private Button     npcTalkBtn;
    [SerializeField] private GameObject npcTalkPanel;
    [SerializeField] private GameObject npcShopPanel;
    [SerializeField] private GameObject npcQuestPanel;

    [Header("Item")]
    [SerializeField] private GameObject itemInfoPanel;

    [Header("Portal")]
    [SerializeField] private List<Portal> portals;
    [SerializeField] private GameObject   portalPanel;

    public Player       Player { set; get; }
    public GameObject   StatusPanel    => statusPanel;
    public GameObject   InventoryPanel => inventoryPanel;

    public Button       NpcTalkBtn     => npcTalkBtn;
    public GameObject   NpcTalkPanel   => npcTalkPanel;
    public GameObject   NpcShopPanel   => npcShopPanel;
    public GameObject   NpcQuestPanel  => npcQuestPanel;

    public GameObject   ItemInfoPanel  => itemInfoPanel;

    public List<Portal> Portals        => portals;
    public GameObject   PortalPanel    => portalPanel;


    public void Notice(string message) => StartCoroutine(NoticeWithBuffer(message));

    public IEnumerator NoticeWithBuffer(string message)
    {
        buffer.Add(message);

        if ( !onNotice )
        {
            onNotice = true;

            while ( buffer.Count > 0 )
            {
                noticeText.text = buffer[0];
                buffer.RemoveAt(0);

                yield return new WaitForSeconds(1);

                noticeText.text = "";
            }

            onNotice = false;
        }
    }

    /// <summary>
    /// 모든 Panel 닫기 (PlayerController에서 호출)
    /// </summary>
    public void CloseAllPanel()
    {
        // Player Panel
        statusPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        // NPC    Panel
        npcTalkPanel.SetActive(false);
        npcShopPanel.SetActive(false);
        npcQuestPanel.SetActive(false);
        // Item   Panel
        itemInfoPanel.SetActive(false);
        // Portal Panel
        portalPanel.SetActive(false);
    }
}
