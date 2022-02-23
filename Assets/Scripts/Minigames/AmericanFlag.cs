using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmericanFlag : Minigame
{
    [SerializeField]
    private GameObject flag;

    private void FixedUpdate()
    {
        float rand = Random.Range(-10f, 10f);
        flag.transform.position = flag.transform.position + new Vector3(rand, rand, 0) * timeScale;
    }

    public override void ActivateScene()
    {
        base.ActivateScene();
    }

    public override void DeactivateScene()
    {
        base.DeactivateScene();
    }
}
