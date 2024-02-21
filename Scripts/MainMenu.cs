using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string loadToScene = "2DTopDown";

    public SceneFader sceneFader;

    GameManager gameManager;

    public void Play()
    {
        //���� ����
        
        sceneFader.FadeTo(loadToScene);
    }

    public void Reset()
    {
        PlayerPrefs.SetFloat("PlayerX", 0);
        PlayerPrefs.SetFloat("PlayerY", -3);

        //Quest Id
        PlayerPrefs.SetInt("QuestId", 10);

        //Quest Action Index
        PlayerPrefs.SetInt("QuestActionIndex", 0);
    }

    public void Quit()
    {
        //���� ����
        Application.Quit();
    }
}
