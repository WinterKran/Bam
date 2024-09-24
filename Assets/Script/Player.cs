using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction; // Stores the current movement direction

    public float gravity = 9.81f * 2f; // Gravity force
    public float jumpForce = 8f; // Jumping force

    private void Awake()
    {
        character = GetComponent<CharacterController>(); // Get the CharacterController component
    }

    private void OnEnable()
    {
        direction = Vector3.zero; // Reset movement direction when enabled
    }

    private void Update()
    {
        // Apply gravity over time
        direction += Vector3.down * gravity * Time.deltaTime;

        // Check if the character is grounded
        if (character.isGrounded)
        {
            direction = Vector3.down; // Reset vertical velocity when on the ground

            // Check for jump input
            if (Input.GetButton("Jump"))
            {
                direction = Vector3.up * jumpForce; // Apply upward force when jumping
            }
        }

        // Move the character according to the direction and deltaTime
        character.Move(direction * Time.deltaTime);
    }

    // Detect collisions with obstacles
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the "Obstacle" tag
        if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver(); // Trigger GameOver in the GameManager
        }
    }
}
