using UnityEngine;
using UnityEngine.UI;
public class SnackAudio : MonoBehaviour, SnackSound
{
    [SerializeField] AudioSource effects;
    [SerializeField] AudioSource chewing;
    [SerializeField] AudioClip unwrapClip;
    [SerializeField] GameObject caption;
    [SerializeField] Text captionText;
    [SerializeField] string unwrapCaption = "Opening the bag...";
    [SerializeField] string chewCaption = "Chewing...";
    public void PlayUnwrap()
    {
        effects.PlayOneShot(unwrapClip);
        ShowCaption(unwrapCaption);
        CancelInvoke(nameof(AfterUnwrap));
        Invoke(nameof(AfterUnwrap), unwrapClip.length);
    }
    public void StartChewing()
    {
        if (!chewing.isPlaying) chewing.Play();
        ShowCaption(chewCaption);
    }
    public void StopChewing()
    {
        chewing.Stop();
        if (captionText.text == chewCaption) caption.SetActive(false);
    }
    void AfterUnwrap()
    {
        if (chewing.isPlaying)
            ShowCaption(chewCaption);
        else
            caption.SetActive(false);
    }
    void ShowCaption(string text)
    {
        captionText.text = text;
        caption.SetActive(true);
    }
}
