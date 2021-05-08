USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[spInsertAdminLoginDetail]    Script Date: 03-05-2021 09:47:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER  PROCEDURE [dbo].[spInsertAdminLoginDetail]
	-- Add the parameters for the stored procedure here
	@Email nvarchar(50),
	@Password nvarchar(MAX)
AS
SET XACT_ABORT on;
SET NOCOUNT ON;
BEGIN
BEGIN TRY
BEGIN TRANSACTION;
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @result int = 0;
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	if((select count(Email) from Admin where Email = @Email) = 0)
		begin;
		set @result = 2;
		THROW 52000, 'Email is invalid', -1;
		end
	if((select count(Email) from Admin where Email = @Email and Password = @Password) = 0)
	begin;
		set @result = 3;
		THROW 52000, 'wrong password', -1;
	end
	else
	begin
	select AdminID,
	AdminName,
	PhoneNumber,
	Email
	from Admin where Email = @Email;
		set @result = 1;
	end
	
COMMIT TRANSACTION
return @result;
END TRY
BEGIN CATCH
--SELECT ERROR_NUMBER() as ErrorNumber, ERROR_MESSAGE() as ErrorMessage;
IF(XACT_STATE()) = -1
	BEGIN
		PRINT
		'transaction is uncommitable' + ' rolling back transaction'
		ROLLBACK TRANSACTION;
		print @result;
		return @result;
	END;
ELSE IF(XACT_STATE()) = 1
	BEGIN
		PRINT
		'transaction is commitable' + ' commiting back transaction'
		COMMIT TRANSACTION;
		print @result;
		return @result;
	END;
END CATCH
	
END
