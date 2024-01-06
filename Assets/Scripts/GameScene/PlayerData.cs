using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    public UI ui;

    private List<int> objects; //����� ������Ʈ�� ID ����Ʈ

    //�÷��̾�� ��� �÷��̾��� ��ġ�� �����ϴ� ������Ʈ���� ������ ���� �����͸� ������:
    //[0]==MaxHealth, [1]==Health, [2]==Volume [3]==Power
    public float[] playerData = { 10, 7, 1, 10 };

    public float[] groundData = { 0, -4, 0, 0 };
    public float[] snowgroundData = { 1, 2, 1, 1 };

    public float radiusRate = 0.33333333333f;
    public float scaleRate = 2f;
    public float[] sizeCut = { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 9999 };
    [HideInInspector] public int playerSize;
    public float snowScale; // �׷��� ��ȯ ����� ���� Scale
    public float targetScale;
    [HideInInspector] public float damage;
    private float damageTimer = 0f;

    [HideInInspector] public int starCount = 0;

    private bool isGround;
    
    void Start()
    {
        objects = new List<int>();
        snowScale = 1f;
        StartCoroutine(InGamePlayerData());
    }

    private IEnumerator InGamePlayerData()
    {
        while (true)
        {
            if (playerData[2] >= sizeCut[playerSize]) playerSize++;

            targetScale = Mathf.Pow(playerData[2], radiusRate);
            if (targetScale > snowScale) snowScale += Time.deltaTime * scaleRate;
            if (Mathf.Abs(targetScale - snowScale) < Time.deltaTime * scaleRate) snowScale = targetScale;
            transform.localScale = new Vector3(snowScale, snowScale, 1);  //������ ����

            for (int i = 0; i < 4; i++) playerData[i] += snowgroundData[i] * Time.deltaTime;

            isGround = GetComponentInChildren<PlayerTrigger>().isGround;
            if (isGround)
                for (int i = 0; i < 4; i++)
                    playerData[i] += (groundData[i] - snowgroundData[i]) * Time.deltaTime;

            if (playerData[1] <= 0) //���ӿ���
            {
                playerData[1] = 0;
                ui.fail();
            }
            if (playerData[0] <= playerData[1]) //ü�� �ʰ�
            {
                playerData[1] = playerData[0];
            }

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

    public void TreeStone(Collision2D other)
    {
        float[] data = other.gameObject.GetComponentInParent<ObjectData>().data;
        for (int i = 0; i < 4; i++) playerData[i] += data[i];
        damage -= data[1];
        damageTimer = 1f;
        if (playerData[1] > 0) Destroy(other.gameObject);
    }

    public void Orb(Collision2D other)
    {
        float power = other.gameObject.GetComponentInParent<ObjectData>().data[3];
        playerData[3] += power;
        Destroy(other.gameObject);
    }

    public void Star(Collision2D other, int ID)
    {
        starCount++;
        objects.Add(ID);
        Destroy(other.gameObject);
    }

    public void Goal()
    {
        ui.finish();
    }

}
