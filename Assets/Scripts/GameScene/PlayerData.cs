using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerData : MonoBehaviour
{

    public UI ui;
    public MoveCamera moveCamera;
    public PlayerMove playerMove;
    public PlayerAnimation playerAnimation;
    public Particle particle;

    private List<int> objects; //흡수한 오브젝트들 ID 리스트

    //플레이어와 모든 플레이어의 수치를 변경하는 오브젝트들은 다음과 같은 데이터를 가진다:
    //[0]==MaxHealth, [1]==Health, [2]==Volume [3]==Power
    public float[] playerData = { 10, 7, 1, 10 };

    public float[] snowgroundData = { 1, 2, 1, 1 };
    public float[] groundData = { 0, -4, 0, 0 };
    public float[] devilData = { -4, 0, 0, 0 };

    private float initialV;
    public float scaleCoefficient = 0.2f;
    public float scaleExponent = 1f;
    public float scaleEnlargeSpeed = 2f;
    public float[] nextSize = { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 9999 };
    [HideInInspector] public int playerSize;
    [HideInInspector] public float snowScale; // 그래픽 변환 고려한 실제 Scale
    private float targetScale;
    [HideInInspector] public float damage;

    [HideInInspector] public int starCount = 0;

    private bool isGround, isDevil;
    private bool DashLL, DashLD, DashDD, DashRD, DashRR, DashRU, DashLU, Rush, Shrink;

    public bool isDashing = false;                         
    public float dashDuration = 0.2f; // Dash 지속 시간 조절
    public float dashSpeed = 100f; // Dash 속도 조절

    public bool isRushing = false; //이동중에 다른 오브젝트를 먹었을때 구분
    public float rushDuration = 10f; // Rush 지속 시간 조절
    public bool crush = false;
    public float rushDamage = 0.02f;

    public bool isShrinked = false;
    [HideInInspector] public float shrinkedScale;
    public float shrinkDuration = 3f; // Shrink 지속 시간 조절


    void Start()
    {
        damage = 0;
        objects = new List<int>();
        initialV = playerData[2];
        snowScale = 1f;
        StartCoroutine(InGamePlayerData());
        StartCoroutine(CheckMouseUp());                 //마우스뗐는지감지-> 오브사용트리거
    }

    private IEnumerator InGamePlayerData()
    {
        while (true)
        {
            if (playerData[2] >= nextSize[playerSize]) playerSize++;

            targetScale = 1f + scaleCoefficient * Mathf.Pow(playerData[2] - initialV, scaleExponent);
            if (targetScale > snowScale) snowScale += Time.deltaTime * scaleEnlargeSpeed;
            if (Mathf.Abs(targetScale - snowScale) < Time.deltaTime * scaleEnlargeSpeed) snowScale = targetScale;
            transform.localScale = new Vector3(snowScale, snowScale, 1);  //스케일 적용

            for (int i = 0; i < 4; i++) playerData[i] += snowgroundData[i] * Time.deltaTime;

            isGround = GetComponentInChildren<PlayerTrigger>().isGround;
            if (isGround)
                for (int i = 0; i < 4; i++)
                    playerData[i] += (groundData[i] - snowgroundData[i]) * Time.deltaTime;

            isDevil = GetComponentInChildren<PlayerTrigger>().isDevil;
            if (isDevil)
                for (int i = 0; i < 4; i++)
                    playerData[i] += (devilData[i] - snowgroundData[i]) * Time.deltaTime;
            yield return null;
        }
    }

    public void Update()
    {
        if (playerData[1] <= 0) //게임오버
        {
            playerData[1] = 0;
            Fail();
        }

        if (playerData[0] <= playerData[1]) //체력 초과
            playerData[1] = playerData[0];

        if (damage > 0)
        {
            playerData[1] -= damage;
            damage = 0;
        }
    }
    private IEnumerator StopPlayerData(bool isGoal)
    {
        GetComponent<SpriteRenderer>().sortingOrder = 8;
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        int count = 0;
        bool countDone = false;
        string prevSprite = "temp";
        string nowSprite;

        while(true)
        {

            if (count <= 1 && isGoal)
            {
                nowSprite = gameObject.GetComponent<SpriteRenderer>().sprite.name;

                if (nowSprite != prevSprite && nowSprite.EndsWith("_0")) count++;
                prevSprite = nowSprite;
            }

            if (!countDone && count == 2)
            {
                countDone = true;
                particle.Stop();
                playerAnimation.Goal();
                playerMove.Stop();
                ui.Goal();
            }

            yield return null;
        }
    }

    public void TreeStone(Collision2D other)
    {
        if(isDashing) return; //Dash중에는 트리,돌 무시하고지나감
        if (isRushing) {
            
            Destroy(other.gameObject);
            particle.SpawnFogDuringRush();                                      //rush중에 밑에 먼지 생기게 함. 코드는 particle에있음
            return; } //Rush중에는 트리,돌 터트리고 지나감

        float[] data = other.gameObject.GetComponentInParent<ObjectData>().data;
        for (int i = 0; i < 4; i++) playerData[i] += data[i];
        ui.takeDamage(playerData[1], -data[1]); // <-- 추가한 함수
        Destroy(other.gameObject);
    }

    public void Orb(Collision2D other)
    {
        float power = other.gameObject.GetComponentInParent<ObjectData>().data[3];
        playerData[3] += power;
        Destroy(other.gameObject);
    }

    public void EnableDashLU()                                                                                               //여기서 다른 오브값이 true인지확인하고 있으면 그거false하고방금먹은거true되도록!!!
    {DashLU = true; DashLL = false; DashLD = false; DashDD = false; DashRD = false; DashRR = false; DashRU = false; Rush = false; Shrink = false;}//좀 더럽지만 아무튼 기존오브 false하고 방금먹은거 true되는거 간단하게구현함..
    public void EnableDashLL()                                                                                                                   
    {DashLU = false; DashLL = true; DashLD = false; DashDD = false; DashRD = false; DashRR = false; DashRU = false; Rush = false; Shrink = false;}
    public void EnableDashLD()
    {DashLU = false; DashLL = false; DashLD = true; DashDD = false; DashRD = false; DashRR = false; DashRU = false; Rush = false; Shrink = false;}
    public void EnableDashDD()
    {DashLU = false; DashLL = false; DashLD = false; DashDD = true; DashRD = false; DashRR = false; DashRU = false; Rush = false; Shrink = false;}
    public void EnableDashRD()
    {DashLU = false; DashLL = false; DashLD = false; DashDD = false; DashRD = true; DashRR = false; DashRU = false; Rush = false; Shrink = false;}
    public void EnableDashRR()
    {DashLU = false; DashLL = false; DashLD = false; DashDD = false; DashRD = false; DashRR = true; DashRU = false; Rush = false; Shrink = false;}
    public void EnableDashRU()
    {DashLU = false; DashLL = false; DashLD = false; DashDD = false; DashRD = false; DashRR = false; DashRU = true; Rush = false; Shrink = false;}

    public void EnableRush()
    {DashLU = false; DashLL = false; DashLD = false; DashDD = false; DashRD = false; DashRR = false; DashRU = false; Rush = true; Shrink = false;}
    public void EnableShrink()
    {DashLU = false; DashLL = false; DashLD = false; DashDD = false; DashRD = false; DashRR = false; DashRU = false; Rush = false; Shrink = true;}


                                                                                                  
    private IEnumerator CheckMouseUp()          //여기서 다른 불값들을 보고 어떻동작을할지 확인
    {
        while (true)
        {
            if (Input.GetMouseButtonUp(0)) // 터치가 끊어진 순간에만 Dash 실행
            {

                if (DashLU)  StartCoroutine(DoDash(Vector2.left + Vector2.up, dashDuration));
                else if (DashLL)  StartCoroutine(DoDash(Vector2.left, dashDuration));  
                else if (DashLD)  StartCoroutine(DoDash(Vector2.left + Vector2.down, dashDuration)); 
                else if (DashDD)  StartCoroutine(DoDash(Vector2.down, dashDuration)); 
                else if (DashRD)  StartCoroutine(DoDash(Vector2.right + Vector2.down, dashDuration));
                else if (DashRR)  StartCoroutine(DoDash(Vector2.right, dashDuration)); 
                else if (DashRU)  StartCoroutine(DoDash(Vector2.right + Vector2.up, dashDuration)); 
                else if (Rush) StartCoroutine(DoRush(rushDuration));
                else if (Shrink) StartCoroutine(DoShrink(shrinkDuration));

                // 모든 방향에 대한 Dash 사용 후 상태 초기화
                DashLU = DashLL = DashLD = DashDD = DashRD = DashRR = DashRU = Rush = Shrink =  false;
            }
            yield return null;
        }
    }    
    private IEnumerator DoDash(Vector2 direction, float duration)
    {
        isDashing = true;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.Translate(direction * dashSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isDashing = false;
    }

    private IEnumerator DoRush(float duration)
    {
        // Rush 동작 중에 입력 무시
        isRushing = true;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            if (Input.GetMouseButtonDown(0))                    //도중 터치감지해서 터치시 종료
            {
                Debug.Log("Touch detected -> rush over");
                break;
            }
            //빨라지는건 playerMove에서 속도증가로넣음

            //이건 rush시 체력 서서히감소 임
            playerData[1] -= rushDamage;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Rush 동작이 끝나면 입력 받기
        isRushing = false;
    }



    private IEnumerator DoShrink(float duration)
    {
        isShrinked = true;
        float initialScale = transform.localScale.x; // 초기 스케일 저장
        float targetShrinkScale = initialScale * 0.5f; // 축소할 목표 스케일 (현재 크기의 절반)

        // 축소
        float elapsedTime = 0f;
        while (elapsedTime < 2f)    //2초간축소과정
        {
            float scale = Mathf.Lerp(initialScale, targetShrinkScale, elapsedTime / 2f);
            shrinkedScale = scale;
            transform.localScale = new Vector3(scale, scale, 1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.localScale = new Vector3(targetShrinkScale, targetShrinkScale, 1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0f;
        while (elapsedTime < 1f)  // 다시 원래 크기로 돌아가도록 1초간 확대
        {
            float scale = Mathf.Lerp(targetShrinkScale, initialScale+1, elapsedTime / 1f);  //그사이 조금 커지니까 1더해줌
            shrinkedScale = scale;
            transform.localScale = new Vector3(scale, scale, 1f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isShrinked = false;
    }


    public void Star(Collision2D other, int ID)
    {
        starCount++;
        objects.Add(ID);
        Destroy(other.gameObject);
    }

    public void Goal()
    {
        StopAllCoroutines();
        StartCoroutine(StopPlayerData(true));
     
        playerMove.Goal();
        moveCamera.Goal();
        playerAnimation.SubGoal();
    }

    public void Fail()
    {
        StopAllCoroutines();
        StartCoroutine(StopPlayerData(false));

        particle.Stop();
        playerMove.Stop();
        moveCamera.Fail();
        playerAnimation.Fail();
        ui.Fail();
    }


}
