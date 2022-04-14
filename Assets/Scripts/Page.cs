using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{
    public int pageNumber = 1;

    public void UpdatePosition(Vector3 pageOffset, Vector3 generalOffset)
    {
        transform.position = new Vector3(transform.position.x, pageNumber * pageOffset.y + generalOffset.y, transform.position.z);
    }
}
