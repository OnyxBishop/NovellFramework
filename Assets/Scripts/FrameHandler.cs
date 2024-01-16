using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextWritter))]
[RequireComponent(typeof(FramesSwitcher))]
public class FrameHandler : MonoBehaviour
{
    [Header("Ссылки на компоненты/объекты")]
    [SerializeField] private Image _backImage;
    [SerializeField] private Text _leftNameField;
    [SerializeField] private Text _rightNameField;
    [SerializeField] private Image _leftCharacterFrame;
    [SerializeField] private Image _rightCharacterFrame;
    [SerializeField] private Button _skipButton;

    private TextWritter _textWritter;
    private FramesSwitcher _framesSwitcher;

    [Header("Настройка кадров")]
    [SerializeField] private FrameInfo[] _frameInfo;

    public event Action Ended;

    private Character _leftCharacter;
    private Character _rightCharacter;
    private byte _currentFrameIndex = 0;
    private byte _currentLineIndex = 0;

    private void Awake()
    {
        _textWritter = GetComponent<TextWritter>();
        _framesSwitcher = GetComponent<FramesSwitcher>();

        _backImage.sprite = _frameInfo[_currentFrameIndex].BackgroundImage;
        _textWritter.Render(_frameInfo[_currentFrameIndex].Lines[_currentLineIndex]);

        if (_frameInfo[_currentFrameIndex].Characters == null)
        {
            _framesSwitcher.Disable();
        }
        else
        {
            if (CheckWhichCharacterSpeak(out _) == true)
                _framesSwitcher.Enable();
            else
                _framesSwitcher.Disable();
        }
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
        if (CheckWhichCharacterSpeak(out Character character) == true)
        {
            _framesSwitcher.Enable();

            if (character.CharactersSide == CharactersSide.Left)
            {
                _leftCharacter = character;
                _leftCharacter.Init(_leftCharacterFrame);

                _leftCharacterFrame.sprite = _leftCharacter.Image;
                _leftNameField.text = _leftCharacter.Name;

                _leftCharacter.Speak();
                _rightCharacter?.Mute();
            }
            else
            {
                _rightCharacter = character;
                _rightCharacter.Init(_rightCharacterFrame);

                _rightCharacterFrame.sprite = _rightCharacter.Image;
                _rightNameField.text = _rightCharacter.Name;

                _rightCharacter.Speak();
                _leftCharacter?.Mute();
            }
        }
        else
        {
            _framesSwitcher.Disable();
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

    private bool CheckWhichCharacterSpeak(out Character character)
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
