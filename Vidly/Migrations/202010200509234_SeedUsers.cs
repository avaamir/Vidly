namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'4538fd6e-db0f-484e-a97d-26ab0df66488', N'guest@vidly.com', 0, N'AE5YHaAkN27czu+D2DGpPmTP0QXkW5joiqKKysUc0kId+KWKHQaOqpqx08HkY8ngyA==', N'5ee72061-1fc0-48d4-9dbc-d0d4b33774ee', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'a1e6fddb-2c45-482e-8858-4724a018b2c7', N'admin@vidly.com', 0, N'AMdiOnBRZTS/JuEBU7aIQrKVTDlBxjvVB6z45uyAXmBEEIGzkYWphoM2Da7r3GEeYw==', N'fe482114-7409-44ef-afde-d6212f782111', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3977c896-fed1-440f-8842-548cfb31270e', N'CanManageMovies')
                                
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a1e6fddb-2c45-482e-8858-4724a018b2c7', N'3977c896-fed1-440f-8842-548cfb31270e')

");


        }
        
        public override void Down()
        {
        }
    }
}
