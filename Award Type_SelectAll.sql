﻿USE [Dev]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==================================================
-- Author:		Robert Franklin
-- Create date: 	08/02/18
-- Description:		Select All for AwardType
-- ==================================================
ALTER PROCEDURE [dbo].[AwardType_SelectAll]

AS
BEGIN
	/* TEST SCRIPT
		DECLARE	@return_value int

		EXEC	@return_value = [dbo].[AwardType_SelectAll]

		SELECT	'Return Value' = @return_value
	*/

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

 --   -- Insert statements for procedure here
	--SELECT * FROM AwardType


	    -- Insert statements for procedure here
	--select [Id], [TypeName]
	--	from dbo.AwardType

	-- New Implementation
	SELECT DISTINCT
		at.Id, 
		at.TypeName, 
		CAST (CASE WHEN ufa.Id is null THEN 1
				ELSE 0
				END AS BIT) AS canDelete
		FROM AwardType AS at
		left join UserFinancialAid ufa ON ufa.AwardTypeId=at.Id
END
