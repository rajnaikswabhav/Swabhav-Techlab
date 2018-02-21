namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v31 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.PAYMENT", "InvoiceNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("gsmktg.PAYMENT", "InvoiceNo");
        }
    }
}
