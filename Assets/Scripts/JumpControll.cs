using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpControll : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            TPSCharacterController.isJumping = false;
        }
    }
}
