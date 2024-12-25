using UnityEngine;
using UnityEngine.Tilemaps;

// Script for handling the pickaxe mechanic and modifying tiles
public class AxeScript : MonoBehaviour
{
    [SerializeField] private Tilemap gameTilemap; // Reference to the game's tilemap
    [SerializeField] private TileBase mountainTile; // The mountain tile type to be replaced
    [SerializeField] private TileBase grassTile; // The grass tile type to replace the mountain tile
    private bool hasPickaxe = false; // Tracks whether the player has the pickaxe
    private Transform playerTransform; // Reference to the player's transform

    private void Update()
    {
        // Check if the player has the pickaxe and clicks the left mouse button
        if (hasPickaxe && Input.GetMouseButtonDown(0)) // Left mouse click
        {
            // Convert the mouse position on the screen to a world position
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Convert the world position to the corresponding tile position in the tilemap
            Vector3Int tilePosition = gameTilemap.WorldToCell(worldPosition);

            // Get the tile at the clicked position
            TileBase clickedTile = gameTilemap.GetTile(tilePosition);

            // Check if the clicked tile is a mountain tile
            if (clickedTile == mountainTile)
            {
                // Replace the mountain tile with a grass tile
                gameTilemap.SetTile(tilePosition, grassTile);
                Debug.Log("Mountain tile replaced with grass!"); // Log the replacement action
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the tag "Player"
        if (other.CompareTag("Player"))
        {
            hasPickaxe = true; // Mark that the player now has the pickaxe
            Debug.Log("Player picked up the pickaxe!"); // Log that the pickaxe was picked up

            // Attach the pickaxe to the player
            playerTransform = other.transform; // Get the player's transform
            transform.SetParent(playerTransform); // Set the pickaxe as a child of the player
            transform.localPosition = new Vector3(0.5f, 0.5f, 0); // Position the pickaxe relative to the player
        }
    }
}
