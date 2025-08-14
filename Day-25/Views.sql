create view vw_PassedStudents as 
select St_Fname + ' ' + St_Lname as [Full name], Crs_Name from Student as s join Stud_Course as c on s.St_Id = c.St_Id  and Grade >50 join Course as cr on  c.Crs_Id =cr.Crs_Id
------------------------------
create view vw_ManagerTopics with encryption 
as
select Ins_Name,Top_Name from course as c join Topic as t on c.Top_Id =t.Top_Id join Ins_Course as i on i.Crs_Id =c.Crs_Id join Instructor as i2 on i.Ins_Id =i2.Ins_Id
---------------------
create view vw_InstructorDepartment 
as
select Ins_Name,Dept_Name from Instructor as i join Department as d on d.Dept_Id =i.Dept_Id and Dept_Name  in ('SD','Java')

------------------------
create view  vw_V1  
as
select * from student 
where St_Address in ('Alex','Cairo')
with check option

Update vw_V1 set st_address='tanta'
Where st_address='alex'

------------------
use Company_SD
create view vw_EmployeeProject
as
select p.Pname,count(e.SSN) as [working employees] from Project as p join Works_for as w on p.Pnumber=w.Pno join Employee as e on w.ESSn = e.SSN
group by p.Pname

------------------
create clustered index idx_hiredate
on Departments([MGRStart Date])

----Cannot create more than one clustered index on table 'Departments'. Drop the existing clustered index 'PK_Departments' before creating another.
----
-----------------
create unique index idx_UniqueAge on
student(St_Age)


------ The CREATE UNIQUE INDEX statement terminated because a duplicate key was found for the object name 'dbo.Student' and the index name 'idx_UniqueAge'. The duplicate key value is (21).
----


merge into usertransactions as target
using usertransactions_new as source 
on target.userId = source.userId

when matched then
update set  target.transactionamount = source.transactionamount

when not matched by target then 
insert (UserID, TransactionAmount)
    values (source.UserID, source.TransactionAmount)

when not matched by source then
    delete;
-------------------------


---------------PART 2-------------------

create view v_clerk 
as
select e.EmpNo, w.ProjectNo,w.Enter_Date from HR.employee as e join Works_on as w on e.EmpNo= w.EmpNo  and w.Job ='Clerk'

-----------------------
create view V_without_budget as
select ProjectNo, ProjectName from HR.Project

-------------------
create view v_count as
select projectName,count(job) as [#jobs] from Works_on w  join Hr.project as p on w.projectNo =p.ProjectNo
group by P.projectName 
---------------------

create view v_project_p2
as
select empNo,projectNo from v_clerk
where projectNo =2

-----------------
alter view v_without_budget as
SELECT *
FROM HR.project
WHERE projectno <=2
-------------------
drop view v_clerk,v_count
---------------------------
create view v_emp_d2
as
select empNo,empLname from Hr.employee
where deptno=2

-----------------------
select * from v_emp_d2
where empLname like '%J%'
-----------------
create view v_dept
as
select deptNo,deptname from Company.department

--------------
insert into v_dept values(4,'Development')
select * from v_dept
---------------------

create view v_2006_check
as
select empno,projectno,enter_date from works_on
	where enter_date between '2006-01-01' and '2006-12-31'
with check option;

