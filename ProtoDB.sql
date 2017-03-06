use master
drop database AuraInventarioProtoDB;

Create database AuraInventarioProtoDB;
use AuraInventarioProtoDB;

create table INV_PC(
SERIAL varchar(30) primary key not null,
MODELO varchar(30),
MARCA varchar(30),
TIPO varchar(10),
ESTADO varchar(30),
OBS varchar(255),
FECHA_ADQ date,
EST_TW varchar(10),
EST_CC varchar(10),
EST_AV varchar(10),
EST_PD varchar(10),
EST_OF varchar(10),
EST_WN varchar(10),
EST_REG varchar(10),
SGI_SW varchar(10),
SGI_RES varchar(10),
F_UL_MAN date,
DEVU varchar(10),
ASIGN_DEVU varchar(10),
OBRA varchar(10)
);

create table USUARIOS(
ID int identity(1,1) primary key not null,
NOMBRE_C varchar(40),
CORREO varchar(30),
UNE varchar(10)
);

create table MOVIMIENTOS_PC(
ID int identity(1,1) primary key not null,
ID_USUARIO int,
ID_PC varchar(30),
TIPO_MOV varchar(30),
FECHA_AS date,
FECHA_DV date,
FECHA_MOV date,
foreign key(ID_USUARIO) references USUARIOS(ID),
foreign key(ID_PC) references INV_PC(SERIAL)
);

