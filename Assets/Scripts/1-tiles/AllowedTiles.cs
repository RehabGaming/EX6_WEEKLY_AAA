using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component just keeps a list of allowed tiles.
 * Such a list is used both for pathfinding and for movement.
 */
public class AllowedTiles : MonoBehaviour  {
    [SerializeField] TileBase[] allowedTiles = null;

    public bool Contains(TileBase tile) {
        return allowedTiles.Contains(tile);
    }

    public TileBase[] Get() { return allowedTiles;  }



    // Add water tiles to the allowed tiles list
    public void AddWaterTiles(TileBase[] waterTiles)
    {
        List<TileBase> updatedTiles = allowedTiles.ToList(); // Convert to List for modification
        foreach (TileBase waterTile in waterTiles)
        {
            if (!updatedTiles.Contains(waterTile)) // Avoid duplicates
            {
                updatedTiles.Add(waterTile);
            }
        }
        allowedTiles = updatedTiles.ToArray(); // Convert back to array
        Debug.Log("Water tiles added to allowed tiles!");
    }


    // Add mountain tiles to the allowed tiles list
    public void AddMountainTiles(TileBase[] mountainTiles)
    {
        List<TileBase> updatedTiles = allowedTiles.ToList(); // Convert to List for modification
        foreach (TileBase mountainTile in mountainTiles)
        {
            if (!updatedTiles.Contains(mountainTile)) // Avoid duplicates
            {
                updatedTiles.Add(mountainTile);
            }
        }
        allowedTiles = updatedTiles.ToArray(); // Convert back to array
        Debug.Log("Mountain tiles added to allowed tiles!");
    }
}

