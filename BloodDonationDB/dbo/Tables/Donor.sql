CREATE TABLE [dbo].[Donor] (
    [Id]                    INT           IDENTITY (1, 1) NOT NULL,
    [Email]                 VARCHAR (100) NOT NULL,
    [LastName]                  VARCHAR (50)  NOT NULL,
    [FirstName]                  VARCHAR (50)  NOT NULL,
    [BirthDate]             DATE          NOT NULL,
    [DonorSexId]            INT           NOT NULL,
    [SocialInsuranceNumber] CHAR(11)           NULL,
    [ABOBloodGroupId]       INT           NULL,
    [RhBloodGroupId]        INT           NULL,
    CONSTRAINT [PK_Donor] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Donor_ABOBloodGroup] FOREIGN KEY ([ABOBloodGroupId]) REFERENCES [dbo].[ABOBloodGroup] ([Id]),
    CONSTRAINT [FK_Donor_DonorSex] FOREIGN KEY ([DonorSexId]) REFERENCES [dbo].[DonorSex] ([Id]),
    CONSTRAINT [FK_Donor_RhBloodGroup] FOREIGN KEY ([RhBloodGroupId]) REFERENCES [dbo].[RhBloodGroup] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Donor]
    ON [dbo].[Donor]([Email] ASC);

