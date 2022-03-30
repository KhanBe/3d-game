using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform objectfollow; // ���� ������Ʈ ����
    public float followSpeed = 10f; // ���󰡴� �ӵ�
    public float sensitivity = 100f; //����
    public float clampAngle = 70f; // ���� ����

    private float rotX; // ���콺 ����
    private float rotY;

    public Transform realCamera;
    public Vector3 dirNormalized; // ����
    public Vector3 finalDir; // ���� ���� ���Ͱ�
    public float minDistance; //�ִ�, �ּ� �Ÿ�
    public float maxDistance;
    public float finalDistance;

    // Start is called before the first frame update
    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        //normalized ���� ũ�� ���� 0���� ���ش�. �׷��� ���⸸ ��Ÿ��
        dirNormalized = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude; //magnitude = ũ��
    }

    // Update is called once per frame
    void Update()
    {
        // deltaTime = ������ �����ӿ��� ���� �����ӱ����� ���� �ð�
        rotX += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;
    }
}
