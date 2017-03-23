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

create table UNE(
ID int identity(1,1) primary key not null,
OBRA varchar(15) not null,
DESCRIPCION varchar(50)
);

create table DETMAN(
ID int identity(1,1) primary key not null,
SERIAL varchar(30) not null,
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

insert into UNE values('OF','Oficina Central');
insert into UNE values('O166','');
insert into UNE values('O172','');
insert into UNE values('O175','');
insert into UNE values('O168','');

insert into USUARIOS values('11111111-1','IVAN GUZMAN CAVIERES','correo@aura.cl','OF');
insert into USUARIOS values('00000000-0','Informatica','informatica@aura.cl','OF');
insert into USUARIOS values('22222222-2','FREDDY MARQUEZ','correo2@aura.cl','O111');
insert into USUARIOS values('33333333-3','HERNAN OPAZO','correo3@aura.cl','OF');

insert into LOGIN values('Admin','fa042c452bda6483cf4cfbb01d2b9df7bbc916903075612afa9a499f27820297','bOb5KyDlxHzbKz3j6EzoesS3mVtSyCGN2Niwxo9s2Rw=');
insert into LOGIN values('Informatica','eb65f4ea944f73683434d9ce606bac2b1ae100a17e5eb238d1424eb5b6805018','h4Q1vHLcplQjILcDoZu7yQJG+ZaoCCH4g56rfMepjyU==');
insert into LOGIN values('RRHH','60c920b7bb63dcc53a2d690cf37bd688ae3e0d48efde2ca2608dc951e3d8227c','AhwsPQI9NY1welnTpfKTFg6xBjuWuunMg7fzRzazJ2U=');
insert into LOGIN values('SGI','141d945fee0d78aceee3c6cf5d6c072a799572ebdebe9308f911487ecb65589b','LnSqbkSTV6VVcwm9a/2xaLuxUlrWAH710Ugq9zVUR6g=');


insert into INV_PC values('XXX12345','ModeloX','MarcaX','AIO','Operativo','Con cargador.','11-1-2017','true','true','true','true','false','true','true','true','false','11-1-2017','false','n/a','O111');
insert into INV_PC values('XXX67890','ModeloX','MarcaX','Notebook','Operativo','Con cargador.','11-1-2017','true','true','true','true','true','true','false','true','true','11-1-2017','true','Freddy Marquez','OF');
insert into INV_PC values('XXX67891','ModeloX','MarcaX','Notebook','Operativo','Con cargador.','11-1-2017','true','true','true','true','true','true','false','true','true','11-1-2017','true','Freddy Marquez','OF');


drop table INV_PC
update INV_PC set DEVU='Si' where SERIAL='XXX67890'
select * from USUARIOS
select * from LOGIN
delete from INV_PC where SERIAL='XXX67890'