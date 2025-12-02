namespace c2030270_saad.Data.Queries.Consumer.Interfaces
{
    using c2030270_saad.Data.Entities;
    
    public interface ICreateConsumerQuery
    {
        Consumer Execute(Consumer consumer);
    }
}