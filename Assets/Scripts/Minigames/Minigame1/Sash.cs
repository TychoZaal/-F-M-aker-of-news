using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sash : MonoBehaviour
{
    private bool isOverlapping = false;

    public bool IsOverLapping { get { return isOverlapping; } }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("m1Woman"))
        {
            isOverlapping = true;
            Debug.Log("ENTER");
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("m1Woman"))
        {
            isOverlapping = false;
            Debug.Log("EXIT");
        }
    }
}
