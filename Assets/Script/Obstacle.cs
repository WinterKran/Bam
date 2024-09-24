using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float leftEdge;

    private void Start()
    {
        // Calculate the left edge of the screen in world units
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
    }

    private void Update()
    {
        // Move the obstacle to the left based on the game speed
        transform.position += Vector3.left * GameManager.Instance.gameSpeed * Time.deltaTime;

        // Destroy the obstacle if it moves past the left edge of the screen
        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }
}
