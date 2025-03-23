using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.DbContexts
{
    public class DbEntities : DbContext
    {
        public DbEntities(DbContextOptions<DbEntities> options)
            : base(options)
        { }

        // DbSet 屬性對應於資料庫中的資料表
        public DbSet<Currency> Currency { get; set; }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (BaseEntity)entry.Entity;
                DateTime now = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entity.CreateDate = now;
                    entity.ModifyDate = now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.ModifyDate = now;
                }
            }

            return base.SaveChanges();
        }

        // 覆寫 SaveChangesAsync 方法
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (BaseEntity)entry.Entity;
                DateTime now = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entity.CreateDate = now;
                    entity.ModifyDate = now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entity.ModifyDate = now;
                }
            }

            // 呼叫基類的 SaveChangesAsync 方法
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
