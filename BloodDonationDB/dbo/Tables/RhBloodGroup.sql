CREATE TABLE [dbo].[RhBloodGroup] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (10) NOT NULL,
    CONSTRAINT [PK_RhBloodGroup] PRIMARY KEY CLUSTERED ([Id] ASC)
);

