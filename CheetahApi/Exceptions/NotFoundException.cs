namespace CheetahApi.Exceptions
{
    public class NotFoundException<T> : Exception
    {
        public NotFoundException(int id) : base($"Object '{typeof(T)}' with Id: {id} not found in the records.")
        {
        }
    }
}
