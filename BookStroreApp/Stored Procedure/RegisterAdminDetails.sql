USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[RegisterAdminDetails]    Script Date: 03-05-2021 09:44:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER   PROCEDURE [dbo].[RegisterAdminDetails]
	@AdminName		varchar(255),
	@PhoneNumber  bigint,
	@Email		varchar(100),
	@Password		varchar(50)
AS
SET XACT_ABORT on;
SET NOCOUNT ON;
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @new_identity INTEGER = 0;
	declare @Identity table (ID int)
	DECLARE @result bit = 0;
    -- Insert statements for procedure here
	
	Insert into Admin(AdminName,PhoneNumber,Email,Password) output Inserted.AdminID into @Identity
	VALUES(@AdminName, @PhoneNumber, @Email, @Password);
	SELECT @new_identity = (select ID from @Identity);

	
	end

	

