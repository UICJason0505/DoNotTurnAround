public enum PoseType { WritingA, WritingB, FakeTell, Turning, FacingSquint, Facing }
public interface Pose
{
    PoseType CurrentPose { get; }
    void SetPose(PoseType pose);
}
public interface Progress
{
    float CurrentProgress { get; }
    void SetProgress(float percent);
}
public interface Food
{
    float Remaining { get; }
    void SetRemaining(float percent);
}
public interface SnackSound
{
    void PlayUnwrap();
    void StartChewing();
    void StopChewing();
}
