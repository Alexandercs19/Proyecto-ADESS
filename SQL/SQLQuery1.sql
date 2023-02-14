create database ADESS

 
create table excluidos 
(
id int not null identity (1,1) primary key,
cedula varchar(15),
motivo varchar (144),
fecha varchar(10)
);

create table add_list
(
id_add int not null identity (1,1) primary key,
cedula_add varchar(15),
apellido varchar(20),
nombre varchar(20),
sub varchar(10),
monto varchar(10),
fecha_add varchar(10)
);

alter table add_list drop column id_add 

alter table add_list add id_add int not null identity (1,1) primary key
select * from add_list  
