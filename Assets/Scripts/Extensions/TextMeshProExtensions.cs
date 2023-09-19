using TMPro;

public static class TextMeshProExtensions
{
    public static void UpdateText(this TMP_Text text, string message)
    {
        if (text.text != message)
        {
            text.text = message;
        }
    }
}
