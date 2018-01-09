namespace MathNet.Spatial.Serialization
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Surrogate for Angle
    /// </summary>
    [DataContract(Name = "Angle")]
    public class AngleSurrogate
    {
        [DataMember(Order = 1)]
        public double Value;

        public static implicit operator AngleSurrogate(Angle angle) => new AngleSurrogate { Value = angle.Radians };

        public static implicit operator Angle(AngleSurrogate angle) => Angle.FromRadians(angle.Value);
    }
}
