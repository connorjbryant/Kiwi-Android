using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class LB_GetAllScores : MonoBehaviour
{
	public delegate void OnGetAllScoresFinishedDelegate(LB_Entry[] entries);
	public static OnGetAllScoresFinishedDelegate OnFinishedDelegate;

	private string API_KEY;
	private int m_boardid;

	private void Awake() {
		DontDestroyOnLoad(gameObject);
	}

	public void GetAllScores(int boardid, string KEY) {
		API_KEY = KEY;
		m_boardid = boardid;
		StartCoroutine(WaitForRequest(boardid));
	}

	private IEnumerator WaitForRequest(int boardid) {

		string URL = "https://apps.mw-systems.com/api/v1/leaderboards/getallscores";

		WWWForm form = new WWWForm();
		form.AddField("api_key", API_KEY);
		form.AddField("board_id", boardid);
		
		UnityWebRequest www = UnityWebRequest.Post(URL, form);
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError) {
			StartCoroutine(Queue());
		} else {
			string result = www.downloadHandler.text;
			RequestStatus requestResult = JsonUtility.FromJson<RequestStatus>(result);
			
			
			if (requestResult.status.code != 0) {
				Debug.LogWarning(requestResult.status.msg);
				OnFinishedDelegate?.Invoke(null);
			} else {
				LeaderboardResult res = JsonUtility.FromJson<LeaderboardResult>(result);
				OnFinishedDelegate?.Invoke(res.entries);
			}

            
		}
	}

	IEnumerator Queue() {
		yield return new WaitForSeconds(5);
		GetAllScores(m_boardid, API_KEY);
	}
}
