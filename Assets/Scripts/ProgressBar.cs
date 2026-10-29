using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour, Progress
{
    [SerializeField, Range(0f, 100f)] float progress;
    [SerializeField, Range(0f, 100f)] float passPercent = 75f;
    [SerializeField] Image[] cells = new Image[12];
    [SerializeField] Image passMarker;
    [SerializeField] Color belowPassColor = new Color(0.86f, 0.27f, 0.23f);
    [SerializeField] Color passedColor = new Color(0.30f, 0.69f, 0.31f);
    [SerializeField] Text label;

    public float CurrentProgress => progress;

    public void SetProgress(float percent)
    {
        progress = Mathf.Clamp(percent, 0f, 100f);
        Apply();
    }

    void OnValidate() => Apply();

    void Apply()
    {
        float filledCells = progress / 100f * cells.Length;
        for (int i = 0; i < cells.Length; i++)
            if (cells[i] != null) cells[i].fillAmount = Mathf.Clamp01(filledCells - i);

        if (passMarker != null)
        {
            if (progress >= passPercent)
                passMarker.color = passedColor;
            else
                passMarker.color = belowPassColor;
            PlacePassMarker();
        }
        if (label != null) label.text = Mathf.FloorToInt(progress) + "%";
    }

    // Cell i is centred at first + i * pitch, so the boundary after k cells sits at first + (k - 0.5) * pitch.
    void PlacePassMarker()
    {
        if (cells.Length < 2 || cells[0] == null || cells[1] == null) return;

        float first = cells[0].rectTransform.anchoredPosition.x;
        float pitch = cells[1].rectTransform.anchoredPosition.x - first;
        float passCells = passPercent / 100f * cells.Length;
        var position = passMarker.rectTransform.anchoredPosition;
        position.x = first + (passCells - 0.5f) * pitch;
        passMarker.rectTransform.anchoredPosition = position;
    }
}
