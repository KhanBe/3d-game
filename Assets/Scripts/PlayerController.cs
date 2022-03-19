using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks, IPunObservable
{

    public float movementSpeed = 3;
    public float jumpForce = 300;
    public float timeBeforeNextJump = 1.2f;
    private float canJump = 0f;
    Animator anim;
    Rigidbody rb;

    float moveHorizontal;
    float moveVertical;

    Vector3 movement;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) //변수 동기화 함수
    {
        
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ControllPlayer();
    }

    void FixedUpdate()
    {
        FreezeRotation();
    }

    void FreezeRotation()
    {
        rb.angularVelocity = Vector3.zero;
    }

    void ControllPlayer()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        if (movement == Vector3.zero)
        {
            anim.SetInteger("Walk", 0);
        }
        else {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);// 몸체 회전
            anim.SetInteger("Walk", 1);
        }

        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);//포지션 바꿔주기

        if (Input.GetButtonDown("Jump") && Time.time > canJump)
        {
                rb.AddForce(0, jumpForce, 0);
                canJump = Time.time + timeBeforeNextJump;
                anim.SetTrigger("jump");
        }

    }
}