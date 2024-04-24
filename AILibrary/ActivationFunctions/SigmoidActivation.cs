public class SigmoidActivation : ILayer{

    public List<double> Inputs { get; private set; }
    public List<double> dInputs { get; private set; }
    public List<double> Outputs { get; private set; }

    public SigmoidActivation(){
        Inputs = new List<double>{ };
        dInputs = new List<double>{ };
        Outputs = new List<double> { };
    }

    public void ForwardPass(List<double> inputValues)
    {
        Outputs.Clear();
        for (int i = 0; i < inputValues.Count; i++)
        {
            Outputs.Add(1 / (1 + Math.Exp(- inputValues[i])));
        }
    }

    public void BackwardPass(List<double> dValues)
    {
        dInputs.Clear();
        for (int i = 0; i < dValues.Count; i++)
        {
            dInputs.Add(dValues[i] * (1 - Outputs[i]) * Outputs[i]);
        }
    }

    public List<double> GetDInputs()
    {
        return dInputs;
    }

    public List<double> GetOutputs()
    {
        return Outputs;
    }
}