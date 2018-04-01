namespace ASPNETTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SmallChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rooms", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Rooms", "RoomNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rooms", "RoomNumber", c => c.String());
            AlterColumn("dbo.Rooms", "Name", c => c.String());
        }
    }
}
