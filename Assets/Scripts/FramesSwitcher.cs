using UnityEngine;
using UnityEngine.UI;

public class FramesSwitcher : MonoBehaviour
{
    [SerializeField] private Image _leftFrame;
    [SerializeField] private Image _rightFrame;

    public void EnableSide(CharactersSide side)
    {
        if (side == CharactersSide.Left)
        {
            _leftFrame.gameObject.SetActive(true);
        }

        if (side == CharactersSide.Right)
        {
            _rightFrame.gameObject.SetActive(true);
        }
    }

    public void Disable()
    {
        _leftFrame.gameObject.SetActive(false);
        _rightFrame.gameObject.SetActive(false);
    }

    public void EnableBoth()
    {
        _leftFrame.gameObject.SetActive(true);
        _rightFrame.gameObject.SetActive(true);
    }
}
