using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using MedEquipCentral.Models;

    public class MedEquipCentralDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<JobFunction> JobFunctions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<SimilarItem> SimilarItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        modelBuilder.Entity<SimilarItem>()
           .HasKey(si => new { si.Id }); 

        modelBuilder.Entity<SimilarItem>()
            .HasOne(si => si.Product)
            .WithMany(p => p.SimilarItems)
            .HasForeignKey(si => si.ProductId)
            .OnDelete(DeleteBehavior.Cascade); 

        modelBuilder.Entity<SimilarItem>()
            .HasOne(si => si.SimilarProduct)
            .WithMany()
            .HasForeignKey(si => si.SimilarProductId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Product>()
        .HasMany(p => p.SimilarItems)
        .WithOne(si => si.Product)
        .OnDelete(DeleteBehavior.Cascade);

        
        modelBuilder.Entity<JobFunction>().HasData(new JobFunction[]
        {
        new JobFunction { Id = 1, Name = "Physician" },
        new JobFunction { Id = 2, Name = "Nurse Practitioner" },
        new JobFunction { Id = 3, Name = "Registered Nurse" },
        new JobFunction { Id = 4, Name = "Licensed Practical Nurse (LPN)" },
        new JobFunction { Id = 5, Name = "Certified Nursing Assistant (CNA)" },
        new JobFunction { Id = 6, Name = "Medical Assistant" },
        new JobFunction { Id = 7, Name = "Surgeon" },
        new JobFunction { Id = 8, Name = "Anesthesiologist" },
        new JobFunction { Id = 9, Name = "Pharmacist" },
        new JobFunction { Id = 10, Name = "Physical Therapist" },
        new JobFunction { Id = 11, Name = "Occupational Therapist" },
        new JobFunction { Id = 12, Name = "Speech-Language Pathologist" },
        new JobFunction { Id = 13, Name = "Dietitian/Nutritionist" },
        new JobFunction { Id = 14, Name = "Respiratory Therapist" },
        new JobFunction { Id = 15, Name = "Radiologic Technologist" },
        new JobFunction { Id = 16, Name = "Medical Laboratory Technician" },
        new JobFunction { Id = 17, Name = "Emergency Medical Technician (EMT)" },
        new JobFunction { Id = 18, Name = "Paramedic" },
        new JobFunction { Id = 19, Name = "Surgical Technologist" },
        new JobFunction { Id = 20, Name = "Healthcare Administrator" }

        });

        modelBuilder.Entity<User>().HasData(new User[]
        {
            new User
    {
        Id = 1,
        Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRtUgBeWHwxqsREgJJ-2XJkV2S34fVpJz63b1AWjYHDgg&s",
        FirstName = "John",
        LastName = "Doe",
        Email = "john.doe@example.com",
        Address = "123 Main Street",
        JobFunctionId = 3, // Registered Nurse
        IsAdmin = true,
        IsBizOwner = true,
        Uid = "JD123"
    },
    new User
    {
        Id = 2,
        Image = "https://headshots-inc.com/wp-content/uploads/2021/06/what-is-a-professional-headshot-example-header.jpg",
        FirstName = "Jane",
        LastName = "Smith",
        Email = "jane.smith@example.com",
        Address = "456 Elm Street",
        JobFunctionId = 9, // Pharmacist
        IsAdmin = false,
        IsBizOwner = true,
        Uid = "JS456"
    },
    new User
    {
        Id = 3,
        Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQPqh65K5r4_clBI27OhCX9_ULfL-dmy7kcVva_o-3-bg&s",
        FirstName = "Alice",
        LastName = "Johnson",
        Email = "alice.johnson@example.com",
        Address = "789 Oak Street",
        JobFunctionId = 6, // Medical Assistant
        IsAdmin = false,
        IsBizOwner = true,
        Uid = "AJ789"
    }

        });

        modelBuilder.Entity<Category>().HasData(new Category[]
{
    new Category
    {
        Id = 1,
        Name = "Wound Care"
    },
    new Category
    {
        Id = 2,
        Name = "IV Therapy"
    },
    new Category
    {
        Id = 3,
        Name = "First Aid"
    },
    new Category
    {
        Id = 4,
        Name = "Diabetes Care"
    },
    new Category
    {
        Id = 5,
        Name = "Home Health Care"
    },
    new Category
    {
        Id = 6,
        Name = "Mom & Baby Care"
    },
    new Category
    {
        Id = 7,
        Name = "Mobility"
    },
    new Category
    {
        Id = 8,
        Name = "Hearing Aids & Amplifiers"
    },
    new Category
    {
        Id = 9,
        Name = "Physical Therapy"
    },
    new Category
    {
        Id = 10,
        Name = "Scrubs"
    },
    new Category
    {
        Id = 11,
        Name = "Protection Wear"
    },
    new Category
    {
        Id = 12,
        Name = "Respiratory"
    },
    new Category
    {
        Id = 13,
        Name = "Ostomy & Urology Supplies"
    }
});

        modelBuilder.Entity<Product>().HasData(new Product[]
        {
    new Product
    {
        Id = 1,
        Name = "Adhesive Bandages",
        Image = "https://m.media-amazon.com/images/I/91D2FTeGRQL.__AC_SX300_SY300_QL70_FMwebp_.jpg",
        CategoryId = 1, // Wound Care
        Description = "High-quality adhesive bandages for minor cuts and scrapes.",
        Price = 4.99M
    },
    new Product
    {
        Id = 2,
        Name = "IV Start Kit",
        Image = "https://m.media-amazon.com/images/I/21bk5VPlZlL.jpg",
        CategoryId = 2, // IV Therapy
        Description = "Comprehensive IV start kit with all necessary components.",
        Price = 12.99M
    },
    new Product
    {
        Id = 3,
        Name = "First Aid Kit",
        Image = "https://firstaidsuppliesonline.com/wp-content/uploads/2023/10/68-BANSI-RR-435x435.png",
        CategoryId = 3, // First Aid
        Description = "Complete first aid kit for handling minor injuries.",
        Price = 25.00M
    },
    new Product
    {
        Id = 4,
        Name = "Blood Glucose Monitor",
        Image = "https://cdn11.bigcommerce.com/s-kslxuc4w/images/stencil/500x659/products/1047/5929/lite_open__68268.1705045702.png?c=2",
        CategoryId = 4, // Diabetes Care
        Description = "Accurate and easy-to-use blood glucose monitor for diabetes management.",
        Price = 29.99M
    },
    new Product
    {
        Id = 5,
        Name = "Home Health Care Kit",
        Image = "https://i5.walmartimages.com/seo/Physical-Therapy-Home-Health-Aide-Kit-with-Home-Health-Call-Bag-for-Nurses-Home-Health-Aides-Physical-Therapy-Patient-Care_d9edc7e8-7ecb-4f7b-a7ef-3d7574236d7b.66cf77d8d12e2ad4af7201e2816d2236.jpeg?odnHeight=640&odnWidth=640&odnBg=FFFFFF",
        CategoryId = 5, // Home Health Care
        Description = "Essential health care kit for home use.",
        Price = 45.00M
    },
    new Product
    {
        Id = 6,
        Name = "Baby Thermometer",
        Image = "https://i5.walmartimages.com/seo/Metene-Forehead-Ear-Thermometer-Infrared-Thermometer-Baby-Kid-Adult-1s-Fast-Reading-2-Colors-Backlight-Fever-Alarm-20-Memories-Recall_5096cdf6-8a39-4237-bc22-e7d205b891e1.039d536705d4c3d17531552adb2fbed5.jpeg?odnHeight=640&odnWidth=640&odnBg=FFFFFF",
        CategoryId = 6, // Mom & Baby Care
        Description = "Safe and accurate thermometer for babies.",
        Price = 15.99M
    },
    new Product
    {
        Id = 7,
        Name = "Wheelchair",
        Image = "https://m.media-amazon.com/images/I/81yOo8IZN7L._AC_SL1500_.jpg",
        CategoryId = 7, // Mobility
        Description = "Durable and comfortable wheelchair for mobility assistance.",
        Price = 150.00M
    },
    new Product
    {
        Id = 8,
        Name = "Hearing Aid",
        Image = "https://www.jabraenhance.com/cdn-cgi/image/width=1920,quality=90,format=auto/_next/static/media/pdp-gallery-champagne-pair-d.bd6c4aa6.jpg",
        CategoryId = 8, // Hearing Aids & Amplifiers
        Description = "High-quality hearing aid for improved sound clarity.",
        Price = 299.99M
    },
    new Product
    {
        Id = 9,
        Name = "Physical Therapy Bands",
        Image = "https://m.media-amazon.com/images/I/61QCIC8XbEL._SL1500_.jpg",
        CategoryId = 9, // Physical Therapy
        Description = "Set of resistance bands for physical therapy exercises.",
        Price = 19.99M
    },
    new Product
    {
        Id = 10,
        Name = "Medical Scrubs",
        Image = "https://i5.walmartimages.com/seo/Dagacci-Medical-Uniform-Unisex-Scrubs-Set-Scrub-Top-and-Pants_ba9c0f34-3655-402a-a11a-622e774fe77c_1.dd2e4f217c097ccbba902dc77a0e6e66.jpeg?odnHeight=2000&odnWidth=2000&odnBg=FFFFFF",
        CategoryId = 10, // Scrubs
        Description = "Comfortable and durable medical scrubs.",
        Price = 30.00M
    },
    new Product
    {
        Id = 11,
        Name = "Face Shield",
        Image = "https://m.media-amazon.com/images/I/51CoS3sVsSL._SL1000_.jpg",
        CategoryId = 11, // Protection Wear
        Description = "Protective face shield for safety and hygiene.",
        Price = 5.99M
    },
    new Product
    {
        Id = 12,
        Name = "Nebulizer",
        Image = "https://omronhealthcare.com/wp-content/uploads/NEC801-FRONT-600x600-1.jpg",
        CategoryId = 12, // Respiratory
        Description = "Efficient nebulizer for respiratory therapy.",
        Price = 60.00M
    },
    new Product
    {
        Id = 13,
        Name = "Ostomy Bag",
        Image = "https://m.media-amazon.com/images/I/71Xi70KyZUL._AC_SL1500_.jpg",
        CategoryId = 13, // Ostomy & Urology Supplies
        Description = "High-quality ostomy bag for reliable use.",
        Price = 20.00M
    },
    new Product
    {
        Id = 14,
        Name = "Hydrocolloid Dressing",
        Image = "https://m.media-amazon.com/images/I/51MfItI2NlL._SL1001_.jpg",
        CategoryId = 1, // Wound Care
        Description = "Hydrocolloid dressing for moist wound healing.",
        Price = 7.99M
    },
    new Product
    {
        Id = 15,
        Name = "IV Drip Set",
        Image = "https://cf1.bettymills.com/store/images/product/500/INDTCRTCBINF033G.JPG",
        CategoryId = 2, // IV Therapy
        Description = "Complete IV drip set for intravenous therapy.",
        Price = 9.99M
    },
    new Product
    {
        Id = 16,
        Name = "Emergency Blanket",
        Image = "https://img.kwcdn.com/product/fancy/5b446367-727e-45bb-91f4-1b01d60b29ab.jpg?imageView2/2/w/650/q/50/format/webp",
        CategoryId = 3, // First Aid
        Description = "Compact emergency blanket for warmth and protection.",
        Price = 3.49M
    },
    new Product
    {
        Id = 17,
        Name = "Insulin Pen",
        Image = "https://www.adwdiabetes.com/images/autopen-delivery-system-blue.jpg",
        CategoryId = 4, // Diabetes Care
        Description = "Easy-to-use insulin pen for diabetes management.",
        Price = 39.99M
    },
    new Product
    {
        Id = 18,
        Name = "Portable Oxygen Concentrator",
        Image = "https://mainclinicsupply.com/cdn/shop/products/G5_300x300.jpg?v=1704317853",
        CategoryId = 5, // Home Health Care
        Description = "Portable oxygen concentrator for respiratory support.",
        Price = 199.99M
    },
    new Product
    {
        Id = 19,
        Name = "Baby Monitor",
        Image = "https://m.media-amazon.com/images/I/71q6DhYbHEL.__AC_SY300_SX300_QL70_FMwebp_.jpg",
        CategoryId = 6, // Mom & Baby Care
        Description = "High-definition baby monitor for parents' peace of mind.",
        Price = 75.00M
    },
    new Product
    {
        Id = 20,
        Name = "Crutches",
        Image = "https://m.media-amazon.com/images/I/416k+il1P6L._AC_SX569_.jpg",
        CategoryId = 7, // Mobility
        Description = "Adjustable crutches for mobility assistance.",
        Price = 40.00M
    },
    new Product
    {
        Id = 21,
        Name = "Personal Sound Amplifier",
        Image = "https://example.com/images/products/sound_amplifier.jpg",
        CategoryId = 8, // Hearing Aids & Amplifiers
        Description = "Compact personal sound amplifier for enhanced hearing.",
        Price = 59.99M
    },
    new Product
    {
        Id = 22,
        Name = "Exercise Ball",
        Image = "https://m.media-amazon.com/images/I/51T5RPDAS1L.__AC_SY300_SX300_QL70_FMwebp_.jpg",
        CategoryId = 9, // Physical Therapy
        Description = "Durable exercise ball for physical therapy and workouts.",
        Price = 25.99M
    },
    new Product
    {
        Id = 23,
        Name = "Nurse Uniform",
        Image = "https://m.media-amazon.com/images/I/51A9Xx2EVPS._AC_SX679_.jpg",
        CategoryId = 10, // Scrubs
        Description = "Comfortable nurse uniform for professional use.",
        Price = 45.00M
    },
    new Product
    {
        Id = 24,
        Name = "Disposable Gowns",
        Image = "https://m.media-amazon.com/images/I/41iMfJateLL._SY445_SX342_QL70_FMwebp_.jpg",
        CategoryId = 11, // Protection Wear
        Description = "Disposable gowns for safety and hygiene in medical settings.",
        Price = 10.00M
    },
    new Product
    {
        Id = 25,
        Name = "CPAP Machine",
        Image = "https://m.media-amazon.com/images/I/41D0DqnhtxL._SY445_SX342_QL70_FMwebp_.jpg",
        CategoryId = 12, // Respiratory
        Description = "Reliable CPAP machine for sleep apnea treatment.",
        Price = 300.00M
    },
    new Product
    {
        Id = 26,
        Name = "Urinary Catheter",
        Image = "https://example.com/images/products/urinary_catheter.jpg",
        CategoryId = 13, // Ostomy & Urology Supplies
        Description = "High-quality urinary catheter for urological care.",
        Price = 12.50M
    },
    new Product
    {
        Id = 27,
        Name = "Sterile Gauze Pads",
        Image = "https://m.media-amazon.com/images/I/61a31ft455L.__AC_SY300_SX300_QL70_FMwebp_.jpg",
        CategoryId = 1, // Wound Care
        Description = "Sterile gauze pads for wound dressing.",
        Price = 5.99M
    },
    new Product
    {
        Id = 28,
        Name = "IV Extension Set",
        Image = "https://m.media-amazon.com/images/I/61dmwO5FNKL.__AC_SX300_SY300_QL70_FMwebp_.jpg",
        CategoryId = 2, // IV Therapy
        Description = "IV extension set for flexible intravenous therapy.",
        Price = 8.49M
    },
    new Product
    {
        Id = 29,
        Name = "CPR Face Shield",
        Image = "https://m.media-amazon.com/images/I/61EgsjpRNZL._SX466_.jpg",
        CategoryId = 3, // First Aid
        Description = "Portable CPR face shield for emergency resuscitation.",
        Price = 2.99M
    },
    new Product
    {
        Id = 30,
        Name = "Lancet Device",
        Image = "https://m.media-amazon.com/images/I/51ujEopXxnL._SY445_SX342_QL70_FMwebp_.jpg",
        CategoryId = 4, // Diabetes Care
        Description = "Lancet device for blood glucose testing.",
        Price = 14.99M
    },
      new Product
    {
        Id = 31,
        Name = "Burn Gel",
        Image = "https://m.media-amazon.com/images/I/51Ndif1TOCL._SX466_.jpg",
        CategoryId = 1, // Wound Care
        Description = "Soothing burn gel for minor burns and scalds.",
        Price = 7.99M
    },
    new Product
    {
        Id = 32,
        Name = "IV Catheter",
        Image = "https://m.media-amazon.com/images/I/51BB53P2ZGL._SX300_SY300_QL70_FMwebp_.jpg",
        CategoryId = 2, // IV Therapy
        Description = "High-quality IV catheter for safe and efficient intravenous access.",
        Price = 4.50M
    },
        new Product
    {
        Id = 33,
        Name = "Bedside Commode",
        Image = "https://m.media-amazon.com/images/I/51jlhB25g1L.__AC_SX300_SY300_QL70_FMwebp_.jpg",
        CategoryId = 5, // Home Health Care
        Description = "Convenient bedside commode for patients with mobility challenges.",
        Price = 60.00M
    },
    new Product
    {
        Id = 34,
        Name = "Insulin Pen Needles",
        Image = "https://m.media-amazon.com/images/I/61rUPZsZzeS._SX466_.jpg",
        CategoryId = 4, // Diabetes Care
        Description = "Sterile insulin pen needles for diabetes management.",
        Price = 14.99M
    },
    new Product
    {
        Id = 35,
        Name = "Blood Pressure Cuff",
        Image = "https://m.media-amazon.com/images/I/61o44znqdPL._AC_SX569_.jpg",
        CategoryId = 5, // Home Health Care
        Description = "Reliable blood pressure cuff for accurate readings.",
        Price = 22.00M
    },
    new Product
    {
        Id = 36,
        Name = "Baby Wipes",
        Image = "https://m.media-amazon.com/images/I/41F1ayv1PbL._SY300_SX300_QL70_FMwebp_.jpg",
        CategoryId = 6, // Mom & Baby Care
        Description = "Gentle and moisturizing baby wipes for sensitive skin.",
        Price = 3.99M
    },
     new Product
    {
        Id = 37,
        Name = "Walker",
        Image = "https://m.media-amazon.com/images/I/61qQ86d-jOL.__AC_SX300_SY300_QL70_FMwebp_.jpg",
        CategoryId = 7, // Mobility
        Description = "Sturdy walker for enhanced mobility support.",
        Price = 55.00M
    },
    new Product
    {
        Id = 38,
        Name = "Digital Hearing Aid",
        Image = "https://m.media-amazon.com/images/I/41QU5Ibd9CL._SY445_SX342_QL70_FMwebp_.jpg",
        CategoryId = 8, // Hearing Aids & Amplifiers
        Description = "Advanced digital hearing aid for better sound clarity.",
        Price = 350.00M
    },
    new Product
    {
        Id = 39,
        Name = "Foam Roller",
        Image = "https://m.media-amazon.com/images/I/714oDUEbv+L._AC_SY300_SX300_.jpg",
        CategoryId = 9, // Physical Therapy
        Description = "High-density foam roller for muscle recovery and therapy.",
        Price = 18.99M
    },
    new Product
    {
        Id = 40,
        Name = "Surgical Scrubs",
        Image = "https://m.media-amazon.com/images/I/51vf7sWndyL._AC_SY679_.jpg",
        CategoryId = 10, // Scrubs
        Description = "Comfortable and durable surgical scrubs.",
        Price = 32.00M
    },
    new Product
    {
        Id = 41,
        Name = "Protective Gown",
        Image = "https://m.media-amazon.com/images/I/41tg-F8qEwL._SX342_SY445_QL70_FMwebp_.jpg",
        CategoryId = 11, // Protection Wear
        Description = "Disposable protective gown for medical use.",
        Price = 6.50M
    },
    new Product
    {
        Id = 42,
        Name = "Portable Oxygen Concentrator",
        Image = "https://m.media-amazon.com/images/I/71b8PsORslL.__AC_SX300_SY300_QL70_FMwebp_.jpg",
        CategoryId = 12, // Respiratory
        Description = "Portable oxygen concentrator for respiratory support.",
        Price = 450.00M
    },
       new Product
    {
        Id = 43,
        Name = "Folding Cane",
        Image = "https://m.media-amazon.com/images/I/61TC-I8LR9L.__AC_SX300_SY300_QL70_FMwebp_.jpg",
        CategoryId = 7, // Mobility
        Description = "Portable folding cane for stability and support while walking.",
        Price = 18.00M
    },
    new Product
    {
        Id = 44,
        Name = "Wound Dressing",
        Image = "https://m.media-amazon.com/images/I/81nIh4kL9TL.__AC_SX300_SY300_QL70_FMwebp_.jpg",
        CategoryId = 1, // Wound Care
        Description = "Sterile wound dressing for effective healing.",
        Price = 12.99M
    },
    new Product
    {
        Id = 45,
        Name = "IV Fluid Bag",
        Image = "https://m.media-amazon.com/images/I/514nbXKja2L.__AC_SX300_SY300_QL70_FMwebp_.jpg",
        CategoryId = 2, // IV Therapy
        Description = "IV fluid bag for hydration and medication administration.",
        Price = 8.50M
    },
       new Product
    {
        Id = 46,
        Name = "Blood Glucose Test Strips",
        Image = "https://m.media-amazon.com/images/I/41Ld08nwkdL._SY445_SX342_QL70_FMwebp_.jpg",
        CategoryId = 4, // Diabetes Care
        Description = "Disposable blood glucose test strips for accurate glucose monitoring.",
        Price = 29.99M
    },
    new Product
    {
        Id = 47,
        Name = "Lancets",
        Image = "https://m.media-amazon.com/images/I/71P6oFj3KRL.__AC_SX300_SY300_QL70_FMwebp_.jpg",
        CategoryId = 4, // Diabetes Care
        Description = "Sterile lancets for blood glucose testing.",
        Price = 9.99M
    },
    new Product
    {
        Id = 48,
        Name = "Portable Shower Chair",
        Image = "https://m.media-amazon.com/images/I/61pz5TDKuwL.__AC_SX300_SY300_QL70_FMwebp_.jpg",
        CategoryId = 5, // Home Health Care
        Description = "Sturdy and adjustable portable shower chair.",
        Price = 45.00M
    },
    new Product
    {
        Id = 49,
        Name = "Infant Nasal Aspirator",
        Image = "https://m.media-amazon.com/images/I/61IR8qxaxBL.__AC_SX300_SY300_QL70_FMwebp_.jpg",
        CategoryId = 6, // Mom & Baby Care
        Description = "Gentle and effective nasal aspirator for infants.",
        Price = 12.00M
    },
       new Product
    {
        Id = 50,
        Name = "Breast Pump",
        Image = "https://m.media-amazon.com/images/I/415zba+iXwL._SY300_SX300_.jpg",
        CategoryId = 6, // Mom & Baby Care
        Description = "Electric breast pump for efficient and comfortable milk expression.",
        Price = 120.00M
    },
       new Product
    {
        Id = 51,
        Name = "Orthopedic Pillow",
        Image = "https://m.media-amazon.com/images/I/61mdPtC0iIL.__AC_SX300_SY300_QL70_FMwebp_.jpg",
        CategoryId = 9, // Physical Therapy
        Description = "Ergonomically designed orthopedic pillow for neck and spine support.",
        Price = 29.99M
    },
    new Product
    {
        Id = 52,
        Name = "Compression Bandage",
        Image = "https://m.media-amazon.com/images/I/614k6uqVbFL.__AC_SX300_SY300_QL70_FMwebp_.jpg",
        CategoryId = 1, // Wound Care
        Description = "Stretchable compression bandage for wound management and support.",
        Price = 8.50M
    },
    new Product
    {
        Id = 53,
        Name = "Home IV Infusion Kit",
        Image = "https://m.media-amazon.com/images/I/810m93ND8gL._AC_SX679_.jpg",
        CategoryId = 2, // IV Therapy
        Description = "Complete IV infusion kit for home-based intravenous therapy.",
        Price = 34.99M
    },
    new Product
    {
        Id = 54,
        Name = "Emergency First Aid Manual",
        Image = "https://m.media-amazon.com/images/I/81G4bfha0lL.__AC_SX300_SY300_QL70_FMwebp_.jpg",
        CategoryId = 3, // First Aid
        Description = "Comprehensive first aid manual for emergency preparedness.",
        Price = 12.99M
    },
    new Product
    {
        Id = 55,
        Name = "Insulin Cooling Case",
        Image = "https://m.media-amazon.com/images/I/71+qmT0A1JL._AC_SX569_.jpg",
        CategoryId = 4, // Diabetes Care
        Description = "Portable cooling case for insulin storage during travel.",
        Price = 19.99M
    },
    new Product
    {
        Id = 56,
        Name = "Portable Nebulizer",
        Image = "https://m.media-amazon.com/images/I/61tSm0np5HL.__AC_SX300_SY300_QL70_FMwebp_.jpg",
        CategoryId = 12, // Respiratory
        Description = "Compact and portable nebulizer for on-the-go respiratory therapy.",
        Price = 69.99M
    },
    new Product
    {
        Id = 57,
        Name = "Urological Drain Bag",
        Image = "https://m.media-amazon.com/images/I/61wDA7XwFnS._AC_SY741_.jpg",
        CategoryId = 13, // Ostomy & Urology Supplies
        Description = "Sterile urological drain bag for urinary collection and management.",
        Price = 15.99M
    },
    new Product
    {
        Id = 58,
        Name = "Adjustable Knee Support Brace",
        Image = "https://m.media-amazon.com/images/I/61A4vopvL7S.__AC_SX300_SY300_QL70_FMwebp_.jpg",
        CategoryId = 9, // Physical Therapy
        Description = "Versatile knee support brace with adjustable straps for customized fit.",
        Price = 24.99M
    },
    new Product
    {
        Id = 59,
        Name = "Reusable Scrub Caps",
        Image = "https://m.media-amazon.com/images/I/61-0J1+qCeL._AC_SX522_.jpg",
        CategoryId = 10, // Scrubs
        Description = "Comfortable and breathable scrub caps for healthcare professionals.",
        Price = 9.99M
    },
    new Product
    {
        Id = 60,
        Name = "Reusable Face Masks",
        Image = "https://m.media-amazon.com/images/I/41iS6HxvXnL._SY445_SX342_QL70_FMwebp_.jpg",
        CategoryId = 11, // Protection Wear
        Description = "Washable and reusable face masks for daily protection.",
        Price = 7.99M
    },
    new Product
    {
        Id = 61,
        Name = "CPAP Nasal Mask",
        Image = "https://m.media-amazon.com/images/I/4113F7qKOqL.__AC_SY300_SX300_QL70_FMwebp_.jpg",
        CategoryId = 12, // Respiratory
        Description = "Comfortable nasal mask for CPAP therapy in sleep apnea.",
        Price = 49.99M
    },
    new Product
    {
        Id = 62,
        Name = "Urinary Leg Bag Straps",
        Image = "https://m.media-amazon.com/images/I/61F46vG4ECL.__AC_SX300_SY300_QL70_FMwebp_.jpg",
        CategoryId = 13, // Ostomy & Urology Supplies
        Description = "Adjustable straps for securing urinary leg bags comfortably.",
        Price = 8.50M
    },
    new Product
    {
        Id = 63,
        Name = "Muscle Massage Gun",
        Image = "https://m.media-amazon.com/images/I/71ILpYb00uL.__AC_SX300_SY300_QL70_FMwebp_.jpg",
        CategoryId = 9, // Physical Therapy
        Description = "Powerful muscle massage gun for deep tissue relaxation and recovery.",
        Price = 79.99M
    },
    new Product
    {
        Id = 64,
        Name = "Antibacterial Hand Sanitizer",
        Image = "https://m.media-amazon.com/images/I/51COxBcVmML._SY445_SX342_QL70_FMwebp_.jpg",
        CategoryId = 11, // Protection Wear
        Description = "Effective antibacterial hand sanitizer gel for hand hygiene.",
        Price = 6.99M
    },
    new Product
    {
        Id = 65,
        Name = "Compact Blood Pressure Monitor",
        Image = "https://m.media-amazon.com/images/I/714ytBdz2nL.__AC_SX300_SY300_QL70_FMwebp_.jpg",
        CategoryId = 5, // Home Health Care
        Description = "Portable and easy-to-use blood pressure monitor for home monitoring.",
        Price = 39.99M
    },
    new Product
    {
        Id = 66,
        Name = "Postpartum Belly Wrap",
        Image = "https://m.media-amazon.com/images/I/41eYV5uftZL._SY445_SX342_QL70_FMwebp_.jpg",
        CategoryId = 6, // Mom & Baby Care
        Description = "Comfortable postpartum belly wrap for abdominal support and recovery.",
        Price = 19.99M
    },
    new Product
    {
        Id = 67,
        Name = "Digital Thermometer",
        Image = "https://m.media-amazon.com/images/I/51sO-p5+cQL._AC_SY300_SX300_.jpg",
        CategoryId = 3, // First Aid
        Description = "Fast and accurate digital thermometer for temperature measurement.",
        Price = 9.99M
    },
    new Product
    {
        Id = 68,
        Name = "Eye Patch",
        Image = "https://m.media-amazon.com/images/I/61VVJsEKlsL.__AC_SY300_SX300_QL70_FMwebp_.jpg",
        CategoryId = 1, // Wound Care
        Description = "Soft and breathable eye patch for post-operative or injury use.",
        Price = 3.49M
    },
    new Product
    {
        Id = 69,
        Name = "IV Catheter Securement Device",
        Image = "https://m.media-amazon.com/images/I/61f37xDXKrL.__AC_SX300_SY300_QL70_FMwebp_.jpg",
        CategoryId = 2, // IV Therapy
        Description = "Securement device for IV catheters to prevent dislodgement and movement.",
        Price = 5.99M
    },
    new Product
    {
        Id = 70,
        Name = "Disposable Shoe Covers",
        Image = "https://m.media-amazon.com/images/I/71vWf-Mw5lL.__AC_SX300_SY300_QL70_FMwebp_.jpg",
        CategoryId = 11, // Protection Wear
        Description = "Disposable shoe covers for cleanliness and contamination control.",
        Price = 12.99M
    }

        });

        modelBuilder.Entity<Order>().HasData(new Order[]
        {
            new Order
        {
            Id = 1,
            UserId = 1, 
            CloseDate = DateTime.Now.AddDays(-2),
            IsClosed = true,
            CreditCardNumber = 1234567890123456,
            ExpirationDate = "12/25",
            CVV = 123,
            Zip = 12345
        },
        new Order
        {
            Id = 2,
            UserId = 2, 
            CloseDate = DateTime.Now.AddDays(-1),
            IsClosed = true,
            CreditCardNumber = 9876543210987654,
            ExpirationDate = "03/27",
            CVV = 456,
            Zip = 54321
        }

        });

        modelBuilder.Entity<SimilarItem>().HasData(new SimilarItem[]
        {
            new SimilarItem { Id = 1, ProductId = 1, UserId = 1, SimilarProductId = 2 },
            new SimilarItem { Id = 2, ProductId = 2, UserId = 1, SimilarProductId = 3 }
        });

        modelBuilder.Entity<Review>().HasData(new Review[]
        {
            new Review { Id = 1, Rating = 4, ProductId = 1, UserId = 1, DateCreated = DateTime.UtcNow, CommentReview = "Great product!" },
            new Review { Id = 2, Rating = 5, ProductId = 2, UserId = 2, DateCreated = DateTime.UtcNow, CommentReview = "Excellent quality!" }
        });

    } 



    public MedEquipCentralDbContext(DbContextOptions<MedEquipCentralDbContext> context) : base(context)
    { 
    } 
        };
