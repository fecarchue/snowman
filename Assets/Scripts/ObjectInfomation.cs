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
        //�ִ� ������Ʈ�� ����
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        //������Ʈ �߰�
        foreach (int obj in objects)
        {
            //���� ������Ʈ �����ؼ� ����
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
