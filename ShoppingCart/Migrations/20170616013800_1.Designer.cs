using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ShoppingCart.Models;

namespace ShoppingCart.Migrations
{
    [DbContext(typeof(ShoppingCartContext))]
    [Migration("20170616013800_1")]
    partial class _1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("ShoppingCart.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ShoppingCart.Models.ProductShoppingCart", b =>
                {
                    b.Property<int>("ProductId");

                    b.Property<int>("ShoppingCartId");

                    b.HasKey("ProductId", "ShoppingCartId");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("ProductShoppingCarts");
                });

            modelBuilder.Entity("ShoppingCart.Models.ShoppingCart", b =>
                {
                    b.Property<int>("ShoppingCartId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientGuid");

                    b.Property<int?>("UserId");

                    b.HasKey("ShoppingCartId");

                    b.HasIndex("UserId");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("ShoppingCart.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<string>("Phone");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ShoppingCart.Models.ProductShoppingCart", b =>
                {
                    b.HasOne("ShoppingCart.Models.Product", "Product")
                        .WithMany("ProductShoppingCarts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ShoppingCart.Models.ShoppingCart", "ShoppingCart")
                        .WithMany("ProductShoppingCarts")
                        .HasForeignKey("ShoppingCartId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ShoppingCart.Models.ShoppingCart", b =>
                {
                    b.HasOne("ShoppingCart.Models.User", "User")
                        .WithMany("Carts")
                        .HasForeignKey("UserId");
                });
        }
    }
}
