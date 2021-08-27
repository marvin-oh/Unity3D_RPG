using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private Player player;
    private string filePath;

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private KeyCode   jumpKeyCode = KeyCode.Space;


    private void Awake() => filePath = Application.dataPath + "/Resources/DB_playerData.txt";

    private void OnEnable()
    {
        player = GetComponent<Player>();

        GameManager.Instance.Player = player;
        GameManager.Instance.CloseAllPanel();
        LoadData();
    }

    private void OnDisable()
    {
        player.Hp = player.MaxHp;
        SaveData();
    }

    private void Update()
    {
        KeyboardControl();
    }


    private void KeyboardControl()
    {
        // 방향키를 눌러 이동
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // 이동 함수 호출 (카메라가 보고있는 방향을 기준으로 방향키에 따라 이동)
        player.MoveTo(cameraTransform.rotation * new Vector3(x, 0, z));
        // 회전 설정 (항상 앞만 보도록 캐릭터의 회전은 카메라와 같은 회전 값으로 설정)
        transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);

        // Space키를 누르면 점프
        if ( Input.GetKeyDown(jumpKeyCode) ) { player.JumpTo(); }

        // UI 창 팝업
        if ( Input.GetKeyDown(KeyCode.I) )      { ToggleUI(GameManager.Instance.InventoryPanel); }
        if ( Input.GetKeyDown(KeyCode.E) )      { ToggleUI(GameManager.Instance.StatusPanel); }
        if ( Input.GetKeyDown(KeyCode.Escape) ) { GameManager.Instance.CloseAllPanel(); }

        // 마우스 좌클릭시 무기 공격
        if ( EventSystem.current.IsPointerOverGameObject() ) { return; }
        if ( Input.GetMouseButtonDown(0) ) { player.Attack(); }
        if ( Input.GetKeyDown(KeyCode.R) ) { player.Attack(); }
    }

    /// <summary>
    /// UI 활성화/비활성화 (Status/Inventory)
    /// </summary>
    private void ToggleUI(GameObject ui)
    {
        bool isOff = !ui.activeInHierarchy;
        ui.SetActive(isOff);
        if      ( isOff && (ui == GameManager.Instance.InventoryPanel) ) { ui.GetComponent<InventoryPanel>().Show(); }
        else if ( isOff && (ui == GameManager.Instance.StatusPanel) )    { ui.GetComponent<StatusPanel>().Show(); }
    }

    /// <summary>
    /// PlayerData를 저장 (Inventory에서 호출)
    /// </summary>
    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(new PlayerData(player));
        //byte[] bytes    = System.Text.Encoding.UTF8.GetBytes(jsonData);
        //string code     = System.Convert.ToBase64String(bytes);
        //File.WriteAllText(filePath, code);
        File.WriteAllText(filePath, jsonData);
    }

    /// <summary>
    /// 저장된 PlayerData를 불러온다 (Inventory에서 호출)
    /// </summary>
    public void LoadData()
    {
        if ( !File.Exists(filePath) ) { SaveData(); }

        //string code     = File.ReadAllText(filePath);
        //byte[] bytes    = System.Convert.FromBase64String(code);
        //string jsonData = System.Text.Encoding.UTF8.GetString(bytes);
        string jsonData = File.ReadAllText(filePath);
        player.LoadData(JsonUtility.FromJson<PlayerData>(jsonData));
    }
}

[System.Serializable]
public class PlayerData
{
    public int   Level;
    public float Hp;
    public float Exp;
    public int   WeaponID;
    public int   ShoesID;
    public int   HalmetID;
    public int   ArmorID;

    public PlayerData(Player player)
    {
        Level    = player.Level;
        Hp       = player.Hp;
        Exp      = player.Exp;
        WeaponID = player.Weapon.ID;
        ShoesID  = player.Shoes.ID;
        HalmetID = player.Halmet.ID;
        ArmorID  = player.Armor.ID;
    }
}
