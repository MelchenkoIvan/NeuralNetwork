namespace RecurrentNeuralNetwork;

public class Tact
{
    public int Id { get; private set; }
    
    public double[] Values { get; private set; }
    
    public Tact(int id, double [] values)
    {
        Id = id;
        Values = values;
    }
}