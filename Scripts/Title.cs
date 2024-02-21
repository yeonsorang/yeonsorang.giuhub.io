using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    
    public string loadToScene = "MainMenu";

    public float countdown;

    public float gotoTime = 10f;

    public SceneFader sceneFader;

    public Animator appIcon;


    // Update is called once per frame
    void Update()
    {
        
        //10초후에 자동으로 메인메뉴 씬으로 이동
        countdown += Time.deltaTime;
        if(countdown >= gotoTime)
        {
            appIcon.SetTrigger("isWink");
            GotoMainMenu();
            return;
        }

        //애니키 누르면 씬으로 이동
        if(Input.anyKeyDown)
        {
            appIcon.SetTrigger("isWink");
            GotoMainMenu();
        }
    }

    //메인메뉴로 이동
    void GotoMainMenu()
    {
        sceneFader.FadeTo(loadToScene);
    }
}
