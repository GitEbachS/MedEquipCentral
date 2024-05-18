using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MedEquipCentral.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobFunctions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobFunctions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Image = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    JobFunctionId = table.Column<int>(type: "integer", nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    IsBizOwner = table.Column<bool>(type: "boolean", nullable: false),
                    Uid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_JobFunctions_JobFunctionId",
                        column: x => x.JobFunctionId,
                        principalTable: "JobFunctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CloseDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsClosed = table.Column<bool>(type: "boolean", nullable: false),
                    CreditCardNumber = table.Column<long>(type: "bigint", nullable: true),
                    ExpirationDate = table.Column<string>(type: "text", nullable: true),
                    CVV = table.Column<int>(type: "integer", nullable: true),
                    Zip = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductUser",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUser", x => new { x.ProductsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ProductUser_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CommentReview = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SimilarItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SimilarProductId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimilarItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimilarItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SimilarItems_Products_SimilarProductId",
                        column: x => x.SimilarProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SimilarItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Wound Care" },
                    { 2, "IV Therapy" },
                    { 3, "First Aid" },
                    { 4, "Diabetes Care" },
                    { 5, "Home Health Care" },
                    { 6, "Mom & Baby Care" },
                    { 7, "Mobility" },
                    { 8, "Hearing Aids & Amplifiers" },
                    { 9, "Physical Therapy" },
                    { 10, "Scrubs" },
                    { 11, "Protection Wear" },
                    { 12, "Respiratory" },
                    { 13, "Ostomy & Urology Supplies" }
                });

            migrationBuilder.InsertData(
                table: "JobFunctions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Physician" },
                    { 2, "Nurse Practitioner" },
                    { 3, "Registered Nurse" },
                    { 4, "Licensed Practical Nurse (LPN)" },
                    { 5, "Certified Nursing Assistant (CNA)" },
                    { 6, "Medical Assistant" },
                    { 7, "Surgeon" },
                    { 8, "Anesthesiologist" },
                    { 9, "Pharmacist" },
                    { 10, "Physical Therapist" },
                    { 11, "Occupational Therapist" },
                    { 12, "Speech-Language Pathologist" },
                    { 13, "Dietitian/Nutritionist" },
                    { 14, "Respiratory Therapist" },
                    { 15, "Radiologic Technologist" },
                    { 16, "Medical Laboratory Technician" },
                    { 17, "Emergency Medical Technician (EMT)" },
                    { 18, "Paramedic" },
                    { 19, "Surgical Technologist" },
                    { 20, "Healthcare Administrator" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Image", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "High-quality adhesive bandages for minor cuts and scrapes.", "https://m.media-amazon.com/images/I/91D2FTeGRQL.__AC_SX300_SY300_QL70_FMwebp_.jpg", "Adhesive Bandages", 4.99m },
                    { 2, 2, "Comprehensive IV start kit with all necessary components.", "https://www.life-assist.com/Content/ProductImages/960x720/04_IT8052_01.jpg", "IV Start Kit", 12.99m },
                    { 3, 3, "Complete first aid kit for handling minor injuries.", "https://www.life-assist.com/Content/ProductImages/460x345/05_fj8000new.jpg", "First Aid Kit", 25.00m },
                    { 4, 4, "Accurate and easy-to-use blood glucose monitor for diabetes management.", "https://www.life-assist.com/Content/ProductImages/460x345/02_CHI708050_tstStrip.jpg", "Blood Glucose Monitor", 29.99m },
                    { 5, 5, "Essential health care kit for home use.", "https://i5.walmartimages.com/seo/Physical-Therapy-Home-Health-Aide-Kit-with-Home-Health-Call-Bag-for-Nurses-Home-Health-Aides-Physical-Therapy-Patient-Care_d9edc7e8-7ecb-4f7b-a7ef-3d7574236d7b.66cf77d8d12e2ad4af7201e2816d2236.jpeg?odnHeight=640&odnWidth=640&odnBg=FFFFFF", "Home Health Care Kit", 45.00m },
                    { 6, 6, "Safe and accurate thermometer for babies.", "https://i5.walmartimages.com/seo/Metene-Forehead-Ear-Thermometer-Infrared-Thermometer-Baby-Kid-Adult-1s-Fast-Reading-2-Colors-Backlight-Fever-Alarm-20-Memories-Recall_5096cdf6-8a39-4237-bc22-e7d205b891e1.039d536705d4c3d17531552adb2fbed5.jpeg?odnHeight=640&odnWidth=640&odnBg=FFFFFF", "Baby Thermometer", 15.99m },
                    { 7, 7, "Durable and comfortable wheelchair for mobility assistance.", "https://m.media-amazon.com/images/I/81yOo8IZN7L._AC_SL1500_.jpg", "Wheelchair", 150.00m },
                    { 8, 8, "High-quality hearing aid for improved sound clarity.", "https://www.jabraenhance.com/cdn-cgi/image/width=1920,quality=90,format=auto/_next/static/media/pdp-gallery-champagne-pair-d.bd6c4aa6.jpg", "Hearing Aid", 299.99m },
                    { 9, 9, "Set of resistance bands for physical therapy exercises.", "https://m.media-amazon.com/images/I/61QCIC8XbEL._SL1500_.jpg", "Physical Therapy Bands", 19.99m },
                    { 10, 10, "Comfortable and durable medical scrubs.", "https://i5.walmartimages.com/seo/Dagacci-Medical-Uniform-Unisex-Scrubs-Set-Scrub-Top-and-Pants_ba9c0f34-3655-402a-a11a-622e774fe77c_1.dd2e4f217c097ccbba902dc77a0e6e66.jpeg?odnHeight=2000&odnWidth=2000&odnBg=FFFFFF", "Medical Scrubs", 30.00m },
                    { 11, 11, "Protective face shield for safety and hygiene.", "https://m.media-amazon.com/images/I/51CoS3sVsSL._SL1000_.jpg", "Face Shield", 5.99m },
                    { 12, 12, "Efficient nebulizer for respiratory therapy.", "https://omronhealthcare.com/wp-content/uploads/NEC801-FRONT-600x600-1.jpg", "Nebulizer", 60.00m },
                    { 13, 13, "High-quality ostomy bag for reliable use.", "https://m.media-amazon.com/images/I/71Xi70KyZUL._AC_SL1500_.jpg", "Ostomy Bag", 20.00m },
                    { 14, 1, "Hydrocolloid dressing for moist wound healing.", "https://m.media-amazon.com/images/I/51MfItI2NlL._SL1001_.jpg", "Hydrocolloid Dressing", 7.99m },
                    { 15, 2, "Complete IV drip set for intravenous therapy.", "https://cf1.bettymills.com/store/images/product/500/INDTCRTCBINF033G.JPG", "IV Drip Set", 9.99m },
                    { 16, 3, "Compact emergency blanket for warmth and protection.", "https://img.kwcdn.com/product/fancy/5b446367-727e-45bb-91f4-1b01d60b29ab.jpg?imageView2/2/w/650/q/50/format/webp", "Emergency Blanket", 3.49m },
                    { 17, 4, "Easy-to-use insulin pen for diabetes management.", "https://www.adwdiabetes.com/images/autopen-delivery-system-blue.jpg", "Insulin Pen", 39.99m },
                    { 18, 5, "Portable oxygen concentrator for respiratory support.", "https://mainclinicsupply.com/cdn/shop/products/G5_300x300.jpg?v=1704317853", "Portable Oxygen Concentrator", 199.99m },
                    { 19, 6, "High-definition baby monitor for parents' peace of mind.", "https://m.media-amazon.com/images/I/71q6DhYbHEL.__AC_SY300_SX300_QL70_FMwebp_.jpg", "Baby Monitor", 75.00m },
                    { 20, 7, "Adjustable crutches for mobility assistance.", "https://m.media-amazon.com/images/I/416k+il1P6L._AC_SX569_.jpg", "Crutches", 40.00m },
                    { 21, 8, "Compact personal sound amplifier for enhanced hearing.", "https://example.com/images/products/sound_amplifier.jpg", "Personal Sound Amplifier", 59.99m },
                    { 22, 9, "Durable exercise ball for physical therapy and workouts.", "https://m.media-amazon.com/images/I/51T5RPDAS1L.__AC_SY300_SX300_QL70_FMwebp_.jpg", "Exercise Ball", 25.99m },
                    { 23, 10, "Comfortable nurse uniform for professional use.", "https://m.media-amazon.com/images/I/51A9Xx2EVPS._AC_SX679_.jpg", "Nurse Uniform", 45.00m },
                    { 24, 11, "Disposable gowns for safety and hygiene in medical settings.", "https://m.media-amazon.com/images/I/41iMfJateLL._SY445_SX342_QL70_FMwebp_.jpg", "Disposable Gowns", 10.00m },
                    { 25, 12, "Reliable CPAP machine for sleep apnea treatment.", "https://m.media-amazon.com/images/I/41D0DqnhtxL._SY445_SX342_QL70_FMwebp_.jpg", "CPAP Machine", 300.00m },
                    { 26, 13, "High-quality urinary catheter for urological care.", "https://example.com/images/products/urinary_catheter.jpg", "Urinary Catheter", 12.50m },
                    { 27, 1, "Sterile gauze pads for wound dressing.", "https://m.media-amazon.com/images/I/61a31ft455L.__AC_SY300_SX300_QL70_FMwebp_.jpg", "Sterile Gauze Pads", 5.99m },
                    { 28, 2, "IV extension set for flexible intravenous therapy.", "https://m.media-amazon.com/images/I/61dmwO5FNKL.__AC_SX300_SY300_QL70_FMwebp_.jpg", "IV Extension Set", 8.49m },
                    { 29, 3, "Portable CPR face shield for emergency resuscitation.", "https://m.media-amazon.com/images/I/61EgsjpRNZL._SX466_.jpg", "CPR Face Shield", 2.99m },
                    { 30, 4, "Lancet device for blood glucose testing.", "https://m.media-amazon.com/images/I/51ujEopXxnL._SY445_SX342_QL70_FMwebp_.jpg", "Lancet Device", 14.99m },
                    { 31, 1, "Soothing burn gel for minor burns and scalds.", "https://m.media-amazon.com/images/I/51Ndif1TOCL._SX466_.jpg", "Burn Gel", 7.99m },
                    { 32, 2, "High-quality IV catheter for safe and efficient intravenous access.", "https://m.media-amazon.com/images/I/51BB53P2ZGL._SX300_SY300_QL70_FMwebp_.jpg", "IV Catheter", 4.50m },
                    { 33, 5, "Convenient bedside commode for patients with mobility challenges.", "https://m.media-amazon.com/images/I/51jlhB25g1L.__AC_SX300_SY300_QL70_FMwebp_.jpg", "Bedside Commode", 60.00m },
                    { 34, 4, "Sterile insulin pen needles for diabetes management.", "https://m.media-amazon.com/images/I/61rUPZsZzeS._SX466_.jpg", "Insulin Pen Needles", 14.99m },
                    { 35, 5, "Reliable blood pressure cuff for accurate readings.", "https://m.media-amazon.com/images/I/61o44znqdPL._AC_SX569_.jpg", "Blood Pressure Cuff", 22.00m },
                    { 36, 6, "Gentle and moisturizing baby wipes for sensitive skin.", "https://m.media-amazon.com/images/I/41F1ayv1PbL._SY300_SX300_QL70_FMwebp_.jpg", "Baby Wipes", 3.99m },
                    { 37, 7, "Sturdy walker for enhanced mobility support.", "https://m.media-amazon.com/images/I/61qQ86d-jOL.__AC_SX300_SY300_QL70_FMwebp_.jpg", "Walker", 55.00m },
                    { 38, 8, "Advanced digital hearing aid for better sound clarity.", "https://m.media-amazon.com/images/I/41QU5Ibd9CL._SY445_SX342_QL70_FMwebp_.jpg", "Digital Hearing Aid", 350.00m },
                    { 39, 9, "High-density foam roller for muscle recovery and therapy.", "https://m.media-amazon.com/images/I/714oDUEbv+L._AC_SY300_SX300_.jpg", "Foam Roller", 18.99m },
                    { 40, 10, "Comfortable and durable surgical scrubs.", "https://m.media-amazon.com/images/I/51vf7sWndyL._AC_SY679_.jpg", "Surgical Scrubs", 32.00m },
                    { 41, 11, "Disposable protective gown for medical use.", "https://m.media-amazon.com/images/I/41tg-F8qEwL._SX342_SY445_QL70_FMwebp_.jpg", "Protective Gown", 6.50m },
                    { 42, 12, "Portable oxygen concentrator for respiratory support.", "https://m.media-amazon.com/images/I/71b8PsORslL.__AC_SX300_SY300_QL70_FMwebp_.jpg", "Portable Oxygen Concentrator", 450.00m },
                    { 43, 7, "Portable folding cane for stability and support while walking.", "https://m.media-amazon.com/images/I/61TC-I8LR9L.__AC_SX300_SY300_QL70_FMwebp_.jpg", "Folding Cane", 18.00m },
                    { 44, 1, "Sterile wound dressing for effective healing.", "https://m.media-amazon.com/images/I/81nIh4kL9TL.__AC_SX300_SY300_QL70_FMwebp_.jpg", "Wound Dressing", 12.99m },
                    { 45, 2, "IV fluid bag for hydration and medication administration.", "https://m.media-amazon.com/images/I/514nbXKja2L.__AC_SX300_SY300_QL70_FMwebp_.jpg", "IV Fluid Bag", 8.50m },
                    { 46, 4, "Disposable blood glucose test strips for accurate glucose monitoring.", "https://m.media-amazon.com/images/I/41Ld08nwkdL._SY445_SX342_QL70_FMwebp_.jpg", "Blood Glucose Test Strips", 29.99m },
                    { 47, 4, "Sterile lancets for blood glucose testing.", "https://m.media-amazon.com/images/I/71P6oFj3KRL.__AC_SX300_SY300_QL70_FMwebp_.jpg", "Lancets", 9.99m },
                    { 48, 5, "Sturdy and adjustable portable shower chair.", "https://m.media-amazon.com/images/I/61pz5TDKuwL.__AC_SX300_SY300_QL70_FMwebp_.jpg", "Portable Shower Chair", 45.00m },
                    { 49, 6, "Gentle and effective nasal aspirator for infants.", "https://m.media-amazon.com/images/I/61IR8qxaxBL.__AC_SX300_SY300_QL70_FMwebp_.jpg", "Infant Nasal Aspirator", 12.00m },
                    { 50, 6, "Electric breast pump for efficient and comfortable milk expression.", "https://m.media-amazon.com/images/I/415zba+iXwL._SY300_SX300_.jpg", "Breast Pump", 120.00m },
                    { 51, 9, "Ergonomically designed orthopedic pillow for neck and spine support.", "https://m.media-amazon.com/images/I/61mdPtC0iIL.__AC_SX300_SY300_QL70_FMwebp_.jpg", "Orthopedic Pillow", 29.99m },
                    { 52, 1, "Stretchable compression bandage for wound management and support.", "https://m.media-amazon.com/images/I/614k6uqVbFL.__AC_SX300_SY300_QL70_FMwebp_.jpg", "Compression Bandage", 8.50m },
                    { 53, 2, "Complete IV infusion kit for home-based intravenous therapy.", "https://m.media-amazon.com/images/I/810m93ND8gL._AC_SX679_.jpg", "Home IV Infusion Kit", 34.99m },
                    { 54, 3, "Comprehensive first aid manual for emergency preparedness.", "https://m.media-amazon.com/images/I/81G4bfha0lL.__AC_SX300_SY300_QL70_FMwebp_.jpg", "Emergency First Aid Manual", 12.99m },
                    { 55, 4, "Portable cooling case for insulin storage during travel.", "https://m.media-amazon.com/images/I/71+qmT0A1JL._AC_SX569_.jpg", "Insulin Cooling Case", 19.99m },
                    { 56, 12, "Compact and portable nebulizer for on-the-go respiratory therapy.", "https://m.media-amazon.com/images/I/61tSm0np5HL.__AC_SX300_SY300_QL70_FMwebp_.jpg", "Portable Nebulizer", 69.99m },
                    { 57, 13, "Sterile urological drain bag for urinary collection and management.", "https://m.media-amazon.com/images/I/61wDA7XwFnS._AC_SY741_.jpg", "Urological Drain Bag", 15.99m },
                    { 58, 9, "Versatile knee support brace with adjustable straps for customized fit.", "https://m.media-amazon.com/images/I/61A4vopvL7S.__AC_SX300_SY300_QL70_FMwebp_.jpg", "Adjustable Knee Support Brace", 24.99m },
                    { 59, 10, "Comfortable and breathable scrub caps for healthcare professionals.", "https://m.media-amazon.com/images/I/61-0J1+qCeL._AC_SX522_.jpg", "Reusable Scrub Caps", 9.99m },
                    { 60, 11, "Washable and reusable face masks for daily protection.", "https://m.media-amazon.com/images/I/41iS6HxvXnL._SY445_SX342_QL70_FMwebp_.jpg", "Reusable Face Masks", 7.99m },
                    { 61, 12, "Comfortable nasal mask for CPAP therapy in sleep apnea.", "https://m.media-amazon.com/images/I/4113F7qKOqL.__AC_SY300_SX300_QL70_FMwebp_.jpg", "CPAP Nasal Mask", 49.99m },
                    { 62, 13, "Adjustable straps for securing urinary leg bags comfortably.", "https://m.media-amazon.com/images/I/61F46vG4ECL.__AC_SX300_SY300_QL70_FMwebp_.jpg", "Urinary Leg Bag Straps", 8.50m },
                    { 63, 9, "Powerful muscle massage gun for deep tissue relaxation and recovery.", "https://m.media-amazon.com/images/I/71ILpYb00uL.__AC_SX300_SY300_QL70_FMwebp_.jpg", "Muscle Massage Gun", 79.99m },
                    { 64, 11, "Effective antibacterial hand sanitizer gel for hand hygiene.", "https://m.media-amazon.com/images/I/51COxBcVmML._SY445_SX342_QL70_FMwebp_.jpg", "Antibacterial Hand Sanitizer", 6.99m },
                    { 65, 5, "Portable and easy-to-use blood pressure monitor for home monitoring.", "https://m.media-amazon.com/images/I/714ytBdz2nL.__AC_SX300_SY300_QL70_FMwebp_.jpg", "Compact Blood Pressure Monitor", 39.99m },
                    { 66, 6, "Comfortable postpartum belly wrap for abdominal support and recovery.", "https://m.media-amazon.com/images/I/41eYV5uftZL._SY445_SX342_QL70_FMwebp_.jpg", "Postpartum Belly Wrap", 19.99m },
                    { 67, 3, "Fast and accurate digital thermometer for temperature measurement.", "https://m.media-amazon.com/images/I/51sO-p5+cQL._AC_SY300_SX300_.jpg", "Digital Thermometer", 9.99m },
                    { 68, 1, "Soft and breathable eye patch for post-operative or injury use.", "https://m.media-amazon.com/images/I/61VVJsEKlsL.__AC_SY300_SX300_QL70_FMwebp_.jpg", "Eye Patch", 3.49m },
                    { 69, 2, "Securement device for IV catheters to prevent dislodgement and movement.", "https://m.media-amazon.com/images/I/61f37xDXKrL.__AC_SX300_SY300_QL70_FMwebp_.jpg", "IV Catheter Securement Device", 5.99m },
                    { 70, 11, "Disposable shoe covers for cleanliness and contamination control.", "https://m.media-amazon.com/images/I/71vWf-Mw5lL.__AC_SX300_SY300_QL70_FMwebp_.jpg", "Disposable Shoe Covers", 12.99m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Email", "FirstName", "Image", "IsAdmin", "IsBizOwner", "JobFunctionId", "LastName", "Uid" },
                values: new object[,]
                {
                    { 1, "123 Main Street", "john.doe@example.com", "John", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRtUgBeWHwxqsREgJJ-2XJkV2S34fVpJz63b1AWjYHDgg&s", true, true, 3, "Doe", "JD123" },
                    { 2, "456 Elm Street", "jane.smith@example.com", "Jane", "https://headshots-inc.com/wp-content/uploads/2021/06/what-is-a-professional-headshot-example-header.jpg", false, true, 9, "Smith", "JS456" },
                    { 3, "789 Oak Street", "alice.johnson@example.com", "Alice", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQPqh65K5r4_clBI27OhCX9_ULfL-dmy7kcVva_o-3-bg&s", false, true, 6, "Johnson", "AJ789" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CVV", "CloseDate", "CreditCardNumber", "ExpirationDate", "IsClosed", "UserId", "Zip" },
                values: new object[,]
                {
                    { 1, 123, new DateTime(2024, 5, 16, 13, 40, 17, 562, DateTimeKind.Local).AddTicks(2733), 1234567890123456L, "12/25", true, 1, 12345 },
                    { 2, 456, new DateTime(2024, 5, 17, 13, 40, 17, 562, DateTimeKind.Local).AddTicks(2790), 9876543210987654L, "03/27", true, 2, 54321 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "CommentReview", "DateCreated", "ProductId", "Rating", "UserId" },
                values: new object[,]
                {
                    { 1, "Great product!", new DateTime(2024, 5, 18, 18, 40, 17, 562, DateTimeKind.Utc).AddTicks(2823), 1, 4, 1 },
                    { 2, "Excellent quality!", new DateTime(2024, 5, 18, 18, 40, 17, 562, DateTimeKind.Utc).AddTicks(2825), 2, 5, 2 }
                });

            migrationBuilder.InsertData(
                table: "SimilarItems",
                columns: new[] { "Id", "ProductId", "SimilarProductId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 2, 1 },
                    { 2, 2, 3, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUser_UsersId",
                table: "ProductUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SimilarItems_ProductId",
                table: "SimilarItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SimilarItems_SimilarProductId",
                table: "SimilarItems",
                column: "SimilarProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SimilarItems_UserId",
                table: "SimilarItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_JobFunctionId",
                table: "Users",
                column: "JobFunctionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "ProductUser");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "SimilarItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "JobFunctions");
        }
    }
}
