namespace AILibrary;

public class Neuron

{
    public List<double> weights {get; private set; }
    public double bias {get; private set; }
    public int inputSize {get; private set; }

    public Neuron(int input_size){

        // initialize new Random and Weight list
        Random rand = new Random();
        weights = new List<double>{ };

        inputSize = input_size;

        // Assign random start values to both the bias and the weights
        bias = rand.NextDouble();
        for (int i = 0; i < input_size; i++)
        {
            weights.Add(rand.NextDouble());
        }
    }

    public void updateWeights(List<double> newWeights){
        weights = newWeights;
    }

    public void updateBias(double newBias){
        bias = newBias;
    }

    // Calculates and returns the output of the neuron
    public double calculateOutput(List<double> inputValues){
        // Check if the inputValues have the right size
        if (inputValues.Count != inputSize) 
        {
            throw new Exception("The Length of the inputValue List does not correspond to the length of the set inputSize of the Neuron");
        }
        
        // calculate the dot product
        double result  = 0; 
        for (int i = 0; i < inputValues.Count; i++)
        {
            result += weights[i] * inputValues[i];
        }

        result += bias;
        return result;
    }
}
