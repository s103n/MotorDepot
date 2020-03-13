namespace MotorDepot.DAL.Interfaces
{
    public interface ILogger<in T> where T : class
    {
        void Log(T item);
    }
}
