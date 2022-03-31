using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform objectfollow; // ���� ������Ʈ ����
    public float followSpeed = 10f; // ���󰡴� �ӵ�
    public float sensitivity = 500f; //����
    public float clampAngle = 70f; // ���� ����

    private float rotX; // ���콺 ����
    private float rotY;

    public Transform realCamera;
    public Vector3 dirNormalized; // ����
    public Vector3 finalDir; // ���� ���� ���Ͱ�
    public float minDistance; //�ִ�, �ּ� �Ÿ�
    public float maxDistance;
    public float finalDistance;
    public float smoothness = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        //normalized ���� ũ�� ���� 0���� ���ش�. �׷��� ���⸸ ��Ÿ��
        dirNormalized = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude; //magnitude = ũ��

        //���콺 Ŀ�� �����
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // deltaTime = ������ �����ӿ��� ���� �����ӱ����� ���� �ð�
        rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;
    }

    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, objectfollow.position, followSpeed * Time.deltaTime);

        //local space���� world space�� 
        finalDir = transform.TransformPoint(dirNormalized * maxDistance);

        RaycastHit hit;

        if (Physics.Linecast(transform.position, finalDir, out hit))
        {
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            finalDistance = maxDistance;
        }
        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNormalized * finalDistance, Time.deltaTime * smoothness);
    }
}
