CREATE TABLE [dbo].[OpeningHours] (
    [Id]        INT          IDENTITY (1, 1) NOT NULL,
    [Monday]    VARCHAR (25) NULL,
    [Tuesday]   VARCHAR (25) NULL,
    [Wednesday] VARCHAR (25) NULL,
    [Thursday]  VARCHAR (25) NULL,
    [Friday]    VARCHAR (25) NULL,
    [Saturday]  VARCHAR (25) NULL,
    [Sunday]    VARCHAR (25) NULL,
    CONSTRAINT [PK_Opening Hours] PRIMARY KEY CLUSTERED ([Id] ASC)
);

