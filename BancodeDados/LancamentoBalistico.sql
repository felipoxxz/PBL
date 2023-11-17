--Criando Banco de Dados (BD)
Create database LancamentoBalistico
--Informando que utilizaremos o BD 'Lancamento Balistico' 
use LancamentoBalistico
GO
--Criando a tabela Lancamentos para armazenar as informações dos lancamentos
create table Lancamentos
(
CodLancamento int primary key,
VelocidadeMeteoro int,
VelocidadeTiro int,
Distancia decimal (10,2),
Angulo decimal(3,0),
Altura decimal (5,2)
)

create table Meteoro
(
CodMeteoro Int primary key,
VelocidadeMeteoro decimal,
AlturaMeteoro decimal
)
create table Colisao
(
CodColisao int primary key,
AlturaColisao Decimal,
DistanciaColisao decimal
)
create table Tiro
(
CodTiro int primary key,
AnguloTiro Decimal,
VelocidadeTiro Decimal,
)
