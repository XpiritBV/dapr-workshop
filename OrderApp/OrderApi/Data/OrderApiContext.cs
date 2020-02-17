using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderApi.Model;

namespace OrderApi.Data
{
    public class OrderApiContext : DbContext
    {
        public OrderApiContext (DbContextOptions<OrderApiContext> options)
            : base(options)
        {
        }

        public DbSet<OrderApi.Model.Order> Order { get; set; }
    }
}
