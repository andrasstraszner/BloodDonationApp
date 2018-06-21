CREATE TABLE [dbo].[PhoneNumber] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [DonationCenterId] INT           NOT NULL,
    [PhoneNumber]      VARCHAR (20)  NOT NULL,
    [Comments]         VARCHAR (MAX) NULL,
    CONSTRAINT [PK_PhoneNumber] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PhoneNumber_DonationCenter] FOREIGN KEY ([DonationCenterId]) REFERENCES [dbo].[DonationCenter] ([Id])
);



