using System.Globalization;

namespace AILibrary;

public class NeuronLayer
{
    private List<Neuron> neurons { get; set; }
    public int neuronCount { get; private set; }
    public int inputCount { get; private set; }

    public NeuronLayer(int numberOfInputs, int numberOfNeurons){
        // set relevant params
        neuronCount = numberOfNeurons;
        inputCount = numberOfInputs;

        // initialize and create the neurons 
        neurons = new List<Neuron>{ };
        for (int i = 0; i < numberOfInputs; i++)
        {
            neurons.Add(new Neuron(numberOfInputs));
        }
    }

    public List<double> calculateOutput(List<double> inputValues){
        // check if the count of the passed Inputs corresponds to the set inputCount of the layer
        if (inputCount != inputValues.Count)
        {
            throw new Exception("The Length of the inputValue List does not correspond to the length of the set inputSize of the Layer");
        }

        List<double> outputs = new List<double>{};

        // calculate the output of each neuron
        for (int i = 0; i < neurons.Count; i++)
        {
            outputs.Add(neurons[i].calculateOutput(inputValues));
        }
        
        return outputs; 
    }
}