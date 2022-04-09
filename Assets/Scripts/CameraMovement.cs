using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 overViewPosition, page1Position, page2Position, page3Position;

    public enum Focus { Overview, Page1, Page2, Page3, Article1, Article2, Article3};
    [SerializeField]
    private Focus focus = Focus.Overview;

    [SerializeField]
    private float movementSpeed = 1.0f;

    [SerializeField]
    private bool isMoving = false;

    [SerializeField]
    private List<Minigame> minigames;

    [HideInInspector]
    public List<GameObject> articleOverlayButtons = new List<GameObject>();

    public List<Button> allButtons = new List<Button>();

    public static CameraMovement instance;

    [SerializeField]
    float ZoomAmount = 0; //With Positive and negative values
    [SerializeField]
    float MaxToClamp = 10;
    [SerializeField]
    float rotSpeed = 10;
    [SerializeField]
    private float yMin, yMax;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = overViewPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ZoomOut();

        if (focus == Focus.Overview) Scroll();
    }

    public void ResetButtons(List<Button> exceptions)
    {
        for (int i = 0; i < allButtons.Count; i++)
        {
            allButtons[i].enabled = false;
        }

        for (int i = 0; i < exceptions.Count; i++)
        {
            exceptions[i].enabled = true;
        }
    }

    private void Scroll()
    {
        ZoomAmount += Input.GetAxis("Mouse ScrollWheel");
        ZoomAmount = Mathf.Clamp(ZoomAmount, -MaxToClamp, MaxToClamp);
        var translate = Mathf.Min(Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")), MaxToClamp - Mathf.Abs(ZoomAmount));

        Vector3 newPos = gameObject.transform.position + new Vector3(0, translate * rotSpeed * Mathf.Sign(Input.GetAxis("Mouse ScrollWheel")), 0);

        if (newPos.y <= yMin) return;
        if (newPos.y >= yMax) return;

        transform.position = newPos;

        overViewPosition = transform.position;
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

        for (int i = 0; i < minigames.Count; i++)
        {
            if (minigames[i] != null)
                minigames[i].DeactivateScene();
        }

        for (int i = 0; i < articleOverlayButtons.Count; i++)
        {
            articleOverlayButtons[i].SetActive(true);
        }

        for (int i = 0; i < allButtons.Count; i++)
        {
            if (allButtons[i].enabled)
            {
                allButtons[i].GetComponent<NewspaperButton>().ActivateParentButtons();
                break;
            }
        }
    }

    public IEnumerator ResetCamera(float timer, Focus focus)
    {
        yield return new WaitForSeconds(timer);

        this.focus = focus;

        ZoomOut();
    }
}
