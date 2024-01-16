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
    private float damageTimer = 0f;

    [HideInInspector] public int starCount = 0;

    private bool isGround, isDevil;
    private bool canDash = false;

    void Start()
    {
        objects = new List<int>();
        initialV = playerData[2];
        snowScale = 1f;
        StartCoroutine(InGamePlayerData());
        StartCoroutine(CheckMouseUpForDash());
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

            if (playerData[1] <= 0) //���ӿ���
            {
                playerData[1] = 0;
                Fail();
            }

            if (playerData[0] <= playerData[1]) //ü�� �ʰ�
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

    public void EnableDash()
    {
        canDash = true;
        Debug.Log("Dash on"); // Dash ���� ���� �޽��� ���
    }
    private IEnumerator CheckMouseUpForDash()
    {
        while (true)
        {
            if (canDash && Input.GetMouseButtonUp(0)) // ��ġ�� ������ �������� Dash ����
            {
                Debug.Log("Dash!"); // Dash �޽��� ���

                // ���⿡ ���ϴ� Dash ������ �߰�
                // ���� ���, �̵� �ӵ� ���� ���� ȿ���� ������ �� ����

                // �������� 1�ʰ� �̵�
                StartCoroutine(MoveLeftForDuration(1f));

                canDash = false; // Dash ��� �� ���� �ʱ�ȭ
            }

            yield return null;
        }
    }
    private IEnumerator MoveLeftForDuration(float duration)
    {
        float elapsedTime = 0f;
        float dashSpeed = 10f; // Dash �ӵ� ����

        while (elapsedTime < duration)
        {
            transform.Translate(Vector2.left * dashSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
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
