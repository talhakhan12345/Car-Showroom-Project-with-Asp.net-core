using Microsoft.EntityFrameworkCore;

namespace Car_web.Models
{
    public class dbcontext : DbContext
    {
        public dbcontext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<admin> tbl_admin { get; set; } 

        public DbSet<Car> tbl_addcar { get; set; }

        public DbSet<contact_us> tbl_contact { get; set; }

        public DbSet<CreditApplication> tbl_finance { get; set; }

        public DbSet<TestDriveRequest> tbl_testdrive { get; set; }

        public DbSet<RequestMoreInfo> tbl_moreinfo { get; set; }

        public DbSet<Request> tbl_req { get; set; }

    }
}
