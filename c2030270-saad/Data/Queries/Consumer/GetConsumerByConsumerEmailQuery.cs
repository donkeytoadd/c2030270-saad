namespace c2030270_saad.Data.Queries.Consumer
{
    using c2030270_saad.Data.Queries.Consumer.Interfaces;
    using c2030270_saad.Data.Entities;
    using Microsoft.EntityFrameworkCore;
    
    public class GetConsumerByConsumerEmailQuery :  IGetConsumerByConsumerEmailQuery
    {
        private readonly IDbContextFactory<ApplicationDbContext> contextFactory;
        
        public GetConsumerByConsumerEmailQuery(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public bool Execute(string email)
        {
            using (var context = this.contextFactory.CreateDbContext())
            {
                var test=  context.Consumer.FirstOrDefault(x => x.Email == email);
                
                return test != null ? true: false;
            }
        }
    }
}