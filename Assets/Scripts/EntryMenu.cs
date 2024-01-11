using UnityEngine;
using UnityEngine.UI;

public class EntryMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _optionsButton;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnPlayClicked);
        _optionsButton.onClick.AddListener(OnPauseClicked);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlayClicked);
        _optionsButton.onClick.RemoveListener(OnPauseClicked);
    }

    private void OnPlayClicked()
    {
        
    }

    private void OnPauseClicked()
    {

    }
}
