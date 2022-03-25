create procedure userControl @systemName nvarchar(20),@systemPass nvarchar(20),@id int output as
begin 
set @id = (select id from userData where systemName = @systemName and systemPass = @systemPass)
if(@id is null) set @id = -1
end

create procedure productControl @systemName nvarchar(20),@id int output as
begin 
set @id = (select id from userData where systemName = @systemName and systemPass = @systemPass)
if(@id is null) set @id = -1
end


