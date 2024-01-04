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

    private int ID;
    private int typeID;
    private int imageID;
    private int posID;
    
    [HideInInspector] public int starCount = 0;
    
    // Player Shadow ��ü�� �������ְ�
    public GameObject playerShadow; 
    void Start()
    {
        objects = new List<int>();
        snowScale = 1f;
    }

    void Update()
    {
        if (playerData[2] >= sizeCut[playerSize]) playerSize++;

        targetScale = Mathf.Pow(playerData[2], radiusRate); //������ 3���� 1�� ������
        if (targetScale > snowScale) snowScale += Time.deltaTime * scaleRate;
        if (Mathf.Abs(targetScale - snowScale) < Time.deltaTime * scaleRate) snowScale = targetScale;
        transform.localScale = new Vector3(snowScale, snowScale, 1);  //������ ����

        for (int i = 0; i < 4; i++) playerData[i] += snowgroundData[i] * Time.deltaTime;



        //�÷��̾� �����쿡�� �����ؾߵ�
        if (playerShadow != null)
        {
            playerShadow.transform.localScale = new Vector3(snowScale, snowScale, 1);
        }





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

    }

    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            for (int i = 0; i < 4; i++) playerData[i] += (groundData[i] - snowgroundData[i]) * Time.deltaTime;
        }
        //Snowground�� �׻� ��� ����
        /*if (other.gameObject.tag == "Snowground")
        {
            for(int i = 0; i < 4; i++) playerData[i] += snowgroundData[i] * Time.deltaTime;
        }*/

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //ID�� 6����, ������ ������ ����:
        //�� 2�ڸ�: ������Ʈ Ÿ��(����, �� ��); 10���� ����
        //�߰� 2�ڸ�: �׷��� ���� (������); 00���� ����
        //�� 2�ڸ�: ���� �̹��� ���� ������ �ʿ��� ��; 00���� ����
        ID = other.gameObject.GetComponentInParent<ObjectID>().ID;
        typeID = ID / 10000;
        imageID = ID % 10000 / 100;
        posID = ID % 100;

        switch (typeID)
        {
            case 10 or 11: //stone or tree
                float[] data = other.gameObject.GetComponentInParent<ObjectData>().data;
                for (int i = 0; i < 4; i++) playerData[i] += data[i];
                damage -= data[1];
                damageTimer = 1f;
                if (playerData[1] > 0) Destroy(other.gameObject);
                break;

            case 20: //orb
                float power = other.gameObject.GetComponentInParent<ObjectData>().data[3];
                playerData[3] += power;
                Destroy(other.gameObject);
                break;

            case 21: //star
                starCount++;
                objects.Add(ID);
                Destroy(other.gameObject);
                break;

            case 40: //goal
                ui.finish();
                break;

        }
    }
}
