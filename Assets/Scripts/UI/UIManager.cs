using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI")]
    public Button fireButton;

    PlayerAttack localPlayerAttack;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RegisterLocalPlayer(PlayerAttack attack)
    {
        localPlayerAttack = attack;

        fireButton.onClick.RemoveAllListeners();
        fireButton.onClick.AddListener(OnFireClicked);
    }

    void OnFireClicked()
    {
        if (localPlayerAttack != null)
            localPlayerAttack.Fire();
    }
}
