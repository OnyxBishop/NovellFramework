using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class TextWritter : MonoBehaviour
{
    [SerializeField] private Text _textView;
    [SerializeField] private List<string> _lines;
    [SerializeField] private float _animationDuration = 12f;

    private int _index = 0;
    private Tween _animTween;

    public event Action LinesOut;

    public void Render()
    {
        if (_index >= _lines.Count)
        {
            LinesOut?.Invoke();
            _animTween.Kill();
            return;
        }

        if (_animTween != null && _animTween.IsActive())
        {
            _animTween.Complete();
        }
        else
        {
            _animTween = _textView.DOText(_lines[_index], _animationDuration);
            _index++;
        }
    }

    public void Clear()
    {
        _textView.text = string.Empty;
    }
}
