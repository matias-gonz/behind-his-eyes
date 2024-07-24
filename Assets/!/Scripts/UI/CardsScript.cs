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
        titleCard.gameObject.SetActive(true);
        settingCard.gameObject.SetActive(false);
        card1.gameObject.SetActive(false);
        card2.gameObject.SetActive(false);
        card3.gameObject.SetActive(false);
        StartCoroutine(ShowCards());
    }

    private IEnumerator ShowCards()
    {
        titleCard.Show();
        yield return new WaitForSeconds(5);
        titleCard.Hide();
        yield return new WaitForSeconds(2);
        
        settingCard.gameObject.SetActive(true);
        settingCard.Show();
        yield return new WaitForSeconds(5);
        settingCard.Hide();
        yield return new WaitForSeconds(2);
        
        card1.gameObject.SetActive(true);
        card1.Show();
        yield return new WaitForSeconds(5);
        card1.Hide();
        yield return new WaitForSeconds(2);
        
        card2.gameObject.SetActive(true);
        card2.Show();
        yield return new WaitForSeconds(5);
        card2.Hide();
        yield return new WaitForSeconds(2);
        
        card3.gameObject.SetActive(true);
        card3.Show();
        yield return new WaitForSeconds(5);
        card3.Hide();
    }
}
