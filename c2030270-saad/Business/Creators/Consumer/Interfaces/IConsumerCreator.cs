namespace c2030270_saad.Business.Creators.Consumer.Interfaces
{
    using c2030270_saad.Data.Entities;
    using c2030270_saad.Resources.Consumer;

    public interface IConsumerCreator
    {
        Consumer CreateConsumer(CreateConsumerRequest request);
    }
}