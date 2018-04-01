namespace ASPNETTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedData : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO ROOMS (Name,RoomNumber) VALUES ('First Room','1A')");
            Sql("INSERT INTO ROOMS (Name,RoomNumber) VALUES ('Second Room','1B')");
            Sql("INSERT INTO ROOMS (Name,RoomNumber) VALUES ('Third Room','2A')");
            Sql("INSERT INTO ROOMS (Name,RoomNumber) VALUES ('Fourth Room','2B')");
            Sql("INSERT INTO ROOMS (Name,RoomNumber) VALUES ('Fifth Room','5A')");
        }
        
        public override void Down()
        {
        }
    }
}
