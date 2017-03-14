use master
drop database AuraInventarioProtoDB;

Create database AuraInventarioProtoDB;
use AuraInventarioProtoDB;

create table INV_PC(
ID int identity(1,1) primary key not null,
SERIAL varchar(30) UNIQUE not null,
MODELO varchar(30) not null,
MARCA varchar(30) not null,
TIPO varchar(10) not null,
ESTADO varchar(30) not null,
OBS varchar(255),
FECHA_ADQ varchar(30) not null,
EST_TW  Bit,
EST_CC  Bit,
EST_AV  Bit,
EST_PD  Bit,
EST_OF  Bit,
EST_WN  Bit,
EST_REG Bit,
SGI_SW  Bit,
SGI_RES Bit,
F_UL_MAN varchar(30) not null,
DEVU varchar(10),
ASIGN_DEVU varchar(30),
OBRA varchar(10)
);

create table USUARIOS(
ID int identity(1,1) primary key not null,
RUT varchar(12) UNIQUE not null,
NOMBRE_C varchar(40) not null,
CORREO varchar(30) UNIQUE not null,
UNE varchar(10)
);

create table MOVIMIENTOS_PC(
ID int identity(1,1) primary key not null,
RUT_USUARIO varchar(12) not null,
ID_PC varchar(30) not null,
TIPO_MOV varchar(30) not null,
FECHA_AS varchar(30),
FECHA_DV varchar(30),
FECHA_MOV varchar(30) not null,
foreign key(RUT_USUARIO) references USUARIOS(RUT),
foreign key(ID_PC) references INV_PC(SERIAL)
);

create table LOGIN(
ID int identity(1,1) primary key not null,
NOMBRE varchar(30) not null,
PASS nvarchar(128) not null,
SALT nvarchar(128)  not null
);

insert into USUARIOS values('11111111-1','nombre completo','correo@aura.cl','OF');
insert into USUARIOS values('22222222-2','nombre completo','correo2@aura.cl','O111');
insert into USUARIOS values('33333333-3','nombre completo','correo3@aura.cl','OF');

insert into LOGIN values('Admin','trapecio23','a');


insert into INV_PC values('XXX12345','ModeloX','MarcaX','AIO','Operativo','Con cargador.','11-1-2017','true','true','true','true','false','true','true','true','false','11-1-2017','No','n/a','O111');
insert into INV_PC values('XXX67890','ModeloX','MarcaX','Notebook','Operativo','Con cargador.','11-1-2017','true','true','true','true','true','true','false','true','true','11-1-2017','Si','Freddy Marquez','OF');


drop table INV_PC
update INV_PC set ESTADO='Operativo' where SERIAL='XXX67890'
select * from USUARIOS
select * from LOGIN
delete from INV_PC where SERIAL='XXX67890'