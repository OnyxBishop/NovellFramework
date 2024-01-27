using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Frame", menuName = "CreateFrameDTO", order = 51)]
public class FrameInfo : ScriptableObject
{
    public Sprite BackgroundImage;
    public List<Character> Characters;
    public List<string> Lines;
    public List<int> LeftCharacterLines;
    public List<int> RightCharacterLines;
    public ActionsInfo ActionsInfo;
    public int ButtonsLine;
}
