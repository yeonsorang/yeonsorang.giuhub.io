using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    //이미지
    public Image img;

    //페이드 효과
    public AnimationCurve curve;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    // 1-> 1-2 -> 2 -> 3 -> 4 5 6 7  2 34567
    IEnumerator FadeIn()
    {
        float a = 1;

        while(a >= 0f)
        {
            a -= Time.deltaTime;
            float cValue = curve.Evaluate(a); 
            img.color = new Color(0, 0, 0, cValue);
            yield return 0;  // 일단 멈춤
        }
    }

    // a:0 -> a:1
    IEnumerator FadeOut(string sceneName)
    {
        float a = 0;
        while (a <= 1f)
        {
            a += Time.deltaTime;
            float cValue = curve.Evaluate(a);
            img.color = new Color(0, 0, 0, cValue);
            yield return 0;   
        }

        SceneManager.LoadScene(sceneName);
    }

    //다른 씬으로 이동
    public void FadeTo(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }
}
