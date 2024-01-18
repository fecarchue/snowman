using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private int ID;
    private int typeID;
    private int imageID;
    private int posID;

    private void OnCollisionEnter2D(Collision2D collider)
    {
        PlayerData playerData = gameObject.GetComponentInParent<PlayerData>();

        //ID�� 6����, ������ ������ ����:
        //�� 2�ڸ�: ������Ʈ Ÿ��(����, �� ��); 10���� ����
        //�߰� 2�ڸ�: �׷��� ���� (������); 00���� ����
        //�� 2�ڸ�: ���� �̹��� ���� ������ �ʿ��� ��; 00���� ����
        ID = collider.gameObject.GetComponentInParent<ObjectID>().ID;
        typeID = ID / 10000;
        imageID = ID % 10000 / 100;
        posID = ID % 100;

        switch (typeID)
        {
            case 10 or 11: //Tree or Stone
                playerData.TreeStone(collider);
                break;

            case 20: //Orb
                playerData.Orb(collider);
                break;

            case 23: // LEFTUP dashOrb
                playerData.EnableDashLU();
                Destroy(collider.gameObject);
                break;
            case 24: // LEFT dashOrb                        
                playerData.EnableDashLL();
                Destroy(collider.gameObject);
                break;
            case 25: // LEFTDOWN dashOrb
                playerData.EnableDashLD();
                Destroy(collider.gameObject);
                break;
            case 26: // DOWN dashOrb
                playerData.EnableDashDD();
                Destroy(collider.gameObject);
                break;
            case 27: // RIGHTDOWN dashOrb
                playerData.EnableDashRD();
                Destroy(collider.gameObject);
                break;
            case 28: // RIGHT dashOrb
                playerData.EnableDashRR();
                Destroy(collider.gameObject);
                break;
            case 29: // RIGHTUP dashOrb
                playerData.EnableDashRU();
                Destroy(collider.gameObject);
                break;
//������� dashOrb

            case 30: // RushOrb
                playerData.EnableRush();
                Destroy(collider.gameObject);
                break;

            case 40: // ShrinkOrb
                playerData.EnableShrink();
                Destroy(collider.gameObject);
                break;

            case 50: //Star
                playerData.Star(collider, ID);
                break;

        }
    }
}
