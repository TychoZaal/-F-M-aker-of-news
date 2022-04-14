using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CloudMovement : Minigame
{
    [SerializeField]
    private List<Transform> clouds;

    private Vector3 startPos, endPos;

    [SerializeField]
    private float moveSpeed = 0.5f, defaultSpeed = 1.0f;

    Vector3 newPos;

    [SerializeField]
    private Transform minXY, maxXY;

    [SerializeField]
    private bool sunny = false;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(new Vector3(minXY.position.x, minXY.position.y, 0), new Vector3(minXY.position.x, maxXY.position.y, 0));
        Gizmos.DrawLine(new Vector3(minXY.position.x, maxXY.position.y, 0), new Vector3(maxXY.position.x, maxXY.position.y, 0));
        Gizmos.DrawLine(new Vector3(maxXY.position.x, maxXY.position.y, 0), new Vector3(maxXY.position.x, minXY.position.y, 0));
        Gizmos.DrawLine(new Vector3(maxXY.position.x, minXY.position.y, 0), new Vector3(minXY.position.x, minXY.position.y, 0));
    }

    private void OnMouseDown()
    {
        if (!miniGameActive) return;

        startPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, clouds[0].gameObject.transform.position.z));
    }

    private void OnMouseUp()
    {
        if (!miniGameActive) return;

        endPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, clouds[0].gameObject.transform.position.z));

        MoveClouds(endPos - startPos);
    }

    private void MoveClouds(Vector3 direction)
    {
        direction.z = 1.0f;

        float distance = 0.0f;

        for (int i = 0; i < clouds.Count; i++)
        {
            distance = Vector3.Distance(startPos, clouds[i].position) / 100;

            newPos = clouds[i].position + direction * distance * defaultSpeed * Mathf.Abs(clouds[i].localScale.x * clouds[i].localScale.y * clouds[i].localScale.z);

            StartCoroutine(MoveToPosition(clouds[i], newPos, moveSpeed));
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < clouds.Count; i++)
        {
            Gizmos.DrawLine(clouds[i].position, newPos);
        }
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

    private void Sunny()
    {
        sunny = true;
        GameManager.Instance.CompleteGame(page.pageNumber);
        Debug.LogError("Sunny");
    }

    private void Update()
    {
        for (int i = 0; i < clouds.Count; i++)
        {
            if (clouds[i].position.x < maxXY.position.x &&
                clouds[i].position.x > minXY.position.x &&
                clouds[i].position.y < maxXY.position.y &&
                clouds[i].position.y > minXY.position.y)
            {
                return;
            }
        }

        if (!sunny)
            Sunny();
    }
}
