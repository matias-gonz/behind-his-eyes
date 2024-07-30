using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utils;

public class EndCardsScript : MonoBehaviour
{
    public Card card1;
    public Card card2;
    public Card card3;
    public Card card4;
    public Card card5;
    
    void Start()
    {
        card1.gameObject.SetActive(false);
        card2.gameObject.SetActive(false);
        card3.gameObject.SetActive(false);
        card4.gameObject.SetActive(false);
        card5.gameObject.SetActive(false);
        StartCoroutine(ShowCards());
    }

    private IEnumerator ShowCards()
    {
        yield return ShowCard(card1, 10, 2);
        yield return ShowCard(card2, 10, 2);
        yield return ShowCard(card3, 10, 2);
        yield return ShowCard(card4, 14, 2);
        yield return ShowCard(card5, 14, 2);
        GameManager.Instance.LoadScene(Scene.MainMenu);
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
