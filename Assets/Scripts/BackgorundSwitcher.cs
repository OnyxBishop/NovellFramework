using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgorundSwitcher : MonoBehaviour
{
    [SerializeField] private Image _imageFrame;
    [SerializeField] private List<Sprite> _sprites;

    private void Awake()
    {
        _imageFrame.sprite = _sprites[0];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _imageFrame.sprite = _sprites[1];
        }
    }
}
