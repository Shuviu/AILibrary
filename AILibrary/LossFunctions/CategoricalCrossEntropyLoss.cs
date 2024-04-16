namespace AILibrary;

public class CategoricalCrossEntropyLoss{


    public List<double> dInputs { get; private set; }

    public CategoricalCrossEntropyLoss(){
        dInputs = new List<double>{ };
    }

    public double ForwardPass(List<double> predictedDistribution, List<double> desiredDistribution){
        
        // Length check for both input vectors
        if (predictedDistribution.Count != desiredDistribution.Count)
        {
            throw new Exception("The length of the predicted and desired distributions dont match!");
        }

        // Calculate the CategoricalCrossEntropyLoss
        double loss = 0.0;
        for (int i = 0; i < predictedDistribution.Count; i++)
        {
            loss += -(desiredDistribution[i] * Math.Log(predictedDistribution[i]));
        }

        return loss; 
    }

    public void BackwardPass(List<double> dValues, List<int> trueValues, bool oneHotEncoded){
        
        // Check if trueValues has the right length when oneHotEncoded (same as dValues)
        if (oneHotEncoded && dValues.Count != trueValues.Count)
        {
            throw new Exception("The length of the one hot encoded trueValues List does not correspond with the length of the dValues list");
        }
        // Check if trueValues has the right length when sparse (1)
        if (!oneHotEncoded && trueValues.Count != 1)
        {
            throw new Exception("The length of the sparse trueValues list is not equal to 1");
        }
        // initialize groundTruth list (desired prediction)
        List<int> groundTruth = new List<int>(new int[dValues.Count]);
        // if not oneHotEncoded convert it
        if (!oneHotEncoded)
        {
            groundTruth[trueValues[0] - 1] = 1;
        }
        // if oneHotEncoded just copy
        else
        {
            groundTruth = trueValues;
        }

        dInputs.Clear();
        // calculate the derivative of the loss function and add it to dInputs
        for (int i = 0; i < dValues.Count; i++)
        {
            // add a small error to the dValue to prevent division by 0 owo
            dInputs.Add(-groundTruth[i] / (dValues[i] + 0.000001));
        }
    }
}