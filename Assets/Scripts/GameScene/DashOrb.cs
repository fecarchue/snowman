using UnityEngine;

public class DashItem : MonoBehaviour
{
    public float dashDuration = 3f; // Adjust this value based on your game's requirement
    private bool isPlayerTouching = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Dash on");
            isPlayerTouching = true;
            Destroy(gameObject); // Destroy the dash item when the player touches it
        }
    }

    private void Update()
    {
        if (isPlayerTouching && (Input.touchCount == 0 && !Input.GetMouseButton(0)))
        {
            ActivateDash();
        }
    }

    private void ActivateDash()
    {
        // Implement your dash activation logic here
        // For example, you can set a flag or call a method on the player script to activate dash
        // For demonstration, I'll just log a message
        Debug.Log("Dash!");
        isPlayerTouching = false; // Reset the player touching state
        Destroy(gameObject); // Optional: Destroy the dash item after the dash is activated
    }
}
