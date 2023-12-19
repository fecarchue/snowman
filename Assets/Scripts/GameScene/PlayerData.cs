using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    public UI ui;

    private List<int> objects; //����� ������Ʈ�� ID ����Ʈ

    //�÷��̾�� ��� �÷��̾��� ��ġ�� �����ϴ� ������Ʈ���� ������ ���� �����͸� ������:
    //[0]==MaxHealth, [1]==Health, [2]==Volume [3]==Power
    public float[] playerData = { 1, 1, 1, 1 };

    public float[] groundData = { 0, -4, 0, 0 };
    public float[] snowgroundData = { 2, 3, 1, 1 };

    public float[] stone0Data = { 0, -2, 2, 2 };
    public float[] stone1Data = { 0, -2, 2, 2 };
    public float[] stone2Data = { 0, -2, 2, 2 };
    public float[] stone3Data = { 0, -2, 2, 2 };
    public float[] stone4Data = { 0, -2, 2, 2 };
    public float[] stone5Data = { 0, -2, 2, 2 };
    public float[] stone6Data = { 0, -2, 2, 2 };
    public float[] stone7Data = { 0, -2, 2, 2 };
    public float[] tree0Data = { 0, -2, 3, 3 };
    public float[] tree1Data = { 0, -2, 3, 3 };
    public float[] tree2Data = { 0, -2, 3, 3 };
    public float[] tree3Data = { 0, -2, 3, 3 };
    public float[] tree4Data = { 0, -2, 3, 3 };
    public float[] tree5Data = { 0, -2, 3, 3 };
    public float[] tree6Data = { 0, -2, 3, 3 };
    public float[] tree7Data = { 0, -2, 3, 3 };
    public float[] tree8Data = { 0, -2, 3, 3 };
    public float[] tree9Data = { 0, -2, 3, 3 };
    public float[] tree10Data = { 0, -2, 3, 3 };
    public float[] tree11Data = { 0, -2, 3, 3 };
    public float[] tree12Data = { 0, -2, 3, 3 };

    public float scaleRate = 0.33333333333f;
    public float[] sizeCut = { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 9999 };
    [HideInInspector] public int playerSize = 0;
    [HideInInspector] public float snowScale; // �׷��� ��ȯ ����� ���� Scale
    
    private int ID;
    private int typeID;
    private int imageID;
    private int posID;

    private int starCount = 0;
    void Start()
    {
        objects = new List<int>();
    }

    void Update()
    {
        if (playerData[2] >= sizeCut[playerSize]) playerSize++;

        snowScale = Mathf.Pow(playerData[2], scaleRate); //������ 3���� 1�� ������
        transform.localScale = new Vector3(snowScale, snowScale, 1);  //������ ����

        if (playerData[1] <= 0) //���ӿ���
        {
            playerData[1] = 0;
            ui.fail();
        }
        if (playerData[0] <= playerData[1]) //ü�� �ʰ�
        {
            playerData[1] = playerData[0];
        }
    }

    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            for (int i = 0; i < 4; i++) playerData[i] += (groundData[i] - snowgroundData[i]) * Time.deltaTime;
        }
        //Snowground�� �׻� ��� ����
        if (other.gameObject.tag == "Snowground")
        {
            for(int i = 0; i < 4; i++) playerData[i] += snowgroundData[i] * Time.deltaTime;
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //ID�� 6����, ������ ������ ����:
        //�� 2�ڸ�: ������Ʈ Ÿ��(����, �� ��); 10���� ����
        //�߰� 2�ڸ�: �׷��� ���� (������); 00���� ����
        //�� 2�ڸ�: ���� �̹��� ���� ������ �ʿ��� ��; 00���� ����
        ID = other.gameObject.GetComponent<ObjectID>().ID;
        typeID = ID / 10000;
        imageID = ID % 10000 / 100;
        posID = ID % 100;

        switch (typeID)
        {
            case 10: //stone
                for (int i = 0; i < 4; i++)
                {
                    switch (imageID)
                    {
                        case 0: playerData[i] += stone0Data[i]; break;
                        case 1: playerData[i] += stone1Data[i]; break;
                        case 2: playerData[i] += stone2Data[i]; break;
                        case 3: playerData[i] += stone3Data[i]; break;
                        case 4: playerData[i] += stone4Data[i]; break;
                        case 5: playerData[i] += stone5Data[i]; break;
                        case 6: playerData[i] += stone6Data[i]; break;
                        case 7: playerData[i] += stone7Data[i]; break;
                    }
                }
                if (playerData[1] > 0) Destroy(other.gameObject);
                break;

            case 11: //tree
                for (int i = 0; i < 4; i++)
                    switch (imageID)
                    {
                        case 0: playerData[i] += tree0Data[i]; break;
                        case 1: playerData[i] += tree1Data[i]; break;
                        case 2: playerData[i] += tree2Data[i]; break;
                        case 3: playerData[i] += tree3Data[i]; break;
                        case 4: playerData[i] += tree4Data[i]; break;
                        case 5: playerData[i] += tree5Data[i]; break;
                        case 6: playerData[i] += tree6Data[i]; break;
                        case 7: playerData[i] += tree7Data[i]; break;
                        case 8: playerData[i] = tree8Data[i]; break;
                        case 9: playerData[i] += tree9Data[i]; break;
                        case 10: playerData[i] += tree10Data[i]; break;
                        case 11: playerData[i] += tree11Data[i]; break;
                        case 12: playerData[i] += tree12Data[i]; break;
                    }
                if (playerData[1] > 0) Destroy(other.gameObject);
                break;

            case 20: //orb
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
