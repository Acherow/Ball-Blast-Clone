using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    public static ScoreTracker instance;
    int score;

    TMP_Text txt;

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        txt = GetComponent<TMP_Text>();
    }

    public void AddScore(int amt)
    {
        score += amt;
        txt.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        txt.text = score.ToString();
    }
}
