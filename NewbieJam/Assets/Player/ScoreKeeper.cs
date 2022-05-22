using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    ScoreKeeper instance;
    int score = 0;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public int GetScore()
    {
        return score;
    }

    public void Score(int scoreToGet)
    {
        score += scoreToGet;
    }

    public void ResetScore()
    {
        score = 0;
    }
}
