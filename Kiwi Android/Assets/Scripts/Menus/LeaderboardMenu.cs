using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardMenu : MonoBehaviour
{
    public GameObject leaderboardMenu;

    public void ShowLeaderboard()
    {
        leaderboardMenu.SetActive(true);
    }

    public void HideLeaderboard()
    {
        leaderboardMenu.SetActive(false);
    }
}
