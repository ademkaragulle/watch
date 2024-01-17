using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=LAPTOP-2CAKTV1F;initial catalog=WatchApiDb;integrated security=true");
        }
        public DbSet<Category>? Category {  get; set; }
        public DbSet<Product>? Product {  get; set; }
        public DbSet<User>? User{  get; set; }
        public DbSet<Order>? Order {  get; set; }
        public DbSet<Blog>? Blog {  get; set; }
        public DbSet<BlogComment>? BlogComment {  get; set; }
        public DbSet<Contact>? Contact {  get; set; }
        public DbSet<OrderDetail>? OrderDetail {  get; set; }
        public DbSet<Favorite>? Favorite { get; set; }
    }
}
