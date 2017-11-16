CREATE TABLE [dbo].[Note]
(
     [NoteId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
     [Text] NVARCHAR(MAX) NULL, 
     [DateCreated] DateTime NULL, 
     [CreatedByUserId] nvarchar(128) NULL, 
     [DateModified] DateTime NULL, 
     [ModifiedByUserId] nvarchar(128) NULL,
     CONSTRAINT [FK_Note_CreatedBy] FOREIGN KEY ([CreatedByUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
     CONSTRAINT [FK_Note_ModifiedBy] FOREIGN KEY ([ModifiedByUserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
)
