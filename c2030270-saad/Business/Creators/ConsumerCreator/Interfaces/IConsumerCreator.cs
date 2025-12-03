using c2030270_saad.Data.Entities;
using c2030270_saad.Resources.Consumer;

namespace c2030270_saad.Business.Creators.ConsumerCreator.Interfaces
{
    public interface IConsumerCreator
    {
        Consumer CreateConsumer(CreateConsumerRequest request);
    }
}