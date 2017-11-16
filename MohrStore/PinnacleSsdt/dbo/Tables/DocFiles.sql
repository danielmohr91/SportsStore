
CREATE TABLE [dbo].[DocFiles](
	[FileId] [bigint] IDENTITY(1,1) NOT NULL,
	[DocumentId] [bigint] NOT NULL,
	[DocName] [varchar](50) NULL,
	[Sequence] [int] NOT NULL,
 CONSTRAINT [PK_DocFiles] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[DocFiles]  WITH CHECK ADD  CONSTRAINT [FK_Files_Documents] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[Documents] ([DocumentId])
GO

ALTER TABLE [dbo].[DocFiles] CHECK CONSTRAINT [FK_Files_Documents]
GO


