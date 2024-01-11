using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class Character : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _image;
    [SerializeField] private bool _isLeftSide;
    [SerializeField] private CharactersSide _charactersSide;

    private Vector3 _originalScale;
    private Color _fadeColor = new Color(0.5f, 0.5f, 0.5f);
    private Color _defaultColor = new Color(1, 1, 1);

    public string Name => _name;
    public Sprite Image => _image;
    public CharactersSide CharactersSide => _charactersSide;

    private SpriteRenderer _renderer;

    private void OnValidate()
    {
        _renderer = GetComponent<SpriteRenderer>();

        _renderer.flipX = _isLeftSide;
    }

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = _image;
        _originalScale = transform.localScale;
    }

    public void Speak()
    {
        _renderer.DOColor(_defaultColor, 0.5f);
        transform.DOScale(_originalScale, 1f);
    }

    public void Mute()
    {
        _renderer.DOColor(_fadeColor, 0.5f);
        transform.DOScale(_originalScale * 0.8f, 1f);
    }
}