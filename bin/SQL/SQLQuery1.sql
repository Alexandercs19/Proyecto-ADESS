/*create database ADESS

 
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

Select * From add_list

//PROCEDURE PARA LA TABLA add_list///////

CREATE PROCEDURE sp_Listar
as
begin 
	select * from add_list
end 

CREATE PROCEDURE sp_Obtener(
@id_add int 
)
as 
begin 
	select * from add_list where id_add = @id_add 
end

create procedure sp_Guardar(
@cedula_add varchar(11),
@apellido varchar(50),
@nombre varchar(50),
@sub varchar(4),
@monto varchar(9),
@fecha_add varchar(6)
)
as
begin 
	insert into add_list(cedula_add,apellido,nombre,sub,monto,fecha_add) values (@cedula_add, @apellido, @nombre, @sub, @monto, @fecha_add)
end

create procedure sp_Editar(
@cedula_add varchar(11),
@apellido varchar(50),
@nombre varchar(50),
@sub varchar(4),
@monto varchar(9),
@fecha_add varchar(6),
@id_add int
)
as
begin 
	update add_list set cedula_add = @cedula_add, apellido = @apellido, nombre = @nombre, sub = @sub, monto = @monto, fecha_add = @fecha_add where id_add = @id_add
end

create procedure sp_Eliminar(
@id_add int 
)
as
begin
	delete from add_list where id_add = @id_add
end

////// a partir de aqui se termina esa parte////////////


/*