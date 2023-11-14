using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Grid
{
    public class Grid : MonoBehaviour
    {
        // This is where the program will start the pathfinding from.
        public Transform StartPosition;
        // This is the mask that the program will look for when trying to find obstructions to the path.
        public LayerMask WallMask;
        public string FloorTag = "Floor";
        // A vector2 to store the width and height of the graph in world units.
        public Vector2 GridWorldSize;
        // This stores how big each square on the graph will be
        public float NodeRadius;
        // The distance that the squares will spawn from each other.
        public float DistanceBetweenNodes;

        // The array of nodes that the A Star algorithm uses.
        private Node[,] _nodeArray;
        // The completed path that the red line will be drawn along
        public List<Node> FinalPath;

        // Twice the amount of the radius (Set in the start function)
        private float _nodeDiameter;
        // Size of the Grid in Array units.
        private int _gridSizeX;
        private int _gridSizeY;

        private void Start()
        {
            // Double the radius to get diameter
            _nodeDiameter = NodeRadius * 2;

            // Divide the grids world co-ordinates by the diameter to get the size of the graph in array units.
            _gridSizeX = Mathf.RoundToInt(GridWorldSize.x / _nodeDiameter);
            // Divide the grids world co-ordinates by the diameter to get the size of the graph in array units.
            _gridSizeY = Mathf.RoundToInt(GridWorldSize.y / _nodeDiameter);

            // Draw the grid
            CreateGrid();
        }

        private void CreateGrid()
        {
            // Declare the array of nodes.
            _nodeArray = new Node[_gridSizeX, _gridSizeY];
            // Get the real world position of the bottom left of the grid.
            Vector3 bottomLeft = transform.position - Vector3.right * GridWorldSize.x / 2 -
                                 Vector3.forward * GridWorldSize.y / 2;

            // Loop through the array of nodes.
            for (int x = 0; x < _gridSizeX; x++)
            {
                // Loop through the array of nodes
                for (int y = 0; y < _gridSizeY; y++)
                {
                    // Get the world co ordinates of the bottom left of the graph
                    Vector3 worldPoint = bottomLeft + Vector3.right * (x * _nodeDiameter + NodeRadius) +
                                         Vector3.forward * (y * _nodeDiameter + NodeRadius);

                    // Make the node a wall
                    bool wall = false;

                    // If the node is not being obstructed
                    // Quick collision check against the current node and anything in the world at its position. If it is colliding with an object with a WallMask,
                    // The if statement will return false

                    // if (Physics.CheckSphere(worldPoint, NodeRadius, WallMask))
                    // {
                    //     // Object is not a wall
                    //     wall = true;
                    // }
                    var collidedObjects = Physics.OverlapSphere(worldPoint, NodeRadius, WallMask);
                    if (collidedObjects.Length > 0)
                    {
                        foreach (Collider collidedObject in collidedObjects)
                        {
                            if (collidedObject.CompareTag(FloorTag))
                            {
                                wall = false;
                                break;
                            }
                            else
                            {
                                wall = true;
                            }
                        }
                    }
                    // Create a new node in the array.
                    _nodeArray[x, y] = new Node(wall, worldPoint, x, y);
                }
            }
        }

        //Function that gets the neighboring nodes of the given node.
        public List<Node> GetNeighboringNodes(Node neighborNode)
        {
            //Make a new list of all available neighbors.
            List<Node> neighborList = new List<Node>();
            //Variable to check if the XPosition is within range of the node array to avoid out of range errors.
            int icheckX;
            //Variable to check if the YPosition is within range of the node array to avoid out of range errors.
            int icheckY;

            //Check the right side of the current node.
            icheckX = neighborNode.GridX + 1;
            icheckY = neighborNode.GridY;

            //If the XPosition is in range of the array
            if (icheckX >= 0 && icheckX < _gridSizeX)
            {
                //If the YPosition is in range of the array
                if (icheckY >= 0 && icheckY < _gridSizeY)
                {
                    //Add the grid to the available neighbors list
                    neighborList.Add(_nodeArray[icheckX, icheckY]);
                }
            }

            //Check the Left side of the current node.
            icheckX = neighborNode.GridX - 1;
            icheckY = neighborNode.GridY;

            //If the XPosition is in range of the array
            if (icheckX >= 0 && icheckX < _gridSizeX)
            {
                //If the YPosition is in range of the array
                if (icheckY >= 0 && icheckY < _gridSizeY)
                {
                    //Add the grid to the available neighbors list
                    neighborList.Add(_nodeArray[icheckX, icheckY]);
                }
            }

            //Check the Top side of the current node.
            icheckX = neighborNode.GridX;
            icheckY = neighborNode.GridY + 1;

            //If the XPosition is in range of the array
            if (icheckX >= 0 && icheckX < _gridSizeX)
            {
                //If the YPosition is in range of the array
                if (icheckY >= 0 && icheckY < _gridSizeY)
                {
                    //Add the grid to the available neighbors list
                    neighborList.Add(_nodeArray[icheckX, icheckY]);
                }
            }

            //Check the Bottom side of the current node.
            icheckX = neighborNode.GridX;
            icheckY = neighborNode.GridY - 1;

            //If the XPosition is in range of the array
            if (icheckX >= 0 && icheckX < _gridSizeX)
            {
                //If the YPosition is in range of the array
                if (icheckY >= 0 && icheckY < _gridSizeY)
                {
                    //Add the grid to the available neighbors list
                    neighborList.Add(_nodeArray[icheckX, icheckY]);
                }
            }

            //Return the neighbors list.
            return neighborList;
        }

        //Gets the closest node to the given world position.
        public Node NodeFromWorldPoint(Vector3 worldPos)
        {
            float xPos = ((worldPos.x + GridWorldSize.x / 2) / GridWorldSize.x);
            float yPos = ((worldPos.z + GridWorldSize.y / 2) / GridWorldSize.y);

            xPos = Mathf.Clamp01(xPos);
            yPos = Mathf.Clamp01(yPos);

            int ix = Mathf.RoundToInt((_gridSizeX - 1) * xPos);
            int iy = Mathf.RoundToInt((_gridSizeY - 1) * yPos);

            return _nodeArray[ix, iy];
        }


        //Function that draws the wireframe
        private void OnDrawGizmos()
        {
            //Draw a wire cube with the given dimensions from the Unity inspector
            Gizmos.DrawWireCube(transform.position,
                new Vector3(GridWorldSize.x, 1, GridWorldSize.y));

            //If the grid is not empty
            if (_nodeArray != null)
            {
                //Loop through every node in the grid
                foreach (Node n in _nodeArray)
                {
                    //If the current node is a wall node
                    if (n.IsWall)
                    {
                        //Set the color of the node
                        Gizmos.color = Color.yellow;
                    }
                    else
                    {
                        //Set the color of the node
                        Gizmos.color = Color.white;
                    }

//If the final path is not empty
                    if (FinalPath != null)
                    {
                        //If the current node is in the final path
                        if (FinalPath.Contains(n))
                        {
                            //Set the color of that node
                            Gizmos.color = Color.red;
                        }
                    }

//Draw the node at the position of the node.
                    Gizmos.DrawCube(n.Position,
                        Vector3.one * (_nodeDiameter - DistanceBetweenNodes));
                }
            }
        }
    }
}