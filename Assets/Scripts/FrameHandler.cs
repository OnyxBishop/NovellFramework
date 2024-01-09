using UnityEngine;
using UnityEngine.UI;

public class FrameHandler : MonoBehaviour
{
    [SerializeField] private SceneSwitcher _sceneSwitcher;
    [SerializeField] private TextWritter _textWritter;
    [SerializeField] private Button _skipButton;

    private void Awake()
    {
        _textWritter.Render();
    }

    private void OnEnable()
    {
        _skipButton.onClick.AddListener(OnSkipClicked);
        _textWritter.LinesOut += OnTextOut;
    }

    private void OnDisable()
    {
        _skipButton.onClick.RemoveListener(OnSkipClicked);
        _textWritter.LinesOut -= OnTextOut;
    }

    private void OnSkipClicked()
    {
        _textWritter.Clear();
        _textWritter.Render();
    }

    private void OnTextOut()
    {
        _skipButton.enabled = false;
        _sceneSwitcher.SetNext();
    }
}
