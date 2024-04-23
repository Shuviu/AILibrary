namespace AILibrary;

public class SpiralDatasetGen{

    public double Density { get; private set; }
    public double MaxRadius { get; private set; }
    List<List<double>> Samples;
    List<List<int>> Labels; 

    public SpiralDatasetGen(double density, double maxRadius)
    {
        Density = density;
        MaxRadius = maxRadius;
        Samples = new();
        Labels = new();
    }

    public void GenerateSamples(int numClasses){
        double points = 96 * Density;

        for (int i = 0; i < points; i++)
        {
            double angle = (i * Math.PI) / (16 * Density);

            double radius = MaxRadius * ((104 * Density) - i ) / (104 * Density);

            double x = radius * Math.Cos(angle);
            double y = radius * Math.Sin(angle);

            Samples.Add(new List<double>{x, y});
            Labels.Add(new List<int>{0, 1});

            if (numClasses > 1)
            {
                Samples.Add(new List<double>{ -x, -y});
                Labels.Add(new List<int>{1, 0});
            }
        }

    }

    public void NormalizeSamples(){
        for (int i = 0; i < Samples.Count; i++)
        {
            Samples[i][0] = Samples[i][0] / 1000;
            Samples[i][1] = Samples[i][1] / 1000;
        }
    }

    public List<List<double>> GetSamples(){
        if (Samples.Count == 0)
        {
            throw new Exception("The Sample count is 0");
        }
        return Samples;
    }

    public List<List<int>> GetLabels(){
        if (Labels.Count == 0)
        {
            throw new Exception("The Label count is 0");
        }
        return Labels;
    }
}