using System.Collections.Generic;
using UnityEngine;
public class PathVisualizer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] AStarPathfinding pathfinder; 
    [SerializeField] Vector3 target;
    [SerializeField] Transform character; 
    [SerializeField] float lineWidth = 0.1f; 

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (target == Vector3.zero) return;
        DrawPath();
    }

    public void SetTarget(Vector3 newtarget)
    {
        lineRenderer.enabled = true;
        target = newtarget;
    }

    public void DisableLine()
    {
        target = Vector3.zero;
        lineRenderer.enabled = false;
    }

    void DrawPath()
    {

        // Get path from the A* script
        List<Vector3> path = pathfinder.FindPath(character.position, target);

        if (path == null || path.Count == 0)
        {
            lineRenderer.positionCount = 0; // Clear the line if no path
            return;
        }

        // Set the line renderer positions
        lineRenderer.positionCount = path.Count;
        for (int i = 0; i < path.Count; i++)
        {
            lineRenderer.SetPosition(i, path[i] + Vector3.up * 0.1f); // Slightly lift the line for better visibility
        }
    }
}