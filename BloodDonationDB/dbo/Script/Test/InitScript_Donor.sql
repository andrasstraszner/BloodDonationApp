
SET IDENTITY_INSERT [dbo].[Donor] ON 

INSERT [dbo].[Donor] ([Id], [Email], [LastName], [FirstName], [DonorSexId], [BirthDate]) VALUES (1, 'a@b.com', 'Kis','Pista', 1, '1975-11-11')

SET IDENTITY_INSERT [dbo].[Donor] OFF
