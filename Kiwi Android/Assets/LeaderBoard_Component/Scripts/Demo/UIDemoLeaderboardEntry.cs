using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDemoLeaderboardEntry : MonoBehaviour
{
    [SerializeField] Text rankText;
    [SerializeField] Text playernameText;
    [SerializeField] Text scoreText;

    private RectTransform _parentRectTransform;

    private void Start() {
        StartCoroutine(SetupLayout());
    }

    public void Setup(LB_Entry entry) {
        rankText.text = "#" + entry.rank;
        playernameText.text = entry.name;
        scoreText.text = "" + entry.points;
        if (gameObject.activeInHierarchy) {
            StartCoroutine(SetupLayout());
        }
        
    }

    private IEnumerator SetupLayout() {
        yield return new WaitForSeconds(0.2f);
        LeaderBoardEntrySizeFitter sizeFitter = GetComponent<LeaderBoardEntrySizeFitter>();
        sizeFitter.UpdateWidth();
        _parentRectTransform = GetComponentInParent<RectTransform>();
        rankText.rectTransform.sizeDelta = new Vector2(_parentRectTransform.rect.size.x * 0.25f, rankText.rectTransform.sizeDelta.y);
        playernameText.rectTransform.sizeDelta = new Vector2(_parentRectTransform.rect.size.x * 0.50f, playernameText.rectTransform.sizeDelta.y);
        scoreText.rectTransform.sizeDelta = new Vector2(_parentRectTransform.rect.size.x * 0.25f, scoreText.rectTransform.sizeDelta.y);

        rankText.rectTransform.position = new Vector3(10, rankText.rectTransform.position.y, 0);
        playernameText.rectTransform.position = new Vector3(rankText.rectTransform.position.x + rankText.rectTransform.sizeDelta.x, playernameText.rectTransform.position.y, 0);
        scoreText.rectTransform.position = new Vector3(playernameText.rectTransform.position.x - 10 + playernameText.rectTransform.sizeDelta.x, scoreText.rectTransform.position.y, 0);
    }
}
