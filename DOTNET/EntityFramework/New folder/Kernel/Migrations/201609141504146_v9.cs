namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.TRANSACTION", "PaymentMode", c => c.String());
            AddColumn("gsmktg.TRANSACTION", "TransactionDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("gsmktg.TRANSACTION", "TransactionDate");
            DropColumn("gsmktg.TRANSACTION", "PaymentMode");
        }
    }
}
