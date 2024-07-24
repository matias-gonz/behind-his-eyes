using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Line[] lines;
    
    public void Show()
    {
        StartCoroutine(ShowLines()); 
    }
    
    public void Hide()
    {
        foreach (var line in lines)
        {
            line.Hide();
        }
    }

    private IEnumerator ShowLines()
    {
        foreach (var line in lines)
        {
            line.Show();
            yield return new WaitForSeconds(4f);
        }  
    }
}
