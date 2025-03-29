IF EXISTS (SELECT TOP 1 1 FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[DBO].[BP0002]') AND  OBJECTPROPERTY(ID, N'IsProcedure') = 1)			
DROP PROCEDURE [DBO].BP0002
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- <Summary>
---- Filter Booking
---- 
-- <History>
---- CREATE BY: HOAI THANH, DATE: 29/03/2025
-- <Example>
-- EXEC BP0002 @UserId='1F269289-BDFE-4B9E-BCCA-320816307215'
-- </Summary>

CREATE PROCEDURE BP0002
(
	@UserId VARCHAR(50),
	@PageIndex INT = 1,
    @PageSize INT = 10
)  
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @Offset INT = (@PageIndex - 1) * @PageSize

	DECLARE @sSQL NVARCHAR(MAX) = '
    SELECT 
        B.Id,
		B.RoomId,
        B.CheckInDate,
        B.CheckOutDate,
        B.TotalPrice,
        B.CreatedAt,
        U.FullName AS CreatedUserName,
        B.UserId AS CreatedUserId,
        S.Name AS StatusName,
        B.StatusId,
		R.Name AS RoomName
    FROM Bookings B
    LEFT JOIN Users U ON B.UserId = U.Id
    LEFT JOIN Rooms R ON B.RoomId = R.Id
    LEFT JOIN Status S ON B.StatusId = S.Id
    WHERE 1=1'

	IF ISNULL(LTRIM(RTRIM(@UserId)), '') != ''
        SET @sSQL += ' AND B.UserId = @UserId'

	SET @sSQL += '
    ORDER BY B.CheckInDate
    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY'

	EXEC sp_executesql @sSQL,
        N'@UserId NVARCHAR(50), @Offset INT, @PageSize INT',
        @UserId = @UserId,
        @Offset = @Offset,
        @PageSize = @PageSize
END
GO

