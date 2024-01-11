using UnityEngine;
using UnityEngine.UI;

public class EndGameHandler : MonoBehaviour
{
    [SerializeField] private FrameHandler _frameHandler;
    [SerializeField] private Image _image;
    [SerializeField] private TextWritter _textWritter;
    [SerializeField] private Sprite _finishSprite;
    [SerializeField] private string _finishText;

    private void OnEnable()
    {
        _frameHandler.Ended += OnFramesEnded;
    }

    private void OnDisable()
    {
        _frameHandler.Ended -= OnFramesEnded;
    }

    private void OnFramesEnded()
    {
        _frameHandler.enabled = false;
        _image.sprite = _finishSprite;
        _textWritter.Render(_finishText);
    }
}
