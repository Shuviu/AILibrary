namespace AILibrary;

public class SigmoidActivation{

    public static List<double> ForwardPass(List<double> inputValues){
        List<double> output = new List<double>{ };

        // pass every value of the inputValues through the Sigmoid function
        for (int i = 0; i < inputValues.Count; i++)
        {
            output.Add(1.0 / (1.0 + Math.Pow(Math.E, -inputValues[i])));
        }

        return output;
    }

    public static List<double> BackwardPass(List<double> dValues){
        return new List<double>{ };
    }
}