using System;
using UnityEngine;

public class Chips : MonoBehaviour, Food
{
    [Serializable]
    struct State
    {
        [Tooltip("Shown when the remaining food is below this percent")]
        public float below;
        public Sprite sprite;
    }

    [SerializeField, Range(0f, 100f)] float remaining = 100f;
    [SerializeField] SpriteRenderer bag;
    [SerializeField] Sprite fullSprite;
    [SerializeField] State[] states = new State[0];

    public float Remaining => remaining;

    public void SetRemaining(float percent)
    {
        remaining = Mathf.Clamp(percent, 0f, 100f);
        Apply();
    }

    // Unity warns when sprites change inside OnValidate, so Inspector edits are applied right after validation.
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
        if (this != null) Apply();
    }
#endif

    void Apply()
    {
        if (bag == null) return;

        // the state with the lowest threshold that remaining is still below wins
        Sprite sprite = fullSprite;
        float lowest = float.MaxValue;
        foreach (var state in states)
        {
            if (remaining < state.below && state.below < lowest)
            {
                lowest = state.below;
                sprite = state.sprite;
            }
        }
        bag.sprite = sprite;
    }
}
