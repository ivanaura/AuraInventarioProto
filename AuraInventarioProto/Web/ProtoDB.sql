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
FECHA_ADQ varchar(30),
EST_TW varchar(10),
EST_CC varchar(10),
EST_AV varchar(10),
EST_PD varchar(10),
EST_OF varchar(10),
EST_WN varchar(10),
EST_REG varchar(10),
SGI_SW varchar(10),
SGI_RES varchar(10),
F_UL_MAN varchar(30),
DEVU varchar(10),
ASIGN_DEVU varchar(30),
OBRA varchar(10)
);

create table USUARIOS(
RUT varchar(12) primary key not null,
NOMBRE_C varchar(40),
CORREO varchar(30),
UNE varchar(10)
);

create table MOVIMIENTOS_PC(
ID int identity(1,1) primary key not null,
RUT_USUARIO varchar(12),
ID_PC varchar(30),
TIPO_MOV varchar(30),
FECHA_AS date,
FECHA_DV date,
FECHA_MOV date,
foreign key(RUT_USUARIO) references USUARIOS(RUT),
foreign key(ID_PC) references INV_PC(SERIAL)
);

insert into USUARIOS values('11.111.111-1','nombre completo','correo@aura.cl','OF');
insert into USUARIOS values('22.222.222-2','nombre completo','correo@aura.cl','O111');

insert into USUARIOS values('11','nombre completo','correo@aura.cl','OF');

insert into INV_PC values('XXX12345','ModeloX','MarcaX','AIO','Operacional','Con cargador.','11-1-2017','Ok','Ok','Ok','Ok','Nok','Ok','Ok','Cumple','No cumple','11-1-2017','No','n/a','O111');
insert into INV_PC values('XXX67890','ModeloX','MarcaX','Notebook','Operacional','Con cargador.','11-1-2017','Ok','Ok','Ok','Ok','Ok','Ok','Nok','Cumple','Cumple','11-1-2017','Si','Freddy Marquez','OF');

update INV_PC set ESTADO='Operativo' where SERIAL='XXX67890'
select * from INV_PC
delete from INV_PC where SERIAL='XXX67890'