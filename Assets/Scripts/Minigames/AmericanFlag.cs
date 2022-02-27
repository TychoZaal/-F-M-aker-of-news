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
    private float speedModifier = 0.5f, jumpBoost = 10.0f, jumpLerpSpeed = 1.0f;

    [SerializeField]
    private Transform bottom, top;

    [SerializeField]
    private List<Rigidbody> stars = new List<Rigidbody>();

    [SerializeField] 
    private Transform middleStar;

    [SerializeField]
    private float explosionForce, explosionRadius;

    private bool reachedTop = false;

    [SerializeField]
    private CameraShake shake;

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
        if (reachedTop) return;

        for (int i = 0; i < stars.Count; i++)
        {
            stars[i].transform.parent = null;
            stars[i].isKinematic = false;
            stars[i].useGravity = true;
            stars[i].AddExplosionForce(explosionForce, middleStar.position, explosionRadius, 1.0f, ForceMode.Impulse);
            Destroy(stars[i].gameObject, 13.0f);
        }

        shake.Begin();

        reachedTop = true;
    }

    private void JumpUpFlag()
    {
        StartCoroutine(MoveToPosition(transform, transform.position + new Vector3(0.0f, jumpBoost, 0.0f) * Time.fixedDeltaTime * timeScale, jumpLerpSpeed));
    }

    public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }

}
