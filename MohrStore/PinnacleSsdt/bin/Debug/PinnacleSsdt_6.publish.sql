﻿/*
Deployment script for Pinnacle

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Pinnacle"
:setvar DefaultFilePrefix "Pinnacle"
:setvar DefaultDataPath "C:\Users\dmohr\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB"
:setvar DefaultLogPath "C:\Users\dmohr\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


--truncate table dbo.Nurse
--truncate table dbo.Company
--truncate table dbo.Address
--truncate table dbo.Nurses
GO

GO
PRINT N'Dropping unnamed constraint on [dbo].[Nurses]...';


GO
ALTER TABLE [dbo].[Nurses] DROP CONSTRAINT [DF__Nurses__IsActive__300424B4];


GO
PRINT N'Dropping [dbo].[FK_Nurse_Address]...';


GO
ALTER TABLE [dbo].[Nurses] DROP CONSTRAINT [FK_Nurse_Address];


GO
PRINT N'Starting rebuilding table [dbo].[Nurses]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Nurses] (
    [NurseId]   UNIQUEIDENTIFIER NOT NULL,
    [CompanyId] UNIQUEIDENTIFIER NULL,
    [AddressId] UNIQUEIDENTIFIER NULL,
    [Name]      NVARCHAR (250)   NOT NULL,
    [IsActive]  BIT              DEFAULT 1 NOT NULL,
    PRIMARY KEY CLUSTERED ([NurseId] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Nurses])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_Nurses] ([NurseId], [CompanyId], [AddressId], [Name], [IsActive])
        SELECT   [NurseId],
                 [CompanyId],
                 [AddressId],
                 [Name],
                 [IsActive]
        FROM     [dbo].[Nurses]
        ORDER BY [NurseId] ASC;
    END

DROP TABLE [dbo].[Nurses];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Nurses]', N'Nurses';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[FK_Nurse_Address]...';


GO
ALTER TABLE [dbo].[Nurses] WITH NOCHECK
    ADD CONSTRAINT [FK_Nurse_Address] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Addresses] ([AddressId]);


GO
/*
Post-Deployment Script Template                                   
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.          
 Use SQLCMD syntax to include a file in the post-deployment script.               
 Example:      :r .\myfile.sql                                        
 Use SQLCMD syntax to reference a variable in the post-deployment script.          
 Example:      :setvar TableName MyTable                                   
               SELECT * FROM [$(TableName)]                         
--------------------------------------------------------------------------------------
*/


-- This is run every install and upgrade. Must be idempotent.
--      https://www.mssqltips.com/sqlservertutorial/9004/insert-inital-seed-data-into-the-table-in-ssdt/

-- Comparison isn't working... hitting timeout. Run publish for now to deploy changes in ssdt project to db.
--      https://www.mssqltips.com/sqlservertutorial/9005/publish-the-database-using-ssdt/


-- TODO - make this re-runnable
--use Pinnacle
--go

BEGIN TRY 
insert into dbo.[Addresses]
values ('10728DAD-3DED-48EF-9120-303C833D1391', '123 Alvin Rd.', null, 'Grand Island', 'NY', 14072, 'USA'),
('0403F799-D4DA-49E1-AC90-8D97C0CA2A76', '1234 Grand Island Blvd', null, 'Grand Island', 'NY', 14072, 'USA')

insert into dbo.Companies
select 'C95066AB-AD1D-49E8-9B6C-7D65B4176DC8', '10728DAD-3DED-48EF-9120-303C833D1391', 'Test Company'

insert into dbo.Nurses
values
(newid(), null, '0403F799-D4DA-49E1-AC90-8D97C0CA2A76', 'Rod Knee', 1),
(newid(), null, '0403F799-D4DA-49E1-AC90-8D97C0CA2A76', 'Perry Scope', 1),
(newid(), null, '0403F799-D4DA-49E1-AC90-8D97C0CA2A76', 'Fran Tick', 1),
(newid(), null, '0403F799-D4DA-49E1-AC90-8D97C0CA2A76', 'Clyde Stale', 1),
(newid(), null, '0403F799-D4DA-49E1-AC90-8D97C0CA2A76', 'Ann Chovey', 1),
(newid(), null, '0403F799-D4DA-49E1-AC90-8D97C0CA2A76', 'Barry Cuda', 1),
(newid(), null, '0403F799-D4DA-49E1-AC90-8D97C0CA2A76', 'Frank Furter', 1),
(newid(), null, '0403F799-D4DA-49E1-AC90-8D97C0CA2A76', 'Marsha Mellow', 1),
(newid(), null, '0403F799-D4DA-49E1-AC90-8D97C0CA2A76', 'Chris P. Bacon', 1),
(newid(), null, '0403F799-D4DA-49E1-AC90-8D97C0CA2A76', 'Di Allysis', 1),
(newid(), null, '0403F799-D4DA-49E1-AC90-8D97C0CA2A76', 'Joseph Arimathea', 1),
(newid(), null, '0403F799-D4DA-49E1-AC90-8D97C0CA2A76', 'Mary Magdalene', 1)
END TRY  
BEGIN CATCH  
    print 'seed script error(s) :('
END CATCH  
GO

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Nurses] WITH CHECK CHECK CONSTRAINT [FK_Nurse_Address];


GO
PRINT N'Update complete.';


GO