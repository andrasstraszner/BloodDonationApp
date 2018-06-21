CREATE TABLE [dbo].[DonationCenterType] (
    [Id]   INT          IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (30) NOT NULL,
    CONSTRAINT [PK_DonationCenterType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

