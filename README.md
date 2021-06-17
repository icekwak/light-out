# LightsOut Gmae
라이트 아웃 퍼즐: 하나의 블록을 클릭하면 인접한 다른 블록들이 켜지거나 꺼지는 게임

![LightsOutIllustration svg](https://user-images.githubusercontent.com/77434165/104590965-647b4d80-56af-11eb-92ee-46f949bfb0b8.png) <br />
https://en.wikipedia.org/wiki/Lights_Out_(game)

Game Engine, Language
-------------
Unity, C#

Game Video
-------------


Game Concept
--------------
1. 퍼즐 게임을 3D로 구현
2. 우주 컨셉으로 제작
3. 장애물 등장
4. 난이도 별 스테이지

Player Implement
--------------
<h3>생성</h3>
<pre>
<code>
Input.GetMouseButtonDown(0)
->  마우스 왼쪽 클릭하면 플레이어 생성
ScreenPointToRay(Input.mousePosition)
->  스크린 마우스 좌표 값을 얻어 월드 좌표로 변환 후 사용
Physic.Raycast(), Instantiate()
-> 충돌 감지한 블록 위에 플레이어 생성
SceneManager.LoadScene()
-> 플레이어가 떨어지거나 장애물에 걸리면  재시작
player_check 
-> 플레이어 중복 생성을 막기 위한 변수
</code>
</pre>

<h3>이동</h3>
<pre>
<code>
Input.GetKey(KeyCode. ), transform.Translate()
-> 방향키 이동
Input.GetAxisRaw() 
-> -1, 0, 1 값을 통해 대각선 방향 설정
transform.rotation = Quaternion.Euler()
-> 플레이어를 회전하여 앞 방향 설정
</code>
</pre>

<h3>점프</h3>
<pre>
<code>
Input.GetKeyDown(KeyCode.Space)
-> 스페이스바 점프
GetComponent <Rigidbody>().velocity()
-> 점프 높이 설정
Jump_count 
-> 더블 점프를 구현하기 위해 점프 횟수 제한 변수
</code>
</pre>

<h3></h3>
<pre>
<code>
gameObject.tag
-> 태그를 통해 충돌한 물체 확인
GameObject.Find()
-> 블록 스크립트에 접근하기 위해
OnCollisionEnter()
-> 발판 충돌 감지
OnTriggerEnter()
-> 장애물끼리 충돌 무시 (단, 플레이어와 충돌 시 재시작)
</code>
</pre>

<h3>애니메이션, 사운드</h3>
<pre>
<code>
GetComponent<Animator>().Play()
-> 플레이어 애니메이터 재생
GetComponent<AudioSoruce>().Play()
-> 블록과 충돌하면 블록 사운드 재생
</code>
</pre>

Block Implement
-------------
<h3>생성</h3>
<pre>
<code>
2차원 배열 [ , ], Instantiate()
-> 블록의 상태를 변경하기 위해 생성하면서 배열에 저장
GetComponent<Renderer>().material.SetColor()
-> “_EmissionColor”를 사용하여 조명 ON/OFF 표현
Random.Range()
-> 블록마다  조명 ON/OFF 값을 0과 1로 랜덤하게 지정
Destroy()
-> 만약 조명이 모두 OFF 상태로 생성되면 삭제 후 재생성
</code>
</pre>

<h3>인접한 블록 접근, 애니메이션</h3>
<pre>
<code>
 GetComponent<Animation>().Play()
-> 블록이 눌리는 표현을 애니메이션을 제작해서 구현
Physics.Raycast()
-> 플레이어와 충돌한 블록의 좌표에서 Ray를 쏜다
RaycastHit
-> 쏜 Ray에 맞은 인접한 블록 정보 저장
(int)(Mathf.Abs())
-> 블록 배열을 인덱스로 접근하기 위해 => |좌표 값|
</code>
</pre>

Layser Implement
-------------
<h3>생성</h3>
<pre>
<code>
transform.localScale()
-> Scale 값을 통해 레이저 가로, 세로 길이 지정
Vector3.forward, back, left, right
-> 레이저가 이동 할 방향 지정
Random.Range(), Instantiate()
-> 레이저 생성 위치를 랜덤 값으로 지정
Time.deltaTime
-> 10초마다 레이저 생성
Destroy()
-> 다음 레이저 생성될 때 빗나간 레이저는 삭제
GetComponent<AudioSource>().Play()
-> 생성될 때만 사운드 재생
</code>
</pre>

Boob Implement
------------
<h3>생성, 충돌</h3>
<pre>
<code>
2차원 배열 [ , ], Random.Range()
-> 배열은 블록 위치를 나타내고 그 위치에 폭탄을 랜덤 생성
Instantiate()
-> 폭탄, 파티클 생성
Physics.Raycast(), GetColor, SetColor, static 변수
-> 폭탄이 생성된 블록 아래 위치에서 위로 Ray를 쏜다
GetComponent<Renderer>().GetColor(), SetColor()
-> Ray와 충돌한 블록은 빨간색으로 변하고 밟을 수 없는 상태
OnTriggerEnter(), Destroy()
-> 폭탄이 블록과 충돌 시 폭탄 삭제
AudioSource.PlayClipAtPoint()
-> 블록과 충돌하는 위치에 폭탄 터지는 사운드 재생
</code>
</pre>

Panel Implement
----------
<h3>생성</h3>
<pre>
<code>
Instantiate()
-> 패널 생성
GetChild(), SetParent()
-> 자식 접근 (패널.텍스트)
SetParent()
-> 부모 설정 (Canvas.패널)
GetComponent<Text>().text =
-> 패널 텍스트 설정 (스테이지 명)
GetComponent<Text>().color =
-> 패널 투명도 설정
GetComponent<AudioSource>.Play()
-> 패널 사운드
</code>
</pre>

Background Implement
---------------
<h3>배경</h3>
<pre>
<code>
GetComponent<AudioSource>().Play()
-> 배경 음악
Camera Object
-> 카메라 두 대 사용하여 물체 카메라, 백그라운드 카메라 나눔
transform.Rotate()
-> 백그라운드 카메라 회전
DontDestroyOnLoad()
-> 배경음악과 배경화면 오브젝트 유지
Destroy()
-> 마지막 씬 진입 시 배경 관련 오브젝트 삭제 (중복 방지)
</code>
</pre>

Scene Implement
-------------
<h3>씬</h3>
<pre>
<code>
static 변수
-> 여러 곳에서 쓰일 씬 인덱스 변수를 stati으로 사용
SceneManager.LoadScene(), OnClick()
-> 씬 전환, 버튼 클릭
</code>
</pre>

개발일지
-------------
1. 인접한 블록 접근 방법 Linecast나 Raycast를 쓰면 끝나는 쉬운 문제였지만,<br />
처음 시작할 때는 떠오르지 않아서 몇 일을 고민을 했습니다.<br />
처음으로 마주한 쉬우면서 어려웠던 첫 번째 어려웠던 점이었습니다. <br />
2. 블록 눌리는 표현
Update문에서 Scale값에 접근하는 방식은 매 프레임마다 실행되다 보니 손이 많이 갔습니다.<br />
그래서 블록에 애니메이션을 적용해 원할 때 만 사용하도록 구현했습니다. <br />
3. 대각선 이동 If(Input.GetKey() && Input.GetKey()) { transform.Translate( new Vector3(,,,) * speed * Time.deltaTime) } 이런 식으로 다양한 시도를 해봤지만,<br />
대각선 속도가 뭔가 배로 늘어난 것처럼 빨라 Vector값을 줄여 봤는데도 부자연스러웠습니다.<br />
계속 고민하다가 수업시간에 배운 Input.GetAxis()는 float값으로 쓸 수 있다는 것이 생각났습니다.<br />
이 후에 구글링을 통해 Input.GetAxisRaw()는 -1, 0, 1값으로만 나온다고 해서 이 것을 사용하여 회전 방향만 대각선으로 잡아주는 식으로 구현했습니다. <br />
4. 폭탄 떨어지는 위치 표시 처음에는 폭탄 낙하 위치에서 블록까지 Line을 그렸는데 레이저랑 헷갈리고 뭔가 난잡했습니다.<br />
다른 방식으로 블록에 폭탄 떨어지는 곳을 색을 나타내는 식으로 구현했습니다.<br />
그런데 생긴 문제가 색이 바뀌고 블록을 밟으면 색과 블록의 애니메이션이 달라지는 경우가 생겨<br />
한참을 고민하다가 폭탄이 생기면 폭탄이 사라질 때까지 블록을 못 밟는 방식으로 구현했습니다.

애셋 출처(애셋스토어)
--------------
플레이어 – Stylized Astronaut <br />
블록 – Low Poly Space Rocks <br />
배경화면 – Space Star Field Backgrounds <br />
버튼 – Clean Vector Icons <br />
폭탄 – Rockets, Missiles & Bombs - Cartoon Low Poly Pack <br />
배경 사운드 – Deep In Space <br />
패널 사운드 – Free Casual SoundFx Pack <br />
장애물, 블록 사운드 – Sound FX – Retro Pack <br />
폭탄 파티클 – 48 파티클 이펙트 패키지
