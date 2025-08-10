SELECT D.Dependent_name,D.SEX FROM Dependent AS D JOIN  Employee AS E ON  D.ESSN = E.SSN
WHERE E.Sex ='F' AND D.Sex ='F'

UNION
SELECT D.Dependent_name,D.SEX FROM Dependent AS D JOIN  Employee AS E ON  D.ESSN = E.SSN
WHERE E.Sex ='M' AND D.Sex ='M'
----------------------------------------------------

SELECT PNAME,SUM(HOURS) AS [TOTAL HOURS] FROM Project JOIN Works_for ON Pno=Pnumber
GROUP BY Pname
----------------------------------------------------
SELECT Dname  FROM Departments JOIN Employee ON Dno=Dnum
WHERE SSN  = ( SELECT MIN(SSN) FROM Employee)

------------------------------------------
SELECT Dname ,MAX(SALARY) AS MAX,MIN(SALARY) AS MIN,AVG(SALARY)  AS AVG FROM Departments JOIN Employee ON Dno=Dnum
GROUP BY Dname

------------------------------------
SELECT LNAME FROM Employee JOIN Departments ON SSN = MGRSSN
WHERE SSN NOT IN ( SELECT DISTINCT ESSN FROM Dependent)

--------------------------------------
SELECT DNO, Dname,COUNT(SSN) AS COUNT  FROM Departments JOIN Employee ON Dno=Dnum
GROUP BY DNO, Dname
HAVING AVG(Salary) <= (SELECT AVG(SALARY) FROM Employee)

----------------------------------
SELECT Dname ,Lname ,Fname ,P.Pname FROM Employee E
JOIN Departments D ON E.Dno = D.Dnum
JOIN Works_for W ON E.SSN = W.Essn
JOIN Project P ON W.Pno = P.Pnumber
ORDER BY D.Dname, E.Lname, E.Fname;
----------------------------------
SELECT *
FROM (
    SELECT *, RANK() OVER (ORDER BY Salary DESC) AS SalaryRank
    FROM Employee
) AS RankedSalaries
WHERE SalaryRank <= 2;

-------------------------------------------
SELECT DISTINCT E.Fname + ' ' + E.Lname AS FullName , D.Dependent_name
FROM Employee E
JOIN Dependent D ON D.Dependent_name LIKE E.Fname + ' ' + E.Lname + '%';

------------------------------------------
UPDATE Employee 
SET Salary= Salary*1.3
FROM Employee 
JOIN Works_for ON SSN=ESSn
JOIN Project ON Pnumber =Pno
WHERE Pname = 'Al Rabwah'

------------------------------
SELECT SSN, Fname + ' ' + Lname AS [FULL NAME]
FROM Employee 
WHERE EXISTS (
    SELECT 1 FROM Dependent 
    WHERE Essn = SSN
);


-----------------------------
select * from Departments
insert into Departments (Dname,Dnum,MGRSSN,[MGRStart Date])
values('DEPT ID',100,112233,1-11-2006)
-------------------------------------
begin transaction;
update Departments
set MGRSSN=968574
where Dnum = 100

update Departments
set MGRSSN=102672
where Dnum = 20

update Employee
set Superssn=102672
where ssn=102660
commit;

------------------------
begin transaction

update Employee
set Superssn=102672
where Superssn=223344

update Departments
set MGRSSN=102672 ,[MGRStart Date]=getdate()
where MGRSSN=223344

update Dependent
set ESSN=102672
where ESSN=223344

update Works_for
set ESSn =102672
where ESSn =223344

delete from Employee 
where SSN=223344

commit;

-----------------