CREATE TABLE [dbo].[Facility](
	[FacilityId] [int] IDENTITY(1,1) NOT NULL,
	[ParentFacilityId] [int] NULL,
	[FacilityTypeId] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Description] [varchar](2000) NULL,
	[FacilityNumber] [varchar](10) NULL,
	[FromDate] [datetime] NOT NULL,
	[ThruDate] [datetime] NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatePartyId] [int] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdatePartyId] [int] NOT NULL)
GO

ALTER TABLE [dbo].[Facility]  WITH CHECK ADD   CONSTRAINT [Facility_PK] PRIMARY KEY CLUSTERED 
([FacilityId] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Facility]  WITH CHECK ADD  CONSTRAINT [Facility_FK01] FOREIGN KEY([FacilityTypeId])
REFERENCES [dbo].[FacilityType] ([FacilityTypeId])
GO

ALTER TABLE [dbo].[Facility] CHECK CONSTRAINT [Facility_FK01]
GO

ALTER TABLE [dbo].[Facility]  WITH CHECK ADD  CONSTRAINT [Facility_FK02] FOREIGN KEY([ParentFacilityId])
REFERENCES [dbo].[Facility] ([FacilityId])
GO

ALTER TABLE [dbo].[Facility] CHECK CONSTRAINT [Facility_FK02]
GO