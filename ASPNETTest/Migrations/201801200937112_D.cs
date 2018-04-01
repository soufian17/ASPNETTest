namespace ASPNETTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class D : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "Room_Id", "dbo.Rooms");
            DropIndex("dbo.Reservations", new[] { "Room_Id" });
            RenameColumn(table: "dbo.Reservations", name: "Room_Id", newName: "RoomId");
            AlterColumn("dbo.Reservations", "RoomId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reservations", "RoomId");
            AddForeignKey("dbo.Reservations", "RoomId", "dbo.Rooms", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "RoomId", "dbo.Rooms");
            DropIndex("dbo.Reservations", new[] { "RoomId" });
            AlterColumn("dbo.Reservations", "RoomId", c => c.Int());
            RenameColumn(table: "dbo.Reservations", name: "RoomId", newName: "Room_Id");
            CreateIndex("dbo.Reservations", "Room_Id");
            AddForeignKey("dbo.Reservations", "Room_Id", "dbo.Rooms", "Id");
        }
    }
}
