create function f_GetMonthName(@date Date)
returns varchar(10)
as
begin
  declare @MonthName varchar(10)
  set @MonthName = DATENAME(Month,@date)
  return @MonthName
end

SELECT dbo.f_GetMonthName('2025-08-14') AS MonthName;
-------------------------
create function f_GetValuesBetween( @first int,@second  int)
returns @numbers table(number int)
as
begin
    declare @current int;

    if @first > @second
    begin
        declare @temp int = @first;
        set @first= @second;
        set @second = @temp;
    end

    set @current = @first;

    while @current <= @second
    begin
        insert into @numbers (number)
        values (@current);

        set @current += 1;
    end

    return;
end;


select * from dbo.f_GetValuesBetween(5, 10);

------------------------------------


create function fn_GetSttudentDepartment (@StudentNo int)
returns table
as
return
(
	select d.Dept_Name, s.St_Fname + ' ' + s.St_Lname as [Full Name]   from Student as s join Department as d on s.Dept_Id = d.Dept_Id
	where s.St_Id =@studentNo
)

SELECT * 
FROM fn_GetSttudentDepartment(1);


------------------------------
create function fn_CheckName (@StudentNo int)
returns varchar(100)
as
begin
	declare @firstname varchar(50)
	declare @lastname varchar(50)
	declare @message varchar(50)
	select @firstname=St_Fname,
	  @lastname=St_Lname
	from Student
	where St_Id =@studentNo

	if @firstname is null and @lastname is null
	set @message = 'first name & last name are null'
	else if @firstname is null
	set @message = 'first name is null'
	else if @lastname is null
	set @message = 'lastname is null'
	else
	set @message = 'first name & last name are not null'

    return @message
	

end

select dbo.fn_CheckName(1)

-------------------------------

create function fn_getManagerDept (@mgrId int)
returns table
as
return (
	select d.Dept_Manager,d.Dept_Name,i.Ins_Name,d.Manager_hiredate from Department
	as d join Instructor  as i on d.Dept_Manager = i.Ins_Id

)
----------------------------------
create function  fn_GetName (@name varchar(20))
returns @result table (
 nameMapped varchar(100)
 )

 as
 begin
	if @name ='first name'
	begin 
	insert into @result
	select ISNULL(St_Fname, 'no first name')
        from student
	end

	else if @name ='last name'
	begin 
	insert into @result
	select ISNULL(St_lname, 'no last name')
        from student
	end

	if @name ='full name'
	begin 
	insert into @result
	select ISNULL(St_Fname, '  ') + ' ' + ISNULL(st_lname,' ')
        from student
	end

	return
 end



 --------------------

 select 
    st_id,
    left(St_Fname, len(St_Fname) - 1) as first_name_without_last_char
from student

---------------------
update sc
set grade = null
from Stud_Course as sc join Student as s on sc.St_Id =s.St_Id join Department as d on s.Dept_Id =d.Dept_Id
where d.Dept_name ='SD'







-------------------