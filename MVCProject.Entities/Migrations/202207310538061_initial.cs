namespace MVCProject.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountingProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountingID = c.Int(),
                        ProductName = c.String(),
                        Quantity = c.Int(),
                        PriceDollar = c.Double(),
                        PriceLira = c.Double(),
                        Tax = c.Double(),
                        Date = c.DateTime(),
                        Cargo = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Accounting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        Telephone = c.String(),
                        City = c.String(),
                        Balance = c.Double(),
                        TotalDollar = c.Double(),
                        TotalLira = c.Double(),
                        Tax = c.Double(),
                        ProductName = c.String(),
                        Quantity = c.Int(),
                        CargoPrice = c.Double(),
                        Exchange = c.Double(),
                        Expensive = c.Double(),
                        Date = c.DateTime(),
                        InvoiceNo = c.Int(),
                        IsCargo = c.Boolean(),
                        IsPaid = c.Boolean(),
                        IsDelivered = c.Boolean(),
                        Notes = c.String(),
                        InvoiceLink1 = c.String(),
                        InvoiceLink2 = c.String(),
                        InvoiceLink3 = c.String(),
                        InvoiceLink4 = c.String(),
                        InvoiceLink5 = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArrivedExchange",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Double(),
                        Total = c.Double(),
                        Name = c.String(),
                        Phone = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ArrivedMoney",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BoxsVar",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Category = c.String(),
                        Expensive = c.Double(),
                        OrderTotal = c.Double(),
                        Telephone = c.String(),
                        Date = c.DateTime(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cargos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        Telephone = c.String(),
                        Quantity = c.Int(nullable: false),
                        Expensive = c.Double(),
                        TotalDollar = c.Double(),
                        TotalCargo = c.Double(),
                        Tax = c.Double(),
                        Notes = c.String(),
                        Date = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProviderId = c.Int(nullable: false),
                        Comment = c.String(nullable: false, maxLength: 350),
                        Rate1 = c.Short(nullable: false),
                        Rate2 = c.Short(nullable: false),
                        Rate3 = c.Short(nullable: false),
                        Rate4 = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerLocation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        Latitude = c.String(),
                        Longitude = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Deposit",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.String(),
                        DepositTotal = c.Double(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Expensive",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Double(),
                        Total = c.Double(),
                        Name = c.String(),
                        Phone = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GpsData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeviceId = c.String(),
                        DeviceIMEI = c.String(),
                        Date = c.DateTime(nullable: false),
                        LocationMark = c.String(),
                        Latitude = c.String(),
                        DirectionLat = c.String(),
                        Longitude = c.String(),
                        DirectionLong = c.String(),
                        Speed = c.String(),
                        UTCDate = c.String(),
                        Direction = c.String(),
                        State = c.String(),
                        Status = c.String(),
                        Mileage = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ilceler",
                c => new
                    {
                        IlceId = c.Short(nullable: false, identity: true),
                        SehirId = c.Short(nullable: false),
                        IlceAdi = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.IlceId)
                .ForeignKey("dbo.Sehirler", t => t.SehirId, cascadeDelete: true)
                .Index(t => t.SehirId);
            
            CreateTable(
                "dbo.Sehirler",
                c => new
                    {
                        SehirId = c.Short(nullable: false, identity: true),
                        SehirAdi = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.SehirId);
            
            CreateTable(
                "dbo.Semt",
                c => new
                    {
                        SemtId = c.Int(nullable: false, identity: true),
                        IlceId = c.Short(nullable: false),
                        SemtAdi = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.SemtId)
                .ForeignKey("dbo.Ilceler", t => t.IlceId, cascadeDelete: true)
                .Index(t => t.IlceId);
            
            CreateTable(
                "dbo.KamuBorc",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceNumber = c.Int(nullable: false),
                        Name = c.String(),
                        Telephone = c.String(),
                        TotalDollar = c.Double(),
                        Notes = c.String(),
                        Date = c.DateTime(),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Komerk",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Double(),
                        TotalWeight = c.Double(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LostOrder",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Marak = c.String(),
                        Adet = c.Int(nullable: false),
                        TotalLira = c.Double(nullable: false),
                        TotalDollar = c.Double(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuAdi = c.String(),
                        Icon = c.String(),
                        Url = c.String(),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SubMenus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenusId = c.Int(nullable: false),
                        SubMenuAdi = c.String(),
                        Url = c.String(),
                        Icon = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menus", t => t.MenusId, cascadeDelete: true)
                .Index(t => t.MenusId);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false),
                        MessageToPost = c.String(nullable: false),
                        Sender = c.String(),
                        DatePosted = c.DateTime(nullable: false),
                        OwnerId = c.String(),
                        ToId = c.String(),
                        OrderId = c.Int(),
                        IsReaded = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MyBalance",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Answer = c.String(nullable: false, maxLength: 4000),
                        UsersId = c.String(maxLength: 128),
                        OrderTime = c.DateTime(),
                        CityId = c.Short(nullable: false),
                        IlceId = c.Short(nullable: false),
                        SemtId = c.Int(nullable: false),
                        MahalleId = c.Int(nullable: false),
                        Status = c.String(),
                        BitisTarihi = c.DateTime(),
                        WinnerBid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Price",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        FromDesi = c.Double(),
                        ToDesi = c.Double(),
                        Zone = c.Short(),
                        ServicePrice = c.Int(),
                        IsExpress = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Refunds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Marak = c.String(),
                        Adet = c.Int(nullable: false),
                        TotalLira = c.Double(nullable: false),
                        TotalDollar = c.Double(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reklam",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Text = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RemainingCost",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TotalAccounting = c.Double(),
                        TurkishCargo = c.Double(),
                        Komerk = c.Double(),
                        Taxi = c.Double(),
                        PTTCosts = c.Double(nullable: false),
                        RemainingCosts = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reply",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MessageId = c.Int(nullable: false),
                        ReplyFrom = c.String(),
                        ReplyMessage = c.String(nullable: false),
                        ReplyDateTime = c.DateTime(nullable: false),
                        IsReaded = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ShipmentBarcodes",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        ShipmentId = c.Int(nullable: false),
                        Barcode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shipment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TrackingNo = c.String(),
                        SenderName = c.String(),
                        SenderIdVATNumber = c.Int(),
                        SenderTelephone = c.String(),
                        SenderEmail = c.String(),
                        SenderAddress = c.String(),
                        SenderPostalCode = c.Int(),
                        SenderCountry = c.String(),
                        ReceiversAccountNo = c.Short(),
                        ReceiversVatNo = c.Short(),
                        ReceiverName = c.String(),
                        ReceiverAddress = c.String(),
                        ReceiverAddress2 = c.String(),
                        ReceiverCity = c.String(),
                        ReceiverTelephoneNo = c.String(),
                        ReceiverEmail = c.String(),
                        ReceiverPostalCode = c.String(),
                        IsResidental = c.Boolean(),
                        ReceiverCountry = c.String(),
                        PackageCount = c.Short(),
                        TotalWeight = c.Short(),
                        DimensionalWeight = c.Short(),
                        IsSameSize = c.Boolean(),
                        SpecialInstructions = c.String(),
                        ShipmentDate = c.DateTime(),
                        Status = c.String(),
                        StatusId = c.Int(),
                        ExternalTrackingCode = c.String(),
                        IsPtt = c.Boolean(nullable: false),
                        IsDhl = c.Boolean(nullable: false),
                        TotalDolar = c.Double(nullable: false),
                        Notes = c.String(),
                        Desi = c.Double(nullable: false),
                        Height = c.Int(nullable: false),
                        Lenght = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        Width = c.Int(nullable: false),
                        PaymentMethod = c.String(),
                        ReceiverCountryId = c.String(),
                        ReceiverMail = c.String(),
                        PostType = c.String(),
                        SenderTCNo = c.String(),
                        Konsimento = c.String(),
                        Acente = c.String(),
                        Content = c.String(),
                        ValueOfPackage = c.Double(nullable: false),
                        ReceiverCityId = c.String(),
                        HarmonyCode = c.Int(nullable: false),
                        MoneyForBuy = c.Double(nullable: false),
                        IsDelivered = c.Boolean(nullable: false),
                        StatusCounter = c.Int(nullable: false),
                        IsApiSuccess = c.Int(nullable: false),
                        OtherLink = c.String(),
                        UserId = c.String(),
                        IsConfirmed = c.Boolean(nullable: false),
                        AgentSenderName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShipmentTurpexBarcodes",
                c => new
                    {
                        Id = c.Short(nullable: false, identity: true),
                        ShipmentId = c.Int(nullable: false),
                        Barcode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StatusUpdates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UpdatedDateTime = c.DateTime(),
                        Status = c.String(),
                        ShipmentId = c.Int(),
                        StatusId = c.Int(),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TaxiCosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Name = c.String(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TotalCargo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Category = c.String(),
                        Expensive = c.Double(),
                        OrderTotal = c.Double(),
                        Telephone = c.String(),
                        Date = c.DateTime(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TurkishCargo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AirBill = c.String(),
                        Price = c.Double(nullable: false),
                        TotalWeight = c.Double(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(nullable: false, maxLength: 256),
                        Address = c.String(),
                        City = c.Short(nullable: false),
                        Ilce = c.Int(nullable: false),
                        Semt = c.Int(nullable: false),
                        Token = c.String(),
                        BirthDate = c.DateTime(),
                        State = c.String(),
                        TCNo = c.String(),
                        Phone = c.String(),
                        ProfilePicture = c.Binary(),
                        Balance = c.Double(),
                        Debt = c.Double(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Zone",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Country = c.String(),
                        ZoneNo = c.Short(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ZuuCargo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerAccountId = c.String(),
                        StockName = c.String(),
                        CustomersName = c.String(),
                        NumberOfContainer = c.Int(),
                        Kg = c.Int(),
                        Pcs = c.Int(),
                        Price = c.Double(),
                        Date = c.DateTime(),
                        Debt = c.Double(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.SubMenus", "MenusId", "dbo.Menus");
            DropForeignKey("dbo.Semt", "IlceId", "dbo.Ilceler");
            DropForeignKey("dbo.Ilceler", "SehirId", "dbo.Sehirler");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.SubMenus", new[] { "MenusId" });
            DropIndex("dbo.Semt", new[] { "IlceId" });
            DropIndex("dbo.Ilceler", new[] { "SehirId" });
            DropTable("dbo.ZuuCargo");
            DropTable("dbo.Zone");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TurkishCargo");
            DropTable("dbo.TotalCargo");
            DropTable("dbo.TaxiCosts");
            DropTable("dbo.StatusUpdates");
            DropTable("dbo.ShipmentTurpexBarcodes");
            DropTable("dbo.Shipment");
            DropTable("dbo.ShipmentBarcodes");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Reply");
            DropTable("dbo.RemainingCost");
            DropTable("dbo.Reklam");
            DropTable("dbo.Refunds");
            DropTable("dbo.Price");
            DropTable("dbo.Order");
            DropTable("dbo.MyBalance");
            DropTable("dbo.Message");
            DropTable("dbo.SubMenus");
            DropTable("dbo.Menus");
            DropTable("dbo.LostOrder");
            DropTable("dbo.Komerk");
            DropTable("dbo.KamuBorc");
            DropTable("dbo.Semt");
            DropTable("dbo.Sehirler");
            DropTable("dbo.Ilceler");
            DropTable("dbo.GpsData");
            DropTable("dbo.Expensive");
            DropTable("dbo.Deposit");
            DropTable("dbo.CustomerLocation");
            DropTable("dbo.Comment");
            DropTable("dbo.Category");
            DropTable("dbo.Cargos");
            DropTable("dbo.BoxsVar");
            DropTable("dbo.ArrivedMoney");
            DropTable("dbo.ArrivedExchange");
            DropTable("dbo.Accounting");
            DropTable("dbo.AccountingProducts");
        }
    }
}
