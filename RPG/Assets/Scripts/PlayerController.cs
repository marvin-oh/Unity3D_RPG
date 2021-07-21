using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player    player;
    private Transform cameraTransform;

    [SerializeField]
    private KeyCode   jumpKeyCode = KeyCode.Space;

    private void Awake()
    {
        player = GetComponent<Player>();
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        // 방향키를 눌러 이동
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // 이동 함수 호출 (카메라가 보고있는 방향을 기준으로 방향키에 따라 이동)
        player.MoveTo(cameraTransform.rotation * new Vector3(x, 0, z));
        // 회전 설정 (항상 앞만 보도록 캐릭터의 회전은 카메라와 같은 회전 값으로 설정)
        transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);

        // Space키를 누르면 점프
        if ( Input.GetKeyDown(jumpKeyCode) )
        {
            player.JumpTo();
        }
    }
}
