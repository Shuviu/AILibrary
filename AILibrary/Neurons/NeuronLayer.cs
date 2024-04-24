namespace AILibrary;

public class NeuronLayer : ILayer
{
    public List<Neuron> Neurons { get; private set; }
    public int NeuronCount { get; private set; }
    public int InputCount { get; private set; }

    // Outputs of the forward function 
    public List<double> Outputs { get; private set; }
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
        Outputs = new List<double> { };

        // initialize and create the neurons 
        Neurons = new List<Neuron>{ };
        for (int i = 0; i < numberOfNeurons; i++)
        {
            Neurons.Add(new Neuron(numberOfInputs));
        }
    }

    public void ForwardPass(List<double> inputValues){
        // check if the count of the passed Inputs corresponds to the set inputCount of the layer
        if (InputCount != inputValues.Count)
        {
            throw new Exception("The Length of the inputValue List does not correspond to the length of the set inputSize of the Layer");
        }

        Outputs.Clear();
        // calculate the output of each neuron
        for (int i = 0; i < Neurons.Count; i++)
        {
            Outputs.Add(Neurons[i].ForwardPass(inputValues));
        }
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
        dInputs = calculateDInputs(dValues);
        dWeights = calculateDWeights(dValues);
        dBiases = calculateDBiases(dValues);
    }

    public List<double> GetOutputs(){
        return Outputs;
    }

    public List<double> GetDInputs(){
        if (dInputs == null)
        {
            throw new Exception("The dInputs of the layer are null");
        }
        return dInputs;
    }

    // calculate the derivatives for the layer Inputs
    private List<double> calculateDInputs(List<double> dValues)
    {
        List<double> derivatives = new List<double>();

        // calculate the dot product to get dInputs
        for (int i = 0; i < Neurons[0].Weights.Count; i++)
        {
            double temp = 0.0;
            for (int j = 0; j < dValues.Count; j++)
            {
                temp += dValues[j] * Neurons[j].Weights[i];
            }

            derivatives.Add(temp);
        }
        return derivatives;
}

private List<List<double>> calculateDWeights(List<double> dValues)
{
    dWeights = new List<List<double>>();

    for (int i = 0; i < Neurons.Count; i++)
    {
        Neuron neuron = Neurons[i];
        List<double> neuronDWeights = new List<double>();

        for (int j = 0; j < neuron.Weights.Count; j++)
        {
            // Compute the derivative of the weight using chain rule
            double dWeight = dValues[i] * neuron.Inputs[j];
            neuronDWeights.Add(dWeight);
        }

        dWeights.Add(neuronDWeights);
    }
    return dWeights;
}

private List<double> calculateDBiases(List<double> dValues)
{
    // Calculate dBiases
    double sum = dValues.Sum();

    dBiases = new List<double>();
    for (int i = 0; i < NeuronCount; i++)
    {
        dBiases.Add(sum);
    }

    return dBiases;
}
}