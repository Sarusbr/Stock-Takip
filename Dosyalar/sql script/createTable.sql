create table userData(
id int identity primary key,
systemName nvarchar(20) unique not null,
systemPass nvarchar(20)not null,
realName nvarchar(20) null,
realLastName nvarchar(20) null,
title nvarchar(20) null,
admins bit not null,
stockDisplay bit not null,
stockChange bit not null,
moveDisplay bit not null,
moveChange bit not null,
notificationEdit bit not null,
)

create table stock(
id int identity primary key,
productName nvarchar(50) unique not null,
productProperty nvarchar(250) null,
totalNumber int not null,
availableNumber int not null,
typeName nvarchar(20) not null,
category tinyint
)

create table moves(
id int identity primary key,
productID int foreign key references stock(id) not null,
productNumber int not null,
workerID int foreign key references userData(id) not null,
workDetail nvarchar(250) null,
workType tinyint,
moveStatus bit not null,
connectedWorkID int,
workDate datetimeoffset not null
)

create table notifications(
id int identity primary key,
notificationType tinyint not null,
userID int foreign key references userData(id) not null,
productID int foreign key references stock(id) not null,
kindID tinyint not null,
kindValue nchar(30) not null
)