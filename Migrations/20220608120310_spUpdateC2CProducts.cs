using Microsoft.EntityFrameworkCore.Migrations;

namespace E_commerce.Migrations
{
    public partial class spUpdateC2CProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"CREATE PROCEDURE spUpdateC2CProducts
                                        @ID			int,
                                        @NameAR		nvarchar(MAX),
                                        @NameEN		nvarchar(MAX),
                                        @Report		nvarchar(MAX),
                                        @Price		decimal(8,2),
                                        @DetailsAR	nvarchar(MAX),
                                        @DetailsEN	nvarchar(MAX),
                                        @Duration	int,
                                        @Image		nvarchar(MAX),
                                        @status		bit,
                                        @Quantity	int,
                                        @Unit		nvarchar(MAX),
                                        @Views		int,
                                        @Discount	int,
                                        @Evaluation	int,
                                        @CategoryID int
                                AS
                                BEGIN
                                        
                                    UPDATE e_commerce.dbo.Products
                                    SET 
                                        NameAR =	@NameAR,
                                        NameEN =	@NameEN,
                                        Report =	@Report,
                                        Price =		@Price,
                                        DetailsAR = @DetailsAR,
                                        DetailsEN = @DetailsEN,
                                        Duration =	@Duration,
                                        Image =		@Image,
                                        Status =	@status,
                                        Quantity =	@Quantity,
                                        Unit =		@Unit,
                                        Views =		@Views,
                                        Discount =	@Discount,
                                        Evaluation =@Evaluation,
                                        CategoryID =@CategoryID
                                    WHERE ID = @ID

                                END";
            migrationBuilder.Sql(procedure);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"DROP PROCEDURE spUpdateC2CProducts";
            migrationBuilder.Sql(procedure);

        }
    }
}
