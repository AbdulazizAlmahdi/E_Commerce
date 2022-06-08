using Microsoft.EntityFrameworkCore.Migrations;

namespace E_commerce.Migrations
{
    public partial class spUpadteUserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			string procedure = @"CREATE OR ALTER PROCEDURE spUpadteUserProfile
									@ID int,
									@Name nvarchar(MAX),
									@address nvarchar(MAX),
									@password nvarchar(MAX),
									@Phone nvarchar(12)

								AS 
								BEGIN

									SET NOCOUNT ON;
									BEGIN TRAN
										BEGIN TRY

											DECLARE @phoneId int

											Select @phoneId = PhoneID
											FROM e_commerce.dbo.Users 
											WHERE ID = @ID

											UPDATE e_commerce.dbo.Phones
												SET Number = @Phone
												WHERE ID = @phoneId

											Update e_commerce.dbo.Users 
												SET Name = @name,
													address =  @address,
													password = @password,
													PhoneID = @phoneId
												WHERE ID = @ID;

											COMMIT TRANSACTION
										END TRY
										BEGIN CATCH
											SELECT ERROR_MESSAGE() AS ErrorMessage;
											ROLLBACK TRANSACTION
										END CATCH	 
								END";
			migrationBuilder.Sql(procedure);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"DROP PROCEDURE spUpadteUserProfile";
			migrationBuilder.Sql(procedure);
		}
    }
}
