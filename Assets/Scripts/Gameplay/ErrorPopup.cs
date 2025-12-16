using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ErrorPopup : MonoBehaviour
{
    public static ErrorPopup Instance;

    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Button retryButton;

    Action retryAction;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        panel.SetActive(false);
    }

    public void Show(string message, Action onRetry)
    {
        retryAction = onRetry;
        messageText.text = message;
        panel.SetActive(true);
    }

    public void OnRetryClicked()
    {
        panel.SetActive(false);
        retryAction?.Invoke();
    }
}
