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
EST_TW varchar(10),
EST_CC varchar(10),
EST_AV varchar(10),
EST_PD varchar(10),
EST_OF varchar(10),
EST_WN varchar(10),
EST_REG varchar(10),
SGI_SW varchar(10),
SGI_RES varchar(10),
F_UL_MAN varchar(30) not null,
DEVU varchar(10),
ASIGN_DEVU varchar(30),
OBRA varchar(10)
);

create table USUARIOS(
ID int identity(1,1) primary key not null,
RUT varchar(12) UNIQUE not null,
NOMBRE_C varchar(40) not null,
CORREO varchar(30) not null,
UNE varchar(10)
);

create table MOVIMIENTOS_PC(
ID int identity(1,1) primary key not null,
RUT_USUARIO varchar(12) not null,
ID_PC varchar(30) not null,
TIPO_MOV varchar(30) not null,
FECHA_AS date,
FECHA_DV date,
FECHA_MOV date not null,
foreign key(RUT_USUARIO) references USUARIOS(RUT),
foreign key(ID_PC) references INV_PC(SERIAL)
);

insert into USUARIOS values('11.111.111-1','nombre completo','correo@aura.cl','OF');
insert into USUARIOS values('22.222.222-2','nombre completo','correo@aura.cl','O111');

insert into USUARIOS values('11','nombre completo','correo@aura.cl','OF');

insert into INV_PC values('XXX12345','ModeloX','MarcaX','AIO','Operacional','Con cargador.','11-1-2017','Ok','Ok','Ok','Ok','Nok','Ok','Ok','Cumple','No cumple','11-1-2017','No','n/a','O111');
insert into INV_PC values('XXX67890','ModeloX','MarcaX','Notebook','Operacional','Con cargador.','11-1-2017','Ok','Ok','Ok','Ok','Ok','Ok','Nok','Cumple','Cumple','11-1-2017','Si','Freddy Marquez','OF');

update INV_PC set ESTADO='Operativo' where SERIAL='XXX67890'
select * from USUARIOS
delete from INV_PC where SERIAL='XXX67890'