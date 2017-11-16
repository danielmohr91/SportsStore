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

BEGIN TRY 
select * from dbo.Note

-- Old Names
 --'Rod Knee', 1),
 --'Perry Scope', 1),
 --'Fran Tick', 1),
 --'Clyde Stale', 1),
 --'Ann Chovey', 1),
 --'Barry Cuda', 1),
 --'Frank Furter', 1),
 --'Marsha Mellow', 1),
 --'Chris P. Bacon', 1),
 --'Di Allysis', 1),
 --'Joseph Arimathea', 1),
 --'Mary Magdalene', 1)

END TRY  
BEGIN CATCH  
    print 'seed script error(s) :('
END CATCH  
GO
