namespace AILibrary;

public class Neuron

{
    public List<double> Weights {get; private set; }
    public List<double> Inputs { get; private set; }
    public double Bias {get; private set; }
    public int InputCount {get; private set; }

    public Neuron(int input_size){
        
        Inputs = new List<double>{ };
        // initialize new Random and Weight list
        Random rand = new Random();
        Weights = new List<double>{ };

        InputCount = input_size;

        // Assign random start values to both the bias and the weights
        Bias = rand.NextDouble();
        for (int i = 0; i < input_size; i++)
        {
            Weights.Add(rand.NextDouble());
        }
    }

    public void UpdateWeights(List<double> newWeights){
        Weights = newWeights;
    }

    public void UpdateBias(double newBias){
        Bias = newBias;
    }

    // Calculates and returns the output of the neuron
    public double ForwardPass(List<double> inputValues){
        // Check if the inputValues have the right size
        if (inputValues.Count != InputCount) 
        {
            throw new Exception("The Length of the inputValue List does not correspond to the length of the set inputCount of the Neuron");
        }
        Inputs = inputValues;
        // calculate the dot product
        double result  = 0; 
        for (int i = 0; i < inputValues.Count; i++)
        {
            result += Weights[i] * inputValues[i];
        }

        result += Bias;
        return result;
    }
}
