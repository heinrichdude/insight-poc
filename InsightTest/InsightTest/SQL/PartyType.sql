CREATE TABLE [dbo].[PartyType](
	[PartyTypeId] [int] NOT NULL,
	[PartyTypeCode] [varchar](20) NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](2000) NULL,
	[FromDate] [datetime] NOT NULL,
	[ThruDate] [datetime] NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatePartyId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdatePartyId] [int] NOT NULL)
GO

ALTER TABLE [dbo].[PartyType]  WITH CHECK ADD    CONSTRAINT [PartyType_PK] PRIMARY KEY CLUSTERED 
([PartyTypeId] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
