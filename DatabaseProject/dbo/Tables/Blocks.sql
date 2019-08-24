CREATE TABLE [dbo].[Blocks] (
    [hash]                       VARCHAR (200) NOT NULL,
    [blockNumber]                INT           NOT NULL,
    [size]                       INT           NULL,
    [height]                     INT           NULL,
    [version]                    INT           NULL,
    [merkleroot]                 VARCHAR (50)  NULL,
    [tx]                         VARCHAR (MAX) NULL,
    [time]                       INT           NULL,
    [nonce]                      INT           NULL,
    [bits]                       VARCHAR (50)  NULL,
    [difficulty]                 FLOAT (53)    NULL,
    [chainwork]                  VARCHAR (50)  NULL,
    [confirmations]              INT           NULL,
    [previousblockhash]          VARCHAR (200) NULL,
    [nextblockhash]              VARCHAR (200) NULL,
    [reward]                     FLOAT (53)    NULL,
    [isMainChain]                BIT           NULL,
    [isAllTransactionLoadedToDb] BIT           CONSTRAINT [DF_Blocks_isAllTransactionLoadedToDb] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_Blocks] PRIMARY KEY CLUSTERED ([hash] ASC)
);

