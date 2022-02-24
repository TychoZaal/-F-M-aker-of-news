using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    [SerializeField]
    protected float timeScale = 0.0f;

    public virtual void ActivateScene()
    {
        timeScale = 1.0f;
    }

    public virtual void DeactivateScene()
    {
        timeScale = 0.0f;
    }
}
