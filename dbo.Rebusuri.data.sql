SET IDENTITY_INSERT [dbo].[Rebusuri] ON
DBCC CHECKIDENT('Rebusuri',RESEED,0)
INSERT INTO [dbo].[Rebusuri] ([IdRebus], [DenumireRebus], [NrColoane], [NrLinii], [TimpEstimat]) VALUES (1, N'dorinte                                           ', 10, 10, 2450)
SET IDENTITY_INSERT [dbo].[Rebusuri] OFF
