using System.Globalization;

namespace AILibrary;

public class NeuronLayer
{
    private List<Neuron> Neurons { get; set; }
    public int NeuronCount { get; private set; }
    public int InputCount { get; private set; }

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
}