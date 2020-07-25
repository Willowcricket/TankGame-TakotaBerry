using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public ScoreData playerScoreData;

    public void AddScoresToScoreList()
    {
        GameManager.Instance.scores.Add(playerScoreData);
    }
}
