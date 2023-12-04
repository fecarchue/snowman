using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private List<int> objects; //����� ������Ʈ�� ID ����Ʈ

    //�÷��̾�� ��� �÷��̾��� ��ġ�� �����ϴ� ������Ʈ���� ������ ���� �����͸� ������:
    //[0]==MaxHealth, [1]==Health, [2]==Volume, [3]==Weight 
    public float[] playerData = { 1, 1, 1, 1 };

    public float[] groundData = { 0, -4, 0, 0 };
    public float[] snowgroundData = { 2, 3, 1, 1 };

    public float[,] stoneData = { { 0, -2, 2, 3 }, { 0, -2, 2, 3 }, { 0, -2, 2, 3 }, { 0, -2, 2, 3 }, { 0, -2, 2, 3 }, { 0, -2, 2, 3 }, { 0, -2, 2, 3 }, { 0, -2, 2, 3 }, { 0, -2, 2, 3 }, { 0, -2, 2, 3 } };
    public float[,] treeData = { { 0, -2, 3, 2 }, { 0, -2, 3, 2 }, { 0, -2, 3, 2 }, { 0, -2, 3, 2 }, { 0, -2, 3, 2 }, { 0, -2, 3, 2 }, { 0, -2, 3, 2 }, { 0, -2, 3, 2 }, { 0, -2, 3, 2 }, { 0, -2, 3, 2 }, { 0, -2, 3, 2 }, { 0, -2, 3, 2 } };

    public float growthRate = 1f; // Ŀ���� ����
    public float cameraScale; // degul12 ���� ��ũ��; ī�޶�, Ʈ���Ͽ�
    public float snowScale; // �׷��� ��ȯ ����� ���� Scale

    public int playerSize = 0;
    public float[] sizeCut = { 12, 16, 20, 26, 36, 48, 64, 86, 116, 202, 9999};

    public UI ui;

    private int ID;
    private int typeID;
    private int imageID;
    private int posID;

    void Start()
    {
        objects = new List<int>();
    }

    void Update()
    {

        if (snowScale > sizeCut[playerSize + 1] / sizeCut[playerSize]) playerSize++;

        cameraScale = growthRate * Mathf.Pow(playerData[2], 0.333333f); //������ 3���� 1�� ������
        snowScale = cameraScale * 12 / sizeCut[playerSize]; //12���� pixel��

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
        //�� 2�ڸ�: ��ġ; 00���� ����
        ID = other.gameObject.GetComponent<ObjectID>().ID;
        typeID = ID / 10000;
        imageID = ID % 10000 / 100;
        posID = ID % 100;

        if (typeID == 10) //Stone
        {
            for (int i = 0; i < 4; i++) playerData[i] += stoneData[imageID, i];
            objects.Add(ID);
            Destroy(other.gameObject);
        }
        if (typeID == 11) //Tree
        {
            for (int i = 0; i < 4; i++) playerData[i] += treeData[imageID, i];
            objects.Add(ID);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Goal")
        {
            ui.finish();
        }
    }
}
