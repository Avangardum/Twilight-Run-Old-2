namespace Avangardum.TwilightRun
{
    public interface ILevelGenerator
    {
        void GenerateToThePoint(float point);
        void ClearBehindThePoint(float point);
        void ClearAll();
        void InjectDependencies(ILevelGenerationConfig config);
    }
}