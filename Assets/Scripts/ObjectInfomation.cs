using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInfomation : MonoBehaviour
{
    public GameObject InfoWindow;
    public GameObject stonePrefab, treePrefab;
    public void MakeObject(List<int> objects)
    {
        //있던 오브젝트들 제거
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        //오브젝트 추가
        foreach (int obj in objects)
        {
            //샘플 오브젝트 복사해서 생성
            if (obj / 10000 == 21)
            {
                Instantiate(stonePrefab, this.transform);
            }
            else if(obj == 654321)
            {
                Instantiate(treePrefab, this.transform);
            }
        }
    }
}
