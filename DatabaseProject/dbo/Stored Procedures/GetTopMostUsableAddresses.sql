-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE GetTopMostUsableAddresses
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT TOP 100 blockhash, COUNT(blockhash) AS totaltransactions   
	FROM [dbo].[TransactionDetails]
	GROUP BY blockhash
	HAVING COUNT(blockhash) > 100 order by COUNT(blockhash) desc
END
