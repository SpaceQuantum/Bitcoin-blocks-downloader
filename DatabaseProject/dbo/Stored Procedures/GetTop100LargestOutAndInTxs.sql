-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetTop100LargestOutAndInTxs]
	-- Add the parameters for the stored procedure here	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
WITH 
InTxs AS
( 
SELECT TOP 100 blockhash, txid, valueIn as [value], 'inTx' as txType 
FROM [dbo].[TransactionDetails]
order by valueIn desc
),

OutTxs AS
( 
SELECT TOP 100 blockhash, txid, valueOut as [value], 'outTx' as txType  
FROM [dbo].[TransactionDetails]
order by valueOut desc
)

select * from InTxs 
union all 
select * from OutTxs order by txType
END
