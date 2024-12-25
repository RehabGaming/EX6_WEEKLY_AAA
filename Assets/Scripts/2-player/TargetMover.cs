using System.Collections; // Provides support for coroutines
using System.Collections.Generic; // Enables the use of collections like lists
using UnityEngine; // Core Unity engine functionalities
using UnityEngine.Tilemaps; // Provides tools for working with tilemaps

/**
 * This component moves its object towards a given target position on a tilemap.
 */
public class TargetMover : MonoBehaviour
{
    [SerializeField] Tilemap tilemap = null; // Reference to the tilemap used for movement
    [SerializeField] AllowedTiles allowedTiles = null; // Reference to the AllowedTiles component to manage valid tiles

    [Tooltip("The speed by which the object moves towards the target, in meters (=grid units) per second")]
    [SerializeField] float speed = 2f; // Speed of movement in grid units per second

    [Tooltip("Maximum number of iterations before BFS algorithm gives up on finding a path")]
    [SerializeField] int maxIterations = 1000; // Maximum iterations for the BFS algorithm

    [Tooltip("The target position in world coordinates")]
    [SerializeField] Vector3 targetInWorld; // The target position in world coordinates

    [Tooltip("The target position in grid coordinates")]
    [SerializeField] Vector3Int targetInGrid; // The target position in grid coordinates

    protected bool atTarget;  // Tracks whether the object has reached the target

    private int one = 1;

    /**
     * Sets a new target position and updates the grid position accordingly.
     * @param newTarget The new target position in world coordinates.
     */
    public void SetTarget(Vector3 newTarget)
    {
        if (targetInWorld != newTarget)
        { // Update the target only if it's different from the current one
            targetInWorld = newTarget; // Update the target in world coordinates
            targetInGrid = tilemap.WorldToCell(targetInWorld); // Convert to grid coordinates
            atTarget = false; // Mark the object as not yet at the target
        }
    }

    /**
     * Returns the current target position in world coordinates.
     * @return The target position in world coordinates.
     */
    public Vector3 GetTarget()
    {
        return targetInWorld; // Return the target in world coordinates
    }

    private TilemapGraph tilemapGraph = null; // Representation of the tilemap as a graph for pathfinding
    private float timeBetweenSteps; // Time delay between movement steps

    /**
     * Initializes the tilemap graph, sets the default target, and starts the movement coroutine.
     */
    protected virtual void Start()
    {
        tilemapGraph = new TilemapGraph(tilemap, allowedTiles.Get()); // Create a graph based on the tilemap and allowed tiles
        timeBetweenSteps = one / speed; // Calculate the time delay between movement steps based on speed

        // Set default target to the player's starting position
        if (targetInWorld == Vector3.zero)
        { // Check if the target is the default (0,0,0)
            targetInWorld = transform.position; // Set the target to the player's current position
            targetInGrid = tilemap.WorldToCell(targetInWorld); // Convert to grid position
        }

        StartCoroutine(MoveTowardsTheTarget()); // Start the coroutine for movement
    }

    /**
     * Coroutine that moves the object towards the target, one step at a time.
     */
    IEnumerator MoveTowardsTheTarget()
    {
        for (; ; )
        { // Infinite loop for continuous movement
            yield return new WaitForSeconds(timeBetweenSteps); // Wait before making the next step
            if (enabled && !atTarget) // Check if the component is enabled and the object hasn't reached the target
                MakeOneStepTowardsTheTarget(); // Take one step towards the target
        }
    }

    /**
     * Calculates and performs one step towards the target using BFS pathfinding.
     */
    private void MakeOneStepTowardsTheTarget()
    {
        Vector3Int startNode = tilemap.WorldToCell(transform.position); // Get the current position in grid coordinates
        Vector3Int endNode = targetInGrid; // Get the target position in grid coordinates
        List<Vector3Int> shortestPath = BFS.GetPath(tilemapGraph, startNode, endNode, maxIterations); // Perform BFS to find the shortest path

        Debug.Log("shortestPath = " + string.Join(" , ", shortestPath)); // Log the path for debugging

        if (shortestPath.Count >= one + one)
        { // Check if a valid path exists
            Vector3Int nextNode = shortestPath[1]; // Get the next step in the path
            transform.position = tilemap.GetCellCenterWorld(nextNode); // Move to the center of the next tile
        }
        else
        {
            if (shortestPath.Count == one - one)
            { // Check if no path is found
                //Debug.LogError($"No path found between {startNode} and {endNode}"); // Log an error if no path exists
            }
            atTarget = true; // Mark the object as having reached the target
        }
    }

    /**
     * Updates the tilemap graph to reflect changes in allowed tiles.
     */
    public void UpdatePathfindingGraph()
    {
        tilemapGraph = new TilemapGraph(tilemap, allowedTiles.Get()); // Recreate the graph with updated tiles
    }
}
