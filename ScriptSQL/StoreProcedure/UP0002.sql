IF EXISTS (SELECT TOP 1 1 FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[DBO].[UP0002]') AND  OBJECTPROPERTY(ID, N'IsProcedure') = 1)			
DROP PROCEDURE [DBO].UP0002
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- <Summary>
---- Update User Profile 
---- 
-- <History>
---- CREATE BY: HOAI THANH, DATE: 17/03/2025
-- <Example>
-- EXEC UP0002 @UserId='C34A64B6-E6B4-4014-862B-047D0FE81865', @FullName='hello', @Phone='0920123212'
-- </Summary>

CREATE PROCEDURE UP0002
(
	@UserId VARCHAR(50),
    @FullName NVARCHAR(50),
	@Phone VARCHAR(20)
)  
AS
BEGIN
    UPDATE Users
    SET FullName = @FullName,
        Phone = @Phone,
        UpdatedAt = GETDATE()
    WHERE Id = @UserId;
END
GO

