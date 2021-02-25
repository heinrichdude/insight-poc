CREATE TABLE [dbo].[Address](
	[AddressId] [int] IDENTITY(1,1) NOT NULL,
	[AddressTypeId] [int] NOT NULL,
	[PartyId] [int] NULL,
	[FacilityId] [int] NULL,
	[Description] [varchar](300) NULL,
	[VerifiedFlag] [char](1) NOT NULL,
	[PrimaryFlag] [char](1) NOT NULL,
	[UnlistedFlag] [char](1) NOT NULL,
	[FromDate] [datetime] NOT NULL,
	[ThruDate] [datetime] NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatePartyId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdatePartyId] [int] NOT NULL)
GO

ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [Address_PK] PRIMARY KEY CLUSTERED 
([AddressId] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Address] ADD  DEFAULT ('N') FOR [VerifiedFlag]
GO

ALTER TABLE [dbo].[Address] ADD  DEFAULT ('N') FOR [PrimaryFlag]
GO

ALTER TABLE [dbo].[Address] ADD  DEFAULT ('N') FOR [UnlistedFlag]
GO

ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [Address_FK01] FOREIGN KEY([AddressTypeId])
REFERENCES [dbo].[AddressType] ([AddressTypeId])
GO

ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [Address_FK01]
GO

ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [Address_FK02] FOREIGN KEY([PartyId])
REFERENCES [dbo].[Party] ([PartyId])
GO

ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [Address_FK02]
GO

ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [Address_FK03] FOREIGN KEY([FacilityId])
REFERENCES [dbo].[Facility] ([FacilityId])
GO

ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [Address_FK03]
GO

ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [Address_CK01] CHECK  (([VerifiedFlag]='N' OR [VerifiedFlag]='Y'))
GO

ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [Address_CK01]
GO

ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [Address_CK02] CHECK  (([PrimaryFlag]='N' OR [PrimaryFlag]='Y'))
GO

ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [Address_CK02]
GO

ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [Address_CK03] CHECK  (([UnlistedFlag]='N' OR [UnlistedFlag]='Y'))
GO

ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [Address_CK03]
GO

-- automatically generates Select/Insert/Update/Delete/Find and more
-- AUTOPROC All [Address] Single=Address Plural=Addresses
GO