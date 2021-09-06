using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScriptUpload : MonoBehaviour
{

    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject leaderBoardPanel;

    // Start is called before the first frame update
    void Start() {
        if (leaderBoardPanel != null) {
            leaderBoardPanel.SetActive(false);
        }

        
        //Upload a score 
        LB_Controller.instance.StoreScore(11110, "Kiwi"); // parameters -> score, username
        LB_Controller.OnUpdatedScores += OnLeaderboardUpdated;
        StartCoroutine(DownloadScores());


        //Change a Username
        LB_Controller.instance.ChangeUsername("Mia_0815", "Mia_0816"); // parameters -> oldUsername, newUsername
        LB_Controller.OnUsernameChangedFinished += OnUsernameChaged;

    }

    IEnumerator DownloadScores() {
        yield return new WaitForSeconds(10);
        LB_Controller.instance.ReloadLeaderboard(); 
    }

    private void OnUsernameChaged(LB_ChangeUsernameResult result, LB_Entry[] entries) {
        switch (result) {
            case LB_ChangeUsernameResult.OK:
                // reload your board with the given entries and / or show a successmessage
                OnLeaderboardUpdated(entries);
                break;
            case LB_ChangeUsernameResult.USER_NOT_FOUND:
                //show an error to the user
                break;
            case LB_ChangeUsernameResult.USERNAME_ALREADY_TAKEN:
                //show an error to the user
                break;
            default:
                // do a fallback stuff here
                break;
        }

    }

    private void OnLeaderboardUpdated(LB_Entry[] entries) {
        if (entries != null && entries.Length > 0) {
            foreach (LB_Entry entry in entries)
            {
                Debug.Log("Rank: " + entry.rank + "; Name: " + entry.name + "; Points: " + entry.points);
            }
        } else if (entries == null) {
            Debug.Log("ups something went wrong");
        }
    }

    public void OkButtonTouched() {
        startPanel.SetActive(false);
        leaderBoardPanel.SetActive(true);
    }

    private void OnDestroy() {
        LB_Controller.OnUpdatedScores -= OnLeaderboardUpdated;
        LB_Controller.OnUsernameChangedFinished -= OnUsernameChaged;
    }

}
