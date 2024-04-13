namespace AILibrary;

public class ActivationFunctions{
    public static List<double> ReLUActivation(List<double> inputValues){
        List<double> output = new List<double>{ };

        // pass every value of inputValues through the ReLU function
        for (int i = 0; i < inputValues.Count; i++)
        {
            output.Add(Math.Max(0, inputValues[i]));
        }

        return output;
    }

    public static List<double> SigmoidActivation(List<double> inputValues){
        List<double> output = new List<double>{ };

        // pass every value of the inputValues through the Sigmoid function
        for (int i = 0; i < inputValues.Count; i++)
        {
            output.Add(1.0 / (1.0 + Math.Pow(Math.E, -inputValues[i])));
        }

        return output;
    }
}
