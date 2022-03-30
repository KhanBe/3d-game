using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform objectfollow; // 따라갈 오브젝트 정보
    public float followSpeed = 10f; // 따라가는 속도
    public float sensitivity = 100f; //감도
    public float clampAngle = 70f; // 제한 각도

    private float rotX; // 마우스 정보
    private float rotY;

    public Transform realCamera;
    public Vector3 dirNormalized; // 방향
    public Vector3 finalDir; // 최종 방향 벡터값
    public float minDistance; //최대, 최소 거리
    public float maxDistance;
    public float finalDistance;

    // Start is called before the first frame update
    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        //normalized 벡터 크기 값을 0으로 해준다. 그래서 방향만 나타남
        dirNormalized = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude; //magnitude = 크기
    }

    // Update is called once per frame
    void Update()
    {
        // deltaTime = 마지막 프레임에서 현재 프레임까지의 간격 시간
        rotX += Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;
    }
}
