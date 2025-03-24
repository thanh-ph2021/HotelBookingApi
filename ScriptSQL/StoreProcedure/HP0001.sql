IF EXISTS (SELECT TOP 1 1 FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[DBO].[HP0001]') AND  OBJECTPROPERTY(ID, N'IsProcedure') = 1)			
DROP PROCEDURE [DBO].HP0001
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- <Summary>
---- Filter Hotel
---- 
-- <History>
---- CREATE BY: HOAI THANH, DATE: 18/03/2025
-- <Example>
-- EXEC HP0001 @City='', @Country=N'Việt Nam', @Rating='', @PageIndex = 1, @PageSize = 10
-- </Summary>

CREATE PROCEDURE HP0001
(
	@City NVARCHAR(50),
    @Country NVARCHAR(50),
	@Rating VARCHAR(20),
	@PageIndex INT = 1,
    @PageSize INT = 10
)  
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @Offset INT = (@PageIndex - 1) * @PageSize

	DECLARE @sSQL NVARCHAR(MAX) = '
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
			SELECT STRING_AGG(ImageUrl, '','') 
			FROM HotelImages 
			WHERE HotelId = H.Id AND IsBanner = 0
		) AS ImageUrls,
        COUNT(*) OVER() AS TotalCount
    FROM Hotels H
    LEFT JOIN Users U ON H.OwnerId = U.Id
    WHERE 1=1 AND H.IsDeleted = 0'

    IF ISNULL(LTRIM(RTRIM(@City)), '') != ''
        SET @sSQL += ' AND H.City LIKE @City'
    IF ISNULL(LTRIM(RTRIM(@Country)), '') != ''
        SET @sSQL += ' AND H.Country LIKE @Country'
    IF ISNULL(LTRIM(RTRIM(@Rating)), '') != ''
        SET @sSQL += ' AND H.Rating LIKE @Rating'

    SET @sSQL += '
    ORDER BY H.Name
    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY'

    DECLARE 
        @CityLike NVARCHAR(50) = '%' + ISNULL(@City, '') + '%',
        @CountryLike NVARCHAR(50) = '%' + ISNULL(@Country, '') + '%',
        @RatingLike NVARCHAR(20) = '%' + ISNULL(@Rating, '') + '%'

    EXEC sp_executesql @sSQL,
        N'@City NVARCHAR(50), @Country NVARCHAR(50), @Rating NVARCHAR(20), @Offset INT, @PageSize INT',
        @City = @CityLike,
        @Country = @CountryLike,
        @Rating = @RatingLike,
        @Offset = @Offset,
        @PageSize = @PageSize
END
GO

