namespace AILibrary;

public class ReLUActivation{

    public static List<double> ForwardPass(List<double> inputValues){
        List<double> output = new List<double>{ };

        // pass every value of inputValues through the ReLU function
        for (int i = 0; i < inputValues.Count; i++)
        {
            output.Add(Math.Max(0, inputValues[i]));
        }

        return output;
    }

    public static List<double> BackwardPass(List<double> dValues){
        return new List<double>{ };
    }
}