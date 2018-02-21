namespace Techlabs.Euphoria.Kernel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("gsmktg.VISITOR", "Education", c => c.String());
            DropColumn("gsmktg.VISITOR", "Educatoin");
        }
        
        public override void Down()
        {
            AddColumn("gsmktg.VISITOR", "Educatoin", c => c.String());
            DropColumn("gsmktg.VISITOR", "Education");
        }
    }
}
