using UnityEngine;
using UnityEngine.InputSystem;

/**
 * This component allows the player to move using the arrow keys.
 */
public class KeyboardMover : MonoBehaviour
{

    [SerializeField] InputAction moveAction; // Input action for handling keyboard movement
    private int zero = 0;

    void OnValidate()
    {
        // Provide default bindings for the input actions.
        // Ensures that the moveAction has default bindings for arrow keys.
        // Based on the solution by DMGregory: https://gamedev.stackexchange.com/a/205345/18261
        if (moveAction == null) // If no move action is defined, create one
            moveAction = new InputAction(type: InputActionType.Button); // Create a button input action
        if (moveAction.bindings.Count == zero) // If no bindings are present, add default bindings
            moveAction.AddCompositeBinding("2DVector") // Add a 2D vector composite binding
                .With("Up", "<Keyboard>/upArrow") // Bind up arrow for upward movement
                .With("Down", "<Keyboard>/downArrow") // Bind down arrow for downward movement
                .With("Left", "<Keyboard>/leftArrow") // Bind left arrow for leftward movement
                .With("Right", "<Keyboard>/rightArrow"); // Bind right arrow for rightward movement
    }

    private void OnEnable()
    {
        moveAction.Enable(); // Enable the input action when the component is enabled
    }

    private void OnDisable()
    {
        moveAction.Disable(); // Disable the input action when the component is disabled
    }

    protected Vector3 NewPosition()
    {
        // Check if the move action was performed in the current frame
        if (moveAction.WasPerformedThisFrame())
        {
            Vector3 movement = moveAction.ReadValue<Vector2>(); // Read the input as a Vector2 and implicitly convert to Vector3
            // Debug.Log("movement: " + movement); // Log the movement for debugging (commented out)
            return transform.position + movement; // Calculate the new position by adding movement to the current position
        }
        else
        {
            return transform.position; // If no movement, return the current position
        }
    }

    void Update()
    {
        // Update the player's position based on the calculated new position
        transform.position = NewPosition();
    }
}
