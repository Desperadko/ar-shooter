using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseConfirmationPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    private Action onConfirm;

    public void Show(string message, Action onConfirm)
    {
        questionText.text = message;
        this.onConfirm = onConfirm;

        gameObject.SetActive(true);

        confirmButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(Confirm);

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(Hide);
    }

    private void Confirm()
    {
        onConfirm?.Invoke();
        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
