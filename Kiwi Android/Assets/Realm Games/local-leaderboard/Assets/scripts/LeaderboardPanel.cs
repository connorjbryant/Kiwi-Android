using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

namespace RealmGames.Leaderboard
{
    public enum ScoreFormat {
        NUMBER,
        CURRENCY,
        DECIMAL
    }

    public class LeaderboardPanel : MonoBehaviour {

        public GameObject prefab;
        public GameObject scrollViewContent;

        [Header("Score Display")]
        public SortOrder sortMode = SortOrder.HIGH_TO_LOW;
        public ScoreFormat format = ScoreFormat.NUMBER;
        public int numberOfScores = -1; //-1 = show all scores
        public bool refreshOnEnable = true;

		public void OnEnable()
		{
            if (refreshOnEnable)
                Refresh("default");
		}

		public void Refresh(string leaderboardID)
        {
            ClearItems();

            LeaderboardManager.Instance.LoadScores(leaderboardID, Callback, sortMode);
        }

        public void ClearItems()
        {
            List<GameObject> destroy = new List<GameObject>();

            int count = scrollViewContent.transform.childCount;

            for (int i = 0; i < count; i++)
            {
                GameObject obj = scrollViewContent.transform.GetChild(i).gameObject;

                if (obj == prefab) continue;

                destroy.Add(obj);
            }

            foreach (GameObject go in destroy)
            {
                DestroyImmediate(go);
            }
        }

        public void Callback(IScore[] scores)
        {
            int count = numberOfScores;

            if (numberOfScores == -1)
                count = scores.Length;

            for (int i = 0; i < count; i++)
            {
                IScore meta = scores[i];

                GameObject button = Instantiate(prefab);

                button.SetActive(true);

                foreach( Text text in button.GetComponentsInChildren<Text>())
                {
                    if(text.name.Equals("userID")) {
                        text.text = meta.userID;
                    }
                    else if (text.name.Equals("rank"))
                    {
                        text.text = meta.rank.ToString();
                    }
                    else if (text.name.Equals("date"))
                    {
                        text.text = meta.date.ToShortDateString();
                    }
                    else if (text.name.Equals("value"))
                    {
                        if(format == ScoreFormat.NUMBER)
                            text.text = string.Format("{0:N0}", meta.value);
                        else if (format == ScoreFormat.CURRENCY)
                            text.text = string.Format("{0:C}", meta.value);
                        else if (format == ScoreFormat.DECIMAL)
                            text.text = string.Format("{0:D}", meta.value);
                    }
                }

                button.transform.SetParent(scrollViewContent.transform);
                button.transform.localPosition = Vector3.zero;
                button.transform.localRotation = Quaternion.identity;
                button.GetComponent<Button>().onClick.AddListener(delegate {
                });
            }

            prefab.SetActive(false);
        }
    }
}