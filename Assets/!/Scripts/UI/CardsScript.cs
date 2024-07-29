using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utils;

public class CardsScript : MonoBehaviour
{
    public Card settingCard;
    public Card card1;
    public Card card2;
    public Card card3;
    public Card timeCard;

    void Start()
    {
        settingCard.gameObject.SetActive(false);
        card1.gameObject.SetActive(false);
        card2.gameObject.SetActive(false);
        card3.gameObject.SetActive(false);
        timeCard.gameObject.SetActive(false);
        StartCoroutine(ShowCards());
    }

    private IEnumerator ShowCards()
    {
        yield return ShowCard(settingCard, 5, 2);
        yield return ShowCard(card1, 10, 2);
        yield return ShowCard(card2, 14, 2);
        yield return ShowCard(card3, 6, 2);
        yield return ShowCard(timeCard, 4, 2);
        GameManager.Instance.LoadScene(Scene.StreetLevel);
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
