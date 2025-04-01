IF EXISTS (SELECT TOP 1 1 FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[DBO].[SRP0001]') AND  OBJECTPROPERTY(ID, N'IsProcedure') = 1)			
DROP PROCEDURE [DBO].SRP0001
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- <Summary>
---- Get Status Support Request by SRId
---- 
-- <History>
---- CREATE BY: HOAI THANH, DATE: 01/04/2025
-- <Example>
-- EXEC SRP0001 @SRId='9F2CB823-60AD-44C4-9460-25CD21E366D6'
-- </Summary>

CREATE PROCEDURE SRP0001
(
	@SRId VARCHAR(50)
)  
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
        SR.Id,
		SR.StatusId,
		S.Name AS StatusName,
		SR.UserId AS UserCreatedId,
		U.FullName AS UserCreatedName,
		SR.Subject,
		SR.Message,
		SR.CreatedAt,
		SR.UpdatedAt
    FROM SupportRequests SR
    LEFT JOIN Status S ON SR.StatusId = S.Id
    LEFT JOIN Users U ON SR.UserId = U.Id
    WHERE SR.Id = @SRId
END
GO

