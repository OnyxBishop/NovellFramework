using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _textField;

    private byte _nextFrameIndex;

    private Button _button;

    public event Action<byte> Choosed;

    public void Init(byte nextFrameIndex)
    {
        _nextFrameIndex = nextFrameIndex;
    }

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    public void Render(string text)
    {
        _textField.text = text;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnClick()
    {
        Choosed?.Invoke(_nextFrameIndex);
    }
}
