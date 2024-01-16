using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _image;
    [SerializeField] private bool _isLeftSide;
    [SerializeField] private CharactersSide _charactersSide;

    private Vector3 _originalScale = new Vector3(1, 1, 1);
    private Vector3 _smallerSize = new Vector3(0.8f, 0.8f, 0.8f);
    private Color _fadeColor = new Color(0.5f, 0.5f, 0.5f);
    private Color _defaultColor = new Color(1, 1, 1);

    public string Name => _name;
    public Sprite Image => _image;
    public CharactersSide CharactersSide => _charactersSide;

    private Image _imageFrame;

    public void Init(Image imageFrame)
    {
        if (_imageFrame == null)
            _imageFrame = imageFrame;
    }

    public void Speak()
    {
        _imageFrame.DOColor(_defaultColor, 0.5f);
        _imageFrame.rectTransform.DOScale(_originalScale, 1f);
    }

    public void Mute()
    {
        _imageFrame.DOColor(_fadeColor, 0.5f);
        _imageFrame.rectTransform.DOScale(_smallerSize, 1f);
    }
}