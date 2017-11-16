﻿/*
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

-- Shared Ids
declare @hospitalid1 uniqueidentifier, @hospitalid2 uniqueidentifier, 
    @hospitalRate1 uniqueidentifier, @hospitalRate2  uniqueidentifier,
    @nurseRate1 uniqueidentifier, @nurseRate2  uniqueidentifier,
    @hospitalTaxRate1 uniqueidentifier, @hospitalTaxRate2  uniqueidentifier,
    @nurseTaxRate1 uniqueidentifier, @nurseTaxRate2  uniqueidentifier,
    @hospitalAddress1 uniqueidentifier, @nurseAddress1 uniqueidentifier, @nurseAddress2 uniqueidentifier,
    @hospitalUserInfo1 uniqueidentifier, @nurseUserInfo1 uniqueidentifier, @nurseUserInfo2 uniqueidentifier,
    @nurseId1 uniqueidentifier, @nurseId2 uniqueidentifier

set @hospitalid1 = 'f3a7bcc6-3e6a-4248-8e92-cf75c1f43464'
set @hospitalid2 = '3961fbf7-6cb6-4503-a186-81a881908b78'
set @hospitalAddress1 = '10728DAD-3DED-48EF-9120-303C833D1391'
set @nurseAddress1 = '88e5f097-b332-4f75-bc9c-a65113bc38e5'
set @nurseAddress2 = 'e7592699-74b7-449e-89f1-37e423d2d3fb'
set @nurseRate1 = '856ee973-a33b-43a5-acc6-537da0696f2a'
set @nurseRate2 = 'd9559fa6-4b66-4fca-b4ca-9e1e307acd01'
set @hospitalRate1 = 'e4591568-d40a-4bf7-a7c7-8d33b6a327b8'
set @hospitalRate2 = 'a403578d-480f-48a8-b250-f4ae6b83cdbe'
set @hospitalTaxRate1 = '5980a89e-22e4-4549-86eb-12c771175571'
set @hospitalTaxRate2 = 'b1d3e0f9-8a8b-4dac-879a-0ff88bfcf54c'
set @nurseTaxRate1 = '2c1c3e6d-9237-487d-ab94-8bc20bea92ff'
set @nurseTaxRate2 = 'c2ef1ce5-998f-42bd-b5bf-ab3d50530428'
set @hospitalUserInfo1 ='f540826d-8d06-4e7f-a645-7b94ac78241c'
set @nurseUserInfo1 = '2b9c5c47-164a-4a19-891a-9d9744466b39'
set @nurseUserInfo2 = 'f4aa432b-6d29-49ec-a72c-98fc3c9a401d'
set @nurseId1 = 'f4aa432b-6d29-49ec-a72c-98fc3c9a401d'
set @nurseId2 = '63d43452-f63b-4db8-88ff-11c74b9d98d3'

-- Generated Guids to pull from
--7347361f-3ea2-4967-ad3d-115ec3a6b27b
--91d0656f-eada-46e1-bb4d-ee67738dbc97
--a62abd5b-9e70-408b-be80-f82c687b2e99
--849df9a9-c153-4b9e-a5e6-f86515c7b285
--4ea1eaca-e2c1-4c47-97e0-54670eed60df
--bb5da26a-5b16-4aaf-ab3d-076b460e10a5
--c889afd4-302f-476b-88e4-19f770090693
--25a50c69-9240-432a-91a7-514d4ba0d367
--b23f137e-b072-49ec-a435-c6ce905db2fa
--122e2afd-dec3-4944-88cd-8dbe26587d14
--7bd8f96c-fcd0-4848-9b2d-98d61135f248
--d949d44e-0966-4a6c-ac0e-671cb7178639
--a8202dae-3d36-4983-af95-ccd042c02ee1
--ef5055d3-567d-4f3f-99ed-372409b61b5d
--8f9b4c50-f18d-4b66-a762-4ee1cc8d2f8b
--62ab09b9-5207-4df1-a042-f787a7e19f92

-- Addresses
merge into dbo.Addresses as target
using( values 
    (@hospitalAddress1, '123 Alvin Rd.', null, 'Grand Island', 'NY', '14072', 'USA'),
    (@nurseAddress1, '1234 Grand Island Blvd', null, 'Grand Island', 'NY', '14072', 'USA'),
	(@nurseAddress2, '1234 East River', null, 'Grand Island', 'NY', '14072', 'USA')
)
as s (AddressId, Street1, Street2, City, State, Zip, Country)
on target.AddressId = s.AddressId
when not matched by target then 
insert (AddressId, Street1, Street2, City, State, Zip, Country)
values (AddressId, Street1, Street2, City, State, Zip, Country);


-- User Infos
merge into dbo.UserInfos as target
using( values 
    (@nurseUserInfo1, @nurseAddress1, 'Perry', 'Scope', 'Dr.', 1, null, '716.773.8888', null, null, 'perryscope@gmail.com', '3/1/84', null, null), 
    (@nurseUserInfo2, @nurseAddress2, 'Ann', 'Chovey', null, 0, null, null, null, '716.909.8888', 'AnnChovey@live.com', '4/1/82', null, null),
    (@hospitalUserInfo1, @hospitalAddress1, 'Rod', 'Knee', null, 1, null, '111-111-1111', '222-222-2222', '333-333-3333', 'rknee@gimemorial.com', '5/1/72', null, null)
)
as s (UserInfoId, AddressId, FirstName, LastName, Suffix, IsMale, Photo, HomePhone, WorkPhone, CellPhone, Email, DateOfBirth, SocialSecurityNumber, NoteId)
on target.UserInfoId = s.UserInfoId
when not matched by target then 
insert (UserInfoId, AddressId, FirstName, LastName, Suffix, IsMale, Photo, HomePhone, WorkPhone, CellPhone, Email, DateOfBirth, SocialSecurityNumber, NoteId)
values (UserInfoId, AddressId, FirstName, LastName, Suffix, IsMale, Photo, HomePhone, WorkPhone, CellPhone, Email, DateOfBirth, SocialSecurityNumber, NoteId);


-- Hospitals
merge into dbo.Hospitals as target
using( values 
    (@hospitalid1, @hospitalAddress1, @hospitalUserInfo1, 'Grand Island Memorial Hospital', null)
)
as s (HospitalId, AddressId, PrimaryContactUserInfoId, Name, NoteId)
on target.HospitalId = s.HospitalId
when not matched by target then 
insert (HospitalId, AddressId, PrimaryContactUserInfoId, Name, NoteId)
values (HospitalId, AddressId, PrimaryContactUserInfoId, Name, NoteId);

-- TaxRates
merge into dbo.TaxRates as target
using( values 
    (@hospitalTaxRate1, 1.27), 
	--    (@hospitalTaxRate2, 1.29)
	(@nurseTaxRate1, 1.25), 
(@nurseTaxRate2, 1.26)

)
as s (TaxRateId, Rate)
on target.TaxRateId = s.TaxRateId
when not matched by target then 
insert (TaxRateId, Rate)
values (TaxRateId, Rate);

-- HospitalRates
merge into dbo.HospitalRates as target
using( values 
    (@hospitalRate1, @hospitalid1, 75, '1/1/17', '1/1/19', @hospitalTaxRate1, null)
    --(@hospitalRate2, @hospitalid2, 80, '11/20/2017', '2/19/18', @hospitalTaxRate2, null)
)
as s (HospitalRateId, HospitalId, HourlyRate, StartDate, EndDate, TaxRateId, NoteId)
on target.HospitalRateId = s.HospitalRateId
when not matched by target then 
insert (HospitalRateId, HospitalId, HourlyRate, StartDate, EndDate, TaxRateId, NoteId)
values (HospitalRateId, HospitalId, HourlyRate, StartDate, EndDate, TaxRateId, NoteId);

-- Nurses
merge into dbo.Nurses as target
using( values 
    (@nurseId1, @nurseUserInfo1, 1, null),
    (@nurseId2, @nurseUserInfo2, 1, null)
)
as s (NurseId, UserInfoId, IsActive, NoteId)
on target.NurseId = s.NurseId
when not matched by target then 
insert (NurseId, UserInfoId, IsActive, NoteId)
values (NurseId, UserInfoId, IsActive, NoteId);

-- NurseRates
merge into dbo.NurseRates as target
using( values 
    (@nurseRate1, @nurseId1, @hospitalRate1, 17, 40, 3.15, 990, '12/1/17', '3/5/18', @nurseTaxRate1, null), -- switch start date back to '12/1/17'
    (@nurseRate2, @nurseId2, @hospitalRate1, 19.50, 40, 3.35, 995, '11/20/2017', '2/19/18', @nurseTaxRate2, null) -- start date originally '11/20/2017'
)
as s (NurseRateId, NurseId, HospitalRateId, HourlyRate, HoursPerWeek, BenefitCostPerHour, MealsAndLodgingAllowance, StartDate, EndDate, TaxRateId, NoteId)
on target.NurseRateId = s.NurseRateId
when not matched by target then 
insert (NurseRateId, NurseId, HospitalRateId, HourlyRate, HoursPerWeek, BenefitCostPerHour, MealsAndLodgingAllowance, StartDate, EndDate, TaxRateId, NoteId)
values (NurseRateId, NurseId, HospitalRateId, HourlyRate, HoursPerWeek, BenefitCostPerHour, MealsAndLodgingAllowance, StartDate, EndDate, TaxRateId, NoteId);

-- TODO: REMOVE BEFORE GO LIVE - SET ALL START AND END DATES TO CURRENT
UPDATE DBO.NurseRates
set StartDate = '11/1/17', EndDate = '1/31/18'

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