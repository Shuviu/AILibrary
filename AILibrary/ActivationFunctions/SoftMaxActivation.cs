namespace AILibrary;

public class SoftMaxActivation{

    public static List<double> ForwardPass(List<double> inputValues){
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

    public static List<double> BackwardPass(List<double> dValues){
        return new List<double>{ };
    }
}