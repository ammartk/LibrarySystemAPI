create database  mvc
create table books(
bookname varchar(55),
bookid int Not null,
category  varchar(55),
shelf int ,
availibilty int
);
create table Users(
username varchar(55),
userid int Not null,
--issuelist varchar(55)
);
drop table issuedlist
create table issuedlist(
id int IDENTITY(1,1) NOT NULL,
userid int not null,
bookname varchar not null,
issuedate date,
returndate date
);
go;
drop procedure issue;
create procedure issue
(
@userid int,
@bookname varchar(255),
@issuedate date,
@returndate date
)
as
begin
insert into issuedlist(userid,bookname,issuedate,returndate) 
values(@userid,@bookname,@issuedate,@returndate)
end
exec issue 1,''

create procedure bookk
As
select * from Users
select * from books
Go;
exec bookk
---q1
CREATE PROCEDURE insertbook
(
@bookname varchar(255),
@bookid int,
@category varchar(255),
@shelf int,
@avail  int
)
As
BEGIN
    INSERT INTO books(bookname, bookid, category, shelf,availibilty)
       VALUES(@bookname, @bookid, @category, @shelf,@avail)   
END
exec insertbook 'book1','1','darama','10','-1'
exec insertbook 'book2','2','fiction','11','-1'
exec insertbook 'book3','3','noval','12','-1'
exec insertbook 'book4','4','darama','14','-1'
exec insertbook 'book5','5','comedy','15','-1'
---q2
--q2
create table Users(
username varchar(55),
userid int Not null,
issuelist varchar(55)
);
drop procedure listofusers
CREATE PROCEDURE listofusers
(
@username varchar(255),
@userid int,
@issuelist  varchar(255)
)
As
BEGIN
    INSERT INTO Users(username, userid,issuelist)
       VALUES(@username, @userid,@issuelist) 
END
create procedure allbook
as
begin
select *from books
end


exec listofusers 'anni','1'
exec listofusers 'dania','3'
exec listofusers 'dania','3'
exec listofusers 'kiran','4'

drop table books
drop table Users

--q3
drop procedure fetchbook

create procedure fetchbook(
@bookname varchar (55)
)
As 
Begin

select * from books where bookname=@bookname
End
exec fetchbook 'book2'

--q4
drop procedure fetchuser
create procedure fetchusername(@userid varchar(55))
AS
begin
select * from Users where userid=@userid
end
drop procedure Fetchbooks
create procedure Fetchbooks(
@username varchar (255)
)
As 
Begin
select Distinct books.bookname,books.availibilty,books.category,books.shelf from Users join issuedlist
on Users.userid=issuedlist.userid
join books on issuedlist.bookname=books.bookname
where username=@username
End
exec fetchuserbyname 'rocky'

--q5
create procedure updatebook(
@bookname varchar(255),
@bookid int,
@category varchar(255),
@shelf int,
@availibilty  int
)
AS
BEGIN 
update books  set 
bookname=@bookname,
bookid=@bookid,
category=@category,
shelf=@shelf,
availibilty=@availibilty
where bookid=@bookid
end
exec updatebook 'updatebook3','3','fiction','16','-1'

---q6
drop procedure updateuser
create procedure updateuser(
@username varchar(255),
@userid int,
@issuelist varchar(255)
)
AS
BEGIN 
update Users  set 
username=@username,
userid=@userid,
issuelist=@issuelist
where userid=@userid
end
exec updateuser'rahil','3','book21'
select *from Users
---q7
create procedure issuebookforuser(@username int,@bookname varchar(255),
@issuedate date,@returndate date)
as
begin
	insert into issuedlist values(@username,@bookname,@issuedate,@returndate)
end

--q8

drop procedure deletebook
create procedure deletebook(@bookname varchar(255))
AS
BEGIN
declare @avail int ;
select @avail= availibilty from books where bookname=@bookname;
select @avail;
IF @avail=-1
   delete from books where bookname=@bookname 
   else
   delete from issuedlist where bookname=@bookname
   delete from books where bookname=@bookname 
END
exec deletebook 'book4'
--q9
create procedure deleteusers(@userid int)
as
begin
delete from Users where userid=@userid
end
--9 alternatequery
create procedure deleteuser(@userid int)
AS
BEGIN
declare @val varchar(55);
IF EXISTS( select * from issuedlist where userid=@userid)
    delete from Users  where  userid=@userid
    delete  from issuedlist where userid=@userid

 IF NOT EXISTS( select * from issuedlist where userid=@userid)
    delete from Users  where  userid=@userid
END
exec deleteuser @userid= '1'


---q10
create procedure returnbook(@bookname varchar(255))
AS
BEGIN
update  books set availibilty=-1 
where bookname=@bookname
delete  from issuedlist where bookname=@bookname
END


