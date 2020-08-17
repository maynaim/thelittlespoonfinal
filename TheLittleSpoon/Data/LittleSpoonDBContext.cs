using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheLittleSpoon;
using TheLittleSpoon.Models;
using TheLittleSpoon.Data;

namespace TheLittleSpoon.Data
{
    public class LittleSpoonDBContext : IdentityDbContext<Models.ApplicationUser>
    {
        // Constructor
        public LittleSpoonDBContext(DbContextOptions<LittleSpoonDBContext> options) : base(options)
        {

        }


        // Define app DB's
        public DbSet<Models.Article> Articles { get; set; }
        public DbSet<Models.Category> Categories { get; set; }
        public DbSet<Models.Comment> Comments { get; set; }


        // Initialize DB (default data)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Init base class
            base.OnModelCreating(modelBuilder);


            // Set relationsships between our DB tables
            modelBuilder.Entity<Models.Article>()
                .HasOne(p => p.Category)
                .WithMany(b => b.Articles)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Models.Comment>()
                .HasOne(p => p.Article)
                .WithMany(b => b.Comments)
                .OnDelete(DeleteBehavior.Cascade);


            // Insert categories
            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 1,
                Name = "Italian",
                Description = "meravigliosa",
                Color = System.Drawing.Color.ForestGreen
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 2,
                Name = "Asian",
                Description = "arigatō",
                Color = System.Drawing.Color.Brown
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 3,
                Name = "Arabic",
                Description = "majnun",
                Color = System.Drawing.Color.Olive
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 4,
                Name = "Mexican",
                Description = "adiós",
                Color = System.Drawing.Color.RoyalBlue
            });

            modelBuilder.Entity<Category>().HasData(new Category
            {
                CategoryId = 5,
                Name = "American",
                Description = "uncle sam",
                Color = System.Drawing.Color.MediumVioletRed
            });


            // Insert articles
            modelBuilder.Entity<Article>().HasData(new Article
            {
                ArticleId = 1,
                CategoryId = 1,
                Header = "Pizza",
                Summary = "Wonderful Homemade Pizza Just Like in Italy",
                DateCreated = new DateTime(2019, 10, 13, 15, 50, 00),
                HomeImageUrl = "https://cdn.vox-cdn.com/thumbor/Bw3NpqYaGgr_wAVVO6DFThLeX24=/0x0:2000x1333/1200x800/filters:focal(840x507:1160x827)/cdn.vox-cdn.com/uploads/chorus_image/image/64037689/Pizza_pepperoni_close.7.jpg",
                Location = "Naples, Italy",
                IsShowMap = true,
                Content = ArticleDataStrings.PizzaRecipe,
            });

            modelBuilder.Entity<Article>().HasData(new Article
            {
                ArticleId = 2,
                CategoryId = 1,
                Header = "Lasagna",
                Summary = "Half Pizza, Half Cake. The Best of Both Worlds!",
                DateCreated = new DateTime(2019, 5, 22, 8, 10, 00),
                HomeImageUrl = "https://www.seriouseats.com/recipes/images/2014/07/lasanga-bolognese-1500x1125.jpg",
                Location = "Rome, Italy",
                IsShowMap = true,
                Content = ArticleDataStrings.LasagnaRecipe,
            });

            modelBuilder.Entity<Article>().HasData(new Article
            {
                ArticleId = 3,
                CategoryId = 2,
                Header = "Pad Thai",
                Summary = "The Most Famous Thai Dish, In Less Than 30 Minutes",
                DateCreated = new DateTime(2019, 5, 22, 10, 30, 00),
                HomeImageUrl = "https://minimalistbaker.com/wp-content/uploads/2019/01/Easy-Vegan-Pad-Thai-SQUARE.jpg",
                Location = "Bangkok, Thailand",
                IsShowMap = true,
                Content = ArticleDataStrings.PadThaiRecipe,
            });

            modelBuilder.Entity<Article>().HasData(new Article
            {
                ArticleId = 4,
                CategoryId = 2,
                Header = "Sushi",
                Summary = "The Famous California Roll, Easy and Delicious",
                DateCreated = new DateTime(2019, 5, 22, 22, 40, 00),
                HomeImageUrl = "https://img.bestrecipes.com.au/Gd8Mdexr/br/2017/05/cucumber-salmon-sushi-rolls-xlarge-ori-jpg-515020-1.jpg",
                Location = "Tokyo, Japan",
                IsShowMap = true,
                Content = ArticleDataStrings.SushiRecipe,
            });

            modelBuilder.Entity<Article>().HasData(new Article
            {
                ArticleId = 5,
                CategoryId = 3,
                Header = "Hummus",
                Summary = "The Dip of The Gods",
                DateCreated = new DateTime(2019, 5, 22, 3, 55, 00),
                HomeImageUrl = "https://www.cookingclassy.com/wp-content/uploads/2014/03/hummus-31.jpg",
                Location = "Jerusalem, Israel",
                IsShowMap = true,
                Content = ArticleDataStrings.HummusRecipe,
            });

            modelBuilder.Entity<Article>().HasData(new Article
            {
                ArticleId = 6,
                CategoryId = 3,
                Header = "Falafel",
                Summary = "Little Brown Balls of Happiness",
                DateCreated = new DateTime(2019, 1, 28, 16, 25, 00),
                HomeImageUrl = "https://img.buzzfeed.com/thumbnailer-prod-us-east-1/5937e1569b514f058d91ace7cc976140/BFV37212_FalafelTwoWays_TastyVeg_v4_1.jpg",
                Location = "Istanbul, Turkey",
                IsShowMap = true,
                Content = ArticleDataStrings.FalafelRecipe,
            });

            modelBuilder.Entity<Article>().HasData(new Article
            {
                ArticleId = 7,
                CategoryId = 4,
                Header = "Nachos",
                Summary = "Having People Over? Now You Have The Answer",
                DateCreated = new DateTime(2019, 3, 9, 18, 12, 00),
                HomeImageUrl = "https://thebusybaker.ca/wp-content/uploads/2017/12/rainbow-vegetable-skillet-nachos-fbig1.jpg",
                Location = "Mexico City, Mexico",
                IsShowMap = true,
                Content = ArticleDataStrings.NachosRecipe,
            });

            modelBuilder.Entity<Article>().HasData(new Article
            {
                ArticleId = 8,
                CategoryId = 4,
                Header = "Churros",
                Summary = "Once You Try One, You Won't Be Able to Have Enough!",
                DateCreated = new DateTime(2019, 5, 22, 8, 5, 00),
                HomeImageUrl = "https://bakingamoment.com/wp-content/uploads/2019/04/IMG_4307-how-to-make-churros.jpg",
                Location = "Mexico City, Mexico",
                IsShowMap = true,
                Content = ArticleDataStrings.ChorrusRecipe,
            });

            modelBuilder.Entity<Article>().HasData(new Article
            {
                ArticleId = 9,
                CategoryId = 5,
                Header = "Potato Salad",
                Summary = "Don't Be Mistaken, These Aren't Just Mesahed Potatos",
                DateCreated = new DateTime(2019, 1, 28, 14, 00, 00),
                HomeImageUrl = "https://hips.hearstapps.com/hmg-prod/images/190411-potato-salad-horizontal-1-1555688422.png",
                Location = "Denver, Colorado, United States",
                IsShowMap = true,
                Content = ArticleDataStrings.ChorrusRecipe,
            });

            modelBuilder.Entity<Article>().HasData(new Article
            {
                ArticleId = 10,
                CategoryId = 5,
                Header = "Apple Pie",
                Summary = "Can You Really Get More American Than Apple Pie?",
                DateCreated = new DateTime(2019, 5, 22, 20, 30, 00),
                HomeImageUrl = "https://cdn3.tmbi.com/toh/GoogleImagesPostCard/exps6086_HB133235C07_19_4b_WEB.jpg",
                Location = "New York, New York, United Stated",
                IsShowMap = true,
                Content = ArticleDataStrings.ChorrusRecipe,
            });

            modelBuilder.Entity<Article>().HasData(new Article
            {
                ArticleId = 11,
                CategoryId = 1,
                Header = "Ravioli",
                Summary = "Delicious Spinach Artichoke Bake, Yummy!",
                DateCreated = new DateTime(2019, 12, 15, 21, 30, 00),
                HomeImageUrl = "https://i0.wp.com/kitchendocs.com/wp-content/uploads/2018/05/IMG_0344_1.jpg?resize=1170%2C878",
                Location = "Milano, Italy",
                IsShowMap = true,
                Content = ArticleDataStrings.RavioliRecipe,
            });
        }
    }
}
