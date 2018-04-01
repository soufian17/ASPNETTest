namespace ASPNETTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedNamingIssue : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RoomNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Reservations", "Room_Id", c => c.Int());
            CreateIndex("dbo.Reservations", "Room_Id");
            AddForeignKey("dbo.Reservations", "Room_Id", "dbo.Rooms", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "Room_Id", "dbo.Rooms");
            DropIndex("dbo.Reservations", new[] { "Room_Id" });
            DropColumn("dbo.Reservations", "Room_Id");
            DropTable("dbo.Rooms");
        }
    }
}
