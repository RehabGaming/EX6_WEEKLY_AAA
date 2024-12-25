using System.Collections.Generic; // Provides support for collections like lists and arrays
using UnityEngine; // Core Unity engine functionalities
using UnityEngine.Tilemaps; // Provides functionalities for working with tilemaps

public class GoatInteraction : MonoBehaviour
{
    public AllowedTiles allowedTilesComponent;  // Reference to the AllowedTiles component to manage valid tiles
    public TileBase[] mountainTiles;            // Array of mountain tiles that the goat enables traversal on

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Add the mountain tiles to the list of allowed tiles for traversal
            allowedTilesComponent.AddMountainTiles(mountainTiles);

            // Notify the TargetMover component of the player to update the pathfinding graph
            TargetMover targetMover = other.GetComponent<TargetMover>();
            if (targetMover != null) // Check if the TargetMover component exists on the player
            {
                targetMover.UpdatePathfindingGraph(); // Update the graph to include the new allowed tiles
            }

            // Destroy the goat object after the interaction to prevent repeated usage
            Destroy(gameObject);
        }
    }
}
