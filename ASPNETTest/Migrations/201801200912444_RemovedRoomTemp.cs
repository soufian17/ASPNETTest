namespace ASPNETTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRoomTemp : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "Room_d", "dbo.Rooms");
            DropIndex("dbo.Reservations", new[] { "Room_d" });
            AddColumn("dbo.Reservations", "Start", c => c.DateTime(nullable: false));
            AddColumn("dbo.Reservations", "End", c => c.DateTime(nullable: false));
            DropColumn("dbo.Reservations", "Room_d");
            DropTable("dbo.Rooms");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        d = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RoomNumber = c.String(),
                    })
                .PrimaryKey(t => t.d);
            
            AddColumn("dbo.Reservations", "Room_d", c => c.Int());
            DropColumn("dbo.Reservations", "End");
            DropColumn("dbo.Reservations", "Start");
            CreateIndex("dbo.Reservations", "Room_d");
            AddForeignKey("dbo.Reservations", "Room_d", "dbo.Rooms", "d");
        }
    }
}
