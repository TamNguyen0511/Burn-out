using UnityEngine;

namespace _Game.Scripts.Grid
{
    [System.Serializable]
    public class Node
    {
        //X, Y Position in the Node Array
        public int GridX;
        public int GridY;

        //Tells the program if this node is being obstructed.
        public bool IsWall;
        //The world position of the node.
        public Vector3 Position;

        //For the AStar algoritm, will store what node it previously came from so it cn trace the shortest path.
        public Node ParentNode;

        //The cost of moving to the next square.
        public int GCost;
        //The distance to the goal from this node.
        public int HCost;

        //Quick get function to add G cost and H Cost, and since we'll never need to edit FCost, we dont need a set function.
        public int FCost
        {
            get { return GCost + HCost; }
        }

        public Node(bool isWall, Vector3 pos, int gridX, int gridY) //Constructor
        {
            IsWall = isWall; //Tells the program if this node is being obstructed.
            Position = pos; //The world position of the node.
            GridX = gridX; //X Position in the Node Array
            GridY = gridY; //Y Position in the Node Array
        }
    }
}