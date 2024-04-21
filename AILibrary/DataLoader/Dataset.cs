namespace AILibrary;

public class Dataset{

    public List<List<double>> Samples { get; private set; }
    public List<List<int>> Labels { get; private set; }

    public Dataset(List<List<double>> samples, List<List<int>> labels){
        Samples = samples;
        Labels = labels;
    }
}