CREATE TABLE [dbo].[Addresses]
(
     [AddressId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
     [Street1] NVARCHAR(250) NULL, 
     [Street2] NVARCHAR(250) NULL, 
     [City] NVARCHAR(250) NULL, 
     [State] NVARCHAR(250) NULL, 
     [Zip] NVARCHAR(25) NULL, 
     [Country] NVARCHAR(100) NULL, 
     [NoteId] UNIQUEIDENTIFIER NULL,
     CONSTRAINT [FK_Address_Note] FOREIGN KEY ([NoteId]) REFERENCES [dbo].[Note] ([NoteId])
)
