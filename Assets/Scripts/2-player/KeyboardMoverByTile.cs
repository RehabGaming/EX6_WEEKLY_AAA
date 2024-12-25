using System.Linq; // Provides LINQ functionalities for collections
using UnityEngine; // Core Unity engine functionalities
using UnityEngine.Tilemaps; // Provides functionalities for tilemaps

/**
 * This component allows the player to move using arrow keys,
 * but only if the new position is on an allowed tile.
 */
public class KeyboardMoverByTile : KeyboardMover
{
    [SerializeField] Tilemap tilemap = null; // Reference to the tilemap used for validating movement
    [SerializeField] AllowedTiles allowedTiles = null; // Reference to the AllowedTiles component for tile validation

    /**
     * Gets the tile at a specific world position on the tilemap.
     * @param worldPosition The position in world coordinates.
     * @return The TileBase at the given position or null if no tile exists.
     */
    private TileBase TileOnPosition(Vector3 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition); // Convert world position to tilemap cell position
        return tilemap.GetTile(cellPosition); // Get the tile at the specified cell position
    }

    /**
     * Updates the player's position based on input,
     * ensuring movement only occurs on allowed tiles.
     */
    void Update()
    {
        Vector3 newPosition = NewPosition(); // Calculate the new position based on player input
        TileBase tileOnNewPosition = TileOnPosition(newPosition); // Get the tile at the new position
        if (allowedTiles.Contains(tileOnNewPosition))
        { // Check if the tile is in the allowed tiles list
            transform.position = newPosition; // Move to the new position if it's valid
        }
    }
}
