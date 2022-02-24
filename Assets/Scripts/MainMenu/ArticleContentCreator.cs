using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ArticleContentCreator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title = null; 
    [SerializeField] TextMeshProUGUI text = null;
    [SerializeField] Image image = null;

    [SerializeField] GameObject mainMenu = null;
    [SerializeField] GameObject articleMenu = null;

    public void updateContent(ArticlePage articlePage)
    {
        title.text = articlePage.title;
        text.text = articlePage.articleText;
        image.sprite = articlePage.articleImage;

        mainMenu.SetActive(false);
        articleMenu.SetActive(true);
    }

    public void backToMainMenu()
    {
        mainMenu.SetActive(true);
        articleMenu.SetActive(false);
    }
}
