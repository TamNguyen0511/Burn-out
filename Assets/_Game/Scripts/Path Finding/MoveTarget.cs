using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MoveTarget : MonoBehaviour
{
    public LayerMask HitLayers;

    private void Update()
    {
        //If the player has left clicked
        if (Input.GetMouseButtonDown(0)) 
        {
            //Get the mouse Position
            Vector3 mouse = Input.mousePosition;
            //Cast a ray to get where the mouse is pointing at
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            //Stores the position where the ray hit.
            RaycastHit hit; 
            //If the raycast doesnt hit a wall
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, HitLayers)) 
            {
                //Move the target to the mouse position
                this.transform.position = hit.point;
            }
        }
    }
}