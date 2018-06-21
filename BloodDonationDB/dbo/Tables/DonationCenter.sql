CREATE TABLE [dbo].[DonationCenter] (
    [Id]                   INT          IDENTITY (1, 1) NOT NULL,
    [Name]                 VARCHAR (50) NOT NULL,
    [DonationCenterTypeId] INT          NOT NULL,
    [RegionId]             INT          NULL,
    [AddressId]            INT          NULL,
    [OpeningHoursId]       INT          NULL,
    CONSTRAINT [PK_DonationCenter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DonationCenter_Address] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([Id]),
    CONSTRAINT [FK_DonationCenter_DonationCenterType] FOREIGN KEY ([DonationCenterTypeId]) REFERENCES [dbo].[DonationCenterType] ([Id]),
    CONSTRAINT [FK_DonationCenter_Opening Hours] FOREIGN KEY ([OpeningHoursId]) REFERENCES [dbo].[OpeningHours] ([Id]),
    CONSTRAINT [FK_DonationCenter_Region] FOREIGN KEY ([RegionId]) REFERENCES [dbo].[Region] ([Id])
);







