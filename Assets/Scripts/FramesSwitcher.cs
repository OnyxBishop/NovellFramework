using UnityEngine;
using UnityEngine.UI;

public class FramesSwitcher : MonoBehaviour
{
    [SerializeField] private Image _leftFrame;
    [SerializeField] private Image _rightFrame;

    public void Enable()
    {
        _leftFrame.gameObject.SetActive(true);
        _rightFrame.gameObject.SetActive(true);
    }

    public void Disable()
    {
        _leftFrame.gameObject.SetActive(false);
        _rightFrame.gameObject.SetActive(false);
    }
}
