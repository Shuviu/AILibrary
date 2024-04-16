namespace AILibrary;

public class SoftMaxActivation{

    public List<double> Outputs { get; private set; }
    public List<double> dInputs { get; private set; }

    public SoftMaxActivation(){
        Outputs = new List<double>{ };
        dInputs = new List<double>{ };
    }

    public List<double> ForwardPass(List<double> inputValues){
        Outputs.Clear();
        double expSum = 0.0;

        // calculate the shared quotient of the SoftMax function
        for (int i = 0; i < inputValues.Count; i++)
        {
            expSum += Math.Pow(Math.E, inputValues[i]);
        }

        // Devide e^inputValues by the shared quotient to complete the SoftMax function
        for (int i = 0; i < inputValues.Count; i++)
        {
            Outputs.Add(Math.Pow(Math.E, inputValues[i]) / expSum);
        }
        
        return Outputs;
    }

    public void BackwardPass(List<double> dValues)
    {
        dInputs.Clear();
        List<double> softmaxOutputs = Outputs;
        // Calculate the derivate for the SoftMax function
        for (int i = 0; i < softmaxOutputs.Count; i++)
        {
            dInputs.Add(dValues[i] * softmaxOutputs[i] * (1 - softmaxOutputs[i]));
        }
    }
}