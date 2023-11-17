--Trigger para Inserir lancamento
CREATE OR ALTER TRIGGER trg_insertLancamento on colisao
for insert
as
IF (SELECT COUNT(*)
	FROM inserted) = 1
	begin
		PRINT('Lançamento inserido com sucesso!')
	end
--Trigger para deletar lancamento
CREATE OR ALTER TRIGGER trg_deleteLancamento on colisao
for delete
as
	if (select count(*)
	from deleted ) >= 1
		print('Lançamento Removido!')

--Trigger para atualizar lancamento
Create or alter trigger trg_updateLancamento on colisao
for update
as
if (select count(*)
	from deleted) <> 0
	begin
		print('Os seguintes dados foram modificados')
		select * from deleted
		select * from inserted
	end
