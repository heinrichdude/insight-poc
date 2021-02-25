CREATE TABLE [dbo].[Party](
	[PartyId] [int] IDENTITY(1,1) NOT NULL,
	[PartyTypeId] [int] NOT NULL,
	[FromDate] [datetime] NOT NULL,
	[ThruDate] [datetime] NULL,
	[RegisteredFlag] [char](1) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatePartyId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdatePartyId] [int] NOT NULL)
GO

ALTER TABLE [dbo].[Party]  WITH CHECK ADD   CONSTRAINT [Party_PK] PRIMARY KEY CLUSTERED 
([PartyId] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Party] ADD  DEFAULT ('Y') FOR [RegisteredFlag]
GO

ALTER TABLE [dbo].[Party]  WITH CHECK ADD  CONSTRAINT [Party_FK01] FOREIGN KEY([PartyTypeId])
REFERENCES [dbo].[PartyType] ([PartyTypeId])
GO

ALTER TABLE [dbo].[Party] CHECK CONSTRAINT [Party_FK01]
GO

ALTER TABLE [dbo].[Party]  WITH CHECK ADD  CONSTRAINT [Party_CK01] CHECK  (([RegisteredFlag]='N' OR [RegisteredFlag]='Y'))
GO

ALTER TABLE [dbo].[Party] CHECK CONSTRAINT [Party_CK01]
GO