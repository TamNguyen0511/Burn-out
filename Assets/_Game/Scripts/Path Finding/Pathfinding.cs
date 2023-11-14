using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    // For referencing the grid class
    private Grid GridReference;
    // Starting position to pathfind from
    public Transform StartPosition;
    // Starting position to pathfind to
    public Transform TargetPosition;

    private void Awake()
    {
        // Get a reference to the game manager
        GridReference = GetComponent<Grid>();
    }

    private void Update()
    {
        // Find a path to the goal
        FindPath(StartPosition.position, TargetPosition.position);
    }

    private void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        // Gets the node closest to the starting position
        Node startNode = GridReference.NodeFromWorldPoint(startPos);
        // Gets the node closest to the target position
        Node targetNode = GridReference.NodeFromWorldPoint(targetPos);

        // List of nodes for the open list
        List<Node> openList = new List<Node>();
        // Hashset of nodes for the closed list
        HashSet<Node> closedList = new HashSet<Node>();

        // Add the starting node to the open list to begin the program
        openList.Add(startNode);

        // Whilst there is something in the open list
        while (openList.Count > 0)
        {
            // Create a node and set it to the first item in the open list
            Node currentNode = openList[0];
            // Loop through the open list starting from the second object
            for (int i = 1; i < openList.Count; i++)
            {
                // If the f cost of that object is less than or equal to the f cost of the current node
                if (openList[i].FCost < currentNode.FCost ||
                    openList[i].FCost == currentNode.FCost &&
                    openList[i].HCost <
                    currentNode.HCost)
                {
                    // Set the current node to that object
                    currentNode = openList[i];
                }
            }

            // Remove that from the open list
            openList.Remove(currentNode);
            // And add it to the closed list
            closedList.Add(currentNode);

            // If the current node is the same as the target node
            if (currentNode == targetNode)
            {
                // Calculate the final path
                GetFinalPath(startNode, targetNode);
            }

            // Loop through each neighbor of the current node
            foreach (Node neighborNode in GridReference.GetNeighboringNodes(currentNode))
            {
                // If the neighbor is a wall or has already been checked
                if (!neighborNode.IsWall || closedList.Contains(neighborNode))
                {
                    // Skip it
                    continue;
                }

                int moveCost = currentNode.GCost +
                               GetManhattenDistance(currentNode, neighborNode); //Get the F cost of that neighbor

                // If the f cost is greater than the g cost or it is not in the open list
                if (moveCost < neighborNode.GCost || !openList.Contains(neighborNode))
                {
                    // Set the g cost to the f cost
                    neighborNode.GCost = moveCost;
                    // Set the h cost
                    neighborNode.HCost = GetManhattenDistance(neighborNode, targetNode);
                    // Set the parent of the node for retracing steps
                    neighborNode.ParentNode = currentNode;

                    // If the neighbor is not in the openList
                    if (!openList.Contains(neighborNode))
                    {
                        // Add it to the list
                        openList.Add(neighborNode);
                    }
                }
            }
        }
    }

    private void GetFinalPath(Node startingNode, Node endNode)
    {
        // List to hold the path sequentially
        List<Node> finalPath = new List<Node>();
        // Node to store the current node being checked
        Node currentNode = endNode; 

        // While loop to work through each node going through the parents to the beginning of the path
        while (currentNode != startingNode) 
        {
            // Add that node to the final path
            finalPath.Add(currentNode);
            // Move onto its parent node
            currentNode = currentNode.ParentNode; 
        }

        // Reverse the path to get the correct order
        finalPath.Reverse();

        // Set the final path
        GridReference.FinalPath = finalPath; 
    }

    private int GetManhattenDistance(Node nodeA, Node nodeB)
    {
        // x1-x2
        int iX = Mathf.Abs(nodeA.GridX - nodeB.GridX); 
        // y1-y2
        int iY = Mathf.Abs(nodeA.GridY - nodeB.GridY); 

        // Return the sum
        return iX + iY;
    }
}