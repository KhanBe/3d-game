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

    void Start()
    {
        anim = characterBody.GetComponent<Animator>();
    }

    void Update()
    {
        LookAround();
    }
    void LookAround()//���콺�� ī�޶� ȸ���ϴ� �Լ�
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;

        cameraArm.rotation = Quaternion.Euler(camAngle.x - mouseDelta.y, camAngle.y + mouseDelta.x, camAngle.z);
    }
}
