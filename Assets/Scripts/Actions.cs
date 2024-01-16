using System;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    private ChooseButton[] _buttons;

    public event Action<byte> ButtonClicked;

    private void Awake()
    {
        _buttons = GetComponentsInChildren<ChooseButton>();

        foreach (ChooseButton button in _buttons)
        {
            button.Choosed += OnChoosed;
            button.Disable();
        }
    }

    public void Init(IReadOnlyList<byte> frameIndexes)
    {
        byte i = 0;

        foreach (var button in _buttons)
        {
            button.Init(frameIndexes[i]);
            i++;
        }
    }

    public void EnableChoosing(IReadOnlyList<string> names)
    {
        byte i = 0;

        foreach (var button in _buttons)
        {
            button.Render(names[i]);
            button.Enable();
            i++;
        }
    }

    private void OnChoosed(byte nextFrameIndex)
    {
        foreach (var button in _buttons)
            button.Disable();

        ButtonClicked?.Invoke(nextFrameIndex);
    }
}
