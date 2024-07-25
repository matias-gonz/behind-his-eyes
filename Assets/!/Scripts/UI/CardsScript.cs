using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsScript : MonoBehaviour
{
    public Card titleCard;
    public Card settingCard;
    public Card card1;
    public Card card2;
    public Card card3;

    void Start()
    {
        titleCard.gameObject.SetActive(false);
        settingCard.gameObject.SetActive(false);
        card1.gameObject.SetActive(false);
        card2.gameObject.SetActive(false);
        card3.gameObject.SetActive(false);
        StartCoroutine(ShowCards());
    }

    private IEnumerator ShowCards()
    {
        yield return ShowCard(titleCard, 5, 2);
        yield return ShowCard(settingCard, 5, 2);
        yield return ShowCard(card1, 10, 2);
        yield return ShowCard(card2, 14, 2);
        yield return ShowCard(card3, 6, 2);
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
