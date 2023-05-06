using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuralNetworkDatabase.Entities;

public class Symptoms
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SymptomId { get; set; }
    public User User { get; set; }
    
    [ForeignKey(nameof(User))]
    public int UserIdentity { get; set; }

    public int NNType { get; set; }
    
    public double Age { get; set; }

    public double Sex { get; set; }

    public double Cp { get; set; }

    public double Trestbps { get; set; }

    public double Chol { get; set; }

    public double Fbs { get; set; }

    public double Restecg { get; set; }

    public double Thalach { get; set; }

    public double Exang { get; set; }

    public double Oldpeak { get; set; }
    
    public double Slope { get; set; }

    public double Ca { get; set; }

    public double Thal { get; set; }
    
    public double Result { get; set; }
}