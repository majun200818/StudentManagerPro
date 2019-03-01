--ָ��ǰҪʹ�õ����ݿ�
use master
go
--�жϵ�ǰ���ݿ��Ƿ����
if exists (select * from sysdatabases where name='SMDB')
drop database SMDB --ɾ�����ݿ�
go
--�������ݿ�
create database SMDB
on primary
(
	--���ݿ��ļ����߼���
    name='SMDB_data',
    --���ݿ������ļ���������·����
    filename='D:\DB\SMDB_data.mdf',
    --���ݿ��ļ���ʼ��С
    size=10MB,
    --�����ļ�������
    filegrowth=1MB
)
--������־�ļ�
log on
(
    name='SMDB_log',
    filename='D:\DB\SMDB_log.ldf',
    size=2MB,
    filegrowth=1MB
)
go
--����ѧԱ��Ϣ���ݱ�
use SMDB
go
if exists (select * from sysobjects where name='Students')
drop table Students
go
create table Students
(
    StudentId int identity(100000,1) ,
    StudentName varchar(20) not null,
    Gender char(2)  not null,
    Birthday datetime  not null,
    StudentIdNo numeric(18,0) not null,--����֤��
    CardNo  varchar(20) not null,--���ڿ���
    StuImage text null,--ѧԱ��Ƭ
    Age int not null,
    PhoneNumber varchar(50),
    StudentAddress varchar(500),
    ClassId int not null  --�༶���
)
go
--�����༶��
if exists(select * from sysobjects where name='StudentClass')
drop table StudentClass
go
create table StudentClass
(
	ClassId int primary key,
    ClassName varchar(20) not null
)
go
--�����ɼ���
if exists(select * from sysobjects where name='ScoreList')
drop table ScoreList
go
create table ScoreList
(
    Id int identity(1,1) primary key,
    StudentId int not null,
    CSharp int null,
    SQLServerDB int null,
    UpdateTime datetime not null
)
go
--��������Ա�û���
if exists(select * from sysobjects where name='Admins')
drop table Admins
create table Admins
(
	LoginId int identity(1000,1) primary key,
    LoginPwd varchar(20) not null,
    AdminName varchar(20) not null
)
go
--�������ݱ��ĸ���Լ��
use SMDB
go
--������������Լ��primary key
if exists(select * from sysobjects where name='pk_StudentId')
alter table Students drop constraint pk_StudentId

alter table Students
add constraint pk_StudentId primary key (StudentId)

--�������Լ��check
if exists(select * from sysobjects where name='ck_Age')
alter table Students drop constraint ck_Age
alter table Students
add constraint ck_Age check (Age between 18 and 35) 

--����ΨһԼ��unique
if exists(select * from sysobjects where name='uq_StudentIdNo')
alter table Students drop constraint uq_StudentIdNo
alter table Students
add constraint uq_StudentIdNo unique (StudentIdNo)

if exists(select * from sysobjects where name='uq_CardNo')
alter table Students drop constraint uq_CardNo
alter table Students
add constraint uq_CardNo unique (CardNo)

--��������֤�ĳ��ȼ��Լ��
if exists(select * from sysobjects where name='ck_StudentIdNo')
alter table Students drop constraint ck_StudentIdNo
alter table Students
add constraint ck_StudentIdNo check (len(StudentIdNo)=18)

--����Ĭ��Լ�� 
if exists(select * from sysobjects where name='df_StudentAddress')
alter table Students drop constraint df_StudentAddress
alter table Students 
add constraint df_StudentAddress default ('��ַ����' ) for StudentAddress

if exists(select * from sysobjects where name='df_UpdateTime')
alter table ScoreList drop constraint df_UpdateTime
alter table ScoreList 
add constraint df_UpdateTime default (getdate() ) for UpdateTime

--�������Լ��
if exists(select * from sysobjects where name='fk_classId')
alter table Students drop constraint fk_classId
alter table Students
add constraint fk_classId foreign key (ClassId) references StudentClass(ClassId)

if exists(select * from sysobjects where name='fk_StudentId')
alter table ScoreList drop constraint fk_StudentId
alter table ScoreList
add constraint fk_StudentId foreign key(StudentId) references Students(StudentId)


-------------------------------------------��������--------------------------------------
use SMDB
go

--����༶����
insert into StudentClass(ClassId,ClassName) values(1,'����1��')
insert into StudentClass(ClassId,ClassName) values(2,'����2��')
insert into StudentClass(ClassId,ClassName) values(3,'�����1��')
insert into StudentClass(ClassId,ClassName) values(4,'�����2��')
insert into StudentClass(ClassId,ClassName) values(5,'����1��')
insert into StudentClass(ClassId,ClassName) values(6,'����2��')

--����ѧԱ��Ϣ
insert into Students (StudentName,Gender,Birthday,Age,StudentIdNo,CardNo,PhoneNumber,StudentAddress,ClassId)
         values('��С��','��','1989-08-07',22,120223198908071111,'0004018766','022-22222222','������Ͽ�����|��Ԣ5-5-102',1)
insert into Students (StudentName,Gender,Birthday,Age,StudentIdNo,CardNo,PhoneNumber,StudentAddress,ClassId)
         values('��С��','Ů','1989-05-06',22,120223198905062426,'0006394426','022-33333333','����кӱ���������58��',2)
insert into Students (StudentName,Gender,Birthday,Age,StudentIdNo,CardNo,PhoneNumber,StudentAddress,ClassId)
         values('��С��','��','1990-02-07',21,120223199002078915,'0006073516','022-44444444','����к��������ֹ����·79��',4)
insert into Students (StudentName,Gender,Birthday,Age,StudentIdNo,CardNo,PhoneNumber,StudentAddress,ClassId)
         values('��Сǿ','Ů','1987-05-12',24,130223198705125167,'0006254540','022-55555555',default,2)
insert into Students (StudentName,Gender,Birthday,Age,StudentIdNo,CardNo,PhoneNumber,StudentAddress,ClassId)
         values('��С��','Ů','1986-05-08',25,130223198605081528,'0006403803','022-66666666','�ӱ���ˮ·����69��',1)
insert into Students (StudentName,Gender,Birthday,Age,StudentIdNo,CardNo,PhoneNumber,StudentAddress,ClassId)
         values('������','��','1987-07-18',24,130223198707182235,'0006404372','022-77777777',default,1)
insert into Students (StudentName,Gender,Birthday,Age,StudentIdNo,CardNo,PhoneNumber,StudentAddress,ClassId)
         values('������','��','1988-09-28',24,130223198909282235,'0006092947','022-88888888','�ӱ������з绪��12��',3)
insert into Students (StudentName,Gender,Birthday,Age,StudentIdNo,CardNo,PhoneNumber,StudentAddress,ClassId)
         values('����','��','1987-01-18',24,130223198701182257,'0006294564','022-99999999','�ӱ���̨���Ҹ�·5��',1)
insert into Students (StudentName,Gender,Birthday,Age,StudentIdNo,CardNo,PhoneNumber,StudentAddress,ClassId)
         values('������','Ů','1987-06-15',24,130223198706152211,'0006092450','022-11111111',default,3)
insert into Students (StudentName,Gender,Birthday,Age,StudentIdNo,CardNo,PhoneNumber,StudentAddress,ClassId)
         values('������','Ů','1989-08-19',24,130223198908192235,'0006069457','022-11111222',default,4)
         
         
         
--����ɼ���Ϣ
insert into ScoreList (StudentId,CSharp,SQLServerDB)values(100000,60,78)
insert into ScoreList (StudentId,CSharp,SQLServerDB)values(100001,55,88)
insert into ScoreList (StudentId,CSharp,SQLServerDB)values(100002,90,58)
insert into ScoreList (StudentId,CSharp,SQLServerDB)values(100003,88,75)

insert into ScoreList (StudentId,CSharp,SQLServerDB)values(100004,62,88)
insert into ScoreList (StudentId,CSharp,SQLServerDB)values(100006,52,80)
insert into ScoreList (StudentId,CSharp,SQLServerDB)values(100007,91,66)
insert into ScoreList (StudentId,CSharp,SQLServerDB)values(100009,78,35)

--�������Ա��Ϣ
insert into Admins (LoginPwd,AdminName) values(123456,'������')
insert into Admins (LoginPwd,AdminName) values(123456,'������')

--ɾ��ѧԱ��Ϣ
--delete from Students 

--truncate table Students --ɾ��ȫ�������Ժ��Զ���ʶ�����±��

--��ʾѧԱ��Ϣ�Ͱ༶��Ϣ
select * from Students
select * from StudentClass
select * from ScoreList
select * from Admins
select * from Attendance



