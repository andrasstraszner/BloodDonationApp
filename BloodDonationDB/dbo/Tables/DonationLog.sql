CREATE TABLE [dbo].[DonationLog] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [DonorId]          INT           NOT NULL,
    [DonationCenterId] INT           NOT NULL,
    [DonationDate]     DATE          NOT NULL,
    [IsSuccessful]     BIT           NOT NULL,
    [NextDate]         DATE          NOT NULL,
    [Comments]         VARCHAR (MAX) NULL,
    CONSTRAINT [PK_DonationLog] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DonationLog_DonationCenter] FOREIGN KEY ([DonationCenterId]) REFERENCES [dbo].[DonationCenter] ([Id]),
    CONSTRAINT [FK_DonationLog_Donor] FOREIGN KEY ([DonorId]) REFERENCES [dbo].[Donor] ([Id])
);



