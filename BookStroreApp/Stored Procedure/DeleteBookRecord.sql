USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[DeleteBookRecord]    Script Date: 03-05-2021 09:38:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER    PROCEDURE [dbo].[DeleteBookRecord] 
	-- Add the parameters for the stored procedure here
	@BookID varchar(100)
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET XACT_ABORT on;
SET NOCOUNT ON;
BEGIN
BEGIN TRY
BEGIN TRANSACTION;

	DECLARE @result int = 0;

	if((select count(*) from Book where BookID = @BookID) = 0)
	begin;
		set @result = 2;
		throw 5000,'Book dont exist',-1;
	end

	begin
	update Book set  BookQuantity = 0 where BookID = @BookID;
	delete  from Book where BookID= @BookID;
	set @result = 1;
	end
	
	

COMMIT TRANSACTION;	
print @result;
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
