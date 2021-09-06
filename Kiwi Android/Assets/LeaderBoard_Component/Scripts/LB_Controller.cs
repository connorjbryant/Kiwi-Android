using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LB_Controller : MonoBehaviour
{
    [SerializeField] GameObject leaderboardStoreScore;
    [SerializeField] GameObject changeUsername;
    [SerializeField] string API_KEY;
    [SerializeField] int boardID;
   
    private LB_Entry[] leaderboardEntries = new LB_Entry[0];

    public static LB_Controller instance;

    public delegate void OnAllScoresUpdated(LB_Entry[] entries);
    public static OnAllScoresUpdated OnUpdatedScores;

    public delegate void OnUsernameChanged(LB_ChangeUsernameResult result, LB_Entry[] entries);
    public static OnUsernameChanged OnUsernameChangedFinished;

    private void Awake() {
        if (instance == null) {
            instance = this;
            if (instance == null) {
                instance = new LB_Controller();
            }
            instance.ReloadLeaderboard();
        } else if (instance != null) {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start() {
        DontDestroyOnLoad(this.gameObject);
    }

    public void StoreScore(float score, string username) {
        GameObject lbInstance = Instantiate(leaderboardStoreScore, new Vector3(0, 0, 0), Quaternion.identity);
        LB_StoreScore storeScore = lbInstance.GetComponent<LB_StoreScore>();
        storeScore.StoreScore(score, username, boardID, API_KEY);
    }

    public void ChangeUsername(string oldName, string newName) {
        GameObject lbInstance = Instantiate(changeUsername, new Vector3(0, 0, 0), Quaternion.identity);
        LB_ChangeUsername changeusername = lbInstance.GetComponent<LB_ChangeUsername>();
        LB_ChangeUsername.OnFinishedDelegate += OnChangeUsernameFinished;
        changeusername.ChangeUsername(oldName, newName, API_KEY, boardID);
    }

    private void OnChangeUsernameFinished(LB_ChangeUsernameResult result, LB_Entry[] entries) {
        if (result == LB_ChangeUsernameResult.OK) {
            leaderboardEntries = entries;
        }
        LB_ChangeUsername.OnFinishedDelegate -= OnChangeUsernameFinished;
        OnUsernameChangedFinished?.Invoke(result, leaderboardEntries);
    }

    public void ReloadLeaderboard() {
        LB_GetAllScores request = gameObject.GetComponent<LB_GetAllScores>();
        LB_GetAllScores.OnFinishedDelegate += OnRequestFinished;
        request.GetAllScores(boardID, API_KEY); 
    }

    private void OnRequestFinished(LB_Entry[] entries) {
        leaderboardEntries = entries; 
        LB_GetAllScores.OnFinishedDelegate -= OnRequestFinished;
        OnUpdatedScores?.Invoke(leaderboardEntries); 
    }

    public int GetRankForUser(string username) {
        int rank = 0;
        foreach (LB_Entry entry in leaderboardEntries) {
            if (entry.name == username) {
                rank = entry.rank;
            }
        }

        return rank;
    }

    public LB_Entry[] Entries() {
        return leaderboardEntries; 
    }

    private void OnDestroy() {
        
    }
}
