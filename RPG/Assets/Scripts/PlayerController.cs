using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Player    player;
    private Transform cameraTransform;

    [SerializeField] private KeyCode    jumpKeyCode = KeyCode.Space;

    [Header("UI")]
    [SerializeField] private GameObject inventoryPanelPrefab;
    private GameObject inventoryPanel;

    private void Awake()
    {
        Cursor.visible   = false;                     // 마우스 커서를 보이지 않게
        Cursor.lockState = CursorLockMode.Locked;     // 마우스 커서 위치 고정

        player          = GetComponent<Player>();
        cameraTransform = Camera.main.transform;
        inventoryPanel  = Instantiate(inventoryPanelPrefab, GameObject.FindGameObjectWithTag("MainCanvas").transform);
        inventoryPanel.SetActive(false);
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

        // 인벤토리 창 팝업
        if ( Input.GetKeyDown(KeyCode.I) ) { ToggleInventory(); }

        // 인벤토리 창 팝업 중에는 동작 x
        if ( inventoryPanel.activeInHierarchy ) { return; }

        // 마우스 좌클릭시 무기 공격
        if ( Input.GetMouseButtonDown(0) ) { player.Attack(); }
        if ( Input.GetKeyDown(KeyCode.R) ) { player.Attack(); }
    }

    private void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);

        Camera.main.GetComponent<CameraController>().Setup(inventoryPanel.activeInHierarchy ? null : player.gameObject);
    }
}
