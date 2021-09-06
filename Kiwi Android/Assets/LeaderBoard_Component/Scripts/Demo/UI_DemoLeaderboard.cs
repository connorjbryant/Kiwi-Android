using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DemoLeaderboard : MonoBehaviour
{

    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject leaderBoardPanel;
    [SerializeField] VerticalLayoutGroup layoutGroup;
    [SerializeField] GameObject entryPrefab;

    [Space (10)]
    [SerializeField] Text rankText;

    private RectTransform rt;
    // Start is called before the first frame update
   
    private void OnEnable() {
        rt = GetComponent<RectTransform>();
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, rt.rect.width);
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, rt.rect.height);


        if(LB_Controller.instance != null) {
             //pass the username as string
            LB_Controller.OnUpdatedScores += OnLeaderboardUpdated;
            LB_Controller.instance.ReloadLeaderboard();
        }
    }

    public void BackbutttonTouched() {
        leaderBoardPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    private void OnDisable() {
        RemoveAllUIEntries();
        leaderBoardPanel.SetActive(false);
        mainPanel.SetActive(true);
    }


    private void RemoveAllUIEntries() {
        foreach (Transform child in layoutGroup.transform) {
            if (child.gameObject.tag == "LeaderBoardRowEntry") {
                Destroy(child.gameObject);
            }
        }
    }

    private void OnLeaderboardUpdated(LB_Entry[] entries) {
        if (entries != null && entries.Length > 0) {
            RemoveAllUIEntries();
            foreach (LB_Entry entry in entries) {
                GameObject entryRow = Instantiate(entryPrefab);
                entryRow.transform.SetParent(layoutGroup.transform);
                UIDemoLeaderboardEntry rowEntry = entryRow.GetComponent<UIDemoLeaderboardEntry>();
                rowEntry.Setup(entry);
            }
        } else if (entries == null) {
            Debug.Log("ups something went wrong");
        }

        SetupRank();
    }

    private void SetupRank() {
        int rank = LB_Controller.instance.GetRankForUser("Tom-531");
        if (rank == 0) {
            //rank is unknown
            rankText.text = "Sorry, can´t find a rank!";
        } else {
            rankText.text = "You are Rank #" + rank;
        }
        
    }
}
