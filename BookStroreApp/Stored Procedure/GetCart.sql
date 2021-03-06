USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[GetCart]    Script Date: 03-05-2021 09:41:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER     PROCEDURE [dbo].[GetCart]
	-- Add the parameters for the stored procedure here
	@CustomerID bigint
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET XACT_ABORT on;
SET NOCOUNT ON;
BEGIN
BEGIN TRY
BEGIN TRANSACTION;

	DECLARE @result int = 0;

	select Cart.CustomerID, Cart.CartID, Cart.BookID, Book.BookPrice, Book.BookName, AuthorName, BookImage,
	BookCount, TotalCost from 
	Cart inner join Book on Cart.BookID = Book.BookID
	inner join Author on Book.AuthorID = Author.AuthorID
	where Cart.CustomerID = @CustomerID;

	set @result = 1;
COMMIT TRANSACTION;	
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
