using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
{

    public float movementSpeed = 3;
    public float jumpForce = 300;
    public float timeBeforeNextJump = 1.2f;
    private float canJump = 0f;
    private Transform cameraTransform;

    Camera _camera;
    Animator anim;
    Rigidbody rb;

    public PhotonView pv;
    public Text NickNameText;
    Vector3 curPos;

    float moveHorizontal;
    float moveVertical;

    Vector3 movement;

    public bool toggleCameraRotation;
    public float smoothness = 10f;

    void Awake()
    {
        NickNameText.text = pv.IsMine ? PhotonNetwork.NickName : pv.Owner.NickName;
        NickNameText.color = pv.IsMine ? Color.green : Color.red;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) //변수 동기화 함수
    {
        
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        cameraTransform = GetComponent<Transform>();

        _camera = Camera.main;  //쓰고싶으면 mainCamera 태그 활성 해야됨 (default)
    }

    void Update()
    {
        // 둘러보기 활성화 코드
        toggleCameraRotation = Input.GetKey(KeyCode.LeftAlt) ? true : false;

        ControllPlayer();
    }

    void LateUpdate()
    {
        if (!toggleCameraRotation)
        {
            Vector3 playerRotate = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * smoothness);
        }
    }
    void FixedUpdate()
    {
        FreezeRotation();
    }

    void FreezeRotation()
    {
        rb.angularVelocity = Vector3.zero;
    }

    void ControllPlayer()// 플레이어 조종 함수
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //moveHorizontal = Input.GetAxisRaw("Horizontal"); // GetAxisRaw() axis 값을 정수로 받기 때문에 입력을 직관적으로 받아들일 수 있다
        //moveVertical = Input.GetAxisRaw("Vertical");

        //Vector3 forward = transform.TransformDirection(Vector3.forward);
        //Vector3 right = transform.TransformDirection(Vector3.right);

        //movement = new Vector3(moveHorizontal, 0, moveVertical).normalized; // normalized 방향값을 1로 보정
        //movement = forward * moveVertical + right * moveHorizontal;



        Vector3 lookForward = new Vector3(cameraTransform.forward.x, 0f, cameraTransform.forward.z).normalized;
        Vector3 lookRight = new Vector3(cameraTransform.right.x, 0f, cameraTransform.right.z).normalized;
        Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

        transform.position += moveDir * Time.deltaTime * 5f;





        if (moveDir == Vector3.zero)
        {
            anim.SetInteger("Walk", 0);
        }
        else {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);// 몸체 회전
            anim.SetInteger("Walk", 1);
        }

        //transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);//포지션 바꿔주기

        if (Input.GetButtonDown("Jump") && Time.time > canJump)
        {
                rb.AddForce(0, jumpForce, 0);
                canJump = Time.time + timeBeforeNextJump;
                anim.SetTrigger("jump");
        }

    }
}