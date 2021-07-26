using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3D : MonoBehaviour
{
    [SerializeField]
    private float   moveSpeed = 5.0f;   // 이동 속도
    [SerializeField]
    private float   gravity = -9.8f;    // 중력 계수
    [SerializeField]
    private float   jumpForce = 2.0f;   // 점프 힘
    private Vector3 moveDirection;      // 이동 방향

    private CharacterController characterController;

    public float MoveSpeed
    {
        // 이동속도는 2~5 사이의 값만 설정 가능
        set => moveSpeed = Mathf.Clamp(value, 2.0f, 5.0f);
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // 중력 설정. 플레이어가 땅을 밟고 있지 않다면
        // y축 이동방향에 gravity*Time.deltaTime을 더해준다.
        if ( characterController.isGrounded == false ) { moveDirection.y += gravity * Time.deltaTime; }

        // 이동 설정. CharacterController의 Move() 함수를 이용한 이동
        if ( characterController != null ) { characterController.Move(moveDirection * moveSpeed * Time.deltaTime); }
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = new Vector3(direction.x, moveDirection.y, direction.z);
    }

    public void JumpTo()
    {
        // 캐릭터가 땅을 밟고 있으면 점프
        if ( characterController.isGrounded == true )
        {
            moveDirection.y = jumpForce;
        }
    }
}
