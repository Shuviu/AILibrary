namespace AILibrary;

public interface IActivationFunction
{
    void ForwardPass(List<double> inputValues);
    void BackwardPass(List<double> dValues);
}