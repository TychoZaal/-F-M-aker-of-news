using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmericanFlag : Minigame
{
    public override void ActivateScene()
    {
        base.ActivateScene();
    }

    public override void DeactivateScene()
    {
        base.DeactivateScene();
    }

    [SerializeField]
    private float speedModifier = 0.5f, jumpBoost = 10.0f;

    [SerializeField]
    private Transform bottom, top;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= top.position.y)
        {
            ReachedTop();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpUpFlag();
        }

        if (transform.position.y <= bottom.position.y - speedModifier) return;

        transform.position = transform.position - new Vector3(0.0f, speedModifier, 0.0f) * Time.fixedDeltaTime * timeScale;
    }

    private void ReachedTop()
    {
        Debug.LogError("Reached Top");
    }

    private void JumpUpFlag()
    {
        transform.position = transform.position + new Vector3(0.0f, jumpBoost, 0.0f) * Time.fixedDeltaTime * timeScale;
    }
}
