using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public TalkManager talkManager;
    public QuestManager questManager;
    public Animator talkPanel;
    public Image portraitImg;
    public Animator portraitAnim;
    public Sprite prevPortrait;
    public TypeEffect talk;
    public TextMeshProUGUI questTalk;
    public GameObject menuSet;
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;
    public GameObject Player;

    public SceneFader sceneFader;

    public string loadToScene = "MainMenu";

    void Start()
    {
        //questManager.questId = 10;
        GameLoad();
        questTalk.text = questManager.CheckQuest();
    }

    void Update()
    {

        //Sub Menu
        if (Input.GetButtonDown("Cancel"))
        {
            if(menuSet.activeSelf)
                menuSet.SetActive(false);
            else
                menuSet.SetActive(true);
        }

    }

    public void Action(GameObject scanObj)
    {
        //Get Current Object
        scanObject = scanObj;
        //talkText.text = "이것의 이름은 " + scanObj.name + "이라고 한다.";
        ObjData objData = scanObj.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);

        //Visible Talk for Action
        talkPanel.SetBool("IsShow", isAction);
    }

    void Talk(int id, bool isNpc)
    {
        //대화데이터 세팅
        int questTalkIndex = 0;
        string talkData = "";

        if (talk.isAnim)
        { 
            talk.SetMsg("");
            return;
        }
        else
        {
            questTalkIndex = questManager.GetQuestTalkIndex(id);
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        }


        //대화끝내기
        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            questTalk.text = questManager.CheckQuest(id);
            Debug.Log(questManager.CheckQuest(id));
            return;
        }

        //대화하기
        if(isNpc) 
        { 
            talk.SetMsg(talkData.Split(':')[0]);

            //이미지 보여주기
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);

            //이미지 애니메이션
            if (prevPortrait != portraitImg.sprite)
            {
                portraitAnim.SetTrigger("DoEffect");
                prevPortrait = portraitImg.sprite;
            }
        }
        else
        {
            talk.SetMsg(talkData);

            //이미지 숨기기
            portraitImg.color = new Color(1, 1, 1, 0);
        }

        isAction = true;
        talkIndex++;
    }

    public void GameSave()
    {
        //player.x, player.y
        PlayerPrefs.SetFloat("PlayerX", Player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", Player.transform.position.y);

        //Quest Id
        PlayerPrefs.SetInt("QuestId", questManager.questId);

        //Quest Action Index
        PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex);

        PlayerPrefs.Save();


        menuSet.SetActive(false);
    }

    public void GameLoad() 
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
        {
            Debug.Log("저장된 게임 데이터가 없습니다.");
            return;
        }

        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int questId = PlayerPrefs.GetInt("QuestId");
        int questActionIndex = PlayerPrefs.GetInt("QuestActionIndex");

        Player.transform.position = new Vector3(x, y, 0);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
        questManager.ControlObject();
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        sceneFader.FadeTo(loadToScene);
    }

}
