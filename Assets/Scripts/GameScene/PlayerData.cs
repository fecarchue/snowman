using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

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
    private float damageTimer = 0f;

    [HideInInspector] public int starCount = 0;

    private bool isGround, isDevil;
    private bool DashLL, DashLD, DashDD, DashRD, DashRR, DashRU, DashLU, Rush, Shrink;    
    public float dashDuration = 0.2f; // Dash 지속 시간 조절
    public float dashSpeed = 100f; // Dash 속도 조절
    public float rushDuration = 0.2f; // Rush 지속 시간 조절
    public float shrinkDuration = 0.2f; // Shrink 지속 시간 조절


    void Start()
    {
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

            if (playerData[1] <= 0) //게임오버
            {
                playerData[1] = 0;
                Fail();
            }

            if (playerData[0] <= playerData[1]) //체력 초과
                playerData[1] = playerData[0];

            damageTimer -= Time.deltaTime;
            if (damage >= playerData[0] - playerData[1]) damage = playerData[0] - playerData[1];
            if (damageTimer <= 0)
            {
                if (damage <= 0) damage = 0;
                else damage -= Time.deltaTime * 10f;
            }
            yield return null;
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
            damageTimer -= Time.deltaTime;
            if (damageTimer <= 0)
            {
                if (damage <= 0) damage = 0;
                else damage -= Time.deltaTime * 10f;
            }

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
        float[] data = other.gameObject.GetComponentInParent<ObjectData>().data;
        for (int i = 0; i < 4; i++) playerData[i] += data[i];
        damage -= data[1];
        damageTimer = 1f;
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


                                                                                                  
    private IEnumerator CheckMouseUp()          //여기서 다른 불값들을 보고 어떻동작을할지 확인(모든동작들 다 여ㅣㄱ서 실행시키도록때려넣을것!!!
    {
        while (true)
        {
            if (Input.GetMouseButtonUp(0)) // 터치가 끊어진 순간에만 Dash 실행
            {

                if (DashLU) StartCoroutine(MoveInDirectionForDuration(Vector2.left + Vector2.up, dashDuration));
                else if (DashLL) StartCoroutine(MoveInDirectionForDuration(Vector2.left, dashDuration));
                else if (DashLD) StartCoroutine(MoveInDirectionForDuration(Vector2.left + Vector2.down, dashDuration));
                else if (DashDD) StartCoroutine(MoveInDirectionForDuration(Vector2.down, dashDuration));
                else if (DashRD) StartCoroutine(MoveInDirectionForDuration(Vector2.right + Vector2.down, dashDuration));
                else if (DashRR) StartCoroutine(MoveInDirectionForDuration(Vector2.right, dashDuration));
                else if (DashRU) StartCoroutine(MoveInDirectionForDuration(Vector2.right + Vector2.up, dashDuration));
                else if (Rush) StartCoroutine(DoRush(rushDuration));
                else if (Shrink) StartCoroutine(DoShrink(shrinkDuration));

                // 모든 방향에 대한 Dash 사용 후 상태 초기화
                DashLU = DashLL = DashLD = DashDD = DashRD = DashRR = DashRU = Rush = Shrink = false;
            }
            yield return null;
        }
    }

    private IEnumerator DoRush(float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.Translate(Vector2.up * dashSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator DoShrink(float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.Translate(Vector2.down * dashSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }



    private IEnumerator MoveInDirectionForDuration(Vector2 direction, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.Translate(direction * dashSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void SetDashDirection(bool lu, bool ll, bool ld, bool dd, bool rd, bool rr, bool ru)
    {
        DashLU = lu; DashLL = ll; DashLD = ld; DashDD = dd; DashRD = rd; DashRR = rr; DashRU = ru;
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
