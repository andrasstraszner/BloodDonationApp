CREATE TABLE [dbo].[Address] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [PostalCode]  INT           NOT NULL,
    [City]        VARCHAR (50)  NOT NULL,
    [Street]      VARCHAR (50)  NOT NULL,
    [HouseNumber] VARCHAR (20)  NOT NULL,
    [Comments]    VARCHAR (MAX) NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([Id] ASC)
);

