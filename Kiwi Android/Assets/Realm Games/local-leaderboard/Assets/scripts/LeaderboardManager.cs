using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;

namespace RealmGames.Leaderboard
{
    public enum SortOrder {
        HIGH_TO_LOW,
        LOW_TO_HIGH
    }

    public class LeaderboardManager : Manager<LeaderboardManager>
    {
        public void DeleteScores(string leaderboardId) {

            if (string.IsNullOrEmpty(leaderboardId))
                return;

            PlayerPrefs.SetInt(leaderboardId + ".users.count", 0);
        }

        public void ReportScore(long score, string leaderboardId, string userId, SortOrder mode, Action<bool> callback)
        {
            if(string.IsNullOrEmpty(leaderboardId) ||
               string.IsNullOrEmpty(userId))
            {                
                if (callback != null)
                    callback(false);

                return;
            }

            List<Score> scores = LoadLocalScores(leaderboardId, mode);

            bool user_found = false;

            foreach (Score s in scores)
            {
                if (s.userID != null &&
                    s.userID.Equals(userId))
                {

                    if(mode == SortOrder.HIGH_TO_LOW)
                    {
                        if (s.value < score)
                        {
                            s.value = score;
                        }
                    } else {
                        if (s.value > score)
                        {
                            s.value = score;
                        }
                    }

                    user_found = true;

                    break;
                }
            }

            if (!user_found)
                scores.Add(new Score(leaderboardId, score, userId, DateTime.Now, score.ToString(), 0));

            PlayerPrefs.SetInt(leaderboardId + ".users.count", scores.Count);

            for (int i = 0; i < scores.Count; i++)
            {
                Score s = scores[i];

                PlayerPrefs.SetString(leaderboardId + ".user" + i + ".id", s.userID);
                PlayerPrefs.SetInt(leaderboardId + ".user" + i + ".score", (int)s.value);
                PlayerPrefs.SetString(leaderboardId + ".user" + i + ".date", s.date.ToString());
            }

            if (callback != null)
                callback(true);
        }

        public void LoadScores(string leaderboardId, Action<IScore[]> callback, SortOrder mode)
        {
            List<Score> scores = LoadLocalScores(leaderboardId, mode);

            if (callback != null)
                callback(scores.ToArray());
        }

        private List<Score> LoadLocalScores(string leaderboardId, SortOrder mode)
        {
            int user_count = PlayerPrefs.GetInt(leaderboardId + ".users.count", 0);

            List<Score> scores = new List<Score>();

            for (int i = 0; i < user_count; i++)
            {
                string userID = PlayerPrefs.GetString(leaderboardId + ".user" + i + ".id", "none");
                int score = PlayerPrefs.GetInt(leaderboardId + ".user" + i + ".score", 0);
                //PlayfabManager.SendLeaderboard(score);
                string date = PlayerPrefs.GetString(leaderboardId + ".user" + i + ".date", DateTime.Now.ToString());
                scores.Add(new Score(leaderboardId, (long)score, userID, DateTime.Parse(date), score.ToString(), 0));
            }

            SortScores(scores, mode);

            int rank = 1;
            foreach (Score score in scores)
            {
                score.SetRank(rank++);
            }

            return scores;
        }

        private void SortScores(List<Score> scores, SortOrder mode) {
            if (mode == SortOrder.HIGH_TO_LOW)
                scores.Sort(SortDescending);
            else
                scores.Sort(SortAscending);
        }

        private int SortAscending(Score a, Score b)
        {
            if (a.value < b.value)
                return -1;
            else if (a.value > b.value)
                return 1;
            else
                return 0;
        }

        private int SortDescending(Score a, Score b) {
            if (a.value < b.value)
                return 1;
            else if (a.value > b.value)
                return -1;
            else
                return 0;
        }
    }
}