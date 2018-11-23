namespace Yatzy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HighScores",
                c => new
                    {
                        HighScoreId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Score = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HighScoreId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HighScores");
        }
    }
}
