select id,productName,totalNumber,availableNumber,typeName from stock
select productProperty from stock
insert into stock values ('oyuncak','güzel oyuncak',22,12,'Adet',1)

select * from stock where productName like '%%'

select moves.id,stock.productName,moves.productNumber,userData.realName+' '+userData.realLastName as worker,moves.workDetail,moves.workDate,moves.workType from moves 
inner join stock on productID = stock.id
inner join userData on workerID = userData.id
where productName like '%%'

insert into userData values ('batu','123','Batuhan','ilter','admin',1,1,1,1,1,1)

insert into moves values (1,1,4,'deneme',1,0,-1,GETDATE())

select * from userData
select * from stock
select * from moves