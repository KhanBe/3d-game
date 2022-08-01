using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCharacterController : MonoBehaviour
{
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;

    Animator anim;
    [SerializeField]
    private Rigidbody rb;

    private float canJump = 0f;
    public float jumpForce = 300;
    public float timeBeforeNextJump = 1.2f;

    void Start()
    {
        anim = characterBody.GetComponent<Animator>();
    }

    void Update()
    {
        LookAround();
        Move();
    }

    void Move()
    {
        Vector3 moveDir = Vector3.zero;
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0;
        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = moveDir;
            transform.position += moveDir * Time.deltaTime * 5f;
        }

        if (moveDir == Vector3.zero) anim.SetInteger("Walk", 0);
        else anim.SetInteger("Walk", 1);
        //카메라암 방향 레이
        //Debug.DrawRay(cameraArm.position, cameraArm.forward, Color.red);
        //Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized, Color.red);

        if (Input.GetButtonDown("Jump") && Time.time > canJump)
        {
            rb.AddForce(0, jumpForce, 0);
            canJump = Time.time + timeBeforeNextJump;

            //레이캐스트를 이용해 바닥에 닿으면 모션 취소하고 timeBeforeNextJump변수를 바꿔줘야할듯
            anim.SetTrigger("jump");
        }
    }

    void LookAround()//마우스로 카메라 회전하는 함수
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y;

        //위로회전 제한
        // 각을 0 부터 안하고 -1f로 부터 제한하는 이유는 수평면 아래로 안내려가서 답답함
        if (x < 180f) x = Mathf.Clamp(x, -1f, 70f);
        //아래회전 제한
        else x = Mathf.Clamp(x, 335f, 361f);

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }
}
