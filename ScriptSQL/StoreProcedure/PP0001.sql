IF EXISTS (SELECT TOP 1 1 FROM DBO.SYSOBJECTS WHERE ID = OBJECT_ID(N'[DBO].[PP0001]') AND  OBJECTPROPERTY(ID, N'IsProcedure') = 1)			
DROP PROCEDURE [DBO].PP0001
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

-- <Summary>
---- Get Status Payment by BookingId
---- 
-- <History>
---- CREATE BY: HOAI THANH, DATE: 30/03/2025
-- <Example>
-- EXEC PP0001 @BookingId='9F2CB823-60AD-44C4-9460-25CD21E366D6'
-- </Summary>

CREATE PROCEDURE PP0001
(
	@BookingId VARCHAR(50)
)  
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
        P.BookingId,
		P.StatusId,
		S.Name AS StatusName,
		P.PaidAt,
		P.TransactionId,
		PM.Name AS PaymentMethodsName,
		P.Amount
    FROM Payments P
    LEFT JOIN Status S ON P.StatusId = S.Id
    LEFT JOIN PaymentMethods PM ON P.PaymentMethodId = PM.Id
    WHERE P.BookingId = @BookingId
END
GO

