IF EXISTS (SELECT TOP 1 1 FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[DBO].[PROP0001]') AND  OBJECTPROPERTY(ID, N'IsProcedure') = 1)			
DROP PROCEDURE [DBO].PROP0001
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- <Summary>
---- Get Promotions By hotelId
---- 
-- <History>
---- CREATE BY: HOAI THANH, DATE: 01/04/2025
-- <Example>
-- EXEC PROP0001 @HotelId='7467BB90-C7C0-4DFC-B4A8-E515AFCBA35F'
-- </Summary>

CREATE PROCEDURE PROP0001
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
        P.Id,
		P.HotelId,
		P.DiscountPercentage,
		P.StartDate,
		P.EndDate,
		P.CreatedAt
    FROM Promotions P
    WHERE 1=1'

	IF ISNULL(LTRIM(RTRIM(@HotelId)), '') != ''
        SET @sSQL += ' AND P.HotelId = @HotelId'

	SET @sSQL += '
    ORDER BY P.StartDate
    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY'

	EXEC sp_executesql @sSQL,
        N'@HotelId NVARCHAR(50), @Offset INT, @PageSize INT',
        @HotelId = @HotelId,
        @Offset = @Offset,
        @PageSize = @PageSize
END
GO

