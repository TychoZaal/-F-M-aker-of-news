using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public static GameManager Instance { get { return instance; } }

    //Invoke this event when the minigame has been finished to go back to the home menu
    public UnityEvent backToHome = new UnityEvent();

    ArticlePage currentArticle = null;

    public ArticlePage CurrentArticle { get { return currentArticle; } set { currentArticle = value; } }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
        backToHome.AddListener(GoBackToHome);
    }

    //Hij gaat er nu nog vanuit dat elke keer dat je terug gaat naar main menu dat betekend dat de game is gecomplete, maar je kan natuurlijk ook teruggaan naar homemenu zonder de game te completen
    private void GoBackToHome()
    {
        currentArticle.isCompleted = true;
        StartCoroutine(WaitGoBackHome());
    }

    IEnumerator WaitGoBackHome()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Newspaper");
    }

    public void SetMiniGameCompleted(ArticlePage articlePage)
    {
        articlePage.isCompleted = true;
    }

    public void ReturnToMain()
    {

    }
}
