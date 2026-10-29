using UnityEngine;
[ExecuteAlways]
[RequireComponent(typeof(SpriteRenderer))]
public class TeacherPose : MonoBehaviour, Pose
{
    [SerializeField] PoseType pose = PoseType.WritingA;
    [SerializeField] Sprite[] poseSprites = new Sprite[6];
    [SerializeField] GameObject squeakMarker;
    SpriteRenderer spriteRenderer;
    public PoseType CurrentPose => pose;
    public void SetPose(PoseType newPose)
    {
        pose = newPose;
        ApplySprite();
        if (squeakMarker != null) squeakMarker.SetActive(pose == PoseType.Turning);
    }
    void OnEnable() => SetPose(pose);
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
        if (this != null) SetPose(pose);
    }
#endif
    void ApplySprite()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        int index = (int)pose;
        if (poseSprites != null && index < poseSprites.Length && poseSprites[index] != null)
            spriteRenderer.sprite = poseSprites[index];
    }
}
