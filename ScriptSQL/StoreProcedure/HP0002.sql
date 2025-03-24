IF EXISTS (SELECT TOP 1 1 FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[DBO].[HP0002]') AND  OBJECTPROPERTY(ID, N'IsProcedure') = 1)			
DROP PROCEDURE [DBO].HP0002
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- <Summary>
---- Get Hotel By Id
---- 
-- <History>
---- CREATE BY: HOAI THANH, DATE: 24/03/2025
-- <Example>
-- EXEC HP0002 @HotelId='F791DE32-F4E0-4EC2-8F96-7DC36A67DE4D'
-- </Summary>

CREATE PROCEDURE HP0002
(
	@HotelId VARCHAR(50)
)  
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
        H.Id,
        H.Name,
        H.Description,
        H.Address,
        H.City,
        H.Country,
        H.Rating,
        U.FullName AS Owner,
		(
			SELECT TOP 1 ImageUrl 
			FROM HotelImages 
			WHERE HotelId = H.Id AND IsBanner = 1
		) AS BannerImage,
		(
			SELECT STRING_AGG(ImageUrl, ',') 
			FROM HotelImages 
			WHERE HotelId = H.Id AND IsBanner = 0
		) AS ImageUrls
    FROM Hotels H
    LEFT JOIN Users U ON H.OwnerId = U.Id
    WHERE H.Id = @HotelId AND H.IsDeleted = 0
END
GO

