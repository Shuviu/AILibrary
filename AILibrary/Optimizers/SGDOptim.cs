namespace AILibrary;

public class SGDOptim : IOptimizer {
    
    public double LearningRate { get; private set; }

    public SGDOptim(double learningRate){
        LearningRate = learningRate;
    }

    public void UpdateParams(NeuronLayer layer){
        
        // Nullchecks
        if (layer.dWeights == null)
        {
            throw new Exception("The Layer does not contain any dWeights");
        }
        else if (layer.dBiases == null)
        {
            throw new Exception("The Layer does not contain any dBiases");    
        }

        // Initialize relevant params
        List<Neuron> neurons = layer.Neurons;
        List<double> dBiases = layer.dBiases;
        List<List<double>> dWeights = layer.dWeights;

        // Calculate and set adjustedWeights for each Neuron
        for (int i = 0; i < neurons.Count; i++)
        {
            List<double> adjustedWeights = CalculateAdjustedWeights(neurons[i], dWeights[i]);
            neurons[i].UpdateWeights(adjustedWeights);
        }

        // Calculate and set adjustedBiases for each Neuron
        for (int i = 0; i < neurons.Count; i++){
            double adjustedBias = CalculateAdjustedBias(neurons[i], dBiases[i]);

            neurons[i].UpdateBias(adjustedBias);
        }
    }

    private List<double> CalculateAdjustedWeights(Neuron neuron, List<double> dWeights){
        List<double> output = new List<double>{ };
        // Iterate over each set of matching NeuronWeight and dWeight
        for (int i = 0; i  < dWeights.Count; i++){
                // Calculate the adjustedWeight using NeuronWeight += -lr * dWeight
                output.Add(neuron.Weights[i] + -LearningRate * dWeights[i]);
            }
        return output;
    }

    private double CalculateAdjustedBias(Neuron neuron, double dBias){
        // Calculate the adjustedBias using NeuronBias += -lr * dBias
        return neuron.Bias + (-LearningRate * dBias);
    }
}