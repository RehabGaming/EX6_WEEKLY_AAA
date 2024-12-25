using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component keeps a list of allowed tiles.
 * Such a list is used both for pathfinding and for movement.
 */
public class AllowedTiles : MonoBehaviour
{
    [SerializeField] private TileBase[] allowedTiles = null;

    public bool Contains(TileBase tile)
    {
        return allowedTiles.Contains(tile);
    }

    public TileBase[] Get()
    {
        return allowedTiles;
    }

    // Add generic tiles to the allowed tiles list
    public void AddAllowedTiles(TileBase[] tilesToAdd)
    {
        List<TileBase> updatedTiles = allowedTiles.ToList(); // Convert to List for modification
        foreach (TileBase tile in tilesToAdd)
        {
            if (!updatedTiles.Contains(tile)) // Avoid duplicates
            {
                updatedTiles.Add(tile);
            }
        }
        allowedTiles = updatedTiles.ToArray(); // Convert back to array
        Debug.Log("New tiles added to allowed tiles!");
    }
}
