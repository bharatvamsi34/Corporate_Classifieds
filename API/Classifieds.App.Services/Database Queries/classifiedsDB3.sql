
Create Table Employee (Id int IDENTITY(1,1) PRIMARY KEY,Name varchar(50),Email varchar(255),Location varchar(1024),Phone varchar(10), ProfilePic varchar(255), Password varchar(255));

Create Table Category (Id int IDENTITY(1,1) PRIMARY KEY,Name varchar(50),Description varchar(255), CreatedOn datetime,IconId int, Status varchar(10));

Create Table Fields (Id int IDENTITY(1,1) PRIMARY KEY, CategoryId int REFERENCES Category(Id) ON 
					DELETE NO ACTION ON UPDATE NO ACTION, Name varchar(50), Datatype varchar(10),Mandatory bit,Position int);

Create Table Advertisement (Id int IDENTITY(1,1) PRIMARY KEY, EmployeeId int REFERENCES Employee(Id) ON DELETE NO ACTION ON UPDATE 
							NO ACTION, SellingType varchar(20) NOT NULL,CategoryId int REFERENCES Category(Id) ON 
							DELETE NO ACTION ON UPDATE NO ACTION,Expiry int NOT NULL,PostedOn datetime, Status varchar(30), DisplayPhone bit);


Create Table AdminMessage (Id int IDENTITY(1,1) PRIMARY KEY, AdvertisementId int REFERENCES Advertisement(Id) ON DELETE NO ACTION ON UPDATE NO ACTION, Message varchar(225));

Create Table AdvertisementDetails (Id int IDENTITY(1,1) PRIMARY KEY,AdvertisementId int REFERENCES Advertisement(Id) ON DELETE NO ACTION ON 
									UPDATE NO ACTION, FieldName varchar(20), Value varchar(255));

Create Table Inbox (Id int IDENTITY(1,1) PRIMARY KEY,FromId int REFERENCES Employee(Id) ON DELETE NO ACTION ON UPDATE NO ACTION, 
					ToId int REFERENCES Employee(Id) ON DELETE NO ACTION ON UPDATE NO ACTION, AdvertisementId int REFERENCES 
					Advertisement(Id) ON DELETE NO ACTION ON UPDATE NO ACTION, Message varchar(225),Time datetime,);
					
Create Table Comments (Id int IDENTITY(1,1) PRIMARY KEY,EmployeeId int REFERENCES Employee(Id) ON DELETE NO ACTION ON UPDATE NO 
					   ACTION, AdvertisementId int REFERENCES Advertisement(Id) ON DELETE NO ACTION ON UPDATE NO ACTION, Comment 
					   varchar(225),Time datetime,);
					   
Create Table Offer (Id int IDENTITY(1,1) PRIMARY KEY,EmployeeId int REFERENCES Employee(Id) ON DELETE NO ACTION ON UPDATE NO 
					ACTION, AdvertisementId int REFERENCES Advertisement(Id) ON DELETE NO ACTION ON UPDATE NO ACTION, 
					OfferedPrice int NOT NULL,Time datetime,);
					
Create Table Report (Id int IDENTITY(1,1) PRIMARY KEY,EmployeeId int REFERENCES Employee(Id) ON DELETE NO ACTION ON UPDATE NO 
					   ACTION, AdvertisementId int REFERENCES Advertisement(Id) ON DELETE NO ACTION ON UPDATE NO ACTION, 
					   Category varchar(10) NOT NULL,Description varchar(225) NOT NULL,IsApproved bit,Time DateTime,IsDeleted bit);

					   
Create Table Images (Id int IDENTITY(1,1) PRIMARY KEY,AdvertisementId int REFERENCES Advertisement(Id) ON DELETE NO ACTION ON 
					UPDATE NO ACTION,Image varchar(255));
					
Create Table Viewers (Id int IDENTITY(1,1) PRIMARY KEY,EmployeeId int REFERENCES Employee(Id) ON DELETE NO ACTION ON UPDATE NO 
					   ACTION, AdvertisementId int REFERENCES Advertisement(Id) ON DELETE NO ACTION ON UPDATE NO ACTION,);
					   


----------------------------------------------------------Views-----------------------------------------------------------------

GO;
CREATE VIEW AdvertisementCardVW as 
Select ad.Id as AdvertisementId, ad.SellingType, ad.CategoryId, category.IconId, ad.Expiry, ad.PostedOn, emp.Location, Image, details1.Value as Title, details2.Value as Price,
                 category.Name as CategoryName, ad.Status from Advertisement as ad,Category as category, Employee as emp,AdvertisementDetails 
                as details1,AdvertisementDetails as details2 , (select * from Images where Id in (select min(Id) from Images group by AdvertisementId)) as imagelist where ad.CategoryId = category.Id
                 AND ad.EmployeeId = emp.Id AND details1.FieldName LIKE '%Title%'  And details2.FieldName LIKE '%Price%' AND ad.Status = 'Active' AND details1.AdvertisementId = details2.AdvertisementId And details1.AdvertisementId = ad.Id
                 AND details1.AdvertisementId = imagelist.AdvertisementId AND category.Status LIKE 'Active';

go;

CREATE VIEW ReportedAdvertisements as
Select ad.Id as AdvertisementId, ad.SellingType, ad.CategoryId, category.IconId, ad.Expiry, ad.PostedOn, emp.Location, Image, details1.Value as Title, details2.Value as Price,
                 category.Name as CategoryName, ad.Status from Advertisement as ad,Category as category, Employee as emp,AdvertisementDetails 
                as details1,AdvertisementDetails as details2 , Report as flag, (select * from Images where Id in (select min(Id) from Images group by AdvertisementId)) as imagelist where ad.CategoryId = category.Id
                 AND ad.EmployeeId = emp.Id AND details1.FieldName LIKE '%Title%'  And details2.FieldName LIKE '%Price%' AND flag.IsApproved = 0 AND flag.IsDeleted = 0 AND details1.AdvertisementId = details2.AdvertisementId And details1.AdvertisementId = ad.Id
                 AND details1.AdvertisementId = imagelist.AdvertisementId AND flag.AdvertisementId = ad.Id ;
GO;

--------------------------------------------------Stored Procedures---------------------------------------------------------
Go;
CREATE PROCEDURE GetActiveAds
As
Begin
UPDATE Advertisement SET Status = 'Expired' WHERE (Expiry < CURRENT_TIMESTAMP - PostedOn) and Status = 'Active';
Select * from AdvertisementCardVW ORDER BY PostedOn DESC;
End;
--Exec GetActiveAds;



GO;
CREATE PROCEDURE GetAdvertisementByEmployeeId @id int
AS
Select ad.Id as AdvertisementId, ad.SellingType, ad.CategoryId, category.IconId, ad.Expiry, ad.PostedOn, emp.Location, Image,details1.Value as Title, details2.Value as Price, category.Name as CategoryName, 
                ad.Status from Advertisement as ad,Category as category, Employee as emp,AdvertisementDetails as details1, AdvertisementDetails as details2 , (select * from Images 
				where Id in (select min(Id) from Images group by AdvertisementId)) as imagelist where emp.Id = @id AND ad.CategoryId = category.Id  AND ad.EmployeeId = emp.Id AND details1.FieldName               
                Like '%Title%' And details2.FieldName Like '%Price%' AND details1.AdvertisementId = details2.AdvertisementId And
				details1.AdvertisementId = ad.Id AND details1.AdvertisementId = imagelist.AdvertisementId ORDER BY PostedOn DESC



GO;
CREATE PROCEDURE GetAdvertisementDetails @id int,@empId int
AS
Begin
if not exists (Select Id from Viewers where AdvertisementId = @id and EmployeeId = @empId)
    Begin
            INSERT INTO Viewers VALUES (@empId, @id);
    End  
Select emp.Id as EmployeeId, emp.Name as EmployeeName, emp.ProfilePic, emp.Email, emp.Location, emp.Phone, ad.DisplayPhone, viewers.ViewCount
                from Advertisement as ad, Employee as emp, (Select count(AdvertisementId) as ViewCount from Viewers 
                where AdvertisementId = @id) as viewers where ad.Id = @id AND ad.EmployeeId = emp.Id;
End;



GO;
CREATE PROCEDURE AllAdvertisementsOfEmployee @id int
AS
Select ad.Id AdvertisementId, category.IconId, ad.Status, ad.PostedOn, ad.Expiry, imagelist.Image, details.Value Title from Advertisement ad, Category category, (select * from Images where Id in 
		(select min(Id) from Images group by AdvertisementId)) as imagelist, AdvertisementDetails details Where ad.EmployeeId = @id AND ad.Id = imagelist.AdvertisementId And ad.Id = details.
		AdvertisementId AND details.FieldName LIKE '%Title%' AND category.Id = ad.CategoryId ORDER BY AdvertisementId;



go;
CREATE PROCEDURE GetOffers @id int
AS
select employee.Id Id, employee.Name OfferedBy,employee.ProfilePic, offer.OfferedPrice, offer.Time OfferedOn from Employee employee, Offer offer Where offer.EmployeeId = employee.Id AND offer.AdvertisementId = @id;



go;
CREATE PROCEDURE GetChat @id int, @id2 int
AS
select employee.Name MessageFrom, Message, inbox.Time MessageOn from Inbox inbox, Employee employee Where inbox.AdvertisementId = @id AND 
		employee.Id = inbox.FromId AND (inbox.FromId = @id2 OR inbox.ToId = @id2);



go;
CREATE PROCEDURE RemoveReport @adId int,@message varchar(225)
AS
INSERT INTO AdminMessage VALUES (@adId,@message);
UPDATE Report SET IsApproved = 1, Time = current_timestamp Where AdvertisementId = @adId;
UPDATE Advertisement SET Status = 'Removed by Admin' Where Id = @adId;

--Exec RemoveReport @adId = 71, @message = 'Provided Ad is not suitable for our portal. Think twice before you post an AD in out Classified Portal';


GO;
CREATE PROCEDURE ReportAd @empId int, @adId int, @category varchar(10), @description varchar(225), @time DateTime
AS
Begin
if not exists (Select Id from Report where AdvertisementId = @adId and EmployeeId = @empId)
    Begin
            INSERT INTO Report VALUES (@empId, @adId, @category, @description, 0, @time, 0)
    End  
End;
--Exec ReportAd  @empId = 4, @adId = 63, @category = 'spam', @description = 'description of report', @time = '7/13/2019 5:24:44 PM';


GO;
CREATE PROCEDURE MakeOffer @empId int, @adId int, @price int, @message varchar(225), @time DateTime
AS
DECLARE @toId INT
Begin
if not exists (Select Id from Offer where AdvertisementId = @adId and EmployeeId = @empId)
    Begin
            INSERT INTO Offer VALUES (@empId, @adId, @price, @time);
			select @toId = EmployeeId from Advertisement Where Id = @adId;
			INSERT INTO Inbox VALUES (@empId, @toId, @adId, @message, @time);
    End  
End;
--Exec MakeOffer @empId = 4, @adId = 63, @price = 123, @message = 'This is message', @time = '7/13/2019 5:23:50 PM';


--GO;
--CREATE PROCEDURE AdDetails @id int
--AS
--Declare @empId INT
--Begin
--Select @empId = EmployeeId from Advertisement Where Id = @id;
--if not exists (Select Id from Viewers where AdvertisementId = @adId and EmployeeId = @empId)
--    Begin
--            INSERT INTO Viewers VALUES (@empId, @id);
--    End  
--Select  EmployeeId from Offer where AdvertisementId = @id;
--Select Image From Images Where AdvertisementId = @id;
--Select emp.Name as EmployeeName, emp.ProfilePic, emp.Email, emp.Location, emp.Phone, ad.DisplayPhone,
--                offer.OfferCount from Advertisement as ad, Employee as emp, (Select count(AdvertisementId) as OfferCount from Offer 
--                where AdvertisementId = @id) as offer where ad.Id = @id AND ad.EmployeeId = emp.Id;
--End;
--Exec AdDetails @id = 31;
