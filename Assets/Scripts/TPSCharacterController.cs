using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCharacterController : MonoBehaviour
{
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;
    [SerializeField]
    private Rigidbody rb;

    Animator anim;

    private BoxCollider characterCollider;

    //�Ⱦ��� ���� ��� private float canJump = 0f;
    public float jumpForce = 300f;
    public float timeBeforeNextJump = 1.2f;

    static public bool isJumping = false;

    void Start()
    {
        anim = characterBody.GetComponent<Animator>();
        rb = characterBody.GetComponent<Rigidbody>();
        characterCollider = characterBody.GetComponent<BoxCollider>();
    }

    void Update()
    {
        LookAround();
        Move();
        follow();
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
        //ī�޶�� ���� ����
        //Debug.DrawRay(cameraArm.position, cameraArm.forward, Color.red);
        //Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized, Color.red);

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
            anim.SetTrigger("jump");
        }

        //���� ���� ���
        /*if (Input.GetButtonDown("Jump") && Time.time > canJump)
        {
            rb.AddForce(0, jumpForce, 0);
            canJump = Time.time + timeBeforeNextJump;

            //����ĳ��Ʈ�� �̿��� �ٴڿ� ������ ��� ����ϰ� timeBeforeNextJump������ �ٲ�����ҵ�
            anim.SetTrigger("jump");
        }*/
    }

    void LookAround()//���콺�� ī�޶� ȸ���ϴ� �Լ�
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y;

        //����ȸ�� ����
        // ���� 0 ���� ���ϰ� -1f�� ���� �����ϴ� ������ ����� �Ʒ��� �ȳ������� �����
        if (x < 180f) x = Mathf.Clamp(x, -1f, 70f);
        //�Ʒ�ȸ�� ����
        else x = Mathf.Clamp(x, 335f, 361f);

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }
    
    void follow()//ĳ���������� y���̵��� ī�޶� y���� ������ ���ϴ� ���� �ذ�;
    {
        cameraArm.transform.position = characterBody.transform.position;
    }
}
