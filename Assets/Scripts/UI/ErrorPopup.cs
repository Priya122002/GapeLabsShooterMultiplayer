using UnityEngine;
using UnityEngine.UI;
using System;

namespace UI   
{
    public class ErrorPopup : MonoBehaviour
    {
        public static ErrorPopup Instance;

        public Button retryButton;

        void Awake()
        {
            Instance = this;
            gameObject.SetActive(false);
        }

        public void ShowRetry(Action retryAction)
        {
            gameObject.SetActive(true);

            retryButton.onClick.RemoveAllListeners();
            retryButton.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                retryAction?.Invoke();
            });
        }
    }
}
