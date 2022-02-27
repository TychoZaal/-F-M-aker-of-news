using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadContent : MonoBehaviour
{
    [SerializeField] GameObject content = null;

    // Start is called before the first frame update
    void Start()
    {
        content.SetActive(true);
    }
}
