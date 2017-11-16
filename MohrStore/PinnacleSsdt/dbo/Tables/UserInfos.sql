CREATE TABLE [dbo].[UserInfos] (
    [UserInfoId]            UNIQUEIDENTIFIER CONSTRAINT [DF_UserInfos_UserInfoId] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [AddressId]             UNIQUEIDENTIFIER NULL,
    [FirstName]             NVARCHAR (100)   NOT NULL,
    [LastName]              NVARCHAR (100)   NOT NULL,
    [Suffix]                NVARCHAR (50)    NULL,
    [IsMale]                BIT              NULL,
    [Photo]                 VARBINARY        NULL, -- http://research.microsoft.com/apps/pubs/default.aspx?id=64525 - if your pictures or document are typically below 256K in size, storing them in a database VARBINARY column is more efficient
    [HomePhone]             NVARCHAR (50)    NULL,
    [WorkPhone]             NVARCHAR (50)    NULL,
    [CellPhone]             NVARCHAR (50)    NULL,
    [Email]                 NVARCHAR (100)   NULL,
    [DateOfBirth]           Date             NULL,
    [SocialSecurityNumber]  NVARCHAR(15)     NULL,
    [NoteId]                UniqueIdentifier NULL,
    CONSTRAINT [PK_UserInfos] PRIMARY KEY CLUSTERED ([UserInfoId] ASC) WITH (FILLFACTOR = 80),
    CONSTRAINT [FK_UserInfos_Address] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Addresses] ([AddressId]),
    CONSTRAINT [FK_UserInfo_Note] FOREIGN KEY ([NoteId]) REFERENCES [dbo].[Notes] ([NoteId])
);
