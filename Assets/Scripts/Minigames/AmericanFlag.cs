using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private Transform minXY, maxXY;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(new Vector3(minXY.position.x, minXY.position.y, 0), new Vector3(minXY.position.x, maxXY.position.y, 0));
        Gizmos.DrawLine(new Vector3(minXY.position.x, maxXY.position.y, 0), new Vector3(maxXY.position.x, maxXY.position.y, 0));
        Gizmos.DrawLine(new Vector3(maxXY.position.x, maxXY.position.y, 0), new Vector3(maxXY.position.x, minXY.position.y, 0));
        Gizmos.DrawLine(new Vector3(maxXY.position.x, minXY.position.y, 0), new Vector3(minXY.position.x, minXY.position.y, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= top.position.y)
        {
            ReachedTop();
            return;
        }

        if (transform.position.y <= bottom.position.y - speedModifier) return;

        transform.position = transform.position - new Vector3(0.0f, speedModifier, 0.0f) * Time.fixedDeltaTime * timeScale;
    }

    private void OnMouseDown()
    {
        JumpUpFlag();
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
        }

        shake.Begin();

        reachedTop = true;
        GameManager.Instance.CompleteGame(page.pageNumber);
        StartCoroutine(CameraMovement.instance.ResetCamera(3.0f));
    }

    public void JumpUpFlag()
    {
        if (reachedTop) return;

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
