create proc sp_GetStudentsPerDept
as
begin
 select d.Dept_name, count(s.St_id) as student# from Department as d join  Student as s on s.Dept_Id =d.Dept_Id
 group by Dept_Name
end

exec  sp_GetStudentsPerDept

--------------------

create proc sp_ChechEmployeesNumber
as
begin
	declare @emplyees_count int
  select @emplyees_count = count(e.SSN)from Employee as e join Works_for as w on e.SSN =w.ESSn 
  where w.Pno =100

  if @emplyees_count >= 3
  select 'The number of employees in the project p1 is 3 or more'
  else 
  begin
  select  'The following employees work for the project p1'
  select Fname +' ' + Lname, Pno from Employee as e
  join Works_for  as w on e.SSN =w.ESSn
  where w.Pno =100
  end
end

------------------------

create procedure sp_replace_employee
    @old_emp int,
    @new_emp int,
    @pno int
as
begin
    
    if exists (
        select 1
        from works_for
        where essn = @old_emp
          and pno = @pno
    )
    begin
        
        if exists (
            select 1
            from employee
            where ssn = @new_emp
        )
        begin
            
            delete from works_for
            where essn = @old_emp
              and pno = @pno;

           
            insert into works_for (essn, pno)
            values (@new_emp, @pno);

            select message = concat(
                'employee ', @old_emp, 
                ' has been replaced by employee ', @new_emp, 
                ' in project ', @pno
            );
        end
        else
        begin
            select message = concat(
                'cannot insert new employee ', @new_emp, 
                ' because they do not exist in the employee table'
            );
        end
    end
    else
    begin
        select message = concat(
            'old employee ', @old_emp, 
            ' does not exist in project ', @pno
        );
    end
end



exec sp_replace_employee @old_emp = 102672, @new_emp = 105, @pno = 100;

select * from Works_for

----------------------
alter table project
add budget decimal(18,2);

update project
set budget = 10000;

select * from project

create table project_budget_audit
(
    projectno varchar(10),
    username varchar(50),
    modifieddate datetime,
    budget_old decimal(18,2),
    budget_new decimal(18,2)
);


create trigger trg_audit_budget_update
on project
after update
as
begin
    if update(budget)
    begin
        insert into project_budget_audit (projectno, username, modifieddate, budget_old, budget_new)
        select 
            d.pnumber,
            suser_sname(),        
            getdate(),            
            d.budget,            
            i.budget              
        from deleted d
        join inserted i on d.pnumber = i.pnumber;
    end
end;


update project
set budget = 200000
where Pnumber = '200';

select * from project_budget_audit;

--------------------
create trigger trg_prevent_department_insert
on department
instead of insert
as
begin
    print 'you cannot insert a new record into the department table';
    rollback;
end


select * from Department

insert into Department values(100,'mariam','marioum','new jersy',null,null)

----------------------------

---there's no hiredate--
create trigger trg_prevent_employee_march_insert
on employee
instead of insert
as
begin
    -- check if any inserted rows have hiredate in march
    if exists (
        select 1
        from inserted
        where month(hiredate) = 3
    )
    begin
        print 'you cannot insert employee records in march';
        rollback;
    end
    else
    begin
        insert into employee (ssn, fname, lname, hiredate, Bdate,dno,Salary,Address,sex,Superssn)
        select ssn, fname, lname,  hiredate, Bdate,dno,Salary,Address,sex,Superssn
        from inserted;
    end
end

----------------------------
create table student_audit
(
    server_username varchar(50),
    audit_date datetime,
    note varchar(200)
);

create trigger trg_student_after_insert
on student
after insert
as
begin
    

    insert into student_audit (server_username, audit_date, note)
    select 
        suser_sname(),
        getdate(),
        concat(suser_sname(), ' inserted new row with key=', i.st_id, ' in table student')
    from inserted i;
end

------------------------
create trigger trg_student_instead_of_delete
on student
instead of delete
as
begin
    set nocount on;

    insert into student_audit (server_username, audit_date, note)
    select 
        suser_sname(),
        getdate(),
        concat('try to delete row with key=', d.st_id)
    from deleted d;

    rollback;
end

