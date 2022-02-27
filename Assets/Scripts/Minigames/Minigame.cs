using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame : MonoBehaviour
{
    [SerializeField]
    protected float timeScale = 0.0f;

    protected bool miniGameActive = false;

    public virtual void ActivateScene()
    {
        timeScale = 1.0f;
        miniGameActive = true;
    }

    public virtual void DeactivateScene()
    {
        timeScale = 0.0f;
        miniGameActive = false;
    }
}
