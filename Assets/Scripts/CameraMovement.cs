using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform objectfollow; // 따라갈 오브젝트 정보
    public float followSpeed = 10f; // 따라가는 속도
    public float sensitivity = 500f; //감도
    public float clampAngle = 100f; // 제한 각도  

    private float rotX; // 마우스 정보
    private float rotY;

    public Transform realCamera;
    public Vector3 dirNormalized; // 방향
    public Vector3 finalDir; // 최종 방향 벡터값
    public float minDistance; //최대, 최소 거리
    public float maxDistance;
    public float finalDistance;
    public float smoothness = 10f;

    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        //normalized 벡터 크기 값을 0으로 해준다. 그래서 방향만 나타남
        dirNormalized = realCamera.localPosition.normalized;
        finalDistance = realCamera.localPosition.magnitude; //magnitude = 크기

        //마우스 커서 지우기
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // deltaTime = 마지막 프레임에서 현재 프레임까지의 시간 간격
        // X회전을 'Mouse Y'로 회전하는것은 x축 원통을 돌리니까 서로다르게 설정한다.
        rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        // 제한 각도 설정
        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);
        transform.rotation = rot;
    }

    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, objectfollow.position, followSpeed * Time.deltaTime);

        //local space에서 world space로 
        finalDir = transform.TransformPoint(dirNormalized * maxDistance);

        RaycastHit hit;

        //플레이어와 카메라 사이 다른 오브젝트가 있는지 Linecast로 확인
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
