using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 overViewPosition, page1Position, page2Position, p1tl, p1tr, p1b, p2tl, p2tr, p2bl, p2br;

    private enum Focus { Overview, Page1, Page2, Article1, Article2};
    [SerializeField]
    private Focus focus = Focus.Overview;

    [SerializeField]
    private float movementSpeed = 1.0f;

    [SerializeField]
    private bool isMoving = false;

    [SerializeField]
    private List<GameObject> overviewButtons, pageOneButtons, pageTwoButtons; 

    // Start is called before the first frame update
    void Start()
    {
        overViewPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ManageButtons();

        if (Input.GetKeyDown(KeyCode.Escape))
            ZoomOut();
    }

    private void ManageButtons()
    {
        switch (focus)
        {
            case Focus.Overview:
                for (int i = 0; i < overviewButtons.Count; i++)
                {
                    overviewButtons[i].SetActive(true);
                }
                for (int i = 0; i < pageOneButtons.Count; i++)
                {
                    pageOneButtons[i].SetActive(false);
                }
                for (int i = 0; i < pageTwoButtons.Count; i++)
                {
                    pageTwoButtons[i].SetActive(false);
                }
                break;

            case Focus.Page1:
                for (int i = 0; i < overviewButtons.Count; i++)
                {
                    overviewButtons[i].SetActive(false);
                }
                for (int i = 0; i < pageOneButtons.Count; i++)
                {
                    pageOneButtons[i].SetActive(true);
                }
                for (int i = 0; i < pageTwoButtons.Count; i++)
                {
                    pageTwoButtons[i].SetActive(false);
                }
                break;

            case Focus.Page2:
                for (int i = 0; i < overviewButtons.Count; i++)
                {
                    overviewButtons[i].SetActive(false);
                }
                for (int i = 0; i < pageOneButtons.Count; i++)
                {
                    pageOneButtons[i].SetActive(false);
                }
                for (int i = 0; i < pageTwoButtons.Count; i++)
                {
                    pageTwoButtons[i].SetActive(true);
                }
                break;
        }
    }

    public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            isMoving = true;
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
        isMoving = false;
    }

    private Vector3 DecypherLocation(string location)
    {
        switch (location)
        {
            case "P1":
                focus = Focus.Page1;
                return page1Position;
            case "P2":
                focus = Focus.Page2;
                return page2Position;
            case "P1TL":
                focus = Focus.Article1;
                return p1tl;
            case "P1TR":
                focus = Focus.Article1;
                return p1tr;
            case "P1B":
                focus = Focus.Article1;
                return p1b;
            case "P2TL":
                focus = Focus.Article2;
                return p2tl;
            case "P2TR":
                focus = Focus.Article2;
                return p2tr;
            case "P2BL":
                focus = Focus.Article2;
                return p2bl;
            case "P2BR":
                focus = Focus.Article2;
                return p2br;
        }

        return Vector3.zero;
    }

    public void ZoomIn(string location)
    {
        Debug.LogError("Zoomin");

        if (isMoving) return;

        StartCoroutine(MoveToPosition(transform, DecypherLocation(location), movementSpeed));
    }

    public void ZoomOut()
    {
        if (isMoving) return;

        switch (focus)
        {
            case Focus.Article1:
                StartCoroutine(MoveToPosition(transform, page1Position, movementSpeed));
                focus = Focus.Page1;
                break;
            case Focus.Article2:
                StartCoroutine(MoveToPosition(transform, page2Position, movementSpeed));
                focus = Focus.Page2;
                break;
            case Focus.Page1:
                StartCoroutine(MoveToPosition(transform, overViewPosition, movementSpeed));
                focus = Focus.Overview;
                break;
            case Focus.Page2:
                StartCoroutine(MoveToPosition(transform, overViewPosition, movementSpeed));
                focus = Focus.Overview;
                break;
        }
    }
}
