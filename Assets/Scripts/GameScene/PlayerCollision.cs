using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private int ID;
    private int typeID;
    private int imageID;
    private int posID;

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerData playerData = gameObject.GetComponent<PlayerData>();

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
            case 10 or 11: //Tree or Stone
                playerData.TreeStone(other);
                break;

            case 20: //Orb
                playerData.Orb(other);
                break;

            case 21: //Star
                playerData.Star(other, ID);
                break;

            case 40: //Goal
                playerData.Goal();
                break;

        }
    }
}
