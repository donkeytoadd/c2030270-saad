namespace c2030270_saad.Data.Queries.Consumer.Interfaces
{
    using c2030270_saad.Data.Entities;
    
    public interface IGetConsumerByConsumerEmailQuery
    { 
        bool Execute(string email);
    }
}