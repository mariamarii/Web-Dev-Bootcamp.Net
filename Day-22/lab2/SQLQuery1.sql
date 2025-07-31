Select * from Employee
---------------------
select fname,lname,salary,dno from Employee
--------------------------
select Pname,plocation,dnum from project
-------------------------------
select fname+' '+lname as fullname,salary*0.1 as [annual commission] from Employee
-------------------
select ssn,fname,salary from Employee
where salary > 1000
---------------------
select ssn,fname,salary*12 as [annual salary] from Employee
where  salary*12 > 10000
------------------------------
select fname,salary from Employee
where sex='F'

--------------------
select dnum,dname from Departments
where mgrssn=968574
-------------------
select pname,Pnumber,plocation from Project
where dnum=10