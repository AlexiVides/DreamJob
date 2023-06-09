create database BdDreamJob
use BdDreamJob
create table Rol
(
idRol int primary key identity(1,1) not null,
rol int not null,
estado nvarchar(50) not null
);

insert into Rol values(1,'Activo');
insert into Rol values(2,'Activo');


create table Categoria
(
idCategoria int primary key identity(1,1) not null,
nombre nvarchar(50) not null,
estado nvarchar(50) not null
);

insert into Categoria Values('Mercadeo','Activo')
insert into Categoria Values('Tecnologia','Activo')
insert into Categoria Values('Recursos Humanos','Activo')
insert into Categoria Values('Contabilidad','Activo')
insert into Categoria Values('Salud','Activo')
insert into Categoria Values('Construccion','Activo')

create table Usuario
(
idUsuario int primary key identity(1,1) not null,
correo nvarchar(100) not null,
contra nvarchar(50) not null,
estado nvarchar(50) not null,
idRol int foreign key references Rol(idRol)
);


create table Curriculum
(
idCurriculum int primary key identity(1,1) not null,
nombre nvarchar(100) not null,
apellido nvarchar(100) not null,
edad int not null,
genero nvarchar(100) not null,
direccion nvarchar(1000) not null,
telefono nvarchar(12) not null,
dui nvarchar(10) not null,
licencia nvarchar(50) not null,
nivelAcademico nvarchar(100) not null,
historialAcademico nvarchar(1000) not null,
referenciaPers nvarchar(1000) not null,
experienciaLab nvarchar(1000) not null,
descripcion nvarchar(1000) not null,
correoOpc nvarchar(100) not null,
segundoIdioma nvarchar(100) not null,
imagen image null,
estado nvarchar(50) not null,
idUsuario int foreign key references Usuario(idUsuario)
);

select * from Usuario


create table DatosEmpresa
(
idDatosEmpresa int primary key identity(1,1) not null,
nombre nvarchar(100) not null,
direccion nvarchar(1000) not null,
telefono nvarchar(12) not null,
licencia nvarchar(100) not null,
descripcion nvarchar(1000) not null,
estado nvarchar(50) not null,
idUsuario int foreign key references Usuario(idUsuario)
);



create table OfertaEmpleo
(
idOfertaEmpleo int primary key identity(1,1) not null,
nombre nvarchar(300) not null,
nVacantes int not null,
descripcion nvarchar(1000) not null,
requerimientos nvarchar(1000) not null,
funciones nvarchar(1000) not null,
Salario float null,
prestaciones varchar(100) null,
cargo nvarchar(50) not null,
estado nvarchar(50) not null,
idCategoria int foreign key references Categoria(idCategoria),
idDatosEmpresa int foreign key references DatosEmpresa(idDatosEmpresa)
);

create table Aplicacion
(
idAplicacion int primary key identity(1,1) not null,
idCurriculum int foreign key references Curriculum(idCurriculum),
idOfertaEmpleo int foreign key references OfertaEmpleo(idOfertaEmpleo)
);

