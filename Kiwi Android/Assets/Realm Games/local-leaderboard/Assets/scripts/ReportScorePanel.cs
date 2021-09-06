using UnityEngine;
using UnityEngine.UI;

namespace RealmGames.Leaderboard
{
    public class ReportScorePanel : MonoBehaviour
    {
        public LeaderboardPanel leaderboard;
        public InputField nameField;
        public InputField scoreField;

        public void DeleteScores() {
            
            LeaderboardManager.Instance.DeleteScores("default");

            leaderboard.Refresh("default");
        }

        public void ReportScore() {

            string leaderboardId = "default";
            string userID = nameField.text;
            long score = PlayerPrefs.GetInt("HighScore");

            LeaderboardManager.Instance.ReportScore(score, leaderboardId, userID, SortOrder.HIGH_TO_LOW, (bool success) =>
            {
                Debug.Log("Score Reported: " + success);

                if (success)
                {
                    leaderboard.Refresh("default");
                }
            });
        }
    }
}