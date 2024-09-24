using UnityEngine;

public class Ground : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        // Get the MeshRenderer component
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        // Calculate speed based on game speed and object scale
        float speed = GameManager.Instance.gameSpeed / transform.lossyScale.x;

        // Move the texture offset to create the illusion of the ground moving
        meshRenderer.material.mainTextureOffset += Vector2.right * speed * Time.deltaTime;
    }
}
