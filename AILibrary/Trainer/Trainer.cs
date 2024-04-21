using System.Dynamic;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;

namespace AILibrary;

public class Trainer
{

    public IActivationFunction CategorizationFunction { get; private set; }
    public ILossFunction LossFunction { get; private set; }
    public IOptimizer Optimizer { get; private set; }
    public List<NeuronLayer> NeuronLayers { get; private set; }
    public Dataset? CurrentDataset { get; private set; }
    public double CurrentLoss { get; private set; }
    public double CurrentAccuracy { get; private set; }
    public String? CurrentDebugOutput { get; private set; }
    public VariantType? CurrentPrediction { get; private set; }

    public Trainer(IActivationFunction categorizationFunction, ILossFunction lossFunction, IOptimizer optimizer)
    {
        CategorizationFunction = categorizationFunction;
        LossFunction = lossFunction;
        Optimizer = optimizer;
        NeuronLayers = new List<NeuronLayer> { };
    }

    public void AddNeuronLayer(int inputcount, int neuronCount)
    {
        NeuronLayers.Add(new NeuronLayer(inputcount, neuronCount));
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
                List<double> layerInput = trainset.Samples[i];
                foreach (var NeuronLayer in NeuronLayers)
                {
                    NeuronLayer.ForwardPass(layerInput);
                    layerInput = NeuronLayer.Outputs;
                }
                softMaxActivation.ForwardPass(layerInput);
                LossFunction.CalculateLoss(softMaxActivation.Outputs, trainset.Labels[i]);

                System.Console.WriteLine(LossFunction.getOutput());
                LossFunction.BackwardPass(softMaxActivation.Outputs, trainset.Labels[i], true);
                softMaxActivation.BackwardPass(LossFunction.getDInputs());

                List<double> dValues = softMaxActivation.dInputs;
                for (int j = NeuronLayers.Count - 1; j >= 0; j--)
                {
                    NeuronLayer neuronLayer = NeuronLayers[j];

                    neuronLayer.BackwardPass(dValues);
                    if (neuronLayer.dInputs == null)
                    {
                        throw new Exception("dInputs of a NeuronLayer is null!");
                    }
                    dValues = neuronLayer.dInputs;

                    if (neuronLayer is IActivationFunction)
                    {
                        System.Console.WriteLine("Is activationFunction");
                        break;
                    }
                    Optimizer.UpdateParams(neuronLayer);
                }
            }
        }


    }

    public void Predict()
    {

    }

}