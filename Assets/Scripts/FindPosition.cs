using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPosition : MonoBehaviour
{
    private GameObject cam;
    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cam.transform.position;
        Debug.LogError(transform.position);
    }
}
