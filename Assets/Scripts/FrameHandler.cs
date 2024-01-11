using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FrameHandler : MonoBehaviour
{
    [Header("Ссылки на компоненты/объекты")]
    [SerializeField] private TextWritter _textWritter;
    [SerializeField] private Text _characterNameField;
    [SerializeField] private Image _backImage;
    [SerializeField] private Image _leftCharacterFrame;
    [SerializeField] private Image _rightCharacterFrame;
    [SerializeField] private Button _skipButton;

    [Header("Настройка кадров")]
    [SerializeField] private FrameInfo[] _frameInfo;

    public event Action Ended;

    private Character _leftCharacter;
    private Character _rightCharacter;
    private byte _currentFrameIndex = 0;
    private byte _currentLineIndex = 0;

    private void Awake()
    {
        _backImage.sprite = _frameInfo[_currentFrameIndex].BackgroundImage;
        _textWritter.Render(_frameInfo[_currentFrameIndex].Lines[_currentLineIndex]);
    }

    private void OnEnable()
    {
        _skipButton.onClick.AddListener(OnSkipClicked);
    }

    private void OnDisable()
    {
        _skipButton.onClick.RemoveListener(OnSkipClicked);
    }

    private void OnSkipClicked()
    {
        if (CheckWhichCharacterSpreak(out Character character) == true)
        {
            if (character.CharactersSide == CharactersSide.Left)
            {
                _leftCharacter = character;
                _leftCharacterFrame.sprite = _leftCharacter.Image;
                _characterNameField.text = _leftCharacter.Name;

                _leftCharacter.Speak();
                _rightCharacter?.Mute();
            }
            else
            {
                _rightCharacter = character;
                _leftCharacterFrame.sprite = _leftCharacter.Image;
                _characterNameField.text = _leftCharacter.Name;

                _rightCharacter.Speak();
                _leftCharacter?.Mute();
            }
        }

        if (_textWritter.IsCompleteTyped() == true)
        {
            _currentLineIndex++;

            if (_currentLineIndex < _frameInfo[_currentFrameIndex].Lines.Count)
            {
                _textWritter.Render(_frameInfo[_currentFrameIndex].Lines[_currentLineIndex]);
            }
            else
            {
                if (_currentFrameIndex < _frameInfo.Length - 1)
                {
                    ChangeFrame();
                }
                else
                {
                    Ended?.Invoke();
                }
            }
        }
        else
        {
            _textWritter.Complete();
        }
    }

    private bool CheckWhichCharacterSpreak(out Character character)
    {
        for (int i = 0; i < _frameInfo[_currentFrameIndex]._leftCharacterLines.Count; i++)
        {
            if (_currentLineIndex == _frameInfo[_currentFrameIndex]._leftCharacterLines[i])
            {
                character = _frameInfo[_currentFrameIndex].Characters
                    .FirstOrDefault(person => person.CharactersSide == CharactersSide.Left);
                return character != null;
            }
        }

        for (int i = 0; i < _frameInfo[_currentFrameIndex]._rightCharacterLines.Count; i++)
        {
            if (_currentLineIndex == _frameInfo[_currentFrameIndex]._rightCharacterLines[i])
            {
                character = _frameInfo[_currentFrameIndex].Characters
                    .FirstOrDefault(person => person.CharactersSide == CharactersSide.Right);
                return character != null;
            }
        }

        character = null;
        return false;
    }

    private void ChangeFrame()
    {
        _currentFrameIndex++;
        _currentLineIndex = 0;

        _backImage.sprite = _frameInfo[_currentFrameIndex].BackgroundImage;
        _textWritter.Render(_frameInfo[_currentFrameIndex].Lines[_currentLineIndex]);
    }
}

[Serializable]
public class FrameInfo
{
    public Sprite BackgroundImage;
    public List<Character> Characters;
    public List<string> Lines;
    public List<int> _leftCharacterLines;
    public List<int> _rightCharacterLines;

}
