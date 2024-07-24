using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Line[] lines;
    
    public void Show()
    {
        foreach (var line in lines)
        {
            Debug.Log(line);
            line.Show();
        }    
    }
    
    public void Hide()
    {
        foreach (var line in lines)
        {
            line.Hide();
        }
    }
}
