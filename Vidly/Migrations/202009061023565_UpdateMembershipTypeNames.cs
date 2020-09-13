namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMembershipTypeNames : DbMigration
    {
        public override void Up()
        {
            Sql("Update MembershipTypes SET Name = 'Pay as You Go' WHERE DurationInMonth = 0");
            Sql("Update MembershipTypes SET Name = 'Monthly' WHERE DurationInMonth = 1");
            Sql("Update MembershipTypes SET Name = 'Season' WHERE DurationInMonth = 3");
            Sql("Update MembershipTypes SET Name = 'Yearly' WHERE DurationInMonth = 12");
        }
        
        public override void Down()
        {
        }
    }
}
