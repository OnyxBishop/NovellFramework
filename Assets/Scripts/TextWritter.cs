using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextWritter : MonoBehaviour
{
    [SerializeField] private Text _textView;

    private WaitForSeconds _sleepTime = new(0.2f);
    private Coroutine _typeCoroutine;
    private string _typingLine;

    public void Render(string line)
    {
        _textView.text = string.Empty;
        _typingLine = line;

        _typeCoroutine = StartCoroutine(SmoothWrite(line));
    }

    public void Complete()
    {
        if (_typeCoroutine != null)
        {
            StopCoroutine(_typeCoroutine);
            _typeCoroutine = null;
        }

        _textView.text = _typingLine;
        _typingLine = null;
    }

    public bool IsCompleteTyping()
    {
        return _typeCoroutine == null;
    }

    private IEnumerator SmoothWrite(string line)
    {
        foreach (char abc in line)
        {
            _textView.text += abc;
            yield return _sleepTime;
        }

        Complete();
    }
}
