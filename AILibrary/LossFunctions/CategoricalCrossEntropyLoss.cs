namespace AILibrary;

public class CategoricalCrossEntropyLoss{

    public static double ForwardPass(List<double> predictedDistribution, List<double> desiredDistribution){
        
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
}