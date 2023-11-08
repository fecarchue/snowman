using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);     //2d�����Ҷ��� �̰ɷ� �ؾߵ�. z�������ϰ� Ŭ���� �ǰ� �ؾߵż�?
    }
}