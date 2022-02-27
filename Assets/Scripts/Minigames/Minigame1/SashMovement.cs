using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SashMovement : Minigame
{
    private bool isClicked = false, wonGame = false;

    private void OnMouseDown()
    {
        if (!miniGameActive || wonGame) return;

        //  Debug.Log("mousedown");
        isClicked = true;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }

    private void OnMouseUp()
    {
        if (!miniGameActive || wonGame) return;

        isClicked = false;
        Debug.Log("mouseup");
        if (GetComponentInChildren<Sash>().IsOverLapping)
        {
            Debug.LogError("Won!!");
            wonGame = true;
            CameraMovement.instance.ResetCamera(3.0f);
        }
    }

    private void OnMouseDrag()
    {
        if (!miniGameActive || wonGame) return;

        Vector3 p = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(p);

       // Debug.Log("dragggg  " + Input.mousePosition);
        
        gameObject.transform.position = new Vector3(mousePosition.x, mousePosition.y, gameObject.transform.position.z);
    }
}
 