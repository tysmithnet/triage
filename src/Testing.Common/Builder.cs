using Moq;

namespace Testing.Common
{
    public abstract class Builder<T> where T : class 
    {
        public Mock<T> Mock { get; set; } = new Mock<T>();

        public T Build()
        {
            return Mock.Object;
        }
    }
}