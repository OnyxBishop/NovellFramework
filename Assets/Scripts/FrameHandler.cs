using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextWritter))]
[RequireComponent(typeof(FramesSwitcher))]
public class FrameHandler : MonoBehaviour
{
    [Header("Ссылки на компоненты/объекты")]
    [SerializeField] private Image _backImage;
    [SerializeField] private TMP_Text _leftNameField;
    [SerializeField] private TMP_Text _rightNameField;
    [SerializeField] private Image _leftCharacterFrame;
    [SerializeField] private Image _rightCharacterFrame;
    [SerializeField] private Button _skipButton;

    [Header("Кнопки выбора ответов")]
    [SerializeField] private Actions _actions;

    private TextWritter _textWritter;
    private FramesSwitcher _framesSwitcher;

    [Header("Настройка кадров")]
    [SerializeField] private FrameInfo[] _frameInfo;

    [Header("Проверка кадра - указать индекс")]
    [SerializeField] private bool _isCheck;
    [SerializeField] private byte _checkIndex;

    public event Action Ended;

    private Character _leftCharacter;
    private Character _rightCharacter;
    private byte _currentFrameIndex = 0;
    private byte _currentLineIndex = 0;

    private void Awake()
    {
        if (_isCheck == true)
        {
            _currentFrameIndex = _checkIndex;
        }

        _textWritter = GetComponent<TextWritter>();
        _framesSwitcher = GetComponent<FramesSwitcher>();

        _backImage.sprite = _frameInfo[_currentFrameIndex].BackgroundImage;
        _textWritter.Render(_frameInfo[_currentFrameIndex].Lines[_currentLineIndex]);
        TryEnableCharacters();

        if (_frameInfo[_currentFrameIndex].Characters == null)
        {
            _framesSwitcher.Disable();
        }
        else
        {
            if (CheckWhichCharacterSpeak(out Character character) == true)
                _framesSwitcher.EnableSide(character.CharactersSide);
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
        if (_textWritter.IsCompleteTyping() == true)
        {
            _currentLineIndex++;

            if (_currentLineIndex == _frameInfo[_currentFrameIndex].ButtonsLine)
            {
                _actions.Init(_frameInfo[_currentFrameIndex].ActionsInfo.FrameIndexes);
                _actions.EnableChoosing(_frameInfo[_currentFrameIndex].ActionsInfo.Names);
                _actions.ButtonClicked += SetFrameOnClick;
                _skipButton.gameObject.SetActive(false);
            }

            if (_currentLineIndex < _frameInfo[_currentFrameIndex].Lines.Count)
            {
                _textWritter.Render(_frameInfo[_currentFrameIndex].Lines[_currentLineIndex]);
                TryEnableCharacters();
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
        for (int i = 0; i < _frameInfo[_currentFrameIndex].LeftCharacterLines.Count; i++)
        {
            if (_currentLineIndex == _frameInfo[_currentFrameIndex].LeftCharacterLines[i])
            {
                character = _frameInfo[_currentFrameIndex].Characters
                    .FirstOrDefault(person => person.CharactersSide == CharactersSide.Left);
                return character != null;
            }
        }

        for (int i = 0; i < _frameInfo[_currentFrameIndex].RightCharacterLines.Count; i++)
        {
            if (_currentLineIndex == _frameInfo[_currentFrameIndex].RightCharacterLines[i])
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
        TryEnableCharacters();
    }

    private void SetFrameOnClick(byte nextFrameIndex)
    {
        _actions.ButtonClicked -= SetFrameOnClick;
        _skipButton.gameObject.SetActive(true);

        _currentFrameIndex = nextFrameIndex;
        _currentLineIndex = 0;

        _backImage.sprite = _frameInfo[_currentFrameIndex].BackgroundImage;
        _textWritter.Render(_frameInfo[_currentFrameIndex].Lines[_currentLineIndex]);
        TryEnableCharacters();
    }

    private void TryEnableCharacters()
    {
        if (CheckWhichCharacterSpeak(out Character character) == true)
        {
            _framesSwitcher.EnableSide(character.CharactersSide);

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
    }
}

[Serializable]
public class ActionsInfo
{
    public List<string> Labels;
    public List<byte> NextFrameIndex;

    public IReadOnlyList<string> Names => Labels;
    public IReadOnlyList<byte> FrameIndexes => NextFrameIndex;
}