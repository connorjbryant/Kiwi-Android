using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public enum LB_ChangeUsernameResult {
	OK = 0,
	USER_NOT_FOUND = -12,
	USERNAME_ALREADY_TAKEN = -13
}

public class LB_ChangeUsername : MonoBehaviour {
    

	private string API_KEY;
	private int m_boardid;
	private string m_old_username;
	private string m_new_username;

	public delegate void OnChangeUsernameFinishedDelegate(LB_ChangeUsernameResult result, LB_Entry[] entries);
	public static OnChangeUsernameFinishedDelegate OnFinishedDelegate;

	private void Awake() {
		DontDestroyOnLoad(gameObject);
	}

	public void ChangeUsername(string oldName, string newName, string KEY, int boardid) {
		API_KEY = KEY;
		m_boardid = boardid;
		m_old_username = oldName;
		m_new_username = newName;
		StartCoroutine(WaitForRequest());
	}

	private IEnumerator WaitForRequest() {

		string URL = "https://apps.mw-systems.com/api/v1/leaderboards/changeusername";

		WWWForm form = new WWWForm();
		form.AddField("api_key", API_KEY);
		form.AddField("board_id", m_boardid);
		form.AddField("new_name", m_new_username);
		form.AddField("old_name", m_old_username);
		

		UnityWebRequest www = UnityWebRequest.Post(URL, form);
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError) {
			StartCoroutine(Queue());
		} else {

			string result = www.downloadHandler.text;
			RequestStatus requestResult = JsonUtility.FromJson<RequestStatus>(result);

			LB_ChangeUsernameResult status = LB_ChangeUsernameResult.OK;
			if (requestResult.status.code != 0) {
				Debug.LogWarning(requestResult.status.msg);
				status = (LB_ChangeUsernameResult)requestResult.status.code;
				OnFinishedDelegate?.Invoke(status, null);
			} else {
				LeaderboardResult res = JsonUtility.FromJson<LeaderboardResult>(result);
				OnFinishedDelegate?.Invoke(status, res.entries);
			}

			Destroy(gameObject);
		}
	}

	IEnumerator Queue() {
		yield return new WaitForSeconds(5);
		ChangeUsername(m_old_username, m_new_username, API_KEY, m_boardid);
	}

}
