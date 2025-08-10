select count(st_id) from  Student 
where st_age is not null
-------------------------
select distinct  ins_name from Instructor
------------------------
select St_Id as [Student Id] , St_Fname + ' ' +St_Lname as[Student Full Name]  , isNull(d.Dept_Name,'No Department') as [Department name] from Student as s left join Department as d on s.Dept_Id =d.Dept_Id

-------------------------------

select Ins_Name , i.Dept_Id from Instructor as i left join Department as d on d.Dept_Id=i.Dept_Id
------------------------
select St_Fname + ' ' +St_Lname as[Student Full Name],
	   Crs_Name 
	   from Stud_Course as c
	   join Student  as s 
	   on s.St_Id =c.St_Id and  c.Grade is not null
	   join course c2 on c.Crs_Id =c2.Crs_Id
------------------------------------
select count(Crs_Name) as Count,t.Top_Name from Course as c join  Topic as t on c.Top_Id =t.Top_Id
group by c.Top_Id, t.Top_Name

--------------------------

select min(salary) as min ,max(salary) as max from Instructor

---------------------
select Ins_Name,Salary from Instructor
where Salary > (select AVG(salary) from Instructor)
-----------------------
SELECT DISTINCT d.Dept_Name FROM Department d
JOIN Instructor i ON d.Dept_Id = i.Dept_Id
WHERE i.Salary = (SELECT MIN(Salary) FROM Instructor);

----------------------------
select  * from (
select * , RANK () over(order by salary) as rank_col from Instructor) as i
where i.rank_col <=2

-------------------
SELECT 
    Ins_Name,
    COALESCE(Salary, 300) AS Payment
FROM Instructor;
---------------
SELECT AVG(Salary) AS AverageSalary FROM Instructor;

--------------------------
SELECT s.St_Fname,i.Ins_Name ,i.Salary ,i.Dept_Id FROM Student s
JOIN Instructor i ON s.St_super= i.Ins_Id;

----------------------------
SELECT Dept_Id, Ins_Name, Salary FROM (
    SELECT Dept_Id,Ins_Name,Salary, RANK() OVER (PARTITION BY Dept_ID ORDER BY Salary DESC) AS SalaryRank
    FROM Instructor
    WHERE Salary IS NOT NULL
) AS RankedSalaries
WHERE SalaryRank <= 2;
----------------------------
SELECT Dept_Id,St_Id,St_Fname, St_Lname FROM (
    SELECT 
        Dept_Id,St_Id,St_Fname, St_Lname,RANK() OVER (PARTITION BY Dept_ID ORDER BY NEWID()) AS rn
    FROM Student
) AS RandomStudents
WHERE rn = 1;
-------------------
select SalesOrderID,ShipDate from SalesLT.SalesOrderHeader
where OrderDate >= '2002-7-28' and  OrderDate <= '2014-7-29'

--------------------------

select ProductID,Name from SalesLT.Product
where StandardCost <110.00

----------------------------

select ProductID,Name from SalesLT.Product
where Weight  is null
---------------------------------
select * from SalesLT.Product
where color  in ('Red','Black','Silver')

-------------
UPDATE SalesLT.ProductDescription
SET Description = 'Chromoly steel_High of defects'
WHERE ProductDescriptionID = 3

select * from SalesLT.ProductDescription 
where Description like '%\_%' escape '\'
-----------------------------------
select sum(TotalDue) from SalesLT.SalesOrderHeader
where OrderDate >= '2001-7-1' and  OrderDate <= '2014-7-31'

---------------------------------

select AVG(distinct ListPrice) from SalesLT.Product
-----------------------
SELECT  'The ' + Name + ' is only! ' + CAST(ListPrice AS VARCHAR) AS ProductInf FROM SalesLT.Product
WHERE ListPrice BETWEEN 100 AND 120
ORDER BY ListPrice;

------------------------------
SELECT CONVERT(VARCHAR, GETDATE(), 101) AS TodayDate  
UNION
SELECT CONVERT(VARCHAR, GETDATE(), 103)              
UNION
SELECT CONVERT(VARCHAR, GETDATE(), 120)               
UNION
SELECT CONVERT(VARCHAR, GETDATE(), 113);               



