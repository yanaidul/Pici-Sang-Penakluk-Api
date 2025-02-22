using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding : MonoBehaviour
{
    public float speed = 5f; // Kecepatan gerakan
    //public Transform targetPos; // Target yang akan dikejar
    public GameObject visualize;

    private GridPathManager gridManager;
    private List<Vector3> path;
    private int currentWaypoint = 0;
    private PathVisualizer pathVisualizer;
    private Character character;

    private void Awake()
    {
        pathVisualizer = GetComponent<PathVisualizer>();
        character = GetComponent<Character>();
    }

    void Start()
    {
        gridManager = FindAnyObjectByType<GridPathManager>();
    }

    private void Update()
    {

        if ((Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)))
        {
            if (character.IsAparTaken) return;
            Ray ray;
            if (Input.touchCount > 0)
            {
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            }
            else
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            }
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Posisi klik di tanah: " + hit.point);
                Vector3 target = hit.point;
                target.y = transform.position.y;
                pathVisualizer.SetTarget(target);
                path = FindPath(transform.position, target);
                currentWaypoint = 0;
                MoveAlongPath();

                OnSpawnArrow(target);
            }
        }

        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    path = FindPath(transform.position, targetPos.position);
        //    currentWaypoint = 0;
        //    MoveAlongPath();
        //}
    }

    public List<Vector3> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = gridManager.NodeFromWorldPoint(startPos);
        Node targetNode = gridManager.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node> { startNode };
        HashSet<Node> closedSet = new HashSet<Node>();

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }

            foreach (Node neighbor in gridManager.GetNeighbors(currentNode))
            {
                if (!neighbor.walkable || closedSet.Contains(neighbor))
                    continue;

                float newMovementCostToNeighbor = currentNode.gCost + Vector3.Distance(currentNode.worldPosition, neighbor.worldPosition);
                if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = Vector3.Distance(neighbor.worldPosition, targetNode.worldPosition);
                    neighbor.parent = currentNode;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return null;
    }

    List<Vector3> RetracePath(Node startNode, Node endNode)
    {
        List<Vector3> path = new List<Vector3>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode.worldPosition);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }

    public void MoveAlongPath()
    {
        StopAllCoroutines();    
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        if(path == null)
            yield break;    

        while (currentWaypoint < path.Count)
        {
            Vector3 targetPosition = path[currentWaypoint];
            targetPosition.y = transform.position.y;
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                transform.LookAt(targetPosition);
                yield return null;
            }

            currentWaypoint++;
        }
        pathVisualizer.DisableLine();
    }

    private void OnSpawnArrow(Vector3 target)
    {
        GameObject arrow = Instantiate(visualize, target, Quaternion.identity);
        Destroy(arrow, 2f);
    }
}
