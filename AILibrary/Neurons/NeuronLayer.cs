namespace AILibrary;

public class NeuronLayer
{
    private List<Neuron> Neurons { get; set; }
    public int NeuronCount { get; private set; }
    public int InputCount { get; private set; }
    // derivative of the Inputs
    public List<double>? dInputs { get; private set; }
    // derivative of the Weights
    public List<List<double>>? dWeights { get; private set; }
    // derivative of the Biases
    public List<double>? dBiases { get; private set; }

    public NeuronLayer(int numberOfInputs, int numberOfNeurons){
        // set relevant params
        NeuronCount = numberOfNeurons;
        InputCount = numberOfInputs;

        // initialize and create the neurons 
        Neurons = new List<Neuron>{ };
        for (int i = 0; i < numberOfInputs; i++)
        {
            Neurons.Add(new Neuron(numberOfInputs));
        }
    }

    public List<double> ForwardPass(List<double> inputValues){
        // check if the count of the passed Inputs corresponds to the set inputCount of the layer
        if (InputCount != inputValues.Count)
        {
            throw new Exception("The Length of the inputValue List does not correspond to the length of the set inputSize of the Layer");
        }

        List<double> outputs = new List<double>{};

        // calculate the output of each neuron
        for (int i = 0; i < Neurons.Count; i++)
        {
            outputs.Add(Neurons[i].ForwardPass(inputValues));
        }
        
        return outputs; 
    }

    public void BackwardPass(List<double> dValues){

        if (dValues.Count != NeuronCount)
        {
            throw new Exception("The length of the dValues list does not correspond with the NeuronCount of the layer");
        }

        // get the current weights of all Neurons in the layer
        List<List<double>> weights = new List<List<double>>{ };
        for (int i = 0; i < Neurons.Count; i++)
        {
            weights.Add(Neurons[i].Weights);
        }
        // calculate the derivates using the helper methods
        dInputs = calculateDInputs(weights, dValues);
        dWeights = calculateDWeights(dValues);
        dBiases = calculateDBiases(dValues);
    }

    // calculate the derivatives for the layer Inputs
    private List<double> calculateDInputs(List<List<double>> weights, List<double> dValues){

        List<double> derivatives = new List<double>{ };

        // calculate the dot product to get dInputs
        for (int i = 0; i < weights.Count; i++)
        {
            double temp = 0.0;
            for (int j = 0; j < dValues.Count; j++)
            {
                temp += dValues[j] * weights[i][j];
            }

            derivatives.Add(temp);
        }

        return derivatives;
    }

    private List<double> calculateDBiases(List<double> dValues){

        List<double> derivatives = new List<double>{ };

        // get the Sum of dValues to get dBias
        double temp = 0.0;
        for (int i = 0; i < dValues.Count; i++)
        {
            temp += dValues[i];
        }
        // add the derivative for each Neuron to the list
        for (int i = 0; i < NeuronCount; i++)
        {
            derivatives.Add(temp);
        }
        
        return derivatives;
    }

    private List<List<double>> calculateDWeights(List<double> dValues){

        List<List<double>> derivatives = new List<List<double>>{ };

        // Iterate over neurons in the layer
        for (int i = 0; i < Neurons.Count; i++)
        {
            Neuron neuron = Neurons[i];
            List<double> neuronDWeights = new List<double>();

            // Iterate over weights of the neuron
            for (int j = 0; j < neuron.Weights.Count; j++)
            {
                // Compute the derivative of the weight using chain rule
                double dWeight = dValues[i] * neuron.Inputs[j];
                neuronDWeights.Add(dWeight);
            }

            derivatives.Add(neuronDWeights);
        }

        return derivatives;
    }
}