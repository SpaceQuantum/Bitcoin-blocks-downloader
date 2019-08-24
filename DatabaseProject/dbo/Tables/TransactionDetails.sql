CREATE TABLE [dbo].[TransactionDetails] (
    [txid]        VARCHAR (200)   NOT NULL,
    [valueOut]    DECIMAL (18, 8) NULL,
    [valueIn]     DECIMAL (18, 8) NULL,
    [blockhash]   VARCHAR (200)   NOT NULL,
    [blockheight] INT             NOT NULL,
    CONSTRAINT [PK_TransactionDetails] PRIMARY KEY CLUSTERED ([txid] ASC)
);


GO
CREATE NONCLUSTERED INDEX [inx_valueOut]
    ON [dbo].[TransactionDetails]([valueOut] ASC)
    INCLUDE([blockhash]);


GO
CREATE NONCLUSTERED INDEX [inx_valueIn]
    ON [dbo].[TransactionDetails]([valueIn] ASC)
    INCLUDE([blockhash]);


GO
CREATE NONCLUSTERED INDEX [inx_blockhash]
    ON [dbo].[TransactionDetails]([blockhash] ASC);

