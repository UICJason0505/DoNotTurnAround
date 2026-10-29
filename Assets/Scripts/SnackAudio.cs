using UnityEngine;
using UnityEngine.UI;
public class SnackAudio : MonoBehaviour, SnackSound
{
    enum CaptionType { None, Unwrap, Chewing }
    [SerializeField] AudioSource effects;
    [SerializeField] AudioSource chewing;
    [SerializeField] AudioClip unwrapClip;
    [SerializeField] GameObject caption;
    [SerializeField] Text captionText;
    [SerializeField] string unwrapCaption = "Opening the bag...";
    [SerializeField] string chewCaption = "Chewing...";
    [SerializeField] CaptionType shownCaption = CaptionType.None;
    public void PlayUnwrap()
    {
        effects.PlayOneShot(unwrapClip);
        ShowCaption(CaptionType.Unwrap);
        CancelInvoke(nameof(AfterUnwrap));
        Invoke(nameof(AfterUnwrap), unwrapClip.length);
    }
    public void StartChewing()
    {
        if (!chewing.isPlaying) chewing.Play();
        ShowCaption(CaptionType.Chewing);
    }
    public void StopChewing()
    {
        chewing.Stop();
        if (shownCaption == CaptionType.Chewing) ShowCaption(CaptionType.None);
    }
    void AfterUnwrap()
    {
        if (chewing.isPlaying)
            ShowCaption(CaptionType.Chewing);
        else
            ShowCaption(CaptionType.None);
    }
    void OnValidate()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.delayCall -= ApplyAfterValidate;
        UnityEditor.EditorApplication.delayCall += ApplyAfterValidate;
#endif
    }
#if UNITY_EDITOR
    void ApplyAfterValidate()
    {
        if (this != null) ShowCaption(shownCaption);
    }
#endif
    void ShowCaption(CaptionType type)
    {
        shownCaption = type;
        if (caption == null || captionText == null) return;
        if (type == CaptionType.Unwrap)
            captionText.text = unwrapCaption;
        else if (type == CaptionType.Chewing)
            captionText.text = chewCaption;
        else
            captionText.text = "";
        caption.SetActive(type != CaptionType.None);
    }
}
