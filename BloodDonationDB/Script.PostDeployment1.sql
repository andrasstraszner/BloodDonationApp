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

:r dbo\Script\Init\InitScript_DonationCenterType.sql
:r dbo\Script\Init\InitScript_Region.sql
:r dbo\Script\Init\InitScript_Address.sql
:r dbo\Script\Init\InitScript_OpeningHours.sql
:r dbo\Script\Init\InitScript_DonationCenter.sql
:r dbo\Script\Init\InitScript_PhoneNumber.sql
:r dbo\Script\Init\InitScript_DonorSex.sql
:r dbo\Script\Init\InitScript_ABOBloodGroup.sql
:r dbo\Script\Init\InitScript_RhBloodGroup.sql

:r dbo\Script\Test\InitScript_Donor.sql
