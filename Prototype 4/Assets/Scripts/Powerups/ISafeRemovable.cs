public interface ISafeRemovable
{
    bool IsSafeToRemove { get; }
    bool SafeRemoveRequested { get; set; }
    void SafeRemove();
}
