USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[ChangeCustomerPassword]    Script Date: 03-05-2021 09:34:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER   PROCEDURE [dbo].[ChangeCustomerPassword]
	-- Add the parameters for the stored procedure here
	@Email nvarchar(50),
	@NewPassword nvarchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		DECLARE @result int = 0;

    -- Insert statements for procedure here
	if((select count(Email) from Customer where Email = @Email) = 1)
		begin;
		update Customer set Password = @NewPassword where Email = @Email;
		set @result = 1;
		end
return @result;

END
