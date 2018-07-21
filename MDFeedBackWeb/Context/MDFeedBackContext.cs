using MDFeedBackWeb.Models;
using System.Data.Entity;

namespace MDFeedBackWeb.Context
{
    public class MDFeedBackContext : DbContext
    {
        public MDFeedBackContext()
        {
            //TODO: ctor context
        }

        public DbSet<MDFeedBackModel> MDFeedBacks { get; set; }
    }
}