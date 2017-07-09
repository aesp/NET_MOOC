﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ShoppingCart.Models
{
    public partial class ShoppingCartContext : DbContext
    {

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<ProductShoppingCart> ProductShoppingCarts { get; set; }

        public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options)
            : base(options)
        {
			var connection = Database.GetDbConnection();
			connection.Open();
			using (var command = connection.CreateCommand())
			{
				command.CommandText = "PRAGMA foreign_keys=\"0\"";
				command.ExecuteNonQuery();
			}
        }

        public ShoppingCartContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ProductShoppingCart>()
                        .HasKey(pc => new { pc.ProductId, pc.ShoppingCartId });

            modelBuilder.Entity<ProductShoppingCart>()
                        .HasOne(pc => pc.Product)
                        .WithMany(p => p.ProductShoppingCarts)
                        .HasForeignKey(pc => pc.ProductId);
            
			modelBuilder.Entity<ProductShoppingCart>()
						.HasOne(pc => pc.ShoppingCart)
                        .WithMany(p => p.ProductShoppingCarts)
                        .HasForeignKey(pc => pc.ShoppingCartId);
            
            modelBuilder.Entity<ShoppingCart>()
                        .HasOne(c => c.User)
                        .WithMany(u => u.Carts);

            modelBuilder.Entity<User>()
                        .HasMany(u => u.Carts)
                        .WithOne(c => c.User);

            base.OnModelCreating(modelBuilder);
        }



    }
}