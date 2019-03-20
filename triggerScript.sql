use ProDb
go

create trigger order_history on payment after insert
As
Begin
Declare 
@findId int,
@find int,
@email nvarchar(128),
@itemId int,
@count int,
@unitprice decimal(18, 2)
select @findId = payId,@find = OrderId, @email = email  from inserted;
while(select count(*) from OrderDetail) >0
begin
select top(1) @itemId = itemId, @unitprice = UnitPrice, @count = Quantity from OrderDetail;
insert into OrderHistory(orderid,payId,itemid,email,unitprice,ItemQuantity) values(@find,@findId,@itemId,@email,@unitprice,@count); 
delete from orderdetail where itemId = @itemId;
end
End
Go


create trigger InitialpurchaseHistory on item after insert
as
begin
declare 
@id int
select @id = id from inserted;
insert into purchaseItemHistory(itemid,purchasecount) values(@id,0); 
end
go

create trigger InitialRateHistory on item after insert
as
begin
declare 
@id int
select @id = id from inserted;
insert into rateitem(itemid,rate) values(@id,0); 
end
go

create trigger updatePurchaseHistory on OrderHistory after insert
as
begin
declare 
@id int,
@count int
select @id = itemid, @count = itemQuantity from inserted;
update purchaseItemHistory set purchasecount = purchasecount + @count where itemId = @id;
end
go

create trigger updatRateHistory on purchaseItemHistory after update
as 
begin
declare
@totalpurchasecount int
select @totalpurchasecount = ISNULL(SUM(purchasecount),1) from purchaseItemhistory;
update rateitem set rate = ((p.purchasecount * 100)/@totalpurchasecount) from rateitem join purchaseitemHistory as p on rateitem.itemid = p.itemid;
end
go

