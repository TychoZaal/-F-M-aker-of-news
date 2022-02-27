using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 overViewPosition, page1Position, page2Position;

    public enum Focus { Overview, Page1, Page2, Article1, Article2};
    [SerializeField]
    private Focus focus = Focus.Overview;

    [SerializeField]
    private float movementSpeed = 1.0f;

    [SerializeField]
    private bool isMoving = false;

    [SerializeField]
    private List<Button> overviewButtons, pageOneButtons, pageTwoButtons;

    [SerializeField]
    private List<Minigame> minigames;

    [HideInInspector]
    public List<GameObject> articleOverlayButtons = new List<GameObject>();

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
                    overviewButtons[i].enabled = true;
                    overviewButtons[i].GetComponent<Image>().enabled = true;
                }
                for (int i = 0; i < pageOneButtons.Count; i++)
                {
                    pageOneButtons[i].enabled = false;
                    pageOneButtons[i].GetComponent<Image>().enabled = false;
                }
                for (int i = 0; i < pageTwoButtons.Count; i++)
                {
                    pageTwoButtons[i].enabled = false;
                    pageTwoButtons[i].GetComponent<Image>().enabled = false;
                }
                break;

            case Focus.Page1:
                for (int i = 0; i < overviewButtons.Count; i++)
                {
                    overviewButtons[i].enabled = false;
                    overviewButtons[i].GetComponent<Image>().enabled = false;
                }
                for (int i = 0; i < pageOneButtons.Count; i++)
                {
                    pageOneButtons[i].enabled = true;
                    pageOneButtons[i].GetComponent<Image>().enabled = true;
                }
                for (int i = 0; i < pageTwoButtons.Count; i++)
                {
                    pageTwoButtons[i].enabled = false;
                    pageTwoButtons[i].GetComponent<Image>().enabled = false;
                }
                break;

            case Focus.Page2:
                for (int i = 0; i < overviewButtons.Count; i++)
                {
                    overviewButtons[i].enabled = false;
                    overviewButtons[i].GetComponent<Image>().enabled = false;
                }
                for (int i = 0; i < pageOneButtons.Count; i++)
                {
                    pageOneButtons[i].enabled = false;
                    pageOneButtons[i].GetComponent<Image>().enabled = false;
                }
                for (int i = 0; i < pageTwoButtons.Count; i++)
                {
                    pageTwoButtons[i].enabled = true;
                    pageTwoButtons[i].GetComponent<Image>().enabled = true;
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

    public void ZoomIn(Vector3 location, Focus focus)
    {
        if (isMoving) return;

        StartCoroutine(MoveToPosition(transform, location, movementSpeed));
        this.focus = focus;
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

        for (int i = 0; i < minigames.Count; i++)
        {
            if (minigames[i] != null)
                minigames[i].DeactivateScene();
        }

        for (int i = 0; i < articleOverlayButtons.Count; i++)
        {
            articleOverlayButtons[i].SetActive(true);
        }
    }
}
