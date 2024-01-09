using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScene : MonoBehaviour
{
    public void GameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void InventoryScene()
    {
        SceneManager.LoadScene("Inventory");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
