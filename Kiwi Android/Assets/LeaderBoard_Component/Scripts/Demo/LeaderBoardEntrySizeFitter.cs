using UnityEngine;
using UnityEngine.UI;
 

public class LeaderBoardEntrySizeFitter : MonoBehaviour {
    private RectTransform _rectTransform, _parentRectTransform;
    private VerticalLayoutGroup verticalLayoutGroup;

    void Start() {
        UpdateWidth();
    }

    void OnRectTransformDimensionsChange() {
        //UpdateWidth();
    }

    private void Update() {
        
    }

    public void UpdateWidth() {
        if (verticalLayoutGroup == null || _rectTransform == null || _parentRectTransform == null) {
            verticalLayoutGroup = GetComponentInParent<VerticalLayoutGroup>();
            if (verticalLayoutGroup != null) {
                _parentRectTransform = verticalLayoutGroup.GetComponentInParent<RectTransform>();
                _rectTransform = GetComponent<RectTransform>();
                _rectTransform.pivot = new Vector2(0, 1);
                _rectTransform.sizeDelta = new Vector2(Screen.width - (verticalLayoutGroup.padding.left + verticalLayoutGroup.padding.right), _rectTransform.sizeDelta.y);
            }
        } else {
            _rectTransform.sizeDelta = new Vector2(Screen.width - (verticalLayoutGroup.padding.left + verticalLayoutGroup.padding.right), _rectTransform.sizeDelta.y);
        }
    }
}