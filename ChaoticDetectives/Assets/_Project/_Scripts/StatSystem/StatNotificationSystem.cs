using System.Collections;
using TMPro;
using UnityEngine;

public class StatNotificationSystem : MonoBehaviour {
    [SerializeField] private float _notificationTime = 3f;
    [SerializeField] private GameObject _notificationObject;
    
    private TextMeshProUGUI _notificationText;

    private void Awake() {
        _notificationText = _notificationObject.GetComponentInChildren<TextMeshProUGUI>();
        _notificationObject.SetActive(false);
    }

    private void OnEnable() {
        StatSystem.OnStatModfied += NotifyOfStatChange;
    }

    private void OnDisable() {
        StatSystem.OnStatModfied -= NotifyOfStatChange;
    }

    private void NotifyOfStatChange(Stat obj) {
        _notificationText.text = $" +1 {obj.statType} ";
        _notificationObject.SetActive(true);

        StartCoroutine(HideNotification());
    }
    private IEnumerator HideNotification() {
        yield return new WaitForSeconds(_notificationTime);
        _notificationObject.SetActive(false);
    }
}
