using UnityEngine;

[CreateAssetMenu(fileName = "NewSubtitle", menuName = "Subtitles/Subtitle Data")]
public class SubtitleData : ScriptableObject
{
    public SubtitleLine[] lines;
}
