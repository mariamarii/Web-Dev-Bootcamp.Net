select Dnum , fname,ssn from Departments join Employee on mgrssn=ssn
------------------
select d.Dname,p.Pname from Departments as d join Project as p on d.Dnum=p.Dnum
----------------------
select d.* , e.fname from Dependent as d join Employee as e on d.essn=e.ssn
--------------------------------------
select pname,pnumber,plocation from Project
where Plocation like '%Cairo%' or plocation like '%Alex%'
----------------------------------
select * from Project
where pname like 'a%'
-----------------------
select * from  Employee
where dno=30 and Salary between 1000 and 3000
------------------------------
select fname,Dno,Pname,Hours from Employee e join Works_for as w on e.ssn=w.ESSn join Project as p on p.Pnumber=w.Pno
where dno=10 and Hours>=10  and pname='Al Rabwah'

---------------------------------
select e1.*  , e2.fname+' '+e2.Lname as supervisor from Employee as e1 join Employee as e2 on e1.Superssn=e2.ssn
where e2.fname+' '+e2.Lname ='Kamel Mohamed'
---------------------------------------------
select fname,Pname from Employee e join Works_for as w on e.ssn=w.ESSn join Project as p on p.Pnumber=w.Pno
order by Pname
----------------------------
select p.Pnumber,d.Dname, e.lname ,e.Address,e.Bdate from project as p join departments as d on d.Dnum=p.Dnum  join Employee as e on d.MGRSSN=e.SSN
where p.Plocation like '%Cairo%'
-------------------------------

select e.* from Employee as e join Departments as d on d.MGRSSN=e.SSN
-----------------------------------------
select e.*,d.Dependent_name from Dependent as d right join Employee as e on d.essn=e.ssn
-----------------------------------------
insert into Employee values('mariam','mohamed',102672,'2003-10-2','Al Fayoum','F',3000,112233,30)
select * from Employee where fname='mariam'
-------------------------------
insert into Employee (Fname,Lname,SSN,Bdate,Address,Sex,Dno) values('sohila','osama',102660,'2003-10-23','Al Fayoum','F',30)
select * from Employee where fname='sohila'
-------------------------------------
update  employee
set salary = salary*1.2
where ssn=102672
select * from Employee where ssn=102672