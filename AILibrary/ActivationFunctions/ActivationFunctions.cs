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

    public static List<double> SoftMaxActivation(List<double> inputValues){
        List<double> output = new List<double> {};
        double expSum = 0.0;

        // calculate the shared quotient of the SoftMax function
        for (int i = 0; i < inputValues.Count; i++)
        {
            expSum += Math.Pow(Math.E, inputValues[i]);
        }

        // Devide e^inputValues by the shared quotient to complete the SoftMax function
        for (int i = 0; i < inputValues.Count; i++)
        {
            output.Add(Math.Pow(Math.E, inputValues[i]) / expSum);
        }

        return output;
    }
}
