using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    
    public int questId = 10;

    public int questActionIndex;
    public GameObject[] questObject;

    Dictionary<int, QuestData> questList;

    // Start is called before the first frame update
    private void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }



    void GenerateData()
    {
        //questList.Add(0, new QuestData("����Ʈ �ޱ�", new int[] { 4000 }));
        questList.Add(10, new QuestData("���� ������ ��ȭ�ϱ�.", new int[] { 2000, 1000 }));
        questList.Add(20, new QuestData("�絵�� ���� ã���ֱ�.", new int[] { 5000, 1000 }));
        questList.Add(30, new QuestData("����Ʈ �� Ŭ����.", new int[] { 0 }));
    }
     
    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {

        //Next Talk Target
        if (id == questList[questId].npcId[questActionIndex])
        {   
            questActionIndex++;
        }
        //Control Quset Object
        ControlObject();

        //Talk Complete & Next Quest
        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();

        //Quest Name
        return questList[questId].questName;
    }

    public string CheckQuest()
    {
        //Quest Name
        return questList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    public void ControlObject()
    {
        switch(questId)
        {
            case 10:
                if (questActionIndex == 2)
                    questObject[0].SetActive(true);
                break;
            case 20:
                if (questActionIndex == 0)
                    questObject[0].SetActive(true);
                else if (questActionIndex == 1)
                    questObject[0].SetActive(false);
                break;
        }
    }


}
