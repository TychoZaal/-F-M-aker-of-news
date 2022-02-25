using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SashMovement : MonoBehaviour
{
    private bool isClicked = false;

    private void OnMouseDown()
    {
      //  Debug.Log("mousedown");
        isClicked = true;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }

    private void OnMouseUp()
    {
        isClicked = false;
        Debug.Log("mouseup");
        if (GetComponentInChildren<Sash>().IsOverLapping)
        {
            Debug.Log("Won!!");

            GetComponent<Collider2D>().enabled = false;

        }
    }

    private void OnMouseDrag()
    {
        Vector3 p = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(p);

       // Debug.Log("dragggg  " + Input.mousePosition);
        
        gameObject.transform.position = new Vector3(mousePosition.x, mousePosition.y, gameObject.transform.position.z);
    }

    private void FixedUpdate()
    {
        //if()
    }
}
 