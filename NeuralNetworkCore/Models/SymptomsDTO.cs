using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;

namespace NeuralNetworkCore.Models;

public class SymptomsDTO
{
    [Required] public double Age { get; set; }

    [Required] public double Sex { get; set; }

    [Required] public double Cp { get; set; }

    [Required] public double Trestbps { get; set; }

    [Required] public double Chol { get; set; }

    [Required] public double Fbs { get; set; }

    [Required] public double Restecg { get; set; }

    [Required] public double Thalach { get; set; }

    [Required] public double Exang { get; set; }

    [Required] public double Oldpeak { get; set; }

    [Required] public double Slope { get; set; }

    [Required] public double Ca { get; set; }

    [Required] public double Thal { get; set; }

    public double[,] GetInputSignals()
    {
        var props = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var inputSignals = new double[1, props.Length];
        for (var i = 0; i < props.Length; i++)
        {
            inputSignals[0, i] = (double)props[i].GetValue(this)!;
        }

        return inputSignals;
    }
    
}