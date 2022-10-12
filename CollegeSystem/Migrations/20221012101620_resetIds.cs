using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollegeSystem.Migrations
{
    public partial class resetIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DBCC CHECKIDENT('Specializations',RESEED,0)");
            migrationBuilder.Sql(@"DBCC CHECKIDENT('Students',RESEED,0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
