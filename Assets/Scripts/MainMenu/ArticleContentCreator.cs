using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArticleContentCreator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title = null; 
    [SerializeField] TextMeshProUGUI text = null;
    [SerializeField] Image image = null;

    [SerializeField] GameObject mainMenu = null;
    [SerializeField] GameObject articleMenu = null;

    private string currentArticle = "";

    public void UpdateContent(ArticlePage articlePage)
    {
        title.text = articlePage.title;
        text.text = articlePage.articleText;
        image.sprite = articlePage.articleImage;
        currentArticle = articlePage.minigameScene;

        mainMenu.SetActive(false);
        articleMenu.SetActive(true);
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        articleMenu.SetActive(false);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(currentArticle);
    }
}
