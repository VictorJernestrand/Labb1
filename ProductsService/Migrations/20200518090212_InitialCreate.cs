using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductsService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    ImageURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageURL", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Wobbler, mini, grön", "https://www.happyangler.se/i/hkg/022677210766_1/rapala-ultra-light-crank-3-cm-wobbler.jpg?$flyout$", "Wobbler", 59.00m },
                    { 2, "rapala Shadow, 3-pack, 11 cm, olikfärgade", "https://www.sportfiskeprylar.se/bilder/artiklar/zoom/SHADOWKIT2_1.jpg", "Rapala Shadow, 3-pack", 199.00m },
                    { 3, "Spinnare, 3-pack, svart, silver, koppar", "https://bojdaspon.se/wp-content/uploads/2020/01/Abu-Droppen-Spinnare-3-Pack.jpg", "Spinnare, 3-pack", 79.00m },
                    { 4, "Hi-Lo Wobbler, Abu Garcia, 11 cm", "https://www.happyangler.se/i/hkg/036282948877_1/abu-garcia-hi-lo-flytande-11-cm-wobbler.jpg", "Abu Garcia Hi-Lo Wobbler", 99.00m },
                    { 5, "Fjäderjiggar, Tungsten, 5-pack, olikfärgade", "https://cdn.shopify.com/s/files/1/1218/2078/products/Untitled_design_3_1024x1024.png?v=1508789934", "Fjäderjiggar 5-pack", 129.00m },
                    { 6, "Fiskelina, nylon, Stroft, 100m, 0.25 diam, 6.40kg", "https://fiskedags.se/5961-large_default/stroft-nylon-fiskelina.jpg", "Stroft Fiskelina, nylon", 59.00m },
                    { 7, "Fiskelina, flätad, Berkely FireLine, 110m, 0.12 diam, 10.20kg", "https://www.jula.se/globalassets/catalog/productimages/722130.jpg?width=458&height=458&scale=both&bgcolor=white", "FireLine, flätad", 145.00m },
                    { 8, "Fiskespö, teleskop, 1.8-3.6m, kolfiber", "https://imgaz2.staticbg.com/thumb/large/oaupload/banggood/images/5A/7A/07658788-500d-4359-923a-ba88e4864376.jpg", "Leo GT400, teleskopspö", 549.00m },
                    { 9, "Pimpelspö, mini, ultralätt", "https://i5.walmartimages.com/asr/8605a207-3410-44a1-93d9-b4eef11b9682_1.2cc67ccd8c20b9016c1e9a5c91d4bea7.jpeg", "Pimpelspö, mini", 199.00m },
                    { 10, "Fiskehåv, 54x28cm", "https://shop.kvibergs.se/wp-content/uploads/2017/11/1519.jpg", "Fiskehåv", 99.00m },
                    { 11, "Fiskevåg, Berkley, digital, max 23kg", "https://www.jula.se/globalassets/catalog/productimages/771026.jpg?width=458&height=458&scale=both&bgcolor=white", "Berkley Fiskevåg", 249.00m },
                    { 12, "Fiskekniv, Rapala, 4\"", "https://www.batofiske.se/pub_images/large/02646_1013.jpg", "Rapala Fiskekniv", 249.00m },
                    { 13, "Abu Garcia Spinnare, 12g, Reflex, Röd", "https://www.happyangler.se/i/hkg/036282342743_1/abu-reflex-red-12-g-spinnare.jpg?$flyout$", "Abu Garcia Spinnare", 59.00m },
                    { 14, "Skeddrag, Toby, Abu Garcia 3-pack, olikfärgade", "https://www.happyangler.se/i/hkg/036282852020_1/abu-garcia-toby-3-pack-skeddragset.jpg?$flyout$", "Toby Skeddrag 3-pack", 59.00m },
                    { 15, "Fiskelåda med 3 uppfällbara sorteringsfack, snäpplås, uppfällbart handtag", "https://www.jula.se/globalassets/catalog/productimages/770028b.jpg?width=458&height=458&scale=both&bgcolor=white", "Fiskelåda", 199.00m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
