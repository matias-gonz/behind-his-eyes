using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utils;

public class PreTutorialCardsScript : MonoBehaviour
{
    public Card titleCard;
    public Card timeCard;
    public GameManager gameManager;

    void Start()
    {
        titleCard.gameObject.SetActive(false);
        timeCard.gameObject.SetActive(false);
        StartCoroutine(ShowCards());
    }

    private IEnumerator ShowCards()
    {
        yield return ShowCard(titleCard, 5, 2);
        yield return ShowCard(timeCard, 4, 2);
        gameManager.LoadScene(Scene.Tutorial);
    }
    
    private IEnumerator ShowCard(Card card, float duration, float outDelay)
    {
        card.gameObject.SetActive(true);
        card.Show();
        yield return new WaitForSeconds(duration);
        card.Hide();
        yield return new WaitForSeconds(outDelay);
    }
}
