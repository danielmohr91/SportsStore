CREATE TABLE [dbo].[Documents](
	[DocumentId] [bigint] IDENTITY(1,1) NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdatedById] [bigint] NULL,
	[DocumentName] [varchar](80) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[OwnerName] [varchar](50) NULL,
	[Description] [varchar](250) NULL,
	[Keywords] [varchar](250) NOT NULL,
 CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED 
(
	[DocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO