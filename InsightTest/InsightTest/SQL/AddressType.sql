CREATE TABLE [dbo].[AddressType](
	[AddressTypeId] [int] NOT NULL,
	[AddressTypeCode] [varchar](20) NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](2000) NULL,
	[FromDate] [datetime] NOT NULL,
	[ThruDate] [datetime] NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatePartyId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdatePartyId] [int] NOT NULL)
GO

ALTER TABLE [dbo].[AddressType]  WITH CHECK ADD   CONSTRAINT [Addresstype_PK] PRIMARY KEY CLUSTERED 
([AddressTypeId] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

-- automatically generates Select/Insert/Update/Delete/Find and more
-- AUTOPROC All [AddressType] Single=AddressType Plural=AddressTypes
GO