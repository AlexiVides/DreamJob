create database BdDreamJob
use BdDreamJob

create table Rol
(
idRol int primary key identity(1,1) not null,
rol nvarchar(50) not null,
estado nvarchar(50) not null
);

create table Cargo
(
idCargo int primary key identity(1,1) not null,
nombre nvarchar(50) not null,
estado nvarchar(50) not null
);

create table Usuario
(
idUsuario int primary key identity(1,1) not null,
correo nvarchar(100) not null,
contra nvarchar(50) not null,
estado nvarchar(50) not null,
idRol int foreign key references Rol(idRol)
);

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

create table Curriculum
(
idCurriculum int primary key identity(1,1) not null,
nombre nvarchar(100) not null,
apellido nvarchar(100) not null,
edad int not null,
genero nvarchar(100) not null,
direccion nvarchar(1000) not null,
telefono nvarchar(12) not null,
dui int not null,
licencia nvarchar(50) not null,
nivelAcademico nvarchar(100) not null,
historialAcademico nvarchar(1000) not null,
referenciaPers nvarchar(1000) not null,
experienciaLab nvarchar(1000) not null,
descripcion nvarchar(1000) not null,
correoOpc nvarchar(100) not null,
segundoIdioma nvarchar(100) not null,
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
categoria nvarchar(100) not null,
Salario float null,
prestaciones varchar(100) null,
estado nvarchar(50) not null,
idCargo int foreign key references Cargo(idCargo),
idDatosEmpresa int foreign key references DatosEmpresa(idDatosEmpresa)
);

create table Aplicacion
(
idAplicacion int primary key identity(1,1) not null,
idCurriculum int foreign key references Curriculum(idCurriculum),
idOfertaEmpleo int foreign key references OfertaEmpleo(idOfertaEmpleo)
);