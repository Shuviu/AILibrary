namespace AILibrary;

public class SigmoidActivation : IActivationFunction{


    public SigmoidActivation(){
        throw new Exception("Not implemted owo");
    }    

    public void ForwardPass(List<double> inputValues){
        List<double> output = new List<double>{ };

        // pass every value of the inputValues through the Sigmoid function
        for (int i = 0; i < inputValues.Count; i++)
        {
            output.Add(1.0 / (1.0 + Math.Pow(Math.E, -inputValues[i])));
        }
    }

    public void BackwardPass(List<double> dValues){
    }
}