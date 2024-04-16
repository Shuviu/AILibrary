namespace AILibrary;

public class ReLUActivation{

    public List<double> Inputs { get; private set; }
    public List<double> dInputs { get; private set; }
    public List<double> Outputs { get; private set; }

    public ReLUActivation(){
        Inputs = new List<double>{ };
        dInputs = new List<double>{ };
        Outputs = new List<double> { };
    }

    public void ForwardPass(List<double> inputValues){

        Inputs = inputValues;
        Outputs.Clear();

        // pass every value of inputValues through the ReLU function
        for (int i = 0; i < inputValues.Count; i++)
        {
            Outputs.Add(Math.Max(0, inputValues[i]));
        }
    }

    public void BackwardPass(List<double> dValues){
        dInputs = dValues;

        for (int i = 0; i < Inputs.Count; i++)
        {
            if (Inputs[i] <= 0)
            {
                dInputs[i] = 0;
            }
        }
    }
}