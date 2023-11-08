using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);     //2d게임할때는 이걸로 해야됨. z축고려안하고 클릭이 되게 해야돼서?
    }
}