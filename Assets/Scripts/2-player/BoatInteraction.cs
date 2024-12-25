using System.Collections.Generic; // Provides support for collections like arrays and lists
using UnityEngine; // Core Unity engine functionalities
using UnityEngine.Tilemaps; // Provides functionalities related to tilemaps

public class BoatInteraction : MonoBehaviour
{
    public AllowedTiles allowedTilesComponent; // Reference to the AllowedTiles component to manage valid tiles
    public TileBase[] boatWaterTiles;          // Array of water tiles that the boat enables movement on

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object colliding with the boat has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Add the boat's water tiles to the list of allowed tiles
            allowedTilesComponent.AddWaterTiles(boatWaterTiles);

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
