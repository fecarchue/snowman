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

        //ID는 6글자, 구조는 다음과 같다:
        //앞 2자리: 오브젝트 타입(나무, 돌 등); 10부터 시작
        //중간 2자리: 그래픽 종류 (사이즈); 00부터 시작
        //뒤 2자리: 같은 이미지 습득 저장이 필요할 때; 00부터 시작
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
