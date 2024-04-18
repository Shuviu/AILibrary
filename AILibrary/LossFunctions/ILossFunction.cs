namespace AILibrary;

public interface ILossFunction
{
    void CalculateLoss(List<double> predictedDistribution, List<int> desiredDistribution);
    void BackwardPass(List<double> dValues, List<int> trueValues, bool oneHotEncoded);
}