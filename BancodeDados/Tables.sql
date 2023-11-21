use LancamentoBalistico
--Criando Banco de Dados (BD)
Create database LancamentoBalistico
--Informando que utilizaremos o BD 'Lancamento Balistico' 
use LancamentoBalistico
GO

--Criando a tabela Meteoro para armazenar as informações do Meteoro
create table Meteoro
(
CodMeteoro int primary key,
VelocidadeMeteoro decimal,
AlturaMeteoro decimal,
CodColisao int foreign key references dbo.Colisao(CodColisao)
)

--Criando a tabela Colisão para armazenar as informações da Colisão
create table Colisao
(
CodColisao int primary key,
AlturaColisao Decimal,
DistanciaColisao decimal,
--CodMeteoro int foreign key references dbo.meteoro(CodMeteoro),
--CodTiro int foreign key references dbo.Tiro(CodTiro)
)

--Criando a tabela Tiro para armazenar as informações do Tiro
create table Tiro
(
CodTiro int primary key,
AnguloTiro Decimal,
VelocidadeTiro Decimal,
CodColisao int foreign key references dbo.Colisao(CodColisao)
)
