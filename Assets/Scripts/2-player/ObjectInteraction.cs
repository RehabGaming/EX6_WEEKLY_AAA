using System.Collections.Generic; // Provides support for collections like arrays and lists
using UnityEngine; // Core Unity engine functionalities
using UnityEngine.Tilemaps; // Provides functionalities related to tilemaps

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] private AllowedTiles allowedTilesComponent; // Reference to the AllowedTiles component to manage valid tiles
    [SerializeField] private TileBase[] additionalAllowedTiles;  // Array of additional tiles to enable movement on

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding with the boat has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Add the additional tiles to the list of allowed tiles
            allowedTilesComponent.AddAllowedTiles(additionalAllowedTiles);

            // Notify the player's TargetMover component to update the pathfinding graph
            TargetMover targetMover = other.GetComponent<TargetMover>();
            if (targetMover != null) // Check if the player has a TargetMover component
            {
                targetMover.UpdatePathfindingGraph(); // Update the pathfinding graph to include new allowed tiles
            }

            // Destroy the boat object after interaction
            Destroy(gameObject);
        }
    }
}
