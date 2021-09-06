using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class LB_StoreScore : MonoBehaviour
{

	private string API_KEY;
	private string m_username;
	private float m_score;
	private int m_boardid;

	private void Awake() {
        DontDestroyOnLoad(gameObject); 
    }

	public void StoreScore(float score, string username, int boardid, string KEY) {
		API_KEY = KEY;
		m_username = username;
		m_boardid = boardid;
		m_score = score; 
		StartCoroutine(WaitForRequest(score, username, boardid));
	}

	private IEnumerator WaitForRequest(float score, string username, int boardid) {

		string URL = "https://apps.mw-systems.com/api/v1/leaderboards/storescore";

		WWWForm form = new WWWForm();
		form.AddField("api_key", API_KEY);
		form.AddField("score", score.ToString());
		form.AddField("username", username);
		form.AddField("board_id", boardid);

		UnityWebRequest www = UnityWebRequest.Post(URL, form);
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError) {
			StartCoroutine(Queue());
		} else {
			Destroy(gameObject);
		}
	}

	IEnumerator Queue() {
		yield return new WaitForSeconds(5);
		StoreScore(m_score, m_username, m_boardid, API_KEY);
	}
}
