# 3d-moblie-game
This project is 3D game


### 3D 쿼터뷰 게임

<details>
  <summary>일지</summary>
  
#### 2022-03-10   
- 캐릭터 구현
- 캐릭터 움직임   
- 캐릭터 script (내용 작성)

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
  
#### 2022-03-19
- 플레이어 동기화 컴포넌트   
기본적으로 Photon View 컴포넌트가 있어야한다.
Photon View의 Observed Components에 Photon animator View, Photon rigidbody View, 플레이어 스크리트가 있어야된다.   
Photon animator View 추가 후, 파라미터 모두 Discrete 한다.   
Photon rigidbody View 추가
  
플레이어 스크립트가 MonoBehaviourPunCallbacks, IPunObservable 를 상속받는다.   
```public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)``` 이 함수 안에서 변수 동기화가 일어난다.
  
#### 2022-03-22
- 프리팹 내부오브젝트는 인스펙터에 가져다 쓸 수 있는데, 외부오브젝트는 쓸 수 없어서 none으로 초기화 된다.
캐릭터와 카메라가 같이 묶여 있어야 할 것 같다.   
  
#### 2022-03-23
- 캐릭터 이름을 text 대신 text mesh를 사용했는데 서버 접속시 이름 적용이 안된다.
  
#### 2022-03-30
- 캐릭터 화면을 고정이 아닌 마우스로 회전할 수 있게 변경하려 한다.
- CameraMovement Script 추가 (내용 작성하기)
  
#### 2022-03-31
- 마우스로 카메라 시점 변환 구현
- 카메라 시점 변경시 캐릭터는 항상 (wasd) 움직이는 방향이 같다. 이점을 바꿔주면 될것.
  
#### 2022-04-09
- (wasd) 움직임 방향 기준을 카메라로 잡긴 했으나 Charactor ctroller로 움직임을 구현시 중력 작용을 안한다.
- 다음번에 Charactor ctroller 대신 다른 방법으로 접근할 예정이다.
  
#### 2022-04-10
- 앞으로 갈 때는 정상적이고 옆,뒤 입력시 캐릭터가 엄청난 회전을 함.
  
#### 2022-06-06
- 카메라 회전 구현
  
#### 2022-06-08
- 플레이어 움직임 재구현
- 부모오브젝트의 rigidbody가 없어서 점프할 방법을 찾아야함. > 해결
  
#### 2022-08-01
- 캐릭터 및 카메라 움직임
- https://www.youtube.com/watch?v=P4qyRyQdySw
- 캐릭터가 오르막길을 올라가면 카메라와 플레이어의 거리값이 변하는 문제. > 해결
- 캐릭터 점프가 연속으로 잘되지 않는 문제. > 해결

#### 2022-08-02
- 캐릭터가 오르막길을 올라가면 카메라와 플레이어의 거리값이 변하는 문제 해결 : cameraArm의 position과 character의 position을 맞춰줌

#### 2022-08-04
- 캐릭터 점프구현방식 변경
- 캐릭터 점프가 연속으로 잘되지 않는 문제 해결 onCollisionEnter함수를 사용

  <details>
  <summary>해결방법</summary>
    
  - onCollision 함수가 자신의 오브젝트 기준으로 다른 tag오브젝트에 닿을 때 사용가능 함.
    
  - 내가 원하는 것은 player와 Ground가 닿을 때
    
  - 하지만 (character)오브젝트에 스크립트가 담겨있어서 player에 새 스크립트(JumpControll)을 넣어 onCollisionEnter함수 사용
    
  - static public 변수 사용으로 외부 스크립트 변수 수정
    </details>

- gravity 10 감소
- 점프 크기는 스크립트에서 바꿔도 Inspector에서 값을 담고있어서 바뀌지 않는다. (시간 소요..)
  </details>
