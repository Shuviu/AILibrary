using System.Dynamic;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;

namespace AILibrary;

public class Trainer
{

    public ILossFunction LossFunction { get; private set; }
    public IOptimizer Optimizer { get; private set; }
    public List<ILayer> Layers { get; private set; }
    public Dataset? CurrentDataset { get; private set; }
    public double CurrentLoss { get; private set; }
    public double CurrentAccuracy { get; private set; }
    public String? CurrentDebugOutput { get; private set; }
    public VariantType? CurrentPrediction { get; private set; }

    public Trainer(ILossFunction lossFunction, IOptimizer optimizer)
    {
        LossFunction = lossFunction;
        Optimizer = optimizer;
        Layers = new List<ILayer> { };
    }

    public void AddNeuronLayer(ILayer layer)
    {
        Layers.Add(layer);
    }

    public void UpdateDataset(Dataset dataset)
    {
        CurrentDataset = dataset;
    }

    public void Train(int iterations)
    {
        if (CurrentDataset == null)
        {
            throw new Exception("No Dataset defined for the Model");
        }

        Dataset trainset = CurrentDataset;
        SoftMaxActivation softMaxActivation = new SoftMaxActivation();

        for (int x = 0; x < iterations; x++)
        {
            for (int i = 0; i < trainset.Samples.Count; i++)
            {
                ForwardPass(trainset.Samples[i], trainset.Labels[i], softMaxActivation);
                BackwardPass(trainset.Labels[i], softMaxActivation);
            }
            if (x%5 == 0)
            {   
            System.Console.WriteLine($"Current Iteration: {x}, Current Loss: {LossFunction.getOutput()}, Current Predictions: {softMaxActivation.Outputs[0]} {softMaxActivation.Outputs[1]}");
            }
        }
    }

    private void ForwardPass(List<double> sample, List<int> label, SoftMaxActivation softMaxActivation)
    {
        List<double> layerInput = sample;
        foreach (var NeuronLayer in Layers)
        {
            NeuronLayer.ForwardPass(layerInput);
            layerInput = NeuronLayer.GetOutputs();
        }
        softMaxActivation.ForwardPass(layerInput);
        LossFunction.CalculateLoss(softMaxActivation.Outputs, label);
    }

    private void BackwardPass(List<int> label, SoftMaxActivation softMaxActivation)
    {
        LossFunction.BackwardPass(softMaxActivation.Outputs, label, true);
        softMaxActivation.BackwardPass(LossFunction.getDInputs());

        List<double> dValues = softMaxActivation.dInputs;
        for (int j = Layers.Count - 1; j >= 0; j--)
        {
            ILayer layer = Layers[j];

            layer.BackwardPass(dValues);
            if (layer.GetDInputs() == null)
            {
                throw new Exception("dInputs of a NeuronLayer is null!");
            }
            dValues = layer.GetDInputs();

            if (layer is NeuronLayer)
            {
                NeuronLayer neuronLayer = (NeuronLayer) layer;
                System.Console.WriteLine("dWeights: " + neuronLayer.dWeights[0][0]);
                Optimizer.UpdateParams((NeuronLayer)layer);
            }
        }
    }

    public void Predict()
    {

    }

}