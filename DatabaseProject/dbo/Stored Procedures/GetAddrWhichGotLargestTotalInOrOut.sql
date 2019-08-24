-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE GetAddrWhichGotLargestTotalInOrOut
	-- Add the parameters for the stored procedure here	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
WITH 
inTxs AS
( 
    SELECT TOP 100 blockhash, SUM(valueIn) AS Total, 'inTx' as txType     
	FROM [dbo].[TransactionDetails]
	
	-- for those operations which have a value above 100 bitcoins
	
	-- if you mean we need include only address where at least 1 transaction value > 100 btc
	--where blockhash IN (SELECT blockhash FROM [dbo].[TransactionDetails] where valueIn > 100)
	
	-- if you mean what we need sum only transactions with value > 100
	-- than where will be
	where valueIn > 100
	
	GROUP BY blockhash
	HAVING SUM(valueIn) > 100 order by SUM(valueIn) desc
),

outTxs AS
( 
    SELECT TOP 100 blockhash, SUM(valueOut) AS Total, 'outTx' as txType   
	FROM [dbo].[TransactionDetails]
	
	-- for those operations which have a value above 100 bitcoins
	
	-- if you mean we need include only address where at least 1 transaction value > 100 btc
	--where blockhash IN (SELECT blockhash FROM [dbo].[TransactionDetails] where valueIn > 100)
	
	-- if you mean what we need sum only transactions with value > 100
	-- than where will be
	where valueOut > 100
	
	GROUP BY blockhash
	HAVING SUM(valueOut) > 100 order by SUM(valueOut) desc
)


select * from inTxs 
union all 
select * from outTxs order by txType

END
