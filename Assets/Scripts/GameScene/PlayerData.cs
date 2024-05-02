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

    private List<int> objects; //����� ������Ʈ�� ID ����Ʈ

    //�÷��̾�� ��� �÷��̾��� ��ġ�� �����ϴ� ������Ʈ���� ������ ���� �����͸� ������:
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
    [HideInInspector] public float snowScale; // �׷��� ��ȯ ����� ���� Scale
    private float targetScale;
    [HideInInspector] public float damage;

    [HideInInspector] public int starCount = 0;

    private bool isGround, isDevil;
    private bool DashLL, DashLD, DashDD, DashRD, DashRR, DashRU, DashLU, Rush, Shrink;

    public bool isDashing = false;                         
    public float dashDuration = 0.2f; // Dash ���� �ð� ����
    public float dashSpeed = 100f; // Dash �ӵ� ����

    public bool isRushing = false; //�̵��߿� �ٸ� ������Ʈ�� �Ծ����� ����
    public float rushDuration = 10f; // Rush ���� �ð� ����
    public bool crush = false;
    public float rushDamage = 0.02f;

    public bool isShrinked = false;
    [HideInInspector] public float shrinkedScale;
    public float shrinkDuration = 3f; // Shrink ���� �ð� ����


    void Start()
    {
        damage = 0;
        objects = new List<int>();
        initialV = playerData[2];
        snowScale = 1f;
        StartCoroutine(InGamePlayerData());
        StartCoroutine(CheckMouseUp());                 //���콺�ô�������-> ������Ʈ����
    }

    private IEnumerator InGamePlayerData()
    {
        while (true)
        {
            if (playerData[2] >= nextSize[playerSize]) playerSize++;

            targetScale = 1f + scaleCoefficient * Mathf.Pow(playerData[2] - initialV, scaleExponent);
            if (targetScale > snowScale) snowScale += Time.deltaTime * scaleEnlargeSpeed;
            if (Mathf.Abs(targetScale - snowScale) < Time.deltaTime * scaleEnlargeSpeed) snowScale = targetScale;
            transform.localScale = new Vector3(snowScale, snowScale, 1);  //������ ����

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
        if (playerData[1] <= 0) //���ӿ���
        {
            playerData[1] = 0;
            Fail();
        }

        if (playerData[0] <= playerData[1]) //ü�� �ʰ�
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
        if(isDashing) return; //Dash�߿��� Ʈ��,�� �����ϰ�������
        if (isRushing) {
            
            Destroy(other.gameObject);
            particle.SpawnFogDuringRush();                                      //rush�߿� �ؿ� ���� ����� ��. �ڵ�� particle������
            return; } //Rush�߿��� Ʈ��,�� ��Ʈ���� ������

        float[] data = other.gameObject.GetComponentInParent<ObjectData>().data;
        for (int i = 0; i < 4; i++) playerData[i] += data[i];
        ui.takeDamage(playerData[1], -data[1]); // <-- �߰��� �Լ�
        Destroy(other.gameObject);
    }

    public void Orb(Collision2D other)
    {
        float power = other.gameObject.GetComponentInParent<ObjectData>().data[3];
        playerData[3] += power;
        Destroy(other.gameObject);
    }

    public void EnableDashLU()                                                                                               //���⼭ �ٸ� ���갪�� true����Ȯ���ϰ� ������ �װ�false�ϰ��ݸ�����true�ǵ���!!!
    {DashLU = true; DashLL = false; DashLD = false; DashDD = false; DashRD = false; DashRR = false; DashRU = false; Rush = false; Shrink = false;}//�� �������� �ƹ�ư �������� false�ϰ� ��ݸ����� true�Ǵ°� �����ϰԱ�����..
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


                                                                                                  
    private IEnumerator CheckMouseUp()          //���⼭ �ٸ� �Ұ����� ���� ����������� Ȯ��
    {
        while (true)
        {
            if (Input.GetMouseButtonUp(0)) // ��ġ�� ������ �������� Dash ����
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

                // ��� ���⿡ ���� Dash ��� �� ���� �ʱ�ȭ
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
        // Rush ���� �߿� �Է� ����
        isRushing = true;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            if (Input.GetMouseButtonDown(0))                    //���� ��ġ�����ؼ� ��ġ�� ����
            {
                Debug.Log("Touch detected -> rush over");
                break;
            }
            //�������°� playerMove���� �ӵ������γ���

            //�̰� rush�� ü�� ���������� ��
            playerData[1] -= rushDamage;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Rush ������ ������ �Է� �ޱ�
        isRushing = false;
    }



    private IEnumerator DoShrink(float duration)
    {
        isShrinked = true;
        float initialScale = transform.localScale.x; // �ʱ� ������ ����
        float targetShrinkScale = initialScale * 0.5f; // ����� ��ǥ ������ (���� ũ���� ����)

        // ���
        float elapsedTime = 0f;
        while (elapsedTime < 2f)    //2�ʰ���Ұ���
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
        while (elapsedTime < 1f)  // �ٽ� ���� ũ��� ���ư����� 1�ʰ� Ȯ��
        {
            float scale = Mathf.Lerp(targetShrinkScale, initialScale+1, elapsedTime / 1f);  //�׻��� ���� Ŀ���ϱ� 1������
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
