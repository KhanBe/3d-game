# 3d-moblie-game
This project is 3D moblie game


### 3D 쿼터뷰 모바일 게임

<details>
  <summary>일지</summary>
  
#### 2022-03-10   
- 캐릭터 구현
- 캐릭터 움직임   

#### 2022-03-11   
- 맵
- 카메라 구도
- 캡슐 콜라이더 적용 후 경사진 곳을 가면 캐릭터가 계속 구르게 됨 (오류)   

#### 2022-03-13   
- 경사진 곳을 가면 캐릭터가 계쏙 구르는 오류
- 원인 : 외부 충돌에 의해 **회전속력**이 발생
- 해결 : FixedUpdate에 회전속력 값을 zero로 만들어준다.
```rb.angularVelocity = Vector3.zero;```

- 다시 박스콜라이더로 교체   

#### 2022-03-16
- UI panel 연습, 구현
  
#### 2022-03-18
- UI 서버 접속 버튼 구현
- Photon 서버 스크립트 작성 (Connect, Disconnect, Join, Create)
- 3D 캐릭터 위에 닉네임 표시 구현
  https://itadventure.tistory.com/401?category=862463 참조
  
#### 2022-13-19
- 플레이어 동기화 컴포넌트   
기본적으로 Photon View 컴포넌트가 있어야한다.
Photon View의 Observed Components에 Photon animator View, Photon rigidbody View, 플레이어 스크리트가 있어야된다.   
Photon animator View 추가 후, 파라미터 모두 Discrete 한다.   
Photon rigidbody View 추가
  
플레이어 스크립트가 MonoBehaviourPunCallbacks, IPunObservable 를 상속받는다.   
```public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)``` 이 함수 안에서 변수 동기화가 일어난다.
  
  </details>
