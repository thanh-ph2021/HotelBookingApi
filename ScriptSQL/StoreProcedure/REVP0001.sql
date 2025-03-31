IF EXISTS (SELECT TOP 1 1 FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[DBO].[REVP0001]') AND  OBJECTPROPERTY(ID, N'IsProcedure') = 1)			
DROP PROCEDURE [DBO].REVP0001
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- <Summary>
---- Get Reviews By hotelId
---- 
-- <History>
---- CREATE BY: HOAI THANH, DATE: 31/03/2025
-- <Example>
-- EXEC REVP0001 @HotelId='7467BB90-C7C0-4DFC-B4A8-E515AFCBA35F'
-- </Summary>

CREATE PROCEDURE REVP0001
(
	@HotelId VARCHAR(50),
	@PageIndex INT = 1,
    @PageSize INT = 10
)  
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @Offset INT = (@PageIndex - 1) * @PageSize

	DECLARE @sSQL NVARCHAR(MAX) = '
    SELECT 
        R.Id,
		R.UserId,
		U.FullName AS UserName,
		U.Country AS UserCountry,
		R.HotelId,
		R.RoomType,
		R.StayDuration,
		R.StayMonth,
		R.Rating,
		R.Title,
		R.Description,
		R.CreatedAt,
		R.UsefulCount,
		R.NotUsefulCount
    FROM Reviews R
    LEFT JOIN Users U ON R.UserId = U.Id
    WHERE 1=1'

	IF ISNULL(LTRIM(RTRIM(@HotelId)), '') != ''
        SET @sSQL += ' AND R.HotelId = @HotelId'

	SET @sSQL += '
    ORDER BY R.CreatedAt
    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY'

	EXEC sp_executesql @sSQL,
        N'@HotelId NVARCHAR(50), @Offset INT, @PageSize INT',
        @HotelId = @HotelId,
        @Offset = @Offset,
        @PageSize = @PageSize
END
GO

