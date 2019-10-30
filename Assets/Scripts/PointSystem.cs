using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointSystem : MonoBehaviour
{
    public static PointSystem sharedInstance;
    public Text points, hipoints;
    int normalScore;
    int hiScore;

    private void Start()
    {
        sharedInstance = GetComponent<PointSystem>();
        points.text = "0000";
        hipoints.text = "0000";
        LoadHiScore();
        AddScore(0);
    }

    public void AddScore(int e)
    {
        normalScore += e;

        WriteNormalScore();
        WriteHiScore();
    }

    void WriteNormalScore()
    {
        if (normalScore < 10)
            points.text = "000" + normalScore;
        else if (normalScore < 100)
            points.text = "00" + normalScore;
        else if (normalScore < 1000)
            points.text = "0" + normalScore;
        else
            points.text = normalScore.ToString();
    }

    void WriteHiScore()
    {
        if (normalScore >= hiScore)
        {
            hiScore = normalScore;
        }
            if (hiScore < 10)
                hipoints.text = "000" + hiScore;
            else if (hiScore < 100)
                hipoints.text = "00" + hiScore;
            else if (hiScore < 1000)
                hipoints.text = "0" + hiScore;
            else
                hipoints.text = "" + hiScore;
    }

    public void SaveHiScore()
    {
        SaveLoad.Save(hiScore);
    }

    public void LoadHiScore()
    {
        hiScore = SaveLoad.Load();
    }
}
