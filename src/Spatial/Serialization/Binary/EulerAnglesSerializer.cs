namespace MathNet.Spatial.Serialization
{
    using System.Runtime.Serialization;
    using MathNet.Spatial.Euclidean;

    internal class EulerAnglesSerializer : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            EulerAngles point = (EulerAngles)obj;
            info.AddValue("Alpha", point.Alpha);
            info.AddValue("Beta", point.Beta);
            info.AddValue("Gamma", point.Gamma);
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            Angle alpha = (Angle)info.GetValue("Alpha", typeof(Angle));
            Angle beta = (Angle)info.GetValue("Beta", typeof(Angle));
            Angle gamma = (Angle)info.GetValue("Gamma", typeof(Angle));
            return new EulerAngles(alpha, beta, gamma);
        }

    }
}
