public interface ILayer
{
    void ForwardPass(List<double> inputValues);
    void BackwardPass(List<double> dValues);
    List<double> GetDInputs();
    List<double> GetOutputs();
}