IF EXISTS (SELECT TOP 1 1 FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[DBO].[RP0001]') AND  OBJECTPROPERTY(ID, N'IsProcedure') = 1)			
DROP PROCEDURE [DBO].RP0001
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- <Summary>
---- Get Room By Id
---- 
-- <History>
---- CREATE BY: HOAI THANH, DATE: 27/03/2025
-- <Example>
-- EXEC RP0001 @RoomId='F791DE32-F4E0-4EC2-8F96-7DC36A67DE4D'
-- </Summary>

CREATE PROCEDURE RP0001
(
	@RoomId VARCHAR(50)
)  
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
       R.Id,
       R.HotelId,
       R.Name,
       R.Description,
       R.PricePerNight,
       R.Capacity,
       R.Amenities,
       R.TotalRooms,
       R.AvailableRooms,
       H.Name AS HotelName,
		(
			SELECT STRING_AGG(ImageUrl, ',') 
			FROM RoomImages 
			WHERE RoomId = R.Id
		) AS ImageUrls
    FROM Rooms R
    LEFT JOIN Hotels H ON R.HotelId = H.Id
    WHERE R.Id = @RoomId AND R.IsDeleted = 0
END
GO

