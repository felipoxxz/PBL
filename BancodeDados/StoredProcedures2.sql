--Stores Procedures
use LancamentoBalistico
--Store Procedure (SP) para Criar o primeiro lancamento
CREATE OR ALTER PROCEDURE CriarLancamento
(@velocidadeMeteoro int,
@velocidadeTiro int,
@distanciaColisao int,
@alturaColisao decimal(5,2),
@anguloTiro decimal(3,0),
@alturaMeteoro decimal (6,2))
AS
BEGIN
declare @Codcolisao int
--set @CodColisao = (select count (*) from Colisao) + 1
set @Codcolisao = (select ISNULL(max(Codcolisao),0) + 1
						from Colisao)
declare @CodMeteoro int
--set @CodMeteoro = (select count (*) from Meteoro) + 1
set @CodMeteoro = (select ISNULL(max(CodMeteoro),0) + 1
						from Meteoro)
declare @CodTiro int
--set @CodTiro = (select count (*) from Tiro) + 1
set @CodTiro = (select ISNULL(max(CodTiro),0) + 1
						from Tiro)

--INSERIR VALROES NA TABELA COLISÃO
insert into Colisao (CodColisao,AlturaColisao, DistanciaColisao)
			values (@Codcolisao,@alturaColisao,  @distanciaColisao)

--INSERIR VALROES NA TABELA TIRO
insert into Tiro (CodTiro, AnguloTiro,VelocidadeTiro,CodColisao)
			values (@CodTiro, @anguloTiro, @velocidadeTiro,@Codcolisao)

--INSERIR VALROES NA TABELA METEORO
insert into Meteoro (CodMeteoro, VelocidadeMeteoro, AlturaMeteoro, CodColisao)
			values (@CodMeteoro, @velocidadeMeteoro, @alturaMeteoro,@Codcolisao)
END

--Store Procedure (SP) para Consultar Lançamento existente
CREATE OR ALTER PROCEDURE ConsultarLancamento
(@anguloTiro int)
AS
if exists (select AnguloTiro
from tiro
where @anguloTiro = AnguloTiro)
	begin

	--Variaveis para armazenar valores da tabela
	declare @CodTiro int
	set @CodTiro = (select CodTiro
						from Tiro
						where @anguloTiro = AnguloTiro)
	declare @VelocidadeTiro decimal
	set @VelocidadeTiro = (select VelocidadeTiro
						from Tiro
						where @anguloTiro = AnguloTiro)
		print ('Valor para o Angulo: ' + cast(@anguloTiro as varchar) + 'º Já está cadastrado!')
		print ''
		print ('Valor do CodTiro: ' + cast(@CodTiro as varchar))
		print ''
		print ('Valor da Velocidade: ' + cast(@VelocidadeTiro as varchar) + 'm/s')	
	end
else
	begin
		print('Valor não cadastrado no Banco de Dados!')
	end

--Store Procedure (SP) para Remover valores das tabelas
CREATE OR ALTER PROCEDURE RemoverLancamento
(@CodColisao int)
as
DELETE 
FROM Tiro
WHERE @CodColisao = CodColisao

DELETE 
FROM Colisao
WHERE @CodColisao = CodColisao

DELETE 
FROM Meteoro
WHERE @CodColisao = CodColisao

--Store Procedure (SP) para Atualizar tabelas
CREATE OR ALTER PROCEDURE AtualizarLancamento
(@velocidadeMeteoro decimal,
@velocidadeTiro decimal,
@distanciaColisao decimal,
@alturaColisao decimal(10,2),
@anguloTiro decimal(10,0),
@codColisao int,
@alturaMeteoro decimal (6,2))
as
--ATUALIZA TABELA COLISÃO
	update Colisao
	set 
	--CodColisao = @codColisao,
	AlturaColisao = @alturaColisao,
	DistanciaColisao = @distanciaColisao
	where CodColisao = @codColisao

--ATUALIZA TABELA TIRO
	update Tiro
	set 
	AnguloTiro = @anguloTiro,
	VelocidadeTiro = @velocidadeTiro
	where CodColisao = @codColisao


--ATUALIZA TABELA TIRO
	update Meteoro
	set 
	VelocidadeMeteoro = @velocidadeMeteoro,
	AlturaMeteoro = @alturaMeteoro
	where CodColisao = @codColisao


-------------------  COLOCAR   --------------
------------------- VARIAVEIS --------------
-------------------    C#     --------------
exec CriarLancamento 1,2,30,4,5,6
exec ConsultarLancamento 5
exec RemoverLancamento 1
exec AtualizarLancamento 0,0,0,1,2,1,7
