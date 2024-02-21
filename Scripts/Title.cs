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
        
        //10���Ŀ� �ڵ����� ���θ޴� ������ �̵�
        countdown += Time.deltaTime;
        if(countdown >= gotoTime)
        {
            appIcon.SetTrigger("isWink");
            GotoMainMenu();
            return;
        }

        //�ִ�Ű ������ ������ �̵�
        if(Input.anyKeyDown)
        {
            appIcon.SetTrigger("isWink");
            GotoMainMenu();
        }
    }

    //���θ޴��� �̵�
    void GotoMainMenu()
    {
        sceneFader.FadeTo(loadToScene);
    }
}
