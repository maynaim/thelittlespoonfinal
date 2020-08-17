using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheLittleSpoon.Migrations
{
    public partial class AddedAnotherArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "ArticleId", "CategoryId", "Content", "DateCreated", "Header", "HomeImageUrl", "IsShowMap", "Location", "Summary" },
                values: new object[] { 11, 1, @"

<u><b>Ingredients:</b></u>

1) 8 oz cream cheese (225 g)

2) 1 ½ cups shredded mozzarella cheese (150 g)

3) 5 oz spinach (25 g), cooked and chopped

4) 16 oz artichoke heart (455 g)

5) ½ cup grated parmesan cheese (55 g)

6) ¾ cup sour cream (170 g)

7) 1 cup milk (240 mL)

8) 20 oz frozen ravioli (570 g)




<u><b>Preparation:</b></u>

1) Preheat the oven to 375ºF (190ºC).

2) In a large bowl, mix together the cream cheese, mozzarella, spinach, artichoke hearts, Parmesan cheese, sour cream, and milk.

3) Pour a third of the spinach artichoke mixture in a large glass baking dish and spread it evenly over the bottom.

4) Line the tray with half of the frozen ravioli, spread more sauce over the top, then add the rest of the ravioli and cover with the remaining sauce.

5) Bake for 30–35 minutes, or until browned.



", new DateTime(2019, 10, 15, 21, 30, 0, 0, DateTimeKind.Unspecified), "Ravioli", "https://i0.wp.com/kitchendocs.com/wp-content/uploads/2018/05/IMG_0344_1.jpg?resize=1170%2C878", true, "Milano, Italy", "Delicious Spinach Artichoke Bake, Yummy!" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "ArticleId",
                keyValue: 11);
        }
    }
}
