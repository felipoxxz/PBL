--Stores Procedures
use LancamentoBalistico
--Store Procedure (SP) para Criar o primeiro lancamento
CREATE OR ALTER PROCEDURE CriarLancamento
(@velocidadeMeteoro int,
@velocidadeTiro int,
@distancia int,
@altura decimal(5,2),
@angulo decimal(3,0))
AS
BEGIN
declare @Codlancamento int
--set @CodLancamento = (select count (*) from Lancamentos) + 1
set @Codlancamento = (select ISNULL(max(CodLancamento),0) + 1
						from Lancamentos)
insert into Lancamentos (VelocidadeMeteoro, VelocidadeTiro, CodLancamento, Distancia, Altura, Angulo)
			values (@velocidadeMeteoro,@velocidadeTiro, @Codlancamento, @distancia, @altura, @angulo)
END

--Store Procedure (SP) para Consultar Lançamento existente
CREATE OR ALTER PROCEDURE ConsultarLancamento
(@angulo int)
AS
if exists (select Angulo
from Lancamentos
where @angulo = Angulo)
	begin
		print ('Valor para o Angulo:' + cast(@angulo as varchar) + 'Já está cadastrado!')
		select *
		from Lancamentos
		where @angulo = Angulo
	end
else
	begin
		print('Valor não cadastrado no Banco de Dados!')
	end

--Store Procedure (SP) para RemoverLançamento existente
CREATE OR ALTER PROCEDURE RemoverLancamento
(@angulo int)
as
DELETE 
FROM Lancamentos
WHERE @angulo = Angulo

--Store Procedure (SP) para Atualizar Lancamento existente
CREATE OR ALTER PROCEDURE AtualizarLancamento
(@velocidadeMeteoro int,
@velocidadeTiro int,
@distancia int,
@altura decimal(5,2),
@angulo decimal(3,0),
@codLancamento int)
as
update Lancamentos
set VelocidadeMeteoro = @velocidadeMeteoro,
VelocidadeTiro = @velocidadeTiro,
CodLancamento = @codLancamento,
Distancia = @distancia,
Altura = @altura,
Angulo = @angulo

exec CriarLancamento 100, 110, 10, 15, 45
exec ConsultarLancamento 45
exec RemoverLancamento 45
exec AtualizarLancamento 1,3,4,5,6,6
